using CadastroDeEmpresas.Model;
using System.Collections.Generic;
using System.Linq;

namespace CadastroDeEmpresas.Repositories
{
    public interface IEmpresaRepository
    {
        IList<Empresa> GetEmpresa(string identificacao);
    }

    //classe responsável por buscar o registro correspondente no banco de dados
    public class EmpresaRepository : BaseRepository<Empresa>, IEmpresaRepository
    {
        public EmpresaRepository(CadastroContext context) : base(context) { }

        public IList<Empresa> GetEmpresa(string identificacao)
        {
            //recebe um valor como parâmetro
            //busca pelo parâmetro correspondente na tabela
            return dbSet
                .Where(emp => emp.CNPJ == identificacao).ToList();
        }

    }
}
