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
    public class ImobiliariasController : ControllerBase
    {
        [HttpPost]
        [Route("CadastrarImobiliaria")]
        [AllowAnonymous]
        public IActionResult CadastrarImobiliaria([FromBody] ImobiliariaDTO imobiliaria)
        {
            var dao = new ImobiliariaDAO();
            dao.CadastrarImobiliaria(imobiliaria);

            return Ok();
        }


        [HttpPost]
        [Route("DefinirImagemDePerfil")]
        public IActionResult DefinirImagemDePerfil(string URLImage)
        {

            var azureBlobStorage = new AzureBlobStorage();
            var imagem = azureBlobStorage.UploadImage(URLImage);
            var CNPJ = HttpContext.User.FindFirst("CNPJ")?.Value;

            var dao = new ImobiliariaDAO();
            dao.adicionarImagemdePerfil(CNPJ, imagem);

            return Ok();
        }

        [HttpPost]
        [Route("AdicionarImovelFavorito")]
        public IActionResult AdicionarImovelFavorito(int idImovel)
        {
            var CNPJ = HttpContext.User.FindFirst("CNPJ")?.Value;
            var dao = new ImobiliariaDAO();
            dao.adicionarImovelFavorito(CNPJ, idImovel);

            return Ok();
        }

        [HttpDelete]
        [Route("RemoverImovelFavorito")]
        public IActionResult RemoverImovelFavorito(int idImovel)
        {
            var CNPJ = HttpContext.User.FindFirst("CNPJ")?.Value;
            var dao = new ImobiliariaDAO();
            dao.removerImovelFavorito(CNPJ, idImovel);

            return Ok();
        }
    }
}
