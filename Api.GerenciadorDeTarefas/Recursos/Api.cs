using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Api.GerenciadorDeTarefas.Recursos
{
    public static class Api
    {
        public static HttpClient _client = new HttpClient();

        public static async Task<string> ConsumirApi(string url, HttpMethod method, object objeto = null)
        {

            if (method == HttpMethod.Get)
            {
                if (objeto != null)
                {
                    IEnumerable<string> properties = from p in objeto.GetType().GetProperties()
                                                     where p.GetValue(objeto, null) != null
                                                     select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(objeto, null).ToString());

                    string queryString = string.Join("&", properties.ToArray());
                    url += "?" + queryString;
                }

                HttpResponseMessage resposta = _client.GetAsync($"{url}").GetAwaiter().GetResult();
                string conteudo = await resposta.Content.ReadAsStringAsync();
                if (resposta.IsSuccessStatusCode)
                    return conteudo;

                throw new Exception("Can't get a success status response. " + resposta.StatusCode.ToString());
            }

            if (method == HttpMethod.Post)
            {

                HttpResponseMessage resposta = _client.PostAsync($"{url}", GerarJson(objeto)).GetAwaiter().GetResult();
                string conteudo = await resposta.Content.ReadAsStringAsync();
                if (resposta.IsSuccessStatusCode)
                    return conteudo;

                throw new Exception("Can't get a success status response. " + resposta.StatusCode.ToString());
            }

            if (method == HttpMethod.Put)
            {

                HttpResponseMessage resposta = _client.PutAsync($"{url}", GerarJson(objeto)).GetAwaiter().GetResult();
                string conteudo = await resposta.Content.ReadAsStringAsync();
                if (resposta.IsSuccessStatusCode)
                    return conteudo;

                throw new Exception("Can't get a success status response. " + resposta.StatusCode.ToString());
            }

            throw new Exception();
        }

        internal static StringContent GerarJson(object objeto)
        {
            string serializedObject = $"{JsonConvert.SerializeObject(objeto)}";
            return new StringContent(serializedObject, Encoding.UTF8, "application/json");
        }
    }
}
