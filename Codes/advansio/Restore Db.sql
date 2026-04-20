RESTORE FILELISTONLY FROM DISK = 'C:\Database\IdentityResource.bak'

-- ALTER DATABASE [IdentityResourceTest]
-- SET SINGLE_USER WITH ROLLBACK IMMEDIATE;

-- Restore the database
RESTORE DATABASE [IdentityResourceTest]
FROM DISK = 'C:\Database\IdentityResource.bak'
WITH MOVE 'GPCL_identity' TO 'C:\Database\IdentityResourceTest.mdf',
     MOVE 'GPCL_identity_log' TO 'C:\Database\IdentityResourceTest_log.ldf',
     REPLACE;

-- Set the database back to multi-user mode
ALTER DATABASE [IdentityResourceTest]
SET MULTI_USER;

RESTORE FILELISTONLY FROM DISK = 'C:\Database\IdentityResource.bak'