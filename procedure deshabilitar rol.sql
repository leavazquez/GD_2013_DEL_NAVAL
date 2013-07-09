if OBJECT_ID ('inhabilitar_rol','P') is not null
 drop procedure inhabilitar_rol
go

create procedure inhabilitar_rol
(@rol int)
as
begin
begin transaction

update DEL_NAVAL.roles
set activo = 0
where id_rol = @rol

--Desasignamos los roles a los usuarios y deshabilitamos los usuarios 
--para evitar que un usuario sin rol pueda logearse en el sistema
update DEL_NAVAL.usuarios
set activo = 0,
    rol = NULL
where rol = @rol

commit
end;
go
