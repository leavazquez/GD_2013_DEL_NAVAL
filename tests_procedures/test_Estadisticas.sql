use GD1C2013
go
set dateformat dmy
go

select top 5 * from DEL_NAVAL.destinos_comprados ('01/01/2013', '30/06/2013')

select top 5 * from DEL_NAVAL.destinos_micros_vacios ('01/07/2012', '31/12/2012')

select top 5 * from DEL_NAVAL.destinos_pasajes_cancelados ('01/07/2012', '31/12/2013')

select top 5 * from DEL_NAVAL.micros_fuera_servicio('01/07/2012', '30/07/2012') 


/*
5-10

*/

update DEL_NAVAL.micros
set fecha_servicio_desde = '01/07/2012',
    fecha_servicio_hasta = '09/07/2012'
where cantidad_asientos = 40  

select * 
from del_naval.micros

select * 
from del_naval.cancelaciones







select COUNT( *) from DEL_NAVAL.ButacasDisponiblesXviaje (4070)