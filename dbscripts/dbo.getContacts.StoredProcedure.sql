USE [ContactManager2.0]
GO
/****** Object:  StoredProcedure [dbo].[getContacts]    Script Date: 15/02/2019 00:26:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[getContacts]
AS 
    SELECT Id, FirstName, LastName, Email, Birthdate  
    FROM contacts;  
GO
