use Библиотека;


DROP PROCEDURE GetUserRoles;
DROP PROCEDURE GetUserTablePermissions;
DROP FUNCTION SearchBooks;
DROP PROCEDURE УвеличитьКурсГрупп;
DROP TRIGGER trg_УдалитьЧитателейПриОбновленииГруппы;
DROP FUNCTION dbo.GetPercentageUsersByGroupAndFaculty;
DROP FUNCTION dbo.GetDebtorsReport
DROP FUNCTION dbo.GetBookStatusAndDueDate
DROP FUNCTION BooksPublicInfo
GO
alter table Книга
drop constraint FK_Книга_Издательство,
constraint FK_Книга_Жанр,
constraint FK_Книга_Стеллаж
GO
alter table [Автор книги]
drop constraint FK_Книга_Автор_книги,
constraint FK_Автор_книги_Авторы
GO
alter table [Поступление книг]
drop constraint FK_Книга_Поступления_книг,
constraint FK_Поступление_книг_Поставки
GO
alter table [Списание книг]
drop constraint FK_Книга_Списание_книг,
constraint FK_Списание_книг_Списание
GO
alter table [Выдача и возврат]
drop constraint FK_Выдача_Возврат_Книга,
constraint FK_Выдача_Возврат_Сотрудники,
constraint FK_Выдача_Возврат_Читатель
GO
alter table [Стеллажи]
drop constraint FK_Стеллажи_Помещения
GO
alter table [Сотрудники]
drop constraint FK_Сотрудники_Помещения
go
alter table [Поставки]
drop constraint FK_Поставки_Сотрудники
go
alter table [Списание]
drop constraint FK_Списание_Сотрудники
go
alter table Группа
drop constraint FK_Группа_Факультеты
go
alter table Читатель
drop constraint FK_Читатель_Группы
go
drop table Операции
go
drop table Издательство
go
drop table Жанр
go
drop table Книга
go
drop table [Автор книги]
go
drop table Авторы
go
drop table [Поступление книг]
go
drop table Поставки
go
drop table [Списание книг]
go
drop table Списание
go
drop table [Выдача и возврат]
go
drop table Стеллажи
go
drop table Читатель
go
drop table [Помещения]
go
drop table Сотрудники
go
drop table Факультет
go
drop table Группа
go


Create table Издательство
(Код int not null primary key identity,
Название varchar(50),
Город varchar(40),
Адрес varchar(80) not null
)
go

create table Жанр
(Код int primary key not null identity,
[Название жанра] varchar(40) not null
)
GO

create table Книга
(Код int primary key not null identity,
[Код издательства] int,
[Код жанра] int,
[Код стеллажа] int,
[Название] varchar(50),
[Год выпуска] int,
[Число страниц] int,
[Язык книги] varchar(20) not null,
[Обложка] VARBINARY(MAX),
[Краткое описание] varchar(4000),
[Цена] money
)
GO

create table [Автор книги]
(Код int primary key not null identity,
[Код книги] int,
[Код автора] int
)
GO

create table Авторы
(Код int primary key not null identity,
[Фамилия] varchar(40),
[Имя] varchar(40),
[Отчество] varchar(40),
[Страна автора] varchar(40)
)
GO

create table [Поступление книг]
(Код int primary key not null identity,
[Код книги] int,
[Код поставки] Int,
[Число книг] int
)
GO

create table Поставки
(Код int primary key not null identity,
[Дата поставки] date,
[Код сотрудника] int,
[Поставщик] varchar(40)
)
GO

create table [Списание книг]
(Код int primary key not null identity,
[Код книги] int,
[Код списания книг] int,
[Число книг] int
)
GO

create table Списание
(Код int primary key not null identity,
[Дата списания] date,
[Код сотрудника] int
)
GO

create table [Выдача и возврат]
(
Код int primary key not null identity,
[Код читателя] int,
[Код книги] int,
[Инвертарный номер] varchar(40),
[Дата выдачи] date not null,
[Дата возврата] date,
[Код сотрудника, выдавшего книгу] int,
[Книга утеряна] bit
)
GO

