/* +++++++ RECONSTRUIR BASE DE DATOS CON LOS CAMBIOS RESPECTIVOS ++++++++++

use master;
drop database "SOGIP_v3"

create database "SOGIP_v3"
use "SOGIP_v3"


 +++++++ RECONSTRUIR BASE DE DATOS CON LOS CAMBIOS RESPECTIVOS ++++++++++

 ++++++++++++++++++++++++++++ Select's ++++++++++++++++++++++++++++

 Rol = Selección/Federación		|	 User = 116630668	|	 Password = JuGu1166121996	|	Id = 0b925665-1c93-42d2-80b0-29c6423daf66
 Rol = Entrenador				|	 User = 112530907	|	 Password = EmCh1125081985	|	Id = 43002ea0-b75c-44a0-8f29-7d5c0eeffed2
 Rol = Atleta					|	 User = 208770327	|	 Password = YuMo2087042006	|	Id = 516c294e-14c2-4bfd-887a-59e763956a54
 Rol = Atleta Becado			|	 User = 118860905	|	 Password = FrMo1188032003	|	Id = 1dd72422-a344-407d-aec6-783907e73164
 Rol = Funcionario ICODER		|	 User = 206140354	|	 Password = FeBa2061111956	|	Id = 843f21d4-b301-4847-848b-f4cd20827a6a
 Rol = Funcionario ICODER		|	 User = 205940271	|	 Password = JuAr2059051984	|	Id = c6d47fdc-c219-4d7f-b613-0aca0c812a29
 Rol = Entidades Públicas		|	 User = 110530938	|	 Password =	AlNa1105111979	|	Id = 31788f50-c82b-4a6a-9cf5-1a5a4d721e2b
 Rol = Asociación/Comité		|	 User = 123456789	|	 Password = KiDí1234081997	|	Id = 0aed3613-00da-4867-b2b5-9a46569590bb
 Rol = Usuario Externo			|	 User = 			|	 Password =					|	Id = 

 select SOGIP_Users.Id, Cedula, Nombre1, Nombre2, Apellido1, Fecha_Nacimiento, SOGIP_Roles.Name, SOGIP_Roles.Id
 from 
 SOGIP_Users, SOGIP_UserRoles, SOGIP_Roles
 where 
 SOGIP_Users.Id = SOGIP_UserRoles.Userid
 and
 SOGIP_UserRoles.Roleid = SOGIP_Roles.id
 order by SOGIP_Roles.Id;

 select * from SOGIP_Archivo;
 select * from SOGIP_Asociacion_Deportiva;
 select * from SOGIP_Atletas;
select * from sogip_users where id='b76a265d-2b9d-4aac-963f-0cc50595b639'
 select * from SOGIP_Atletas;
 select * from SOGIP_Categorias;
 select * from SOGIP_Color;
 select * from SOGIP_Cita;
 select * from SOGIP_Conjunto_Ejercicio;
 select * from SOGIP_Deportes;
 select * from SOGIP_Entidad_Publica;
 select * from SOGIP_Estados;
 select * from SOGIP_EstadosPagos order by FechaPago;
 select * from SOGIP_Expedientes_Fisicos;
 select * from SOGIP_Funcionario_ICODER as f, sogip_users as u where f.usuario_id=u.id and u.Sexo=0;
 select * from SOGIP_Horario;
 select * from SOGIP_Parametro;
 select * from SOGIP_Roles order by Id asc;
 select * from SOGIP_Rutina;
 select * from SOGIP_Selecciones;
 select * from SOGIP_SubSeleccion;
 select * from SOGIP_Tipo order by TipoId;
 select * from SOGIP_Tipo_Deporte;
 select * from SOGIP_Tipo_Entidad;
 select * from SOGIP_UserClaims;
 select * from SOGIP_UserLogins;
 select * from SOGIP_UserRoles
		where RoleId='4';


304270289
HuLo3042041988


 select * from SOGIP_Users where Id='0c3fe9ea-2e80-4d7b-b8d2-1158f6c1b824' order by Cedula;
 select * from sogip_atletas where atletaid > 11;
 select u.Id, u.Nombre1, u.Nombre2, u.Apellido1, rol.name from SOGIP_Users as u, sogip_userRoles as r, sogip_roles as rol where u.Id=r.Userid and r.roleid=rol.id and (r.roleid='5' or r.roleid='6');

 select u.Id, u.Nombre1, u.Nombre2, u.Apellido1, rol.name
 from SOGIP_Users as u, sogip_userRoles as r, sogip_roles as rol
 where u.Id = r.Userid
 and r.roleid = rol.id
 and (r.roleid='5' or r.roleid='6');


 1: 914db4cb-8e02-4476-9e42-31befefd7a0e
 2: de8b4ac7-40a3-4b23-aa2b-28ae9fcb9253
 3: 137feecf-2c48-4e86-8b79-5906b0057c70



 4: 9d9d279f-016a-47e9-bf70-9ed4a4754de5 // Desconocido
 5: 7440f3c4-3528-4606-9bc8-501ad8f15b51
 6: 377e1527-58bb-40dc-a873-f88d4a1c1fcf
 7: 2f3a289f-6370-4524-a21a-c72d5f5699e4 // Desconocido

 ++++++++++++++++++++++++++++ Select's ++++++++++++++++++++++++++++

 ++++++++++++++++++++++++++++ Delete's ++++++++++++++++++++++++++++

 delete from SOGIP_Archivo;
 delete from SOGIP_Asociacion_Deportiva;
 delete from SOGIP_Atletas where AtletaId =14;
 delete from SOGIP_Categorias;
 delete from SOGIP_Cita;
 delete from SOGIP_Color;
 delete from SOGIP_Conjunto_Ejercicio;
 delete from SOGIP_Deportes;
 delete from SOGIP_Entidad_Publica;
 delete from SOGIP_Estados;
 delete from SOGIP_Expedientes_Fisicos;
 delete from SOGIP_Funcionario_ICODER;
 delete from SOGIP_Horario;
 delete from SOGIP_Parametro;
 delete from SOGIP_Roles;
 delete from SOGIP_Rutina;
 delete from SOGIP_Selecciones;
 delete from SOGIP_SubSeleccion;
 delete from SOGIP_Tipo;
 delete from SOGIP_Tipo_Deporte;
 delete from SOGIP_Tipo_Entidad;
 delete from SOGIP_UserClaims;
 delete from SOGIP_UserLogins;
 delete from SOGIP_UserRoles;
 delete from SOGIP_Users;

 ++++++++++++++++++++++++++++ Delete's ++++++++++++++++++++++++++++

 ++++++++++++++++++++++++++ Trigger's ++++++++++++++++++++++++++

create trigger fecha_expiracion on SOGIP_Users
 for update, insert
  as
   if update(PasswordHash)
    begin
     update SOGIP_Users
     set fecha_expiracion=SOGIP_Users.fecha_expiracion+90
     from inserted
     where SOGIP_Users.id = inserted.id
    end

 ++++++++++++++++++++++++++ Trigger's ++++++++++++++++++++++++++

 ++++++++++++++++++++++++++++ Insert's ++++++++++++++++++++++++++++

use master;
drop database "SOGIP_v3"

create database "SOGIP_v3"
use "SOGIP_v3"

*/

