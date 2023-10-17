SELECT TOP (1000) [userId]
      ,[firstName]
      ,[lastName]
      ,[DateofBirth]
      ,[Age]
      ,[PhoneNumber]
      ,[Email]
  FROM [Airticket].[dbo].[TblUserLogin]

CREATE PROCEDURE GetUserDetailsbyID
    @UserId INT
AS
BEGIN
    SELECT
        *
    FROM [Airticket].[dbo].[TblUserLogin]
    WHERE [userId] = @UserId;
END;

Exec GetUserDetailsbyID	@UserID = '1'

CREATE PROCEDURE DeleteUserById
    @UserId INT
AS
BEGIN
    -- Delete the user with the specified userId
    DELETE FROM [TblUserLogin] WHERE [userId] = @UserId;
END

CREATE PROCEDURE UpdateUserDetailsById
    @UserId INT,
    @FirstName NVARCHAR(50),
    @LastName NVARCHAR(50),
    @DateofBirth DATE,
    @Age INT,
    @PhoneNumber NVARCHAR(15),
    @Email NVARCHAR(100)
AS
BEGIN
 UPDATE [TblUserLogin]
        SET
            [firstName] = @FirstName,
            [lastName] = @LastName,
            [DateofBirth] = @DateofBirth,
            [Age] = @Age,
            [PhoneNumber] = @PhoneNumber,
            [Email] = @Email
        WHERE [userId] = @UserId;
END