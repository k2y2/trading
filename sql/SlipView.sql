SELECT s.id, s.ReferenceNo, s.SlipDate, s.ProviderTradingProfileID, s.TxnID, s.AccountBankAccountID, s.SenderID, s.SlipAmount, s.ActualAmount, s.DateTimeModified, s.DateTimeAdded, p.ProviderTradingProfileName, 
                  t.ReferenceNo AS TxnReferenceNo, a.AccountName AS AccountBankAccountName, sdr.SenderName,
				  cp.CurrencyName2
FROM     dbo.Slip AS s LEFT OUTER JOIN
                  dbo.ProviderTradingProfile AS p ON s.ProviderTradingProfileID = p.id LEFT OUTER JOIN
				  dbo.CurrencyPairView cp on p.CurrencyPairID = cp.id LEFT OUTER JOIN
                  dbo.Txn AS t ON s.TxnID = t.id LEFT OUTER JOIN
                  dbo.AccountBankAccount AS a ON s.AccountBankAccountID = a.id LEFT OUTER JOIN
                  dbo.Sender AS sdr ON s.SenderID = sdr.id