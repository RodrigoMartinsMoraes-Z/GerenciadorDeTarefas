using AutoMapper;

using GerenciadorDeTarefas.Common.Models.Usuarios;
using GerenciadorDeTarefas.Domain.Contexto;
using GerenciadorDeTarefas.Domain.ManyToMany;
using GerenciadorDeTarefas.Domain.Usuarios;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorDeTarefas.WebApi.Controllers
{
    [Route("api/usuario")]
    public class UsuarioController : BaseApiController
    {
        private readonly IContextoDeDados _contexto;
        private readonly IMapper _mapper;

        public UsuarioController(IContextoDeDados contexto, IMapper mapper)
        {
            _contexto = contexto;
            _mapper = mapper;
        }

        [HttpGet, Route("{id}")]
        public async Task<ActionResult> BuscarUsuarioPorId(int id)
        {
            Usuario usuario = _contexto.Usuarios.Find(id);

            await Task.CompletedTask;

            return Ok(_mapper.Map<UsuarioModel>(usuario));
        }

        [HttpGet]
        public async Task<ActionResult> BuscarUsuarioPorEmail(string email)
        {
            var usuario = _contexto.Usuarios.FirstOrDefault(u => u.Pessoa.Email == email);

            if (usuario == null)
                return NotFound();

            await Task.CompletedTask;

            return Ok(_mapper.Map<UsuarioModel>(usuario));
        }

        [HttpGet, Route("verifica/email")]
        public Task<bool> VerificaEmail(string email)
        {
            return Task.FromResult(_contexto.Pessoas.Any(p => p.Email == email));
        }

        [HttpGet, Route("verifica/login")]
        public Task<bool> VerificaLogin(string login)
        {
            return Task.FromResult(_contexto.Usuarios.Any(u => u.Login == login));
        }

        [HttpGet, Route("permissao/{idUsuario}/{idEquipe}")]
        public async Task<ActionResult> PermissaoUsuario(int idUsuario, int idEquipe)
        {
            EquipeUsuario equipeUsuario = _contexto.EquipeUsuario.FirstOrDefault(eu => eu.IdUsuario == idUsuario && eu.IdEquipe == idEquipe);

            if (equipeUsuario == null)
                return NotFound();

            UsuarioModel usuarioModel = _mapper.Map<UsuarioModel>(equipeUsuario.Usuario);
            usuarioModel.Permissao = (Common.Permissao?)equipeUsuario.PermissaoUsuario;

            await Task.CompletedTask;

            return Ok(usuarioModel);
        }

        [HttpPut]
        public async Task<ActionResult> AtualizarUsuario([FromBody] UsuarioModel usuarioModel)
        {
            Usuario usuario = _mapper.Map<Usuario>(usuarioModel);

            if (usuario.Login == null)
                throw new Exception("Login não deve ser nulo");

            if (usuario.Senha == null)
                throw new Exception("Senha não pode ser nula");

            Usuario existente = _contexto.Usuarios.FirstOrDefault(u => u.Login == usuario.Login);

            if (existente != null)
            {
                //existente.Pessoa = _contexto.Pessoas.Find(existente.IdPessoa);

                usuario.Senha = existente.Senha;
                if (usuario.Pessoa != null && usuario.Pessoa.Email != existente.Pessoa.Email)
                    if (_contexto.Pessoas.Any(p => p.Email == usuario.Pessoa.Email))
                        return BadRequest("Este email já está sendo utilizado.");

                usuario.IdPessoa = existente.IdPessoa;
                usuario.Pessoa.Id = existente.IdPessoa;

                _contexto.Usuarios.Update(usuario);
            }
            else
            {
                _contexto.Usuarios.Add(usuario);
            }

            await Task.CompletedTask;

            _contexto.SaveChanges();

            return Ok();
        }

        [HttpPost, AllowAnonymous]
        public async Task<ActionResult> NovoUsuario([FromBody] UsuarioModel usuarioModel)
        {
            if (usuarioModel.Login == null)
                return BadRequest("Login não deve ser nulo!");

            if (usuarioModel.Senha == null)
                return BadRequest("A senha não deve ser nula!");

            if (usuarioModel.Pessoa == null || usuarioModel.Pessoa.Email == null)
                return BadRequest("Email não deve ser nulo!");

            if (_contexto.Usuarios.Any(u => u.Login == usuarioModel.Login))
                return BadRequest("Este login já está sendo utilizado.");

            if (usuarioModel.Pessoa == null || _contexto.Pessoas.Any(p => p.Email == usuarioModel.Pessoa.Email))
                return BadRequest("Este email já está sendo utilizado.");

            Usuario usuario = _mapper.Map<Usuario>(usuarioModel);

            _contexto.Add(usuario);
            _contexto.SaveChanges();

            await Task.CompletedTask;

            return Ok();
        }

    }
}
