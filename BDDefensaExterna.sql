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
idRolPer int not null identity primary key,
Login varchar(30) not null Foreign key references Personal(Login),
idRol int not null Foreign key references Roles(idRol),
Descripcion varchar(100) not null
)
go
Create table ChoferDerLin(
idChoDer int not null identity primary key,
Login varchar(30) not null Foreign key references Personal(Login),
idDerechoLinea int not null Foreign key references DerechoLinea(idDerechoLinea),
Descripcion varchar(100) not null
)
go
Create table DetalleVenta(
idDetVen int not null identity primary key,
idVenta int not null Foreign key references Venta(idVenta),
idRepuesto int not null Foreign key references Repuesto(idRepuesto),
Cantidad int not null
)
go 
Create table DetalleDevolucion(
idDetDev int not null identity primary key,
idDevolucion int not null Foreign key references Devolucion(idDevolucion),
idRepuesto int not null Foreign key references Repuesto(idRepuesto),
Cantidad int not null
)
go
Create table DetallePrestamo(
idDetPre int not null identity primary key,
idPrestamo int not null Foreign key references Prestamo(idPrestamo),
idRepuesto int not null Foreign key references Repuesto(idRepuesto),
Cantidad int not null,
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

Insert into Roles values('Admin','El administrador de la aplicacion')
Insert into Roles values('Encargado de Almacen','El encargado de prestamos, devoluciones e inventario')
Insert into Roles values('Encargado de Venta','El encargado de ventas')
Insert into Roles values('Dueño','Dueño de un Derecho de Linea')
Insert into Roles values('Chofer','Chofer de un interno')

Insert into Personal  values ('Admin','e3afed0047b08059d0fada10f400c1e5','Jose Solanor Romero',8177406,70200974,'jsolanor1994@gmail.com')
Insert into Personal  values ('EncAlm','d87355fb99b2cc48620ca7a70ccd7e3b','Diego Sanchez Lora',8985406,70621474,'diegosanchez@gmail.com')
Insert into Personal  values ('EncVen','3f71d506873306329d36292a4e15f37a','Maria Rocha Siles',8176321,70266448,'mariarocha1994@gmail.com')

Insert into Personal  values ('Dueño1','c854ad0f75b0996e6e93641824c2fd18','Juan Perez Perales',8665406,64855201,'juanperez@gmail.com')
Insert into Personal  values ('Dueño2','6f315f9ccce0febf7c718e6c854bde4d','Ricado Roca Mamani',8955408,62155478,'ricardoroca@gmail.com')

Insert into Personal  values ('Chofer1','df042aeb66c6d7b21b29a8b5cda9c86e','Rene Cuellar Melgar',6511248,72466521,'renecuellar@gmail.com')
Insert into Personal  values ('Chofer2','8322400c60d3951b44f5c48b253dab37','Pedro Sanchez Roman',3544128,79566485,'pedrosanchez@gmail.com')
Insert into Personal  values ('Chofer3','73f415b50a8bdb312bc152ce399becd6','Rodrigo Perez Hurtado',3288456,76599221,'rodrigoperez@gmail.com')
Insert into Personal  values ('Chofer4','4e0f5fb1ca7b5f8cf4c2b564508721d8','Daniel Medina Romero',9766410,73655223,'danielmedina@gmail.com')

Insert into RolesPersonal values('Admin',1,'Tiene acceso a todo')
Insert into RolesPersonal values('EncAlm',2,'Permite Prestamos, Devoluciones e Inventario')
Insert into RolesPersonal values('EncVen',3,'Permite Ventas')
Insert into RolesPersonal values('Dueño1',4,'Permite Reportes')
Insert into RolesPersonal values('Dueño2',4,'Permite Reportes')
Insert into RolesPersonal values('Chofer1',5,'Nada')
Insert into RolesPersonal values('Chofer2',5,'Nada')
Insert into RolesPersonal values('Chofer3',5,'Nada')
Insert into RolesPersonal values('Chofer4',5,'Nada')

Insert into DerechoLinea values ('Permiso para usar un interno','Habilitado','Admin')
Insert into DerechoLinea values ('Permiso para usar un interno','Habilitado','Admin')
Insert into DerechoLinea values ('Permiso para usar un interno','Habilitado','Dueño1')
Insert into DerechoLinea values ('Permiso para usar un interno','Habilitado','Dueño2') 

Insert into Interno values('4s85wq621xdf7wqs8','4568weq','Coaster',1)
Insert into Interno values('sdawa65456wesf878','3254fvs','Mitsubishi',2)
Insert into Interno values('gdfre545857reqsgg','3254gds','Mitsubishi',3)
Insert into Interno values('5642123rwsf4wafer','3241abs','Coaster',4)

Insert into TipoRepuesto values ('Motor','Repuestos del Motor')
Insert into TipoRepuesto values ('Freno','Repuestos de los Frenos')
Insert into TipoRepuesto values ('Direccion','Repuestos de la Direccion')
Insert into TipoRepuesto values ('Electrico','Repuestos del Sistema Electrico')
Insert into TipoRepuesto values ('Suspension','Repuestos de la Suspension')

insert into Repuesto values ('Pistones',30,50,1)
insert into Repuesto values ('Juntas',30,60,1)

insert into Repuesto values ('Pastillas',50,35,2)
insert into Repuesto values ('Discos',30,20,2)

insert into Repuesto values ('Bomba',20,80,3)
insert into Repuesto values ('Cremallera',50,20,3)

insert into Repuesto values ('Chicote Electrico',70,40,4)
insert into Repuesto values ('Foco',40,60,4)

insert into Repuesto values ('Amortiguadores',30,50,5)
insert into Repuesto values ('Resortes',50,40,5)

insert into ChoferDerLin values('Chofer1',1,'Chofer del interno por Derecho de linea')
insert into ChoferDerLin values('Chofer1',2,'Chofer del interno por Derecho de linea')
insert into ChoferDerLin values('Chofer2',2,'Chofer del interno por Derecho de linea')
insert into ChoferDerLin values('Chofer3',2,'Chofer del interno por Derecho de linea')
insert into ChoferDerLin values('Chofer4',4,'Chofer del interno por Derecho de linea')
insert into ChoferDerLin values('Chofer3',3,'Chofer del interno por Derecho de linea')
insert into ChoferDerLin values('Chofer2',3,'Chofer del interno por Derecho de linea')
----------------------Triggers-----------------------------------
Create trigger tg_Inventario_Repuesto on DetallePrestamo for insert
as
begin
declare @CantidadActual as int
declare @CantidadOp as int
declare @CantidadTotal as int
declare @ID as int
set @ID = (select idRepuesto from inserted)
set @CantidadActual = (select Cantidad from Repuesto where idRepuesto = @ID)
set @CantidadOp = (select Cantidad from inserted)
set @CantidadTotal = @CantidadActual-@CantidadOp

		
update Repuesto set Cantidad = @CantidadTotal where idRepuesto = @ID

end
Create trigger tg_Inventario_Repuesto_up on DetallePrestamo for update
as
begin
declare @CantidadPAct as int
declare @CantidadDPAct as int
declare @CantidadMod as int
declare @CantidadRes as int
declare @CantidadFinal as int
declare @IDI as int
declare @IDD as int

set @IDI = (select idRepuesto from inserted)

set @CantidadPAct = (select Cantidad from Repuesto where idRepuesto = @IDI)
set @CantidadDPAct = (select Cantidad from deleted)
set @CantidadMod = (select Cantidad from inserted)
set @CantidadRes = @CantidadDPAct-@CantidadMod
set @CantidadFinal = @CantidadPAct+@CantidadRes

		
update Repuesto set Cantidad = @CantidadFinal where idRepuesto = @IDI

end
Create trigger tg_Inventario_Repuesto_Del on DetallePrestamo for delete
as
begin
declare @CantidadActual as int
declare @CantidadOp as int
declare @CantidadTotal as int
declare @ID as int
set @ID = (select idRepuesto from deleted)
set @CantidadActual = (select Cantidad from Repuesto where idRepuesto = @ID)
set @CantidadOp = (select Cantidad from deleted)
set @CantidadTotal = @CantidadActual+@CantidadOp

		
update Repuesto set Cantidad = @CantidadTotal where idRepuesto = @ID

end
-------------------------Bitacora Triggers-------------------------------------------------
create trigger tg_alta_DetPrestamo on DetallePrestamo for insert
as
declare @CodRep as int
declare @CodPre as int
set @CodRep = (select idRepuesto from inserted)
set @CodPre = (select idPrestamo from inserted)
insert into Bitacora values('Se guardo Detalle del Prestamo con id : '+convert(char(10),@CodPre)+', con id del Repuesto: '+convert(char(10),@CodRep),GETDATE(),HOST_NAME(),SYSTEM_USER,APP_NAME())
go
create trigger tg_baja_DetPrestamo on DetallePrestamo for delete
as
declare @CodRep as int
declare @CodPre as int
set @CodRep = (select idRepuesto from deleted)
set @CodPre = (select idPrestamo from deleted)
insert into Bitacora values('Se elimino Detalle de del Prestamo con id : '+convert(char(10),@CodPre)+', con id del Repuesto: '+convert(char(10),@CodRep),GETDATE(),HOST_NAME(),SYSTEM_USER,APP_NAME())
go
create trigger tg_Mod_DetPrestamo on DetallePrestamo for update
as
declare @CodRep as int
declare @CodPre as int
set @CodRep = (select idRepuesto from deleted)
set @CodPre = (select idPrestamo from deleted)
insert into Bitacora values('Se modifico Detalle de del Prestamo con id : '+convert(char(10),@CodPre)+', con id del Repuesto: '+convert(char(10),@CodRep),GETDATE(),HOST_NAME(),SYSTEM_USER,APP_NAME())
--------------------------------------------------------------------------------------------------------------------------------------------------------------------
create trigger tg_alta_Prestamo on Prestamo for insert
as
declare @CodPre as int
set @CodPre = (select idPrestamo from inserted)
insert into Bitacora values('Se guardo Prestamo con id : '+convert(char(10),@CodPre),GETDATE(),HOST_NAME(),SYSTEM_USER,APP_NAME())
go
create trigger tg_baja_Prestamo on Prestamo for delete
as
declare @CodPre as int
set @CodPre = (select idPrestamo from deleted)
insert into Bitacora values('Se elimino el Prestamo con id : '+convert(char(10),@CodPre),GETDATE(),HOST_NAME(),SYSTEM_USER,APP_NAME())
go
create trigger tg_Mod_Prestamo on Prestamo for update
as
declare @CodPre as int
set @CodPre = (select idPrestamo from deleted)
insert into Bitacora values('Se modifico el Prestamo con id : '+convert(char(10),@CodPre),GETDATE(),HOST_NAME(),SYSTEM_USER,APP_NAME())
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

create trigger tg_alta_Repuesto on Repuesto for insert
as
declare @CodRep as int
set @CodRep = (select idRepuesto from inserted)
insert into Bitacora values('Se guardo Repuesto con id: '+convert(char(10),@CodRep),GETDATE(),HOST_NAME(),SYSTEM_USER,APP_NAME())
go
create trigger tg_baja_Repuesto on Repuesto for delete
as
declare @CodRep as int
set @CodRep = (select idRepuesto from deleted)
insert into Bitacora values('Se elimino Repuesto con id: '+convert(char(10),@CodRep),GETDATE(),HOST_NAME(),SYSTEM_USER,APP_NAME())
go
create trigger tg_Mod_Repuesto on Repuesto for update
as
declare @CodRep as int
set @CodRep = (select idRepuesto from deleted)
insert into Bitacora values('Se modifico Repuesto con id: '+convert(char(10),@CodRep),GETDATE(),HOST_NAME(),SYSTEM_USER,APP_NAME())
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

create trigger tg_alta_TipoRepuesto on TipoRepuesto for insert
as
declare @CodRep as int
set @CodRep = (select idTipoRepuesto from inserted)
insert into Bitacora values('Se guardo TipoRepuesto con id : '+convert(char(10),@CodRep),GETDATE(),HOST_NAME(),SYSTEM_USER,APP_NAME())
go
create trigger tg_baja_TipoRepuesto on TipoRepuesto for delete
as
declare @CodRep as int
set @CodRep = (select idTipoRepuesto from deleted)
insert into Bitacora values('Se elimino TipoRepuesto con id : '+convert(char(10),@CodRep),GETDATE(),HOST_NAME(),SYSTEM_USER,APP_NAME())
go
create trigger tg_Mod_TipoRepuesto on TipoRepuesto for update
as
declare @CodRep as int
set @CodRep = (select idTipoRepuesto from deleted)
insert into Bitacora values('Se modifico TipoRepuesto con id : '+convert(char(10),@CodRep),GETDATE(),HOST_NAME(),SYSTEM_USER,APP_NAME())
---------------------------------Comandos----------------------------------------------------
select * from Repuesto
select * from DetallePrestamo
select * from Prestamo
select * from RolesPersonal
select * from Personal
select * from Bitacora

--------------------------------------View Reporte 1---------------------------------------------------------
Create View View_Report1
as
select tr.Nombre, SUM(dp.Cantidad)as CantidadRepuesto 
from DetallePrestamo dp 
		inner join Repuesto r on dp.idRepuesto = r.idRepuesto
		inner join TipoRepuesto tr on r.idTipoRepuesto = tr.idTipoRepuesto
		inner join Prestamo p on p.idPrestamo = dp.idPrestamo
		Group by tr.Nombre
------------------------------------------Prueba Reporte 1----------------------------------
select tr.Nombre, SUM(dp.Cantidad) as CantidadRepuesto 
from DetallePrestamo dp 
		inner join Repuesto r on dp.idRepuesto = r.idRepuesto
		inner join TipoRepuesto tr on r.idTipoRepuesto = tr.idTipoRepuesto
		inner join Prestamo p on p.idPrestamo = dp.idPrestamo
		Where p.fechaInicio between '20/01/2010' And '28/05/2018'
		Group by tr.Nombre
				
select * from DetallePrestamo dp 
		inner join Repuesto r on dp.idRepuesto = r.idRepuesto
		inner join TipoRepuesto tr on r.idTipoRepuesto = tr.idTipoRepuesto
		inner join Prestamo p on p.idPrestamo = dp.idPrestamo
		
select * from View_Report1			
--------------------------------------------------------------View Reporte 2--------------------------------------------
Create View View_Report2
as
Select CONCAT(nombreCompleto,' - Interno: ', pr.idDerechoLinea) as ChoferInterno, pr.idPrestamo,pr.Descripcion,pr.fechaLimite from Prestamo pr
		inner join Personal p on p.Login = pr.idChofer
		where pr.fechaLimite >= getdate()
--------------------------------------------------------------View Reporte 3----------------------------------------------
Create View View_Report3
as
select tr.Nombre, SUM(dp.Cantidad) as CantidadRepuesto, SUM(r.Precio) as MontoTotal
from DetallePrestamo dp 
		inner join Repuesto r on dp.idRepuesto = r.idRepuesto
		inner join TipoRepuesto tr on r.idTipoRepuesto = tr.idTipoRepuesto
		inner join Prestamo p on p.idPrestamo = dp.idPrestamo
		Group by tr.Nombre
----------------------------------------------------Prueba Reporte 3--------------------------------------------------------
select tr.Nombre, SUM(dp.Cantidad) as CantidadRepuesto, SUM(r.Precio) as MontoTotal
from DetallePrestamo dp 
		inner join Repuesto r on dp.idRepuesto = r.idRepuesto
		inner join TipoRepuesto tr on r.idTipoRepuesto = tr.idTipoRepuesto
		inner join Prestamo p on p.idPrestamo = dp.idPrestamo
		Where p.fechaInicio between '20/01/2010' And '28/05/2018'
		Group by tr.Nombre
--------------------------------------------------------------View Reporte 4---------------------------------------------
Create View View_Report4
as
select Sum(Cantidad) as Stock, tr.idTipoRepuesto, tr.Nombre from Repuesto r 
		inner join TipoRepuesto tr on tr.idTipoRepuesto = r.idTipoRepuesto
		group by tr.idTipoRepuesto, tr.Nombre

		order by Stock DESC
-------------------------------------------------------------------------------------------------Tareas Programadas----------------------------------------------------------------

create procedure SPBackupTotal
as
	begin	
		declare @Fec date
		declare @nom varchar(50)	
		set @Fec = SYSDATETIME()
		set @nom = 'C:\FINAL\CopyBDDEFEXTtotal - '+(CONVERT(varchar(30),@Fec))+'.bak' 
		backup database BDDEFEXT
		to Disk = @nom
		with CHECKSUM
end
create procedure SPBackupDiferencial
as
	begin	
		declare @Fec date
		declare @nom varchar(50)	
		set @Fec = SYSDATETIME()
		set @nom = 'C:\FINAL\CopyBDDEFEXTdif - '+(CONVERT(varchar(30),@Fec))+'.bak'
		backup database BDDEFEXT
		to Disk = @nom
		with DIFFERENTIAL
end
------------------------------------------------------------------------------------------------------------------------------------------------