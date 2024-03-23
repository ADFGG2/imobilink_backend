using Imoblink.DTOs;
using MySql.Data.MySqlClient;

namespace Imoblink.DAOs
{
    public class PessoaJuridicaDAO
    {
        public void CadastrarPessoaJuridica(PessoaJuridicaDTO pessoaJuridica)
        {
            var conexao = ConnectionFactory.build();
            conexao.Open();

            var query = @"INSERT INTO pessoaJuridica (NomeEmpresa, CNPJ, InscricaoEstadual, email, celular, telefone, senha, 
                       cidade, cep, bairro)
                    VALUES (@NomeEmpresa, @CNPJ, @InscricaoEstadual, @Email, @celular, @telefone, @senha, 
                       @cidade, @cep, @bairro, @notificacao);
                    SELECT LAST_INSERT_ID(); ";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@NomeEmpresa", pessoaJuridica.NomeEmpresa);
            comando.Parameters.AddWithValue("@CNPJ", pessoaJuridica.CNPJ);
            comando.Parameters.AddWithValue("rg", pessoaJuridica.InscricaoEstadual);
            comando.Parameters.AddWithValue("@Email", pessoaJuridica.Email);
            comando.Parameters.AddWithValue("@celular", pessoaJuridica.Celular);
            comando.Parameters.AddWithValue("@telefone", pessoaJuridica.Telefone);
            comando.Parameters.AddWithValue("@senha", pessoaJuridica.senha);
            comando.Parameters.AddWithValue("@cidade", pessoaJuridica.cidade);
            comando.Parameters.AddWithValue("@cep", pessoaJuridica.cep);
            comando.Parameters.AddWithValue("@bairro", pessoaJuridica.bairro);
        }
    }
}
