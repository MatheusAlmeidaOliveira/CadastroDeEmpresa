using System;
using System.Net.Http;
using System.Threading.Tasks;
using CadastroDeEmpresas.Model;
using Microsoft.AspNetCore.Mvc;
using CadastroDeEmpresas.Repositories;

namespace CadastroDeEmpresas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CadastroController : Controller
    {
        private readonly CadastroContext _cadastroContext;
        private readonly IEmpresaRepository _empresaRepository;

        public CadastroController(CadastroContext cadastroContext,
            IEmpresaRepository empresaRepository)
        {
            _cadastroContext = cadastroContext;
            _empresaRepository = empresaRepository;
        }
        
        //Action do menu principal
        [HttpGet]
        public IActionResult Menu()
        {
            return View();
        }

        //Action com o campo para digitar o cnpj para busca
        [HttpGet("index")]
        public IActionResult Index()
        {
            return View();
        }

        //Action com campo para digitar o registro a ser buscado no banco de dados
        [HttpGet("buscarempresas")]
        public IActionResult BuscarEmpresas()
        {
            return View();
        }

        //Action que realiza a conexão com a api da receita federal
        [HttpGet("cadastroasync/{cnpj?}")]
        public async Task<IActionResult> CadastroAsync(string cnpj)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://www.receitaws.com.br/v1/cnpj/");
            cnpj = ValidarCnpj(cnpj);
            
            HttpResponseMessage resposta = await httpClient.GetAsync($"{cnpj}");

            //Converte o json para os campos da classe Empresa
            var empresa = await resposta.Content.ReadAsAsync<Empresa>();
            if (empresa.CNPJ == null)
            {
                throw new Exception("Não há informações sobre o CNPJ na Receita federal!");
            }

            return View(empresa);
        }

        //valida o cnpj
        //retira os espaços em brancos e caracteres especiais para que o cnpj seja enviado para a api
        private static string ValidarCnpj(string cnpj)
        {
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "").Replace(" ", "");

            if (!long.TryParse(cnpj, out long validar))
            {
                throw new Exception("CNPJ inválido, digite somente números e os seguintes simbolos: '.', '-', '/'");
            }
            return cnpj;
        }

        //Adiciona as informações da empresa no banco de dados
        [HttpPost]
        public IActionResult Adicionar(Empresa empresa)
        {
            var lista = _empresaRepository.GetEmpresa(empresa.CNPJ);
            if (lista.Count != 0)
            {
                throw new Exception("Empresa já está cadastrada, consulte-a em Buscar Empresas");
            }

            //adiciona os campos
            _cadastroContext.Empresas.Add(empresa);
            //efetiva a adição dos novos registros no banco de dados
            _cadastroContext.SaveChanges();

            return View();
        }

        //busca pelo registo no banco de dados
        [HttpPost("buscar/{identificacao?}")]
        public IActionResult Buscar()
        {
            //pega o valor digitado pelo usuário no campo de busca
            var identificacao = Request.Form["cnpj"].ToString();
            //busca pelo registro correspondente no banco de dados
            var lista = _empresaRepository.GetEmpresa(identificacao);
            if (lista.Count == 0)
            {
                throw new Exception("Empresa não está cadastrada, cadestre-a em Adicionar Empresas");
            }

            return View(lista);
        }

        //remove o registo selecionado do banco de dados
        [HttpGet("remover/{id?}")]
        public IActionResult Remover(int id)
        {
            var empresa = _cadastroContext.Empresas.Find(id);
            if (empresa != null)
            {
                //remove o registro do banco de dados
                _cadastroContext.Empresas.Remove(empresa);
                //efetiva a remoção
                _cadastroContext.SaveChanges();

                return View();
            }

            return RedirectToAction("Adicionar");
        }

    }
}