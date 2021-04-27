using vulns.Models.Db;
using vulns.Models.vulnerabilidades;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace vulns.Controllers
{
    public class VulnsCounterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile FormFile, int cliente )
        {

            var filename = ContentDispositionHeaderValue.Parse(FormFile.ContentDisposition).FileName.Trim('"');
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", FormFile.FileName);
            using (System.IO.Stream stream = new FileStream(path, FileMode.Create))
            {
                await FormFile.CopyToAsync(stream);
            }
            string[] lineas = System.IO.File.ReadAllLines(path);
            List<Vulnerabilidade> vulnsdb = new List<Vulnerabilidade>();
            List<vulnerabilidades> vulnerabilidades = new List<vulnerabilidades>();
            

            foreach (var linea in lineas)
            {
                var valores = linea.Split(',');

                vulnerabilidades vuln = new vulnerabilidades();
                vuln.cve= valores[0];
                vuln.risk = valores[1];
                vuln.name = valores[2];

                vulnerabilidades.Add(vuln);
            }
            
            using (Models.Db.CounterVulnsContext db = new Models.Db.CounterVulnsContext())
            {

                Vulnerabilidade vulns = new Vulnerabilidade();

                vulns.CriticasVulnerabilidades = (from d in vulnerabilidades where d.risk == "\"Critical\"" select d).Count();
                vulns.AltasVulnerabilidades = (from d in vulnerabilidades where d.risk == "\"High\"" select d).Count();
                vulns.MediasVulnerabilidades = (from d in vulnerabilidades where d.risk == "\"Medium\"" select d).Count();
                vulns.IdClienteVulnerabilidades = cliente;

                db.Vulnerabilidades.Add(vulns);
                db.SaveChanges();


                return RedirectToAction("Index", "Home");
            }
        }
    }
}
