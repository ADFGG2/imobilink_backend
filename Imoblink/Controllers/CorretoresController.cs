using Imoblink.Azure;
using Imoblink.DAOs;
using Imoblink.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Imoblink.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public IActionResult DefinirImagemDePerfil(int id, string base64)
        {

            var azureBlobStorage = new AzureBlobStorage();
            var imagem = azureBlobStorage.UploadImage(base64);

            var dao = new CorretorDAO();
            dao.adicionarImagemdePerfil(id, imagem);

            return Ok();
        }
    }
}
