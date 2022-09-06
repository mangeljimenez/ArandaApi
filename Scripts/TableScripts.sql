IF (EXISTS (SELECT *
   FROM INFORMATION_SCHEMA.TABLES
   WHERE TABLE_SCHEMA = 'dbo'
   AND TABLE_NAME = 'Categories'))
   BEGIN
      PRINT 'Database Table Exists'
   END;
ELSE
   BEGIN
     
	CREATE TABLE Categories(
	[idProductCategory] [int] IDENTITY(1,1) NOT NULL,
	[categoryName] [varchar](120) NOT NULL,
	[isActive] [bit] NOT NULL,
	 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
	(
		[idProductCategory] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

END;
GO

IF (EXISTS (SELECT *
   FROM INFORMATION_SCHEMA.TABLES
   WHERE TABLE_SCHEMA = 'dbo'
   AND TABLE_NAME = 'Products'))
   BEGIN
      PRINT 'Database Table Exists'
   END;
ELSE
   BEGIN
     
	CREATE TABLE Products(
		[idProduct] [int] IDENTITY(1,1) NOT NULL,
		[productName] [varchar](120) NOT NULL,
		[description] [varchar](512) NULL,
		[idProductCategory] [int] NOT NULL,
		[productImage] [image] NULL,
		[isActive] [bit] NOT NULL,
		CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
	(
		[idProduct] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
		) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
	
	ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Products] FOREIGN KEY([idProductCategory])
	REFERENCES [dbo].[Categories] ([idProductCategory])	
	
	ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Products]
	
END;



