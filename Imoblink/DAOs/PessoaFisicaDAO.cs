using Imoblink.DTOs;
using MySql.Data.MySqlClient;

namespace Imoblink.DAOs
{
    public class PessoaFisicaDAO
    {
        public void CadastrarPessoaFisica(PessoaFisicaDTO pessoaFisica)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = @"INSERT INTO pessoafisica (cpf, nome, rg, Email, telefone, senha, 
                       cidade, cep, bairro, nascimento)
                    VALUES (@cpf, @nome, @rg, @Email, @telefone, @senha, 
                       @cidade, @cep, @bairro, @nascimento); ";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@cpf", pessoaFisica.cpf);
            comando.Parameters.AddWithValue("@nome", pessoaFisica.nome);
            comando.Parameters.AddWithValue("rg", pessoaFisica.rg);
            comando.Parameters.AddWithValue("@Email", pessoaFisica.email);
            comando.Parameters.AddWithValue("@telefone", pessoaFisica.telefone);
            comando.Parameters.AddWithValue("@senha", pessoaFisica.senha);
            comando.Parameters.AddWithValue("@cidade", pessoaFisica.cidade);
            comando.Parameters.AddWithValue("@cep", pessoaFisica.cep);
            comando.Parameters.AddWithValue("@bairro", pessoaFisica.bairro);
            comando.Parameters.AddWithValue("@nascimento", pessoaFisica.nascimento);

            comando.ExecuteNonQuery();
            conexao.Close();
        }

        public void adicionarImagemdePerfil(string id, string url)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = @"update pessoafisica set URL_imagem_Perfil = @id where pessoafisica.cpf;";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@id", id);
            comando.Parameters.AddWithValue("@url", url);

            comando.ExecuteNonQuery();
            conexao.Close();
        }
    }
}
