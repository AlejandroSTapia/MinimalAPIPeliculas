using Microsoft.EntityFrameworkCore;
using MinimalAPIPeliculas.Entidades;

namespace MinimalAPIPeliculas
{
	public class ApplicationDbContext : DbContext //DbContext es la pieza central de EFC
												  //A traves de esta clase podremos configurar las tablas de nuestra bd, consultas, etc
	{
		public ApplicationDbContext(DbContextOptions options) : base(options) //DbContextOptions se refeire a configuraciones que podemos pasar hacia nuestro dbcontext desde la clase program
		{//configuraciones como usar sql server, connectionString(cadena de conexion que aputna hacia nuestra base),etc
		}
		//Y con esto ahora si, ya podemos decir que tenemos una entidad, ya que esta usando DbSet, su estructura sera Genero
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer("DefaultConnection",
					sqlServerOptionsAction: sqlOptions =>
					{
						sqlOptions.EnableRetryOnFailure(
							maxRetryCount: 5, // Número máximo de reintentos
							maxRetryDelay: TimeSpan.FromSeconds(30), // Tiempo máximo de espera entre reintentos
							errorNumbersToAdd: null); // Errores adicionales para considerar transitorios
					});
			}
		}
		//Sin embargo la tabla aun no se ha creado, porque falta hacer la migracion por comados

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Genero>().ToTable("Generos");
		}
	}
}
