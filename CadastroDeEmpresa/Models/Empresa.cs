using CadastroDeEmpresas.Models;
using System.Runtime.Serialization;

namespace CadastroDeEmpresas.Model
{
    //classe correspondente aos campos da tabela Empresas no banco de dados
    public class Empresa : BaseModel
    {
        public Empresa() { }

        [DataMember]
        public string UF { get; set; }
        [DataMember]
        public string CEP { get; set; }
        [DataMember]
        public string Nome { get; set; }
        [DataMember]
        public string CNPJ { get; set; }
        [DataMember]
        public string Porte { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Situacao { get; set; }
        [DataMember]
        public string Abertura { get; set; }
        [DataMember]
        public string Telefone { get; set; }
        [DataMember]
        public string Fantasia { get; set; }
        [DataMember]
        public string Numero { get; set; }
        [DataMember]
        public string Bairro { get; set; }
        [DataMember]
        public string Municipio { get; set; }
        [DataMember]
        public string Logradouro { get; set; }
        [DataMember]
        public string Complemento { get; set; }
    }
}
