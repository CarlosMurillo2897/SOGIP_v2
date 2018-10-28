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

-- update SOGIP_Users set Sexo = 1 where Cedula='110280141';

insert into SOGIP_Users values('079f4abe-80b2-43c0-9566-640869ea266a', '110280141', NULL, '2019-04-20 13:42:39.747', 'ALONSO', NULL, 'LEON', 'MENA', '1979-02-11 00:00:00.000', 1, 1, 'alonso.leon@icoder.co.cr', 0, 'AEuLAa53HkMBZx7hobzIRLRXS39d88xPS7n6xrsz5cHxjL/TG9UG+FCkyM0kpQ8mLg==', 'aa379c05-9fe6-4f9f-b0b8-11520a87d019', NULL, 0, 0, NULL, 1, 0, '110280141');
insert into SOGIP_Users values('09d9e8a1-14e1-43bd-8bc2-75b52378757d', '104670428', NULL, '2019-01-22 16:02:16.563', 'ILEANA', NULL, 'MADRIGAL', 'CECILIANO', '1956-07-18 00:00:00.000', 0, 1, 'ileana.madrigal@icoder.go.cr', 0, 'AC8f7m9SJVC5Hp2EpGB+6RvhzQFyW1HDAHMNtNwFzb9q5SPPst6T6pNXnjS9L3HaKg==', '5535d562-81b7-42a6-9fb4-83e77a4b5e2f', NULL, 0, 0, NULL, 1, 0, '104670428');
insert into SOGIP_Users values('0c3fe9ea-2e80-4d7b-b8d2-1158f6c1b824', '603290698', NULL, '2019-01-22 15:58:20.630', 'YENCI', NULL, 'GONZALEZ', 'RUIZ', '1983-06-04 00:00:00.000', 0, 1, 'yenci.gonzalez@icoder.co.cr', 0, 'ADz1OynzfyQk7xOpalS/Usus/H+jG1ilTFPPbpWOL01+UFKgCPGMK+C5J/hK68OnUw==', 'ba3deaaa-ba69-4a54-9369-8bde2cd1166e', NULL, 0, 0, NULL, 1, 0, '603290698');
insert into SOGIP_Users values('43002ea0-b75c-44a0-8f29-7d5c0eeffed2', '112530907', NULL, '2019-04-18 16:32:10.283', 'EMMANUEL', 'DAVID', 'CHANTO', 'SEGURA', '1985-08-25 00:00:00.000', 1, 1, 'chanto@chanto.com', 0, 'AOP0cWhiVc+Eot6ruccEtMvhqwrSt6K6bXixvSTtOryoGNt8TP8k7/ETtBrPjlgACA==', 'f24f4272-ceeb-4f08-bf54-611817c49847', NULL, 0, 0, NULL, 1, 0, '112530907');
insert into SOGIP_Users values('5c48e11d-0abc-4f76-88a5-bc49e83af272', '112600685', NULL, '2019-04-20 13:38:49.550', 'AGUSTIN', NULL, 'HERRERA', 'CORDERO', '1985-11-08 00:00:00.000', 1, 1, 'agustin.herrera@icoder.co.cr', 0, 'AHfDOYBGEabFBynUnXw+nQsQ+n6kTPX1mDeyZdObrVgourKOUmOEvtBoo619B3ALag==', 'b95dd422-32ac-4707-a4c1-b1b9952d2a6f', NULL, 0, 0, NULL, 1, 0, '112600685');
insert into SOGIP_Users values('6642f3e1-dc3f-43df-9be2-ebff4ee67d56', '104730812', NULL, '2019-01-22 16:00:18.750', 'ARCADIO', NULL, 'QUESADA', 'BARRANTES', '1956-07-21 00:00:00.000', 1, 1, 'arcadio.quesada@icoder.go.cr', 0, 'AOuwdjmySA/PV9y5wcsj0tMAWPkECptnCN2YWRxUFWT1x43RFw2v2NukoVLqFG9c/A==', 'ae5e643c-5830-4ffe-bb91-d1caeb551412', NULL, 0, 0, NULL, 1, 0, '104730812');
insert into SOGIP_Users values('79a605e5-2900-44bf-9b35-eaf0b419bd17', '302070270', NULL, '2019-01-22 16:04:59.927', 'ALEJANDRA', NULL, 'VALVERDE', 'BRENES', '1955-02-19 00:00:00.000', 0, 1, 'alejandra.valverde@icoder.go.cr', 0, 'AOBW3lc3N/8SxJ34xf0veqxxivER5SDuOPXB2YCU2csvA94Rnj8RgdQaIfAJmfnHZQ==', 'c635327d-8421-431d-bf0b-c10731f7919f', NULL, 0, 0, NULL, 1, 0, '302070270');
insert into SOGIP_Users values('843f21d4-b301-4847-848b-f4cd20827a6a', '206140354', NULL, '2019-01-22 16:04:20.163', 'FELIPE', NULL, 'BARRANTES', 'MADRIZ', '1956-11-28 00:00:00.000', 1, 1, 'felipe.barrantes@icoder.go.cr', 0, 'AD0ykwUv1mMXpk8O+5xyeXMNH4rdawPru+iAyDqFNoKMJZSoCRTIcHV2oq3apwFEnw==', '1920116b-64f5-4ab9-8ee7-b78986029ce7', NULL, 0, 0, NULL, 1, 0, '206140354');
insert into SOGIP_Users values('8f9c47bf-edbd-40bf-9b5e-f753dd81a766', '000000000', NULL, '2019-04-19 21:34:58.363', 'SISTEMA', 'OPERATIVO', 'GIMNASIO', 'PESAS', '2000-01-01 00:00:00.000', 0, 1, 'admSOGIP@hotmail.com', 0, 'AG33oMVrb494bn6JdvpIGf2UV1wbopV1ttoNqJP9/LOD8S0PPltbD4XUYzMorUX8mA==', '6508b809-9b26-4f7e-aad6-b550cbe32fa6', NULL, 0, 0, NULL, 1, 0, '000000000');
insert into SOGIP_Users values('914db4cb-8e02-4476-9e42-31befefd7a0e', '402120310', NULL, '2019-04-18 17:08:56.407', 'ANA', 'MARIA', 'PORRAS', 'LORIA', '1991-09-21 00:00:00.000', 0, 1, 'anaporras@anaporras.com', 0, 'AMJoytp/8v+RwnlaWrtK4MDwxUGWwBdJ+/XNbpQbtRo0bC2eTf3uMs5rX0iHsus1ng==', 'e4a8b6c5-eb52-4c07-aeae-48216b8b286e', NULL, 0, 0, NULL, 1, 0, '402120310');
insert into SOGIP_Users values('9d9d279f-016a-47e9-bf70-9ed4a4754de5', '110830174', NULL, '2019-04-18 17:07:06.053', 'AARON', 'ANDRES', 'HIDALGO', 'NUÑEZ', '1980-02-10 00:00:00.000', 1, 1, 'aaron@aaron.com', 0, 'AI33t6s1zh79xQNvhIZu+a3ew6AP+qhorBRdp0Q8xFn6bhhU4cemPKEE8t334VC2aA==', 'b8589e3c-e75c-47a0-9dfc-5e56ea5582eb', NULL, 0, 0, NULL, 1, 0, '110830174');
insert into SOGIP_Users values('b889fe47-541d-4453-8733-022709c1592f', '114070986', NULL, '2019-04-18 15:52:12.557', 'JOSAFAT', 'ANTONIO', 'BARBOZA', 'UMAÑA', '1989-10-06 00:00:00.000', 1, 1, 'josa@josa.com', 0, 'AAAAGb+hoJnZqkFtRi3SDPAQqwviIyybQISr4nF99nLf9cEoygh0rzKIUm2Dx1qYyQ==', 'de3fb782-d243-4a25-8106-623ebfa68fc7', NULL, 0, 0, NULL, 1, 0, '114070986');
insert into SOGIP_Users values('c6d47fdc-c219-4d7f-b613-0aca0c812a29', '205940271', NULL, '2019-04-20 13:40:26.030', 'JUAN', 'GABRIEL', 'ARCE', 'VIQUEZ', '1984-05-05 00:00:00.000', 1, 1, 'juangabriel.arce@icoder.co.cr', 0, 'AHWWx/H6t/zJVPpbdk115btZWrHz302G3/WQflJUxJzT0o9y9iy9Od1DPZXhfFzp+g==', '3c3063d7-2b23-4248-a070-32fa9d4ced96', NULL, 0, 0, NULL, 1, 0, '205940271');
insert into SOGIP_Users values('cbbe0afc-dbf4-46b3-ba5f-91ca8531decd', '115280606', NULL, '2019-01-22 15:59:35.137', 'ELIZABETH', NULL, 'CESPEDES', 'VIQUEZ', '1993-02-22 00:00:00.000', 0, 1, 'elizabeth.cespedes@icoder.go.cr', 0, 'AP6Ij6lHC9SYnAsen9j1DUsmbhoSie32VwozIHClTELLOL6XOumpltyGcqJSb4q8Zw==', 'c866e133-79d6-4663-b0e3-a2cd70b92670', NULL, 0, 0, NULL, 1, 0, '115280606');
insert into SOGIP_Users values('de8b4ac7-40a3-4b23-aa2b-28ae9fcb9253', '402260033', NULL, '2019-04-18 17:13:38.123', 'EMMANUEL', NULL, 'NIÑO', 'VILLALTA', '1995-02-14 00:00:00.000', 1, 1, 'emmanuelnino@emmanuelnino.com', 0, 'AArLIlxJ6VcgWrGQRqlVf8jM1UA7+WoOndCCiMZ3D6bJJQ2KI8MiBLmttJCQSL/3Hg==', '93511ecb-cb2b-4a6c-8191-81dcfdc47ced', NULL, 0, 0, NULL, 1, 0, '402260033');
insert into SOGIP_Users values('fbb7a9fe-9975-4f17-bff2-d760d267a942', '111470524', NULL, '2019-01-22 16:01:05.007', 'MANUEL', NULL, 'GUZMAN', 'SABORIO', '1982-08-25 00:00:00.000', 1, 1, 'manuel.guzman@icoder.go.cr', 0, 'AB1OFhuEBHK7opKoLmdAMWAMUwkCE7ILUQ+fYYQzayVfvJVOnFrBli6FpM1BxnXK7A==', 'cab480f8-b03d-47aa-9f90-e7c197325e5c', NULL, 0, 0, NULL, 1, 0, '111470524');

