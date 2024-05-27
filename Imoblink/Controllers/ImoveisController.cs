using Imoblink.Azure;
using Imoblink.DAOs;
using Imoblink.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;


namespace Imoblink.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ImoveisController : ControllerBase
    {
        [HttpPost]
        [Route("CadastrarImovel")]
        public IActionResult CadastrarImovel([FromBody] ImoveisDTO imoveis)
        {
            var dao = new ImoveisDAO();
            dao.CadastrarImovel(imoveis);

            return Ok();
        }

        [HttpPost]
        [Route("CadastrarImagem")]
        public IActionResult CadastrarImagem([FromBody] Imovel_imagesDTO body)
        {
            var image = new Imovel_imagesDTO();
            image.idImovel = body.idImovel;
            image.descricao = body.descricao;

            var azureBlobStorage = new AzureBlobStorage();
            image.URLImage = azureBlobStorage.UploadImage(body.URLImage);

            var dao = new ImoveisDAO();
            dao.CadastrarImagem(image);

            return Ok();
        }
        [HttpDelete]
        [Route("ApagarImagem/{id}")]
        public IActionResult ApagarImagem([FromRoute] int id)
        {
            var dao = new ImoveisDAO();
            dao.ApagarImagem(id);

            return Ok();
        }
        [HttpGet]
        [Route("ListarImagens/{idImovel}")]
        public IActionResult ListarImoveis([FromRoute] int idImovel)    
        {
            var dao = new ImoveisDAO();

            return Ok(dao.listarImagens(idImovel));
        }
        [HttpGet]
        [Route("PegaImagemFav/{idImovel}")]
        public IActionResult PegaImagemFav([FromRoute] int idImovel)
        {
            var dao = new ImoveisDAO();

            return Ok(dao.PegaImagemFav(idImovel));
        }
        [HttpPost]
        [Route("AdicionarImagemFavorita")]
        public IActionResult AdicionarImagemFavorita([FromBody] Imovel_imagesDTO img)
        {
            var dao = new ImoveisDAO();

            dao.AdicionarImagemFavorita(img);

            return Ok();
        }
        [HttpGet]
        [Route("ListarImoveis")]
        public IActionResult ListarImoveis()
        {
            var dao = new ImoveisDAO();

            return Ok(dao.ListarImoveis());
        }

        [HttpGet]
        [Route("ListarMeusImoveisCpf")]
        public IActionResult ListarMeusImoveisCpf()
        {
            var CPF = HttpContext.User.FindFirst("CPF")?.Value;

            var dao = new ImoveisDAO();
            var imoveis = dao.ListarMeusImoveisCpf(CPF);
            return Ok(imoveis);
        }

        [HttpGet]
        [Route("ListarMeusImoveisCnpj")]
        public IActionResult ListarMeusImoveisCnpj()
        {
            var CNPJ = HttpContext.User.FindFirst("cnpj")?.Value;

            var dao = new ImoveisDAO();            

            return Ok(dao.ListarMeusImoveisCnpj(CNPJ));
        }

        [HttpGet]
        [Route("ListarImoveisAVenda")]
        public IActionResult ListarImoveisAVenda()
        {
            var dao = new ImoveisDAO();

            return Ok(dao.ListarImoveisAVenda());
        }

        [HttpGet]
        [Route("ListarImoveisAlugaveis")]
        public IActionResult ListarImoveisAlugaveis()
        {
            var dao = new ImoveisDAO();

            return Ok(dao.ListarImoveisAlugaveis());
        }
    }  
}
