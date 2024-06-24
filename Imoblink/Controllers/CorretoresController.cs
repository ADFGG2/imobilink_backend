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
        [AllowAnonymous]


        public IActionResult CadastrarCorretor([FromBody] CorretorDTO corretor)
        {
            var dao = new CorretorDAO();
            dao.CadastrarCorretor(corretor);

            return Ok();
        }

        [HttpPost]
        [Route("DefinirImagemDePerfil")]
        public IActionResult DefinirImagemDePerfil( string URLImage)
        {


            var azureBlobStorage = new AzureBlobStorage();
            var imagem = azureBlobStorage.UploadImage(URLImage);
            var CNPJ = HttpContext.User.FindFirst("CNPJ")?.Value;

            var dao = new CorretorDAO();
            dao.adicionarImagemdePerfil(CNPJ, imagem);

            return Ok();
        }

        [HttpPost]
        [Route("AdicionarImovelFavorito")]
        public IActionResult AdicionarImovelFavorito(string idImovel)
        {
            var CPF = HttpContext.User.FindFirst("CPF")?.Value;
            var dao = new CorretorDAO();
            dao.adicionarImovelFavorito(CPF, int.Parse(idImovel));

            return Ok(idImovel);
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

        [HttpGet]
        [Route("ListarFavoritos")]
        public IActionResult ListarFavoritos(int idImovel)
        {
            var CPF = HttpContext.User.FindFirst("CPF")?.Value;
            var dao = new CorretorDAO();
            dao.removerImovelFavorito(CPF, idImovel);

            return Ok();
        }
    }
}
