using AutoMapper;

using GerenciadorDeTarefas.Common.Models.Users;
using GerenciadorDeTarefas.Domain.Context;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeTarefas.WebApi.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : BaseApiController
    {
        private readonly IContext _contexto;
        private readonly IMapper _mapper;

        public AccountController(IContext contexto, IMapper mapper)
        {
            _contexto = contexto;
            _mapper = mapper;
        }

        [HttpPost, Route("logar"), AllowAnonymous]
        [ProducesResponseType(typeof(UserModel), 200)]
        public async Task<ActionResult> Logar(string login, string pass)
        {
            pass = EncriptarSenha(login, pass);

            Domain.Users.User user = _contexto.Users.FirstOrDefault(u => u.Login == login && u.Pass == pass);

            _usuarioLogado = user;

            if (user == null)
                return NotFound();

            UserModel UserModel = _mapper.Map<UserModel>(user);
            UserModel.Token = TokenService.GenerateToken(UserModel);

            await Task.CompletedTask;

            return Ok(UserModel);
        }

        private static string EncriptarSenha(string login, string senha)
        {
            byte[] salt = Encoding.UTF8.GetBytes(login);
            byte[] senhaByte = Encoding.UTF8.GetBytes(senha);
            byte[] sha256 = new SHA256Managed().ComputeHash(senhaByte.Concat(salt).ToArray());
            return Convert.ToBase64String(sha256);
        }
    }
}
