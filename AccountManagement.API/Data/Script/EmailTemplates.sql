GO
DELETE FROM [dbo].[EmailTemplate]
INSERT INTO [dbo].[EmailTemplate]([Id]
      ,[Subject]
      ,[Body]
      ,[Type])VALUES(NEWID()
      ,'New application is licensed'
      ,'Hello, {Organization}' + CHAR(10) + 'New application is now licensed for this organization'
      ,'appRegister'),(NEWID()
      ,'Organization registered'
      ,'Hello, {Organization} is now registered to AccountManagement'
      ,'organizationRegister'),(NEWID()
      ,'Account registered'
      ,'Hello, {Username} is now registered to AccountManagement under {Organization}'
      ,'accountRegister'),(NEWID()
      ,'License needs to be renewd'
      ,'Hello, {Organization} You need to renew your application license'
      ,'licenseRenew'),(NEWID()
      ,'License has been locked'
      ,'Hello, {Organization} One of your licenses has been locked'
      ,'licenseLock'),(NEWID()
      ,'License has been freed'
      ,'Hello, {Organization} One of your licenses has been freed'
      ,'licenseFreed'),(NEWID()
      ,'Suspicious LogIn'
      ,'Hello, {Organization} One of your accounts has suspicious logins'
      ,'suspiciousLogIn')
GO