using System.Dynamic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using IntegraBrasilApi.Dtos;
using IntegraBrasilApi.Interfaces;
using IntegraBrasilApi.Model;

namespace IntegraBrasilApi.Rest
{
    public class BrasilApiRest : IBrasilApi
    {
        public async Task<ResponseGenerico<EnderecoModel>> BuscarEnderecoPorCep(string cep)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://brasilapi.com.br/api/cep/v1/{cep}");

            var response = new ResponseGenerico<EnderecoModel>();
            using (var client = new HttpClient())
            {
                try
                {
                    var responseBrasilApi = await client.SendAsync(request);
                    var contentResp = await responseBrasilApi.Content.ReadAsStringAsync();
                    
                    Console.WriteLine("Response content: " + contentResp); // Log do conteúdo da resposta

                    try
                    {
                        var objResponse = JsonSerializer.Deserialize<EnderecoModel>(contentResp);
                        Console.WriteLine("Deserialized object: " + (objResponse != null ? objResponse.ToString() : "null")); // Log do objeto desserializado
                        
                        if (responseBrasilApi.IsSuccessStatusCode)
                        {
                            response.CodigoHttp = responseBrasilApi.StatusCode;
                            response.DadosRetorno = objResponse;
                        }
                        else
                        {
                            response.CodigoHttp = responseBrasilApi.StatusCode;
                            response.ErroRetorno = JsonSerializer.Deserialize<ExpandoObject>(contentResp);
                        }
                    }
                    catch (JsonException jsonEx)
                    {
                        Console.WriteLine("JSON Deserialization error: " + jsonEx.Message); // Log de erro de desserialização
                        dynamic erroRetorno = new ExpandoObject();
                        erroRetorno.message = "Erro ao desserializar a resposta.";
                        response.ErroRetorno = erroRetorno;
                    }
                }
                catch (HttpRequestException httpEx)
                {
                    Console.WriteLine("HTTP Request error: " + httpEx.Message); // Log de erro de solicitação HTTP
                    response.CodigoHttp = System.Net.HttpStatusCode.ServiceUnavailable;
                    dynamic erroRetorno = new ExpandoObject();
                    erroRetorno.message = "Erro na solicitação HTTP.";
                    response.ErroRetorno = erroRetorno;
                }
            }

            return response;
        }

        public Task<ResponseGenerico<List<BancoModel>>> BuscarTodosBancos()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseGenerico<BancoModel>> BuscaBanco(string codigoBanco)
        {
            throw new NotImplementedException();
        }
    }
}