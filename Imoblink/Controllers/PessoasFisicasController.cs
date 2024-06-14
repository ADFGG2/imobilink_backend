using Imoblink.Azure;
using Imoblink.DAOs;
using Imoblink.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Imoblink.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoasFisicasController : ControllerBase
    {
        [HttpPost]
        [Route("CadastrarPessoaFisica")]

        public IActionResult CadastrarPessoaFisica([FromBody] PessoaFisicaDTO pessoaFisica)
        {
            var dao = new PessoaFisicaDAO();
            dao.CadastrarPessoaFisica(pessoaFisica);

            return Ok();
        }


        [HttpPost]
        [Route("DefinirImagemDePerfil")]
        public IActionResult DefinirImagemDePerfil(int id, string base64)
        {

            var azureBlobStorage = new AzureBlobStorage();
            var imagem = azureBlobStorage.UploadImage(base64);

            var dao = new PessoaFisicaDAO();
            dao.adicionarImagemdePerfil(id, imagem);

            return Ok();
        }
    }
}
