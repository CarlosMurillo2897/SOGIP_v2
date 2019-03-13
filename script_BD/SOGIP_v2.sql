/* +++++++ RECONSTRUIR BASE DE DATOS CON LOS CAMBIOS RESPECTIVOS ++++++++++

use master;
drop database "SOGIP_v3"

create database "SOGIP_v3"
use "SOGIP_v3"

 +++++++ RECONSTRUIR BASE DE DATOS CON LOS CAMBIOS RESPECTIVOS ++++++++++

 ++++++++++++++++++++++++++++ Select's ++++++++++++++++++++++++++++
<<<<<<< HEAD
=======

>>>>>>> parent of 1daa7ee... Revert "Merge remote-tracking branch 'origin/Merge_Sprint3' into canelones"
 Rol = Selección/Federación		|	 User = 116630668	|	 Password = JuGu1166121996	|	Id =
 Rol = Entrenador				|	 User = 112530907	|	 Password = EmCh1125081985	|	Id =
 Rol = Atleta					|	 User = 208770327	|	 Password = YuMo2087042006	|	Id =
 Rol = Atleta Becado			|	 User = 110830174	|	 Password = AaHi1108021980	|	Id =
 Rol = Funcionario ICODER		|	 User = 206140354	|	 Password = FeBa2061111956	|	Id =
<<<<<<< HEAD
 Rol = Funcionario ICODER		|	 User = 205940271	|	 Password = JuAr2059051984	|	Id =
 Rol = Entidades Públicas		|	 User = 			|	 Password =					|	Id =
 Rol = Asociación/Comité		|	 User = 123456789	|	 Password = KiDí1234081997	|	Id = 0aed3613-00da-4867-b2b5-9a46569590bb
 Rol = Usuario Externo			|	 User = 			|	 Password =					|	Id =
=======
 Rol = Entidades Públicas		|	 User = 			|	 Password =					|	Id =
 Rol = Asociación/Comité		|	 User = 123456789	|	 Password = KiDí1234081997	|	Id = 0aed3613-00da-4867-b2b5-9a46569590bb
 Rol = Usuario Externo			|	 User = 			|	 Password =					|	Id =

>>>>>>> parent of 1daa7ee... Revert "Merge remote-tracking branch 'origin/Merge_Sprint3' into canelones"

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
 select * from SOGIP_Categorias;
 select * from SOGIP_Color;
 select * from SOGIP_Cita;
 select * from SOGIP_Conjunto_Ejercicio;
 select * from SOGIP_Deportes;
 select * from SOGIP_Entidad_Publica;
 select * from SOGIP_Estados;
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
		where RoleId='5';
 select * from SOGIP_Users order by Cedula;
 select * from sogip_atletas where atletaid > 11;
<<<<<<< HEAD
 select u.Id, u.Nombre1, u.Nombre2, u.Apellido1, rol.name from SOGIP_Users as u, sogip_userRoles as r, sogip_roles as rol where u.Id=r.Userid and r.roleid=rol.id and (r.roleid='6' or r.roleid='5');

 1: 914db4cb-8e02-4476-9e42-31befefd7a0e
 2: de8b4ac7-40a3-4b23-aa2b-28ae9fcb9253
 3: 137feecf-2c48-4e86-8b79-5906b0057c70
 4: 9d9d279f-016a-47e9-bf70-9ed4a4754de5
 5: 7440f3c4-3528-4606-9bc8-501ad8f15b51
 6: 377e1527-58bb-40dc-a873-f88d4a1c1fcf
=======

 select u.Id, u.Nombre1, u.Nombre2, u.Apellido1, rol.name from SOGIP_Users as u, sogip_userRoles as r, sogip_roles as rol where u.Id=r.Userid and r.roleid=rol.id and (r.roleid='6' or r.roleid='5');

1: 914db4cb-8e02-4476-9e42-31befefd7a0e
2: de8b4ac7-40a3-4b23-aa2b-28ae9fcb9253
3: 137feecf-2c48-4e86-8b79-5906b0057c70
4: 9d9d279f-016a-47e9-bf70-9ed4a4754de5
5: 7440f3c4-3528-4606-9bc8-501ad8f15b51
6: 377e1527-58bb-40dc-a873-f88d4a1c1fcf
>>>>>>> parent of 1daa7ee... Revert "Merge remote-tracking branch 'origin/Merge_Sprint3' into canelones"

 ++++++++++++++++++++++++++++ Select's ++++++++++++++++++++++++++++

 ++++++++++++++++++++++++++++ Delete's ++++++++++++++++++++++++++++

 delete from SOGIP_Archivo;
 delete from SOGIP_Asociacion_Deportiva;
 delete from SOGIP_Atletas;
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
*/

DECLARE @sql varchar(200);
DECLARE @VARIABLEACAMBIAR varchar(100);

-- *************************** SE DEBE CAMBIAR LA SIGUIENTE VARIABLE*********************************************************
-- *************************** OSEA, @VARIABLEACAMBIAR POR LA DIRECCIÓN *********************************************************
-- *************************** DONDE ESTÉ EL PROYECTO SOGIP_V2 *********************************************************
-- *************************** Y DENTRO DE ESTA CARPETA LA CARPETA SCRIPT_BD *********************************************************
set @VARIABLEACAMBIAR = 'C:\Users\CCM\Documents\GitHub\SOGIP_v2\script_BD'

-- ************************************************************************************
-- ************************************************************************************


