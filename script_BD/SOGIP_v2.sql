/*

+++++++ RECONSTRUIR BASE DE DATOS CON LOS CAMBIOS RESPECTIVOS ++++++++++

use master;
drop database "SOGIP_v3"

create database "SOGIP_v3"
use "SOGIP_v3"

+++++++ RECONSTRUIR BASE DE DATOS CON LOS CAMBIOS RESPECTIVOS ++++++++++

-- ++++++++++++++++++++++++++++ Select's ++++++++++++++++++++++++++++


 select SOGIP_Users.Id, SOGIP_Users.Nombre1, SOGIP_Users.Fecha_Nacimiento, SOGIP_Users.Cedula, SOGIP_Roles.Name, SOGIP_Roles.id
 from 
 SOGIP_Users, SOGIP_UserRoles, SOGIP_Roles
 where 
 SOGIP_Users.Id = SOGIP_UserRoles.Userid
 and
 SOGIP_UserRoles.Roleid = SOGIP_Roles.id
 order by SOGIP_Roles.id;

 insert into SOGIP_Users values('13801ff2-347b-47fe-be88-07c2e903e8be','402360192',NULL,'2019-01-18 15:45:47.237','Carlos','Isaias','Murillo','Badilla','1997-08-28 00:00:00.000',0,1,'cmb28@hotmail.com',0,'APnw7+KqN+h6+3hSMIqXd7oijKxbOBZd2nDsY7c7FXak3NDB+ZLt+KSG7r4skP7B3A==','fd984d9b-1446-4510-a6bb-62384169f32c',NULL,0,0,NULL,1,0,'402360192');

 delete from SOGIP_Users where Email != 'cmb28@hotmail.com';
 delete from SOGIP_Users;

 select * from SOGIP_Users;
 select * from sogip_archivo
 drop table SOGIP_Estados;
 select * from SOGIP_Roles order by Id asc;

 select * from SOGIP_UserRoles, SOGIP_Roles;
 select * from SOGIP_Estados;

insert into SOGIP_Users values('aab4f4cd-060b-4755-8439-dcb1321442db','123456789',NULL,'2018-10-14 17:30:50.793','Carlos','Isaias','Murillo','Badilla','1997-08-28 00:00:00.000',1,'joha@yahos.com',0,'AIyOJ27uVPdqtXxCKXPVcZYYRFs5tW5BRq/F8ZaZnBFanUZRvJjAB7iCD70wVB1b1w==','3285f946-2df7-4ffa-ab40-b0fc82464f32',NULL,0,0,NULL,1,0,'123456789');

drop table SOGIP_Cita;

 select * from SOGIP_Selecciones;
 select * from SOGIP_Entrenadores;
 select * from SOGIP_Atletas;
 select * from SOGIP_Funcionario_ICODER;	
 select * from SOGIP_Entidad_Publica;
 select * from SOGIP_Asociacion_Deportiva;
 select * from SOGIP_Expedientes_Fisicos;

 select * from SOGIP_Archivo;
 select * from SOGIP_Rutina;
select * from SOGIP_Conjunto_Ejercicio;

 sp_help SOGIP_Users; -- Describe los atributos de cualquier tabla.
 sp_help SOGIP_Entrenadores;

*/

-- ++++++++++++++++++++++++++++ Select's ++++++++++++++++++++++++++++

-- ++++++++++++++++++++++++++ Trigger's ++++++++++++++++++++++++++

/*
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
*/

-- ++++++++++++++++++++++++++ Trigger's ++++++++++++++++++++++++++

-- ++++++++++++++++++++++++++++ Insert's ++++++++++++++++++++++++++++

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

/*
insert into sogip_cita values(1,1,'68f77a05-90b7-43a6-ad6e-604837744662', convert(datetime,'10-11-18 08:00:11',1),convert(datetime,'10-11-18 08:25:11',1));
insert into sogip_cita values(1,1,'68f77a05-90b7-43a6-ad6e-604837744662', convert(datetime,'10-11-18 09:00:11',1),convert(datetime,'10-11-18 09:25:11',1));
insert into sogip_cita values(1,1,'68f77a05-90b7-43a6-ad6e-604837744662', convert(datetime,'10-09-18 10:00:11',1),convert(datetime,'10-09-18 10:25:11',1));
insert into sogip_cita values(1,1,'68f77a05-90b7-43a6-ad6e-604837744662', convert(datetime,'10-09-18 11:00:11',1),convert(datetime,'10-09-18 11:25:11',1));
insert into sogip_cita values(1,1,'68f77a05-90b7-43a6-ad6e-604837744662', convert(datetime,'10-23-18 08:00:11',1),convert(datetime,'10-23-18 08:25:11',1));
insert into sogip_cita values(1,1,'68f77a05-90b7-43a6-ad6e-604837744662', convert(datetime,'10-23-18 09:00:11',1),convert(datetime,'10-23-18 09:25:11',1));
insert into sogip_cita values(1,1,'68f77a05-90b7-43a6-ad6e-604837744662', convert(datetime,'11-01-18 08:00:11',1),convert(datetime,'11-01-18 08:25:11',1));
insert into sogip_cita values(1,1,'68f77a05-90b7-43a6-ad6e-604837744662', convert(datetime,'11-01-18 09:00:11',1),convert(datetime,'11-01-18 09:25:11',1));
insert into sogip_cita values(1,1,'68f77a05-90b7-43a6-ad6e-604837744662', convert(datetime,'10-12-18 08:00:11',1),convert(datetime,'10-12-18 08:25:11',1));
insert into sogip_cita values(1,1,'68f77a05-90b7-43a6-ad6e-604837744662', convert(datetime,'10-12-18 09:00:11',1),convert(datetime,'10-12-18 09:25:11',1));
insert into sogip_cita values(1,1,'68f77a05-90b7-43a6-ad6e-604837744662', convert(datetime,'10-12-18 10:00:11',1),convert(datetime,'10-12-18 11:25:11',1));
insert into sogip_cita values(1,1,'68f77a05-90b7-43a6-ad6e-604837744662', convert(datetime,'10-12-18 13:00:11',1),convert(datetime,'10-12-18 13:25:11',1));
*/

-- ++++++++++++++++++++++++++ Insert's ++++++++++++++++++++++++++