-- =============================================
-- Author:		Kieran McVey
-- Create date: 2nd May 2024
-- Description:	Get all products for orderId 
-- =============================================
CREATE PROCEDURE [dbo].[SPBW_GET_ORDER_PRODUCTS] --1
	(	
	@OrderId int
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT 
		op.price as Price
		, op.order_id as OrderId 
		, op.product_id as ProductId 
		, op.quantity as Quantity
		, p.name as ProductName
    FROM orderproduct op 
    INNER JOIN product p on op.product_id = p.product_id 
    WHERE op.order_id = @OrderId

END
