using Imoblink.DTOs;
using MySql.Data.MySqlClient;

namespace Imoblink.DAOs
{
    public class CorretorDAO
    {
        public void CadastrarCorretor(CorretorDTO corretor)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = @"INSERT INTO corretor (Nome_completo, CPF, CRECI, Email, Telefone, senha)
                    VALUES (@Nome_completo, @CPF, @CRECI, @Email, @Telefone, @senha);";
                
            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@Nome_completo", corretor.Nome_completo);
            comando.Parameters.AddWithValue("@CPF", corretor.CPF);
            comando.Parameters.AddWithValue("@CRECI", corretor.CRECI);
            comando.Parameters.AddWithValue("@Email", corretor.Email);
            comando.Parameters.AddWithValue("@Telefone", corretor.Telefone);
            comando.Parameters.AddWithValue("@senha", corretor.senha);
            

            comando.ExecuteNonQuery();
            conexao.Close();
        }

        public void adicionarImagemdePerfil(int id, string url)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = @"update corretor set URL_imagem_Perfil = @url where corretor.CPF = @id;";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@id", id);
            comando.Parameters.AddWithValue("@url", url);

            comando.ExecuteNonQuery();
            conexao.Close();
        }
        
        public void adicionarImovelFavorito(string id,int idImovel)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = @"insert into favoritoscorretora(CNPJ_Corretora, ID_Imovel) values(@id, @idImovel);";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@id", id);
            comando.Parameters.AddWithValue("@idImovel", idImovel);

            comando.ExecuteNonQuery();
            conexao.Close();
        }

        public void removerImovelFavorito(string id, int idImovel)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = @"remove from favoritoscorretora where CNPJ_Corretora = @id and ID_Imovel = @idImovel;";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@id", id);
            comando.Parameters.AddWithValue("@idImovel", idImovel);

            comando.ExecuteNonQuery();
            conexao.Close();
        }
    }
}
