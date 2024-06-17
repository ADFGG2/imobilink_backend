using Imoblink.Azure;
using Imoblink.DAOs;
using Imoblink.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Imoblink.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImobiliariasController : ControllerBase
    {
        [HttpPost]
        [Route("CadastrarImobiliaria")]

        public IActionResult CadastrarImobiliaria([FromBody] ImobiliariaDTO imobiliaria)
        {
            var dao = new ImobiliariaDAO();
            dao.CadastrarImobiliaria(imobiliaria);

            return Ok();
        }


        [HttpPost]
        [Route("DefinirImagemDePerfil")]
        public IActionResult DefinirImagemDePerfil(int id, string URLImage)
        {

            var azureBlobStorage = new AzureBlobStorage();
            var imagem = azureBlobStorage.UploadImage(URLImage);

            var dao = new ImobiliariaDAO();
            dao.adicionarImagemdePerfil(id, imagem);

            return Ok();
        }
    }
}
