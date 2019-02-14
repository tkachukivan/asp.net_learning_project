USE [ContactManager2.0]
GO
/****** Object:  StoredProcedure [dbo].[getContactById]    Script Date: 15/02/2019 00:26:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[getContactById]
	@Id uniqueidentifier
AS
BEGIN
    -- Insert statements for procedure here
	SELECT
	c.Id,
	FirstName,
	LastName,
	Email,
	Birthdate,
	a.Id as 'AddressId',
	a.Country,
	a.City,
	a.Street,
	a.Building,
	a.Appartment,
	a.ZipCode
    FROM contacts c
	LEFT JOIN addresses a ON a.ContactId = c.Id
	WHERE c.Id = @Id;
END
GO
