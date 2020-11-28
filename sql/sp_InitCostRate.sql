-- ================================================ 
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO 
CREATE OR ALTER PROCEDURE sp_InitCostRate
	@TradeDate datetime
AS
BEGIN 
	SET NOCOUNT ON;
	
insert into CostRate
select distinct @TradeDate,  ProviderTradingProfileID , 0, 
CONVERT(datetime, SYSDATETIMEOFFSET() AT TIME ZONE 'Taipei Standard Time'),
CONVERT(datetime, SYSDATETIMEOFFSET() AT TIME ZONE 'Taipei Standard Time')
from Txn  
where TradeDate = @TradeDate 
and ProviderTradingProfileID is not null
and ProviderTradingProfileID not in (
select ProviderTradingProfileID from CostRate 
where TradeDate = @TradeDate
)

END
GO
