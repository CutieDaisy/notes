RESTORE FILELISTONLY FROM DISK = 'E:\bk\IdentityResource.bak'

-- ALTER DATABASE [GazetteFormFile]
-- SET SINGLE_USER WITH ROLLBACK IMMEDIATE;

-- Restore the database
RESTORE DATABASE [IdentityResource]
FROM DISK = 'E:\bk\IdentityResource.bak'
WITH MOVE 'GPCL_identity' TO 'E:\Dbs\IdentityResource.mdf',
     MOVE 'GPCL_identity_log' TO 'E:\Dbs\IdentityResource_log.ldf',
     REPLACE;

-- Set the database back to multi-user mode
ALTER DATABASE [IdentityResource]
SET MULTI_USER;

RESTORE FILELISTONLY FROM DISK = 'E:\bk\IdentityResource.bak'