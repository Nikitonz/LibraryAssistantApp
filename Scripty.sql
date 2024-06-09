use ����������;


DROP FUNCTION CheckUserRights;
DROP PROCEDURE GetSaltForUser
DROP PROCEDURE GetUserTablePermissions;
DROP PROCEDURE RemoveOutdatedReaders;
DROP PROCEDURE GetPercentageUsersByGroupAndFaculty;
DROP PROCEDURE GetDebtorsReport
DROP PROCEDURE GetReaderInfoExact
DROP FUNCTION BooksPublicInfo
DROP PROCEDURE dbo.GetDependentTableName;
DROP PROCEDURE FindBookLocation;
DROP PROCEDURE SetNewPass
DROP TABLE ��������������
alter table �����
drop constraint FK_�����_������������,
constraint FK_�����_����,
constraint FK_�����_�������
alter table [����� �����]
drop constraint FK_�����_�����_�����,
constraint FK_�����_�����_������
alter table [����������� ����]
drop constraint FK_�����_�����������_����,
constraint FK_�����������_����_��������
alter table [�������� ����]
drop constraint FK_�����_��������_����,
constraint FK_��������_����_��������
alter table [������ � �������]
drop constraint FK_������_�_�������_�����,
constraint FK_������_�_�������_����������,
constraint FK_������_�_�������_������������
alter table [��������]
drop constraint FK_��������_���������
alter table [����������]
drop constraint FK_����������_���������
alter table [��������]
drop constraint FK_��������_������������
alter table [��������]
drop constraint FK_��������_������������
alter table ������
drop constraint FK_������_����������
alter table ������������
drop constraint FK_������������_������

go
drop table ������������
drop table ����
drop table �����
drop table [����� �����]
drop table ������
drop table [����������� ����]
drop table ��������
drop table [�������� ����]
drop table ��������
drop table [������ � �������]
drop table ��������

drop table [���������]
drop table ����������
drop table ���������
drop table ������
drop table ������������
GO

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
[�������] VARBINARY(MAX),
[������� ��������] varchar(4000),
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

CREATE TABLE ������������
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
[��� ��� �����] VARCHAR(100),
[����� �������] INT NOT NULL DEFAULT 3, -- 0: �����, 1: ������������, 2: �����, 3: ��������
[��� ������] VARCHAR(256),
[����] VARCHAR(256),
[��������� ����� ������] bit default 1
);

create table ����������
(��� int primary key not null identity,
[��� ���������] int,
[��� ������������] int unique,
[���������� ���������] varchar(40),
[����] int
)
GO


CREATE TABLE ������
(
��� INT PRIMARY KEY NOT NULL IDENTITY,
�������� VARCHAR(40) NOT NULL,
[��� �����������] INT NOT NULL,
[��� ���������] INT NOT NULL,
[��� ����������] INT NOT NULL
);
CREATE TABLE ���������
(
��� INT PRIMARY KEY NOT NULL IDENTITY,
�������� VARCHAR(40) NOT NULL
);


go



