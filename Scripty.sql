use ����������;


DROP PROCEDURE GetUserRoles;
DROP PROCEDURE GetUserTablePermissions;
DROP PROCEDURE SearchBooks;
DROP PROCEDURE ������������������;
DROP TRIGGER trg_�����������������������������������;
DROP FUNCTION dbo.GetPercentageUsersByGroupAndFaculty;
DROP FUNCTION dbo.GetDebtorsReport
GO
alter table �����
drop constraint FK_�����_������������,
constraint FK_�����_����,
constraint FK_�����_�������
GO
alter table [����� �����]
drop constraint FK_�����_�����_�����,
constraint FK_�����_�����_������
GO
alter table [����������� ����]
drop constraint FK_�����_�����������_����,
constraint FK_�����������_����_��������
GO
alter table [�������� ����]
drop constraint FK_�����_��������_����,
constraint FK_��������_����_��������
GO
alter table [������ � �������]
drop constraint FK_������_�������_�����,
constraint FK_������_�������_����������,
constraint FK_������_�������_��������
GO
alter table [��������]
drop constraint FK_��������_���������
GO
alter table [����������]
drop constraint FK_����������_���������
go
alter table [��������]
drop constraint FK_��������_����������
go
alter table [��������]
drop constraint FK_��������_����������
go
alter table ������
drop constraint FK_������_����������
go
alter table ��������
drop constraint FK_��������_������
go
drop table ��������
go
drop table ������������
go
drop table ����
go
drop table �����
go
drop table [����� �����]
go
drop table ������
go
drop table [����������� ����]
go
drop table ��������
go
drop table [�������� ����]
go
drop table ��������
go
drop table [������ � �������]
go
drop table ��������
go
drop table ��������
go
drop table [���������]
go
drop table ����������
go
drop table ���������
go
drop table ������
go


Create table ������������
(��� int not null primary key identity,
�������� varchar(50),
����� varchar(40),
����� varchar(80) not null
)
go

create table ����
(��� int primary key not null identity,
[�������� �����] varchar(40) not null
)
GO

create table �����
(��� int primary key not null identity,
[��� ������������] int,
[��� �����] int,
[��� ��������] int,
[��������] varchar(50),
[��� �������] int,
[����� �������] int,
[���� �����] varchar(20) not null,
[����] money
)
GO

create table [����� �����]
(��� int primary key not null identity,
[��� �����] int,
[��� ������] int
)
GO

create table ������
(��� int primary key not null identity,
[�������] varchar(40),
[���] varchar(40),
[��������] varchar(40),
[������ ������] varchar(40)
)
GO

create table [����������� ����]
(��� int primary key not null identity,
[��� �����] int,
[��� ��������] Int,
[����� ����] int
)
GO

create table ��������
(��� int primary key not null identity,
[���� ��������] date,
[��� ����������] int,
[���������] varchar(40)
)
GO

create table [�������� ����]
(��� int primary key not null identity,
[��� �����] int,
[��� �������� ����] int,
[����� ����] int
)
GO

create table ��������
(��� int primary key not null identity,
[���� ��������] date,
[��� ����������] int
)
GO

create table [������ � �������]
(
��� int primary key not null identity,
[��� ��������] int,
[��� �����] int,
[����������� �����] varchar(40),
[���� ������] date not null,
[���� ��������] date,
[��� ����������, ��������� �����] int,
[����� �������] bit
)
GO

create table ��������
(��� int primary key not null identity,
[��� ����] int not null,
[����� ��������] varchar(4)
)
GO

create table ���������
(��� int primary key not null identity,
[��������] varchar(40),
[��������� ���] bit,
[�����] bit,
[���������] bit,
[����� ���������] varchar(40) 
)
GO

CREATE TABLE ��������
(
��� INT PRIMARY KEY NOT NULL IDENTITY,
[�������] VARCHAR(40),
[���] VARCHAR(40),
[��������] VARCHAR(40),
[���� ��������] DATE,
[���������� �����] VARCHAR(40),
[����� ����������] VARCHAR(40),
[������ ��������] CHAR(60),
[����� ������������� ������] VARCHAR(40),
[��� ������] INT,
[��� ��� �����] VARCHAR(100)
);