create table Стеллажи
(Код int primary key not null identity,
[Код зала] int not null,
[Номер стеллажа] varchar(4)
)
GO

create table Помещения
(Код int primary key not null identity,
[Название] varchar(40),
[Читальный зал] bit,
[Архив] bit,
[Абонемент] bit,
[Адрес помещения] varchar(40) 
)
GO

CREATE TABLE Читатель
(
Код INT PRIMARY KEY NOT NULL IDENTITY,
[Фамилия] VARCHAR(40),
[Имя] VARCHAR(40),
[Отчество] VARCHAR(40),
[Дата рождения] DATE,
[Контактный номер] VARCHAR(40),
[Адрес проживания] VARCHAR(40),
[Данные паспорта] CHAR(60),
[Номер читательского билета] VARCHAR(40),
[Код группы] INT,
[Имя для входа] VARCHAR(100)
);

create table Сотрудники
(Код int primary key not null identity,
[Код помещения] int,
[Фамилия] varchar(40),
[Имя] varchar(40),
[Отчество] varchar(40),
[Занимаемая должность] varchar(40),
[Стаж] int
)
GO


CREATE TABLE Группа
(
Код INT PRIMARY KEY NOT NULL IDENTITY,
Название VARCHAR(40) NOT NULL,
Курс INT NOT NULL,
[Последний курс] INT NOT NULL,
[Код факультета] INT NOT NULL
);
CREATE TABLE Факультет
(
Код INT PRIMARY KEY NOT NULL IDENTITY,
Название VARCHAR(40) NOT NULL
);
go
alter table Книга
add constraint FK_Книга_Издательство  foreign key ([Код издательства]) references Издательство(Код),
constraint FK_Книга_Жанр  foreign key ([Код жанра]) references Жанр(Код),
constraint FK_Книга_Стеллаж  foreign key ([Код стеллажа]) references Стеллажи(Код)
GO
alter table [Автор книги]
add constraint FK_Книга_Автор_книги  foreign key ([Код книги]) references Книга(Код),
constraint FK_Автор_книги_Авторы  foreign key ([Код автора]) references Авторы(Код)
GO
alter table [Поступление книг]
add constraint FK_Книга_Поступления_книг foreign key ([Код книги]) references Книга(Код),
constraint FK_Поступление_книг_Поставки  foreign key ([Код поставки]) references Авторы(Код)
GO
alter table [Списание книг]
add constraint FK_Книга_Списание_книг foreign key ([Код книги]) references Поставки(Код),
constraint FK_Списание_книг_Списание  foreign key ([Код списания книг]) references Списание(Код)
GO
alter table [Выдача и возврат]
add constraint FK_Выдача_Возврат_Книга foreign key ([Код книги]) references Книга(Код),
constraint FK_Выдача_Возврат_Сотрудники foreign key ([Код сотрудника, выдавшего книгу]) references Сотрудники(Код),
constraint FK_Выдача_Возврат_Читатель foreign key ([Код читателя]) references Читатель(Код)
GO
alter table [Стеллажи]
add constraint FK_Стеллажи_Помещения  foreign key ([Код зала]) references Помещения(Код)
GO
alter table [Сотрудники]
add constraint FK_Сотрудники_Помещения  foreign key ([Код помещения]) references Помещения(Код)
GO

alter table [Поставки]
add constraint FK_Поставки_Сотрудники  foreign key ([Код сотрудника]) references Сотрудники(Код)
GO

alter table [Списание]
add constraint FK_Списание_Сотрудники  foreign key ([Код сотрудника]) references Сотрудники(Код)
GO

alter table Читатель
add constraint FK_Читатель_Группы foreign key ([Код группы]) references Группа(Код)
GO
alter table Группа
add constraint FK_Группа_Факультеты foreign key ([Код факультета]) references Факультет(Код)
GO

-- Для таблицы Жанр
INSERT INTO Жанр ([Название жанра])
VALUES 
    ('Фантастика'),
    ('Роман'),
    ('Детектив'),
    ('Поэзия');

