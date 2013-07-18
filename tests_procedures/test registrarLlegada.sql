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
 where id_viaje = 1414
 
 set dateformat dmy
go

declare @retorno int
exec DEL_NAVAL.registrarLlegada 35,6,41,'27/01/2013',@retorno output
select @retorno

select * 
from DEL_NAVAL.encomiendas 

select convert(date,'27/01/2013')

