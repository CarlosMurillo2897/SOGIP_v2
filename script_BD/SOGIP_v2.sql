-- create database SOGIP_v2.2
-- use "SOGIP_v2.2"
-- use master;

-- drop database "SOGIP_v2.2"
-- delete from SOGIP_UserLogins;
-- delete from SOGIP_UserClaims;
-- delete from SOGIP_Users;
-- delete from SOGIP_UserRoles;
-- delete from SOGIP_Roles;

-- select * from SOGIP_Users;
-- select * from SOGIP_Users, SOGIP_UserRoles where SOGIP_Users.Id = SOGIP_UserRoles.Userid;
-- select * from SOGIP_Roles order by Id asc;
-- select * from SOGIP_UserRoles;
-- sp_help SOGIP_Users; -- Describe la tabla.
-- select * from SOGIP_Estados;

-- ++++++++++++++++++++++++++ TABLAS ++++++++++++++++++++++++++

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
	nomEntPub varchar(90) not null,
	usuario int,
	tipo_entidad int,
	constraint pkSOGIP_Entidad_Publica primary key(idEntidadPublica),
	constraint fkSOGIP_Users8 foreign key(usuario) references SOGIP_Users(Id),
	constraint fkSOGIP_Tipo_Entidad foreign key(tipo_entidad) references SOGIP_Users(idTipoEntidad)
);


-- ++++++++++++++++++++++++++ TABLAS ++++++++++++++++++++++++++


-- ++++++++++++++++++++++++++++ Inserts ++++++++++++++++++++++++++++

 insert into SOGIP_Roles values('1', 'Supervisor');
 insert into SOGIP_Roles values('2', 'Administrador');
 insert into SOGIP_Roles values('3', 'Seleccion/Federacion');
 insert into SOGIP_Roles values('4', 'Entrenador');
 insert into SOGIP_Roles values('5', 'Atleta');
 insert into SOGIP_Roles values('6', 'Funcionarios ICODER');
 insert into SOGIP_Roles values('7', 'Entidades Publicas');
 insert into SOGIP_Roles values('8', 'Asociacion/Comite');
-- insert into SOGIP_Roles values('9','Federacion');
-- insert into SOGIP_Roles values('10','Comite');


-- insert into SOGIP_Estado values('Finalizado');
-- insert into SOGIP_Estado values('Activo');
-- insert into SOGIP_Estado values('En Proceso');


-- ++++++++++++++++++++++++++++ Inserts ++++++++++++++++++++++++++++


-- ++++++++++++++++++++++++++ TRIGGERS ++++++++++++++++++++++++++

create trigger fecha_expiracion on SOGIP_Users
 for update, insert
  as
   if update(PasswordHash)
    begin
     update AspNetUsers
     set fecha_expiracion=SOGIP_Users.fecha_expiracion+90
     from inserted
     where SOGIP_Users.id = inserted.id
    end

-- ++++++++++++++++++++++++++ TRIGGERS ++++++++++++++++++++++++++


-- ++++++++++++++++++++++++++++ Drops ++++++++++++++++++++++++++++

-- drop table SOGIP_Supervisor;
-- drop table SOGIP_Administrador;
-- drop table SOGIP_Seleccion;
-- drop table SOGIP_Entrenador;
-- drop table SOGIP_Atleta;
-- drop table SOGIP_Funcionarios_ICODER;
-- drop table SOGIP_Entidades Publicas;

-- drop table SOGIP_Estado;
-- drop table SOGIP_UserLogins;
-- drop table SOGIP_UserClaims;
-- drop table SOGIP_Users;
-- drop table SOGIP_UserRoles;
-- drop table SOGIP_Roles;
-- drop trigger tr1;

-- Drop DB
-- 1. use master;
-- 2. drop database "SOGIP_v2.2";

-- ++++++++++++++++++++++++++++ Drops ++++++++++++++++++++++++++++