-- Для таблицы Авторы
INSERT INTO Авторы ([Фамилия], [Имя], [Отчество], [Страна автора])
VALUES 
    ('Толстой', 'Лев', 'Николаевич', 'Россия'),
    ('Достоевский', 'Федор', 'Михайлович', 'Россия'),
    ('Пушкин', 'Александр', 'Сергеевич', 'Россия'),
    ('Чехов', 'Антон', 'Павлович', 'Россия');

-- Для таблицы Помещения
INSERT INTO Помещения ([Название], [Адрес помещения])
VALUES 
    ('Главный читальный зал', 'ул. Ленина, 10'),
    ('Детский отдел', 'ул. Пушкина, 5'),
    ('Абонемент', 'ул. Кирова, 15');

-- Для таблицы Стеллажи
INSERT INTO Стеллажи ([Код зала], [Номер стеллажа])
VALUES 
    (1, 'A1'),
    (1, 'A2'),
    (1, 'B1'),
    (2, 'C1'),
    (3, 'A1');

INSERT INTO Издательство values
('АСТ', 'Москва', 'Междугородняя 6'),
('ГГУ им. Ф. Скорины', 'Гомель', 'Советская ?')
-- Для таблицы Книга
INSERT INTO Книга ([Код издательства], [Код жанра], [Код стеллажа], [Название], [Год выпуска], [Число страниц], [Язык книги], [Обложка], [Краткое описание], [Цена])
VALUES 
    (1, 1, 1, 'Война и мир', 1869, 1225, 'русский', NULL, 'Эпический роман Льва Толстого о войне 1812 года', 25.99),
    (2, 2, 2, 'Преступление и наказание', 1866, 551, 'русский', NULL, 'Роман Федора Достоевского о преступлении и наказании', 19.99),
    (2, 3, 3, 'Евгений Онегин', 1833, 368, 'русский', NULL, 'Роман Александра Пушкина', 14.99),
    (1, 4, 4, 'Дама с собачкой', 1899, 192, 'русский', (SELECT BulkColumn FROM Openrowset(Bulk 'D:\Downloads\43w8XwCrfeQ.jpg', Single_Blob) as Image), 'Повесть Антона Чехова', 12.99);

-- Для таблицы [Автор книги]
INSERT INTO [Автор книги] ([Код книги], [Код автора])
VALUES 
    (1, 1),
    (2, 2),
    (3, 3),
    (4, 4);

-- Для таблицы Сотрудники
INSERT INTO Сотрудники ([Код помещения], [Фамилия], [Имя], [Отчество], [Занимаемая должность], [Стаж])
VALUES 
    (1, 'Иванов', 'Петр', 'Ильич', 'Библиотекарь', 5),
    (1, 'Петров', 'Иван', 'Сергеевич', 'Администратор', 3),
    (2, 'Сидорова', 'Анна', 'Петровна', 'Библиотекарь', 4),
    (3, 'Козлова', 'Елена', 'Ивановна', 'Кассир', 2);

-- Для таблицы Поставки
INSERT INTO Поставки ([Дата поставки], [Код сотрудника], [Поставщик])
VALUES 
    ('2023-01-15', 1, 'Книгоиздательство "Русский Классик"'),
    ('2023-02-20', 2, 'Издательский дом "Москва"'),
    ('2023-03-25', 3, 'Издательство "Огонек"');

-- Для таблицы [Поступление книг]
INSERT INTO [Поступление книг] ([Код книги], [Код поставки], [Число книг])
VALUES 
    (1, 1, 20),
    (2, 2, 15),
    (3, 3, 30),
    (4, 1, 25);


-- Для таблицы Факультет
INSERT INTO Факультет (Название)
VALUES 
    ('математики и ТП'),
    ('физики');
