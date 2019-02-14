USE [ContactManager2.0]
GO
/****** Object:  StoredProcedure [dbo].[updateContactById]    Script Date: 15/02/2019 00:26:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[updateContactById] 
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
	UPDATE [dbo].contacts
	SET
		FirstName = @FirstName,
		LastName = @LastName, 
		Email = @Email,
		Birthdate = @Birthdate
	WHERE Id = @Id

	IF @AddressId IS NOT NULL
		BEGIN
		UPDATE [dbo].addresses
			SET
				Country = @Country,
				City = @City,
				Street = @Street,
				Building = @Building,
				Appartment = @Appartment,
				ZipCode = @ZipCode
			FROM contacts c, addresses a
			WHERE c.Id = @Id AND a.ContactId = c.Id
		END

	EXEC getContactById @Id=@Id;
END
GO
