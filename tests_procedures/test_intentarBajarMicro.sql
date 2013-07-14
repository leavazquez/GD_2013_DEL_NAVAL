select *
from DEL_NAVAL.micros
where id_micro = 7

set dateformat dmy
go


 select *
 from DEL_NAVAL.viajes
 where micro = 15
  and fecha_salida >= '27/01/2013'
 and fecha_estimada <= '31/12/9999'

 
select * 
 from del_naval.pasajes
 where viaje = 3663
 
select * from ButacasDisponiblesXviaje (3663)

  set dateformat dmy
go

declare @retorno int
exec intentarBajarMicro 7,'27/01/2013',NULL, @retorno output
select @retorno 
 
 exec bajaOserviceMicro 4,'01/05/2012', NULL