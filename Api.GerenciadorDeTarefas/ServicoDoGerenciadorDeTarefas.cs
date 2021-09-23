using GerenciadorDeTarefas.Common.Models.Users;

using System.Threading.Tasks;

namespace Api.GerenciadorDeTarefas
{
    public class ServicoDoGerenciadorDeTarefas
    {
        private readonly Recursos.Recursos _recursos = new Recursos.Recursos();

        public Task DefinirUrlDoSistema(string url)
        {
            if (url.EndsWith("/api/"))
                _recursos.UrlDoSistema = url;
            else if (url.EndsWith("/"))
                _recursos.UrlDoSistema = $"{url}api/";
            else
                _recursos.UrlDoSistema = $"{url}/api/";

            return Task.CompletedTask;
        }

        public async Task NovoUsuario(UserModel usuarioModel)
        {
            await _recursos.NovoUsuario(usuarioModel);
        }
    }
}
