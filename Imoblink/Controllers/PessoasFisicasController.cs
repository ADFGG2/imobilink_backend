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
        public IActionResult DefinirImagemDePerfil( string URLImage)
        {

            var azureBlobStorage = new AzureBlobStorage();
            var imagem = azureBlobStorage.UploadImage(URLImage);
            var CPF = HttpContext.User.FindFirst("CPF")?.Value;

            var dao = new PessoaFisicaDAO();
            dao.adicionarImagemdePerfil(CPF, imagem);

            return Ok();
        }
    }
}
