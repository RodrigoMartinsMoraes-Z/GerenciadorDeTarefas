using AutoMapper;

using GerenciadorDeTarefas.Common.Models.Users;
using GerenciadorDeTarefas.Domain.Context;
using GerenciadorDeTarefas.Domain.Users;

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
        [ProducesResponseType(typeof(UserModel), 200)]
        public async Task<ActionResult> GetUser(int id)
        {
            User user = _contexto.Users.Find(id);

            await Task.CompletedTask;

            return Ok(_mapper.Map<UserModel>(user));
        }

        [HttpGet]
        [ProducesResponseType(typeof(UserModel), 200)]
        public async Task<ActionResult> GetUserByEmail(string email)
        {
            User user = _contexto.Users.FirstOrDefault(u => u.Email == email);

            if (user == null)
                return NotFound();

            await Task.CompletedTask;

            return Ok(_mapper.Map<UserModel>(user));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser([FromBody] UserModel userModel)
        {
            User user = _mapper.Map<User>(userModel);

            if (user.Login == null)
                throw new Exception("Login não deve ser nulo");

            if (user.Pass == null)
                throw new Exception("Senha não pode ser nula");

            User exist = _contexto.Users.FirstOrDefault(u => u.Login == user.Login);

            if (exist != null)
            {
                //existente.Pessoa = _contexto.Pessoas.Find(existente.IdPessoa);

                user.Pass = exist.Pass;
                if (user.Person != null && user.Email != exist.Email)
                    if (_contexto.Users.Any(p => p.Email == user.Email))
                        return BadRequest("Este email já está sendo utilizado.");

                user.PersonId = exist.PersonId;
                user.Person.Id = exist.PersonId;

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
        public async Task<ActionResult> NewUser([FromBody] UserModel userModel)
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

            User user = _mapper.Map<User>(userModel);

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
