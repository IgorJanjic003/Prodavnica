create database prodza
use prodza

create table artikal(
id int identity(1,1) primary key,
id_proizvodjaca int,
naziv nvarchar(50),
cena int,
foreign key (id_proizvodjaca) references proizvodjac(id)
)

create table proizvodjac(
id int identity(1,1) primary key,
naziv nvarchar(50),
adresa nvarchar(50),
email nvarchar(50)
)

create table radnik(
id int identity(1,1) primary key,
ime nvarchar(50),
prezime nvarchar(50),
jmbg nvarchar(15),
email nvarchar(50)
)

create table prodaja(
id int identity(1,1) primary key,
id_artikla int,
kolicina int,
id_radnika int,
foreign key (id_artikla) references artikal(id),
foreign key (id_radnika) references radnik(id)
)


insert into proizvodjac values
('Frikom','Bakionicka 56','frikom@gmail.com'),
('Panini','Zrmanjska 420','panini@gmail.com'),
('Imlek','Zaplanjska 3','imlek@gmail.com'),
('Versace','Narodnih heroja 20','oridjidji@gmail.com'),
('Gucci','Vatromira Petrovica 76','dobromicuci@gmail.com'),
('Rolex','Decanska 8','rol@gmail.com')



insert into artikal values
(1,'Sladoled u kornetu',150),
(1,'Sladoled na stapicu',120),
(1,'Smrznuti grasak',200),
(2,'FIFA slicice',50),
(2,'UEFA CL slicice',70),
(3,'Jogurt',80),
(3,'Pavlaka',40),
(3,'Mleko',100),
(3,'Kiselo mleko',50),
(4,'Parfem',5000),
(4,'Torba',15000),
(4,'Rob',1000000),
(5,'Rob',5000000),
(6,'Rob',100000),
(5,'Torba',20000),
(5,'Sat',123456),
(6,'Sat',123456)


insert into radnik values
('Milos','Rakic', null, null),
('Igor', 'Janjic', null, null)

select * from radnik

select * from artikal

select * from proizvodjac