-- Для таблицы Группа
INSERT INTO Группа (Название, Курс, [Последний курс], [Код факультета])
VALUES 
    ('ПИ-20', 2, 4, 1),
    ('ПО-19', 1, 4, 1),
    ('ИТП-21', 1, 4, 1),
    ('ЭК-22', 1, 4, 1),
    ('М-20', 2, 4, 2);



-- Для таблицы Читатель
INSERT INTO Читатель ([Фамилия], [Имя], [Отчество], [Дата рождения], [Контактный номер], [Адрес проживания], [Данные паспорта], [Номер читательского билета], [Код группы])
VALUES 
    ('Иванов', 'Иван', 'Иванович', '1999-03-15', '+375291234567', 'ул. Ленина, 10', 'MP1234567', 'C001', 1),
    ('Петрова', 'Елена', 'Сергеевна', '2000-05-20', '+375291234568', 'ул. Пушкина, 5', 'MP2345678', 'C002', 2),
    ('Сидоров', 'Алексей', 'Петрович', '1998-07-25', '+375291234569', 'ул. Кирова, 15', 'MP3456789', 'C003', 3);

-- Для таблицы [Выдача и возврат]
INSERT INTO [Выдача и возврат] ([Код читателя], [Код книги], [Инвертарный номер], [Дата выдачи], [Код сотрудника, выдавшего книгу])
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
        Книга.Название AS 'Название книги',
        Авторы.Фамилия + ' ' + Авторы.Имя + ' ' + Авторы.Отчество AS 'Автор',
        Жанр.[Название жанра] AS 'Жанр',
        Издательство.Название AS 'Издательство',
        Книга.[Год выпуска] AS 'Год выпуска',
        Книга.[Число страниц] AS 'Число страниц',
        Книга.[Язык книги] AS 'Язык книги'
    FROM
        Книга
    JOIN
        Жанр ON Книга.[Код жанра] = Жанр.Код
    JOIN
        [Автор книги] ON [Автор книги].[Код книги] = Книга.Код
    JOIN
        Авторы ON Авторы.Код = [Автор книги].[Код автора]
    JOIN
        Издательство ON Книга.[Код издательства] = Издательство.Код 
    WHERE
        Книга.Название LIKE '%' + @SearchTerm + '%'
        OR Авторы.Фамилия LIKE '%' + @SearchTerm + '%'
        OR Авторы.Имя LIKE '%' + @SearchTerm + '%'
        OR Авторы.Отчество LIKE '%' + @SearchTerm + '%'
        OR Жанр.[Название жанра] LIKE '%' + @SearchTerm + '%'
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



CREATE TABLE Операции
(
    Код INT PRIMARY KEY NOT NULL IDENTITY,
    [Таблица] VARCHAR(50) NOT NULL,
    [Операция] VARCHAR(50) NOT NULL,
    [Дата операции] INT NOT NULL
);

go
CREATE PROCEDURE УвеличитьКурсГрупп
AS
BEGIN
	IF NOT EXISTS (SELECT 1 FROM Операции WHERE [Таблица] = 'Группа' AND [Операция] = 'Увеличение курса' AND YEAR([Дата операции]) = YEAR(GETDATE()))
	BEGIN
		UPDATE Группа
		SET Курс = Курс + 1;
		INSERT INTO Операции ([Таблица], [Операция], [Дата операции])
		VALUES ('Группа', 'Увеличение курса', YEAR(GETDATE()));
	END;
END;
go



CREATE TRIGGER trg_УдалитьЧитателейПриОбновленииГруппы
ON Группа
AFTER UPDATE
AS
BEGIN
    IF UPDATE([Последний курс]) OR UPDATE([Курс])
    BEGIN
        IF EXISTS (
            SELECT 1
            FROM Операции
            WHERE [Таблица] = 'Группа' 
                AND [Операция] = 'Увеличение курса' 
                AND [Дата операции] = YEAR(GETDATE())
				
        )
		BEGIN
            ROLLBACK;
            RETURN;
        END
        DELETE FROM Читатель
        WHERE [Код группы] IN (
            SELECT G.[Код]
            FROM Группа G
            INNER JOIN INSERTED I ON G.[Код] = I.[Код]
            WHERE G.[Курс] > G.[Последний курс]
        );

    
        DELETE FROM Группа
        WHERE [Курс] > [Последний курс];
    END
