use Библиотека;


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
DROP TABLE АрхивЧитателей
alter table Книга
drop constraint FK_Книга_Издательство,
constraint FK_Книга_Жанр,
constraint FK_Книга_Стеллаж
alter table [Автор книги]
drop constraint FK_Книга_Автор_книги,
constraint FK_Автор_книги_Авторы
alter table [Поступление книг]
drop constraint FK_Книга_Поступления_книг,
constraint FK_Поступление_книг_Поставки
alter table [Списание книг]
drop constraint FK_Книга_Списание_книг,
constraint FK_Списание_книг_Списание
alter table [Выдача и возврат]
drop constraint FK_Выдача_и_возврат_Книга,
constraint FK_Выдача_и_возврат_Сотрудники,
constraint FK_Выдача_и_возврат_Пользователь
alter table [Стеллажи]
drop constraint FK_Стеллажи_Помещения
alter table [Сотрудники]
drop constraint FK_Сотрудники_Помещения
alter table [Поставки]
drop constraint FK_Поставки_Пользователь
alter table [Списание]
drop constraint FK_Списание_Пользователь
alter table Группа
drop constraint FK_Группа_Факультеты
alter table Пользователь
drop constraint FK_Пользователь_Группы

go
drop table Издательство
drop table Жанр
drop table Книга
drop table [Автор книги]
drop table Авторы
drop table [Поступление книг]
drop table Поставки
drop table [Списание книг]
drop table Списание
drop table [Выдача и возврат]
drop table Стеллажи

drop table [Помещения]
drop table Сотрудники
drop table Факультет
drop table Группа
drop table Пользователь
GO

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
[Инвентарный номер] varchar(40),
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

CREATE TABLE Пользователь
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
[Имя для входа] VARCHAR(100),
[Права доступа] INT NOT NULL DEFAULT 3, -- 0: Админ, 1: Библиотекарь, 2: Гость, 3: Читатель
[Хэш пароля] VARCHAR(256),
[Соль] VARCHAR(256),
[Требовать смены пароля] bit default 1
);

create table Сотрудники
(Код int primary key not null identity,
[Код помещения] int,
[Код пользователя] int unique,
[Занимаемая должность] varchar(40),
[Стаж] int
)
GO


CREATE TABLE Группа
(
Код INT PRIMARY KEY NOT NULL IDENTITY,
Название VARCHAR(40) NOT NULL,
[Год поступления] INT NOT NULL,
[Год окончания] INT NOT NULL,
[Код факультета] INT NOT NULL
);
CREATE TABLE Факультет
(
Код INT PRIMARY KEY NOT NULL IDENTITY,
Название VARCHAR(40) NOT NULL
);


go



CREATE TABLE АрхивЧитателей (
        Код INT PRIMARY KEY NOT NULL IDENTITY,
        [Фамилия] VARCHAR(40),
        [Имя] VARCHAR(40),
        [Отчество] VARCHAR(40),
        [Дата рождения] DATE,
        [Контактный номер] VARCHAR(40),
        [Адрес проживания] VARCHAR(40),
        [Данные паспорта] CHAR(60),
        [Номер читательского билета] VARCHAR(40),
        [Название группы] varchar(20),
		[Факультет] varchar(20),
		[Задолженности] varchar(500),
        [Имя для входа] VARCHAR(100)
    );



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
constraint FK_Поступление_книг_Поставки  foreign key ([Код поставки]) references Поставки(Код)
GO
alter table [Списание книг]
add constraint FK_Книга_Списание_книг foreign key ([Код книги]) references Книга(Код),
constraint FK_Списание_книг_Списание  foreign key ([Код списания книг]) references Списание(Код)
GO
alter table [Выдача и возврат]
add constraint FK_Выдача_и_возврат_Книга foreign key ([Код книги]) references Книга(Код),
constraint FK_Выдача_и_возврат_Сотрудники foreign key ([Код сотрудника, выдавшего книгу]) references Сотрудники(Код),
constraint FK_Выдача_и_возврат_Пользователь foreign key ([Код читателя]) references Пользователь(Код)
GO
alter table [Стеллажи]
add constraint FK_Стеллажи_Помещения  foreign key ([Код зала]) references Помещения(Код)
GO
alter table [Сотрудники]
add constraint FK_Сотрудники_Помещения  foreign key ([Код помещения]) references Помещения(Код),
constraint FK_Сотрудники_Пользователь foreign key([Код пользователя]) references Пользователь(Код)
GO