insert into SOGIP_UserRoles values('8f9c47bf-edbd-40bf-9b5e-f753dd81a766', 1);
insert into SOGIP_UserRoles values('b889fe47-541d-4453-8733-022709c1592f', 2);
insert into SOGIP_UserRoles values('43002ea0-b75c-44a0-8f29-7d5c0eeffed2', 3);
insert into SOGIP_UserRoles values('914db4cb-8e02-4476-9e42-31befefd7a0e', 6);
insert into SOGIP_UserRoles values('9d9d279f-016a-47e9-bf70-9ed4a4754de5', 6);
insert into SOGIP_UserRoles values('079f4abe-80b2-43c0-9566-640869ea266a', 7);
insert into SOGIP_UserRoles values('09d9e8a1-14e1-43bd-8bc2-75b52378757d', 7);
insert into SOGIP_UserRoles values('0c3fe9ea-2e80-4d7b-b8d2-1158f6c1b824', 7);
insert into SOGIP_UserRoles values('5c48e11d-0abc-4f76-88a5-bc49e83af272', 7);
insert into SOGIP_UserRoles values('6642f3e1-dc3f-43df-9be2-ebff4ee67d56', 7);
insert into SOGIP_UserRoles values('79a605e5-2900-44bf-9b35-eaf0b419bd17', 7);
insert into SOGIP_UserRoles values('843f21d4-b301-4847-848b-f4cd20827a6a', 7);
insert into SOGIP_UserRoles values('c6d47fdc-c219-4d7f-b613-0aca0c812a29', 7);
insert into SOGIP_UserRoles values('cbbe0afc-dbf4-46b3-ba5f-91ca8531decd', 7);
insert into SOGIP_UserRoles values('fbb7a9fe-9975-4f17-bff2-d760d267a942', 7);
-- Ana María Porras Loria no tiene rol.
-- Aaron Andres Hidalgo

