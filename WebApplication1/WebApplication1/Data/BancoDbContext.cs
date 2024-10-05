using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{

    //Heredamos de la base del entity, contexto de base de datos
    public class BancoDbContext : DbContext
    {
            public BancoDbContext(DbContextOptions <BancoDbContext> options )
            {

            }

            public DbSet<Clientes> Clientes { get; set; }
            public DbSet<Cuentas> Cuentas { get; set; }
            public DbSet<Transacciones> Transacciones { get; set; }
   


        //Se interlacen las clases modelo contra el acceso de datos o (DATA)


        //Nos genera interacion con la Base de datos
        //public class Cuentas
        //public class Transacciones

        //public class Clientes

   

        //public DbSet<Cliente> Clientes { get; set; }
        //public DbSet<Cuenta> Cuentas { get; set; }
        //public DbSet<Transaccion> Transacciones { get; set; }
    }
}
