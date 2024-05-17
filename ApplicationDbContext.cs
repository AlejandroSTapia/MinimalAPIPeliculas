﻿using Microsoft.EntityFrameworkCore;
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
		public DbSet<Genero> Generos { get; set; }
		//Sin embargo la tabla aun no se ha creado, porque falta hacer la migracion
	}
}
