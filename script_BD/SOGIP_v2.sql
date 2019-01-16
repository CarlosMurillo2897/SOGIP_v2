/*

+++++++ RECONSTRUIR BASE DE DATOS CON LOS CAMBIOS RESPECTIVOS ++++++++++

use master;
drop database "SOGIP_v3"

create database "SOGIP_v3"
use "SOGIP_v3"

 +++++++ RECONSTRUIR BASE DE DATOS CON LOS CAMBIOS RESPECTIVOS ++++++++++

 ++++++++++++++++++++++++++++ Select's ++++++++++++++++++++++++++++

-- Rol = Selección/Federación	|	 User = 116630668	|	 Password = JuGu1166121996
-- Rol = Entrenador				|	 User = 112530907	|	 Password = EmCh1125081985
-- Rol = Atleta					|	 User = 208770327	|	 Password = YuMo2087042006
-- Rol = Atleta Becado			|	 User = 110830174	|	 Password = AaHi1108021980
-- Rol = Funcionario ICODER		|	 User = 206140354	|	 Password = FeBa2061111956
-- Rol = Entidades Públicas		|	 User = 			|	 Password = 
-- Rol = Asociación/Comité		|	 User = 			|	 Password = 
-- Rol = Usuario Externo		|	 User = 			|	 Password = 


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
 select * from SOGIP_Cita;
 select * from SOGIP_Conjunto_Ejercicio;
 select * from SOGIP_Deportes;
 select * from SOGIP_Entidad_Publica;
 select * from SOGIP_Entrenadores;
 select * from SOGIP_Estados;
 select * from SOGIP_Expedientes_Fisicos;
 select * from SOGIP_Funcionario_ICODER;
 select * from SOGIP_Horario;
 select * from SOGIP_Roles order by Id asc;
 select * from SOGIP_Rutina;
 select * from SOGIP_Selecciones;
 select * from SOGIP_Tipo_Deporte;
 select * from SOGIP_Tipo_Entidad;
 select * from SOGIP_UserClaims;
 select * from SOGIP_UserLogins;
 select * from SOGIP_UserRoles where RoleId='6';
 select * from SOGIP_Users;

JoBa1140101989

 SELECT * FROM SOGIP_Users WHERE Cedula like(11)

 -- Describe los atributos de cualquier tabla. --

		sp_help SOGIP_Users; 
		sp_help SOGIP_Entrenadores;

 ++++++++++++++++++++++++++++ Select's ++++++++++++++++++++++++++++

 ++++++++++++++++++++++++++++ Delete's ++++++++++++++++++++++++++++

 delete from SOGIP_Archivo;
 delete from SOGIP_Asociacion_Deportiva;
 delete from SOGIP_Atletas;
 delete from SOGIP_Categorias;
 delete from SOGIP_Cita;
 delete from SOGIP_Conjunto_Ejercicio;
 delete from SOGIP_Deportes;
 delete from SOGIP_Entidad_Publica;
 delete from SOGIP_Entrenadores;
 delete from SOGIP_Estados;
 delete from SOGIP_Expedientes_Fisicos;
 delete from SOGIP_Funcionario_ICODER;
 delete from SOGIP_Horario;
 delete from SOGIP_Roles order by Id asc;
 delete from SOGIP_Rutina;
 delete from SOGIP_Selecciones;
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

insert into SOGIP_Roles values('1', 'Supervisor');
insert into SOGIP_Roles values('2', 'Administrador');
insert into SOGIP_Roles values('3', 'Seleccion/Federacion');
insert into SOGIP_Roles values('4', 'Entrenador');
insert into SOGIP_Roles values('5', 'Atleta');
insert into SOGIP_Roles values('6', 'Atleta Becados');
insert into SOGIP_Roles values('7', 'Funcionarios ICODER');
insert into SOGIP_Roles values('8', 'Entidades Publicas');
insert into SOGIP_Roles values('9', 'Asociacion/Comite');
insert into SOGIP_Roles values('10', 'Usuario Externo');

insert into SOGIP_Estados values('INACTIVO');
insert into SOGIP_Estados values('ACTIVO');
insert into SOGIP_Estados values('FINALIZADO');
insert into SOGIP_Estados values('EN PROCESO');

insert into SOGIP_Categorias values('JUVENIL');
insert into SOGIP_Categorias values('MAYOR');
insert into SOGIP_Categorias values('SUB 20');
insert into SOGIP_Categorias values('NACIONAL');

insert into SOGIP_Tipo_Deporte values('INDIVIDUAL');
insert into SOGIP_Tipo_Deporte values('DE CONJUNTO');
insert into SOGIP_Tipo_Deporte values('DE TIEMPO Y MARCA');
insert into SOGIP_Tipo_Deporte values('DE COMBATE');
insert into SOGIP_Tipo_Deporte values('DE RAQUETA');
insert into SOGIP_Tipo_Deporte values('DE PRECISION');

insert into SOGIP_Deportes values('AJEDREZ', 3);
insert into SOGIP_Deportes values('ATLETISMO', 1);
insert into SOGIP_Deportes values('BILLAR', 1);
insert into SOGIP_Deportes values('BOLICHE', 2);
insert into SOGIP_Deportes values('BOXEO', 1);
insert into SOGIP_Deportes values('CICLISMO', 1);
insert into SOGIP_Deportes values('ESGRIMA', 4);
insert into SOGIP_Deportes values('FISICOCULTURISMO', 1);
insert into SOGIP_Deportes values('HALTEROFILIA', 1);
insert into SOGIP_Deportes values('JUDO', 4);
insert into SOGIP_Deportes values('KARATE DO', 4);
insert into SOGIP_Deportes values('MOTORES', 1);
insert into SOGIP_Deportes values('NADO SINCRONIZADO', 2);
insert into SOGIP_Deportes values('NATACION', 3);
insert into SOGIP_Deportes values('PARALIMPICO', 1);
insert into SOGIP_Deportes values('PATINAJE', 2);
insert into SOGIP_Deportes values('POTENCIA', 1);
insert into SOGIP_Deportes values('PULSOS', 1);
insert into SOGIP_Deportes values('RACQUETBOL', 5);
insert into SOGIP_Deportes values('Atletas Alto Rendimiento', 1);

-- update SOGIP_Users set Estado=0 where Apellido1 = 'Murillo';

insert into SOGIP_Tipo_Entidad values('Aeropuerto Internacional Daniel Oduber');
insert into SOGIP_Tipo_Entidad values('Aeropuerto Internacional Juan Santamaría');
insert into SOGIP_Tipo_Entidad values('Banco Central de Costa Rica (BCCR)');
insert into SOGIP_Tipo_Entidad values('Banco Hipotecario de la Vivienda');
insert into SOGIP_Tipo_Entidad values('Banco Nacional de Costa Rica');
insert into SOGIP_Tipo_Entidad values('Caja Costarricense de Seguro Social (CCSS)');
insert into SOGIP_Tipo_Entidad values('Centro Costarricense de la Ciencia y la Cultura');
insert into SOGIP_Tipo_Entidad values('Centro Costarricense de Producción Cinematográfica');
insert into SOGIP_Tipo_Entidad values('Colegio de Médicos y Cirujanos de Costa Rica');
insert into SOGIP_Tipo_Entidad values('Colegio Federado de Ingenieros y Arquitectos de Costa Rica');
insert into SOGIP_Tipo_Entidad values('Comisión Nacional de Asuntos Indígenas');
insert into SOGIP_Tipo_Entidad values('Comisión Nacional de Emergencias (Costa Rica)');
insert into SOGIP_Tipo_Entidad values('Comisión Nacional de Rescate de Valores (Costa Rica)');
insert into SOGIP_Tipo_Entidad values('Comisionado Nacional Antidrogas');
insert into SOGIP_Tipo_Entidad values('Consejo Nacional de Producción');
insert into SOGIP_Tipo_Entidad values('Consejo Nacional para Investigaciones Científicas y Tecnológicas');
insert into SOGIP_Tipo_Entidad values('Contraloría General de la República de Costa Rica');
insert into SOGIP_Tipo_Entidad values('Corte Suprema de Justicia de Costa Rica');
insert into SOGIP_Tipo_Entidad values('Defensoría de los Habitantes de la República de Costa Rica');
insert into SOGIP_Tipo_Entidad values('Dirección Nacional de CEN-CINAI');
insert into SOGIP_Tipo_Entidad values('Editorial Costa Rica');
insert into SOGIP_Tipo_Entidad values('Escuela de Arquitectura de la Universidad de Costa Rica');
insert into SOGIP_Tipo_Entidad values('Esencial Costa Rica');
insert into SOGIP_Tipo_Entidad values('Ficha de Información Social');
insert into SOGIP_Tipo_Entidad values('Instituto Costarricense de Acueductos y Alcantarillados (AYA)');
insert into SOGIP_Tipo_Entidad values('Instituto Costarricense de Electricidad (ICE)');
insert into SOGIP_Tipo_Entidad values('Instituto Costarricense de Ferrocarriles (INCOFER)');
insert into SOGIP_Tipo_Entidad values('Instituto Costarricense de Pesca y Acuacultura (INCOPESCA)');
insert into SOGIP_Tipo_Entidad values('Instituto Costarricense de Turismo');
insert into SOGIP_Tipo_Entidad values('Instituto Costarricense de Turismo (ICT)');
insert into SOGIP_Tipo_Entidad values('Instituto Costarricense del Deporte y la Recreación (ICODER)');
insert into SOGIP_Tipo_Entidad values('Instituto de Desarrollo Rural');
insert into SOGIP_Tipo_Entidad values('Instituto de Desarrollo Rural (INDER)');
insert into SOGIP_Tipo_Entidad values('Instituto de Fomento y Asesoría Municipal (IFAM)');
insert into SOGIP_Tipo_Entidad values('Instituto Mixto de Ayuda Social (IMAS)');
insert into SOGIP_Tipo_Entidad values('Instituto Nacional de Aprendizaje (INA)');
insert into SOGIP_Tipo_Entidad values('Instituto Nacional de Biodiversidad');
insert into SOGIP_Tipo_Entidad values('Instituto Nacional de Estadística y Censos de Costa Rica');
insert into SOGIP_Tipo_Entidad values('Instituto Nacional de la Mujer (Costa Rica)');
insert into SOGIP_Tipo_Entidad values('Instituto Nacional de la Mujer (INAMU)');
insert into SOGIP_Tipo_Entidad values('Instituto Nacional de Seguros');
insert into SOGIP_Tipo_Entidad values('Instituto Nacional de Seguros (INS)');
insert into SOGIP_Tipo_Entidad values('Instituto Nacional de Vivienda y Urbanismo (INVU)');
insert into SOGIP_Tipo_Entidad values('Instituto Tecnológico de Costa Rica');
insert into SOGIP_Tipo_Entidad values('Junta de Administración Portuaria y de Desarrollo Económico de la Vertiente Atlántica de Costa Rica');
insert into SOGIP_Tipo_Entidad values('Junta de Protección Social (JPS)');
insert into SOGIP_Tipo_Entidad values('Ministerio de Agricultura y Ganadería');insert into SOGIP_Tipo_Entidad values('Ministerio de Ambiente y Energía');
insert into SOGIP_Tipo_Entidad values('Ministerio de Ciencia, Tecnología y Telecomunicaciones');
insert into SOGIP_Tipo_Entidad values('Ministerio de Comercio Exterior');
insert into SOGIP_Tipo_Entidad values('Ministerio de Comunicación');
insert into SOGIP_Tipo_Entidad values('Ministerio de Cultura y Juventud');
insert into SOGIP_Tipo_Entidad values('Ministerio de Deporte y Recreación');
insert into SOGIP_Tipo_Entidad values('Ministerio de Economía, Industria y Comercio');
insert into SOGIP_Tipo_Entidad values('Ministerio de Educación Pública');
insert into SOGIP_Tipo_Entidad values('Ministerio de Gobernación y Policía');
insert into SOGIP_Tipo_Entidad values('Ministerio de Hacienda');
insert into SOGIP_Tipo_Entidad values('Ministerio de Justicia y Paz');
insert into SOGIP_Tipo_Entidad values('Ministerio de la Presidencia');
insert into SOGIP_Tipo_Entidad values('Ministerio de Obras Públicas y Transportes');
insert into SOGIP_Tipo_Entidad values('Ministerio de Planificación Nacional y Política Económica');
insert into SOGIP_Tipo_Entidad values('Ministerio de Relaciones Exteriores');
insert into SOGIP_Tipo_Entidad values('Ministerio de Salud Pública');
insert into SOGIP_Tipo_Entidad values('Ministerio de Seguridad Pública');
insert into SOGIP_Tipo_Entidad values('Ministerio de Trabajo y Seguridad Social');
insert into SOGIP_Tipo_Entidad values('Ministerio de Vivienda y Asentamientos Humanos');
insert into SOGIP_Tipo_Entidad values('Observatorio Vulcanológico y Sismológico de Costa Rica');
insert into SOGIP_Tipo_Entidad values('Organismo de Investigación Judicial');
insert into SOGIP_Tipo_Entidad values('Patronato Nacional de la Infancia (PANI)');
insert into SOGIP_Tipo_Entidad values('Refinadora Costarricense de Petróleo (RECOPE)');
insert into SOGIP_Tipo_Entidad values('Servicio de Vigilancia Aérea de Costa Rica');
insert into SOGIP_Tipo_Entidad values('Sistema de Información de la Población Objetivo');
insert into SOGIP_Tipo_Entidad values('Sistema Nacional de Áreas de Conservación');
insert into SOGIP_Tipo_Entidad values('Sistema Nacional de Radio y Televisión');
insert into SOGIP_Tipo_Entidad values('Universidad Empresarial de Costa Rica');
insert into SOGIP_Tipo_Entidad values('Universidad Estatal a Distancia');
insert into SOGIP_Tipo_Entidad values('Universidad Nacional de Costa Rica');

INSERT INTO [sogip_users] ([Id],[Cedula],[CedulaExtra],[Fecha_Expiracion],[Nombre1],[Nombre2],[Apellido1],[Apellido2],[Fecha_Nacimiento],[Sexo],[Estado],[Email],[EmailConfirmed],[PasswordHash],[SecurityStamp],[PhoneNumber],[PhoneNumberConfirmed],[TwoFactorEnabled],[LockoutEndDateUtc],[LockoutEnabled],[AccessFailedCount],[UserName])VALUES('079f4abe-80b2-43c0-9566-640869ea266a','110280141',NULL,'Apr 20 2019  1:42:39:747PM','ALONSO',NULL,'LEON','MENA','Feb 11 1979 12:00:00:000AM',1,1,'alonso.leon@icoder.co.cr',0,'AEuLAa53HkMBZx7hobzIRLRXS39d88xPS7n6xrsz5cHxjL/TG9UG+FCkyM0kpQ8mLg==','aa379c05-9fe6-4f9f-b0b8-11520a87d019',NULL,0,0,NULL,1,0,'110280141')
INSERT INTO [sogip_users] ([Id],[Cedula],[CedulaExtra],[Fecha_Expiracion],[Nombre1],[Nombre2],[Apellido1],[Apellido2],[Fecha_Nacimiento],[Sexo],[Estado],[Email],[EmailConfirmed],[PasswordHash],[SecurityStamp],[PhoneNumber],[PhoneNumberConfirmed],[TwoFactorEnabled],[LockoutEndDateUtc],[LockoutEnabled],[AccessFailedCount],[UserName])VALUES('09d9e8a1-14e1-43bd-8bc2-75b52378757d','104670428',NULL,'Jan 22 2019  4:02:16:563PM','ILEANA',NULL,'MADRIGAL','CECILIANO','Jul 18 1956 12:00:00:000AM',0,1,'ileana.madrigal@icoder.go.cr',0,'AC8f7m9SJVC5Hp2EpGB+6RvhzQFyW1HDAHMNtNwFzb9q5SPPst6T6pNXnjS9L3HaKg==','5535d562-81b7-42a6-9fb4-83e77a4b5e2f',NULL,0,0,NULL,1,0,'104670428');
INSERT INTO [sogip_users] ([Id],[Cedula],[CedulaExtra],[Fecha_Expiracion],[Nombre1],[Nombre2],[Apellido1],[Apellido2],[Fecha_Nacimiento],[Sexo],[Estado],[Email],[EmailConfirmed],[PasswordHash],[SecurityStamp],[PhoneNumber],[PhoneNumberConfirmed],[TwoFactorEnabled],[LockoutEndDateUtc],[LockoutEnabled],[AccessFailedCount],[UserName])VALUES('0b925665-1c93-42d2-80b0-29c6423daf66','116630668',NULL,'Nov 14 2018  1:28:18:793PM','JUAN','CARLOS','GUTIÉRREZ','VARGAS','Dec 29 1996 12:00:00:000AM',1,1,'balonmano.fecobal@hotmail.es',0,'AKcGKh67YP3QHfjYtrYHR32InrCmzOoJ0+Bc7UnYtZj4zKaQdGkEOr4z6UQxPmBbxw==','c99c18ee-a33a-4956-ba76-28e1f0f0cda1',NULL,0,0,NULL,1,0,'116630668');
INSERT INTO [sogip_users] ([Id],[Cedula],[CedulaExtra],[Fecha_Expiracion],[Nombre1],[Nombre2],[Apellido1],[Apellido2],[Fecha_Nacimiento],[Sexo],[Estado],[Email],[EmailConfirmed],[PasswordHash],[SecurityStamp],[PhoneNumber],[PhoneNumberConfirmed],[TwoFactorEnabled],[LockoutEndDateUtc],[LockoutEnabled],[AccessFailedCount],[UserName])VALUES('0c3fe9ea-2e80-4d7b-b8d2-1158f6c1b824','603290698',NULL,'Jan 22 2019  3:58:20:630PM','YENCI',NULL,'GONZALEZ','RUIZ','Jun  4 1983 12:00:00:000AM',0,1,'yenci.gonzalez@icoder.co.cr',0,'ADz1OynzfyQk7xOpalS/Usus/H+jG1ilTFPPbpWOL01+UFKgCPGMK+C5J/hK68OnUw==','ba3deaaa-ba69-4a54-9369-8bde2cd1166e',NULL,0,0,NULL,1,0,'603290698');
INSERT INTO [sogip_users] ([Id],[Cedula],[CedulaExtra],[Fecha_Expiracion],[Nombre1],[Nombre2],[Apellido1],[Apellido2],[Fecha_Nacimiento],[Sexo],[Estado],[Email],[EmailConfirmed],[PasswordHash],[SecurityStamp],[PhoneNumber],[PhoneNumberConfirmed],[TwoFactorEnabled],[LockoutEndDateUtc],[LockoutEnabled],[AccessFailedCount],[UserName])VALUES('18858306-7098-4657-8f5f-56027fac7952','117910654',NULL,'Nov 14 2018  6:20:41:653PM','JHAIR','ESTEBAN','DIA','CALDERON','Oct 13 2000 12:00:00:000AM',1,1,'fadres2006@hotmail.com',0,'ACw/9y7p/ZW7W6aRMIp9YqmPNpVGVLlsf3UKgklD6hdQV4BoY19LVEAV+cBbEdIwwA==','dbaff8d5-372b-4447-9f2d-5cdb89ba7e3d',NULL,0,0,NULL,1,0,'117910654');
INSERT INTO [sogip_users] ([Id],[Cedula],[CedulaExtra],[Fecha_Expiracion],[Nombre1],[Nombre2],[Apellido1],[Apellido2],[Fecha_Nacimiento],[Sexo],[Estado],[Email],[EmailConfirmed],[PasswordHash],[SecurityStamp],[PhoneNumber],[PhoneNumberConfirmed],[TwoFactorEnabled],[LockoutEndDateUtc],[LockoutEnabled],[AccessFailedCount],[UserName])VALUES('1dd72422-a344-407d-aec6-783907e73164','118860905',NULL,'Nov 14 2018  6:30:39:893PM','FRANCINI','VANESSA','MORA','GARRO','Mar 10 2003 12:00:00:000AM',0,1,'fvanessa2003@hotmail.com',0,'AKJCnx0JQTWjJ0dQKc6IdE649eqxNCGzMq4kiwQ0X+kvBPy4vc/xjAQUS7eWHTIpUQ==','55ccb542-5ecb-4d14-92f1-a42e7c90ad5a',NULL,0,0,NULL,1,0,'118860905');
INSERT INTO [sogip_users] ([Id],[Cedula],[CedulaExtra],[Fecha_Expiracion],[Nombre1],[Nombre2],[Apellido1],[Apellido2],[Fecha_Nacimiento],[Sexo],[Estado],[Email],[EmailConfirmed],[PasswordHash],[SecurityStamp],[PhoneNumber],[PhoneNumberConfirmed],[TwoFactorEnabled],[LockoutEndDateUtc],[LockoutEnabled],[AccessFailedCount],[UserName])VALUES('2d9dcbb1-e611-43a5-928e-ecfa9e057aee','504520933',NULL,'Nov 14 2018  6:28:39:803PM','NAOMY','ALEJANDRA','MENDOZA','MENDOZA','Jan 29 2004 12:00:00:000AM',0,1,'alemenmen@yahoo.com',0,'ABM6mBj3FgOplqrGWQ2LGUHt7axfpvZg8yumTeOCxD13g022QXIBoWZO7la2ps5bQw==','c88cd0a9-69f4-48d9-8585-7c9b5297c0a9',NULL,0,0,NULL,1,0,'504520933');
INSERT INTO [sogip_users] ([Id],[Cedula],[CedulaExtra],[Fecha_Expiracion],[Nombre1],[Nombre2],[Apellido1],[Apellido2],[Fecha_Nacimiento],[Sexo],[Estado],[Email],[EmailConfirmed],[PasswordHash],[SecurityStamp],[PhoneNumber],[PhoneNumberConfirmed],[TwoFactorEnabled],[LockoutEndDateUtc],[LockoutEnabled],[AccessFailedCount],[UserName])VALUES('2f108dd4-69ef-42b2-9f70-fb4f7d7714f9','116630664',NULL,'Nov 14 2018  1:14:06:383PM','RAMÓN',NULL,'COLE','DE TEMPLE','Dec 29 1996 12:00:00:000AM',1,1,'gestionrugbycr@gmail.com',0,'AKPgtnzh3Bd3PiNu8NbRLbCvm99Xi1Y91/oA0BTeSWPv0JxmbZf5VSPWDW/4f4YLaA==','226acc9b-1da1-4843-8cd3-8c5cfb09e2a5',NULL,0,0,NULL,1,0,'116630664');
INSERT INTO [sogip_users] ([Id],[Cedula],[CedulaExtra],[Fecha_Expiracion],[Nombre1],[Nombre2],[Apellido1],[Apellido2],[Fecha_Nacimiento],[Sexo],[Estado],[Email],[EmailConfirmed],[PasswordHash],[SecurityStamp],[PhoneNumber],[PhoneNumberConfirmed],[TwoFactorEnabled],[LockoutEndDateUtc],[LockoutEnabled],[AccessFailedCount],[UserName])VALUES('3df14eaa-e638-4f47-8d03-197dde644d56','111810361',NULL,'Nov 14 2018  4:37:47:620PM','MINOR',NULL,'MONGE','MONTERO','Dec  8 1983 12:00:00:000AM',1,1,'mmonge.montero@gmail.com',0,'AEQs3+TDs5IXMGoDLd8cfewyzlDFahT32i+iQOiX44+F3EZNkLqEoN5vK7AkAz5ZZw==','b2ded438-6def-4302-83dc-a4279faceb5a',NULL,0,0,NULL,1,0,'111810361');
INSERT INTO [sogip_users] ([Id],[Cedula],[CedulaExtra],[Fecha_Expiracion],[Nombre1],[Nombre2],[Apellido1],[Apellido2],[Fecha_Nacimiento],[Sexo],[Estado],[Email],[EmailConfirmed],[PasswordHash],[SecurityStamp],[PhoneNumber],[PhoneNumberConfirmed],[TwoFactorEnabled],[LockoutEndDateUtc],[LockoutEnabled],[AccessFailedCount],[UserName])VALUES('40cb3962-3c2f-4f64-ab03-dba913a03010','116630666',NULL,'Nov 14 2018  1:18:53:523PM','LUIS','ALBERTO','CRUZ','MELÉNDEZ','Dec 29 1996 12:00:00:000AM',1,1,'fmonge@esgrimacostarica.net',0,'AFyH9YooEDK2ybOQEZmxJ06CRCHjxMMZ5kWKjMqrqwPAy/Jk5WlRCoAW3NQesZ7Fxw==','9207e55e-842f-47b8-aa95-ba1efe5d8e8f',NULL,0,0,NULL,1,0,'116630666');
INSERT INTO [sogip_users] ([Id],[Cedula],[CedulaExtra],[Fecha_Expiracion],[Nombre1],[Nombre2],[Apellido1],[Apellido2],[Fecha_Nacimiento],[Sexo],[Estado],[Email],[EmailConfirmed],[PasswordHash],[SecurityStamp],[PhoneNumber],[PhoneNumberConfirmed],[TwoFactorEnabled],[LockoutEndDateUtc],[LockoutEnabled],[AccessFailedCount],[UserName])VALUES('43002ea0-b75c-44a0-8f29-7d5c0eeffed2','112530907',NULL,'Apr 18 2019  4:32:10:283PM','EMMANUEL','DAVID','CHANTO','SEGURA','Aug 25 1985 12:00:00:000AM',1,1,'chanto@chanto.com',0,'AOP0cWhiVc+Eot6ruccEtMvhqwrSt6K6bXixvSTtOryoGNt8TP8k7/ETtBrPjlgACA==','f24f4272-ceeb-4f08-bf54-611817c49847',NULL,0,0,NULL,1,0,'112530907');
INSERT INTO [sogip_users] ([Id],[Cedula],[CedulaExtra],[Fecha_Expiracion],[Nombre1],[Nombre2],[Apellido1],[Apellido2],[Fecha_Nacimiento],[Sexo],[Estado],[Email],[EmailConfirmed],[PasswordHash],[SecurityStamp],[PhoneNumber],[PhoneNumberConfirmed],[TwoFactorEnabled],[LockoutEndDateUtc],[LockoutEnabled],[AccessFailedCount],[UserName])VALUES('4c219de8-b076-4778-84df-bc76e46c138a','119630482',NULL,'Nov 14 2018  6:15:49:943PM','IFER','JORDANA','CHACON','MORALES','Apr 28 2006 12:00:00:000AM',0,1,'ifer28ch@gmail.com',0,'ALb8t6NZSnhvO7sF+Flr9p9IjfpHG9SSacoIO+eHjeU3c91STw5ALK8+0s8k/OvILw==','d318434d-b65a-4ec3-abca-ded4c327396b',NULL,0,0,NULL,1,0,'119630482');
INSERT INTO [sogip_users] ([Id],[Cedula],[CedulaExtra],[Fecha_Expiracion],[Nombre1],[Nombre2],[Apellido1],[Apellido2],[Fecha_Nacimiento],[Sexo],[Estado],[Email],[EmailConfirmed],[PasswordHash],[SecurityStamp],[PhoneNumber],[PhoneNumberConfirmed],[TwoFactorEnabled],[LockoutEndDateUtc],[LockoutEnabled],[AccessFailedCount],[UserName])VALUES('50294ee3-4b38-49ff-8b02-38aacbffb129','703010116',NULL,'Nov 14 2018  6:11:33:367PM','KRISTEL','GABRIELA','ABARCA','JARQUIN','Jul  5 2003 12:00:00:000AM',0,1,'kriss2003j@gmail.com',0,'AOKFBkqo41Yf9v6uSOeeBW4uBXrw71tWtU2ck8BWSyAjQj7JobbAuEDdhqqbOJyf6A==','5e234771-fd03-4793-9a13-d5868699750b',NULL,0,0,NULL,1,0,'703010116');
INSERT INTO [sogip_users] ([Id],[Cedula],[CedulaExtra],[Fecha_Expiracion],[Nombre1],[Nombre2],[Apellido1],[Apellido2],[Fecha_Nacimiento],[Sexo],[Estado],[Email],[EmailConfirmed],[PasswordHash],[SecurityStamp],[PhoneNumber],[PhoneNumberConfirmed],[TwoFactorEnabled],[LockoutEndDateUtc],[LockoutEnabled],[AccessFailedCount],[UserName])VALUES('51653da9-0be5-4cbc-a3d6-1297b6d776a1','116630669',NULL,'Nov 14 2018  1:30:27:760PM','JUAN','MANUEL','GONZÁLEZ','ZAMORA','Dec 29 1996 12:00:00:000AM',1,1,'secretaria@fecoci.net',0,'ANlBJqPL/7OAl3/YHBxwheWV69+tpfe6Jz/gqfHd8OscA03n62fyVxXRhx1m4/rkZA==','c8c5a714-4725-44bb-a655-eb19688a34d2',NULL,0,0,NULL,1,0,'116630669');
INSERT INTO [sogip_users] ([Id],[Cedula],[CedulaExtra],[Fecha_Expiracion],[Nombre1],[Nombre2],[Apellido1],[Apellido2],[Fecha_Nacimiento],[Sexo],[Estado],[Email],[EmailConfirmed],[PasswordHash],[SecurityStamp],[PhoneNumber],[PhoneNumberConfirmed],[TwoFactorEnabled],[LockoutEndDateUtc],[LockoutEnabled],[AccessFailedCount],[UserName])VALUES('516c294e-14c2-4bfd-887a-59e763956a54','208770327',NULL,'Nov 14 2018  6:32:17:377PM','YULEISY','MARIA','MORERA','ALVARADO','Apr 25 2006 12:00:00:000AM',0,1,'yule.morera.al10@gmail.com',0,'ALxWkJy6GOxRE0yEG5b3I8MuBHhv8+oenLvEoSQxApreqO4+rmWQXMWoKW4v9GySrA==','f0e6f2ae-c17a-476e-b3c1-c0f68014b92d',NULL,0,0,NULL,1,0,'208770327');
INSERT INTO [sogip_users] ([Id],[Cedula],[CedulaExtra],[Fecha_Expiracion],[Nombre1],[Nombre2],[Apellido1],[Apellido2],[Fecha_Nacimiento],[Sexo],[Estado],[Email],[EmailConfirmed],[PasswordHash],[SecurityStamp],[PhoneNumber],[PhoneNumberConfirmed],[TwoFactorEnabled],[LockoutEndDateUtc],[LockoutEnabled],[AccessFailedCount],[UserName])VALUES('5c48e11d-0abc-4f76-88a5-bc49e83af272','112600685',NULL,'Apr 20 2019  1:38:49:550PM','AGUSTIN',NULL,'HERRERA','CORDERO','Nov  8 1985 12:00:00:000AM',1,1,'agustin.herrera@icoder.co.cr',0,'AHfDOYBGEabFBynUnXw+nQsQ+n6kTPX1mDeyZdObrVgourKOUmOEvtBoo619B3ALag==','b95dd422-32ac-4707-a4c1-b1b9952d2a6f',NULL,0,0,NULL,1,0,'112600685');
INSERT INTO [sogip_users] ([Id],[Cedula],[CedulaExtra],[Fecha_Expiracion],[Nombre1],[Nombre2],[Apellido1],[Apellido2],[Fecha_Nacimiento],[Sexo],[Estado],[Email],[EmailConfirmed],[PasswordHash],[SecurityStamp],[PhoneNumber],[PhoneNumberConfirmed],[TwoFactorEnabled],[LockoutEndDateUtc],[LockoutEnabled],[AccessFailedCount],[UserName])VALUES('6642f3e1-dc3f-43df-9be2-ebff4ee67d56','104730812',NULL,'Jan 22 2019  4:00:18:750PM','ARCADIO',NULL,'QUESADA','BARRANTES','Jul 21 1956 12:00:00:000AM',1,1,'arcadio.quesada@icoder.go.cr',0,'AOuwdjmySA/PV9y5wcsj0tMAWPkECptnCN2YWRxUFWT1x43RFw2v2NukoVLqFG9c/A==','ae5e643c-5830-4ffe-bb91-d1caeb551412',NULL,0,0,NULL,1,0,'104730812');
INSERT INTO [sogip_users] ([Id],[Cedula],[CedulaExtra],[Fecha_Expiracion],[Nombre1],[Nombre2],[Apellido1],[Apellido2],[Fecha_Nacimiento],[Sexo],[Estado],[Email],[EmailConfirmed],[PasswordHash],[SecurityStamp],[PhoneNumber],[PhoneNumberConfirmed],[TwoFactorEnabled],[LockoutEndDateUtc],[LockoutEnabled],[AccessFailedCount],[UserName])VALUES('6a86769d-4fbf-4e78-b2bb-8c2524c6a968','116630663',NULL,'Nov 14 2018  1:12:21:820PM','WILMAR',NULL,'ALVARADO','CASTILLO','Dec 29 1996 12:00:00:000AM',1,1,'presidente@tkdcr.org',0,'AFH+r9xLrqyzEQ7dB0WrBVeeUr+0A9f3/AgqSBEFoGn7NGRW/HCoHUV2QWlW4y7Klw==','0a55be59-7fbf-46b8-955c-405267e8c932',NULL,0,0,NULL,1,0,'116630663');
INSERT INTO [sogip_users] ([Id],[Cedula],[CedulaExtra],[Fecha_Expiracion],[Nombre1],[Nombre2],[Apellido1],[Apellido2],[Fecha_Nacimiento],[Sexo],[Estado],[Email],[EmailConfirmed],[PasswordHash],[SecurityStamp],[PhoneNumber],[PhoneNumberConfirmed],[TwoFactorEnabled],[LockoutEndDateUtc],[LockoutEnabled],[AccessFailedCount],[UserName])VALUES('70822878-4c81-4972-8208-e1ad12d0d7cf','208640722',NULL,'Nov 14 2018  6:24:32:400PM','EMILY','MARIA','JIMENEZ','VILLALOBOS','Apr 14 2005 12:00:00:000AM',0,1,'emilyv69@gmail.com',0,'AE3bLtV1ZaTfbypxsjYth5ovquQ8DSpgo2cduwhZc86+mj0PgIlIMP4IYJzrcsJUlw==','bde76a20-36f4-4a50-8338-3904d18e3fc4',NULL,0,0,NULL,1,0,'208640722');
INSERT INTO [sogip_users] ([Id],[Cedula],[CedulaExtra],[Fecha_Expiracion],[Nombre1],[Nombre2],[Apellido1],[Apellido2],[Fecha_Nacimiento],[Sexo],[Estado],[Email],[EmailConfirmed],[PasswordHash],[SecurityStamp],[PhoneNumber],[PhoneNumberConfirmed],[TwoFactorEnabled],[LockoutEndDateUtc],[LockoutEnabled],[AccessFailedCount],[UserName])VALUES('71cb667a-9ab0-41bb-aad6-33ef288a15eb','116630661',NULL,'Nov 14 2018  1:07:20:667PM','DUDLEY',NULL,'LÓPEZ','URIBE','Dec 29 1996 12:00:00:000AM',1,1,'dlopez@fecojudo.com',0,'AGaqRCzrGEnd2cV3eNK3DHEnd2E5QLG/mxpXOKhrgx/vWV8SQr7lT8AGqauPvcHiVg==','1fc707dc-8ccb-47ab-91ee-67f2b453baff',NULL,0,0,NULL,1,0,'116630661');
INSERT INTO [sogip_users] ([Id],[Cedula],[CedulaExtra],[Fecha_Expiracion],[Nombre1],[Nombre2],[Apellido1],[Apellido2],[Fecha_Nacimiento],[Sexo],[Estado],[Email],[EmailConfirmed],[PasswordHash],[SecurityStamp],[PhoneNumber],[PhoneNumberConfirmed],[TwoFactorEnabled],[LockoutEndDateUtc],[LockoutEnabled],[AccessFailedCount],[UserName])VALUES('774079d4-2a34-4bfe-8c74-1fe3a3df5c45','604650760',NULL,'Nov 14 2018  6:34:30:523PM','REYCHELL','MARIA','OBREGON','BLANCO','Mar 27 2001 12:00:00:000AM',0,1,'blanco.272001@yahoo.com',0,'AF0Y6mcqumKbXfvW743Ac7yNy9/NDJHKIQWEztgxcPHUsQZG+CMoHSfek3hzWQKx/Q==','24f4f10d-e829-4d33-966e-bef52fd30f55',NULL,0,0,NULL,1,0,'604650760');
INSERT INTO [sogip_users] ([Id],[Cedula],[CedulaExtra],[Fecha_Expiracion],[Nombre1],[Nombre2],[Apellido1],[Apellido2],[Fecha_Nacimiento],[Sexo],[Estado],[Email],[EmailConfirmed],[PasswordHash],[SecurityStamp],[PhoneNumber],[PhoneNumberConfirmed],[TwoFactorEnabled],[LockoutEndDateUtc],[LockoutEnabled],[AccessFailedCount],[UserName])VALUES('77e4861f-9ad0-45c1-929e-464061866233','116630667',NULL,'Nov 14 2018  1:24:32:377PM','RAFAEL',NULL,'VEGA','RODRÍGUEZ','Dec 29 1996 12:00:00:000AM',1,1,'vegaboxcr@gmail.com',0,'APah8YnwAZ210pz/hiXBjC8l65m0OJ4Rcr3k2OWP1S4XpGLbr6uj5RsV7q3vKzpSDQ==','a926a818-918a-4430-b9dc-1bb58c8f5f6b',NULL,0,0,NULL,1,0,'116630667');
INSERT INTO [sogip_users] ([Id],[Cedula],[CedulaExtra],[Fecha_Expiracion],[Nombre1],[Nombre2],[Apellido1],[Apellido2],[Fecha_Nacimiento],[Sexo],[Estado],[Email],[EmailConfirmed],[PasswordHash],[SecurityStamp],[PhoneNumber],[PhoneNumberConfirmed],[TwoFactorEnabled],[LockoutEndDateUtc],[LockoutEnabled],[AccessFailedCount],[UserName])VALUES('79a605e5-2900-44bf-9b35-eaf0b419bd17','302070270',NULL,'Jan 22 2019  4:04:59:927PM','ALEJANDRA',NULL,'VALVERDE','BRENES','Feb 19 1955 12:00:00:000AM',0,1,'alejandra.valverde@icoder.go.cr',0,'AOBW3lc3N/8SxJ34xf0veqxxivER5SDuOPXB2YCU2csvA94Rnj8RgdQaIfAJmfnHZQ==','c635327d-8421-431d-bf0b-c10731f7919f',NULL,0,0,NULL,1,0,'302070270');
INSERT INTO [sogip_users] ([Id],[Cedula],[CedulaExtra],[Fecha_Expiracion],[Nombre1],[Nombre2],[Apellido1],[Apellido2],[Fecha_Nacimiento],[Sexo],[Estado],[Email],[EmailConfirmed],[PasswordHash],[SecurityStamp],[PhoneNumber],[PhoneNumberConfirmed],[TwoFactorEnabled],[LockoutEndDateUtc],[LockoutEnabled],[AccessFailedCount],[UserName])VALUES('7fcacfe5-9e6e-4b89-9eaa-4a608eefb3bd','205790100',NULL,'Nov 14 2018  4:41:08:977PM','JOAQUIN',NULL,'BARRANTES','SOLANO','Feb 12 1982 12:00:00:000AM',1,1,'jbarrantes1982@hotmail.com',0,'AGo5tVcfL/NaLPlyIvl6ZSyDA9Wl+ch2AnrMbWvmZQpw7FBsO1f0fyd2Bn+sDOsYKg==','c8f872fc-8565-448c-a94a-ede68192586b',NULL,0,0,NULL,1,0,'205790100');
INSERT INTO [sogip_users] ([Id],[Cedula],[CedulaExtra],[Fecha_Expiracion],[Nombre1],[Nombre2],[Apellido1],[Apellido2],[Fecha_Nacimiento],[Sexo],[Estado],[Email],[EmailConfirmed],[PasswordHash],[SecurityStamp],[PhoneNumber],[PhoneNumberConfirmed],[TwoFactorEnabled],[LockoutEndDateUtc],[LockoutEnabled],[AccessFailedCount],[UserName])VALUES('843f21d4-b301-4847-848b-f4cd20827a6a','206140354',NULL,'Jan 22 2019  4:04:20:163PM','FELIPE',NULL,'BARRANTES','MADRIZ','Nov 28 1956 12:00:00:000AM',1,1,'felipe.barrantes@icoder.go.cr',0,'AD0ykwUv1mMXpk8O+5xyeXMNH4rdawPru+iAyDqFNoKMJZSoCRTIcHV2oq3apwFEnw==','1920116b-64f5-4ab9-8ee7-b78986029ce7',NULL,0,0,NULL,1,0,'206140354');
INSERT INTO [sogip_users] ([Id],[Cedula],[CedulaExtra],[Fecha_Expiracion],[Nombre1],[Nombre2],[Apellido1],[Apellido2],[Fecha_Nacimiento],[Sexo],[Estado],[Email],[EmailConfirmed],[PasswordHash],[SecurityStamp],[PhoneNumber],[PhoneNumberConfirmed],[TwoFactorEnabled],[LockoutEndDateUtc],[LockoutEnabled],[AccessFailedCount],[UserName])VALUES('8ed376ac-b906-4ead-b3f7-2bd405fb00a4','116630660',NULL,'Nov 14 2018  1:04:59:467PM','GEEN',NULL,'CLARKE','CLARKE','Dec 29 1996 12:00:00:000AM',1,1,'secretaria.fecoa@gmail.com',0,'AJDnORhN65lNYyC0wGNRvI4+CstROyD6hFTf9Pc4Ggo2yzKYRHf6pd5p2P6Nk0WdXQ==','277844b3-3022-47ff-a036-36bbc6f1a0a6',NULL,0,0,NULL,1,0,'116630660');
INSERT INTO [sogip_users] ([Id],[Cedula],[CedulaExtra],[Fecha_Expiracion],[Nombre1],[Nombre2],[Apellido1],[Apellido2],[Fecha_Nacimiento],[Sexo],[Estado],[Email],[EmailConfirmed],[PasswordHash],[SecurityStamp],[PhoneNumber],[PhoneNumberConfirmed],[TwoFactorEnabled],[LockoutEndDateUtc],[LockoutEnabled],[AccessFailedCount],[UserName])VALUES('8f9c47bf-edbd-40bf-9b5e-f753dd81a766','000000000',NULL,'Apr 19 2019  9:34:58:363PM','SISTEMA','OPERATIVO','GIMNASIO','PESAS','Jan  1 2000 12:00:00:000AM',1,1,'admSOGIP@hotmail.com',0,'AG33oMVrb494bn6JdvpIGf2UV1wbopV1ttoNqJP9/LOD8S0PPltbD4XUYzMorUX8mA==','6508b809-9b26-4f7e-aad6-b550cbe32fa6',NULL,0,0,NULL,1,0,'000000000');
INSERT INTO [sogip_users] ([Id],[Cedula],[CedulaExtra],[Fecha_Expiracion],[Nombre1],[Nombre2],[Apellido1],[Apellido2],[Fecha_Nacimiento],[Sexo],[Estado],[Email],[EmailConfirmed],[PasswordHash],[SecurityStamp],[PhoneNumber],[PhoneNumberConfirmed],[TwoFactorEnabled],[LockoutEndDateUtc],[LockoutEnabled],[AccessFailedCount],[UserName])VALUES('914db4cb-8e02-4476-9e42-31befefd7a0e','402120310',NULL,'Apr 18 2019  5:08:56:407PM','ANA','MARIA','PORRAS','LORIA','Sep 21 1991 12:00:00:000AM',0,1,'anaporras@anaporras.com',0,'AMJoytp/8v+RwnlaWrtK4MDwxUGWwBdJ+/XNbpQbtRo0bC2eTf3uMs5rX0iHsus1ng==','e4a8b6c5-eb52-4c07-aeae-48216b8b286e',NULL,0,0,NULL,1,0,'402120310');
INSERT INTO [sogip_users] ([Id],[Cedula],[CedulaExtra],[Fecha_Expiracion],[Nombre1],[Nombre2],[Apellido1],[Apellido2],[Fecha_Nacimiento],[Sexo],[Estado],[Email],[EmailConfirmed],[PasswordHash],[SecurityStamp],[PhoneNumber],[PhoneNumberConfirmed],[TwoFactorEnabled],[LockoutEndDateUtc],[LockoutEnabled],[AccessFailedCount],[UserName])VALUES('95c12f5c-7b9c-4a65-abd5-1dcdc604f847','109710144',NULL,'Nov 14 2018  4:49:02:463PM','KENJETH',NULL,'TENCIO','CERVANTES','May 27 1977 12:00:00:000AM',1,1,'ken.tencio.cervantes@hotmail.com',0,'APH1m5eZTsU1SXIDP8f9V7ZuXD/g+zj4nVZ9mSA2rALrWwD+AzHbdgf+e2d0NPXFDA==','dd404910-f043-4314-8557-30b40fea7ac1',NULL,0,0,NULL,1,0,'109710144');
INSERT INTO [sogip_users] ([Id],[Cedula],[CedulaExtra],[Fecha_Expiracion],[Nombre1],[Nombre2],[Apellido1],[Apellido2],[Fecha_Nacimiento],[Sexo],[Estado],[Email],[EmailConfirmed],[PasswordHash],[SecurityStamp],[PhoneNumber],[PhoneNumberConfirmed],[TwoFactorEnabled],[LockoutEndDateUtc],[LockoutEnabled],[AccessFailedCount],[UserName])VALUES('9d9d279f-016a-47e9-bf70-9ed4a4754de5','110830174',NULL,'Apr 18 2019  5:07:06:053PM','AARON','ANDRES','HIDALGO','NUÑEZ','Feb 10 1980 12:00:00:000AM',1,1,'aaron@aaron.com',0,'AI33t6s1zh79xQNvhIZu+a3ew6AP+qhorBRdp0Q8xFn6bhhU4cemPKEE8t334VC2aA==','b8589e3c-e75c-47a0-9dfc-5e56ea5582eb',NULL,0,0,NULL,1,0,'110830174');
INSERT INTO [sogip_users] ([Id],[Cedula],[CedulaExtra],[Fecha_Expiracion],[Nombre1],[Nombre2],[Apellido1],[Apellido2],[Fecha_Nacimiento],[Sexo],[Estado],[Email],[EmailConfirmed],[PasswordHash],[SecurityStamp],[PhoneNumber],[PhoneNumberConfirmed],[TwoFactorEnabled],[LockoutEndDateUtc],[LockoutEnabled],[AccessFailedCount],[UserName])VALUES('b76a265d-2b9d-4aac-963f-0cc50595b639','117370048',NULL,'Nov 14 2018  6:13:42:260PM','DAVID','GUILLERMO','ABARCA','PADILLA','Mar 16 1999 12:00:00:000AM',1,1,'lus45a@hotmail.com',0,'ALLamDtUDjybcmFFcgQPBfWjPm6ZjDJBn1cH9m4RSbSaIuQ+7IcF37fA+yc5vvr43g==','5827c8b8-bc93-47f3-b699-4a8e67de0381',NULL,0,0,NULL,1,0,'117370048');
INSERT INTO [sogip_users] ([Id],[Cedula],[CedulaExtra],[Fecha_Expiracion],[Nombre1],[Nombre2],[Apellido1],[Apellido2],[Fecha_Nacimiento],[Sexo],[Estado],[Email],[EmailConfirmed],[PasswordHash],[SecurityStamp],[PhoneNumber],[PhoneNumberConfirmed],[TwoFactorEnabled],[LockoutEndDateUtc],[LockoutEnabled],[AccessFailedCount],[UserName])VALUES('b889fe47-541d-4453-8733-022709c1592f','114070986',NULL,'Apr 18 2019  3:52:12:557PM','JOSAFAT','ANTONIO','BARBOZA','UMAÑA','Oct  6 1989 12:00:00:000AM',1,1,'josa@josa.com',0,'AAAAGb+hoJnZqkFtRi3SDPAQqwviIyybQISr4nF99nLf9cEoygh0rzKIUm2Dx1qYyQ==','de3fb782-d243-4a25-8106-623ebfa68fc7',NULL,0,0,NULL,1,0,'114070986');
INSERT INTO [sogip_users] ([Id],[Cedula],[CedulaExtra],[Fecha_Expiracion],[Nombre1],[Nombre2],[Apellido1],[Apellido2],[Fecha_Nacimiento],[Sexo],[Estado],[Email],[EmailConfirmed],[PasswordHash],[SecurityStamp],[PhoneNumber],[PhoneNumberConfirmed],[TwoFactorEnabled],[LockoutEndDateUtc],[LockoutEnabled],[AccessFailedCount],[UserName])VALUES('c6d47fdc-c219-4d7f-b613-0aca0c812a29','205940271',NULL,'Apr 20 2019  1:40:26:030PM','JUAN','GABRIEL','ARCE','VIQUEZ','May  5 1984 12:00:00:000AM',1,1,'juangabriel.arce@icoder.co.cr',0,'AHWWx/H6t/zJVPpbdk115btZWrHz302G3/WQflJUxJzT0o9y9iy9Od1DPZXhfFzp+g==','3c3063d7-2b23-4248-a070-32fa9d4ced96',NULL,0,0,NULL,1,0,'205940271');
INSERT INTO [sogip_users] ([Id],[Cedula],[CedulaExtra],[Fecha_Expiracion],[Nombre1],[Nombre2],[Apellido1],[Apellido2],[Fecha_Nacimiento],[Sexo],[Estado],[Email],[EmailConfirmed],[PasswordHash],[SecurityStamp],[PhoneNumber],[PhoneNumberConfirmed],[TwoFactorEnabled],[LockoutEndDateUtc],[LockoutEnabled],[AccessFailedCount],[UserName])VALUES('cbbe0afc-dbf4-46b3-ba5f-91ca8531decd','115280606',NULL,'Jan 22 2019  3:59:35:137PM','ELIZABETH',NULL,'CESPEDES','VIQUEZ','Feb 22 1993 12:00:00:000AM',0,1,'elizabeth.cespedes@icoder.go.cr',0,'AP6Ij6lHC9SYnAsen9j1DUsmbhoSie32VwozIHClTELLOL6XOumpltyGcqJSb4q8Zw==','c866e133-79d6-4663-b0e3-a2cd70b92670',NULL,0,0,NULL,1,0,'115280606');
INSERT INTO [sogip_users] ([Id],[Cedula],[CedulaExtra],[Fecha_Expiracion],[Nombre1],[Nombre2],[Apellido1],[Apellido2],[Fecha_Nacimiento],[Sexo],[Estado],[Email],[EmailConfirmed],[PasswordHash],[SecurityStamp],[PhoneNumber],[PhoneNumberConfirmed],[TwoFactorEnabled],[LockoutEndDateUtc],[LockoutEnabled],[AccessFailedCount],[UserName])VALUES('d449b200-fdf7-4c3a-ac1d-07bd7e496137','402670252',NULL,'Nov 14 2018  6:22:53:870PM','YADER','JOSE','GOMEZ','SOLIS','Jan  3 2005 12:00:00:000AM',1,1,'yadgomso@gmail.com',0,'APbl2XQOOLA9dECszQC6oSBJx1GMdij0nIsDS2wdoBaHaV8TGiOVgoKHjDsh6Ybo2g==','d2f4d6d9-94ec-4b65-8ee5-da75572a39ec',NULL,0,0,NULL,1,0,'402670252');
INSERT INTO [sogip_users] ([Id],[Cedula],[CedulaExtra],[Fecha_Expiracion],[Nombre1],[Nombre2],[Apellido1],[Apellido2],[Fecha_Nacimiento],[Sexo],[Estado],[Email],[EmailConfirmed],[PasswordHash],[SecurityStamp],[PhoneNumber],[PhoneNumberConfirmed],[TwoFactorEnabled],[LockoutEndDateUtc],[LockoutEnabled],[AccessFailedCount],[UserName])VALUES('de8b4ac7-40a3-4b23-aa2b-28ae9fcb9253','402260033',NULL,'Apr 18 2019  5:13:38:123PM','EMMANUEL',NULL,'NIÑO','VILLALTA','Feb 14 1995 12:00:00:000AM',1,1,'emmanuelnino@emmanuelnino.com',0,'AArLIlxJ6VcgWrGQRqlVf8jM1UA7+WoOndCCiMZ3D6bJJQ2KI8MiBLmttJCQSL/3Hg==','93511ecb-cb2b-4a6c-8191-81dcfdc47ced',NULL,0,0,NULL,1,0,'402260033');
INSERT INTO [sogip_users] ([Id],[Cedula],[CedulaExtra],[Fecha_Expiracion],[Nombre1],[Nombre2],[Apellido1],[Apellido2],[Fecha_Nacimiento],[Sexo],[Estado],[Email],[EmailConfirmed],[PasswordHash],[SecurityStamp],[PhoneNumber],[PhoneNumberConfirmed],[TwoFactorEnabled],[LockoutEndDateUtc],[LockoutEnabled],[AccessFailedCount],[UserName])VALUES('f0d05b48-9097-4d97-abc8-5ffdebe81d4c','116630665',NULL,'Nov 14 2018  1:15:26:767PM','ANGEL',NULL,'HERRERA','ULLOA','Dec 29 1996 12:00:00:000AM',1,1,'info@fecona.co.cr',0,'ABsGXQpaHZqBDEZ0yICudYAGc4cX6IJv9i/a3SSuF72qJNcxe7GFQuv5QUKuh5Kccg==','54d52c10-711a-4460-bc41-f221a5403359',NULL,0,0,NULL,1,0,'116630665');
INSERT INTO [sogip_users] ([Id],[Cedula],[CedulaExtra],[Fecha_Expiracion],[Nombre1],[Nombre2],[Apellido1],[Apellido2],[Fecha_Nacimiento],[Sexo],[Estado],[Email],[EmailConfirmed],[PasswordHash],[SecurityStamp],[PhoneNumber],[PhoneNumberConfirmed],[TwoFactorEnabled],[LockoutEndDateUtc],[LockoutEnabled],[AccessFailedCount],[UserName])VALUES('f67e2727-0e64-449d-8186-8ee3656a41ba','116630662',NULL,'Nov 14 2018  1:10:43:690PM','EDGAR',NULL,'ALVARADO','ARDÓN','Dec 29 1996 12:00:00:000AM',1,1,'fecovol@gmail.com',0,'AFPveN8Z5givd1HKUCO70x9XFFdfybgQaPFgjfgLVl5+htxEyox8DXg4j2VNj5n7dg==','e6f7c02a-ceb2-4564-9544-33de33985e46',NULL,0,0,NULL,1,0,'116630662');
INSERT INTO [sogip_users] ([Id],[Cedula],[CedulaExtra],[Fecha_Expiracion],[Nombre1],[Nombre2],[Apellido1],[Apellido2],[Fecha_Nacimiento],[Sexo],[Estado],[Email],[EmailConfirmed],[PasswordHash],[SecurityStamp],[PhoneNumber],[PhoneNumberConfirmed],[TwoFactorEnabled],[LockoutEndDateUtc],[LockoutEnabled],[AccessFailedCount],[UserName])VALUES('fbb7a9fe-9975-4f17-bff2-d760d267a942','111470524',NULL,'Jan 22 2019  4:01:05:007PM','MANUEL',NULL,'GUZMAN','SABORIO','Aug 25 1982 12:00:00:000AM',1,1,'manuel.guzman@icoder.go.cr',0,'AB1OFhuEBHK7opKoLmdAMWAMUwkCE7ILUQ+fYYQzayVfvJVOnFrBli6FpM1BxnXK7A==','cab480f8-b03d-47aa-9f90-e7c197325e5c',NULL,0,0,NULL,1,0,'111470524');
INSERT INTO [sogip_users] ([Id],[Cedula],[CedulaExtra],[Fecha_Expiracion],[Nombre1],[Nombre2],[Apellido1],[Apellido2],[Fecha_Nacimiento],[Sexo],[Estado],[Email],[EmailConfirmed],[PasswordHash],[SecurityStamp],[PhoneNumber],[PhoneNumberConfirmed],[TwoFactorEnabled],[LockoutEndDateUtc],[LockoutEnabled],[AccessFailedCount],[UserName])VALUES('377e1527-58bb-40dc-a873-f88d4a1c1fcf', '118830889', NULL, current_timestamp, 'GERARDO', NULL, 'ROJAS', 'ROJAS', '1975-01-13 00:00:00.000', 1, 1, 'gerardo.rojas@gmail.com', 0, 'AB0zA76vmOAJoo7FihO55BnxfiLLX8nuNW/TSAGsbgPA7Zq/dDA2KwUPs+Jqg36UQw==', '3aa7b84c-00b2-4685-8fbf-b879a0f59d62', NULL, 0, 0, NULL, 1, 0, '118830889');
INSERT INTO [sogip_users] ([Id],[Cedula],[CedulaExtra],[Fecha_Expiracion],[Nombre1],[Nombre2],[Apellido1],[Apellido2],[Fecha_Nacimiento],[Sexo],[Estado],[Email],[EmailConfirmed],[PasswordHash],[SecurityStamp],[PhoneNumber],[PhoneNumberConfirmed],[TwoFactorEnabled],[LockoutEndDateUtc],[LockoutEnabled],[AccessFailedCount],[UserName])VALUES('7440f3c4-3528-4606-9bc8-501ad8f15b51', '117709069', NULL, current_timestamp, 'PETER', NULL, 'JARA', 'GARCIA', '1996-12-29 00:00:00.000', 1, 1, 'peter.jara@gmail.com', 0, 'AHcuQU/b0Cuxth0cLPpP81p64kRCG7lUUzKFMgCAsCK5Tz+o1qZmoywnWzUbp9Pr9g==', '784ca7f2-b036-4fdc-a56c-c2aaa022ab4a', NULL, 0, 0, NULL, 1, 0, '117709069');
INSERT INTO [sogip_users] ([Id],[Cedula],[CedulaExtra],[Fecha_Expiracion],[Nombre1],[Nombre2],[Apellido1],[Apellido2],[Fecha_Nacimiento],[Sexo],[Estado],[Email],[EmailConfirmed],[PasswordHash],[SecurityStamp],[PhoneNumber],[PhoneNumberConfirmed],[TwoFactorEnabled],[LockoutEndDateUtc],[LockoutEnabled],[AccessFailedCount],[UserName])VALUES('2f3a289f-6370-4524-a21a-c72d5f5699e4', '123456787', NULL, current_timestamp, 'LUIS', NULL, 'SOLANO', NULL, '1977-03-09 00:00:00.000', 1, 1, 'solanoaraya@gmail.com', 0, 'ACdXCJQQMBbefEsJVjCA9lW9cY0RxpsIe/vY3mtNbcFP3qp8tIwBN7KbNM2gTQlSFg==', '1e48eeba-2205-4d82-a579-4c7150842889', NULL, 0, 0, NULL, 1, 0, '123456787');
INSERT INTO [sogip_users] ([Id],[Cedula],[CedulaExtra],[Fecha_Expiracion],[Nombre1],[Nombre2],[Apellido1],[Apellido2],[Fecha_Nacimiento],[Sexo],[Estado],[Email],[EmailConfirmed],[PasswordHash],[SecurityStamp],[PhoneNumber],[PhoneNumberConfirmed],[TwoFactorEnabled],[LockoutEndDateUtc],[LockoutEnabled],[AccessFailedCount],[UserName])VALUES('b3bf343d-4526-41ac-ba13-4c7212d4c5b3', '112880664', NULL, current_timestamp, 'JOHANNA', 'ANDREA', 'ARAYA', 'VALVERDE', '1986-08-25 00:00:00.000', 0, 1, 'johanna.araya@icoder.go.cr', 0, 'AHSTwGe+NiE/PDZr8j9E2mdWNMVhKr/mGI0pzcG1JcuP/MWAsKMpEtxbzuNvVO4NXA==', '81222d33-d6e3-48f2-9f18-652b40efa227', NULL, 0, 0, NULL, 1, 0, '112880664');
INSERT INTO [sogip_users] ([Id],[Cedula],[CedulaExtra],[Fecha_Expiracion],[Nombre1],[Nombre2],[Apellido1],[Apellido2],[Fecha_Nacimiento],[Sexo],[Estado],[Email],[EmailConfirmed],[PasswordHash],[SecurityStamp],[PhoneNumber],[PhoneNumberConfirmed],[TwoFactorEnabled],[LockoutEndDateUtc],[LockoutEnabled],[AccessFailedCount],[UserName])VALUES('137feecf-2c48-4e86-8b79-5906b0057c70', '105600812', NULL, current_timestamp, 'MARCO', NULL, 'MADRIGAL', 'MONGE', '1999-03-12 00:00:00.000', 1, 1, 'marco.madrigal@gmail.com', 0, 'AGb93SXXVUTzlgqtQLjK0Gs5NTWHnd2g8Z0QBj8epBwKU/WBdUvSKTNqLKbvTZ5yQg==', '4aaac041-df46-4a26-91b3-5efad380698c', NULL, 0, 0, NULL, 1, 0, '105600812');


--INSERTS ROLES--
INSERT INTO [sogip_userroles] ([UserId],[RoleId])VALUES('8f9c47bf-edbd-40bf-9b5e-f753dd81a766','1');
INSERT INTO [sogip_userroles] ([UserId],[RoleId])VALUES('b889fe47-541d-4453-8733-022709c1592f','2');
INSERT INTO [sogip_userroles] ([UserId],[RoleId])VALUES('0b925665-1c93-42d2-80b0-29c6423daf66','3');
INSERT INTO [sogip_userroles] ([UserId],[RoleId])VALUES('2f108dd4-69ef-42b2-9f70-fb4f7d7714f9','3');
INSERT INTO [sogip_userroles] ([UserId],[RoleId])VALUES('40cb3962-3c2f-4f64-ab03-dba913a03010','3');
INSERT INTO [sogip_userroles] ([UserId],[RoleId])VALUES('51653da9-0be5-4cbc-a3d6-1297b6d776a1','3');
INSERT INTO [sogip_userroles] ([UserId],[RoleId])VALUES('6a86769d-4fbf-4e78-b2bb-8c2524c6a968','3');
INSERT INTO [sogip_userroles] ([UserId],[RoleId])VALUES('71cb667a-9ab0-41bb-aad6-33ef288a15eb','3');
INSERT INTO [sogip_userroles] ([UserId],[RoleId])VALUES('77e4861f-9ad0-45c1-929e-464061866233','3');
INSERT INTO [sogip_userroles] ([UserId],[RoleId])VALUES('8ed376ac-b906-4ead-b3f7-2bd405fb00a4','3');
INSERT INTO [sogip_userroles] ([UserId],[RoleId])VALUES('f0d05b48-9097-4d97-abc8-5ffdebe81d4c','3');
INSERT INTO [sogip_userroles] ([UserId],[RoleId])VALUES('f67e2727-0e64-449d-8186-8ee3656a41ba','3');
INSERT INTO [sogip_userroles] ([UserId],[RoleId])VALUES('3df14eaa-e638-4f47-8d03-197dde644d56','4');
INSERT INTO [sogip_userroles] ([UserId],[RoleId])VALUES('43002ea0-b75c-44a0-8f29-7d5c0eeffed2','4');
INSERT INTO [sogip_userroles] ([UserId],[RoleId])VALUES('7fcacfe5-9e6e-4b89-9eaa-4a608eefb3bd','4');
INSERT INTO [sogip_userroles] ([UserId],[RoleId])VALUES('95c12f5c-7b9c-4a65-abd5-1dcdc604f847','4');
INSERT INTO [sogip_userroles] ([UserId],[RoleId])VALUES('2f3a289f-6370-4524-a21a-c72d5f5699e4','4');
INSERT INTO [sogip_userroles] ([UserId],[RoleId])VALUES('18858306-7098-4657-8f5f-56027fac7952','5');
INSERT INTO [sogip_userroles] ([UserId],[RoleId])VALUES('2d9dcbb1-e611-43a5-928e-ecfa9e057aee','5');
INSERT INTO [sogip_userroles] ([UserId],[RoleId])VALUES('4c219de8-b076-4778-84df-bc76e46c138a','5');
INSERT INTO [sogip_userroles] ([UserId],[RoleId])VALUES('50294ee3-4b38-49ff-8b02-38aacbffb129','5');
INSERT INTO [sogip_userroles] ([UserId],[RoleId])VALUES('516c294e-14c2-4bfd-887a-59e763956a54','5');
INSERT INTO [sogip_userroles] ([UserId],[RoleId])VALUES('70822878-4c81-4972-8208-e1ad12d0d7cf','5');
INSERT INTO [sogip_userroles] ([UserId],[RoleId])VALUES('774079d4-2a34-4bfe-8c74-1fe3a3df5c45','5');
INSERT INTO [sogip_userroles] ([UserId],[RoleId])VALUES('b76a265d-2b9d-4aac-963f-0cc50595b639','5');
INSERT INTO [sogip_userroles] ([UserId],[RoleId])VALUES('d449b200-fdf7-4c3a-ac1d-07bd7e496137','5');
INSERT INTO [sogip_userroles] ([UserId],[RoleId])VALUES('7440f3c4-3528-4606-9bc8-501ad8f15b51','5');
INSERT INTO [sogip_userroles] ([UserId],[RoleId])VALUES('377e1527-58bb-40dc-a873-f88d4a1c1fcf','5');
INSERT INTO [sogip_userroles] ([UserId],[RoleId])VALUES('137feecf-2c48-4e86-8b79-5906b0057c70', 5);
INSERT INTO [sogip_userroles] ([UserId],[RoleId])VALUES('1dd72422-a344-407d-aec6-783907e73164','6');
INSERT INTO [sogip_userroles] ([UserId],[RoleId])VALUES('914db4cb-8e02-4476-9e42-31befefd7a0e','6');
INSERT INTO [sogip_userroles] ([UserId],[RoleId])VALUES('9d9d279f-016a-47e9-bf70-9ed4a4754de5','6');
INSERT INTO [sogip_userroles] ([UserId],[RoleId])VALUES('de8b4ac7-40a3-4b23-aa2b-28ae9fcb9253','6');

INSERT INTO [sogip_userroles] ([UserId],[RoleId])VALUES('079f4abe-80b2-43c0-9566-640869ea266a','7');
INSERT INTO [sogip_userroles] ([UserId],[RoleId])VALUES('09d9e8a1-14e1-43bd-8bc2-75b52378757d','7');
INSERT INTO [sogip_userroles] ([UserId],[RoleId])VALUES('0c3fe9ea-2e80-4d7b-b8d2-1158f6c1b824','7');
INSERT INTO [sogip_userroles] ([UserId],[RoleId])VALUES('5c48e11d-0abc-4f76-88a5-bc49e83af272','7');
INSERT INTO [sogip_userroles] ([UserId],[RoleId])VALUES('6642f3e1-dc3f-43df-9be2-ebff4ee67d56','7');
INSERT INTO [sogip_userroles] ([UserId],[RoleId])VALUES('79a605e5-2900-44bf-9b35-eaf0b419bd17','7');
INSERT INTO [sogip_userroles] ([UserId],[RoleId])VALUES('843f21d4-b301-4847-848b-f4cd20827a6a','7');
INSERT INTO [sogip_userroles] ([UserId],[RoleId])VALUES('c6d47fdc-c219-4d7f-b613-0aca0c812a29','7');
INSERT INTO [sogip_userroles] ([UserId],[RoleId])VALUES('cbbe0afc-dbf4-46b3-ba5f-91ca8531decd','7');
INSERT INTO [sogip_userroles] ([UserId],[RoleId])VALUES('fbb7a9fe-9975-4f17-bff2-d760d267a942','7');
INSERT INTO [sogip_userroles] ([UserId],[RoleId])VALUES('b3bf343d-4526-41ac-ba13-4c7212d4c5b3','7');


--INSERTS FUNCIONARIOS
SET IDENTITY_INSERT sogip_funcionario_icoder ON
INSERT INTO [sogip_funcionario_icoder] ([Funcionario_ICODERId],[Entrenador_Id],[Usuario_Id])VALUES(1,'b889fe47-541d-4453-8733-022709c1592f','5c48e11d-0abc-4f76-88a5-bc49e83af272');
INSERT INTO [sogip_funcionario_icoder] ([Funcionario_ICODERId],[Entrenador_Id],[Usuario_Id])VALUES(2,'b889fe47-541d-4453-8733-022709c1592f','c6d47fdc-c219-4d7f-b613-0aca0c812a29');
INSERT INTO [sogip_funcionario_icoder] ([Funcionario_ICODERId],[Entrenador_Id],[Usuario_Id])VALUES(3,'b889fe47-541d-4453-8733-022709c1592f','079f4abe-80b2-43c0-9566-640869ea266a');
INSERT INTO [sogip_funcionario_icoder] ([Funcionario_ICODERId],[Entrenador_Id],[Usuario_Id])VALUES(4,'b889fe47-541d-4453-8733-022709c1592f','0c3fe9ea-2e80-4d7b-b8d2-1158f6c1b824');
INSERT INTO [sogip_funcionario_icoder] ([Funcionario_ICODERId],[Entrenador_Id],[Usuario_Id])VALUES(5,'b889fe47-541d-4453-8733-022709c1592f','cbbe0afc-dbf4-46b3-ba5f-91ca8531decd');
INSERT INTO [sogip_funcionario_icoder] ([Funcionario_ICODERId],[Entrenador_Id],[Usuario_Id])VALUES(6,'b889fe47-541d-4453-8733-022709c1592f','6642f3e1-dc3f-43df-9be2-ebff4ee67d56');
INSERT INTO [sogip_funcionario_icoder] ([Funcionario_ICODERId],[Entrenador_Id],[Usuario_Id])VALUES(7,'b889fe47-541d-4453-8733-022709c1592f','fbb7a9fe-9975-4f17-bff2-d760d267a942');
INSERT INTO [sogip_funcionario_icoder] ([Funcionario_ICODERId],[Entrenador_Id],[Usuario_Id])VALUES(8,'b889fe47-541d-4453-8733-022709c1592f','09d9e8a1-14e1-43bd-8bc2-75b52378757d');
INSERT INTO [sogip_funcionario_icoder] ([Funcionario_ICODERId],[Entrenador_Id],[Usuario_Id])VALUES(9,'b889fe47-541d-4453-8733-022709c1592f','843f21d4-b301-4847-848b-f4cd20827a6a');
INSERT INTO [sogip_funcionario_icoder] ([Funcionario_ICODERId],[Entrenador_Id],[Usuario_Id])VALUES(10,'b889fe47-541d-4453-8733-022709c1592f','79a605e5-2900-44bf-9b35-eaf0b419bd17');
INSERT INTO [sogip_funcionario_icoder] ([Funcionario_ICODERId],[Entrenador_Id],[Usuario_Id])VALUES(11,'b889fe47-541d-4453-8733-022709c1592f','b3bf343d-4526-41ac-ba13-4c7212d4c5b3');
SET IDENTITY_INSERT sogip_funcionario_icoder OFF

--INSERTS ENTRENADORES
SET IDENTITY_INSERT sogip_entrenadores ON
INSERT INTO [sogip_entrenadores] ([EntrenadorId],[Usuario_Id])VALUES(1,'3df14eaa-e638-4f47-8d03-197dde644d56');
INSERT INTO [sogip_entrenadores] ([EntrenadorId],[Usuario_Id])VALUES(4,'43002ea0-b75c-44a0-8f29-7d5c0eeffed2');
INSERT INTO [sogip_entrenadores] ([EntrenadorId],[Usuario_Id])VALUES(2,'7fcacfe5-9e6e-4b89-9eaa-4a608eefb3bd');
INSERT INTO [sogip_entrenadores] ([EntrenadorId],[Usuario_Id])VALUES(3,'95c12f5c-7b9c-4a65-abd5-1dcdc604f847');
SET IDENTITY_INSERT sogip_entrenadores OFF

--INSERTS SELECCIONES
SET IDENTITY_INSERT sogip_selecciones ON
INSERT INTO [sogip_selecciones] ([SeleccionId],[Nombre_Seleccion],[Categoria_Id_CategoriaId],[Deporte_Id_DeporteId],[Entrenador_Id_EntrenadorId],[Usuario_Id])VALUES(1,'FEDERACIÓN COSTARRICENSE DE ATLETISMO',1,1,4,'8ed376ac-b906-4ead-b3f7-2bd405fb00a4');
INSERT INTO [sogip_selecciones] ([SeleccionId],[Nombre_Seleccion],[Categoria_Id_CategoriaId],[Deporte_Id_DeporteId],[Entrenador_Id_EntrenadorId],[Usuario_Id])VALUES(2,'FEDERACIÓN COSTARRICENSE DE JUDO',1,1,1,'71cb667a-9ab0-41bb-aad6-33ef288a15eb');
INSERT INTO [sogip_selecciones] ([SeleccionId],[Nombre_Seleccion],[Categoria_Id_CategoriaId],[Deporte_Id_DeporteId],[Entrenador_Id_EntrenadorId],[Usuario_Id])VALUES(3,'FEDERACIÓN COSTARRICENSE DE VOLEIBOL',1,1,2,'f67e2727-0e64-449d-8186-8ee3656a41ba');
INSERT INTO [sogip_selecciones] ([SeleccionId],[Nombre_Seleccion],[Categoria_Id_CategoriaId],[Deporte_Id_DeporteId],[Entrenador_Id_EntrenadorId],[Usuario_Id])VALUES(4,'FEDERACIÓN COSTARRICENSE DE TAEKWONDO',1,1,NULL,'6a86769d-4fbf-4e78-b2bb-8c2524c6a968');
INSERT INTO [sogip_selecciones] ([SeleccionId],[Nombre_Seleccion],[Categoria_Id_CategoriaId],[Deporte_Id_DeporteId],[Entrenador_Id_EntrenadorId],[Usuario_Id])VALUES(5,'FEDERACIÓN COSTARRICENSE DE RUGBY',1,1,3,'2f108dd4-69ef-42b2-9f70-fb4f7d7714f9');
INSERT INTO [sogip_selecciones] ([SeleccionId],[Nombre_Seleccion],[Categoria_Id_CategoriaId],[Deporte_Id_DeporteId],[Entrenador_Id_EntrenadorId],[Usuario_Id])VALUES(6,'FEDERACIÓN COSTARRICENSE DE NATACIÓN',1,1,NULL,'f0d05b48-9097-4d97-abc8-5ffdebe81d4c');
INSERT INTO [sogip_selecciones] ([SeleccionId],[Nombre_Seleccion],[Categoria_Id_CategoriaId],[Deporte_Id_DeporteId],[Entrenador_Id_EntrenadorId],[Usuario_Id])VALUES(7,'FEDERACIÓN COSTARRICENSE DE ESGRIMA',1,1,NULL,'40cb3962-3c2f-4f64-ab03-dba913a03010');
INSERT INTO [sogip_selecciones] ([SeleccionId],[Nombre_Seleccion],[Categoria_Id_CategoriaId],[Deporte_Id_DeporteId],[Entrenador_Id_EntrenadorId],[Usuario_Id])VALUES(8,'FEDERACIÓN COSTARRICENSE DE BOXEO',1,1,NULL,'77e4861f-9ad0-45c1-929e-464061866233');
INSERT INTO [sogip_selecciones] ([SeleccionId],[Nombre_Seleccion],[Categoria_Id_CategoriaId],[Deporte_Id_DeporteId],[Entrenador_Id_EntrenadorId],[Usuario_Id])VALUES(9,'FEDERACIÓN COSTARRICENSE DE BALONMANO',1,1,NULL,'0b925665-1c93-42d2-80b0-29c6423daf66');
INSERT INTO [sogip_selecciones] ([SeleccionId],[Nombre_Seleccion],[Categoria_Id_CategoriaId],[Deporte_Id_DeporteId],[Entrenador_Id_EntrenadorId],[Usuario_Id])VALUES(10,'FEDERACIÓN COSTARRICENSE DE CICLISMO',1,1,NULL,'51653da9-0be5-4cbc-a3d6-1297b6d776a1');
SET IDENTITY_INSERT sogip_selecciones OFF


--INSERTS ALETAS
SET IDENTITY_INSERT sogip_atletas ON
INSERT INTO [sogip_atletas] ([AtletaId],[Localidad],[Asociacion_Deportiva_Asociacion_DeportivaId],[Seleccion_SeleccionId],[Usuario_Id])VALUES(1,'',NULL,1,'50294ee3-4b38-49ff-8b02-38aacbffb129');
INSERT INTO [sogip_atletas] ([AtletaId],[Localidad],[Asociacion_Deportiva_Asociacion_DeportivaId],[Seleccion_SeleccionId],[Usuario_Id])VALUES(2,'',NULL,1,'b76a265d-2b9d-4aac-963f-0cc50595b639');
INSERT INTO [sogip_atletas] ([AtletaId],[Localidad],[Asociacion_Deportiva_Asociacion_DeportivaId],[Seleccion_SeleccionId],[Usuario_Id])VALUES(3,'',NULL,2,'4c219de8-b076-4778-84df-bc76e46c138a');
INSERT INTO [sogip_atletas] ([AtletaId],[Localidad],[Asociacion_Deportiva_Asociacion_DeportivaId],[Seleccion_SeleccionId],[Usuario_Id])VALUES(4,'',NULL,5,'18858306-7098-4657-8f5f-56027fac7952');
INSERT INTO [sogip_atletas] ([AtletaId],[Localidad],[Asociacion_Deportiva_Asociacion_DeportivaId],[Seleccion_SeleccionId],[Usuario_Id])VALUES(5,'',NULL,3,'d449b200-fdf7-4c3a-ac1d-07bd7e496137');
INSERT INTO [sogip_atletas] ([AtletaId],[Localidad],[Asociacion_Deportiva_Asociacion_DeportivaId],[Seleccion_SeleccionId],[Usuario_Id])VALUES(6,'',NULL,6,'70822878-4c81-4972-8208-e1ad12d0d7cf');
INSERT INTO [sogip_atletas] ([AtletaId],[Localidad],[Asociacion_Deportiva_Asociacion_DeportivaId],[Seleccion_SeleccionId],[Usuario_Id])VALUES(7,'',NULL,9,'2d9dcbb1-e611-43a5-928e-ecfa9e057aee');
INSERT INTO [sogip_atletas] ([AtletaId],[Localidad],[Asociacion_Deportiva_Asociacion_DeportivaId],[Seleccion_SeleccionId],[Usuario_Id])VALUES(8,'',NULL,8,'1dd72422-a344-407d-aec6-783907e73164');
INSERT INTO [sogip_atletas] ([AtletaId],[Localidad],[Asociacion_Deportiva_Asociacion_DeportivaId],[Seleccion_SeleccionId],[Usuario_Id])VALUES(9,'',NULL,6,'516c294e-14c2-4bfd-887a-59e763956a54');
INSERT INTO [sogip_atletas] ([AtletaId],[Localidad],[Asociacion_Deportiva_Asociacion_DeportivaId],[Seleccion_SeleccionId],[Usuario_Id])VALUES(10,'',NULL,6,'774079d4-2a34-4bfe-8c74-1fe3a3df5c45');
SET IDENTITY_INSERT sogip_atletas OFF

--INSERTS RUTINAS
insert into SOGIP_Rutina values('2018-10-09 00:00:00.000', 'BAJAR PESO, AUMENTAR MASA MUSCULAR', '079f4abe-80b2-43c0-9566-640869ea266a');
insert into SOGIP_Rutina values('2018-10-09 00:00:00.000', 'BAJAR PORCENTAJE DE GRASA', '5c48e11d-0abc-4f76-88a5-bc49e83af272');
insert into SOGIP_Rutina values('2018-10-09 00:00:00.000', 'DESCANSO 1 MIN DESPUES DE TERMINAR LA SERIE', 'c6d47fdc-c219-4d7f-b613-0aca0c812a29');
insert into SOGIP_Rutina values('2018-11-01 00:00:00.000', 'SUBIR MÚSCULOS', '377e1527-58bb-40dc-a873-f88d4a1c1fcf');
insert into SOGIP_Rutina values('2018-11-01 00:00:00.000', 'DESCANSO 1 MINUTO', 'b3bf343d-4526-41ac-ba13-4c7212d4c5b3');
insert into SOGIP_Rutina values('2018-11-06 00:00:00.000', NULL, '137feecf-2c48-4e86-8b79-5906b0057c70');

-- RUTINA #1
insert into SOGIP_Conjunto_Ejercicio values('APERTURAS MAQ INDIVIDUAL', 3, 10, 60, 3, 12, 60, 3, 12, 70, 'chartreuse', 'Dia1', 1);
insert into SOGIP_Conjunto_Ejercicio values('PULL DOWN S MAQUINA INDIVIDUAL', 3, 10, 90, 3, 12, 90, 3, 90, 100, 'chartreuse', 'Dia1', 1);
insert into SOGIP_Conjunto_Ejercicio values('RODILLA A CODO OPUESTO', 3, 20, 0, 3, 25, 0, 3, 30, 0, 'orange', 'Dia1', 1);
insert into SOGIP_Conjunto_Ejercicio values('SENTADILLA CON MANCUERDA', 3, 10, 15, 3, 12, 15, 3, 12, 20, 'deeppink', 'Dia1', 1);
insert into SOGIP_Conjunto_Ejercicio values('SENTADILLAS+SALTO CAJON', 3, 10, 0, 3, 12, 0, 3, 12, 0, 'deeppink', 'Dia1', 1);
insert into SOGIP_Conjunto_Ejercicio values('ABDOMINALES', 3, 25, 0, 3, 27, 0, 3, 30, 0, 'aqua', 'Dia1', 1);
insert into SOGIP_Conjunto_Ejercicio values('LUMBARES', 3, 20, 0, 3, 25, 0, 3, 30, 0, 'aqua', 'Dia1', 1);
-- RUTINA #2
insert into SOGIP_Conjunto_Ejercicio values('PRESS PECHO PLANO', 3, 10, 0, 3, 12, 0, 3, 12, 0, 'chartreuse', 'Dia1', 2);
insert into SOGIP_Conjunto_Ejercicio values('EXTENSION RODILLA', 3, 10, 0, 3, 12, 0, 3, 12, 0, 'chartreuse',  'Dia1', 2);
insert into SOGIP_Conjunto_Ejercicio values('REMO INDIVIDUAL POLEA', 3, 10, 0, 3, 12, 0, 3, 12, 0, 'deeppink','Dia1', 2);
insert into SOGIP_Conjunto_Ejercicio values('PULL DOWN', 3, 10, 0, 3, 12, 0, 3, 12, 0, 'chartreuse', 'Dia2', 2);
insert into SOGIP_Conjunto_Ejercicio values('FLEX CODO', 3, 10, 0, 3, 12, 0, 3, 12, 0, 'yellow', 'Dia2', 2);
insert into SOGIP_Conjunto_Ejercicio values('ELEVACION FRONTAL HOMBROS', 3, 10, 0, 3, 12, 0, 3, 12, 0, 'orange', 'Dia2', 2);
-- RUTINA #3
insert into SOGIP_Conjunto_Ejercicio values('PRESS PECHO', 3, 12, 0, 3, 12, 0, 3, 12, 0, 'orange', 'Dia1', 3);
insert into SOGIP_Conjunto_Ejercicio values('APERTURAS CON POLEA', 3, 12, 0, 3, 12, 0, 3, 12, 0, 'orange', 'Dia1', 3);
insert into SOGIP_Conjunto_Ejercicio values('REMO MANCUERDA', 3, 12, 0, 3, 12, 0, 3, 12, 0, 'chartreuse', 'Dia1', 3);
insert into SOGIP_Conjunto_Ejercicio values('EXTENSION', 3, 12, 0, 3, 12, 0, 3, 12, 0, 'aqua', 'Dia1', 3);
insert into SOGIP_Conjunto_Ejercicio values('EXTENSION RODILLA', 3, 8, 0, 3, 8, 0, 3, 8, 0, 'mediumpurple', 'Dia2', 3);
insert into SOGIP_Conjunto_Ejercicio values('PESO MUERTO', 3, 12, 0, 3, 12, 0, 3, 12, 0, 'chartreuse', 'Dia2', 3);
insert into SOGIP_Conjunto_Ejercicio values('PANTORRILLA', 3, 8, 0, 3, 12, 0, 3, 12, 0, 'deeppink', 'Dia2', 3);
insert into SOGIP_Conjunto_Ejercicio values('LUMBARES', 3, 20, 0, 3, 23, 0, 3, 25, 0, 'orange', 'Dia2', 3);
 -- RUTINA #5
insert into SOGIP_Conjunto_Ejercicio values('APERTURAS PECHO CON POLEA', 4, 12, NULL, 4, '+2', '=', 4, '=', '+', 'chartreuse', 'Dia1', 5);
insert into SOGIP_Conjunto_Ejercicio values('REMO CON BARRA Z CERRADO', 4, 12, NULL, 4, '+2', '=', 4, '=', '+', 'chartreuse', 'Dia1', 5);
insert into SOGIP_Conjunto_Ejercicio values('2 MIN SKATER HOP', 1, '-', '-', 1, '-', '-', 1, '=', '+', 'aqua', 'Dia1', 5);
insert into SOGIP_Conjunto_Ejercicio values('SENTADILLA + SALTO + VERTICAL + DESPLANTE', 4, 12, NULL, 4, '+2', '=', 4, '=', '+', 'deeppink', 'Dia1', 5);
insert into SOGIP_Conjunto_Ejercicio values('PISTOL CON MANCUERDA EN ARGOLLAS', 4, 12, NULL, 4, '+2', '=', 4, '=', '+', 'deeppink', 'Dia1', 5);
insert into SOGIP_Conjunto_Ejercicio values('SQUAT CLEAN CON MANCUERDA', 4, 12, NULL, 4, '+2', '=', 4, '=', '=', 'orange', 'Dia1', 5);
insert into SOGIP_Conjunto_Ejercicio values('ABDUCTORES', 4, 12, NULL, 4, '+2', '=', 4, '=', '=', 'orange', 'Dia1', 5);
-- RUTINA #6
insert into SOGIP_Conjunto_Ejercicio values('PRESS BANCA', 3, 15, 220, NULL, NULL, NULL, NULL, NULL, NULL, 'orange', 'Dia1', 6);
insert into SOGIP_Conjunto_Ejercicio values('PECK DECK', 3, 15, 180, NULL, NULL, NULL, NULL, NULL, NULL, 'white', 'Dia1', 6);
insert into SOGIP_Conjunto_Ejercicio values('PRESS INCLINADO', 3, 15, 90, NULL, NULL, NULL, NULL, NULL, NULL, 'white', 'Dia1', 6);
insert into SOGIP_Conjunto_Ejercicio values('TRICEPS FRANCES', 3, 15, 50, NULL, NULL, NULL, NULL, NULL, NULL, 'white', 'Dia1', 6);
insert into SOGIP_Conjunto_Ejercicio values('TRICEPS POLEA', 3, 15, 80, NULL, NULL, NULL, NULL, NULL, NULL, 'white', 'Dia1', 6);

--INSERTS CITAS
insert into SOGIP_Cita values(1, 0, '603290698', 'YENCI', 'GONZALEZ', 'RUIZ', '2018-10-24 08:00:00.000', '2018-10-24 08:20:00.000', '0c3fe9ea-2e80-4d7b-b8d2-1158f6c1b824');
insert into SOGIP_Cita values(1, 1, '115280606', 'ELIZABETH', 'CESPEDES', 'VIQUEZ', '2018-10-24 09:00:00.000', '2018-10-24 10:50:00.000', 'cbbe0afc-dbf4-46b3-ba5f-91ca8531decd');
insert into SOGIP_Cita values(0, 1, '104730812', 'ARCADIO', 'QUESADA', 'BARRANTES', '2018-10-25 13:00:00.000', '2018-10-25 14:30:00.000', '6642f3e1-dc3f-43df-9be2-ebff4ee67d56');
insert into SOGIP_Cita values(1, 0, '104730812', 'ARCADIO', 'QUESADA', 'BARRANTES', '2018-10-24 13:00:00.000', '2018-10-24 13:20:00.000', '6642f3e1-dc3f-43df-9be2-ebff4ee67d56');
insert into SOGIP_Cita values(1, 1, '205940271', 'JUAN', 'ARCE', 'VIQUEZ', '2018-10-26 08:00:00.000', '2018-10-26 09:50:00.000', 'c6d47fdc-c219-4d7f-b613-0aca0c812a29');
insert into SOGIP_Cita values(0, 1, '111470524', 'MANUEL', 'GUZMAN', 'SABORIO', '2018-11-06 13:00:00.000', '2018-11-06 14:30:00.000', 'fbb7a9fe-9975-4f17-bff2-d760d267a942');
insert into SOGIP_Cita values(1, 0, '111470524', 'MANUEL', 'GUZMAN', 'SABORIO', '2018-10-26 10:00:00.000', '2018-10-26 10:20:00.000', 'fbb7a9fe-9975-4f17-bff2-d760d267a942');
insert into SOGIP_Cita values(1, 0, '104670428', 'ILEANA', 'MADRIGAL', 'CECILIANO', '2018-11-06 16:30:00.000', '2018-11-06 16:50:00.000', '09d9e8a1-14e1-43bd-8bc2-75b52378757d');
insert into SOGIP_Cita values(0, 1, '104670428', 'ILEANA', 'MADRIGAL', 'CECILIANO', '2018-11-08 08:00:00.000', '2018-11-08 09:30:00.000', '09d9e8a1-14e1-43bd-8bc2-75b52378757d');
insert into SOGIP_Cita values(1, 1, '603290698', 'YENCI', 'GONZALEZ', 'RUIZ', '2018-11-19 18:00:00.000', '2018-11-19 19:50:00.000', '0c3fe9ea-2e80-4d7b-b8d2-1158f6c1b824');
insert into SOGIP_Cita values(0, 1, '603290698', 'YENCI', 'GONZALEZ', 'RUIZ', '2018-11-20 18:00:00.000', '2018-11-20 19:30:00.000', '0c3fe9ea-2e80-4d7b-b8d2-1158f6c1b824');
insert into SOGIP_Cita values(1, 0, '206140354', 'FELIPE', 'BARRANTES', 'MADRIZ', '2018-10-29 06:00:00.000', '2018-10-29 06:20:00.000', '843f21d4-b301-4847-848b-f4cd20827a6a');
insert into SOGIP_Cita values(0, 1, '206140354', 'FELIPE', 'BARRANTES', 'MADRIZ', '2018-10-30 06:00:00.000', '2018-10-30 07:30:00.000', '843f21d4-b301-4847-848b-f4cd20827a6a');
insert into SOGIP_Cita values(1, 0, '206140354', 'FELIPE', 'BARRANTES', 'MADRIZ', '2018-11-29 06:00:00.000', '2018-11-29 06:20:00.000', '843f21d4-b301-4847-848b-f4cd20827a6a');
insert into SOGIP_Cita values(0, 1, '206140354', 'FELIPE', 'BARRANTES', 'MADRIZ', '2018-11-30 06:00:00.000', '2018-11-30 07:30:00.000', '843f21d4-b301-4847-848b-f4cd20827a6a');
insert into SOGIP_Cita values(1, 1, '104670428', 'ILEANA', 'MADRIGAL', 'CECILIANO', '2018-10-29 08:20:00.000', '2018-10-29 10:10:00.000', '09d9e8a1-14e1-43bd-8bc2-75b52378757d');
insert into SOGIP_Cita values(1, 0, '302070270', 'ALEJANDRA', 'VALVERDE', 'BRENES', '2018-10-29 17:00:00.000', '2018-10-29 17:20:00.000', '79a605e5-2900-44bf-9b35-eaf0b419bd17');
insert into SOGIP_Cita values(1, 1, '117709069', 'PETER', 'JARA', 'GARCIA', '2018-11-23 16:00:00.000', '2018-11-23 17:50:00.000', '7440f3c4-3528-4606-9bc8-501ad8f15b51');

-- ++++++++++++++++++++++++++ Insert's ++++++++++++++++++++++++++