select * from del_naval.usuarios

update DEL_NAVAL.usuarios
 set activo = 1
 where id_usuario = 3
 
 update DEL_NAVAL.usuarios
 set intentos = 3
 where id_usuario = 3