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
        [HttpGet]
        [Route("CadastrarPessoaFisica")]

        public IActionResult CadastrarPessoaFisica([FromBody] PessoaFisicaDTO pessoaFisica)
        {
            var dao = new PessoaFisicaDAO();
            dao.CadastrarPessoaFisica(pessoaFisica);

            return Ok();
        }
    }
}
