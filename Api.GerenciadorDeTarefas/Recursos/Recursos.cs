using Starlight.Standard;

using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Api.GerenciadorDeTarefas.Recursos
{
    public class Recursos
    {
        public Recursos()
        {
            Api.client = new System.Net.Http.HttpClient();
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
    }
}
