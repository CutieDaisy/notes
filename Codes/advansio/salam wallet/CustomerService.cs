    public async Task<IActionResult> ServicePayment(
        ServicePaymentRequest? model,
        TransactionType transactionType,
        TransactionRequestType transactionRequestType,
        ServiceType serviceType)
    {
        try
        {
            if (model is null)
                return this.ToBadRequestResult(ResponseCodeType.InvalidModel, "Invalid model");

            if (model.AccountNumber?.Trim() is null or { Length: 0 })
                return this.ToBadRequestResult(ResponseCodeType.InvalidModel, "Account number is required");


            Logger.LogInformation($"{serviceType} Request Model {0}",
                Newtonsoft.Json.JsonConvert.SerializeObject(model));


            if (transactionType is TransactionType.BankToWalletTransfer or TransactionType.WalletToBankTransfer
                or TransactionType.IntraBankTransfer)
            {
                var actionResult = await BankApiRepoService.GetCustomerByAccountNumber(model.AccountNumber);
                if (actionResult is OkObjectResult o)
                {
                    var value = o.Value.Map<BaseResponse<CoreBankingBaseResponse>>();
                    if (value?.ResponseCode != ResponseCodeType.Successful.GetCode())
                    {
                        return actionResult;
                    }
                }
            }


            // validate the amount
            if (model.Amount <= 0)
                return this.ToBadRequestResult(ResponseCodeType.InvalidModel, "Invalid amount");

            if (model.Pin?.Trim() is null or "")
                return this.ToBadRequestResult(ResponseCodeType.InvalidModel, "No pin provided");

            // var validPin = model.Pin
            //     .VerifyEncryption(LoggedUserEntity.Customer.PinSalt, LoggedUserEntity.Customer.PinHash);
            //
            // if (!validPin)
            //     return this.ToOkRequestResult(ResponseCodeType.InvalidModel, "Invalid Pin");


            var validPin = UserService.ValidateLoggerTransactionPin(model.Pin.ToString());

            if (!validPin)
                return this.ToBadRequestResult(ResponseCodeType.InvalidModel, "Invalid Pin");


            var balance = DbContext.CustomerBalances.AsNoTracking().FirstOrDefault(b =>
                b.CustomerId == LoggedUserEntity.CustomerId);

            if (balance.FloatingBalance < model.Amount)
                this.ToOkRequestResult(ResponseCodeType.InsufficientFunds, "Insufficient funds");

            ServiceDetail detail = new()
            {
                AccountName = model.AccountName,
                AccountNumber = model.AccountNumber,
                ProductCode = model.ProductCode,
                ProviderCode = model.ProviderCode,
                Provider = model.Provider,
                Amount = model.Amount,
                Narration = model.Narration,
                ServiceType = serviceType, // ServiceType.Airtime,
                Date = DateTime.Now
            };


            var (transaction, error) = CreateTransaction(
                null, model.Amount,
                transactionRequestType, // TransactionRequestType.pr_airtime_purchase,
                transactionType, // TransactionType.AirtimePurchase,
                false,
                model.Narration,
                model.Latitude,
                model.Longitude, serviceDetail: detail, saveChanges: true);


            if (error is not null)
            {
                Logger.LogError("Transaction creation failed : " + error);
                return this.ToOkRequestResult(ResponseCodeType.RecordCreationFailed, error);
            }

            if (transaction is null)
            {
                Logger.LogError("Transaction was not saved");
                return this.ToOkRequestResult(ResponseCodeType.RecordCreationFailed, "Transaction creation failed");
            }

            // bool transactionSuccess = false;
            // INFO : Make API call here to Service Provider to process the transaction
            // API CALL HERE


            var (_, bankRef) = await BankApiRepoService.ProcessTransfer(new()
            {
                amt = transaction.Amount,
                refNo = transaction.TransactionReference,
                ccy = AppConfig.DefaultCurrency,
                frmAcct = transaction.DebitAccountNumber,
                toAcct = AppConfig.SuspenseAccount,
                desc = transaction.Narration
            });


            Task.Delay(2000).Wait(); // Wait for the transfer to complete

            var (errorMsg, isSuccessful) =
                await BankApiRepoService.GetTransactionStatus(transaction.TransactionReference);

            // // COMMENT THIS OUT -----
            // bool isSuccessful = true;
            // var errorMsg = "";
            // var bankRef = "";
            // // ----------------------

            var status = ((isSuccessful) ? ResponseStatus.Successful : ResponseStatus.Failed);


            // // var response = ServiceProviderService.PostTransaction(transaction);
            // transactionSuccess = true; // Must be set to true for now
            // // -------------------------------------------------------------------------

            transaction.ApprovalDate = DateTime.Now;
            transaction.ApprovedByUserId = LoggedUserId;
            transaction.ApprovedByUserName = LoggedUserName;
            // transaction.ResponseCode = response.StatusCode;
            // transaction.ResponseMessage = response.message;
            transaction.Status = status;

            if (isSuccessful)
            {
                balance.FloatingBalance -= model.Amount;
                balance.RunningBalance += model.Amount;
                DbContext.CustomerBalances.Update(balance);
            }

            DbContext.SaveChanges();

            return DefaultResult.ToOkRequestResult(ResponseCodeType.Successful);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            return this.ToOkRequestResult(ResponseCodeType.InternalServerError, ex.Message);
        }
    }
