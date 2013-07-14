

if OBJECT_ID ('top5_destinos_comprados','P') is not null
 drop function top5_destinos_comprados
go 


create funcrtion top5_destinos_comprados
@desde datetime,
@hasta datetime
)

select top 5 CI.nombre_ciudad, COUNT (*) as Cant_vendida
 from DEL_NAVAL.pasajes PA, DEL_NAVAL.viajes VI, DEL_NAVAL.recorridos RE, 
      DEL_NAVAL.ciudades CI, DEL_NAVAL.compras CO
 where PA.viaje = VI.id_viaje
 and   VI.recorrido = RE.id_recorrido
 and   RE.destino = CI.id_ciudad
 and   CO.id_voucher = PA.voucher 
 and   CO.fecha_compra between '01/01/2012' and '30/06/2012'
 group by CI.nombre_ciudad
 order by Cant_vendida desc
 

 
 