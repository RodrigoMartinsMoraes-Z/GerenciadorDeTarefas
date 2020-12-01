using AutoMapper;

using GerenciadorDeTarefas.Common.Models.Equipes;
using GerenciadorDeTarefas.Common.Models.Projetos;
using GerenciadorDeTarefas.Common.Models.Usuarios;
using GerenciadorDeTarefas.Domain.Contexto;
using GerenciadorDeTarefas.Domain.Equipes;
using GerenciadorDeTarefas.Domain.ManyToMany;
using GerenciadorDeTarefas.Domain.Usuarios;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorDeTarefas.WebApi.Controllers
{
    [Route("api/equipe")]
    public class EquipeController : BaseApiController
    {
        private readonly IContextoDeDados _contexto;
        private readonly IMapper _mapper;

        public EquipeController(IContextoDeDados contexto, IMapper mapper)
        {
            _contexto = contexto;
            _mapper = mapper;
        }

        [HttpGet, Route("{id}")]
        public async Task<ActionResult> BuscarEquipe(int id)
        {
            Equipe equipe = _contexto.Equipes.Find(id);

            await Task.CompletedTask;

            return Ok(_mapper.Map<EquipeModel>(equipe));
        }

        [HttpGet, Route("usuarios/{idEquipe}")]
        public async Task<ActionResult> UsuariosDaEquipe(int idEquipe)
        {
            List<Usuario> usuarios = _contexto.EquipeUsuario
                .Where(eu => eu.IdEquipe == idEquipe)
                .Select(e => e.Usuario)
                .ToList();

            List<UsuarioModel> models = new List<UsuarioModel>();

            foreach (var usuario in usuarios)
            {
                var model = _mapper.Map<UsuarioModel>(usuario);

                models.Add(model);
            }

            await Task.CompletedTask;
            return Ok(models);
        }

        [HttpGet, Route("projetos/{idEquipe}")]
        public async Task<ActionResult> ProjetosDaEquipe(int idEquipe)
        {
            var projetos = _contexto.Projetos.Where(p => p.IdEquipe == idEquipe);

            List<ProjetoModel> models = new List<ProjetoModel>();

            foreach (var projeto in projetos)
            {
                var model = _mapper.Map<ProjetoModel>(projeto);
                models.Add(model);
            }

            return Ok(models);
        }

        [HttpPut, Route("adicionar/usuario/{idEquipe}")]
        public async Task<ActionResult> AdicionarUsuario([FromBody] UsuarioModel usuarioModel, int idEquipe)
        {
            Equipe equipe = _contexto.Equipes.Find(idEquipe);
            Usuario usuario = _contexto.Usuarios.FirstOrDefault(u => u.Login == usuarioModel.Login);

            EquipeUsuario equipeUsuario = new EquipeUsuario
            {
                Equipe = equipe,
                IdEquipe = equipe.Id,
                IdUsuario = usuario.IdPessoa,
                Usuario = usuario,
                PermissaoUsuario = (Permissao)usuarioModel.Permissao
            };

            equipe.Usuarios.Add(equipeUsuario);
            _contexto.SaveChanges();

            await Task.CompletedTask;

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> AtualizarEquipe([FromBody] EquipeModel equipeModel)
        {
            //Equipe equipe = _contexto.Equipes.FirstOrDefault(e=>e.)

            //if (equipe.Id <= 0)
            //{
            //    return NotFound();
            //}
            //_contexto.Update(equipe);
            //_contexto.SaveChanges();


            await Task.CompletedTask;

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> CadastrarEquipe([FromBody] EquipeModel equipeModel)
        {

            Equipe equipe = _mapper.Map<Equipe>(equipeModel);

            await equipe.Validate();

            Usuario usuario = _contexto.Usuarios.FirstOrDefault(u => u.Login == User.Identity.Name);

            _contexto.Add(equipe);
            _contexto.SaveChanges();



            EquipeUsuario equipeUsuario = new EquipeUsuario
            {
                Usuario = usuario,
                IdUsuario = usuario.IdPessoa,
                Equipe = equipe,
                IdEquipe = equipe.Id,
                PermissaoUsuario = Permissao.Administrador
            };

            usuario.Equipes.Add(equipeUsuario);
            equipe.Usuarios.Add(equipeUsuario);

            _contexto.Update(usuario);
            _contexto.Update(equipe);
            _contexto.SaveChanges();

            return Ok();
        }


    }
}
