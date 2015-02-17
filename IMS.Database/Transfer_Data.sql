update Product
SET BarCode1 = ''
WHERE BarCode1 = '0'

DELETE from Product
SELECT * FROM Product p
SET IDENTITY_INSERT Product ON
GO
INSERT INTO Product
(
	ProductId, -- this column value is auto-generated
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
SELECT 
	PLU,
	NULL,
	NULL,
	Name,
	NULL,
	Price,
	Stock,
	NULL,
	1,
	0,
	Bar_code,
	NULL,
	NULL,
	NULL,
	Updated,
	Updated
FROM jule.Mikavi.dbo.Articles a