insert into SOGIP_Funcionario_ICODER values('b889fe47-541d-4453-8733-022709c1592f', '5c48e11d-0abc-4f76-88a5-bc49e83af272');
insert into SOGIP_Funcionario_ICODER values('b889fe47-541d-4453-8733-022709c1592f', 'c6d47fdc-c219-4d7f-b613-0aca0c812a29');
insert into SOGIP_Funcionario_ICODER values('b889fe47-541d-4453-8733-022709c1592f', '079f4abe-80b2-43c0-9566-640869ea266a');
insert into SOGIP_Funcionario_ICODER values('b889fe47-541d-4453-8733-022709c1592f', '0c3fe9ea-2e80-4d7b-b8d2-1158f6c1b824');
insert into SOGIP_Funcionario_ICODER values('b889fe47-541d-4453-8733-022709c1592f', 'cbbe0afc-dbf4-46b3-ba5f-91ca8531decd');
insert into SOGIP_Funcionario_ICODER values('b889fe47-541d-4453-8733-022709c1592f', '6642f3e1-dc3f-43df-9be2-ebff4ee67d56');
insert into SOGIP_Funcionario_ICODER values('b889fe47-541d-4453-8733-022709c1592f', 'fbb7a9fe-9975-4f17-bff2-d760d267a942');
insert into SOGIP_Funcionario_ICODER values('b889fe47-541d-4453-8733-022709c1592f', '09d9e8a1-14e1-43bd-8bc2-75b52378757d');
insert into SOGIP_Funcionario_ICODER values('b889fe47-541d-4453-8733-022709c1592f', '843f21d4-b301-4847-848b-f4cd20827a6a');
insert into SOGIP_Funcionario_ICODER values('b889fe47-541d-4453-8733-022709c1592f', '79a605e5-2900-44bf-9b35-eaf0b419bd17');