DECLARE @sql varchar(400);
DECLARE @VARIABLEACAMBIAR varchar(100);
set @VARIABLEACAMBIAR = 'C:\Users\402360192\Documents\GitHub\SOGIP_v2\script_BD';

set @sql = 'BULK INSERT SOGIP_Roles FROM '''+@VARIABLEACAMBIAR+'\SOGIP_Roles.csv'' WITH(datafiletype=''widechar'', codepage = ''ACP'', fieldterminator = '';'', rowterminator = ''\n'');';
exec (@sql)
print N'SOGIP_Roles rows 11';

set @sql = 'BULK INSERT SOGIP_Estados FROM '''+@VARIABLEACAMBIAR+'\SOGIP_Estados.csv'' WITH(datafiletype=''widechar'', codepage = ''ACP'', fieldterminator = '';'', rowterminator = ''\n'');';
exec (@sql)
print N'SOGIP_Estados rows 10';

set @sql = 'BULK INSERT SOGIP_Categorias FROM '''+@VARIABLEACAMBIAR+'\SOGIP_Categorias.csv'' WITH(datafiletype=''widechar'', codepage = ''ACP'', fieldterminator = '';'', rowterminator = ''\n'');';
exec (@sql)
print N'SOGIP_Categorias rows 06';

set @sql = 'BULK INSERT SOGIP_Tipo_Deporte FROM '''+@VARIABLEACAMBIAR+'\SOGIP_Tipo_Deporte.csv'' WITH(datafiletype=''widechar'', codepage = ''ACP'', fieldterminator = '';'', rowterminator = ''\n'');';
exec (@sql)
print N'SOGIP_Tipo_Deporte rows 06';

