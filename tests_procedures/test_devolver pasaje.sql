use GD1C2013
go

exec devolverPasaje 7,'RANDOM',"07/30/2011",'TEST', -1

select  *
from DEL_NAVAL.pasajes PA, DEL_NAVAL.butacas_ocupadas BO, del_naval.viajes VI
where pa.butaca = BO.butaca 
and   pa.viaje = BO.viaje 
and   vi.fecha_llegada < '2011-01-24' 
--and   pa.id_pasaje between 5 and 1000

select * from DEL_NAVAL.cancelaciones



/*
@pasaje int,
@codigo_devolucion nvarchar(255),
@fecha_devolucion datetime,
@motivo nvarchar(255),
@monto numeric(18,2) output
)

*/