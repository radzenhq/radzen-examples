USE [master]
GO
/****** Object:  Database [Test]    Script Date: 11/22/2018 10:28:05 ******/
CREATE DATABASE [Test]
GO
USE [Test]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 11/22/2018 10:28:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](255) NOT NULL,
	[ProductPrice] [money] NOT NULL,
	[ProductPicture] [varbinary](max) NULL,
 CONSTRAINT [PK_dbo.Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [Test] SET  READ_WRITE 
GO
