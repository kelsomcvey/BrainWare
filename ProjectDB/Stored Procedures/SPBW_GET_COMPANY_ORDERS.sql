-- =============================================
-- Author:		Kieran McVey
-- Create date: 2nd May 2024
-- Description:	Get all orders for companyId with Total cost of order
-- =============================================
CREATE PROCEDURE [dbo].[SPBW_GET_COMPANY_ORDERS] 
	
	(@CompanyId int)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT c.name AS CompanyName,
       o.description AS Description,
       o.order_id AS OrderId,
       (SELECT SUM(op2.price * op2.quantity)
        FROM orderproduct op2
        WHERE op2.order_id = o.order_id) AS OrderTotal
	FROM company c
	INNER JOIN [order] o ON c.company_id = o.company_id
	where c.company_id = @CompanyId

END