USE [ContactManager2.0]
GO
/****** Object:  Table [dbo].[phones]    Script Date: 15/02/2019 00:26:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[phones](
	[Id] [uniqueidentifier] NOT NULL,
	[ContactId] [uniqueidentifier] NOT NULL,
	[CountryCode] [nvarchar](10) NULL,
	[PhoneNumber] [nvarchar](40) NULL,
	[PhoneType] [int] NULL,
 CONSTRAINT [PK_phones] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[phones]  WITH CHECK ADD FOREIGN KEY([ContactId])
REFERENCES [dbo].[contacts] ([Id])
ON DELETE CASCADE
GO
