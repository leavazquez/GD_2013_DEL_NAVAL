set dateformat dmy
go

select *
from DEL_NAVAL.viajes VI, DEL_NAVAL.recorridos re, DEL_NAVAL.ciudades CI, DEL_NAVAL.ciudades CIu
where fecha_salida between '27/01/2013' and '28/01/2013' 
 and re.id_recorrido = VI.recorrido
 and re.destino = CI.id_ciudad
 and re.origen = CIu.id_ciudad
 
 update DEL_NAVAL.viajes
 set fecha_llegada = NULL
 where id_viaje = 403
 
 set dateformat dmy
go

declare @retorno int
exec DEL_NAVAL.registrarLlegada 19,11,41,'28/01/2013',@retorno output
select @retorno

select * 
from DEL_NAVAL.encomiendas 

select convert(date,'27/01/2013')

select *
from del_naval.puntos PU, DEL_NAVAL.pasajes PA
where PU.pasaje = PA.id_pasaje
       and PA.viaje = 403
union
select *
from del_naval.puntos PU, DEL_NAVAL.encomiendas EN
where PU.encomienda  = EN.id_encomienda 
       and EN.viaje = 403       

      
 select *
 from del_naval.encomiendas 
 where viaje = 403