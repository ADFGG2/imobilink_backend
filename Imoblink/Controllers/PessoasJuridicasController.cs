using Imoblink.Azure;
using Imoblink.DAOs;
using Imoblink.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Imoblink.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public IActionResult DefinirImagemDePerfil(int id, string base64)
        {

            var azureBlobStorage = new AzureBlobStorage();
            var imagem = azureBlobStorage.UploadImage(base64);

            var dao = new PessoaJuridicaDAO();
            dao.adicionarImagemdePerfil(id, imagem);

            return Ok();
        }
    }
}
