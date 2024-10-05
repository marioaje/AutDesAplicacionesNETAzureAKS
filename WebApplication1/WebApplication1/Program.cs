using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Leemos el string desde el json appsettings
var connectionString = builder.Configuration.GetConnectionString("Banco");

//Se establece los settings para el contexto o conexion de base de datos, aca permite 
//el ajuste de la version del servidor, es parametrizable e incluso se puede agregar 
//opciones para la captura de error.
builder.Services.AddDbContext<BancoDbContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21))
));
//Solo cuando todo los ambientes esten homogeneos.

//options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