set @sql = 'BULK INSERT SOGIP_Deportes FROM '''+@VARIABLEACAMBIAR+'\SOGIP_Deportes.csv'' WITH(datafiletype=''widechar'', codepage = ''ACP'', fieldterminator = '';'', rowterminator = ''\n'');';
exec (@sql)
print N'SOGIP_Deportes rows 06';

set @sql = 'BULK INSERT SOGIP_Tipo_Entidad FROM '''+@VARIABLEACAMBIAR+'\SOGIP_Tipo_Entidad.csv'' WITH(datafiletype=''widechar'', codepage = ''ACP'', fieldterminator = '';'', rowterminator = ''\n'');';
exec (@sql)
print N'SOGIP_Tipo_Entidad rows 77';

/*********** HASTA ACÁ OCUPAMOS TODO. *******************/
/*********** OCUPAMOS SUPERVISOR Y JOSAFAT. *******************/

set @sql = 'BULK INSERT SOGIP_Users FROM '''+@VARIABLEACAMBIAR+'\SOGIP_Users.csv'' WITH(datafiletype=''widechar'', codepage = ''ACP'', fieldterminator = '';'', rowterminator = ''\n'');';
exec (@sql)
print N'SOGIP_Users rows 45 rows';

set @sql = 'BULK INSERT SOGIP_UserRoles FROM '''+@VARIABLEACAMBIAR+'\SOGIP_UserRoles.csv'' WITH(datafiletype=''widechar'', codepage = ''ACP'', fieldterminator = '';'', rowterminator = ''\n'');';
exec (@sql)
print N'SOGIP_UserRoles rows 45 rows';

set @sql = 'BULK INSERT SOGIP_Funcionario_ICODER FROM '''+@VARIABLEACAMBIAR+'\SOGIP_Funcionario_ICODER.csv'' WITH(datafiletype=''widechar'', codepage = ''ACP'', fieldterminator = '';'', rowterminator = ''\n'');';
exec (@sql)
print N'SOGIP_Funcionario_ICODER rows 11 rows';

set @sql = 'BULK INSERT SOGIP_Asociacion_Deportiva FROM '''+@VARIABLEACAMBIAR+'\SOGIP_Asociacion.csv'' WITH(datafiletype=''widechar'', codepage = ''ACP'', fieldterminator = '';'', rowterminator = ''\n'');';
exec (@sql)
print N'SOGIP_Asociacion_Deportiva 01 rows';

set @sql = 'BULK INSERT SOGIP_Selecciones FROM '''+@VARIABLEACAMBIAR+'\SOGIP_Selecciones.csv'' WITH(datafiletype=''char'', codepage = ''ACP'', fieldterminator = '';'', rowterminator = ''\n'');';
exec (@sql)
print N'SOGIP_Selecciones 10 rows';

set @sql = 'BULK INSERT SOGIP_SubSeleccion FROM '''+@VARIABLEACAMBIAR+'\SOGIP_SubSeleccion.csv'' WITH(firstrow = 2, datafiletype=''char'', fieldterminator = '';'', rowterminator = ''\n'');';
exec (@sql)
print N'SOGIP_SubSeleccion 11 rows';

