namespace WebApplication1.Models
{
    public class Cuentas
    {
        //Definiendo el modelo o entidad
        public int Id { get; set; }

        public string NumeroCuenta { get; set; }

        public decimal Saldo { get; set; }

        public int ClientId { get; set; }


    }
}
