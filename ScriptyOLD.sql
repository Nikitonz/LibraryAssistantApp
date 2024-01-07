use Библиотека
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
alter table [Выдача]
drop constraint FK_Книга_Выдача,
constraint FK_Выдача_Сотрудники,
constraint FK_Выдача_Читатель
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
drop table Выдача
go
drop table Стеллажи
go
drop table Читатель
go
drop table [Помещения]
go
drop table Сотрудники
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

create table Выдача
(Код int primary key not null identity,
[Код читателя] int,
[Код книги] int,
[Инвертарный номер] varchar(40),
[Дата выдачи] date not null,
[Дата возврата] date,
[Код сотрудника, выдавшего книгу] int
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

create table Читатель
(Код int primary key not null identity,
[Фамилия] varchar(40),
[Имя] varchar(40),
[Отчество] varchar(40),
[Дата рождения] date,
[Контактный номер] varchar(40),
[Адрес проживания] varchar(40),
[Данные паспорта] char(60),
[Номер читательского билета] varchar(40)
)
GO

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
alter table [Выдача]
add constraint FK_Книга_Выдача  foreign key ([Код книги]) references Книга(Код),
constraint FK_Выдача_Сотрудники  foreign key ([Код сотрудника, выдавшего книгу]) references Сотрудники(Код),
constraint FK_Выдача_Читатель  foreign key ([Код читателя]) references Читатель(Код)
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


insert into Жанр
values
  ('Практическое пособие')

insert into Издательство
values
  ('ГГУ имени Ф. Скорины','Гомель','Советская 104')
  
insert into Читатель
values
  ('Обухов','Никита','Михайлович','2002-06-21','+37529#######','Свиридова ##','HB#######','number_bilet')

insert into Авторы
values
  ('Белокурский','Максим','Сергеевич','Беларусь'),
  ('Рябченко','Наталия','Валерьевна','Беларусь'),
  ('Атвиновский','Александр','Алексеевич','Беларусь')


insert into Помещения(Название,[Адрес помещения])
values
  ('1й читальный зал при ГГУ им. Ф. Скорины','ул. Советская 102'),
  ('2й читальный зал при ГГУ им. Ф. Скорины','ул. Советская 102')
insert into Стеллажи([Код зала],[Номер стеллажа])
values
  (1,1)

insert into Книга
values
  (1,1,1,'Дифференциальные уравнения', 2022, 47, 'русский', 30)
  
insert into [Автор книги]([Код книги],[Код автора])
values
  (1,1),
  (1,2),
  (1,3)

  


insert into Сотрудники
values
  (1,'Иванов','Петр','Ильич','библиотекарь',3),
  (1,'Семченко','Алла','Сергеевна','уборщица',2)
  
insert into [Поступление книг]([Код книги],[Код поставки],[Число книг])
values
  (1,1,10)
  


insert into [Поставки]
values
  ('2022-05-07',1,'ГГУ им. Ф. Скорины')
  