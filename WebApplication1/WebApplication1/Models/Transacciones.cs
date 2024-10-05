namespace WebApplication1.Models
{
    public class Transacciones
    {
        public int Id { get; set; }

        public int CuentaId { get; set; }

        public decimal Monto { get; set; }

        public DateTime Fecha { get; set; }
        
    }
}
