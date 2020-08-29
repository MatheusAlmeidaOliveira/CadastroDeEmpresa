using CadastroDeEmpresas.Model;
using Microsoft.EntityFrameworkCore;

namespace CadastroDeEmpresas
{
    public class CadastroContext : DbContext
    {
        //Define o nome da tabela no banco de dados
        public DbSet<Empresa> Empresas { get; set; }
        
        public CadastroContext(DbContextOptions<CadastroContext> option) : base(option)
        {
            //Verifica se o banco foi criado, caso não, ele é criado
            Database.EnsureCreated();
        }
    }
}
