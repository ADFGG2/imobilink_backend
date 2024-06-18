using Imoblink.DTOs;
using MySql.Data.MySqlClient;
using Mysqlx;
using Mysqlx.Expr;

namespace Imoblink.DAOs
{
    public class ImoveisDAO
    {
        public void CadastrarImovel(ImoveisDTO imovel)
        {
            if (!donoExiste(imovel.IdDono))
            {
                return;
            }
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = @"INSERT INTO imoveis (matricula, endereco, cep, bairro, cidade, tipo, andares, dormitorios,
                          suites, salas, garagens, areasUteis, CondominioFechado, valor,status, autorizaFoto, autorizaPlaca, finalidade,
                           descricao, taxaCondo, taxaIPTU, unidadesDisponiveis)
                    VALUES (@matricula, @endereco, @cep, @bairro, @cidade, @tipo, @andares, @dormitorios,
                          @suites, @salas, @garagens, @areasUteis, @CondominioFechado, @valor, @status, @autorizaFoto, @autorizaPlaca, @finalidade,
                           @descricao, @taxaCondo, @taxaIPTU, @unidadesDisponiveis);
                    SELECT LAST_INSERT_ID(); ";


            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@Codigo", imovel.Codigo);
            comando.Parameters.AddWithValue("@matricula", imovel.Matricula);
            comando.Parameters.AddWithValue("@endereco", imovel.Endereco);
            comando.Parameters.AddWithValue("@cep", imovel.CEP);
            comando.Parameters.AddWithValue("@bairro", imovel.Bairro);
            comando.Parameters.AddWithValue("@cidade", imovel.Cidade);
            comando.Parameters.AddWithValue("@tipo", imovel.Tipo);
            comando.Parameters.AddWithValue("@andares", imovel.Andares);
            comando.Parameters.AddWithValue("@dormitorios", imovel.Dormitorios);
            comando.Parameters.AddWithValue("@suites", imovel.Suites);
            comando.Parameters.AddWithValue("@salas", imovel.Salas);
            comando.Parameters.AddWithValue("@garagens", imovel.Garagens);
            comando.Parameters.AddWithValue("@areasUteis", imovel.AreaUtil);
            comando.Parameters.AddWithValue("@CondominioFechado", imovel.CondominioFechado);
            comando.Parameters.AddWithValue("@valor", imovel.Valor);
            comando.Parameters.AddWithValue("@status", imovel.Status);
            comando.Parameters.AddWithValue("@autorizaFoto", imovel.AutorizaFoto);
            comando.Parameters.AddWithValue("@autorizaPlaca", imovel.AutorizaPlaca);
            comando.Parameters.AddWithValue("@finalidade", imovel.Finalidade);
            comando.Parameters.AddWithValue("@descricao", imovel.Descricao);
            comando.Parameters.AddWithValue("@taxaCondo", imovel.TaxaCondo);
            comando.Parameters.AddWithValue("@taxaIPTU", imovel.TaxaIPTU);
            comando.Parameters.AddWithValue("@unidadesDisponiveis", imovel.UnidadesDisponiveis);

            int id_imovel = Convert.ToInt32(comando.ExecuteScalar());
            conexao.Close();

            cadastrarObservacoes(imovel.Observacoes, id_imovel);
            cadastrarImovelDono(id_imovel, imovel.IdDono);
        }

