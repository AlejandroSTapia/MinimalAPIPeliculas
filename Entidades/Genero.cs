using System.ComponentModel.DataAnnotations;

namespace MinimalAPIPeliculas.Entidades
{
	//Si aun no existe esta tabla en sql, aun no se considera una entidad, para eso se va a configurar en ApplicationDbContext
	public class Genero
	{
		public int Id { get; set; }
		//Anotaciones de datos: supongamos que queremos que la coplumna nombre no sea varcahr max, si no varchar(50)
		[StringLength(50)] 
		public string Nombre { get; set; } = null!; //Perdonar el nulo
		
		//despues de este cambio, se ejecuta una nueva migracion
			//Add-Migration NombreConfigurado
	}
}
