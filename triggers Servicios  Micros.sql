if OBJECT_ID ('DEL_NAVAL.actualizar_servicios_microUPD','TR') is not null
  drop trigger DEL_NAVAL.actualizar_servicios_microUPD   
go  
 
--El siguiente trigger se encarga de registrar las bajas por servicio de un micro
--en la tabla de historico de bajas por servicio
  
create trigger del_naval.actualizar_servicios_microUPD 
on del_naval.micros
after update
as 

   declare @micro int, @desdenuevo datetime, @hastanuevo datetime,
                       @desdeviejo datetime, @hastaviejo datetime
   
   set @micro = (select top 1 id_micro from inserted)
   set @desdenuevo = (select top 1 fecha_servicio_desde from inserted)
   set @hastanuevo = (select top 1 fecha_servicio_hasta from inserted)
   
   set @desdeviejo = (select top 1 fecha_servicio_desde from deleted)
   set @hastaviejo = (select top 1 fecha_servicio_hasta from deleted)
   
   --si el rango de fechas de servicio no es null...
  if (@desdenuevo is not null and @hastanuevo is not null)
  
  --se fija que si el rango viejo no era null y hay variacion, hace el insert en el registro 
  -- historico. O si estaba en null el registro anterior, tmb hace el insert  
   if  ((@desdeviejo is not null and @hastaviejo is not null)
      and (@desdenuevo <> @desdeviejo) and (@hastanuevo <> @hastaviejo))
       OR 
      (@desdeviejo is null and @hastaviejo is null)
   
       insert into del_naval.servicios_micro
        values(@micro,@desdenuevo,@hastanuevo)   
                
 go           
 
 
 if OBJECT_ID ('DEL_NAVAL.actualizar_servicios_microINS','TR') is not null
  drop trigger DEL_NAVAL.actualizar_servicios_microINS   
go  
 
--El siguiente trigger se encarga de registrar las bajas por servicio de un micro
--en la tabla de historico de bajas por servicio
  
create trigger del_naval.actualizar_servicios_microINS 
on del_naval.micros
after insert
as 

   declare @micro int, @desdenuevo datetime, @hastanuevo datetime
          
   
   set @micro = (select top 1 id_micro from inserted)
   set @desdenuevo = (select top 1 fecha_servicio_desde from inserted)
   set @hastanuevo = (select top 1 fecha_servicio_hasta from inserted)

   --si el rango de fechas de servicio no es null...
  if (@desdenuevo is not null and @hastanuevo is not null) 

       insert into del_naval.servicios_micro
        values(@micro,@desdenuevo,@hastanuevo)   
                
 go                   
 