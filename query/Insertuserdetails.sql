USE [Airticket]
GO

SELECT [userId]
      ,[firstName]
      ,[lastName]
      ,[DateofBirth]
      ,[Age]
      ,[PhoneNumber]
      ,[Email]
  FROM [dbo].[TblUserLogin]

GO


USE [Airticket]
GO

-- Create a stored procedure to insert user details
CREATE PROCEDURE InsertUserDetails
    @FirstName VARCHAR(255),
    @LastName VARCHAR(255),
    @DateOfBirth DATE,
    @Age INT,
    @PhoneNumber VARCHAR(20),
    @Email VARCHAR(255),
    @Password VARCHAR(255),
    @UserType VARCHAR(50) = 'User'
AS
BEGIN
    -- Insert user details into TblUserLogin
    INSERT INTO [dbo].[TblUserLogin] (
        [firstName],
        [lastName],
        [DateofBirth],
        [Age],
        [PhoneNumber],
        [Email]
    )
    VALUES (
        @FirstName,
        @LastName,
        @DateOfBirth,
        @Age,
        @PhoneNumber,
        @Email
    );

    -- Insert email and password into Login table
    INSERT INTO [dbo].[TblLogin] (
        [Useremail],
        [Password],
		[Name],
        [Usertype]
    )
    VALUES (
        @Email,
        @Password,
		@FirstName,
        @UserType
    );

    -- Check for errors and return a status
    IF @@ERROR <> 0
    BEGIN
        ROLLBACK;
        RETURN -1; -- Indicates an error
    END
    ELSE
    BEGIN
        COMMIT;
        RETURN 0; -- Indicates success
    END
END;