alter table [Поставки]
add constraint FK_Поставки_Пользователь  foreign key ([Код сотрудника]) references Пользователь(Код)
GO

alter table [Списание]
add constraint FK_Списание_Пользователь  foreign key ([Код сотрудника]) references Пользователь(Код)
GO

alter table Пользователь
add constraint FK_Пользователь_Группы foreign key ([Код группы]) references Группа(Код)
GO
alter table Группа
add constraint FK_Группа_Факультеты foreign key ([Код факультета]) references Факультет(Код)
GO

-- Для таблицы Жанр
INSERT INTO Жанр ([Название жанра])
VALUES 
    ('Учебник'),
    ('Монография'),
    ('Научная литература'),
    ('Справочник');

-- Для таблицы Авторы
INSERT INTO Авторы ([Фамилия], [Имя], [Отчество], [Страна автора])
VALUES 
    ('Лаврентьев', 'Михаил', 'Алексеевич', 'Россия'),
    ('Кудрявцев', 'Леонид', 'Дмитриевич', 'Россия'),
    ('Фихтенгольц', 'Григорий', 'Михайлович', 'Россия'),
    ('Таненбаум', 'Эндрю', '', 'США'),
    ('Дейтел', 'Гарви', '', 'США'),
    ('Дейтел', 'Пол', '', 'США'),
    ('Стрельников', 'Александр', 'Владимирович', 'Беларусь'),
    ('Забродин', 'Юрий', 'Александрович', 'Беларусь');

-- Для таблицы Помещения
INSERT INTO Помещения ([Название], [Адрес помещения])
VALUES 
    ('Главный читальный зал', 'ул. Ленина, 10'),
    ('Научный отдел', 'ул. Пушкина, 5'),
    ('Абонемент', 'ул. Кирова, 15');

-- Для таблицы Стеллажи
INSERT INTO Стеллажи ([Код зала], [Номер стеллажа])
VALUES 
    (1, 'A1'),
    (1, 'A2'),
    (1, 'B1'),
    (2, 'C1'),
    (3, 'A1');

INSERT INTO Издательство ([Название], [Город], [Адрес])
VALUES 
    ('ГГУ им. Ф. Скорины', 'Гомель', 'Советская 104'),
    ('БГУ', 'Минск', 'пр. Независимости 4')
-- Для таблицы Книга
INSERT INTO Книга ([Код издательства], [Код жанра], [Код стеллажа], [Название], [Год выпуска], [Число страниц], [Язык книги], [Обложка], [Краткое описание], [Цена])
VALUES 
    (1, 1, 1, 'Математический анализ', 2020, 800, 'русский', NULL, 'Учебник по математическому анализу', 35.99),
    (1, 2, 1, 'Основы программирования', 2019, 600, 'русский', NULL, 'Монография по основам программирования', 40.99),
    (2, 3, 2, 'Физика', 2018, 750, 'русский', NULL, 'Научная литература по физике', 28.99),
    (2, 1, 3, 'Химия', 2021, 900, 'русский', NULL, 'Учебник по химии', 32.99),
    (1, 4, 4, 'Справочник инженера', 2017, 1000, 'русский', NULL, 'Справочник для инженеров', 50.99);
	-- (SELECT BulkColumn FROM Openrowset(Bulk 'D:\Downloads\43w8XwCrfeQ.jpg', Single_Blob) as Image)
-- Для таблицы [Автор книги]
INSERT INTO [Автор книги] ([Код книги], [Код автора])
VALUES 
    (1, 1),
    (1, 2),
    (2, 3),
    (2, 4),
    (3, 5),
    (3, 6),
    (4, 7),
    (5, 8);





-- Для таблицы Факультет
INSERT INTO Факультет (Название)
VALUES 
    ('математики и ТП'),
    ('физики');
