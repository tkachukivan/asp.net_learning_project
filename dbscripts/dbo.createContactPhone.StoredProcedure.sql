USE [ContactManager2.0]
GO
/****** Object:  StoredProcedure [dbo].[createContactPhone]    Script Date: 15/02/2019 00:26:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[createContactPhone]  
	-- Add the parameters for the stored procedure here
	@Id uniqueidentifier,
	@ContactId uniqueidentifier,
	@CountryCode nvarchar(10),
	@PhoneNumber nvarchar(40),
	@PhoneType int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [dbo].phones(Id, ContactId, CountryCode, PhoneNumber, PhoneType)
		VALUES (
			@Id,
			@ContactId,
			@CountryCode,
			@PhoneNumber,
			@PhoneType
		)

	EXEC getContactPhoneById @Id=@Id, @ContactId=@ContactId;
END
GO
