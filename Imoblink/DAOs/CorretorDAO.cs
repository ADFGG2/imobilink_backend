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
    }
}
