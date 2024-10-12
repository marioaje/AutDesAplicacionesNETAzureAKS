using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Cryptography;
using WebApplication1.Data;
using WebApplication1.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly BancoDbContext _contextAccess;
        ////SELECT * FROM AKS1.ProfM_Clientes;id, nombre, apellido, tele
        /////MySqlConnector.MySqlException: 'Table 'AKS1.Clientes' doesn't exist'
        //public int Id { get; set; }
        //public string Nombre { get; set; }
        //public string Apellido { get; set; }
        //public string Email { get; set; }
        //public string Telefono { get; set; }
        //public string Direccion { get; set; }
        //public DateTime FechaRegisto { get; set; }
        public ClientesController(BancoDbContext contextAccess)
        {
            _contextAccess = contextAccess;
        }


        [HttpGet]
        public ActionResult<IEnumerable<Clientes>> ConsultarClientes()
        {
            return Ok(_contextAccess.ProfM_Clientes.ToList());
        }

        [HttpGet("{_id}")]
        public ActionResult<IEnumerable<Clientes>> ConsultarClientes(int _id)
        {
            var _clientes = _contextAccess.ProfM_Clientes.Find(_id);

            if (_clientes == null) {
                return NotFound("El cliente no esta en el sistema");
            }


            return Ok(_clientes);
        }

        [HttpPost]
        public IActionResult AgregarClientes(Clientes _clientes)
        {
            try
            {

                _contextAccess.ProfM_Clientes.Add(_clientes);
                _contextAccess.SaveChanges();

                return CreatedAtAction(nameof (ConsultarClientes),
                                            new
                                            {
                                                id = _clientes.Id,
                                            },
                                            _clientes
                                        );
            }
            catch (Exception ex) {
                  return StatusCode(500, ex.Message);
            }

        }


        [HttpPut]
        public ActionResult ModificarClientes(Clientes _clientes)
        {
            try
            {
                //var _clientesRegisto = _contextAccess.ProfM_Clientes.Find(_clientes.Id);

                //if (_clientesRegisto == null)
                //{
                //    return NotFound("El cliente no esta en el sistema");
                //}

                //_contextAccess.Entry(_clientesRegisto).State = EntityState.Detached;

                if (!ConsultarExistencia(_clientes.Id))
                {
                    return NotFound("El cliente no esta en el sistema");
                }

                _contextAccess.Entry(_clientes).State = EntityState.Modified;

                _contextAccess.SaveChanges();
                return Ok(_clientes);
            }
            catch (Exception ex) {

                return StatusCode(500, ex.Message);

            }
            
        }


        [HttpDelete("{_id}")]
        public ActionResult EliminarClientes(int _id)
        {
            try 
            { 

                if (!ConsultarExistencia(_id))
                {
                    return NotFound("El cliente no esta en el sistema");
                }
                var _clientes = _contextAccess.ProfM_Clientes.Find(_id);

                _contextAccess.ProfM_Clientes.Remove(_clientes);
                _contextAccess.SaveChanges();
                return Ok($"Se eilimina el registro {_id}");

            }
            catch (Exception ex) {

                        return StatusCode(500, ex.Message);

            }
    }


        private bool ConsultarExistencia(int _id)
        {   
            return _contextAccess.ProfM_Clientes.Any(c => c.Id == _id);
        }


        ////DELETE :api/productos/{id}
        //[HttpDelete("{id}")]
        //public IActionResult deletetProductos(int id)
        //{
        //    //Se extraigan los datos desde BD

        //    if (id < 0 || id >= _products.Count)
        //    {
        //        //Aca seria el mensaje que nos devuelve el SP
        //        return NotFound("Producto no esta en el sistema");
        //    }

        //    _products.RemoveAt(id);


        //    return NoContent();
        //}



    }
}
