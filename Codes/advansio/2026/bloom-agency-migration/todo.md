1. Add the ff Data To New Agency Db:
    # Application Roles :
    * SuperAgent
    * SubAgent
    * FixAgent
    * Customer

    # Tier :
    * Tier1
    * Tier2

    # TierBenefit :
    * TierBenefit with TierId of 1
    * TierBenefit with TierId of 2
    
    # SystemWallet
    * SystemAccount with AccountType of 0

2. Change the data type for the Agent field CommissionAccountNumberType to int in the New Agent Banking Db

3. We had to modify the WalletTransaction entity field for GST from string to decimal in the Internet Banking Db

4. We had to modify the Transaction table's Narration field length from 150 to 450 in the New Agent Banking DB

5. Create Retail View on the Retail DB : vw_combined_users
    SCRIPT:
    CREATE view [dbo].[vw_combined_users] as 
        select TRIM(lower(UserName)) as username, 'Bank' as Channel from [RETAIL_DB].dbo.Customers
        union
        select trim(lower(au.NormalizedUserName)) as username , 'Wallet' as Channel  from [AgencyBanking].dbo.ApplicationUsers au  where au.Id in (select ApplicationUserId from [AgencyBanking].dbo.Customers) ;
        GO