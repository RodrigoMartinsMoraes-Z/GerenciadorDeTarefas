using AutoMapper;

using GerenciadorDeTarefas.Common.Models.Usuarios;
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
        public async Task<ActionResult> Logar(string login, string password)
        {
            password = EncriptarSenha(login, password);

            Domain.Users.User user = _contexto.Users.FirstOrDefault(u => u.Login == login && u.Pass == password);

            _usuarioLogado = user;

            if (user == null)
                return NotFound();

            UserModel usuarioModel = _mapper.Map<UserModel>(user);
            usuarioModel.Token = TokenService.GenerateToken(usuarioModel);

            await Task.CompletedTask;

            return Ok(usuarioModel);
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
