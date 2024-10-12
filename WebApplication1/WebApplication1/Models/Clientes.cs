using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Clientes
    {
        //Definiendo el modelo o entidad
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }

        [Column("fecha_registro")]
        public DateTime FechaRegisto { get; set; }
    }
}
