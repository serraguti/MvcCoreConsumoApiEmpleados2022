using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using LibrariesEmpleado;

namespace MvcCoreConsumoApiEmpleados2022.Services
{
    public class ServiceApiEmpleados
    {
        private string Url;
        private MediaTypeWithQualityHeaderValue header;

        public ServiceApiEmpleados(String url)
        {
            this.Url = url;
            this.header =
                new MediaTypeWithQualityHeaderValue("application/json");
        }

        //LO UNICO QUE VA A CAMBIAR EN LA LLAMADA
        //DE ESTE METODO SON DOS COSAS:
        //1) OBJETO QUE DEVUELVE
        //2) PETICION AL SERVICIO
        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.Url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response =
                    await client.GetAsync(request);
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
