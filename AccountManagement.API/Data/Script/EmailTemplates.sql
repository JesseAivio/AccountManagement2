GO
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
      ,'licenseRenew')
GO