using CadastroDeEmpresas.Models;
using Microsoft.EntityFrameworkCore;

namespace CadastroDeEmpresas.Repositories
{
    //classe responsável por se relacionar diretamente à tabela no banco de dados
    public class BaseRepository<T> where T : BaseModel
    {
        protected readonly CadastroContext _context;
        protected readonly DbSet<T> dbSet;

        public BaseRepository(CadastroContext context)
        {
            _context = context;
            dbSet = _context.Set<T>();
        }
    }
}
