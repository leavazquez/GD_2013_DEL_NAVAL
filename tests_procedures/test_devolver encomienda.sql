use GD1C2013
go

exec devolverEncomienda 8183,'RANDOM',"07/25/2011",'TEST', -1

select *
from DEL_NAVAL.encomiendas 
where viaje = 9

select * 
from DEL_NAVAL.viajes
order by fecha_llegada asc

select * from DEL_NAVAL.cancelaciones



/*
@pasaje int,
@codigo_devolucion nvarchar(255),
@fecha_devolucion datetime,
@motivo nvarchar(255),
@monto numeric(18,2) output
)

*/