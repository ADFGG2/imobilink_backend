using Imoblink.DTOs;
using MySql.Data.MySqlClient;

namespace Imoblink.DAOs
{
    public class ImobiliariaDAO
    {
        public void CadastrarImobiliaria(ImobiliariaDTO imobiliaria)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = @"INSERT INTO imobiliaria (RazaoSocial, CNPJ, RepresentanteLegal, CRECI, Email, senha, cep, 
                       cidade, bairro, telefone)
                    VALUES (@RazaoSocial, @CNPJ, @RepresentanteLegal, @CRECI, @Email, @senha, @cep,
                       @cidade, @bairro, @telefone); ";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@RazaoSocial", imobiliaria.RazaoSocial);
            comando.Parameters.AddWithValue("@CNPJ", imobiliaria.CNPJ);
            comando.Parameters.AddWithValue("@RepresentanteLegal", imobiliaria.RepresentanteLegal   );
            comando.Parameters.AddWithValue("@CRECI", imobiliaria.CRECI);
            comando.Parameters.AddWithValue("@Email", imobiliaria.Email);
            comando.Parameters.AddWithValue("@senha", imobiliaria.senha);
            comando.Parameters.AddWithValue("@cep", imobiliaria.cep);
            comando.Parameters.AddWithValue("@cidade", imobiliaria.cidade);
            comando.Parameters.AddWithValue("@bairro", imobiliaria.bairro);
            comando.Parameters.AddWithValue("@telefone", imobiliaria.Telefone);


            comando.ExecuteNonQuery();
            conexao.Close();
        }

        public void adicionarImagemdePerfil(int id, string url)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = @"update imobiliaria set URL_imagem_Perfil = @id where imobiliaria.CNPJ ;";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@id", id);
            comando.Parameters.AddWithValue("@url", url);

            comando.ExecuteNonQuery();
            conexao.Close();
        }
    }
}
