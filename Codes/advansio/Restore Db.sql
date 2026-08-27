RESTORE FILELISTONLY FROM DISK = 'D:\Db\agency_migration.bak'

-- ALTER DATABASE [IdentityResourceTest]
-- SET SINGLE_USER WITH ROLLBACK IMMEDIATE;

-- Restore the database
RESTORE DATABASE [agency_migration]
FROM DISK = 'D:\Db\agency_migration.bak'
WITH MOVE 'agency_migration' TO 'D:\Db\agency_migration.mdf',
     MOVE 'agency_migration_log' TO 'D:\Db\agency_migration.ldf',
     REPLACE;

-- Set the database back to multi-user mode
ALTER DATABASE [agency_migration]
SET MULTI_USER;

RESTORE FILELISTONLY FROM DISK = 'D:\Db\agency_migration.bak'