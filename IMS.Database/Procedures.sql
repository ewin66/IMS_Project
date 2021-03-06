ALTER TABLE [dbo].[Product] DROP CONSTRAINT [CK_Product_Barcode]
GO
ALTER FUNCTION dbo.fn_check_UniqueBarcode(@Barcode VARCHAR(13))
RETURNS bit
AS
BEGIN
-- 1.Proveruva dali eden isti Person se pojavuva so 2 ili poveke korisnicki iminja za RETAIL	
IF EXISTS (
			SELECT 'x'
			FROM   Product p (NOLOCK)
		    WHERE @Barcode IS NOT NULL
		    AND @Barcode != ''
		    AND (
		    	p.BarCode1 = @Barcode 
			    OR p.BarCode2 = @Barcode
			    OR p.BarCode3 = @Barcode
			    OR p.BarCode4 = @Barcode
			    )
		  )
    RETURN 1

RETURN 0
END
GO
ALTER TABLE [dbo].[Product]  WITH NOCHECK ADD  CONSTRAINT [CK_Product_Barcode] CHECK  (([dbo].[fn_check_UniqueBarcode](Barcode1)=(0)))
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [CK_Product_Barcode]
GO
/*====================================
* GetProduct null, N'пире', null
* ====================================*/
ALTER PROCEDURE [dbo].[GetProduct]    
 @ProductId INT = NULL,    
 @ProductName NVARCHAR(22) = NULL,    
 @BarCode VARCHAR(13) = NULL
AS 

SELECT *
FROM Product p
WHERE p.ProductId = ISNULL(@ProductId, p.ProductId)
AND p.ProductName LIKE ISNULL('%'+@ProductName+'%', p.ProductName)  
AND 
(
	p.BarCode1 = ISNULL(@BarCode, p.BarCode1) OR
	p.BarCode2 = ISNULL(@BarCode, p.BarCode2) OR
	p.BarCode3 = ISNULL(@BarCode, p.BarCode3) OR
	p.BarCode4 = ISNULL(@BarCode, p.BarCode4)
)
GO
/*====================================
* AddProduct
* ====================================*/
CREATE PROCEDURE [dbo].[AddProduct]    
	@ProductId	INT,
	@CategoryId	INT,
	@SuplierId	INT,
	@ProductName NVARCHAR(22),
	@QuantityPerUnit INT,
	@UnitPrice	DECIMAL(8,2),
	@UnitsInStock	DECIMAL(8,2),
	@ReorderLevel	DECIMAL(8,2),
	@IsDomestic	BIT,
	@Discontinued	BIT,
	@BarCode1	VARCHAR(13),
	@BarCode2	VARCHAR(13),
	@BarCode3	VARCHAR(13),
	@BarCode4	VARCHAR(13)
AS    
 DECLARE @ErrorMessage           NVARCHAR(4000)                  
 DECLARE @ErrorSeverity          INT                  
 DECLARE @ErrorState             INT 
                  
 SET @ProductName = LTRIM(RTRIM(UPPER(@ProductName)))
     
 BEGIN TRY    
      
  IF NOT EXISTS (SELECT 'x' FROM Product p(NOLOCK) WHERE p.ProductId = @ProductId)    
  BEGIN    
	INSERT INTO Product
	(
		-- ProductId -- this column value is auto-generated
		CategoryId,
		SuplierId,
		ProductName,
		QuantityPerUnit,
		UnitPrice,
		UnitsInStock,
		ReorderLevel,
		IsDomestic,
		Discontinued,
		BarCode1,
		BarCode2,
		BarCode3,
		BarCode4,
		Created,
		Updated
	)
	VALUES
	(
		@CategoryId,
		@SuplierId,
		@ProductName,
		@QuantityPerUnit,
		@UnitPrice,
		@UnitsInStock,
		@reorderLevel,
		@Isdomestic,
		@Discontinued,
		@BarCode1,
		@BarCode2,
		@BarCode3,
		@BarCode4,
		GETDATE(),
		GETDATE()
	)
  END    
  ELSE    
  BEGIN    
      UPDATE Product
      SET
      	-- ProductId = ? -- this column value is auto-generated
      	CategoryId = @CategoryId,
      	SuplierId = @SuplierId,
      	ProductName = @ProductName,
      	QuantityPerUnit = @QuantityPerUnit,
      	UnitPrice = @UnitPrice,
      	UnitsInStock = @UnitsInStock,
      	ReorderLevel = @ReorderLevel,
      	IsDomestic = @IsDomestic,
      	Discontinued = @Discontinued,
      	BarCode1 = @BarCode1,
      	BarCode2 = @BarCode2,
      	BarCode3 = @BarCode3,
      	BarCode4 = @BarCode4,
      	Updated = GETDATE()
      WHERE ProductId = @ProductId
  END    
 END TRY                  
 BEGIN CATCH    
  SELECT @ErrorMessage = 'ERROR_MESSAGE: ' + ERROR_MESSAGE() + ' ERROR_PROCEDURE: ' + ERROR_PROCEDURE(),    
         @ErrorSeverity = ERROR_SEVERITY(),    
         @ErrorState = ERROR_STATE()    
      
  RAISERROR(@ErrorMessage, @ErrorSeverity, 1)    
 END CATCH                  
GO

/*====================================
* AddOrder
* ====================================*/
ALTER PROCEDURE [dbo].[AddOrder]    
	@CustomerId	INT,
	@EmployeeId	INT,
	@OrderStatusId INT,
	@OrderNumber	VARCHAR(100),
	@Comment	NVARCHAR(100)
AS    
 DECLARE @ErrorMessage           NVARCHAR(4000)                  
 DECLARE @ErrorSeverity          INT                  
 DECLARE @ErrorState             INT 
                  
 BEGIN TRY    
      
  IF NOT EXISTS (SELECT 'x' FROM [Order] o(NOLOCK) WHERE o.OrderNumber = @OrderNumber)    
  BEGIN    
 	INSERT INTO [Order]
 	(
 		-- OrderId -- this column value is auto-generated
 		CustomerId,
 		EmployeeId,
		OrderStatusId,
		OrderNumber,
		Comment,
		OrderDate
	)
	VALUES
	(
		@customerId,
		@EmployeeId,
		1, -- Plateno
		@OrderNumber,
		@Comment,
		GETDATE()
	)
  END
 END TRY                  
 BEGIN CATCH    
  SELECT @ErrorMessage = 'ERROR_MESSAGE: ' + ERROR_MESSAGE() + ' ERROR_PROCEDURE: ' + ERROR_PROCEDURE(),    
         @ErrorSeverity = ERROR_SEVERITY(),    
         @ErrorState = ERROR_STATE()    
      
  RAISERROR(@ErrorMessage, @ErrorSeverity, 1)    
 END CATCH                  
GO