END;
GO

--exec УвеличитьКурсГрупп


CREATE FUNCTION dbo.GetPercentageUsersByGroupAndFaculty
(
    @StartDate DATE,
    @EndDate DATE
)
RETURNS TABLE
AS
RETURN
(
    WITH ВыдачиКниг AS (
        SELECT
            Ч.Код AS [Код читателя],
            Г.[Код] AS [Код группы]
        FROM
            [Выдача и возврат] В
            INNER JOIN Читатель Ч ON В.[Код читателя] = Ч.Код
            INNER JOIN Группа Г ON Ч.[Код группы] = Г.[Код]
        WHERE
            В.[Дата выдачи] BETWEEN @StartDate AND @EndDate
    ),
    ЧитателиСВыдачами AS (
        SELECT
            Г.[Код факультета],
            Г.[Код] AS [Код группы],
            COUNT(DISTINCT Ч.[Код]) AS С_выдачами
        FROM
            Читатель Ч
            INNER JOIN ВыдачиКниг ВК ON Ч.Код = ВК.[Код читателя]
            INNER JOIN Группа Г ON Ч.[Код группы] = Г.[Код]
        WHERE
            Ч.[Код группы] IS NOT NULL
        GROUP BY
            Г.[Код факультета],
            Г.[Код]
    )
    SELECT
        Ф.[Название] AS [Название факультета],
        Г.[Название] AS [Название группы],
        Г.[Курс],
        ISNULL(ЧитателиСВыдачами.С_выдачами, 0) AS [С выдачами],
        ISNULL(Читатели.Общее_количество, 0) AS [Общее количество],
        IIF(ISNULL(Читатели.Общее_количество, 0) = 0, 0, 100 * CAST(ISNULL(ЧитателиСВыдачами.С_выдачами, 0) AS FLOAT) / CAST(ISNULL(Читатели.Общее_количество, 0) AS FLOAT)) AS [Процент пользователей от читателей]
    FROM
        Факультет Ф
        INNER JOIN Группа Г ON Ф.[Код] = Г.[Код факультета]
        LEFT JOIN ЧитателиСВыдачами ON Г.[Код] = ЧитателиСВыдачами.[Код группы] AND Ф.[Код] = ЧитателиСВыдачами.[Код факультета]
        LEFT JOIN (
            SELECT
                Г.[Код факультета],
                Г.[Код] AS [Код группы],
                COUNT(DISTINCT Ч.[Код]) AS Общее_количество
            FROM
                Читатель Ч
                LEFT JOIN Группа Г ON Ч.[Код группы] = Г.[Код]
            WHERE
                Ч.[Код группы] IS NOT NULL
            GROUP BY
                Г.[Код факультета],
                Г.[Код]
        ) AS Читатели ON Г.[Код] = Читатели.[Код группы] AND Ф.[Код] = Читатели.[Код факультета]
);
go


