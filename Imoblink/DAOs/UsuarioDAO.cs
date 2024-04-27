using Imoblink.DTOs;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace Imoblink.DAOs
{
    public class UsuarioDAO
    {

        public int VerificaQualTipoDeLogin(UsuarioDTO usuario)
        {
            var conexao = ConnectionFactory.Build();
       
            var query1 = "SELECT CNPJ as id, senha FROM imobiliaria WHERE CNPJ = @user and senha = @senha";
            var query2 = "SELECT CPF as id, senha FROM corretor WHERE CPF = @user and senha = @senha";
            var query3 = "SELECT CPF as id, senha FROM pessoafisica WHERE CPF = @user and senha = @senha";
            var query4 = "SELECT CNPJ as id, senha FROM pessoajuridica WHERE CNPJ = @user and senha = @senha";

            //-------------------------------------------------------------------------------------------

            for (int i = 1; i <= 4; i++)
            {
                conexao.Open();

                var comando = new MySqlCommand();
                if (i == 1)
                {
                    comando = new MySqlCommand(query1, conexao);
                }
                else if (i == 2)
                {
                    comando = new MySqlCommand(query2, conexao);
                }
                else if (i == 3)
                {
                    comando = new MySqlCommand(query3, conexao);
                }
                else if (i == 4)
                {
                    comando = new MySqlCommand(query4, conexao);
                }

                comando.Parameters.AddWithValue("@user", usuario.User);
                comando.Parameters.AddWithValue("@senha", usuario.Senha);

                var dataReader = comando.ExecuteReader();

                while (dataReader.Read())
                {
                    return i;
                }

                conexao.Close();
            }


            return 0;
        }



        public ImobiliariaDTO LoginImobiliaria(UsuarioDTO usuario)
        {

            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = "SELECT * FROM imobiliaria WHERE CNPJ = @user and senha = @senha";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@user", usuario.User);
            comando.Parameters.AddWithValue("@senha", usuario.Senha);

            var dataReader = comando.ExecuteReader();

            var imobiliaria = new ImobiliariaDTO();

            while (dataReader.Read())
            {
                imobiliaria.RazaoSocial = dataReader["RazaoSocial"].ToString();
                imobiliaria.CNPJ = dataReader["CNPJ"].ToString();
                imobiliaria.Email = dataReader["email"].ToString();
                imobiliaria.CRECI = dataReader["CRECI"].ToString();
                imobiliaria.RepresentanteLegal = dataReader["representantelegal"].ToString();
                imobiliaria.cidade = dataReader["cidade"].ToString();
                imobiliaria.cep = dataReader["cep"].ToString();
                imobiliaria.bairro = dataReader["bairro"].ToString();
                imobiliaria.Telefone = dataReader["Telefone"].ToString();
            }

            conexao.Close();

            return imobiliaria;
        }

        public CorretorDTO LoginCorretor(UsuarioDTO usuario)
        {

            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = "SELECT * FROM corretor WHERE CPF = @user and senha = @senha";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@user", usuario.User);
            comando.Parameters.AddWithValue("@senha", usuario.Senha);

            var dataReader = comando.ExecuteReader();

            var corretor = new CorretorDTO();

            while (dataReader.Read())
            {
                corretor.Nome_completo = dataReader["nome_completo"].ToString();
                corretor.CRECI = dataReader["CRECI"].ToString();
                corretor.Email = dataReader["email"].ToString();
                corretor.CPF = dataReader["cpf"].ToString();
                corretor.Telefone = dataReader["telefone"].ToString();
            }

            conexao.Close();

            return corretor;
        }

        public PessoaJuridicaDTO LoginJuridica(UsuarioDTO usuario)
        {
            
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = "SELECT * FROM pessoaJuridica WHERE CNPJ = @user and senha = @senha";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@user", usuario.User);
            comando.Parameters.AddWithValue("@senha", usuario.Senha);

            var dataReader = comando.ExecuteReader();

            var pessoaJuridica = new PessoaJuridicaDTO();

            while (dataReader.Read())
            {
                pessoaJuridica.NomeEmpresa = dataReader["NomeEmpresa"].ToString();
                pessoaJuridica.CNPJ = dataReader["CNPJ"].ToString();
                pessoaJuridica.InscricaoEstadual = dataReader["Inscricaoestadual"].ToString();
                pessoaJuridica.Email = dataReader["Email"].ToString();
                pessoaJuridica.Telefone = dataReader["Telefone"].ToString();
                pessoaJuridica.cidade = dataReader["cidade"].ToString();
                pessoaJuridica.cep = dataReader["cep"].ToString();
                pessoaJuridica.bairro = dataReader["bairro"].ToString();

            }

            conexao.Close();

            return pessoaJuridica;
        }

        public PessoaFisicaDTO LoginFisica(UsuarioDTO usuario)
        {

            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = "SELECT * FROM pessoafisica WHERE cpf = @user and senha = @senha";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@user", usuario.User);
            comando.Parameters.AddWithValue("@senha", usuario.Senha);

            var dataReader = comando.ExecuteReader();

            var pessoafisica = new PessoaFisicaDTO();

            while (dataReader.Read())
            {
                pessoafisica.nome = dataReader["nome"].ToString();
                pessoafisica.cpf = dataReader["CPF"].ToString();
                pessoafisica.rg = dataReader["rg"].ToString();
                pessoafisica.email = dataReader["email"].ToString();
                pessoafisica.telefone = dataReader["telefone"].ToString();
                pessoafisica.cidade = dataReader["cidade"].ToString();
                pessoafisica.cep = dataReader["cep"].ToString();
                pessoafisica.bairro = dataReader["bairro"].ToString();
            }

            conexao.Close();

            return pessoafisica;
        }
    }
}
