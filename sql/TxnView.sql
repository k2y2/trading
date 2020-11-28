SELECT t.id, t.ReferenceNo, t.Type, t.LinkedDepositID, t.TradeDate, t.ClientTradingProfileID, t.Status, t.ClientPriceRate, t.ClientCurrencyPairID, t.ClientDfrRate, t.ClientUniqueDfr, t.ClientExRate, t.ClientCurrencyIDIn, t.ClientAmountIn, 
                  t.ClientCurrencyIDOut, t.ClientAmountOut, t.ClientPayOutAccountID, t.ProviderTradingProfileID, t.ProviderCurrencyID, t.ProviderCostDate, t.ProviderCostRate, t.ProviderExpectedPayInAmount, t.ProviderBankAccountID, 
                  t.DateTimeModified, t.DateTimeAdded, t2.ReferenceNo AS LinkedDepositReferenceNo, cl.ClientID, cl.ClientTradingProfileName, cp.CurrencyPairName AS ClientCurrencyPairName, cri.CurrencyName AS ClientCurrencyNameIn, 
                  cro.CurrencyName AS ClientCurrencyNameOut, ac.AccountName AS ClientPayOutAccountName, p.ProviderTradingProfileName, crp.CurrencyName AS ProviderCurrencyName, pb.AccountName AS ProviderBankAccountName,
				t.Remark,t.PayoutDone,
				t.PayoutDoneByID,up.UserName as PayoutDoneBy,
				t.MiniAccount,t.OvernightDeposit,
				t.ModifiedByID,um.UserName as ModifiedBy,
				t.AddedByID,ua.UserName as AddedBy,
				
	iif(isnull(t.ClientPriceRate,0)=0,'PRICE ','')+
	iif(isnull(t.ClientDfrRate,0)=0,'DFR ','')+
	iif(isnull(t.ProviderCostRate,0)=0,'COST ','') as Alert
	 
 
FROM     dbo.Txn AS t LEFT OUTER JOIN
                  dbo.Txn AS t2 ON t.LinkedDepositID = t2.id LEFT OUTER JOIN
                  dbo.ClientTradingProfile AS cl ON t.ClientTradingProfileID = cl.id LEFT OUTER JOIN
                  dbo.CurrencyPair AS cp ON t.ClientCurrencyPairID = cp.id LEFT OUTER JOIN
                  dbo.Currency AS cri ON t.ClientCurrencyIDIn = cri.id LEFT OUTER JOIN
                  dbo.Currency AS cro ON t.ClientCurrencyIDOut = cro.id LEFT OUTER JOIN
                  dbo.Account AS ac ON t.ClientPayOutAccountID = ac.id LEFT OUTER JOIN
                  dbo.ProviderTradingProfile AS p ON t.ProviderTradingProfileID = p.id LEFT OUTER JOIN
                  dbo.Currency AS crp ON t.ProviderCurrencyID = crp.id LEFT OUTER JOIN
                  dbo.ProviderBankAccount AS pb ON t.ProviderBankAccountID = pb.id LEFT OUTER JOIN
				  dbo.[User] as up on t.PayoutDoneByID = up.id LEFT OUTER JOIN
				  dbo.[User] as um on t.ModifiedByID = um.id LEFT OUTER JOIN
				  dbo.[User] as ua on t.AddedByID = ua.id 