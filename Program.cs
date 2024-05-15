using Microsoft.AspNetCore.Cors;
using MinimalAPIPeliculas.Entidades;

var builder = WebApplication.CreateBuilder(args);
//var apellido = builder.Configuration.GetValue<string>("apellido");

//Cors
var origenesPermitidos = builder.Configuration.GetValue<string>("origenesPermitidos")!;
//COn ! indicamos que siempre tendra valor y no sera nulo

				//Inicio de area de los servicios

//Obteneer el valor de appsettings dev
//porque se estan usando proveedores de configuracion


	//---  Habilitar CORS  --  SOLO configuracion, no uso o implementacion   -- Solo con esto, aun tenemos cors bloqueado
		//Paera usarlo, es en el area de los middlewares, mas abajo
//Servicio de microsoft ya preconfigurado
builder.Services.AddCors(opciones =>
{//para multiples, se agrego esta {}
	opciones.AddDefaultPolicy(configuracion =>
	{
		//Con esto permitiremos cualquier pagina web podra comunicarse de cualquier forma
		//permitir todos los origenes
		//permitir cualquier cabecera
		//cualquier metodo, como es get, post, put
		//configuracion.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();

		//COn un ORIGEN ESPECIFICADO permitido En lugar de cmo arriba, usaremos
			//WithOrigins para decir que origenes con ayuda de lo establecido en appsettings:
		configuracion.WithOrigins(origenesPermitidos).AllowAnyHeader().AllowAnyMethod();
	});

	//si son varias politicas se hace de esta manera
	opciones.AddPolicy("libre", configuracion =>
	{
		configuracion.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
	});

});

//cone sto ya se tiene configurado y disponible pero aun no se usa en este punto- para usarlo es ir al area de los middleware
builder.Services.AddOutputCache();

//Configuracion de swagger, despues de instalar nuget
builder.Services.AddEndpointsApiExplorer();// esto permitira que swagger pueda explorar los enpoints y listarlos en el sitio web
//configuracion tal cual de swagger:
builder.Services.AddSwaggerGen();

			//Fin area de los servicios

var app = builder.Build();

//Inicio del area de los MIDDLEWAREs
//Los middleware definen las acciones  que queremos ejecutar cada vez que una peticion http sea recibida por la app
//El orden de estos es importante

//Utilizar swager - para swagger, debe se antes de UseCors, porque el sitio a ingresar de swagger es propio, no es origen ajeno y por ende no requiere cors
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mi API v1"));
}


//Si queremos aplicar cors a mis endpoints, debo configurarlo antes de los endpints en los middleware

//Aqui colocaquermos la configuracion de CORS
app.UseCors();

//para usar outputcache- posteriormente se debe indicar que endoint va a usar esto
app.UseOutputCache();

			//Middleware MapGet
//esto permite difinir que quiere que ocurra cuando una peticion get llega a un endpoint especifico

//Se especifico que se habilitara cors usando la politica de libre en lugar de la primra que es por defecto, solo para este endpoint
app.MapGet("/",[EnableCors(policyName:"libre")] () => "Hello World!");

//app.MapGet("/", () => apellido); //se comento esto y se descomento lo de arriba, ya que se quito appe de appsett

//Esto es un endpoint, ya que, ante una peticion a la ruta(/), haremos el apellido

//Ahora crearemos un endpint para obtenr un listado de generos
//por ahora se trabajara con datos en memoria, depsues sera desde bd
//get porque se necesita obtener
app.MapGet("/generos", () =>
{
	//colocar la funcionalidad que se quiere que se ejecute cuand recibamos una peticion get a la ruta generos

	//En este caso, se devolera un listado de generos
	var generos = new List<Genero>
	{

		new Genero
		{
			Id = 1,
			Nombre = "Drama"
		},
		new Genero
		{
			Id = 2,
			Nombre = "Accion"
		},
		new Genero
		{
			Id = 3,
			Nombre = "Comedia"
		},
	};
	return generos;
}).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(15))); //Con esto se indica que a este endpoint se aplicara el outputCache y se especifica el tiempo a usarse

			//Fin del area de los middleware
//estos definen las accins que queremos ejecutar cada vez que una peticion http sea recibida por la app


app.Run();