-- Для таблицы Группа
INSERT INTO Группа (Название, [Год поступления], [Год окончания], [Код факультета])
VALUES 
    ('ПИ-20', 2020, 2023, 1),
    ('ПО-19', 2019, 2023, 1),
    ('ИТП-21', 2019, 2025, 1),
    ('ЭК-22', 2020, 2025, 1),
    ('М-20', 2020, 2024, 2);



-- Для таблицы Читатель
INSERT INTO Пользователь ([Фамилия], [Имя], [Отчество], [Дата рождения], [Контактный номер], [Адрес проживания], [Данные паспорта], [Номер читательского билета], [Код группы])
VALUES 
    ('Иванов', 'Иван', 'Иванович', '1999-03-15', '+375291234567', 'ул. Ленина, 10', 'MP1234567', 'C001', 1),
    ('Петрова', 'Елена', 'Сергеевна', '2000-05-20', '+375291234568', 'ул. Пушкина, 5', 'MP2345678', 'C002', 2),
    ('Сидоров', 'Алексей', 'Петрович', '1998-07-25', '+375291234569', 'ул. Кирова, 15', 'MP3456789', 'C003', 3)

INSERT INTO Пользователь ([Фамилия], [Имя], [Отчество], [Дата рождения], [Контактный номер], [Адрес проживания], [Данные паспорта], [Имя для входа], [Права доступа], [Хэш пароля], Соль, [Требовать смены пароля])
VALUES 
    ('Петров', 'Всеволод', 'Анатольевич', '1990-04-14', '+3752922334433', 'ул. Ленина, 13', 'НВ2987656', 'admin1', 0, 'B97/cfUafV/dcI4Y708CcouHo6d4wzY0om2d0wIgO6w=', 'N4KC7DaFp9HnV8DEdy3p7A==',0),
	('Леонтьев', 'Леонид', 'Иванович', '1978-04-14', '+3752922334431', 'ул. Свиридова, 22', 'НВ2932323', 'librarian1', 1, 'U1WD1Izi9aimqcMijIvnQHczSmH5dBIvloMWUyrMMMc=', '1bCOUtL+Khv7XQb0ABYr2A==',0);
-- Для таблицы Сотрудники
INSERT INTO Сотрудники ([Код пользователя], [Занимаемая должность], [Стаж])
VALUES 
	(4,'Администратор', 3),
    (5,'Библиотекарь', 10);

-- Для таблицы Поставки
INSERT INTO Поставки ([Дата поставки], [Код сотрудника], [Поставщик])
VALUES 
    ('2023-01-15', 1, 'Книгоиздательство "Русский Классик"'),
    ('2023-02-20', 1, 'Издательский дом "Москва"'),
    ('2023-03-25', 1, 'Издательство "Огонек"');

-- Для таблицы [Поступление книг]
INSERT INTO [Поступление книг] ([Код книги], [Код поставки], [Число книг])
VALUES 
    (1, 1, 20),
    (2, 2, 15),
    (3, 3, 30),
    (4, 1, 25);

-- Для таблицы [Выдача и возврат]
INSERT INTO [Выдача и возврат] ([Код читателя], [Код книги], [Инвентарный номер], [Дата выдачи],[Дата возврата], [Код сотрудника, выдавшего книгу])
VALUES 
    (1, 1, 'INV001', '2023-04-01',null, 2),
    (2, 2, 'INV002', '2023-05-05',null, 2),
    (3, 3, 'INV003', '2023-06-10','2024-05-05', 2)

GO

