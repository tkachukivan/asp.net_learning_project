USE [ContactManager2.0]
GO
/****** Object:  StoredProcedure [dbo].[createContact]    Script Date: 15/02/2019 00:26:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[createContact] 
	-- Add the parameters for the stored procedure here
	@Id uniqueidentifier,
	@FirstName varchar(255),
	@LastName varchar(255),
	@Email varchar(255) = NULL,
	@Birthdate datetime = NULL,
	@AddressId uniqueidentifier = NULL,
	@Country varchar(255) = NULL,
	@City varchar(255) = NULL,
	@Street varchar(255) = NULL,
	@Building varchar(255) = NULL,
	@Appartment varchar(255) = NULL,
	@ZipCode varchar(255) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [dbo].contacts (Id, FirstName, LastName, Email, Birthdate)
	VALUES(
		@Id,
		@FirstName,
		@LastName,
		@Email,
		@Birthdate
	)

	IF @AddressId IS NOT NULL
		BEGIN
		INSERT INTO [dbo].addresses(Id, ContactId, Country, City, Street, Building, Appartment, ZipCode)
		VALUES(
			@AddressId,
			@Id,
			@Country,
			@City,
			@Street,
			@Building,
			@Appartment,
			@ZipCode
		)
		END

	EXEC getContactById @Id=@Id;
END
GO
