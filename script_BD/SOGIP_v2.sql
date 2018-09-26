-- use "SOGIP_v2.2"
-- select * from SOGIP_Users;
-- sp_help SOGIP_Users;

-- ++++++++++++++++++++++++++ TABLAS ++++++++++++++++++++++++++

create table SOGIP_Estado(
	idEstado int not null identity,
	descripcion varchar(80) not null unique,
	constraint pkSOGIP_Estado primary key(idEstado)
);


create table SOGIP_Usuario(
	idUsuario int not null identity,
	cedula varchar(45) not null unique,
	cedulaExtra varchar(45),
	contrasena varchar(45) not null,
	fecha_expiracion datetime,
	nombre varchar(90) not null,
	correo varchar(40),
	telefono varchar(20),
	estado int,
	rol int,
	constraint pkSOGIP_Usuario primary key(id),
	constraint fkSOGIP_Rol1 foreign key(tipo) references SOGIP_Rol(idRol)
	constraint fkSOGIP_Estado1 foreign key(tipo) references SOGIP_Estado1(idEstado)
);

-- Supervisor es un tipo de Administrador.
create table SOGIP_Administrador( 
	id int not null identity,
	apellido1 varchar(90),
	apellido2 varchar(90),
	fecha_nacimiento datetime not null,
	usuario int,
	constraint pkSOGIP_Administrador primary key(id),
	constraint fkSOGIP_Usuario1 foreign key(usuario) references SOGIP_Usuario(id)
);

create table SOGIP_Entrenador(
	id int not null identity,
	apellido1 varchar(90),
	apellido2 varchar(90),
	fecha_nacimiento datetime,
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
	apellido1 varchar(90),
	apellido2 varchar(90),
	fecha_nacimiento datetime,
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

-- Asociaciones/Federaciones/Selecciones(?)
create table SOGIP_Entidades( 
	idEntidades int not null identity,
	localidad varchar(45),
	usuario int,
	constraint plkSOGIP_Entidades primary key(idEntidades),
	constraint fkSOGIP_Usuario5 foreign key(usuario) references SOGIP_Usuario(id)
);


create table SOGIP_Funcionarios_ICODER(
	idFuncionarios_ICODER int not null identity,
	apellido1 varchar(90),
	apellido2 varchar(90),
	fecha_nacimiento datetime,
	genero tinyint not null,
	usuario int,
	entrenador int,
	constraint pkSOGIP_Funcionarios_ICODER primary key(id),
	constraint fkSOGIP_Usuario5 foreign key(usuario) references SOGIP_Usuario(id),
	constraint fkSOGIP_Usuario7 foreign key(entrenador) references SOGIP_Usuario(idUsuario),
);


create table SOGIP_Entidades_Publicas(
	idEntidades_Publicas int not null identity,
	apellido1 varchar(90),
	apellido2 varchar(90),
	fecha_nacimiento datetime,
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
 insert into SOGIP_Roles values('3', 'Seleccion');
 insert into SOGIP_Roles values('4','Federacion');
 insert into SOGIP_Roles values('5','Entrenador');
 insert into SOGIP_Roles values('6','Atleta');
 insert into SOGIP_Roles values('7','Funcionarios ICODER');
 insert into SOGIP_Roles values('8','Entidades Publicas');
 insert into SOGIP_Roles values('9','Asociacion');
 insert into SOGIP_Roles values('10','Comite');

-- insert into SOGIP_Estado values('Finalizado');
-- insert into SOGIP_Estado values('Activo');
-- insert into SOGIP_Estado values('En Proceso');


-- ++++++++++++++++++++++++++++ Inserts ++++++++++++++++++++++++++++


-- ++++++++++++++++++++++++++ TRIGGERS ++++++++++++++++++++++++++


--create trigger tr1 on SOGIP_Usuario
-- for update, insert
--  as
--   if update(contrasena)
--    begin
--     update SOGIP_Usuario
--     set fecha_expiracion=SOGIP_Usuario.fecha_expiracion+90
--     from inserted
--     where SOGIP_Usuario.id = inserted.id
--    end

-- ++++++++++++++++++++++++++ TRIGGERS ++++++++++++++++++++++++++


-- ++++++++++++++++++++++++++++ Drops ++++++++++++++++++++++++++++

-- drop table SOGIP_Supervisor;
-- drop table SOGIP_Administrador;
-- drop table SOGIP_Seleccion;
-- drop table SOGIP_Entrenador;
-- drop table SOGIP_Atleta;
-- drop table SOGIP_Funcionarios_ICODER;
-- drop table SOGIP_Entidades Publicas;

-- drop table SOGIP_Tipo;
-- drop table SOGIP_Estado;

-- drop table SOGIP_Usuario;

-- drop trigger tr1;

-- Para dropear la DB se posiciona sobre otra DB y a continuación
-- se dropea.

--  1. use master;
--  2. drop database SOGIP;

-- ++++++++++++++++++++++++++++ Drops ++++++++++++++++++++++++++++