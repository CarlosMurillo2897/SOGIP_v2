-- use "SOGIP_v2.2"
 delete from SOGIP_UserLogins;
 delete from SOGIP_UserClaims;
 delete from SOGIP_Users;
 delete from SOGIP_UserRoles;
 delete from SOGIP_Roles;
-- select * from SOGIP_Users;
-- select * from SOGIP_Roles order by Id asc;
-- select * from SOGIP_UserRoles;
-- sp_help SOGIP_Users;

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
	id int not null identity,
	genero tinyint not null,
	titulo varBinary(MAX),
	usuario int,
	seleccion int,
	constraint pkSOGIP_Entrenador primary key(id),
	constraint fkSOGIP_Usuario3 foreign key(usuario) references SOGIP_Usuario(id),
	constraint fkSOGIP_Seleccion1 foreign key(seleccion) references SOGIP_Seleccion(id)
);


create table SOGIP_Atleta(
	id int not null identity,
	genero tinyint not null,
	usuario int,
	seleccion int,
	asociacion int,
	entrenador int,
	constraint pkSOGIP_Atleta primary key(id),
	constraint fkSOGIP_Usuario4 foreign key(usuario) references SOGIP_Usuario(id),
	constraint fkSOGIP_Seleccion2 foreign key(seleccion) references SOGIP_Seleccion(id),
	constraint fkSOGIP_Entrenador1 foreign key(entrenador) references SOGIP_Entrenador(id)
);


create table SOGIP_Entidades( 
	idEntidades int not null identity,
	localidad varchar(45),
	usuario int,
	constraint plkSOGIP_Entidades primary key(idEntidades),
	constraint fkSOGIP_Usuario5 foreign key(usuario) references SOGIP_Usuario(id)
);


create table SOGIP_Funcionarios_ICODER(
	idFuncionarios_ICODER int not null identity,
	genero tinyint not null,
	usuario int,
	entrenador int,
	constraint pkSOGIP_Funcionarios_ICODER primary key(id),
	constraint fkSOGIP_Usuario5 foreign key(usuario) references SOGIP_Usuario(id),
	constraint fkSOGIP_Usuario7 foreign key(entrenador) references SOGIP_Usuario(idUsuario),
);


create table SOGIP_Entidades_Publicas(
	idEntidades_Publicas int not null identity,
	genero tinyint not null,
	usuario int,
	entrenador int,
	constraint pkSOGIP_Entidades_Publicas primary key(id),
	constraint fkSOGIP_Usuario6 foreign key(usuario) references SOGIP_Usuario(id),
	constraint fkSOGIP_Entrenador3 foreign key(entrenador) references SOGIP_Entrenador(idEntrenador),
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