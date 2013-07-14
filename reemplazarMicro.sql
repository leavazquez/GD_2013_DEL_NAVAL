

set dateformat dmy
go

select *
 from DEL_NAVAL.viajes
 where micro = 3
 and fecha_salida >= '27/01/2013'
 and fecha_estimada <= '12/07/9999'
 and cancelado = 0 
 

select *
 from DEL_NAVAL.pasajes
 where viaje = 532



select cantidad_asientos 
from DEL_NAVAL.micros
where id_micro = 7



select cantidad_asientos
from DEL_NAVAL.micros 
where id_micro = 3

select * 
from del_naval.butacas_ocupadas 
where viaje = 532 or viaje = 3799



select * from ButacasDisponiblesXviaje (532)

select * from ButacasDisponiblesXviaje (3799)




-- asigna micro nuevo a viaje para poder contar ya con la funcionalidad
-- de la funcion "butacas disponibles x viaje"

update DEL_NAVAL.viajes 
set micro = 3 --micro destino
where id_viaje in (select id_viaje
 from DEL_NAVAL.viajes
 where micro = 7 --micro origen
 and fecha_salida >= '27/01/2013'
 and fecha_estimada <= '12/07/9999'
 and cancelado = 0 )

--libera todas las butacas ocupadas en los viajes que involucran 
--al micro que acabamos de sustituir
 delete DEL_NAVAL.butacas_ocupadas 
  where viaje in (select id_viaje
 from DEL_NAVAL.viajes
 where micro = 7 --micro origen
 and fecha_salida >= '27/01/2013'
 and fecha_estimada <= '12/07/9999'
 and cancelado = 0 )



--actualiza butacas en pasajes y les cambia el status a ocupadas
declare @pasaje int, @viaje int

declare cPasajes cursor for
  select id_pasaje, viaje
   from DEL_NAVAL.pasajes
   where viaje in (select id_viaje
 from DEL_NAVAL.viajes
 where micro = 7 --micro origen
 and fecha_salida >= '27/01/2013'
 and fecha_estimada <= '12/07/9999'
 and cancelado = 0 ) 
                     
 open cPasajes 
 fetch cPasajes into @pasaje, @viaje
 
 while (@@FETCH_STATUS = 0)
 begin
  

--obtengo la primer butaca disponible (corresponden al nuevo micro)
--de la lista de disponibles para ese viaje
declare @butaca int
set @butaca = (select top 1 id_butaca from ButacasDisponiblesXviaje (@viaje))

--actualizo la butaca del pasaje con la butaca disponible del nuevo micro
update DEL_NAVAL.pasajes 
set butaca = @butaca
where id_pasaje = @pasaje  

--si la butaca figura como desocupada para ese viaje, lo actualiza a ocupada
--poniendola en la tabla correspondiente
  if not exists (select * 
                  from del_naval.butacas_ocupadas 
                  where butaca = @butaca
                   and  viaje =  @viaje)    
      insert into del_naval.butacas_ocupadas
        values(@viaje,@butaca)         
 
  
  fetch cPasajes into @pasaje, @viaje
 end
 
 close cPasajes
 deallocate cPasajes




 select BU.id_butaca, BU.micro, BU.tipo, BU.piso, BU.numero
 from del_naval.butacas BU
 left join del_naval.butacas_ocupadas OC
  on OC.viaje = 532
 and OC.butaca = BU.id_butaca
 left join del_naval.viajes VI
 on VI.id_viaje = 532
 and BU.micro = VI.micro
 where VI.cancelado = 0
                              
                                       
               