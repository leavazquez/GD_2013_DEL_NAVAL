select vi.id_viaje, vi.fecha_salida, ts.nombre_servicio, (mi.cantidad_asientos - (select count(*) from DEL_NAVAL.butacas_ocupadas where viaje = vi.id_viaje)) as Butacas_Libres
,(mi.kilos_bodega - SUM(en.peso)) as Kilos_Disponibles
from DEL_NAVAL.viajes vi 
left join DEL_NAVAL.recorridos re on vi.recorrido = re.id_recorrido
left join DEL_NAVAL.tipos_servicio ts on re.tipo_servicio = ts.id_servicio
left join DEL_NAVAL.micros mi on  mi.id_micro = vi.micro
left join DEL_NAVAL.encomiendas EN on vi.id_viaje = EN.viaje
group by vi.id_viaje, vi.fecha_salida, ts.nombre_servicio, mi.cantidad_asientos, mi.kilos_bodega
order by vi.id_viaje asc


select * from DEL_NAVAL.ButacasDisponiblesXviaje (2)
select DEL_NAVAL.pesoLibreEncomiendasXviaje (2)