set @sql = 'BULK INSERT SOGIP_Atletas FROM '''+@VARIABLEACAMBIAR+'\SOGIP_Atletas.csv'' WITH(codepage = ''ACP'', datafiletype =''widechar'', fieldterminator = '';'', rowterminator = ''\n'');';
exec (@sql)
print N'SOGIP_Atletas 11 rows';

set @sql = 'BULK INSERT SOGIP_Entidad_Publica FROM '''+@VARIABLEACAMBIAR+'\SOGIP_Entidades.csv'' WITH(codepage = ''ACP'', datafiletype =''char'', fieldterminator = '';'', rowterminator = ''\n'');';
exec (@sql)
print N'SOGIP_Entidad_Publica 01 rows';

set @sql = 'BULK INSERT SOGIP_Rutina FROM '''+@VARIABLEACAMBIAR+'\SOGIP_Rutina.csv'' WITH(datafiletype=''widechar'', codepage = ''ACP'', fieldterminator = '';'', rowterminator = ''\n'');';
exec (@sql)
print N'SOGIP_Rutina 06 rows';

--set @sql = 'BULK INSERT SOGIP_Conjunto_Ejercicio FROM '''+@VARIABLEACAMBIAR+'\SOGIP_Conjunto_Ejercicio.csv'' WITH(firstrow = 2, datafiletype=''widechar'',  fieldterminator = '';'', rowterminator = ''\n'');';
--exec (@sql)
print N'SOGIP_Conjunto_Ejercicio 33 rows';
-- select * from sogip_conjunto_ejercicio

set @sql = 'BULK INSERT SOGIP_Cita FROM '''+@VARIABLEACAMBIAR+'\SOGIP_Cita.csv'' WITH(firstrow=2,datafiletype=''widechar'',  fieldterminator = '';'', rowterminator = ''\n'');';
exec (@sql)
print N'SOGIP_Cita 17 rows';

/*********** OCUPAMOS DE ACÁ. *******************/
set @sql = 'BULK INSERT SOGIP_Tipo FROM '''+@VARIABLEACAMBIAR+'\SOGIP_Tipo.csv'' WITH(datafiletype=''char'', codepage = ''ACP'', fieldterminator = '';'', rowterminator = ''\n'');';
exec (@sql)
print N'SOGIP_Tipo 06 rows';

set @sql = 'BULK INSERT SOGIP_Color FROM '''+@VARIABLEACAMBIAR+'\SOGIP_Color.csv'' WITH(datafiletype=''char'', codepage = ''ACP'', fieldterminator = '';'', rowterminator = ''\n'');';
exec (@sql)
print N'SOGIP_Color 07 rows';
/*********** A ACÁ. *******************/

--set @sql = 'BULK INSERT SOGIP_Ejercicio FROM '''+@VARIABLEACAMBIAR+'\SOGIP_Ejercicio.csv'' WITH(datafiletype=''widechar'', codepage = ''ACP'', fieldterminator = '';'', rowterminator = ''\n'');';
--exec (@sql)
print N'SOGIP_Ejercicio 06 rows';

--set @sql = 'BULK INSERT SOGIP_Maquina FROM '''+@VARIABLEACAMBIAR+'\SOGIP_Maquina.csv'' WITH(datafiletype=''widechar'', codepage = ''ACP'', fieldterminator = '';'', rowterminator = ''\n'');';
--exec (@sql)
print N'SOGIP_Maquina 06 rows';

set @sql = 'BULK INSERT SOGIP_MaquinaEjercicio FROM '''+@VARIABLEACAMBIAR+'\SOGIP_MaquinaEjercicio.csv'' WITH(datafiletype=''widechar'', codepage = ''ACP'', fieldterminator = '';'', rowterminator = ''\n'');';
exec (@sql)
print N'SOGIP_MaquinaEjercicio 02 rows';

