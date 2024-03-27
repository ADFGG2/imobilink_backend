using Imoblink.DTOs;
using MySql.Data.MySqlClient;

namespace Imoblink.DAOs
{
    public class PessoaFisicaDAO
    {
        public void CadastrarPessoaFisica(PessoaFisicaDTO pessoaFisica)
        {
            var conexao = ConnectionFactory.build();
            conexao.Open();

            var query = @"INSERT INTO pessoaFisica (cpf, nome, rg, Email, celular, telefone, senha, 
                       cidade, cep, bairro, notificacao)
                    VALUES (@cpf, @nome, @rg, @Email, @celular, @telefone, @senha, 
                       @cidade, @cep, @bairro, @notificacao);
                    SELECT LAST_INSERT_ID(); ";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@cpf", pessoaFisica.cpf);
            comando.Parameters.AddWithValue("@nome", pessoaFisica.nome);
            comando.Parameters.AddWithValue("rg", pessoaFisica.rg);
            comando.Parameters.AddWithValue("@Email", pessoaFisica.email);
            comando.Parameters.AddWithValue("@celular", pessoaFisica.celular);
            comando.Parameters.AddWithValue("@telefone", pessoaFisica.telefone);
            comando.Parameters.AddWithValue("@senha", pessoaFisica.senha);
            comando.Parameters.AddWithValue("@cidade", pessoaFisica.cidade);
            comando.Parameters.AddWithValue("@cep", pessoaFisica.cep);
            comando.Parameters.AddWithValue("@bairro", pessoaFisica.bairro);
            comando.Parameters.AddWithValue("@notificacao", pessoaFisica.notificacao);

        }
    }
}
