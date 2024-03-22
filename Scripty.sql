use ����������;


DROP PROCEDURE GetUserRoles;
DROP PROCEDURE GetUserTablePermissions;
DROP FUNCTION SearchBooks;
DROP PROCEDURE ������������������;
DROP TRIGGER trg_�����������������������������������;
DROP FUNCTION dbo.GetPercentageUsersByGroupAndFaculty;
DROP FUNCTION dbo.GetDebtorsReport
DROP FUNCTION dbo.GetBookStatusAndDueDate
DROP FUNCTION BooksPublicInfo
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
go
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

-- ��� ������� ����
INSERT INTO ���� ([�������� �����])
VALUES 
    ('����������'),
    ('�����'),
    ('��������'),
    ('������');

-- ��� ������� ������
INSERT INTO ������ ([�������], [���], [��������], [������ ������])
VALUES 
    ('�������', '���', '����������', '������'),
    ('�����������', '�����', '����������', '������'),
    ('������', '���������', '���������', '������'),
    ('�����', '�����', '��������', '������');

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

INSERT INTO ������������ values
('���', '������', '������������� 6'),
('��� ��. �. �������', '������', '��������� ?')
-- ��� ������� �����
INSERT INTO ����� ([��� ������������], [��� �����], [��� ��������], [��������], [��� �������], [����� �������], [���� �����], [�������], [������� ��������], [����])
VALUES 
    (1, 1, 1, '����� � ���', 1869, 1225, '�������', NULL, '��������� ����� ���� �������� � ����� 1812 ����', 25.99),
    (2, 2, 2, '������������ � ���������', 1866, 551, '�������', NULL, '����� ������ ������������ � ������������ � ���������', 19.99),
    (2, 3, 3, '������� ������', 1833, 368, '�������', NULL, '����� ���������� �������', 14.99),
    (1, 4, 4, '���� � ��������', 1899, 192, '�������', (SELECT BulkColumn FROM Openrowset(Bulk 'D:\Downloads\43w8XwCrfeQ.jpg', Single_Blob) as Image), '������� ������ ������', 12.99);

-- ��� ������� [����� �����]
INSERT INTO [����� �����] ([��� �����], [��� ������])
VALUES 
    (1, 1),
    (2, 2),
    (3, 3),
    (4, 4);

-- ��� ������� ����������
INSERT INTO ���������� ([��� ���������], [�������], [���], [��������], [���������� ���������], [����])
VALUES 
    (1, '������', '����', '�����', '������������', 5),
    (1, '������', '����', '���������', '�������������', 3),
    (2, '��������', '����', '��������', '������������', 4),
    (3, '�������', '�����', '��������', '������', 2);

-- ��� ������� ��������
INSERT INTO �������� ([���� ��������], [��� ����������], [���������])
VALUES 
    ('2023-01-15', 1, '����������������� "������� �������"'),
    ('2023-02-20', 2, '������������ ��� "������"'),
    ('2023-03-25', 3, '������������ "������"');

-- ��� ������� [����������� ����]
INSERT INTO [����������� ����] ([��� �����], [��� ��������], [����� ����])
VALUES 
    (1, 1, 20),
    (2, 2, 15),
    (3, 3, 30),
    (4, 1, 25);


-- ��� ������� ���������
INSERT INTO ��������� (��������)
VALUES 
    ('���������� � ��'),
    ('������');
-- ��� ������� ������
INSERT INTO ������ (��������, ����, [��������� ����], [��� ����������])
VALUES 
    ('��-20', 2, 4, 1),
    ('��-19', 1, 4, 1),
    ('���-21', 1, 4, 1),
    ('��-22', 1, 4, 1),
    ('�-20', 2, 4, 2);



-- ��� ������� ��������
INSERT INTO �������� ([�������], [���], [��������], [���� ��������], [���������� �����], [����� ����������], [������ ��������], [����� ������������� ������], [��� ������])
VALUES 
    ('������', '����', '��������', '1999-03-15', '+375291234567', '��. ������, 10', 'MP1234567', 'C001', 1),
    ('�������', '�����', '���������', '2000-05-20', '+375291234568', '��. �������, 5', 'MP2345678', 'C002', 2),
    ('�������', '�������', '��������', '1998-07-25', '+375291234569', '��. ������, 15', 'MP3456789', 'C003', 3);

