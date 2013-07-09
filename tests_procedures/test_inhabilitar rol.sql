select * 
from del_naval.roles ro
right join del_naval.usuarios us
on ro.id_rol = us.rol

exec dbo.inhabilitar_rol 2