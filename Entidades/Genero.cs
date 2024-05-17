namespace MinimalAPIPeliculas.Entidades
{
	//Si aun no existe esta tabla en sql, aun no se considera una entidad, para eso se va a configurar en ApplicationDbContext
	public class Genero
	{
		public int Id { get; set; }
		public string Nombre { get; set; } = null!; //Perdonar el nulo
	}
}
