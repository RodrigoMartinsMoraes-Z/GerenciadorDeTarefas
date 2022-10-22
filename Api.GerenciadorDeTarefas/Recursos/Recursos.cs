using GerenciadorDeTarefas.Common.Models.Usuarios;

using Newtonsoft.Json;

using Starlight.Standard;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Api.GerenciadorDeTarefas.Recursos
{
    public class Recursos
    {
        public string UrlDoSistema { get; set; }

        public Recursos()
        {
            Api.client = new HttpClient();
        }

        public Task AdicionarHeaders(Header[] headers)
        {
            foreach (var header in headers)
                Api.client.DefaultRequestHeaders.Add(header.Key, header.Value);

            return Task.CompletedTask;
        }

        public async Task AdicionarAutenticacao(Authorization authorization)
        {
            switch (authorization.AuthenticationType)
            {
                case TipoDeAutorizacao.Api_Key:
                    Api.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authorization.Key, authorization.Value);
                    break;

                case TipoDeAutorizacao.Basic:
                    if (await authorization.User.IsValidString() && await authorization.Password.IsValidString())
                        Api.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                        Convert.ToBase64String(
                            Encoding.ASCII.GetBytes(
                                $"{authorization.User}:{authorization.Password}")));
                    else
                        throw new Exception("Falha ao adicionar o cabeçalho de autenticação, verifique se o usuario e senha estão preenchidos corretamente.");
                    break;

                case TipoDeAutorizacao.Bearer:
                    Api.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authorization.Key);
                    break;
            }
        }

        public async Task<UserModel> Logar(string login, string senha)
        {
            object usuario = await Api.ConsumirApi($"{UrlDoSistema}conta?login={login}&senha={senha}", HttpMethod.Post, typeof(UserModel));

            return (UserModel)usuario;
        }

        public async Task NovoUsuario(UserModel usuario)
        {
            await Api.ConsumirApi($"{UrlDoSistema}usuario", HttpMethod.Post, usuario);
        }

        internal Task<string> GenerateQueryString(object obj)
        {
            IEnumerable<string> properties = from p in obj.GetType().GetProperties()
                                             where p.GetValue(obj, null) != null
                                             select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null).ToString());

            return Task.FromResult('?' + string.Join("&", properties.ToArray()));
        }

        internal StringContent GenerateJson(object objeto)
        {
            string serializedObject = $"{JsonConvert.SerializeObject(objeto)}";
            return new StringContent(serializedObject, Encoding.UTF8, "application/json");
        }
    }
}
