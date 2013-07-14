
     
 select *
 from DEL_NAVAL.butacas_ocupadas
 where viaje = 3363 
              

set dateformat dmy
go              
exec reemplazarMicro 7, 15, '27/01/2013', NULL            
              



set dateformat dmy
go
select *
   from DEL_NAVAL.pasajes
   where viaje in (select id_viaje
 from DEL_NAVAL.viajes
 where micro = 15 --micro origen
 and fecha_salida >= '27/01/2013'
 and fecha_estimada <= '31/12/9999'
 and cancelado = 0 ) 


set dateformat dmy
go

select *
 from DEL_NAVAL.viajes
 where micro = 15
 and fecha_salida >= '31/01/2013'
 and fecha_estimada <= '12/07/9999'
 and cancelado = 0 
 

select distinct butaca
 from DEL_NAVAL.pasajes
 where viaje = 3663

select *
from DEL_NAVAL.butacas
where id_butaca between 271 and 320

select cantidad_asientos 
from DEL_NAVAL.micros
where id_micro = 15



select cantidad_asientos
from DEL_NAVAL.micros 
where id_micro = 2

select *
from del_naval.butacas_ocupadas 
where viaje = 3663



select * from ButacasDisponiblesXviaje (4147)

select * from ButacasDisponiblesXviaje (3799)


             