using Imoblink.DTOs;
using MySql.Data.MySqlClient;

namespace Imoblink.DAOs
{
    public class ImoveisDAO
    {
       public void CadastrarImovel(ImoveisDTO imovel)
        {
            var conexao = ConnectionFactory.build();
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
            comando.Parameters.AddWithValue("@areasUteis", imovel.areasUteis);
            comando.Parameters.AddWithValue("@CondominiFechado", imovel.CondominioFechado);
            comando.Parameters.AddWithValue("@valor", imovel.velor);
            comando.Parameters.AddWithValue("@status", imovel.status);
            comando.Parameters.AddWithValue("@autorizaFoto", imovel.autorizaFoto);
            comando.Parameters.AddWithValue("@autorizaPlaca", imovel.autorizaPlaca);
            comando.Parameters.AddWithValue("@finalidade", imovel.finalidade);
            comando.Parameters.AddWithValue("@descricao", imovel.descricao);
            comando.Parameters.AddWithValue("@taxaCondo", imovel.taxaCondo);
            comando.Parameters.AddWithValue("@taxaIPTU", imovel.taxaIPTU);

        }
    }
}
