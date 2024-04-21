using MinimalAPIPeliculas.Entidades;

var builder = WebApplication.CreateBuilder(args);
var apellido = builder.Configuration.GetValue<string>("apellido");

//Inicio de area de los servicios

//Obteneer el valor de appsettings dev
//porque se estan usando proveedores de configuracion

//Fin area de los servicios

var app = builder.Build();

//Iniciodel area de los diddleware

//Middleware MapGet
//esto permite difinir que quiere que ocurra cuando una peticion get llega a un endpoint especifico
//app.MapGet("/", () => "Hello World!");
app.MapGet("/", () => apellido);
//Esto es un endpoint, ya que, ante una peticion a la ruta(/), haremos el apellido

//Ahora crearemos un endpint para obtenr un listado de generos
//por ahora se trabajara con datos en memoria, depsues sera desde bd
//get porque se necesita obtener
app.MapGet("/generos", () =>
{
	//colocar la funcionalidad que se quiere que se ejecute cuand recibamos una peticion get a la ruta generos

	//En este caso, se devolera un listado de generos
	var generos = new List<Genero> {

		new Genero {
			Id = 1,
			Nombre = "Drama"
		},
		new Genero {
			Id = 2,
			Nombre = "Accion"
		},
		new Genero {
			Id = 3,
			Nombre = "Comedia"
		},
	};
	return generos;
});

//Fin del area de los middleware
//estos definen las accins que queremos ejecutar cada vez que una peticion http sea recibida por la app

app.Run();
