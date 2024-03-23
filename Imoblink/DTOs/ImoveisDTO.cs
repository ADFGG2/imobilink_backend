using System.ComponentModel;

namespace Imoblink.DTOs
{
    public class ImoveisDTO
    {
                public int Codigo { get; set; }
        public int Matricula { get; set; }
        public int Alugavel { get; set; }
        public int endereco { get; set; }
        public string cep { get; set; }
        public string bairro { get; set; }
        public int cidade { get; set; }
        public string tipo { get; set; }
        public int andares { get; set; }
        public int dormitorios { get; set; }
        public string suites { get; set; }
        public int salas { get; set; }
        public int garagens { get; set; }

        public int areasUteis { get; set; }
        public int CondominioFechado { get; set; }
        public int velor { get; set; }
        public int status { get; set; }
        public int autorizaFoto { get; set; }
        public int autorizaPlaca { get; set; }
        public string finalidade { get; set; }
        public string descricao { get; set; }
        public  int taxaCondo { get; set; }
        public int taxaIPTU { get; set; }
    

    }
}
