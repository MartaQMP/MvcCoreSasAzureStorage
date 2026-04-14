using MvcCoreSasAzureStorage.Models;
using System.Xml.Linq;

namespace MvcCoreSasAzureStorage.Helpers
{
    public class HelperXML
    {
        private XDocument document;

        public HelperXML()
        {
            string pathResourceXML = "MvcCoreSasAzureStorage.Documents.alumnos_tables.xml";
            Stream stream = this.GetType().Assembly.GetManifestResourceStream(pathResourceXML);
            this.document = XDocument.Load(stream);
        }

        public List<Alumno> GetAlumnos()
        {
            var consulta = from datos in this.document.Descendants("alumno")
                           select new Alumno
                           {
                               IdAlumno = int.Parse(datos.Element("idalumno").Value),
                               Nombre = datos.Element("nombre").Value,
                               Curso = datos.Element("curso").Value,
                               Apellidos = datos.Element("apellidos").Value,
                               Nota = int.Parse(datos.Element("nota").Value),
                           };
            return consulta.ToList();
        }
    }
}
