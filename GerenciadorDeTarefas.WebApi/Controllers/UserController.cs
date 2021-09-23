using AutoMapper;

using GerenciadorDeTarefas.Common.Models.Usuarios;
using GerenciadorDeTarefas.Domain.Contexto;
using GerenciadorDeTarefas.Domain.Usuarios;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorDeTarefas.WebApi.Controllers
{
    [Route("api/user")]
    public class UserController : BaseApiController
    {
        private readonly IContext _contexto;
        private readonly IMapper _mapper;

        public UserController(IContext contexto, IMapper mapper)
        {
            _contexto = contexto;
            _mapper = mapper;
        }

        [HttpGet, Route("{id}")]
        [ProducesResponseType(typeof(UsuarioModel), 200)]
        public async Task<ActionResult> GetUser(int id)
        {
            Usuario user = _contexto.Users.Find(id);

            await Task.CompletedTask;

            return Ok(_mapper.Map<UsuarioModel>(user));
        }

        [HttpGet]
        [ProducesResponseType(typeof(UsuarioModel), 200)]
        public async Task<ActionResult> GetUserByEmail(string email)
        {
            var user = _contexto.Users.FirstOrDefault(u => u.Email == email);

            if (user == null)
                return NotFound();

            await Task.CompletedTask;

            return Ok(_mapper.Map<UsuarioModel>(user));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser([FromBody] UsuarioModel userModel)
        {
            Usuario user = _mapper.Map<Usuario>(userModel);

            if (user.Login == null)
                throw new Exception("Login não deve ser nulo");

            if (user.Senha == null)
                throw new Exception("Senha não pode ser nula");

            Usuario existente = _contexto.Users.FirstOrDefault(u => u.Login == user.Login);

            if (existente != null)
            {
                //existente.Pessoa = _contexto.Pessoas.Find(existente.IdPessoa);

                user.Senha = existente.Senha;
                if (user.Pessoa != null && user.Email != existente.Email)
                    if (_contexto.Users.Any(p => p.Email == user.Email))
                        return BadRequest("Este email já está sendo utilizado.");

                user.IdPessoa = existente.IdPessoa;
                user.Pessoa.Id = existente.IdPessoa;

                _contexto.Users.Update(user);
            }
            else
            {
                _contexto.Users.Add(user);
            }

            await Task.CompletedTask;

            _contexto.SaveChanges();

            return Ok();
        }

        [HttpPost, AllowAnonymous]
        public async Task<ActionResult> NewUser([FromBody] UsuarioModel userModel)
        {
            if (userModel.Login == null)
                return BadRequest("Login não deve ser nulo!");

            if (userModel.Senha == null)
                return BadRequest("A senha não deve ser nula!");

            if (userModel.Email == null)
                return BadRequest("Email não deve ser nulo!");

            if (await LoginExist(userModel.Login))
                return BadRequest("Este login já está sendo utilizado.");

            if (await EmailExist(userModel.Email))
                return BadRequest("Este email já está sendo utilizado.");

            Usuario user = _mapper.Map<Usuario>(userModel);

            _contexto.Add(user);
            _contexto.SaveChanges();

            await Task.CompletedTask;

            return Ok();
        }

        internal Task<bool> EmailExist(string email)
        {
            return Task.FromResult(_contexto.Users.Any(p => p.Email == email));
        }

        internal Task<bool> LoginExist(string login)
        {
            return Task.FromResult(_contexto.Users.Any(u => u.Login == login));
        }

    }
}
