Create PROCEDURE [dbo].[GetOrdersInSelectedPosition] @List NVARCHAR(MAX), @Position int
AS

select [ORD_NUM]
      ,[ORD_AMOUNT]
      ,[ADVANCE_AMOUNT]
      ,[ORD_DATE]
      ,[CUST_CODE]
      ,[AGENT_CODE]
      ,[ORD_DESCRIPTION] from
		(select *,  ( Rank() over (Partition BY AGENT_CODE ORDER BY AGENT_CODE, ord_date desc ) ) As _xRank 
		from ORDERS
		where AGENT_CODE in 
		(SELECT * FROM dbo.splitstring(@List))
		) A where _xRank = @Position
GO