/*********** ESTE. *******************/
set @sql = 'BULK INSERT SOGIP_TipoPago FROM '''+@VARIABLEACAMBIAR+'\SOGIP_TipoPago.csv'' WITH(datafiletype=''widechar'', codepage = ''ACP'', fieldterminator = '';'', rowterminator = ''\n'');';
exec (@sql)
print N'SOGIP_TipoPago 06 rows';
/*********** ESTE. *******************/

set @sql = 'BULK INSERT SOGIP_EstadosPagos FROM '''+@VARIABLEACAMBIAR+'\SOGIP_EstadosPagos.csv'' WITH(datafiletype=''widechar'', codepage = ''ACP'', fieldterminator = '';'', rowterminator = ''\n'');';
exec (@sql)
print N'SOGIP_EstadosPagos 02 rows';

/*********** ESTE. *******************/
set @sql='insert into sogip_archivo('+
	'Nombre, Contenido, actividad_Id, Tipo_TipoId, Usuario_Id)'+
'select'+
	'''Ingreso_Masivo_Original.xlsx'', Contenido.*, NULL, 6, ''8f9c47bf-edbd-40bf-9b5e-f753dd81a766'''+
'from openrowset '+
	'(bulk '''+@VARIABLEACAMBIAR+'\Ingreso_Masivo_Original.xlsx'', SINGLE_BLOB) Contenido;';
exec (@sql)
/*********** ESTE. *******************/

--UPDATE SOGIP_EstadosPagos SET usuario_id='29e4a004-4c40-4a06-bf2f-2ee61aa8143c' where id=5

/*
select * from sogip_horario;
select * from sogip_actividad;
*/
insert into sogip_actividad values('GRAN CARRERA','LARGA, GRANDE', 'CURRIDABAT');
insert into sogip_actividad values('TRIATLÓN','PARA LSO MEJORES', 'GUANACASTE');
insert into sogip_actividad values('RELEASE','NOT SURE', 'ICODER');
insert into sogip_actividad values('FINAL 6TRE','NOT MUCH', 'ALREDEDORES');

insert into sogip_horario values(getdate()+3, getdate()+50,1);
insert into sogip_horario values(getdate()-5, getdate()+10,2);
insert into sogip_horario values(getdate(), getdate()+8,3);
insert into sogip_horario values(getdate()-2, getdate()+20,4);

/*
DECLARE @sql varchar(400);
DECLARE @VARIABLEACAMBIAR varchar(100);
*/

set @sql='insert into sogip_archivo('+
	'Nombre, Contenido, actividad_Id, Tipo_TipoId, Usuario_Id)'+
'select'+
	'''A1.jpg'', Contenido.*, 1, 5, null '+
'from openrowset '+
	'(bulk '''+@VARIABLEACAMBIAR+'\A1.jpeg'', SINGLE_BLOB) Contenido;';
exec (@sql)

set @sql='insert into sogip_archivo('+
	'Nombre, Contenido, actividad_Id, Tipo_TipoId, Usuario_Id)'+
'select'+
	'''A2.jpeg'', Contenido.*, 2, 5, null '+
'from openrowset '+
	'(bulk '''+@VARIABLEACAMBIAR+'\A2.jpeg'', SINGLE_BLOB) Contenido;';
exec (@sql)

set @sql='insert into sogip_archivo('+
	'Nombre, Contenido, actividad_Id, Tipo_TipoId, Usuario_Id)'+
'select'+
	'''A3.jpeg'', Contenido.*, 3, 5, null '+
'from openrowset '+
	'(bulk '''+@VARIABLEACAMBIAR+'\A3.jpeg'', SINGLE_BLOB) Contenido;';
exec (@sql)

set @sql = 'BULK INSERT SOGIP_ControlIngreso FROM '''+@VARIABLEACAMBIAR+'\SOGIP_ControlIngreso.csv'' WITH(datafiletype=''widechar'', codepage = ''ACP'', fieldterminator = '';'', rowterminator = ''\n'');';
exec (@sql)
PRINT N'SOGIP_ControlIngreso 04 rows';

-- ++++++++++++++++++++++++++ Insert's ++++++++++++++++++++++++++