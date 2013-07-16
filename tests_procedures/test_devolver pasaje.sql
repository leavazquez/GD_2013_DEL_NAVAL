use GD1C2013
go


declare @outp numeric(18,2)
exec del_naval.devolverPasaje 7300,'RANDOM',"07/30/2012",'TEST', @outp output 
select @outp

select  *
from DEL_NAVAL.pasajes PA, DEL_NAVAL.butacas_ocupadas BO, del_naval.viajes VI
where pa.butaca = BO.butaca 
and   pa.viaje = BO.viaje 
and   PA.viaje = 10
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