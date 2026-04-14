using Azure.Data.Tables;
using Microsoft.AspNetCore.Mvc;
using MvcCoreSasAzureStorage.Helpers;
using MvcCoreSasAzureStorage.Models;
using System.Threading.Tasks;

namespace MvcCoreSasAzureStorage.Controllers
{
    public class MigracionController : Controller
    {
        private HelperXML helper;
        private IConfiguration configuration;

        public MigracionController(HelperXML helper, IConfiguration configuration)
        {
            this.helper = helper;
            this.configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index (string accion)
        {
            // EN ESTE METODO LO QUE NECESITAMOS SON LAS KEYS DE AZURE STORAGE
            // ESTA FUNCIONALIDAD DEBERIA ESTAR EN OTRO PROYECTO
            string azureKeys = this.configuration.GetValue<string>("AzureKeys:StorageAccount");
            TableServiceClient tableService = new TableServiceClient(azureKeys);
            TableClient tableClient = tableService.GetTableClient("alumnos");
            await tableClient.CreateIfNotExistsAsync();
            List<Alumno> alumnos = this.helper.GetAlumnos();
            foreach(Alumno al in alumnos)
            {
                tableClient.AddEntityAsync<Alumno>(al);
            }
            ViewBag.Mensaje = "Migracion de alumnos completada";
            return View();
        }
    }
}