insert into SOGIP_Rutina values('2018-10-09 00:00:00.000', 'Bajar Peso, aumentar masa Muscular', '079f4abe-80b2-43c0-9566-640869ea266a');
insert into SOGIP_Rutina values('2018-10-09 00:00:00.000', 'Bajar porcentaje de grasa', '5c48e11d-0abc-4f76-88a5-bc49e83af272');
insert into SOGIP_Rutina values('2018-10-09 00:00:00.000', 'Descanso 1 min despues de terminar la serie', 'c6d47fdc-c219-4d7f-b613-0aca0c812a29');
select * from sogip_users where id = '991f4235-ab92-4fad-94ca-98b9a6dc503c'
select * from sogip_archivo
insert into SOGIP_Conjunto_Ejercicio values('aperturas maq individual', 3, 10, 60, 3, 12, 60, 3, 12, 70, 1, 'Dia1', 'Verde');
insert into SOGIP_Conjunto_Ejercicio values('pull down maquina individual', 3, 10, 90, 3, 12, 90, 3, 90, 100, 1, 'Dia1', 'Verde');
insert into SOGIP_Conjunto_Ejercicio values('rodilla a codo opuesto', 3, 20, 0, 3, 25, 0, 3, 30, 0, 1, 'Dia1', 'Rojo');
insert into SOGIP_Conjunto_Ejercicio values('sentadilla con mancuerda', 3, 10, 15, 3, 12, 15, 3, 12, 20, 1, 'Dia1', 'Azul');
insert into SOGIP_Conjunto_Ejercicio values('santadilla+salto cajon', 3, 10, 0, 3, 12, 0, 3, 12, 0, 1, 'Dia1', 'Azul');
insert into SOGIP_Conjunto_Ejercicio values('abdominales', 3, 25, 0, 3, 27, 0, 3, 30, 0, 1, 'Dia1', 'Celeste');
insert into SOGIP_Conjunto_Ejercicio values('lumbares', 3, 20, 0, 3, 25, 0, 3, 30, 0, 1, 'Dia1', 'Celeste');
insert into SOGIP_Conjunto_Ejercicio values('Press pecho plano', 3, 10, 0, 3, 12, 0, 3, 12, 0, 2, 'Dia1', 'Verde');
insert into SOGIP_Conjunto_Ejercicio values('Extension rodilla', 3, 10, 0, 3, 12, 0, 3, 12, 0, 2, 'Dia1', 'Verde');
insert into SOGIP_Conjunto_Ejercicio values('Remo individaul polea', 3, 10, 0, 3, 12, 0, 3, 12, 0, 2, 'Dia1', 'Azul');
insert into SOGIP_Conjunto_Ejercicio values('Pull down', 3, 10, 0, 3, 12, 0, 3, 12, 0, 2, 'Dia2', 'Verde');
insert into SOGIP_Conjunto_Ejercicio values('Flex codo', 3, 10, 0, 3, 12, 0, 3, 12, 0, 2, 'Dia2', 'Amarrillo');
insert into SOGIP_Conjunto_Ejercicio values('Elevacion frontal hombros', 3, 10, 0, 3, 12, 0, 3, 12, 0, 2, 'Dia2', 'Rojo');
insert into SOGIP_Conjunto_Ejercicio values('press pecho', 3, 12, 0, 3, 12, 0, 3, 12, 0, 3, 'Dia1', 'Rojo');
insert into SOGIP_Conjunto_Ejercicio values('Aperturas con polea', 3, 12, 0, 3, 12, 0, 3, 12, 0, 3, 'Dia1', 'Rojo');
insert into SOGIP_Conjunto_Ejercicio values('Remo Mancuerda', 3, 12, 0, 3, 12, 0, 3, 12, 0, 3, 'Dia1', 'Verde');
insert into SOGIP_Conjunto_Ejercicio values('extension', 3, 12, 0, 3, 12, 0, 3, 12, 0, 3, 'Dia1', 'Celeste');
insert into SOGIP_Conjunto_Ejercicio values('Extension rodilla', 3, 8, 0, 3, 8, 0, 3, 8, 0, 3, 'Dia2', 'Morado');
insert into SOGIP_Conjunto_Ejercicio values('Peso muerto', 3, 12, 0, 3, 12, 0, 3, 12, 0, 3, 'Dia2', 'verde');
insert into SOGIP_Conjunto_Ejercicio values('Pantorrilla', 3, 8, 0, 3, 12, 0, 3, 12, 0, 3, 'Dia2', 'Azul');
insert into SOGIP_Conjunto_Ejercicio values('lumbares', 3, 20, 0, 3, 23, 0, 3, 25, 0, 3, 'Dia2', 'Rojo');

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


-- ++++++++++++++++++++++++++ Insert's ++++++++++++++++++++++++++