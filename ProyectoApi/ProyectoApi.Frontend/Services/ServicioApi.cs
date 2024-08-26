using Newtonsoft.Json;
using ProyectoApi.Frontend.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace ProyectoApi.Frontend.Services
{
    public class ServicioApi : IServicioApi
    {
        private static string _usuario = string.Empty;
        private static string _clave = string.Empty;
        private static string _baseUrl = string.Empty;
        private static string _token = string.Empty;

        public ServicioApi()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            _usuario = builder.GetSection("ApiSetting:usuario").Value!;
            _clave = builder.GetSection("ApiSetting:clave").Value!;
            _baseUrl = builder.GetSection("ApiSetting:baseUrl").Value!;
        }

        public async Task Autenticar()
        {
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);

            var credenciales = new Credencial() { Correo = _usuario, Clave = _clave };
            var jsonContent = JsonConvert.SerializeObject(credenciales);
            Console.WriteLine("JSON Enviado: " + jsonContent);
            var content = new StringContent(JsonConvert.SerializeObject(credenciales), Encoding.UTF8, "application/json");
            Console.WriteLine(content);
            var response = await cliente.PostAsync("api/Autenticacion/Validar", content);

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                Console.WriteLine(json_respuesta); // Verifica la respuesta JSON

                var resultado = JsonConvert.DeserializeObject<ResultadoCredencial>(json_respuesta);

                if (resultado != null)
                {
                    _token = resultado.Token!;
                }
                else
                {
                    Console.WriteLine("El objeto resultado es null.");
                }
            }
            else
            {
                Console.WriteLine($"Error en la API: {response.StatusCode}");
            }
        }

        public async Task<bool> Editar(Producto objeto)
        {
            bool respuesta = false;

            await Autenticar();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var content = new StringContent(JsonConvert.SerializeObject(objeto), Encoding.UTF8, "application/json");

            var response = await cliente.PutAsync("api/Productos", content);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }

            return respuesta;
        }

        public async Task<bool> Eliminar(int idProducto)
        {
            bool respuesta = false;

            await Autenticar();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var response = await cliente.DeleteAsync($"api/Productos{idProducto}");

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }

            return respuesta;
        }

        public async Task<bool> Guardar(Producto objeto)
        {
            bool respuesta = false;

            await Autenticar();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var content = new StringContent(JsonConvert.SerializeObject(objeto), Encoding.UTF8, "application/json");

            var response = await cliente.PostAsync("api/Productos", content);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }

            return respuesta;
        }

        public async Task<List<Producto>> Lista()
        {
            List<Producto> lista = new List<Producto>();

            await Autenticar();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var response = await cliente.GetAsync("api/Productos");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                lista = JsonConvert.DeserializeObject<List<Producto>>(json_respuesta)!;
            }

            return lista;
        }

        public async Task<Producto> Obtener(int idProducto)
        {
            Producto objeto = new Producto();

            await Autenticar();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var response = await cliente.GetAsync($"api/Productos/{idProducto}");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<Producto>(json_respuesta);
                objeto = resultado!;
            }

            return objeto;
        }
    }
}