--ПУБЛИЧНАЯ ИНФОРМАЦИЯ
CREATE FUNCTION BooksPublicInfo(
    @SearchTerm VARCHAR(100)
)
RETURNS TABLE
AS
RETURN
(
    SELECT 
        Книга.[Название], 
        Автор.Автор AS Автор,
        Жанр.[Название жанра] AS Жанр,
        Издательство.[Название] AS Издательство,
        Книга.[Год выпуска], 
        Книга.[Число страниц], 
        Книга.[Язык книги], 
        Книга.[Обложка],
        COUNT(DISTINCT CASE WHEN ([Выдача и возврат].[Дата выдачи] IS NOT NULL 
            AND [Выдача и возврат].[Дата возврата] IS NOT NULL
            AND [Выдача и возврат].[Книга утеряна] IS NULL)
            OR [Выдача и возврат].[Дата выдачи] IS NULL
            THEN Книга.Код ELSE NULL END) AS Доступность,
        COUNT(CASE WHEN [Выдача и возврат].[Дата выдачи] IS NOT NULL THEN [Выдача и возврат].[Код читателя] ELSE NULL END) AS [Как часто брали], 
        Книга.[Краткое описание]
    FROM 
        Книга
    JOIN 
        Жанр ON Книга.[Код жанра] = Жанр.Код
    JOIN 
        [Издательство] ON Книга.[Код издательства] = [Издательство].Код
    LEFT JOIN 
        (SELECT 
            [Автор книги].[Код книги],
            STRING_AGG([Авторы].[Фамилия] + ' ' + [Авторы].[Имя] + ' ' + Авторы.Отчество, ', ') AS Автор
         FROM 
            [Автор книги]
         JOIN 
            [Авторы] ON [Авторы].Код = [Автор книги].[Код автора]
         GROUP BY 
            [Автор книги].[Код книги]
        ) AS Автор ON Автор.[Код книги] = Книга.Код
    LEFT JOIN 
        [Выдача и возврат] ON [Выдача и возврат].[Код книги] = Книга.Код
    WHERE 
        Книга.Название LIKE '%' + @SearchTerm + '%'
        OR Автор.Автор LIKE '%' + @SearchTerm + '%'
        OR Жанр.[Название жанра] LIKE '%' + @SearchTerm + '%'
    GROUP BY
        Книга.[Название], 
        Автор.Автор,
        Жанр.[Название жанра],
        Издательство.[Название],
        Книга.[Год выпуска], 
        Книга.[Число страниц], 
        Книга.[Язык книги], 
        Книга.[Обложка],
        Книга.[Краткое описание]
);

go

--КОНЕЦ ПУБЛИЧНАЯ ИНФОРМАЦИЯ
GO
--СЛУЖЕБНЫЕ ОПЕРАЦИИ

CREATE PROCEDURE SetNewPass
	@username NVARCHAR(255),
    @hashedpass NVARCHAR(255),
	@newsalt NVARCHAR(255)
AS
BEGIN
	update Пользователь set [Хэш пароля]=@hashedpass, Соль = @newsalt, [Требовать смены пароля] = 0 where @username = [Имя для входа]
END
go

--КОНЕЦ СЛУЖЕБНЫЕ ОПЕРАЦИИ
--СЛУЖЕБНАЯ ИНФОРМАЦИЯ
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
    -- Получение хэша пароля и соли для указанного пользователя
    SELECT 
        @StoredHash = [Хэш пароля],
        @Salt = [Соль],
        @UserRights = [Права доступа],
		@RequirePasswordChange = [Требовать смены пароля]
    FROM Библиотека.dbo.Пользователь
    WHERE [Имя для входа] = @Username;

    -- Если пользователь не найден, вернуть -1
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
        RETURN -2; -- Если пароли не совпадают, вернуть -2
    END
    RETURN -10; 
END;
go



CREATE PROCEDURE GetSaltForUser
    @Username VARCHAR(100)
AS
BEGIN
    DECLARE @Salt VARCHAR(256);

    -- Получаем соль для указанного пользователя
    SELECT @Salt = [Соль]
    FROM Пользователь
    WHERE [Имя для входа] = @Username;

    -- Возвращаем соль
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
    OBJECT_NAME(fk.parent_object_id) = 'Книга'
	*/
--КОНЕЦ СЛУЖЕБНАЯ ИНФОРМАЦИЯ
go
--ОТЧЕТЫ 

