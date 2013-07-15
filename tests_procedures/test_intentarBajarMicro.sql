select *
from DEL_NAVAL.micros
where id_micro = 1

set dateformat dmy
go


 select *
 from DEL_NAVAL.viajes
 where micro = 1
  and fecha_salida >= '01/01/2013'
 and fecha_estimada <= '02/01/2013'

 
select * 
 from del_naval.pasajes
 where viaje = 3663
 
select * from ButacasDisponiblesXviaje (3663)

  set dateformat dmy
go

declare @retorno int
exec del_naval.intentarBajarMicro 7,'27/01/2014',NULL, @retorno output
select @retorno 
 
 exec bajaOserviceMicro 4,'01/05/2012', NULL