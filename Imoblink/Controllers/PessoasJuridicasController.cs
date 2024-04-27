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
    }
}
