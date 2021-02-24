using Newtonsoft.Json;

using Starlight.Standard;

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
        public static HttpClient client;

        public static async Task<object> ConsumirApi(string url, HttpMethod method, Type returnType, object objeto = null)
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

                HttpResponseMessage resposta = client.GetAsync($"{url}").GetAwaiter().GetResult();
                string conteudo = await resposta.Content.ReadAsStringAsync();
                if (resposta.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject(conteudo, returnType);

                throw new Exception("Can't get a success status response. " + resposta.StatusCode.ToString());
            }

            if (method == HttpMethod.Post)
            {
                HttpResponseMessage resposta = client.PostAsync($"{url}", GerarJson(objeto)).GetAwaiter().GetResult();
                string conteudo = await resposta.Content.ReadAsStringAsync();
                if (resposta.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject(conteudo, returnType);

                throw new Exception("Can't get a success status response. " + resposta.StatusCode.ToString());
            }

            if (method == HttpMethod.Put)
            {

                HttpResponseMessage resposta = client.PutAsync($"{url}", GerarJson(objeto)).GetAwaiter().GetResult();
                string conteudo = await resposta.Content.ReadAsStringAsync();
                if (resposta.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject(conteudo, returnType);

                throw new Exception("Can't get a success status response. " + resposta.StatusCode.ToString());
            }

            if (method == HttpMethod.Delete)
            {
                HttpResponseMessage resposta = client.DeleteAsync($"{url}").GetAwaiter().GetResult();
                string conteudo = await resposta.Content.ReadAsStringAsync();
                if (resposta.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject(conteudo, returnType);

                throw new Exception("Can't get a success status response. " + resposta.StatusCode.ToString());
            }

            throw new Exception();
        }
        public static async Task ConsumirApi(string url, HttpMethod method, object objeto = null)
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

                HttpResponseMessage resposta = client.GetAsync($"{url}").GetAwaiter().GetResult();
                string conteudo = await resposta.Content.ReadAsStringAsync();
                if (!resposta.IsSuccessStatusCode)
                    throw new Exception("Can't get a success status response. " + resposta.StatusCode.ToString());
            }

            if (method == HttpMethod.Post)
            {
                HttpResponseMessage resposta = client.PostAsync($"{url}", GerarJson(objeto)).GetAwaiter().GetResult();
                string conteudo = await resposta.Content.ReadAsStringAsync();
                if (!resposta.IsSuccessStatusCode)
                    if (await conteudo.IsValidString())
                        throw new Exception(conteudo);
                    else
                        throw new Exception("Can't get a success status response. " + resposta.StatusCode.ToString());
            }

            if (method == HttpMethod.Put)
            {

                HttpResponseMessage resposta = client.PutAsync($"{url}", GerarJson(objeto)).GetAwaiter().GetResult();
                string conteudo = await resposta.Content.ReadAsStringAsync();
                if (!resposta.IsSuccessStatusCode)
                    throw new Exception("Can't get a success status response. " + resposta.StatusCode.ToString());
            }

            if (method == HttpMethod.Delete)
            {
                HttpResponseMessage resposta = client.DeleteAsync($"{url}").GetAwaiter().GetResult();
                string conteudo = await resposta.Content.ReadAsStringAsync();
                if (!resposta.IsSuccessStatusCode)
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
