using Imoblink.DAOs;
using Imoblink.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Imoblink.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImoveisController : ControllerBase
    {
        [HttpGet]
        [Route("CadastrarImovel")]

        public IActionResult CadastrarImovel([FromBody] ImoveisDTO imoveis)
        {
            var dao = new ImoveisDAO();
            dao.CadastrarImovel(imoveis);

            return Ok();
        }

    }  
}
