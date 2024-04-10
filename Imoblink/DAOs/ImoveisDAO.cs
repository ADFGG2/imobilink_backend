using Imoblink.DTOs;
using MySql.Data.MySqlClient;
using Mysqlx;

namespace Imoblink.DAOs
{
    public class ImoveisDAO
    {
        public void CadastrarImovel(ImoveisDTO imovel)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = @"INSERT INTO imoveis (Codigo, matricula, Alugavel, endereco, cep, bairro, cidade, tipo, andares, dormitorios
                          suites, salas, garagens, areasUteis, CondominioFechado, valor,status, autorizaFoto, autorizaPlaca, finalidade
                           descricao, taxaCondo, taxaIPTU)
                    VALUES (@Codigo, @matricula, @Alugavel, @endereco, @cep, @bairro, @cidade, @tipo, @andares, @dormitorios
                          @suites, @salas, @garagens, @areasUteis, @CondominioFechado, @valor, @status, @autorizaFoto, @autorizaPlaca, @finalidade
                           @descricao, @taxaCondo, @taxaIPTU);
                    SELECT LAST_INSERT_ID(); ";


            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@Codigo", imovel.Codigo);
            comando.Parameters.AddWithValue("@matricula", imovel.Matricula);
            comando.Parameters.AddWithValue("@Alugavel", imovel.Alugavel);
            comando.Parameters.AddWithValue("@endereco", imovel.endereco);
            comando.Parameters.AddWithValue("@cep", imovel.cep);
            comando.Parameters.AddWithValue("@bairro", imovel.bairro);
            comando.Parameters.AddWithValue("@cidade", imovel.cidade);
            comando.Parameters.AddWithValue("@tipo", imovel.tipo);
            comando.Parameters.AddWithValue("@andares", imovel.andares);
            comando.Parameters.AddWithValue("@dormitorios", imovel.dormitorios);
            comando.Parameters.AddWithValue("@suites", imovel.suites);
            comando.Parameters.AddWithValue("@salas", imovel.salas);
            comando.Parameters.AddWithValue("@garagens", imovel.garagens);
            comando.Parameters.AddWithValue("@areasUteis", imovel.areaUtil);
            comando.Parameters.AddWithValue("@CondominiFechado", imovel.CondominioFechado);
            comando.Parameters.AddWithValue("@valor", imovel.valor);
            comando.Parameters.AddWithValue("@status", imovel.status);
            comando.Parameters.AddWithValue("@autorizaFoto", imovel.autorizaFoto);
            comando.Parameters.AddWithValue("@autorizaPlaca", imovel.autorizaPlaca);
            comando.Parameters.AddWithValue("@finalidade", imovel.finalidade);
            comando.Parameters.AddWithValue("@descricao", imovel.descricao);
            comando.Parameters.AddWithValue("@taxaCondo", imovel.taxaCondo);
            comando.Parameters.AddWithValue("@taxaIPTU", imovel.taxaIPTU);

            comando.ExecuteNonQuery();
            conexao.Close();
        }

      

        public List<ImoveisDTO> ListarImoveis()
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = "SELECT*FROM Imoveis";

            var comando = new MySqlCommand(query, conexao);
            var dataReader = comando.ExecuteReader();

            var imoveis = new List<ImoveisDTO>();


            while (dataReader.Read())
            {
                var imovel = new ImoveisDTO();
                imovel.Codigo = int.Parse(dataReader["ID"].ToString());
                imovel.Alugavel = int.Parse(dataReader["Alugavel"].ToString());
                imovel.endereco = dataReader["endereco"].ToString();
                imovel.cep = dataReader["cep"].ToString();
                imovel.bairro = dataReader["bairro"].ToString();
                imovel.cidade = dataReader["cidade"].ToString();
                imovel.tipo = dataReader["tipo"].ToString();
                imovel.andares = int.Parse(dataReader["andares"].ToString());
                imovel.dormitorios = int.Parse( dataReader["dormitorios"].ToString());
                imovel.suites = dataReader["suites"].ToString();
                imovel.salas = int.Parse(dataReader["salas"].ToString());
                imovel.garagens = int.Parse(dataReader["cidade"].ToString());
                imovel.areaUtil = int.Parse(dataReader["areaUtil"].ToString());
                imovel.CondominioFechado = int.Parse( dataReader["CondominioFechado"].ToString()); 
                imovel.valor = int.Parse(dataReader["valor"].ToString());
                imovel.status = int.Parse(dataReader["status"].ToString());
                imovel.autorizaFoto = int.Parse(dataReader["autorizaFoto"].ToString());
                imovel.autorizaPlaca = int.Parse(dataReader["cidade"].ToString());
                imovel.finalidade = dataReader["finalidade"].ToString();
                imovel.descricao = dataReader["~descricao"].ToString();
                imovel.taxaCondo = int.Parse(dataReader["cidade"].ToString());
                imovel.taxaIPTU = int.Parse(dataReader["taxaIPTU"].ToString());

             imoveis.Add(imovel);               

            }
            return imoveis;
        }
    }
}