create table ����������
(��� int primary key not null identity,
[��� ���������] int,
[�������] varchar(40),
[���] varchar(40),
[��������] varchar(40),
[���������� ���������] varchar(40),
[����] int
)
GO


CREATE TABLE ������
(
��� INT PRIMARY KEY NOT NULL IDENTITY,
�������� VARCHAR(40) NOT NULL,
���� INT NOT NULL,
[��������� ����] INT NOT NULL,
[��� ����������] INT NOT NULL
);
CREATE TABLE ���������
(
��� INT PRIMARY KEY NOT NULL IDENTITY,
�������� VARCHAR(40) NOT NULL
);

alter table �����
add constraint FK_�����_������������  foreign key ([��� ������������]) references ������������(���),
constraint FK_�����_����  foreign key ([��� �����]) references ����(���),
constraint FK_�����_�������  foreign key ([��� ��������]) references ��������(���)
GO
alter table [����� �����]
add constraint FK_�����_�����_�����  foreign key ([��� �����]) references �����(���),
constraint FK_�����_�����_������  foreign key ([��� ������]) references ������(���)
GO
alter table [����������� ����]
add constraint FK_�����_�����������_���� foreign key ([��� �����]) references �����(���),
constraint FK_�����������_����_��������  foreign key ([��� ��������]) references ������(���)
GO
alter table [�������� ����]
add constraint FK_�����_��������_���� foreign key ([��� �����]) references ��������(���),
constraint FK_��������_����_��������  foreign key ([��� �������� ����]) references ��������(���)
GO
alter table [������ � �������]
add constraint FK_������_�������_����� foreign key ([��� �����]) references �����(���),
constraint FK_������_�������_���������� foreign key ([��� ����������, ��������� �����]) references ����������(���),
constraint FK_������_�������_�������� foreign key ([��� ��������]) references ��������(���)
GO
alter table [��������]
add constraint FK_��������_���������  foreign key ([��� ����]) references ���������(���)
GO
alter table [����������]
add constraint FK_����������_���������  foreign key ([��� ���������]) references ���������(���)
GO

alter table [��������]
add constraint FK_��������_����������  foreign key ([��� ����������]) references ����������(���)
GO

alter table [��������]
add constraint FK_��������_����������  foreign key ([��� ����������]) references ����������(���)
GO

alter table ��������
add constraint FK_��������_������ foreign key ([��� ������]) references ������(���)
GO
alter table ������
add constraint FK_������_���������� foreign key ([��� ����������]) references ���������(���)
GO

insert into ����
values
  ('������������ �������'),
  ('�����'),
  ('������')

insert into ������������
values
  ('��� ����� �. �������','������','��������� 104')

insert into ���������
values
	('���������� � ���������� ����������������')
insert into [������]
values
	('��', 4,4,1),
	('��',3,4,1)



insert into ��������
values
  ('������','������','����������','2002-06-21','+37529#######','��������� ##','HB#######','number_bilet', 1, 'librarian'),
  ('���������','���������','�������������','2002-12-12','+37529#######','��������� ##','HB#######','number_bilet', 2,  null),
  ('�������','���','��������','2003-12-12','+37529#######','��������� ##','HB#######','number_bilet', 2, null)

insert into ������
values
  ('�����������','������','���������','��������'),
  ('��������','�������','����������','��������'),
  ('�����������','���������','����������','��������')


insert into ���������(��������,[����� ���������])
values
  ('1� ��������� ��� ��� ��� ��. �. �������','��. ��������� 102'),
  ('2� ��������� ��� ��� ��� ��. �. �������','��. ��������� 102')

insert into ��������([��� ����],[����� ��������])
values
  (1,1)

insert into �����
values
  (1,1,1,'���������������� ���������', 2022, 47, '�������', 30),
  (1,2,1,'����������������', 2007, 211, '�������', 38)
  
insert into [����� �����]([��� �����],[��� ������])
values
  (1,1),
  (2,2)

insert into ����������
values
  (1,'������','����','�����','������������',3),
  (1,'��������','����','���������','��������',2)
  
insert into [����������� ����]([��� �����],[��� ��������],[����� ����])
values
  (1,1,10)
 
insert into [��������]
values
  ('2022-05-07',1,'��� ��. �. �������')

insert into [������ � �������]
values
	(3, 2, '1', '2022-12-15', NULL, 1, 0);
go