-- ��� ������� [������ � �������]
INSERT INTO [������ � �������] ([��� ��������], [��� �����], [����������� �����], [���� ������], [��� ����������, ��������� �����])
VALUES 
    (1, 1, 'INV001', '2023-04-01', 1),
    (2, 2, 'INV002', '2023-04-05', 2),
    (3, 3, 'INV003', '2023-04-10', 3),
    (1, 4, 'INV004', '2023-04-15', 4),
    (2, 1, 'INV005', '2023-04-20', 1);
GO
CREATE FUNCTION SearchBooks(
    @SearchTerm VARCHAR(100)
)
RETURNS TABLE
AS
RETURN (
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
        ������ ON ������.��� = [����� �����].[��� ������]
    JOIN
        ������������ ON �����.[��� ������������] = ������������.��� 
    WHERE
        �����.�������� LIKE '%' + @SearchTerm + '%'
        OR ������.������� LIKE '%' + @SearchTerm + '%'
        OR ������.��� LIKE '%' + @SearchTerm + '%'
        OR ������.�������� LIKE '%' + @SearchTerm + '%'
        OR ����.[�������� �����] LIKE '%' + @SearchTerm + '%'
);
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

CREATE FUNCTION dbo.GetBookStatusAndDueDate
(
    @ReaderID INT,
    @BookID INT
)
RETURNS TABLE
AS
RETURN
(
    SELECT
        CASE
            WHEN EXISTS (
                SELECT 1
                FROM [������ � �������]
                WHERE [��� ��������] = @ReaderID
                AND [��� �����] = @BookID
                AND [���� ��������] IS NULL
            ) THEN '����� �� �����'
            ELSE '����� �����'
        END AS [������ �����],
        DATEADD(MONTH, 1, [������ � �������].[���� ������]) AS [��������� ���� �����]--MIN(ISNULL([���� ��������], '9999-12-31'))
    FROM [������ � �������]
    WHERE [��� ��������] = @ReaderID
    AND [��� �����] = @BookID
);
go

--select * from dbo.GetBookStatusAndDueDate(1,1)

--o1
--SELECT * FROM dbo.GetPercentageUsersByGroupAndFaculty('2020-01-01', '2024-12-31');
--o2
--SELECT * FROM dbo.GetDebtorsReport('2020-01-01', '2024-12-31');
--o3
--select * from dbo.GetBookStatusAndDueDate(3,2)
--y++
--exec ������������������


CREATE FUNCTION dbo.BooksPublicInfo(
    @SearchTerm VARCHAR(100)
)
RETURNS TABLE
AS
RETURN
(
    SELECT 
        �����.[��������], 
        [������].[�������] + ' ' + [������].[���] AS �����,
        ����.[�������� �����] AS ����,
        ������������.[��������] AS ������������,
        �����.[��� �������], 
        �����.[����� �������], 
        �����.[���� �����], 
        �����.[�������], 
        CASE 
            WHEN (
                SELECT COUNT(*) 
                FROM [������ � �������] 
                WHERE 
                    [��� �����] = �����.��� AND 
                    [���� ��������] IS NULL AND 
                    [����� �������] = 0
            ) = 0 THEN 0
            ELSE 1
        END AS �����������, 
        (
            SELECT COUNT(*) 
            FROM [������ � �������] 
            WHERE 
                [��� �����] = �����.���
        ) AS [��� ����� �����], 
        �����.[������� ��������]
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
        OR [������].[�������] LIKE '%' + @SearchTerm + '%'
        OR [������].[���] LIKE '%' + @SearchTerm + '%'
        OR [������].[��������] LIKE '%' + @SearchTerm + '%'
        OR ����.[�������� �����] LIKE '%' + @SearchTerm + '%'
);
GO
DROP USER GuestTest;
GO
DROP LOGIN Guest;
GO
CREATE LOGIN Guest WITH PASSWORD = '1';
CREATE USER GuestTest FOR LOGIN Guest;
GRANT SELECT, INSERT ON dbo.����������������� TO GuestTest;
GO