CREATE PROCEDURE dbo.GetPercentageUsersByGroupAndFaculty
(
    @StartDate DATE,
    @EndDate DATE
)
AS
BEGIN
    WITH ВыдачиКниг AS (
        SELECT
            Ч.Код AS [Код читателя],
            Г.[Код] AS [Код группы]
        FROM
            [Выдача и возврат] В
            INNER JOIN Пользователь Ч ON В.[Код читателя] = Ч.Код
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
            Пользователь Ч
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
        CAST(YEAR(GETDATE()) - Г.[Год поступления] AS INT) AS [Курс],
        ISNULL(ЧитателиСВыдачами.С_выдачами, 0) AS [С выдачами],
        ISNULL(Читатели.Общее_количество, 0) AS [Общее количество читателей],
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
                Пользователь Ч
                LEFT JOIN Группа Г ON Ч.[Код группы] = Г.[Код]
            WHERE
                Ч.[Код группы] IS NOT NULL
            GROUP BY
                Г.[Код факультета],
                Г.[Код]
        ) AS Читатели ON Г.[Код] = Читатели.[Код группы] AND Ф.[Код] = Читатели.[Код факультета];
END;
go


CREATE PROCEDURE dbo.GetDebtorsReport
(
    @StartDate DATETIME2,
    @EndDate DATETIME2
)
AS
BEGIN
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
            Пользователь Ч
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
        CAST(YEAR(GETDATE()) - Г.[Год поступления] AS INT) AS Курс,
        VK.[Месяц] as [Месяц выдачи книги],
        VK.[Семестр],
        VK.[Год],
        VK.[Количество должников]
    FROM
        ВыдачиКниг VK
        INNER JOIN Группа Г ON VK.[Код группы] = Г.[Код]
        INNER JOIN Факультет Ф ON VK.[Код факультета] = Ф.[Код];
END;
go

CREATE PROCEDURE dbo.GetReaderInfoExact
AS
BEGIN
    SELECT
        [Фамилия],
        [Имя],
        [Отчество],
        [Дата рождения],
        [Контактный номер],
        [Адрес проживания],
        [Данные паспорта],
        [Номер читательского билета],
        [Группа].[Название] AS 'Название группы',
        [Факультет].[Название] AS 'Название факультета',
        [Имя для входа],
        [Выдача и возврат].[Дата выдачи] AS [Дата выдачи книги],
        [Книга].[Название] AS [Название книги],
        [Книга].[Язык книги] AS [Язык книги]
    FROM [Пользователь]
    INNER JOIN [Выдача и возврат] ON [Пользователь].[Код] = [Выдача и возврат].[Код читателя]
    INNER JOIN [Книга] ON [Выдача и возврат].[Код книги] = [Книга].[Код]
    INNER JOIN Группа ON Группа.Код = Пользователь.[Код группы]
    INNER JOIN Факультет ON Факультет.Код = Группа.[Код факультета]
    WHERE [Выдача и возврат].[Дата возврата] IS NULL;
END;
GO


CREATE PROCEDURE FindBookLocation
    @sterm NVARCHAR(100)
AS
BEGIN
    SELECT 
        Книга.Название AS 'Название книги',
        Авторы.Автор,
        Стеллажи.[Номер стеллажа] AS 'Номер стеллажа',
        Помещения.Название AS 'Название помещения',
        Помещения.Название AS 'Тип помещения',
        Помещения.[Адрес помещения] AS 'Адрес помещения'
    FROM
        Книга
    INNER JOIN 
        (SELECT [Автор книги].[Код книги], STRING_AGG([Авторы].[Фамилия] + ' ' + [Авторы].[Имя] + ' ' + Авторы.Отчество, ', ') AS Автор
         FROM [Автор книги]
         INNER JOIN [Авторы] ON [Автор книги].[Код автора] = Авторы.Код
         GROUP BY [Автор книги].[Код книги]) AS Авторы ON Книга.Код = Авторы.[Код книги]
    INNER JOIN
        Стеллажи ON Книга.[Код стеллажа] = Стеллажи.Код
    INNER JOIN
        Помещения ON Стеллажи.[Код зала] = Помещения.Код
    WHERE
        Книга.Название LIKE '%' + @sterm + '%' OR
        Авторы.Автор LIKE '%' + @sterm + '%' OR
        EXISTS (
            SELECT 1
            FROM
                Жанр
            WHERE
                Жанр.[Название жанра] LIKE '%' + @sterm + '%'
        )
    GROUP BY Книга.Название, Авторы.Автор, Стеллажи.[Номер стеллажа], Помещения.Название, Помещения.Название, Помещения.[Адрес помещения]