CREATE PROCEDURE SearchBooks
    @SearchTerm VARCHAR(100)
AS
BEGIN
    SELECT
        �����.�������� AS '�������� �����',
        ������.������� + ' ' + ������.��� + ' ' + ������.�������� AS '�����',
        ����.[�������� �����] AS '����',
        ������������.�������� AS '������������',
        �����.[��� �������] AS '��� �������',
        �����.[����� �������] AS '����� �������',
        �����.[���� �����] AS '���� �����'
    FROM
        �����
    JOIN
        ���� ON �����.[��� �����] = ����.���
    JOIN
        [����� �����] ON [����� �����].[��� �����] = �����.���
    JOIN
        [������] ON [������].��� = [����� �����].[��� ������]
    JOIN
        [������������] ON �����.[��� ������������] = [������������].��� 
    WHERE
        �����.�������� LIKE '%' + @SearchTerm + '%'
        OR ������.������� LIKE '%' + @SearchTerm + '%'
        OR ������.��� LIKE '%' + @SearchTerm + '%'
        OR ������.�������� LIKE '%' + @SearchTerm + '%'
        OR ����.[�������� �����] LIKE '%' + @SearchTerm + '%';
END;
go




CREATE PROCEDURE GetUserTablePermissions
	@DatabaseUserName NVARCHAR(255),
    @TableName NVARCHAR(255)
AS
BEGIN
SELECT dp.permission_name
        FROM sys.database_permissions dp
        JOIN sys.database_principals dpn ON dp.grantee_principal_id = dpn.principal_id
        WHERE dpn.name = (SELECT dp.name AS DatabaseUserName
        FROM sys.database_principals dp
        JOIN sys.server_principals sp ON dp.sid = sp.sid
        WHERE sp.name = @DatabaseUserName) AND dp.major_id = OBJECT_ID(@TableName)
END;
go

CREATE PROCEDURE GetUserRoles
    @LoginName NVARCHAR(100)
AS
BEGIN
    DECLARE @UserId INT;
    
    SELECT @UserId = dp.principal_id
    FROM sys.database_principals dp
    JOIN sys.server_principals sp ON dp.sid = sp.sid
    WHERE sp.name = @LoginName;

    SELECT role.name AS RoleName
    FROM sys.database_role_members members
    JOIN sys.database_principals role ON members.role_principal_id = role.principal_id
    WHERE members.member_principal_id = @UserId;
END;
go



CREATE TABLE ��������
(
    ��� INT PRIMARY KEY NOT NULL IDENTITY,
    [�������] VARCHAR(50) NOT NULL,
    [��������] VARCHAR(50) NOT NULL,
    [���� ��������] INT NOT NULL
);

go
CREATE PROCEDURE ������������������
AS
BEGIN
	IF NOT EXISTS (SELECT 1 FROM �������� WHERE [�������] = '������' AND [��������] = '���������� �����' AND YEAR([���� ��������]) = YEAR(GETDATE()))
	BEGIN
		UPDATE ������
		SET ���� = ���� + 1;
		INSERT INTO �������� ([�������], [��������], [���� ��������])
		VALUES ('������', '���������� �����', YEAR(GETDATE()));
	END;
END;
go



CREATE TRIGGER trg_�����������������������������������
ON ������
AFTER UPDATE
AS
BEGIN
    IF UPDATE([��������� ����]) OR UPDATE([����])
    BEGIN
        IF EXISTS (
            SELECT 1
            FROM ��������
            WHERE [�������] = '������' 
                AND [��������] = '���������� �����' 
                AND [���� ��������] = YEAR(GETDATE())
				
        )
		BEGIN
            ROLLBACK;
            RETURN;
        END
        DELETE FROM ��������
        WHERE [��� ������] IN (
            SELECT G.[���]
            FROM ������ G
            INNER JOIN INSERTED I ON G.[���] = I.[���]
            WHERE G.[����] > G.[��������� ����]
        );

        -- ������� ������, �� ������ ���� �� �������� ���������
        DELETE FROM ������
        WHERE [����] > [��������� ����];
    END
END;
GO

--exec ������������������


