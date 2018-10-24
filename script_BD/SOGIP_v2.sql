/*

+++++++ RECONSTRUIR BASE DE DATOS CON LOS CAMBIOS RESPECTIVOS ++++++++++

use master;
drop database "SOGIP_v3"

create database "SOGIP_v3"
use "SOGIP_v3"

+++++++ RECONSTRUIR BASE DE DATOS CON LOS CAMBIOS RESPECTIVOS ++++++++++

 ++++++++++++++++++++++++++++ Select's ++++++++++++++++++++++++++++


 select SOGIP_Users.Id, SOGIP_Users.Nombre1, SOGIP_Users.Fecha_Nacimiento, SOGIP_Users.Cedula, SOGIP_Roles.Name, SOGIP_Roles.id
 from 
 SOGIP_Users, SOGIP_UserRoles, SOGIP_Roles
 where 
 SOGIP_Users.Id = SOGIP_UserRoles.Userid
 and
 SOGIP_UserRoles.Roleid = SOGIP_Roles.id
 order by SOGIP_Roles.id;

 delete from SOGIP_Users where Email != 'cmb28@hotmail.com';
 delete from SOGIP_Users;

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
 select * from SOGIP_UserRoles;
 select * from SOGIP_Users;

 -- Describe los atributos de cualquier tabla. --

		sp_help SOGIP_Users; 
		sp_help SOGIP_Entrenadores;

insert into SOGIP_Users values('b889fe47-541d-4453-8733-022709c1592f', '114070986', NULL, '2019-01-18 15:52:12.557', 'Josafat', 'Antonio', 'Barboza', 'Umaña', '1989-10-06 00:00:00.000', 0, 1, 'josa@josa.com', 0, 'AAAAGb+hoJnZqkFtRi3SDPAQqwviIyybQISr4nF99nLf9cEoygh0rzKIUm2Dx1qYyQ==', 'de3fb782-d243-4a25-8106-623ebfa68fc7', NULL, 0, 0, NULL, 1, 0, '114070986');


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

insert into SOGIP_Estados values('Inactivo');
insert into SOGIP_Estados values('Activo');
insert into SOGIP_Estados values('Finalizado');
insert into SOGIP_Estados values('En Proceso');

insert into SOGIP_Categorias values('Juvenil');
insert into SOGIP_Categorias values('Mayor');
insert into SOGIP_Categorias values('SUB 20');
insert into SOGIP_Categorias values('Nacional');

insert into SOGIP_Tipo_Deporte values('Individual');
insert into SOGIP_Tipo_Deporte values('De conjunto');
insert into SOGIP_Tipo_Deporte values('De tiempo y marca');
insert into SOGIP_Tipo_Deporte values('De combate');
insert into SOGIP_Tipo_Deporte values('De raqueta');
insert into SOGIP_Tipo_Deporte values('De precision');

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

insert into SOGIP_Users values('43002ea0-b75c-44a0-8f29-7d5c0eeffed2', '112530907', NULL, '2019-01-18 16:32:10.283', 'Emmanuel', 'David', 'Chanto', 'Segura', '1985-08-25 00:00:00.000', 0, 1, 'chanto@chanto.com', 0, 'AOP0cWhiVc+Eot6ruccEtMvhqwrSt6K6bXixvSTtOryoGNt8TP8k7/ETtBrPjlgACA==', 'f24f4272-ceeb-4f08-bf54-611817c49847', NULL, 0, 0, NULL, 1, 0, '112530907');
insert into SOGIP_Users values('914db4cb-8e02-4476-9e42-31befefd7a0e', '402120310', NULL, '2019-01-18 17:08:56.407', 'Ana', 'Maria', 'Porras', 'Loria', '1991-09-21 00:00:00.000', 0, 1, 'anaporras@anaporras.com', 0, 'AMJoytp/8v+RwnlaWrtK4MDwxUGWwBdJ+/XNbpQbtRo0bC2eTf3uMs5rX0iHsus1ng==', 'e4a8b6c5-eb52-4c07-aeae-48216b8b286e', NULL, 0, 0, NULL, 1, 0, '402120310');
insert into SOGIP_Users values('9d9d279f-016a-47e9-bf70-9ed4a4754de5', '110830174', NULL, '2019-01-18 17:07:06.053', 'Aaron', 'Andres', 'Hidalgo', 'Nuñez', '1980-02-10 00:00:00.000', 0, 1, 'aaron@aaron.com', 0, 'AI33t6s1zh79xQNvhIZu+a3ew6AP+qhorBRdp0Q8xFn6bhhU4cemPKEE8t334VC2aA==', 'b8589e3c-e75c-47a0-9dfc-5e56ea5582eb', NULL, 0, 0, NULL, 1, 0, '110830174');
insert into SOGIP_Users values('b889fe47-541d-4453-8733-022709c1592f', '114070986', NULL, '2019-01-18 15:52:12.557', 'Josafat', 'Antonio', 'Barboza', 'Umaña', '1989-10-06 00:00:00.000', 0, 1, 'josa@josa.com', 0, 'AAAAGb+hoJnZqkFtRi3SDPAQqwviIyybQISr4nF99nLf9cEoygh0rzKIUm2Dx1qYyQ==', 'de3fb782-d243-4a25-8106-623ebfa68fc7', NULL, 0, 0, NULL, 1, 0, '114070986');
insert into SOGIP_Users values('de8b4ac7-40a3-4b23-aa2b-28ae9fcb9253', '402260033', NULL, '2019-01-18 17:13:38.123', 'Emmanuel', NULL, 'Niño', 'Villalta', '1995-02-14 00:00:00.000', 0, 1, 'emmanuelnino@emmanuelnino.com', 0, 'AArLIlxJ6VcgWrGQRqlVf8jM1UA7+WoOndCCiMZ3D6bJJQ2KI8MiBLmttJCQSL/3Hg==', '93511ecb-cb2b-4a6c-8191-81dcfdc47ced', NULL, 0, 0, NULL, 1, 0, '402260033');
insert into SOGIP_Users values('8f9c47bf-edbd-40bf-9b5e-f753dd81a766', '000000000', NULL, '2019-01-19 21:34:58.363', 'Sistema', 'Operativo', 'Gimnasio', 'Pesas', '2000-01-01 00:00:00.000', 0, 1, 'admSOGIP@hotmail.com', 0, 'AG33oMVrb494bn6JdvpIGf2UV1wbopV1ttoNqJP9/LOD8S0PPltbD4XUYzMorUX8mA==', '6508b809-9b26-4f7e-aad6-b550cbe32fa6', NULL, 0, 0, NULL, 1, 0, '000000000');

insert into SOGIP_UserRoles values('8f9c47bf-edbd-40bf-9b5e-f753dd81a766', 1);
insert into SOGIP_UserRoles values('b889fe47-541d-4453-8733-022709c1592f', 2);
insert into SOGIP_UserRoles values('43002ea0-b75c-44a0-8f29-7d5c0eeffed2', 3);
insert into SOGIP_UserRoles values('de8b4ac7-40a3-4b23-aa2b-28ae9fcb9253', 6);


-- ++++++++++++++++++++++++++ Insert's ++++++++++++++++++++++++++