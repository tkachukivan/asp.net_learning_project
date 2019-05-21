USE [ContactManager2.0]
GO
/****** Object:  Table [dbo].[addresses]    Script Date: 15/02/2019 00:26:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[addresses](
	[Id] [uniqueidentifier] NOT NULL,
	[ContactId] [uniqueidentifier] NOT NULL,
	[Country] [nvarchar](255) NULL,
	[City] [nvarchar](255) NULL,
	[Street] [nvarchar](255) NULL,
	[Building] [nvarchar](255) NULL,
	[Appartment] [nvarchar](255) NULL,
	[ZipCode] [nvarchar](255) NULL,
 CONSTRAINT [PK_addresses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[addresses]  WITH CHECK ADD FOREIGN KEY([ContactId])
REFERENCES [dbo].[contacts] ([Id])
ON DELETE CASCADE
GO