CREATE FUNCTION dbo.GetDebtorsReport(@StartDate DATETIME2, @EndDate DATETIME2)
RETURNS TABLE
AS
RETURN
(
    WITH ВыдачиКниг AS (
        SELECT
            Г.[Код факультета],
            Ч.[Код группы],
            MONTH(В.[Дата выдачи]) AS [Месяц],
            CASE
                WHEN MONTH(В.[Дата выдачи]) BETWEEN 1 AND 6 THEN '1-й семестр'
                WHEN MONTH(В.[Дата выдачи]) BETWEEN 7 AND 12 THEN '2-й семестр'
            END AS [Семестр],
            YEAR(В.[Дата выдачи]) AS [Год],
            COUNT(*) AS [Количество должников]
        FROM
            Читатель Ч
            INNER JOIN [Выдача и возврат] В ON Ч.[Код] = В.[Код читателя] AND В.[Дата возврата] IS NULL -- Должники
            INNER JOIN Группа Г ON Ч.[Код группы] = Г.[Код]
        WHERE
            В.[Дата выдачи] BETWEEN @StartDate AND @EndDate
        GROUP BY
            Г.[Код факультета],
            Ч.[Код группы],
            MONTH(В.[Дата выдачи]),
            CASE
                WHEN MONTH(В.[Дата выдачи]) BETWEEN 1 AND 6 THEN '1-й семестр'
                WHEN MONTH(В.[Дата выдачи]) BETWEEN 7 AND 12 THEN '2-й семестр'
            END,
            YEAR(В.[Дата выдачи])
    )
    SELECT
        Ф.[Название] AS [Название факультета],
        Г.[Название] AS [Название группы],
        Г.[Курс],
        VK.[Месяц],
        VK.[Семестр],
        VK.[Год],
        VK.[Количество должников]
    FROM
        ВыдачиКниг VK
        INNER JOIN Группа Г ON VK.[Код группы] = Г.[Код]
        INNER JOIN Факультет Ф ON VK.[Код факультета] = Ф.[Код]
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
                FROM [Выдача и возврат]
                WHERE [Код читателя] = @ReaderID
                AND [Код книги] = @BookID
                AND [Дата возврата] IS NULL
            ) THEN 'Книга на руках'
            ELSE 'Книга сдана'
        END AS [Статус книги],
        DATEADD(MONTH, 1, [Выдача и возврат].[Дата выдачи]) AS [Ближайшая дата сдачи]--MIN(ISNULL([Дата возврата], '9999-12-31'))
    FROM [Выдача и возврат]
    WHERE [Код читателя] = @ReaderID
    AND [Код книги] = @BookID
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
--exec УвеличитьКурсГрупп


CREATE FUNCTION dbo.BooksPublicInfo(
    @SearchTerm VARCHAR(100)
)
RETURNS TABLE
AS
RETURN
(
    SELECT 
        Книга.[Название], 
        [Авторы].[Фамилия] + ' ' + [Авторы].[Имя] AS Автор,
        Жанр.[Название жанра] AS Жанр,
        Издательство.[Название] AS Издательство,
        Книга.[Год выпуска], 
        Книга.[Число страниц], 
        Книга.[Язык книги], 
        Книга.[Обложка], 
        CASE 
            WHEN (
                SELECT COUNT(*) 
                FROM [Выдача и возврат] 
                WHERE 
                    [Код книги] = Книга.Код AND 
                    [Дата возврата] IS NULL AND 
                    [Книга утеряна] = 0
            ) = 0 THEN 0
            ELSE 1
        END AS Доступность, 
        (
            SELECT COUNT(*) 
            FROM [Выдача и возврат] 
            WHERE 
                [Код книги] = Книга.Код
        ) AS [Как часто брали], 
        Книга.[Краткое описание]
    FROM 
        Книга
    JOIN 
        Жанр ON Книга.[Код жанра] = Жанр.Код
    JOIN 
        [Автор книги] ON [Автор книги].[Код книги] = Книга.Код
    JOIN 
        [Авторы] ON [Авторы].Код = [Автор книги].[Код автора]
    JOIN 
        [Издательство] ON Книга.[Код издательства] = [Издательство].Код
    WHERE 
        Книга.Название LIKE '%' + @SearchTerm + '%'
        OR [Авторы].[Фамилия] LIKE '%' + @SearchTerm + '%'
        OR [Авторы].[Имя] LIKE '%' + @SearchTerm + '%'
        OR [Авторы].[Отчество] LIKE '%' + @SearchTerm + '%'
        OR Жанр.[Название жанра] LIKE '%' + @SearchTerm + '%'
);
GO
DROP USER GuestTest;
GO
DROP LOGIN Guest;
GO
CREATE LOGIN Guest WITH PASSWORD = '1';
CREATE USER GuestTest FOR LOGIN Guest;
GRANT SELECT, INSERT ON dbo.ТаблицаДляМобилок TO GuestTest;
GO

