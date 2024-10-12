using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace WebApplication1.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProbarConexionController : ControllerBase
    {
        //Dos maneras de probar la conexion??
        //Una es una consulta a Base de datos una tabla en especifica.
        //Y la otra que es un test de conexion

        //Declaracion de configuracion
        private readonly IConfiguration _configuration;

        //Inicializador con configuracion
        public ProbarConexionController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //Metodo ProbarConexion
        [HttpGet]
        public IActionResult ProbarConexion()
        {
            string connectionString = _configuration.GetConnectionString("Banco");

            try
            {
                using ( var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    return Ok("Conexion establecida");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }

    //      "ConnectionString": {
    //"ConnectionStrings": {
    //  "Banco": "Server=saz-dev-mdb01;Port=3306;User=AKS1;Password=aKs1!ssvmnDEV;Database=AKS1;"
    //}



       // private readonly BancoDbContext _context;

        //---select la tabla
    }
}
