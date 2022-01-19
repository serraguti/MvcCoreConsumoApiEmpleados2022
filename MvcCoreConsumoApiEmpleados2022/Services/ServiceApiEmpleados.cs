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

        public async Task<List<Empleado>> GetEmpleadosAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/empleados";
                client.BaseAddress = new Uri(this.Url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response =
                    await client.GetAsync(request);
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
    }
}