        public void cadastrarObservacoes(List<int> observacoes, int id)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            foreach (int obcervacao in observacoes)
            {
                var query = @"INSERT INTO observacoes_imovel(id_observacoes, id_imovel) VALUES(@observacoes, @imovel)";
                var comando = new MySqlCommand(query, conexao);
                comando.Parameters.AddWithValue("@observacoes", obcervacao);
                comando.Parameters.AddWithValue("@imovel", id);
                comando.ExecuteNonQuery();
            }
            conexao.Close();
        }
        public bool donoExiste(string id)
        {
            var usuarioDAO = new UsuarioDAO();
            if (usuarioDAO.VerificaTipoDeUsuario(id) != "")
            {
                return true;
            }
            return false;
        }

        public void cadastrarImovelDono(int id_imovel, string id_dono)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();
            var usuarioDAO = new UsuarioDAO();
            var tipo = usuarioDAO.VerificaTipoDeUsuario(id_dono);
            var query1 = @"INSERT INTO imovel_pessoafisica(cpf_pessoaFisica, codigo_imovel) VALUES(@dono, @imovel)";
            var query2 = @"INSERT INTO imovel_pessoajuridica(CNPJ_pessoaJuridica, codigo_imovel) VALUES(@dono, @imovel)";

            if (tipo == "PF")
            {
                var comando = new MySqlCommand(query1, conexao);
                comando.Parameters.AddWithValue("@dono", id_dono);
                comando.Parameters.AddWithValue("@imovel", id_imovel);
                comando.ExecuteNonQuery();
            }
            else if (tipo == "PJ")
            {
                var comando = new MySqlCommand(query2, conexao);
                comando.Parameters.AddWithValue("@dono", id_dono);
                comando.Parameters.AddWithValue("@imovel", id_imovel);
                comando.ExecuteNonQuery();
            }

            conexao.Close();
        }

        public List<ImoveisDTO> ListarImoveis()
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = "SELECT imoveis.*, COUNT(imovel_images.id) as quantas_imagens FROM imoveis LEFT JOIN imovel_images ON imovel.Codigo = imagem.id_imovel GROUP BY imoveis.Codigo HAVING COUNT(imovel_images.id) > 5;";

            var comando = new MySqlCommand(query, conexao);
            var dataReader = comando.ExecuteReader();

            var imoveis = new List<ImoveisDTO>();


            while (dataReader.Read())
            {
                var imovel = new ImoveisDTO();
                imovel.Codigo = int.Parse(dataReader["Codigo"].ToString());
                imovel.Endereco = dataReader["endereco"].ToString();
                imovel.CEP = dataReader["cep"].ToString();
                imovel.Bairro = dataReader["bairro"].ToString();
                imovel.Cidade = dataReader["cidade"].ToString();
                imovel.Tipo = dataReader["tipo"].ToString();
                imovel.Andares = int.Parse(dataReader["andares"].ToString());
                imovel.Dormitorios = int.Parse(dataReader["dormitorios"].ToString());
                imovel.Suites = int.Parse(dataReader["suites"].ToString());
                imovel.Salas = int.Parse(dataReader["salas"].ToString());
                imovel.Garagens = int.Parse(dataReader["garagens"].ToString());
                imovel.AreaUtil = int.Parse(dataReader["areasUteis"].ToString());
                imovel.CondominioFechado = int.Parse(dataReader["CondominioFechado"].ToString()).Equals(1);
                imovel.Valor = int.Parse(dataReader["valor"].ToString());
                imovel.Status = dataReader["status"].ToString();
                imovel.AutorizaFoto = dataReader["autorizaFoto"].ToString().Equals(1);
                imovel.AutorizaPlaca = int.Parse(dataReader["autorizaPlaca"].ToString()).Equals(1);
                imovel.Finalidade = dataReader["finalidade"].ToString();
                imovel.Descricao = dataReader["descricao"].ToString();
                imovel.TaxaCondo = int.Parse(dataReader["taxaCondo"].ToString());
                imovel.TaxaIPTU = int.Parse(dataReader["taxaIPTU"].ToString());
                imovel.UnidadesDisponiveis = int.Parse(dataReader["unidadesDisponiveis"].ToString());
                imovel.quantasImagens = int.Parse(dataReader["quantas_imagens"].ToString());
                int id = (int)imovel.Codigo;
                imovel.ObservacoesNomes = PegaObservacoes(id);
                imovel.NomeAutor = PegaNomeDono(id);

                imoveis.Add(imovel);

            }
            return imoveis;
        }
        public List<ImoveisDTO> ListarMeusImoveisCnpj(string cnpj)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = "SELECT Imoveis.*, COUNT(imovel_images.id) as quantas_imagens FROM Imoveis inner join imovel_pessoajuridica on Imoveis.Codigo = imovel_pessoajuridica.codigo_imovel LEFT JOIN imovel_images ON imoveis.Codigo = imovel_images.id_imovel where imovel_pessoajuridica.CNPJ_pessoaJuridica = @cnpj GROUP BY imoveis.Codigo";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@cnpj", cnpj);
            var dataReader = comando.ExecuteReader();

            var imoveis = new List<ImoveisDTO>();


            while (dataReader.Read())
            {
                var imovel = new ImoveisDTO();
                imovel.Codigo = int.Parse(dataReader["Codigo"].ToString());
                imovel.Endereco = dataReader["endereco"].ToString();
                imovel.CEP = dataReader["cep"].ToString();
                imovel.Bairro = dataReader["bairro"].ToString();
                imovel.Cidade = dataReader["cidade"].ToString();
                imovel.Tipo = dataReader["tipo"].ToString();
                imovel.Andares = int.Parse(dataReader["andares"].ToString());
                imovel.Dormitorios = int.Parse(dataReader["dormitorios"].ToString());
                imovel.Suites = int.Parse(dataReader["suites"].ToString());
                imovel.Salas = int.Parse(dataReader["salas"].ToString());
                imovel.Garagens = int.Parse(dataReader["garagens"].ToString());
                imovel.AreaUtil = int.Parse(dataReader["areasUteis"].ToString());
                imovel.CondominioFechado = int.Parse(dataReader["CondominioFechado"].ToString()).Equals(1);
                imovel.Valor = int.Parse(dataReader["valor"].ToString());
                imovel.Status = dataReader["status"].ToString();
                imovel.AutorizaFoto = dataReader["autorizaFoto"].ToString().Equals(1);
                imovel.AutorizaPlaca = int.Parse(dataReader["autorizaPlaca"].ToString()).Equals(1);
                imovel.Finalidade = dataReader["finalidade"].ToString();
                imovel.Descricao = dataReader["descricao"].ToString();
                imovel.TaxaCondo = int.Parse(dataReader["taxaCondo"].ToString());
                imovel.TaxaIPTU = int.Parse(dataReader["taxaIPTU"].ToString());
                imovel.UnidadesDisponiveis = int.Parse(dataReader["unidadesDisponiveis"].ToString());
                imovel.quantasImagens = int.Parse(dataReader["quantas_imagens"].ToString());
                int id = (int)imovel.Codigo;
                imovel.ObservacoesNomes = PegaObservacoes(id);
                imovel.NomeAutor = PegaNomeDono(id);

                imoveis.Add(imovel);
            }
            return imoveis;
        }
        public List<ImoveisDTO> ListarMeusImoveisCpf(string cpf)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = "select Imoveis.*, COUNT(imovel_images.id) as quantas_imagens from imoblink.imoveis inner join imoblink.imovel_pessoafisica on imoblink.imoveis.Codigo = imoblink.imovel_pessoafisica.codigo_imovel LEFT JOIN imovel_images  ON imoveis.Codigo = imovel_images.id_imovel where imoblink.imovel_pessoafisica.cpf_pessoaFisica  = @cpf GROUP BY imoveis.Codigo";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@cpf", cpf);
            var dataReader = comando.ExecuteReader();

            var imoveis = new List<ImoveisDTO>();


            while (dataReader.Read())
            {
                var imovel = new ImoveisDTO();
                imovel.Codigo = int.Parse(dataReader["Codigo"].ToString());
                imovel.Endereco = dataReader["endereco"].ToString();
                imovel.CEP = dataReader["cep"].ToString();
                imovel.Bairro = dataReader["bairro"].ToString();
                imovel.Cidade = dataReader["cidade"].ToString();
                imovel.Tipo = dataReader["tipo"].ToString();
                imovel.Andares = int.Parse(dataReader["andares"].ToString());
                imovel.Dormitorios = int.Parse(dataReader["dormitorios"].ToString());
                imovel.Suites = int.Parse(dataReader["suites"].ToString());
                imovel.Salas = int.Parse(dataReader["salas"].ToString());
                imovel.Garagens = int.Parse(dataReader["garagens"].ToString());
                imovel.AreaUtil = int.Parse(dataReader["areasUteis"].ToString());
                imovel.CondominioFechado = int.Parse(dataReader["CondominioFechado"].ToString()).Equals(1);
                imovel.Valor = double.Parse(dataReader["valor"].ToString());
                imovel.Status = dataReader["status"].ToString();
                imovel.AutorizaFoto = dataReader["autorizaFoto"].ToString().Equals(1);
                imovel.AutorizaPlaca = int.Parse(dataReader["autorizaPlaca"].ToString()).Equals(1);
                imovel.Finalidade = dataReader["finalidade"].ToString();
                imovel.Descricao = dataReader["descricao"].ToString();
                imovel.TaxaCondo = double.Parse(dataReader["taxaCondo"].ToString());
                imovel.TaxaIPTU = double.Parse(dataReader["taxaIPTU"].ToString());
                imovel.UnidadesDisponiveis = int.Parse(dataReader["unidadesDisponiveis"].ToString());
                imovel.quantasImagens = int.Parse(dataReader["quantas_imagens"].ToString());
                int id = (int)imovel.Codigo;
                imovel.ObservacoesNomes = PegaObservacoes(id);
                imovel.NomeAutor = PegaNomeDono(id);

                imoveis.Add(imovel);

            }
            return imoveis;
        }
        public List<ImoveisDTO> ListarImoveisAVenda()
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = "SELECT imoveis.*, COUNT(imovel_images.id) as quantas_imagens FROM imoveis LEFT JOIN imovel_images ON imovel.Codigo = imagem.id_imovel  where finalidade = 'VENDA' GROUP BY imoveis.Codigo HAVING COUNT(imovel_images.id) > 5;";

            var comando = new MySqlCommand(query, conexao);
            var dataReader = comando.ExecuteReader();

            var imoveis = new List<ImoveisDTO>();


            while (dataReader.Read())
            {
                var imovel = new ImoveisDTO();
                imovel.Codigo = int.Parse(dataReader["Codigo"].ToString());
                imovel.Endereco = dataReader["endereco"].ToString();
                imovel.CEP = dataReader["cep"].ToString();
                imovel.Bairro = dataReader["bairro"].ToString();
                imovel.Cidade = dataReader["cidade"].ToString();
                imovel.Tipo = dataReader["tipo"].ToString();
                imovel.Andares = int.Parse(dataReader["andares"].ToString());
                imovel.Dormitorios = int.Parse(dataReader["dormitorios"].ToString());
                imovel.Suites = int.Parse(dataReader["suites"].ToString());
                imovel.Salas = int.Parse(dataReader["salas"].ToString());
                imovel.Garagens = int.Parse(dataReader["garagens"].ToString());
                imovel.AreaUtil = int.Parse(dataReader["areasUteis"].ToString());
                imovel.CondominioFechado = int.Parse(dataReader["CondominioFechado"].ToString()).Equals(1);
                imovel.Valor = double.Parse(dataReader["valor"].ToString());
                imovel.Status = dataReader["status"].ToString();
                imovel.AutorizaFoto = dataReader["autorizaFoto"].ToString().Equals(1);
                imovel.AutorizaPlaca = int.Parse(dataReader["autorizaPlaca"].ToString()).Equals(1);
                imovel.Finalidade = dataReader["finalidade"].ToString();
                imovel.Descricao = dataReader["descricao"].ToString();
                imovel.TaxaCondo = double.Parse(dataReader["taxaCondo"].ToString());
                imovel.TaxaIPTU = double.Parse(dataReader["taxaIPTU"].ToString());
                imovel.UnidadesDisponiveis = int.Parse(dataReader["unidadesDisponiveis"].ToString());
                imovel.quantasImagens = int.Parse(dataReader["quantas_imagens"].ToString());
                int id = (int)imovel.Codigo;
                imovel.ObservacoesNomes = PegaObservacoes(id);
                imovel.NomeAutor = PegaNomeDono(id);

                imoveis.Add(imovel);

            }
            return imoveis;
        }
        public List<ImoveisDTO> ListarImoveisAlugaveis()
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = "SELECT imoveis.*, COUNT(imovel_images.id) as quantas_imagens FROM imoveis LEFT JOIN imovel_images ON imovel.Codigo = imagem.id_imovel  where finalidade = 'ALUGUEL' GROUP BY imoveis.Codigo HAVING COUNT(imovel_images.id) > 5;";

            var comando = new MySqlCommand(query, conexao);
            var dataReader = comando.ExecuteReader();

            var imoveis = new List<ImoveisDTO>();


            while (dataReader.Read())
            {
                var imovel = new ImoveisDTO();
                imovel.Codigo = int.Parse(dataReader["Codigo"].ToString());
                imovel.Endereco = dataReader["endereco"].ToString();
                imovel.CEP = dataReader["cep"].ToString();
                imovel.Bairro = dataReader["bairro"].ToString();
                imovel.Cidade = dataReader["cidade"].ToString();
                imovel.Tipo = dataReader["tipo"].ToString();
                imovel.Andares = int.Parse(dataReader["andares"].ToString());
                imovel.Dormitorios = int.Parse(dataReader["dormitorios"].ToString());
                imovel.Suites = int.Parse(dataReader["suites"].ToString());
                imovel.Salas = int.Parse(dataReader["salas"].ToString());
                imovel.Garagens = int.Parse(dataReader["garagens"].ToString());
                imovel.AreaUtil = int.Parse(dataReader["areasUteis"].ToString());
                imovel.CondominioFechado = int.Parse(dataReader["CondominioFechado"].ToString()).Equals(1);
                imovel.Valor = double.Parse(dataReader["valor"].ToString());
                imovel.Status = dataReader["status"].ToString();
                imovel.AutorizaFoto = dataReader["autorizaFoto"].ToString().Equals(1);
                imovel.AutorizaPlaca = int.Parse(dataReader["autorizaPlaca"].ToString()).Equals(1);
                imovel.Finalidade = dataReader["finalidade"].ToString();
                imovel.Descricao = dataReader["descricao"].ToString();
                imovel.TaxaCondo = double.Parse(dataReader["taxaCondo"].ToString());
                imovel.TaxaIPTU = double.Parse(dataReader["taxaIPTU"].ToString());
                imovel.UnidadesDisponiveis = int.Parse(dataReader["unidadesDisponiveis"].ToString());
                imovel.quantasImagens = int.Parse(dataReader["quantas_imagens"].ToString());
                int id = (int)imovel.Codigo;
                imovel.ObservacoesNomes = PegaObservacoes(id);
                imovel.NomeAutor = PegaNomeDono(id);

                imoveis.Add(imovel);

            }
            return imoveis;
        }

        public List<string> PegaObservacoes(int id)
        {
            var allObservacoes = new List<string>();
            var conexao = ConnectionFactory.Build();
            conexao.Open();
            var query = "select * from imoblink.imoveis " +
                        "inner join imoblink.observacoes_imovel on imoblink.imoveis.Codigo = imoblink.observacoes_imovel.id_imovel " +
                        "inner join imoblink.observacoes on imoblink.observacoes_imovel.id_observacoes = imoblink.observacoes.ID_O " +
                        "where imoblink.imoveis.Codigo =  @ID ";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@ID", id);
            var dataReader = comando.ExecuteReader();

            while (dataReader.Read())
            {
                string observacao;
                observacao = dataReader["Nome"].ToString();
                allObservacoes.Add(observacao);
            }

            conexao.Close();
            return allObservacoes;
        }
        public string PegaNomeDono( int id)
        {
            string nome = "";
            var allObservacoes = new List<string>();
            var conexao = ConnectionFactory.Build();
            for (int i = 0; i < 2; i++)
            {
                string query = "";
                var query1 = "select imoblink.pessoafisica.nome as nome from imoblink.imovel_pessoafisica inner join imoblink.pessoafisica " +
                         "on imoblink.imovel_pessoafisica.cpf_pessoaFisica = imoblink.pessoafisica.cpf " +
                         "where imoblink.imovel_pessoafisica.codigo_imovel = @id;";

                var query2 = "select imoblink.pessoajuridica.NomeEmpresa as nome from imoblink.imovel_pessoajuridica inner join imoblink.pessoajuridica " +
                             "on imoblink.imovel_pessoajuridica.CNPJ_pessoaJuridica = imoblink.pessoajuridica.CNPJ " +
                             "where imoblink.imovel_pessoajuridica.codigo_imovel = @id;";


                if (i == 1)
                {
                    conexao.Open();
                    var comando = new MySqlCommand(query1, conexao);
                    comando.Parameters.AddWithValue("@ID", id);
                    var dataReader = comando.ExecuteReader();

                    while (dataReader.Read())
                    {
                        nome = dataReader["nome"].ToString();
                        return nome;
                    }

                }
                else {
                        conexao.Open();
                        var comando = new MySqlCommand(query2, conexao);
                           comando.Parameters.AddWithValue("@ID", id);
                        var dataReader = comando.ExecuteReader();

                        while (dataReader.Read())
                        {
                            nome = dataReader["nome"].ToString();
                            return nome;
                        }
                    }

                conexao.Close();
            }
            return nome;
        }
        public  void CadastrarImagem(Imovel_imagesDTO image)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();
            var query = "insert into imovel_images(id_imovel, url_imagem, descricao_imagem, favorito) values(@id, @url, @descricao, 0)";
            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@id", image.idImovel);
            comando.Parameters.AddWithValue("@url", image.URLImage );
            comando.Parameters.AddWithValue("@descricao", image.descricao );    
            comando.ExecuteNonQuery();
            conexao.Close();
            return;
        }

        public List<Imovel_imagesDTO> listarImagens(int idImovel)
        {
            var imagens = new List<Imovel_imagesDTO>();
            var conexao = ConnectionFactory.Build();
            conexao.Open();
            var query = "select * from imoblink.imovel_images where imoblink.imovel_images.id_imovel = @idImovel ";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@idImovel", idImovel);
            var dataReader = comando.ExecuteReader();

            while (dataReader.Read())
            {
                Imovel_imagesDTO imagem = new Imovel_imagesDTO();
                imagem.id = int.Parse(dataReader["id"].ToString());
                imagem.idImovel = int.Parse(dataReader["id_imovel"].ToString());
                imagem.URLImage = dataReader["url_Imagem"].ToString();
                imagem.descricao = dataReader["descricao_imagem"].ToString();
                imagem.fav = int.Parse(dataReader["favorito"].ToString()) ==1;

                imagens.Add(imagem);
            }

            conexao.Close();
            return imagens;
        }
        public void ApagarImagem(int id)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();
            var query = "delete from imoblink.imovel_images where imoblink.imovel_images.id = @id ";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@id", id);
            comando.ExecuteNonQuery();           

            conexao.Close();
        }
        public void AdicionarImagemFavorita(Imovel_imagesDTO img)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();
            var query = @"UPDATE Imovel_Images SET Favorito = 0 WHERE ID_Imovel = @idImovel;
                          UPDATE Imovel_Images SET Favorito = 1 WHERE ID = @id;";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@id", img.id);
            comando.Parameters.AddWithValue("@idImovel", img.idImovel);
            comando.ExecuteNonQuery();

            conexao.Close();
        }
        public string PegaImagemFav(int idImovel)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();
            var query = "select url_Imagem from imoblink.imovel_images where imoblink.imovel_images.id_imovel = @idImovel and favorito = 1";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@idImovel", idImovel);
            var dataReader = comando.ExecuteReader();
            Imovel_imagesDTO imagem = new Imovel_imagesDTO();

            while (dataReader.Read())
            {
                imagem.URLImage = dataReader["url_Imagem"].ToString();
                
            }

            conexao.Close();
            return imagem.URLImage;
        }
    }
}
