using Imoblink.DTOs;
using MySql.Data.MySqlClient;

namespace Imoblink.DAOs
{
    public class CorretorDAO
    {
        public void CadastrarCorretor(CorretorDTO corretor)
        {
            var conexao = ConnectionFactory.build();
            conexao.Open();

            var query = @"INSERT INTO corretor (Nome_completo, CPF, CRECI, Email, celular, Telefone, senha, 
                       notifica)
                    VALUES (@Nome_completo, @CPF, @CRECI, @Email, @celular, @Telefone, @senha, 
                       @notifica);
                    SELECT LAST_INSERT_ID(); ";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@Nome_completo", corretor.Nome_completo);
            comando.Parameters.AddWithValue("@CPF", corretor.CPF);
            comando.Parameters.AddWithValue("@CRECI", corretor.CRECI);
            comando.Parameters.AddWithValue("@Email", corretor.Email);
            comando.Parameters.AddWithValue("@celular", corretor.Celular);
            comando.Parameters.AddWithValue("@Telefone", corretor.Telefone);
            comando.Parameters.AddWithValue("@senha", corretor.senha);
            comando.Parameters.AddWithValue("@notifica", corretor.notifica);
        }
    }
}
