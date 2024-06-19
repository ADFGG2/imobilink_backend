using Imoblink.Azure;
using Imoblink.DAOs;
using Imoblink.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Imoblink.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CorretoresController : ControllerBase
    {
        [HttpPost]
        [Route("CadastrarCorretor")]

        public IActionResult CadastrarCorretor([FromBody] CorretorDTO corretor)
        {
            var dao = new CorretorDAO();
            dao.CadastrarCorretor(corretor);

            return Ok();
        }

        [HttpPost]
        [Route("DefinirImagemDePerfil")]
        public IActionResult DefinirImagemDePerfil(int id, string URLImage)
        {


            var azureBlobStorage = new AzureBlobStorage();
            var imagem = azureBlobStorage.UploadImage(URLImage);

            var dao = new CorretorDAO();
            dao.adicionarImagemdePerfil(id, imagem);

            return Ok();
        }

        [HttpPost]
        [Route("AdicionarImovelFavorito")]
        public IActionResult AdicionarImovelFavorito(int idImovel)
        {
            var CPF = HttpContext.User.FindFirst("CPF")?.Value;
            var dao = new CorretorDAO();
            dao.adicionarImovelFavorito(CPF, idImovel);

            return Ok();
        }

        [HttpDelete]
        [Route("RemoverImovelFavorito")]
        public IActionResult RemoverImovelFavorito(int idImovel)
        {
            var CPF = HttpContext.User.FindFirst("CPF")?.Value;
            var dao = new CorretorDAO();
            dao.removerImovelFavorito(CPF, idImovel);

            return Ok();
        }
    }
}
