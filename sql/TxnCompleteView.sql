SELECT t1.id, t1.ReferenceNo, t1.Type, t1.LinkedDepositID, t1.TradeDate, t1.ClientTradingProfileID, t1.Status, t1.ClientPriceRate, t1.ClientCurrencyPairID, t1.ClientDfrRate, t1.ClientUniqueDfr, t1.ClientExRate, t1.ClientCurrencyIDIn, 
                  t1.ClientAmountIn, t1.ClientCurrencyIDOut, t1.ClientAmountOut, t1.ClientPayOutAccountID, t1.ProviderTradingProfileID, t1.ProviderCurrencyID, t1.ProviderCostDate, t1.ProviderCostRate, t1.ProviderExpectedPayInAmount, 
                  t1.ProviderBankAccountID, t1.DateTimeModified, t1.DateTimeAdded, t1.LinkedDepositReferenceNo, t1.ClientTradingProfileName, t1.ClientCurrencyPairName, t1.ClientCurrencyNameIn, t1.ClientCurrencyNameOut, 
                  t1.ClientPayOutAccountName, t1.ProviderTradingProfileName, t1.ProviderCurrencyName, t1.ProviderBankAccountName, ISNULL(t2.ProviderTradingProfileID, t1.ProviderTradingProfileID) AS LinkedProviderTradingProfileID, 
                  ISNULL(t2.ProviderTradingProfileName, t1.ProviderTradingProfileName) AS LinkedProviderTradingProfileName, ISNULL(t2.ProviderCurrencyID, t1.ProviderCurrencyID) AS LinkedProviderCurrencyID, ISNULL(t2.ProviderCurrencyName, 
                  t1.ProviderCurrencyName) AS LinkedProviderCurrencyName, ISNULL(t2.ProviderCostDate, t1.ProviderCostDate) AS LinkedProviderCostDate, ISNULL(t2.ProviderCostRate, t1.ProviderCostRate) AS LinkedProviderCostRate, 
                  ISNULL(t2.ProviderExpectedPayInAmount, t1.ProviderExpectedPayInAmount) AS LinkedProviderExpectedPayInAmount, ISNULL(t2.ProviderBankAccountID, t1.ProviderBankAccountID) AS LinkedProviderBankAccountID, 
                  ISNULL(t2.ProviderBankAccountName, t1.ProviderBankAccountName) AS LinkedProviderBankAccountName,
				  isnull(t1.ClientAmountOut,0) - isnull(pout.ClientPayoutAmount,0) as ClientPayoutAmountOutstanding 
FROM     
dbo.TxnView AS t1 LEFT OUTER JOIN
dbo.TxnView AS t2 ON t1.LinkedDepositID = t2.id LEFT OUTER JOIN
(select TxnID, sum(ClientPayoutAmount) as ClientPayoutAmount from Payout group by TxnID) pout on t1.id = pout.TxnID








 