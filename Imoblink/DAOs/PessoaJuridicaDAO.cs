using Imoblink.DTOs;
using MySql.Data.MySqlClient;

namespace Imoblink.DAOs
{
    public class PessoaJuridicaDAO
    {
        public void CadastrarPessoaJuridica(PessoaJuridicaDTO pessoaJuridica)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = @"INSERT INTO pessoaJuridica (NomeEmpresa, CNPJ, InscricaoEstadual, email,  telefone, senha, 
                       cidade, cep, bairro)
                    VALUES (@NomeEmpresa, @CNPJ, @InscricaoEstadual, @Email,  @telefone, @senha, 
                       @cidade, @cep, @bairro); ";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@NomeEmpresa", pessoaJuridica.NomeEmpresa);
            comando.Parameters.AddWithValue("@CNPJ", pessoaJuridica.CNPJ);
            comando.Parameters.AddWithValue("InscricaoEstadual", pessoaJuridica.InscricaoEstadual);
            comando.Parameters.AddWithValue("@Email", pessoaJuridica.Email);
            comando.Parameters.AddWithValue("@telefone", pessoaJuridica.Telefone);
            comando.Parameters.AddWithValue("@senha", pessoaJuridica.senha);
            comando.Parameters.AddWithValue("@cidade", pessoaJuridica.cidade);
            comando.Parameters.AddWithValue("@cep", pessoaJuridica.cep);
            comando.Parameters.AddWithValue("@bairro", pessoaJuridica.bairro);

            comando.ExecuteNonQuery();
            conexao.Close();
        }
    }
}
