using Microsoft.AspNetCore.Mvc;

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
        [HttpGet]
        public IActionResult GetProductos()
        {
            //Se extraigan los datos desde BD
            return Ok(_products);
        }


        //GET :api/productos/{id}
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
