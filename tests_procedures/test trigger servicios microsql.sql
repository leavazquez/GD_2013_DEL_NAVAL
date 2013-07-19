set dateformat dmy
go

select  MI.id_micro, MI.patente, sum(day(SE.fecha_hasta - SE.fecha_desde)) as dias_fuera_servicio
 from  del_naval.micros MI, del_naval.servicios_micro SE
 where fecha_servicio_desde >= '01/12/2012'
 and fecha_servicio_hasta <= '31/12/2013'
 and mi.id_micro = SE.micro
 group by id_micro, patente
 


 set dateformat dmy
go

 update DEL_NAVAL.micros
 set fecha_servicio_desde = '01/01/2013',
     fecha_servicio_hasta = '31/01/2013'
where id_micro = 28  
     
select *
from DEL_NAVAL.servicios_micro     