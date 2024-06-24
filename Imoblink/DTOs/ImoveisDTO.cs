using System.ComponentModel;

namespace Imoblink.DTOs
{
    public class ImoveisDTO
    {
        public int? Codigo { get; set; }
        public int Matricula { get; set; }
        public string Endereco { get; set; }
        public string CEP { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Tipo { get; set; }
        public int Andares { get; set; }
        public int Dormitorios { get; set; }
        public int Suites { get; set; }
        public int Salas { get; set; }
        public int Garagens { get; set; }
        public double AreaUtil { get; set; }
        public Boolean CondominioFechado { get; set; }
        public double Valor { get; set; }
        public string Status { get; set; }
        public Boolean AutorizaFoto { get; set; }
        public Boolean AutorizaPlaca { get; set; }
        public string Finalidade { get; set; }
        public string Descricao { get; set; }
        public double TaxaCondo { get; set; }
        public double TaxaIPTU { get; set; }
        public List<int>? Observacoes { get; set; }
        public string IdDono {  get; set; }
        public string? EmailDono { get; set; }
        public string? TelefonelDono { get; set; }
        public int UnidadesDisponiveis { get; set; }
        public List<string>? ObservacoesNomes { get; set; }
        public string? NomeAutor { get; set; } 
        public int? quantasImagens { get; set; }
       
    }
}
