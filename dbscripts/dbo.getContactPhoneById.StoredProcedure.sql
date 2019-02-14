USE [ContactManager2.0]
GO
/****** Object:  StoredProcedure [dbo].[getContactPhoneById]    Script Date: 15/02/2019 00:26:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[getContactPhoneById]
	-- Add the parameters for the stored procedure here
	@Id uniqueidentifier,
	@ContactId uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT
		p.Id,
		p.CountryCode,
		p.PhoneNumber,
		p.PhoneType
    FROM phones p
	WHERE p.Id = @Id AND p.ContactId = @ContactId 
END
GO
