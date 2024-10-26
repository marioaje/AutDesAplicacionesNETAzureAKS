using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using System.Numerics;


namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {

        //Variables
        private static List<string> _products = new List<string>{
                "TV", "Cel","Laptop"
        };


        //Es la base, es el index, es la consulta principal, es el por defecto
        //URL el host, www
        //GET :api/productos
        [Authorize]
        [HttpGet]
        public IActionResult GetProductos()
        {
            //Se extraigan los datos desde BD
            return Ok(_products);
        }

        //GET :api/productos/{id}
        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login(string usertname, string password )
        {
            //Si el usuario y la clave estan en la base de datos, ingrese

            if ((usertname == "admin") && (password == "pass"))
            {
                //Aca se genera el token para el controlador...
                //ocpamos el generador
                var tokenBase = new JwtSecurityTokenHandler();

                //nuestra llave???
                var nuestrallave = Encoding.UTF8.GetBytes("NosotroscreamosacalaclaveNosotroscreamosacalaclave1234");

                //vamos a la descripcion del token, o generador de security

                var tokenDescripcion = new SecurityTokenDescriptor{
                
                    Subject = new ClaimsIdentity( new Claim[]
                        {
                            new Claim(ClaimTypes.Name, usertname)
                        }
                    ),
                    Expires = DateTime.UtcNow.AddHours(1),
                    Issuer = "https://localhost:7268",
                    Audience = "https://localhost:7268", 
                    SigningCredentials = new SigningCredentials (
                            new SymmetricSecurityKey(nuestrallave),
                            SecurityAlgorithms.HmacSha256Signature
                        )
                };


                var token = tokenBase.CreateToken(tokenDescripcion);
                var tokenString = tokenBase.WriteToken(token);


                return Ok( "Bearer " + tokenString  );

            }

            return Unauthorized("Sin clave no ingresas");

        }

        //GET :api/productos/{id}
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetProductos(int id)
        {
            //Se extraigan los datos desde BD

            if (id < 0 || id >= _products.Count)
            {
                //Aca seria el mensaje que nos devuelve el SP
                return NotFound("Producto no esta en el sistema");
            }
            return Ok(_products[id]);
        }




        //POST :api/productos/
        [HttpPost]
        public IActionResult addProductos([FromBody] string nuevoProducto)
        {
            //Se inserta los datos desde BD

            if ( string.IsNullOrWhiteSpace(nuevoProducto) )
            {
                //Aca seria el mensaje que nos devuelve el SP
                return BadRequest("El nombre esta vacio o esta en blanco");
            }

            _products.Add(nuevoProducto);
            return CreatedAtAction(nameof(GetProductos), new { id = _products.Count - 1 }, nuevoProducto);
            
        }



        //DELETE :api/productos/{id}
        [HttpDelete("{id}")]
        public IActionResult deletetProductos(int id)
        {
            //Se extraigan los datos desde BD

            if (id < 0 || id >= _products.Count)
            {
                //Aca seria el mensaje que nos devuelve el SP
                return NotFound("Producto no esta en el sistema");
            }

            _products.RemoveAt(id);


            return NoContent();
        }



    }
}