set @sql = 'BULK INSERT SOGIP_Roles FROM '''+@VARIABLEACAMBIAR+'\SOGIP_Roles.csv'' WITH(codepage = ''ACP'', fieldterminator = '';'', rowterminator = ''\n'');';
exec (@sql)

set @sql = 'BULK INSERT SOGIP_Estados FROM '''+@VARIABLEACAMBIAR+'\SOGIP_Estados.csv'' WITH(codepage = ''ACP'', fieldterminator = '';'', rowterminator = ''\n'');';
exec (@sql)
 
set @sql = 'BULK INSERT SOGIP_Categorias FROM '''+@VARIABLEACAMBIAR+'\SOGIP_Categorias.csv'' WITH(codepage = ''ACP'', fieldterminator = '';'', rowterminator = ''\n'');';
exec (@sql)

set @sql = 'BULK INSERT SOGIP_Tipo_Deporte FROM '''+@VARIABLEACAMBIAR+'\SOGIP_Tipo_Deporte.csv'' WITH(codepage = ''ACP'', fieldterminator = '';'', rowterminator = ''\n'');';
exec (@sql)

set @sql = 'BULK INSERT SOGIP_Deportes FROM '''+@VARIABLEACAMBIAR+'\SOGIP_Deportes.csv'' WITH(codepage = ''ACP'', fieldterminator = '';'', rowterminator = ''\n'');';
exec (@sql)

-- 77 rows
set @sql = 'BULK INSERT SOGIP_Tipo_Entidad FROM '''+@VARIABLEACAMBIAR+'\SOGIP_Tipo_Entidad.csv'' WITH(codepage = ''ACP'', fieldterminator = '';'', rowterminator = ''\n'');';
exec (@sql)

set @sql = 'BULK INSERT SOGIP_Users FROM '''+@VARIABLEACAMBIAR+'\SOGIP_Users.csv'' WITH(codepage = ''ACP'', fieldterminator = '';'', rowterminator = ''\n'');';
exec (@sql)

set @sql = 'BULK INSERT SOGIP_UserRoles FROM '''+@VARIABLEACAMBIAR+'\SOGIP_UserRoles.csv'' WITH(codepage = ''ACP'', fieldterminator = '';'', rowterminator = ''\n'');';
exec (@sql)

-- 11 rows
set @sql = 'BULK INSERT SOGIP_Funcionario_ICODER FROM '''+@VARIABLEACAMBIAR+'\SOGIP_Funcionario_ICODER.csv'' WITH(codepage = ''ACP'', fieldterminator = '';'', rowterminator = ''\n'');';
exec (@sql)

set @sql = 'BULK INSERT SOGIP_Asociacion_Deportiva FROM '''+@VARIABLEACAMBIAR+'\SOGIP_Asociacion.csv'' WITH(codepage = ''ACP'', fieldterminator = '';'', rowterminator = ''\n'');';
exec (@sql)

SET IDENTITY_INSERT SOGIP_SELECCIONES ON
set @sql = 'BULK INSERT SOGIP_Selecciones FROM '''+@VARIABLEACAMBIAR+'\SOGIP_Selecciones.csv'' WITH(codepage = ''ACP'', fieldterminator = '';'', rowterminator = ''\n'');';
SET IDENTITY_INSERT SOGIP_SELECCIONES OFF
exec (@sql)

set @sql = 'BULK INSERT SOGIP_SubSeleccion FROM '''+@VARIABLEACAMBIAR+'\SOGIP_SubSeleccion.csv'' WITH(codepage = ''ACP'', datafiletype =''char'', fieldterminator = '';'', rowterminator = ''\n'');';
exec (@sql)
-- 10 rows

set @sql = 'BULK INSERT SOGIP_Atletas FROM '''+@VARIABLEACAMBIAR+'\SOGIP_Atletas.csv'' WITH(codepage = ''ACP'', datafiletype =''char'', fieldterminator = '';'', rowterminator = ''\n'');';
exec (@sql)
-- 10 rows

set @sql = 'BULK INSERT SOGIP_Rutina FROM '''+@VARIABLEACAMBIAR+'\SOGIP_Rutina.csv'' WITH(codepage = ''ACP'', fieldterminator = '';'', rowterminator = ''\n'');';
exec (@sql)
-- 6 rows

set @sql = 'BULK INSERT SOGIP_Conjunto_Ejercicio FROM '''+@VARIABLEACAMBIAR+'\SOGIP_Conjunto_Ejercicio.csv'' WITH(codepage = ''ACP'', fieldterminator = '';'', rowterminator = ''\n'');';
exec (@sql)
-- 33 rows

set @sql = 'BULK INSERT SOGIP_Cita FROM '''+@VARIABLEACAMBIAR+'\SOGIP_Cita.csv'' WITH(codepage = ''ACP'', fieldterminator = '';'', rowterminator = ''\n'');';
exec (@sql)
-- 18 rows

set @sql = 'BULK INSERT SOGIP_Tipo FROM '''+@VARIABLEACAMBIAR+'\SOGIP_Tipo.csv'' WITH(codepage = ''ACP'', fieldterminator = '';'', rowterminator = ''\n'');';
exec (@sql)
-- 6 rows

set @sql = 'BULK INSERT SOGIP_Color FROM '''+@VARIABLEACAMBIAR+'\SOGIP_Color.csv'' WITH(codepage = ''ACP'', fieldterminator = '';'', rowterminator = ''\n'');';
exec (@sql)
-- 7 rows

-- ++++++++++++++++++++++++++ Insert's ++++++++++++++++++++++++++