use ����������
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
alter table [������]
drop constraint FK_�����_������,
constraint FK_������_����������,
constraint FK_������_��������
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
drop table ������
go
drop table ��������
go
drop table ��������
go
drop table [���������]
go
drop table ����������
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

create table ������
(��� int primary key not null identity,
[��� ��������] int,
[��� �����] int,
[����������� �����] varchar(40),
[���� ������] date not null,
[���� ��������] date,
[��� ����������, ��������� �����] int
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

create table ��������
(��� int primary key not null identity,
[�������] varchar(40),
[���] varchar(40),
[��������] varchar(40),
[���� ��������] date,
[���������� �����] varchar(40),
[����� ����������] varchar(40),
[������ ��������] char(60),
[����� ������������� ������] varchar(40)
)
GO

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
alter table [������]
add constraint FK_�����_������  foreign key ([��� �����]) references �����(���),
constraint FK_������_����������  foreign key ([��� ����������, ��������� �����]) references ����������(���),
constraint FK_������_��������  foreign key ([��� ��������]) references ��������(���)
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


insert into ����
values
  ('������������ �������')

insert into ������������
values
  ('��� ����� �. �������','������','��������� 104')
  
insert into ��������
values
  ('������','������','����������','2002-06-21','+37529#######','��������� ##','HB#######','number_bilet')

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
  (1,1,1,'���������������� ���������', 2022, 47, '�������', 30)
  
insert into [����� �����]([��� �����],[��� ������])
values
  (1,1),
  (1,2),
  (1,3)

  


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
  