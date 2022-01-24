using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using LibrariesEmpleado;
using System.Web;
using System.Collections.Specialized;

namespace MvcCoreConsumoApiEmpleados2022.Services
{
    public class ServiceApiEmpleados
    {
        private string Url;
        private MediaTypeWithQualityHeaderValue header;
        private NameValueCollection queryString;

        public ServiceApiEmpleados(String url)
        {
            this.Url = url;
            this.header =
                new MediaTypeWithQualityHeaderValue("application/json");
            this.queryString = HttpUtility.ParseQueryString(string.Empty);
        }

        //LO UNICO QUE VA A CAMBIAR EN LA LLAMADA
        //DE ESTE METODO SON DOS COSAS:
        //1) OBJETO QUE DEVUELVE
        //2) PETICION AL SERVICIO
        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                ///api/empleados?
                request = request + "?" + this.queryString;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                client.DefaultRequestHeaders.CacheControl =
                     CacheControlHeaderValue.Parse("no-cache");
                HttpResponseMessage response =
                    await client.GetAsync(this.Url + request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        public async Task<List<Empleado>> GetEmpleadosManagementAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                //TENEMOS QUE MODIFICAR EL REQUEST
                //SE INCLUYE AL FINAL UNA CADENA VACIA PARA
                //LA PETICION
                var queryString = HttpUtility.ParseQueryString(string.Empty);
                string request = "/api/empleados?" + queryString;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                //DEBEMOS INDICAR EN LAS LLAMADAS QUE NO UTILIZAMOS CACHE
                client.DefaultRequestHeaders.CacheControl =
                    CacheControlHeaderValue.Parse("no-cache");
                //LA PETICION AL API MANAGEMENT SE REALIZA CON LA URL
                //Y EL REQUEST A LA VEZ
                HttpResponseMessage response =
                    await client.GetAsync(this.Url + request);
                if (response.IsSuccessStatusCode)
                {
                    List<Empleado> empleados =
                        await response.Content.ReadAsAsync<List<Empleado>>();
                    return empleados;
                }
                else
                {
                    return null;
                }
            }
        }


        public async Task<List<Empleado>> GetEmpleadosAsync()
        {
            //NECESITAMOS LA PETICION AL SERVICIO
            string request = "/api/empleados";
            //LLAMAMOS AL METODO GENERICO INDICANDO LO QUE DESEAMOS
            //RECUPERAR
            List<Empleado> empleados =
                await this.CallApiAsync<List<Empleado>>(request);
            return empleados;
        }

        public async Task<List<string>> GetOficiosAsync()
        {
            string request = "/api/empleados/oficios";
            List<string> oficios = await this.CallApiAsync<List<string>>(request);
            return oficios;
        }

        public async Task<Empleado> FindEmpleadoAsync(int id)
        {
            string request = "/api/empleados/" + id;
            Empleado emp = await this.CallApiAsync<Empleado>(request);
            return emp;
        }

        public async Task<List<Empleado>> GetEmpleadosOficioAsync(string oficio)
        {
            string request = "/api/empleados/empleadosoficio/" + oficio;
            List<Empleado> empleados =
                await this.CallApiAsync<List<Empleado>>(request);
            return empleados;
        }
    }
}
