USE [master];
CREATE LOGIN admin WITH PASSWORD = 'SmartsNinja1'
GO

Use FAMILY_COUNCIL;
GO

IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'admin')
BEGIN
    CREATE USER [admin] FOR LOGIN [admin]
    EXEC sp_addrolemember N'db_owner', N'admin'
END;
GO