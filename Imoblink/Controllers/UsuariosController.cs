using Imoblink.DAOs;
using Imoblink.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Imoblink.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class UsuariosController : ControllerBase
    {
        [HttpPost]
        [Route("Login")]
       

        public IActionResult Login([FromForm] UsuarioDTO usuario)
        {
            var dao = new UsuarioDAO();
            var tipo = dao.VerificaQualTipoDeLogin(usuario);

            switch (tipo)
            {
                case 1:
                    usuario.DadosUsuario = dao.LoginImobiliaria(usuario);
                    break;

                case 2:
                    usuario.DadosUsuario = dao.LoginCorretor(usuario);
                    break;

                case 3:
                    usuario.DadosUsuario = dao.LoginJuridica(usuario);
                    break;

                case 4:
                    usuario.DadosUsuario = dao.LoginFisica(usuario);
                    break;

                default:
                    return Ok("erro dados não encontrados");
                    break;
            }           
   

            var token = GenerateJwtToken(usuario);

            return Ok(new { token });
        }

      /*  [HttpGet]
        [Route("GetUserData")]
        public IActionResult GetUserData()  
        {
            var dao = new UsuarioDAO();
            var id = int.Parse(HttpContext.User.FindFirst("id")?.Value);

            var usuario = dao.BuscarUsuarioPorID(id);

            return Ok(usuario);
        }*/

        private string GenerateJwtToken(UsuarioDTO usuario)
        {
            var secretKey = "PU8a9W4sv2opkqlOwmgsn3w3Innlc4D5";
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = GetClaims(usuario);
            var token = new JwtSecurityToken(
                "APIUsuarios", //Nome da sua api
                "APIUsuarios", //Nome da sua api
                claims, //Lista de claims
                expires: DateTime.UtcNow.AddDays(30), //Tempo de expiração do Token, nesse caso o Token expira em um dia
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private List<Claim> GetClaims(UsuarioDTO usuario)
        {
            var claims = new List<Claim>(); 
            if(usuario.DadosUsuario is ImobiliariaDTO)
            {
                var dados = (ImobiliariaDTO)usuario.DadosUsuario;
                claims.Add(new Claim("CNPJ", dados.CNPJ));
                claims.Add(new Claim("RazaoSocial", dados.CNPJ));
                claims.Add(new Claim("RepresentanteLegal", dados.RepresentanteLegal));
                claims.Add(new Claim("CRECI", dados.CRECI.ToString()));
                claims.Add(new Claim("Email", dados.Email));
                claims.Add(new Claim("senha", dados.senha));
                claims.Add(new Claim("cep", dados.cep));
                claims.Add(new Claim("cidade", dados.cidade));
                claims.Add(new Claim("bairro", dados.bairro));
                claims.Add(new Claim("Telefone", dados.Telefone));


                return claims;
            }
            else if(usuario.DadosUsuario is CorretorDTO)
            {
                var dados = (CorretorDTO)usuario.DadosUsuario;
                claims.Add(new Claim("CPF", dados.CPF));
                claims.Add(new Claim("Nome", dados.Nome_completo));
                claims.Add(new Claim("CRECI", dados.CRECI.ToString()));
                claims.Add(new Claim("Email", dados.Email));
                claims.Add(new Claim("Celular", dados.Celular));
                claims.Add(new Claim("Telefone", dados.Telefone));

                return claims;
            }
            else if (usuario.DadosUsuario is PessoaFisicaDTO)
            {
                var dados = (PessoaFisicaDTO)usuario.DadosUsuario;
                claims.Add(new Claim("CPF", dados.cpf));
                claims.Add(new Claim("nome", dados.nome));
                claims.Add(new Claim("rg", dados.rg));
                claims.Add(new Claim("email", dados.email));
                claims.Add(new Claim("telefone", dados.telefone));
                claims.Add(new Claim("senha", dados.senha));
                claims.Add(new Claim("cidade", dados.cidade));
                claims.Add(new Claim("cep", dados.cep));
                claims.Add(new Claim("bairro", dados.bairro));


                return claims;
            }
            else if (usuario.DadosUsuario is PessoaJuridicaDTO)
            {
                var dados = (PessoaJuridicaDTO)usuario.DadosUsuario;
                claims.Add(new Claim("NomeEmpresa", dados.NomeEmpresa));
                claims.Add(new Claim("InscricaoEstadual", dados.InscricaoEstadual));
                claims.Add(new Claim("CNPJ", dados.CNPJ));
                claims.Add(new Claim("Email", dados.Email));
                claims.Add(new Claim("Celular", dados.Celular));
                claims.Add(new Claim("Telefone", dados.Telefone));
                claims.Add(new Claim("senha", dados.senha));
                claims.Add(new Claim("cidade", dados.cidade));
                claims.Add(new Claim("cep", dados.cep));
                claims.Add(new Claim("bairro", dados.bairro));



                return claims;
            }
            else
            {
                return claims;
            }
            
        }

    }
}
