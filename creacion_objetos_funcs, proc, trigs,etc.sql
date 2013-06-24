use GD1C2013
go

--descomentar drops para primera vez
drop function PesoLibreEncomiendasXviaje
drop function ButacasDisponiblesXviaje 
go

--Devuelve espacio libre en bodega para un viaje determinado
create function PesoLibreEncomiendasXviaje 

(@viaje int)

returns numeric(18,0)

as

Begin
declare @kilosBodega numeric(18,0)
declare @pesoOcupado numeric(18,0)

 set @kilosBodega = (select top 1 MI.kilos_bodega 
 from del_naval.viajes VI, del_naval.micros MI
 where MI.id_micro = VI.micro
 and VI.id_viaje = @viaje)       
 
 set @pesoOcupado = (select SUM (peso)
 from DEL_NAVAL.encomiendas
 where viaje = @viaje)
 
 return @kilosBodega - @pesoOcupado       
End;
go

create function ButacasDisponiblesXviaje 
(@viaje int)

returns @butacas_disponibles TABLE
( id_butaca int, 
  micro int,
  tipo nvarchar(255), 
  piso numeric(18,0),
  numero numeric(18,0)
  )
  --,ocupado bit )
  --no traigo el campo ocupado, ya q solo traigo los desocupados
 as
Begin
 insert @butacas_disponibles
 select BU.id_butaca, BU.micro, BU.tipo, BU.piso, BU.numero --,OC.ocupado 
 from del_naval.butacas BU
 left join del_naval.butacas_ocupadas OC
  on OC.viaje = @viaje
 and OC.butaca = BU.id_butaca
 inner join del_naval.viajes VI
 on VI.id_viaje = @viaje
 and BU.micro = VI.micro
 where (OC.ocupado is null) OR OC.ocupado = 0

return
    
End;