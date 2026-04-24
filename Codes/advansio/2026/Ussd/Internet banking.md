graph TD
    Start([User dials USSD Code]) --> CheckIB[Check if enrolled on Internet Banking]

    CheckIB -- No --> MsgIB[Display: 'Please enroll on Internet Banking to use USSD']
    MsgIB --> End([End Session])

    CheckIB -- Yes --> CheckUssdActive[Check if IsUssdActive column is TRUE]

    CheckUssdActive -- No --> MsgContact[Display: 'USSD not active. Please contact the bank for activation']
    MsgContact --> End

    CheckUssdActive -- Yes --> MainMenu[Display Main Menu: 1. Balance 2. Transfer 3. Airtime]

    %% BALANCE FLOW
    MainMenu --> BalanceSel{User selects Balance?}
    BalanceSel -- Yes --> ShowAccounts[Display list of user accounts]
    ShowAccounts --> SelectAccount[User selects account]
    SelectAccount --> PromptPinBal[Prompt for Transaction PIN]
    PromptPinBal --> AuthBal{PIN Correct?}
    AuthBal -- No --> FailBal[Display: Invalid PIN]
    FailBal --> End
    AuthBal -- Yes --> ShowBalance[Display Account Balance]
    ShowBalance --> End

    %% TRANSFER FLOW
    MainMenu --> TransferSel{User selects Transfer?}
    TransferSel -- Yes --> ShowAccountsT[Display list of user accounts]
    ShowAccountsT --> SelectSource[User selects source account]
    SelectSource --> EnterBeneficiary[Enter beneficiary account number]
    EnterBeneficiary --> EnterAmount[Enter transfer amount]
    EnterAmount --> SelectBank[Select destination bank]
    SelectBank --> PromptPinTr[Prompt for Transaction PIN]
    PromptPinTr --> AuthTr{PIN Correct?}
    AuthTr -- No --> FailTr[Display: Invalid PIN]
    FailTr --> End
    AuthTr -- Yes --> ProcessTransfer[Process Transfer]
    ProcessTransfer --> SuccessMsg[Display: Transfer Successful]
    SuccessMsg --> End

    %% AIRTIME FLOW
    MainMenu --> AirtimeSel{User selects Airtime?}
    AirtimeSel -- Yes --> SelectAccountA[Select debit account]
    SelectAccountA --> EnterPhone[Enter phone number]
    EnterPhone --> EnterAmtAir[Enter airtime amount]
    EnterAmtAir --> PromptPinAir[Prompt for Transaction PIN]
    PromptPinAir --> AuthAir{PIN Correct?}
    AuthAir -- No --> FailAir[Display: Invalid PIN]
    FailAir --> End
    AuthAir -- Yes --> ProcessAir[Process Airtime Purchase]
    ProcessAir --> SuccessAir[Display: Airtime Successful]
    SuccessAir --> End