CREATE FUNCTION dbo.GetPercentageUsersByGroupAndFaculty
(
    @StartDate DATE,
    @EndDate DATE
)
RETURNS TABLE
AS
RETURN
(
    WITH ���������� AS (
        SELECT
            �.��� AS [��� ��������],
            �.[���] AS [��� ������]
        FROM
            [������ � �������] �
            INNER JOIN �������� � ON �.[��� ��������] = �.���
            INNER JOIN ������ � ON �.[��� ������] = �.[���]
        WHERE
            �.[���� ������] BETWEEN @StartDate AND @EndDate
    ),
    ����������������� AS (
        SELECT
            �.[��� ����������],
            �.[���] AS [��� ������],
            COUNT(DISTINCT �.[���]) AS �_��������
        FROM
            �������� �
            INNER JOIN ���������� �� ON �.��� = ��.[��� ��������]
            INNER JOIN ������ � ON �.[��� ������] = �.[���]
        WHERE
            �.[��� ������] IS NOT NULL
        GROUP BY
            �.[��� ����������],
            �.[���]
    )
    SELECT
        �.[��������] AS [�������� ����������],
        �.[��������] AS [�������� ������],
        �.[����],
        ISNULL(�����������������.�_��������, 0) AS [� ��������],
        ISNULL(��������.�����_����������, 0) AS [����� ����������],
        IIF(ISNULL(��������.�����_����������, 0) = 0, 0, 100 * CAST(ISNULL(�����������������.�_��������, 0) AS FLOAT) / CAST(ISNULL(��������.�����_����������, 0) AS FLOAT)) AS [������� ������������� �� ���������]
    FROM
        ��������� �
        INNER JOIN ������ � ON �.[���] = �.[��� ����������]
        LEFT JOIN ����������������� ON �.[���] = �����������������.[��� ������] AND �.[���] = �����������������.[��� ����������]
        LEFT JOIN (
            SELECT
                �.[��� ����������],
                �.[���] AS [��� ������],
                COUNT(DISTINCT �.[���]) AS �����_����������
            FROM
                �������� �
                LEFT JOIN ������ � ON �.[��� ������] = �.[���]
            WHERE
                �.[��� ������] IS NOT NULL
            GROUP BY
                �.[��� ����������],
                �.[���]
        ) AS �������� ON �.[���] = ��������.[��� ������] AND �.[���] = ��������.[��� ����������]
);
go


CREATE FUNCTION dbo.GetDebtorsReport(@StartDate DATETIME2, @EndDate DATETIME2)
RETURNS TABLE
AS
RETURN
(
    WITH ���������� AS (
        SELECT
            �.[��� ����������],
            �.[��� ������],
            MONTH(�.[���� ������]) AS [�����],
            CASE
                WHEN MONTH(�.[���� ������]) BETWEEN 1 AND 6 THEN '1-� �������'
                WHEN MONTH(�.[���� ������]) BETWEEN 7 AND 12 THEN '2-� �������'
            END AS [�������],
            YEAR(�.[���� ������]) AS [���],
            COUNT(*) AS [���������� ���������]
        FROM
            �������� �
            INNER JOIN [������ � �������] � ON �.[���] = �.[��� ��������] AND �.[���� ��������] IS NULL -- ��������
            INNER JOIN ������ � ON �.[��� ������] = �.[���]
        WHERE
            �.[���� ������] BETWEEN @StartDate AND @EndDate
        GROUP BY
            �.[��� ����������],
            �.[��� ������],
            MONTH(�.[���� ������]),
            CASE
                WHEN MONTH(�.[���� ������]) BETWEEN 1 AND 6 THEN '1-� �������'
                WHEN MONTH(�.[���� ������]) BETWEEN 7 AND 12 THEN '2-� �������'
            END,
            YEAR(�.[���� ������])
    )
    SELECT
        �.[��������] AS [�������� ����������],
        �.[��������] AS [�������� ������],
        �.[����],
        VK.[�����],
        VK.[�������],
        VK.[���],
        VK.[���������� ���������]
    FROM
        ���������� VK
        INNER JOIN ������ � ON VK.[��� ������] = �.[���]
        INNER JOIN ��������� � ON VK.[��� ����������] = �.[���]
);

go

--o1
SELECT * FROM dbo.GetPercentageUsersByGroupAndFaculty('2020-01-01', '2024-12-31');
--o2
SELECT * FROM dbo.GetDebtorsReport('2020-01-01', '2024-12-31');
--y++
exec ������������������