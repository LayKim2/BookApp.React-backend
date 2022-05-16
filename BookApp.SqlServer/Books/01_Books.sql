CREATE TABLE [dbo].[Books]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1),
	Title nvarchar(255) not null,
	Description nvarchar(max) null,

	Created DateTime Default(GetDate()) null
)
Go