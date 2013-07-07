use GD1C2013
go

exec devolverEncomienda 7,'RANDOM',"07/25/2011",'TEST', -1

select *
from DEL_NAVAL.encomiendas 
where id_encomienda between 1 and 10

select * from DEL_NAVAL.cancelaciones



/*
@pasaje int,
@codigo_devolucion nvarchar(255),
@fecha_devolucion datetime,
@motivo nvarchar(255),
@monto numeric(18,2) output
)

*/