CREATE TABLE �������������� (
        ��� INT PRIMARY KEY NOT NULL IDENTITY,
        [�������] VARCHAR(40),
        [���] VARCHAR(40),
        [��������] VARCHAR(40),
        [���� ��������] DATE,
        [���������� �����] VARCHAR(40),
        [����� ����������] VARCHAR(40),
        [������ ��������] CHAR(60),
        [����� ������������� ������] VARCHAR(40),
        [�������� ������] varchar(20),
		[���������] varchar(20),
		[�������������] varchar(500),
        [��� ��� �����] VARCHAR(100)
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
constraint FK_�����������_����_��������  foreign key ([��� ��������]) references ��������(���)
GO
alter table [�������� ����]
add constraint FK_�����_��������_���� foreign key ([��� �����]) references �����(���),
constraint FK_��������_����_��������  foreign key ([��� �������� ����]) references ��������(���)
GO
alter table [������ � �������]
add constraint FK_������_�_�������_����� foreign key ([��� �����]) references �����(���),
constraint FK_������_�_�������_���������� foreign key ([��� ����������, ��������� �����]) references ����������(���),
constraint FK_������_�_�������_������������ foreign key ([��� ��������]) references ������������(���)
GO
alter table [��������]
add constraint FK_��������_���������  foreign key ([��� ����]) references ���������(���)
GO
alter table [����������]
add constraint FK_����������_���������  foreign key ([��� ���������]) references ���������(���),
constraint FK_����������_������������ foreign key([��� ������������]) references ������������(���)
GO

alter table [��������]
add constraint FK_��������_������������  foreign key ([��� ����������]) references ������������(���)
GO

alter table [��������]
add constraint FK_��������_������������  foreign key ([��� ����������]) references ������������(���)
GO

alter table ������������
add constraint FK_������������_������ foreign key ([��� ������]) references ������(���)
GO
alter table ������
add constraint FK_������_���������� foreign key ([��� ����������]) references ���������(���)
GO

-- ��� ������� ����
INSERT INTO ���� ([�������� �����])
VALUES 
    ('�������'),
    ('����������'),
    ('������� ����������'),
    ('����������');

-- ��� ������� ������
INSERT INTO ������ ([�������], [���], [��������], [������ ������])
VALUES 
    ('����������', '������', '����������', '������'),
    ('���������', '������', '����������', '������'),
    ('�����������', '��������', '����������', '������'),
    ('���������', '�����', '', '���'),
    ('������', '�����', '', '���'),
    ('������', '���', '', '���'),
    ('�����������', '���������', '������������', '��������'),
    ('��������', '����', '�������������', '��������');

-- ��� ������� ���������
INSERT INTO ��������� ([��������], [����� ���������])
VALUES 
    ('������� ��������� ���', '��. ������, 10'),
    ('������� �����', '��. �������, 5'),
    ('���������', '��. ������, 15');

-- ��� ������� ��������
INSERT INTO �������� ([��� ����], [����� ��������])
VALUES 
    (1, 'A1'),
    (1, 'A2'),
    (1, 'B1'),
    (2, 'C1'),
    (3, 'A1');

INSERT INTO ������������ ([��������], [�����], [�����])
VALUES 
    ('��� ��. �. �������', '������', '��������� 104'),
    ('���', '�����', '��. ������������� 4')
-- ��� ������� �����
INSERT INTO ����� ([��� ������������], [��� �����], [��� ��������], [��������], [��� �������], [����� �������], [���� �����], [�������], [������� ��������], [����])
VALUES 
    (1, 1, 1, '�������������� ������', 2020, 800, '�������', NULL, '������� �� ��������������� �������', 35.99),
    (1, 2, 1, '������ ����������������', 2019, 600, '�������', NULL, '���������� �� ������� ����������������', 40.99),
    (2, 3, 2, '������', 2018, 750, '�������', NULL, '������� ���������� �� ������', 28.99),
    (2, 1, 3, '�����', 2021, 900, '�������', NULL, '������� �� �����', 32.99),
    (1, 4, 4, '���������� ��������', 2017, 1000, '�������', NULL, '���������� ��� ���������', 50.99);
	-- (SELECT BulkColumn FROM Openrowset(Bulk 'D:\Downloads\43w8XwCrfeQ.jpg', Single_Blob) as Image)
-- ��� ������� [����� �����]
INSERT INTO [����� �����] ([��� �����], [��� ������])
VALUES 
    (1, 1),
    (1, 2),
    (2, 3),
    (2, 4),
    (3, 5),
    (3, 6),
    (4, 7),
    (5, 8);





-- ��� ������� ���������
INSERT INTO ��������� (��������)
VALUES 
    ('���������� � ��'),
    ('������');
-- ��� ������� ������
INSERT INTO ������ (��������, [��� �����������], [��� ���������], [��� ����������])
VALUES 
    ('��-20', 2020, 2023, 1),
    ('��-19', 2019, 2023, 1),
    ('���-21', 2019, 2025, 1),
    ('��-22', 2020, 2025, 1),
    ('�-20', 2020, 2024, 2);



-- ��� ������� ��������
INSERT INTO ������������ ([�������], [���], [��������], [���� ��������], [���������� �����], [����� ����������], [������ ��������], [����� ������������� ������], [��� ������])
VALUES 
    ('������', '����', '��������', '1999-03-15', '+375291234567', '��. ������, 10', 'MP1234567', 'C001', 1),
    ('�������', '�����', '���������', '2000-05-20', '+375291234568', '��. �������, 5', 'MP2345678', 'C002', 2),
    ('�������', '�������', '��������', '1998-07-25', '+375291234569', '��. ������, 15', 'MP3456789', 'C003', 3)

INSERT INTO ������������ ([�������], [���], [��������], [���� ��������], [���������� �����], [����� ����������], [������ ��������], [��� ��� �����], [����� �������], [��� ������], ����, [��������� ����� ������])
VALUES 
    ('������', '��������', '�����������', '1990-04-14', '+3752922334433', '��. ������, 13', '��2987656', 'admin1', 0, 'B97/cfUafV/dcI4Y708CcouHo6d4wzY0om2d0wIgO6w=', 'N4KC7DaFp9HnV8DEdy3p7A==',0),
	('��������', '������', '��������', '1978-04-14', '+3752922334431', '��. ���������, 22', '��2932323', 'librarian1', 1, 'U1WD1Izi9aimqcMijIvnQHczSmH5dBIvloMWUyrMMMc=', '1bCOUtL+Khv7XQb0ABYr2A==',0);
-- ��� ������� ����������
INSERT INTO ���������� ([��� ������������], [���������� ���������], [����])
VALUES 
	(4,'�������������', 3),
    (5,'������������', 10);

-- ��� ������� ��������
INSERT INTO �������� ([���� ��������], [��� ����������], [���������])
VALUES 
    ('2023-01-15', 1, '����������������� "������� �������"'),
    ('2023-02-20', 1, '������������ ��� "������"'),
    ('2023-03-25', 1, '������������ "������"');

-- ��� ������� [����������� ����]
INSERT INTO [����������� ����] ([��� �����], [��� ��������], [����� ����])
VALUES 
    (1, 1, 20),
    (2, 2, 15),
    (3, 3, 30),
    (4, 1, 25);

-- ��� ������� [������ � �������]
INSERT INTO [������ � �������] ([��� ��������], [��� �����], [����������� �����], [���� ������],[���� ��������], [��� ����������, ��������� �����])
VALUES 
    (1, 1, 'INV001', '2023-04-01',null, 2),
    (2, 2, 'INV002', '2023-05-05',null, 2),
    (3, 3, 'INV003', '2023-06-10','2024-05-05', 2)

GO

--��������� ����������
CREATE FUNCTION BooksPublicInfo(
    @SearchTerm VARCHAR(100)
)
RETURNS TABLE
AS
RETURN
(
    SELECT 
        �����.[��������], 
        �����.����� AS �����,
        ����.[�������� �����] AS ����,
        ������������.[��������] AS ������������,
        �����.[��� �������], 
        �����.[����� �������], 
        �����.[���� �����], 
        �����.[�������],
        COUNT(DISTINCT CASE WHEN ([������ � �������].[���� ������] IS NOT NULL 
            AND [������ � �������].[���� ��������] IS NOT NULL
            AND [������ � �������].[����� �������] IS NULL)
            OR [������ � �������].[���� ������] IS NULL
            THEN �����.��� ELSE NULL END) AS �����������,
        COUNT(CASE WHEN [������ � �������].[���� ������] IS NOT NULL THEN [������ � �������].[��� ��������] ELSE NULL END) AS [��� ����� �����], 
        �����.[������� ��������]
    FROM 
        �����
    JOIN 
        ���� ON �����.[��� �����] = ����.���
    JOIN 
        [������������] ON �����.[��� ������������] = [������������].���
    LEFT JOIN 
        (SELECT 
            [����� �����].[��� �����],
            STRING_AGG([������].[�������] + ' ' + [������].[���] + ' ' + ������.��������, ', ') AS �����
         FROM 
            [����� �����]
         JOIN 
            [������] ON [������].��� = [����� �����].[��� ������]
         GROUP BY 
            [����� �����].[��� �����]
        ) AS ����� ON �����.[��� �����] = �����.���
    LEFT JOIN 
        [������ � �������] ON [������ � �������].[��� �����] = �����.���
    WHERE 
        �����.�������� LIKE '%' + @SearchTerm + '%'
        OR �����.����� LIKE '%' + @SearchTerm + '%'
        OR ����.[�������� �����] LIKE '%' + @SearchTerm + '%'
    GROUP BY
        �����.[��������], 
        �����.�����,
        ����.[�������� �����],
        ������������.[��������],
        �����.[��� �������], 
        �����.[����� �������], 
        �����.[���� �����], 
        �����.[�������],
        �����.[������� ��������]
);

go

--����� ��������� ����������
GO
--��������� ��������

CREATE PROCEDURE SetNewPass
	@username NVARCHAR(255),
    @hashedpass NVARCHAR(255),
	@newsalt NVARCHAR(255)
AS
BEGIN
	update ������������ set [��� ������]=@hashedpass, ���� = @newsalt, [��������� ����� ������] = 0 where @username = [��� ��� �����]
END
go

--����� ��������� ��������
--��������� ����������
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

CREATE FUNCTION dbo.CheckUserRights (
    @Username VARCHAR(100),
    @Password VARCHAR(256)
)
RETURNS INT
AS
BEGIN
    DECLARE @StoredHash VARCHAR(256);
    DECLARE @Salt VARCHAR(256);
    DECLARE @UserRights INT;
	DECLARE @RequirePAsswordChange bit;
    -- ��������� ���� ������ � ���� ��� ���������� ������������
    SELECT 
        @StoredHash = [��� ������],
        @Salt = [����],
        @UserRights = [����� �������],
		@RequirePasswordChange = [��������� ����� ������]
    FROM ����������.dbo.������������
    WHERE [��� ��� �����] = @Username;

    -- ���� ������������ �� ������, ������� -1
    IF @StoredHash IS NULL OR @Salt IS NULL
    BEGIN
        RETURN -1;
    END;
	IF @StoredHash = @Password and @RequirePasswordChange = 1
    BEGIN
        RETURN -5;
    END
	
   

    
    IF @StoredHash = @Password 
    BEGIN
        RETURN @UserRights;
    END
    ELSE
    BEGIN
        RETURN -2; -- ���� ������ �� ���������, ������� -2
    END
    RETURN -10; 
END;
go



CREATE PROCEDURE GetSaltForUser
    @Username VARCHAR(100)
AS
BEGIN
    DECLARE @Salt VARCHAR(256);

    -- �������� ���� ��� ���������� ������������
    SELECT @Salt = [����]
    FROM ������������
    WHERE [��� ��� �����] = @Username;

    -- ���������� ����
    SELECT @Salt AS Salt;
END;





go
CREATE PROCEDURE GetDependentTableName
	@ParentTableName varchar(50),
	@ColumnName varchar(50)
as
BEGIN
SELECT 
        OBJECT_NAME(fk.referenced_object_id) AS ReferencedTable
    FROM 
        sys.foreign_key_columns fkc
        JOIN sys.foreign_keys fk ON fkc.constraint_object_id = fk.object_id
        JOIN sys.columns c ON fkc.parent_column_id = c.column_id AND fkc.parent_object_id = c.object_id
    WHERE 
        OBJECT_NAME(fk.parent_object_id) = @ParentTableName
        AND c.name = @ColumnName
END;

/*
SELECT 
    fk.name AS ForeignKey,
    OBJECT_NAME(fk.parent_object_id) AS ParentTable,
    c1.name AS ParentColumn,
    OBJECT_NAME(fk.referenced_object_id) AS ReferencedTable,
    c2.name AS ReferencedColumn
FROM 
    sys.foreign_keys AS fk
INNER JOIN 
    sys.foreign_key_columns AS fkc ON fk.OBJECT_ID = fkc.constraint_object_id
INNER JOIN 
    sys.columns AS c1 ON fkc.parent_object_id = c1.object_id AND fkc.parent_column_id = c1.column_id
INNER JOIN 
    sys.columns AS c2 ON fkc.referenced_object_id = c2.object_id AND fkc.referenced_column_id = c2.column_id
WHERE 
    OBJECT_NAME(fk.parent_object_id) = '�����'
	*/
--����� ��������� ����������
go
--������ 

CREATE PROCEDURE dbo.GetPercentageUsersByGroupAndFaculty
(
    @StartDate DATE,
    @EndDate DATE
)
AS
BEGIN
    WITH ���������� AS (
        SELECT
            �.��� AS [��� ��������],
            �.[���] AS [��� ������]
        FROM
            [������ � �������] �
            INNER JOIN ������������ � ON �.[��� ��������] = �.���
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
            ������������ �
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
        CAST(YEAR(GETDATE()) - �.[��� �����������] AS INT) AS [����],
        ISNULL(�����������������.�_��������, 0) AS [� ��������],
        ISNULL(��������.�����_����������, 0) AS [����� ���������� ���������],
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
                ������������ �
                LEFT JOIN ������ � ON �.[��� ������] = �.[���]
            WHERE
                �.[��� ������] IS NOT NULL
            GROUP BY
                �.[��� ����������],
                �.[���]
        ) AS �������� ON �.[���] = ��������.[��� ������] AND �.[���] = ��������.[��� ����������];
END;
go


CREATE PROCEDURE dbo.GetDebtorsReport
(
    @StartDate DATETIME2,
    @EndDate DATETIME2
)
AS
BEGIN
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
            ������������ �
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
        CAST(YEAR(GETDATE()) - �.[��� �����������] AS INT) AS ����,
        VK.[�����] as [����� ������ �����],
        VK.[�������],
        VK.[���],
        VK.[���������� ���������]
    FROM
        ���������� VK
        INNER JOIN ������ � ON VK.[��� ������] = �.[���]
        INNER JOIN ��������� � ON VK.[��� ����������] = �.[���];
END;
go

CREATE PROCEDURE dbo.GetReaderInfoExact
AS
BEGIN
    SELECT
        [�������],
        [���],
        [��������],
        [���� ��������],
        [���������� �����],
        [����� ����������],
        [������ ��������],
        [����� ������������� ������],
        [������].[��������] AS '�������� ������',
        [���������].[��������] AS '�������� ����������',
        [��� ��� �����],
        [������ � �������].[���� ������] AS [���� ������ �����],
        [�����].[��������] AS [�������� �����],
        [�����].[���� �����] AS [���� �����]
    FROM [������������]
    INNER JOIN [������ � �������] ON [������������].[���] = [������ � �������].[��� ��������]
    INNER JOIN [�����] ON [������ � �������].[��� �����] = [�����].[���]
    INNER JOIN ������ ON ������.��� = ������������.[��� ������]
    INNER JOIN ��������� ON ���������.��� = ������.[��� ����������]
    WHERE [������ � �������].[���� ��������] IS NULL;
END;
GO


CREATE PROCEDURE FindBookLocation
    @sterm NVARCHAR(100)
AS
BEGIN
    SELECT 
        �����.�������� AS '�������� �����',
        ������.�����,
        ��������.[����� ��������] AS '����� ��������',
        ���������.�������� AS '�������� ���������',
        ���������.�������� AS '��� ���������',
        ���������.[����� ���������] AS '����� ���������'
    FROM
        �����
    INNER JOIN 
        (SELECT [����� �����].[��� �����], STRING_AGG([������].[�������] + ' ' + [������].[���] + ' ' + ������.��������, ', ') AS �����
         FROM [����� �����]
         INNER JOIN [������] ON [����� �����].[��� ������] = ������.���
         GROUP BY [����� �����].[��� �����]) AS ������ ON �����.��� = ������.[��� �����]
    INNER JOIN
        �������� ON �����.[��� ��������] = ��������.���
    INNER JOIN
        ��������� ON ��������.[��� ����] = ���������.���
    WHERE
        �����.�������� LIKE '%' + @sterm + '%' OR
        ������.����� LIKE '%' + @sterm + '%' OR
        EXISTS (
            SELECT 1
            FROM
                ����
            WHERE
                ����.[�������� �����] LIKE '%' + @sterm + '%'
        )
    GROUP BY �����.��������, ������.�����, ��������.[����� ��������], ���������.��������, ���������.��������, ���������.[����� ���������]
END;
GO



--����� �������
GO
-- ��������

CREATE PROCEDURE RemoveOutdatedReaders AS
BEGIN
    
    INSERT INTO �������������� ([�������], [���], [��������], [���� ��������],
                                 [���������� �����], [����� ����������], [������ ��������],
                                 [����� ������������� ������], [�������� ������], [���������],
                                 [�������������], [��� ��� �����])
    SELECT ������������.[�������], ������������.[���], ������������.[��������], ������������.[���� ��������],
           ������������.[���������� �����], ������������.[����� ����������], ������������.[������ ��������],
           ������������.[����� ������������� ������], ������.[��������], ���������.[��������],
           (SELECT STRING_AGG(CONCAT(�����.[��������], ' (', [������ � �������].[����������� �����], ')', ' - ', 
                                    CONVERT(VARCHAR(10), [���� ������], 104)), ', ')
            FROM [������ � �������]
            INNER JOIN ����� ON [������ � �������].[��� �����] = �����.[���]
            WHERE [������ � �������].[��� ��������] = ������������.[���] AND [���� ��������] IS NULL) AS [�������������],
           ������������.[��� ��� �����]
    FROM ������������
    INNER JOIN ������ ON ������������.[��� ������] = ������.���
    INNER JOIN ��������� ON ������.[��� ����������] = ���������.���
    WHERE YEAR(GETDATE()) > ������.[��� ���������];

    DELETE FROM [������ � �������] where [��� ��������] IN(SELECT [���] FROM ��������������)
    DELETE FROM ������������ WHERE [���] IN (SELECT [���] FROM ��������������);
END;
GO



--����� ��������
--����

DROP USER Guest1;
DROP USER librarian;
DROP USER libadmin;
DROP LOGIN Guest1;
DROP LOGIN librarian;
DROP LOGIN libadmin;
DROP ROLE LibrarianRole;
DROP ROLE AdminRole;

CREATE LOGIN Guest1 WITH PASSWORD = '1';
CREATE USER Guest1 FOR LOGIN Guest1;

GRANT SELECT ON ����������.dbo.BooksPublicInfo TO Guest1;
GRANT EXECUTE ON ����������.dbo.CheckUserRights TO Guest1;
GRANT EXECUTE ON ����������.dbo.GetSaltForUser TO Guest1;
GRANT EXECUTE ON ����������.dbo.SetNewPass TO Guest1;


GO
CREATE LOGIN librarian WITH PASSWORD = '123321';
CREATE USER librarian FOR LOGIN librarian;

CREATE LOGIN libadmin WITH PASSWORD = '33223311';
CREATE USER libadmin FOR LOGIN libadmin;

-- ������� ����
CREATE ROLE LibrarianRole;
CREATE ROLE AdminRole;

-- ��������� ���� �������������
EXEC sp_addrolemember 'LibrarianRole', 'librarian';
EXEC sp_addrolemember 'AdminRole', 'libadmin';

-- ��������� ����� �����
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.��������� TO LibrarianRole;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.������ TO LibrarianRole;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.������������ TO LibrarianRole;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.[������ � �������] TO LibrarianRole;
GRANT SELECT ON dbo.[�����] TO LibrarianRole;
GRANT SELECT ON ����������.dbo.BooksPublicInfo TO LibrarianRole;
GRANT EXECUTE ON ����������.dbo.CheckUserRights TO LibrarianRole;
GRANT EXECUTE ON ����������.dbo.GetSaltForUser TO LibrarianRole;
GRANT EXECUTE ON dbo.GetPercentageUsersByGroupAndFaculty TO LibrarianRole;
GRANT EXECUTE ON dbo.GetDebtorsReport TO LibrarianRole;
GRANT EXECUTE ON dbo.GetReaderInfoExact TO LibrarianRole;
GRANT EXECUTE ON FindBookLocation TO LibrarianRole;
GRANT EXECUTE ON ����������.dbo.SetNewPass TO LibrarianRole;
GRANT CONTROL ON DATABASE::[����������] TO AdminRole;

--����� ����

GO


