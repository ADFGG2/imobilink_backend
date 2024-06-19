using Imoblink.Azure;
using Imoblink.DAOs;
using Imoblink.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Imoblink.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class PessoasJuridicasController : ControllerBase
    {
        [HttpPost]
        [Route("CadastrarPessoaJuridica")]

        public IActionResult CadastrarPessoaJuridica([FromBody] PessoaJuridicaDTO pessoaJuridica)
        {
            var dao = new PessoaJuridicaDAO();
            dao.CadastrarPessoaJuridica(pessoaJuridica);

            return Ok();
        }

        [HttpPost]
        [Route("DefinirImagemDePerfil")]
        [Authorize]
        public IActionResult DefinirImagemDePerfil( string URLImage)
        {

            var azureBlobStorage = new AzureBlobStorage();
            var imagem = azureBlobStorage.UploadImage(URLImage);
            var CNPJ = HttpContext.User.FindFirst("CNPJ")?.Value;

            var dao = new PessoaJuridicaDAO();
            dao.adicionarImagemdePerfil(CNPJ, imagem);

            return Ok();
        }
    }
}
