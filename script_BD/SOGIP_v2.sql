/*

+++++++ RECONSTRUIR BASE DE DATOS CON LOS CAMBIOS RESPECTIVOS ++++++++++

use master;
drop database "SOGIP_v2.2"

create database "SOGIP_v2.2"
use "SOGIP_v2.2"

+++++++ RECONSTRUIR BASE DE DATOS CON LOS CAMBIOS RESPECTIVOS ++++++++++

-- ++++++++++++++++++++++++++++ Select's ++++++++++++++++++++++++++++


 select SOGIP_Users.Nombre1, SOGIP_Roles.Name, SOGIP_Roles.id
 from 
 SOGIP_Users, SOGIP_UserRoles, SOGIP_Roles
 where 
 SOGIP_Users.Id = SOGIP_UserRoles.Userid
 and
 SOGIP_UserRoles.Roleid = SOGIP_Roles.id
 order by SOGIP_Roles.id;

****PARA PRUEBAS INGRESO MASIVO****
delete SOGIP_Users;
****PARA PRUEBAS INGRESO MASIVO****

 select * from SOGIP_Users;
 select * from SOGIP_Roles order by Id asc;
 select * from SOGIP_UserRoles, SOGIP_Roles;
 select * from SOGIP_Estados;

 select * from SOGIP_Selecciones;
 select * from SOGIP_Entrenadores;
 select * from SOGIP_Atletas;
 select * from SOGIP_Funcionario_ICODER;	
 select * from SOGIP_Entidad_Publica;
 select * from SOGIP_Asociacion_Deportiva;

 sp_help SOGIP_Users; -- Describe los atributos de cualquier tabla.

*/

-- ++++++++++++++++++++++++++++ Select's ++++++++++++++++++++++++++++

-- *********PRUEBA*********
create table SOGIP_Ejercicio(
	EjercicioId int not null identity,
	EjercicioNombre varchar(50) not null,
	EjercicioDescripcion varchar(120),
	EjercicioRef int,
	constraint pkSOGIP_Ejercicio primary key(EjercicioId),
	constraint fkSOGIP_Ejercicio foreign key(EjercicioRef) references SOGIP_Ejercicio(EjercicioId)
);

drop table SOGIP_Ejercicio;
-- *********PRUEBA*********

-- ++++++++++++++++++++++++++ Trigger's ++++++++++++++++++++++++++


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


-- ++++++++++++++++++++++++++ Trigger's ++++++++++++++++++++++++++

-- ++++++++++++++++++++++++++++ Insert's ++++++++++++++++++++++++++++


 insert into SOGIP_Roles values('1', 'Supervisor');
 insert into SOGIP_Roles values('2', 'Administrador');
 insert into SOGIP_Roles values('3', 'Seleccion/Federacion');
 insert into SOGIP_Roles values('4', 'Entrenador');
 insert into SOGIP_Roles values('5', 'Atleta');
 insert into SOGIP_Roles values('6', 'Atleta Alto Rendimiento');
 insert into SOGIP_Roles values('7', 'Funcionarios ICODER');
 insert into SOGIP_Roles values('8', 'Entidades Publicas');
 insert into SOGIP_Roles values('9', 'Asociacion/Comite');

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
 insert into SOGIP_Deportes values('ABC', 1);


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


-- ++++++++++++++++++++++++++ Insert's ++++++++++++++++++++++++++

-- ++++++++++++++++++++++++++ Tablas ++++++++++++++++++++++++++

/*

create table SOGIP_Estado(
	idEstado int not null identity,
	descripcion varchar(80) not null unique,
	constraint pkSOGIP_Estado primary key(idEstado)
);


create table SOGIP_Categoria(
	idCategoria int not null identity,
	descripcion varchar(80) not null unique,
	constraint pkSOGIP_Categoria primary key(idCategoria)
);


create table SOGIP_Tipo_Deporte(
	idTipoDeporte int not null identity,
	descripcion varchar(80) not null unique,
	constraint pkSOGIP_Tipo_Deporte primary key(idTipoDeporte)
);


create table SOGIP_Deporte(
	idDeporte int not null identity,
	nombre varchar(80) not null unique,
	tipoDeporte int,
	constraint pkSOGIP_Deporte primary key(idDeporte)
	constraint fkSOGIP_TipoDeporte1 foreign key(tipoDeporte) references SOGIP_Tipo_Deporte(idTipoDeporte)
);


create table SOGIP_Entrenador(
	idEntrenador int not null identity,
	titulo varBinary(MAX),
	usuario int,
	constraint pkSOGIP_Entrenador primary key(idEntrenador),
	constraint fkSOGIP_Users4 foreign key(usuario) references SOGIP_Users(Id)
);


create table SOGIP_Seleccion(
	idSeleccion int not null identity,
	nombreSeleccion varchar(90) not null,
	usuario int,
	deporte int,
	categoria int,
	entrenador int,
	constraint pkSOGIP_Seleccion primary key(idSeleccion),
	constraint fkSOGIP_Users3 foreign key(usuario) references SOGIP_Users(Id)
	constraint fkSOGIP_Deporte1 foreign key(deporte) references SOGIP_Deporte(idDeporte)
	constraint fkSOGIP_Categoria1 foreign key(categoria) references SOGIP_Categoria(idCategoria),
	constraint fkSOGIP_Entrenador3 foreign key(entrenador) references SOGIP_Entrenador(idEntrenador)
);


create table SOGIP_Asociacion_Deportiva(
	idAsociacionDeportiva int not null identity,
	localidad varchar(100),
	usuario int,
	constraint pkSOGIP_Asociacion_Deportiva primary key(idAsociacionDeportiva),
	constraint fkSOGIP_Users8 foreign key(usuario) references SOGIP_Users(Id)
);


create table SOGIP_Atleta(
	idAtleta int not null identity,
	localidad varchar(100),
	usuario int,
	seleccion int,
	asociacion int,
	constraint pkSOGIP_Atleta primary key(idAtleta),
	constraint fkSOGIP_Users5 foreign key(usuario) references SOGIP_Users(Id),
	constraint fkSOGIP_Seleccion2 foreign key(seleccion) references SOGIP_Seleccion(idSeleccion),
	constraint fkSOGIP_Asociacion1 foreign key(asociacion) references SOGIP_Asociacion_Deportiva(idAsociacionDeportiva)
);


create table SOGIP_Funcionario_ICODER(
	idFuncionarioICODER int not null identity,
	usuario int,
	entrenador int,
	constraint pkSOGIP_Funcionario_ICODER primary key(idFuncionarioICODER),
	constraint fkSOGIP_Users6 foreign key(usuario) references SOGIP_Users(Id),
	constraint fkSOGIP_Users7 foreign key(entrenador) references SOGIP_Entrenador(idEntrenador)
);


create table SOGIP_Tipo_Entidad(
	idTipoEntidad int not null identity,
	descripcion varchar(80) not null unique,
	constraint pkSOGIP_Tipo_Entidad primary key(idTipoEntidad)
);

create table SOGIP_Entidad_Publica(
	idEntidadPublica int not null identity,
	usuario int,
	tipo_entidad int,
	constraint pkSOGIP_Entidad_Publica primary key(idEntidadPublica),
	constraint fkSOGIP_Users8 foreign key(usuario) references SOGIP_Users(Id),
	constraint fkSOGIP_Tipo_Entidad foreign key(tipo_entidad) references SOGIP_Users(idTipoEntidad)
);

*/

-- ++++++++++++++++++++++++++ Tablas ++++++++++++++++++++++++++
