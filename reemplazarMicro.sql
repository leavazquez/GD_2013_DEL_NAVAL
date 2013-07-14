

if OBJECT_ID ('reemplazarMicro','P') is not null
 drop procedure reemplazarMicro
go 


--reemplaza un micro "origen" por otro que "destino" en un periodo 
--determinado. Acepta null como fecha "hasta" lo cual significa que lo reemplaza
--en todos los viajes. 
--ESTE PROCEDIMIENTO NO REALIZA VALIDACION, DEBE VALIDARSE PREVIAMENTE
--EL REEMPLAZO CON EL PROCEDIMIENTO intentarBajarMicro
create procedure reemplazarMicro
(@orig int,
@dest int,
@desde datetime,
@hasta datetime
)

as
begin

 --en el caso de que venga NULL por parametro se reemplaza de aqui en adelante
 set @hasta = ISNULL(@hasta, '31/12/9999')
 
-- asigna micro nuevo a viaje para poder contar ya con la funcionalidad
-- de la funcion "butacas disponibles x viaje"

begin transaction

update DEL_NAVAL.viajes 
set micro = @dest --micro destino
where id_viaje in (select id_viaje
 from DEL_NAVAL.viajes
 where micro = @orig --micro origen
 and fecha_salida >= @desde
 and fecha_estimada <= @hasta
 and cancelado = 0 )

--libera todas las butacas ocupadas en los viajes que involucran 
--al micro que acabamos de sustituir
 delete DEL_NAVAL.butacas_ocupadas 
  where viaje in (select id_viaje
 from DEL_NAVAL.viajes
 where micro = @dest --micro destino (porque lo acabo de actualizar)
 and fecha_salida >= @desde
 and fecha_estimada <= @hasta
 and cancelado = 0 )



--actualiza butacas en pasajes y les cambia el status a ocupadas
declare @pasaje int, @viaje int

declare cPasajes cursor for
  select id_pasaje, viaje
   from DEL_NAVAL.pasajes
   where viaje in (select id_viaje
 from DEL_NAVAL.viajes
 where micro = @dest --micro destino (porque lo acabo de actualizar)
 and fecha_salida >= @desde
 and fecha_estimada <= @hasta
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
 
commit


 close cPasajes
 deallocate cPasajes

return 0 
 end
 go



            
               