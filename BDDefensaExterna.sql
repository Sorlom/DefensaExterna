Create Database BDDEFEXT
go
use BDDEFEXT
go
-------------------------------------------------Tablas---------------------------------------------
Create table Personal(
Login varchar(30) not null primary key ,
Password varchar(Max) not null,
nombreCompleto varchar(50) not null,
CI int not null,
Celular int not null,
Email varchar(50) not null
)
go
Create table Roles(
idRol int not null identity primary key,
Nombre varchar (50) not null,
Descripcion varchar (100) not null
)
go
Create table TipoRepuesto(
idTipoRepuesto int not null identity primary key,
Nombre varchar(50) not null,
Descripcion varchar(100) not null
)
go
Create table Repuesto(
idRepuesto int not null identity primary key,
Nombre varchar(50) not null,
Cantidad int not null,
Precio decimal(10,2) not null,
idTipoRepuesto int not null Foreign key references TipoRepuesto(idTipoRepuesto) 
)
go
Create table DerechoLinea(
idDerechoLinea int not null identity primary key,
Descripcion varchar(100) not null,
Estado varchar(50) not null,
idDueño varchar(30) not null Foreign key references Personal(Login)
)
go
Create table Interno(
idInterno int not null identity primary key,
Chasis varchar(50) not null,
Placa varchar(50) not null,
Modelo varchar(50) not null,
idDerechoLinea int not null Foreign key references DerechoLinea(idDerechoLinea)
)
go
Create table Venta(
idVenta int not null identity primary key,
Descripcion varchar(100) not null,
fechaInicio datetime not null,
fechaLimite datetime not null,
Monto decimal(10,2) not null,
idDerechoLinea int not null Foreign key references DerechoLinea(idDerechoLinea),
idChofer varchar(30) not null Foreign key references Personal(Login),
idEncargado varchar(30) not null Foreign key references Personal(Login)
)
go
Create table Prestamo(
idPrestamo int not null identity primary key,
Descripcion varchar(100) not null,
fechaInicio datetime not null,
fechaLimite datetime not null,
idDerechoLinea int not null Foreign key references DerechoLinea(idDerechoLinea),
idChofer varchar(30) not null Foreign key references Personal(Login),
idEncargado varchar(30) not null Foreign key references Personal(Login)
)
go
Create table Devolucion(
idDevolucion int not null identity primary key,
Descripcion varchar(100) not null,
Fecha datetime not null,
idPrestamo int not null Foreign key references Prestamo(idPrestamo)
)
go
Create table RolesPersonal(
Login varchar(30) not null Foreign key references Personal(Login),
idRol int not null Foreign key references Roles(idRol)
)
go
Create table ChoferDerLin(
Login varchar(30) not null Foreign key references Personal(Login),
idDerechoLinea int not null Foreign key references DerechoLinea(idDerechoLinea)
)
go
Create table DetalleVenta(
idVenta int not null Foreign key references Venta(idVenta),
idRepuesto int not null Foreign key references Repuesto(idRepuesto)
)
go 
Create table DetalleDevolucion(
idDevolucion int not null Foreign key references Devolucion(idDevolucion),
idRepuesto int not null Foreign key references Repuesto(idRepuesto)
)
go
Create table DetallePrestamo(
idPrestamo int not null Foreign key references Prestamo(idPrestamo),
idRepuesto int not null Foreign key references Repuesto(idRepuesto),
Estado varchar(50) not null
)
go
Create table Bitacora(
idBit int not null identity primary key, 
Descripcion varchar(300) not null,
Fecha datetime not null,
Terminal varchar(100) not null,
Usuario varchar(100) not null,
Aplicacion varchar(100) not null
)
----------------------------------------------------------------------------------------------------
use master
go
drop database BDDEFEXT
---------------------------------------------Datos--------------------------------------------------

Insert into Personal values()