END;
GO



--КОНЕЦ ОТЧЕТОВ
GO
-- ОПЕРАЦИИ

CREATE PROCEDURE RemoveOutdatedReaders AS
BEGIN
    
    INSERT INTO АрхивЧитателей ([Фамилия], [Имя], [Отчество], [Дата рождения],
                                 [Контактный номер], [Адрес проживания], [Данные паспорта],
                                 [Номер читательского билета], [Название группы], [Факультет],
                                 [Задолженности], [Имя для входа])
    SELECT Пользователь.[Фамилия], Пользователь.[Имя], Пользователь.[Отчество], Пользователь.[Дата рождения],
           Пользователь.[Контактный номер], Пользователь.[Адрес проживания], Пользователь.[Данные паспорта],
           Пользователь.[Номер читательского билета], Группа.[Название], Факультет.[Название],
           (SELECT STRING_AGG(CONCAT(Книга.[Название], ' (', [Выдача и возврат].[Инвентарный номер], ')', ' - ', 
                                    CONVERT(VARCHAR(10), [Дата выдачи], 104)), ', ')
            FROM [Выдача и возврат]
            INNER JOIN Книга ON [Выдача и возврат].[Код книги] = Книга.[Код]
            WHERE [Выдача и возврат].[Код читателя] = Пользователь.[Код] AND [Дата возврата] IS NULL) AS [Задолженности],
           Пользователь.[Имя для входа]
    FROM Пользователь
    INNER JOIN Группа ON Пользователь.[Код группы] = Группа.Код
    INNER JOIN Факультет ON Группа.[Код факультета] = Факультет.Код
    WHERE YEAR(GETDATE()) > Группа.[Год окончания];

    DELETE FROM [Выдача и возврат] where [Код читателя] IN(SELECT [Код] FROM АрхивЧитателей)
    DELETE FROM Пользователь WHERE [Код] IN (SELECT [Код] FROM АрхивЧитателей);
END;
GO



--КОНЕЦ ОПЕРАЦИИ
--РОЛИ

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

GRANT SELECT ON Библиотека.dbo.BooksPublicInfo TO Guest1;
GRANT EXECUTE ON Библиотека.dbo.CheckUserRights TO Guest1;
GRANT EXECUTE ON Библиотека.dbo.GetSaltForUser TO Guest1;
GRANT EXECUTE ON Библиотека.dbo.SetNewPass TO Guest1;


GO
CREATE LOGIN librarian WITH PASSWORD = '123321';
CREATE USER librarian FOR LOGIN librarian;

CREATE LOGIN libadmin WITH PASSWORD = '33223311';
CREATE USER libadmin FOR LOGIN libadmin;

-- Создаем роли
CREATE ROLE LibrarianRole;
CREATE ROLE AdminRole;

-- Назначаем роли пользователям
EXEC sp_addrolemember 'LibrarianRole', 'librarian';
EXEC sp_addrolemember 'AdminRole', 'libadmin';

-- Назначаем права ролям
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.Факультет TO LibrarianRole;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.Группа TO LibrarianRole;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.Пользователь TO LibrarianRole;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.[Выдача и возврат] TO LibrarianRole;
GRANT SELECT ON dbo.[Книга] TO LibrarianRole;
GRANT SELECT ON Библиотека.dbo.BooksPublicInfo TO LibrarianRole;
GRANT EXECUTE ON Библиотека.dbo.CheckUserRights TO LibrarianRole;
GRANT EXECUTE ON Библиотека.dbo.GetSaltForUser TO LibrarianRole;
GRANT EXECUTE ON dbo.GetPercentageUsersByGroupAndFaculty TO LibrarianRole;
GRANT EXECUTE ON dbo.GetDebtorsReport TO LibrarianRole;
GRANT EXECUTE ON dbo.GetReaderInfoExact TO LibrarianRole;
GRANT EXECUTE ON FindBookLocation TO LibrarianRole;
GRANT EXECUTE ON Библиотека.dbo.SetNewPass TO LibrarianRole;
GRANT CONTROL ON DATABASE::[Библиотека] TO AdminRole;

--КОНЕЦ РОЛИ

GO


