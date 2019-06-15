using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AplicacaoWebExemplo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AplicacaoWebExemplo.Controllers
{   
    
    public class GithubController : Controller
    {
        // GET: Github não utilizado => Redirecionado para Create()
        public ActionResult Index()
        {
            
            return View();
        }

        // GET: Github/Create
        public ActionResult Create()
        {

            StreamReader leitor = new StreamReader("Models/Dados/Repositorios.txt");
            string texto;

            IList<RepositoriosInfo> repositorios = new List<RepositoriosInfo>();

            while((texto = leitor.ReadLine()) != null)
            {
                string[] linha = texto.Split(",");
                RepositoriosInfo rep = new RepositoriosInfo();

                //rep.ID = long.Parse(linha[0]);
                rep.Nome = linha[0];
                rep.Descricao = linha[1];
                rep.UltimaData = linha[3];
                rep.CriadorReposit = linha[2];
                //rep.Linguagem = linha[5];

                repositorios.Add(rep);
                
            }
           
            string resultado = JsonConvert.SerializeObject(repositorios, Formatting.None);
            
            ViewData["RepositoriosInfo"]= resultado;
            
            leitor.Close();
            return View();
        }
        [HttpPost]
        public ActionResult Repositorio([FromBody]RepositoriosInfo rep)
        {
            string linha = rep.ID +","+rep.Nome+","+rep.Descricao+","+rep.CriadorReposit+","+rep.Linguagem+","+rep.UltimaData + Environment.NewLine;
            System.IO.File.AppendAllText("Models/Dados/RepositoriosTemp.txt",linha);

            return Json(new { urlT = Url.Action("Repositorio", "Github") });
        }
        public ActionResult Repositorio()
        {
            // abrir e excluir os dados do repositoriosTemp
            StreamReader leitor = new StreamReader("Models/Dados/RepositoriosTemp.txt");
            string texto;
            RepositoriosInfo rep = new RepositoriosInfo();

            while ((texto = leitor.ReadLine()) != null)
            {
                string[] linha = texto.Split(",");

                rep.ID = long.Parse(linha[0]);
                rep.Nome = linha[1];
                rep.Descricao = linha[2];
                rep.UltimaData = linha[5];
                rep.CriadorReposit = linha[3];
                rep.Linguagem = linha[4];

            }
            leitor.Close();

            StreamWriter wr = new StreamWriter("Models/Dados/RepositoriosTemp.txt",false);
            wr.Close();

            string resultado = JsonConvert.SerializeObject(rep, Formatting.None);
            ViewBag.RepositorioProxPagina = resultado;

            return View();
        }
        //Adicionar Item aos favoritos
        [HttpPost]
        public ActionResult Favoritar([FromBody] RepositoriosInfo rep)
        {
            string linha = rep.Nome + "," + rep.Descricao + "," + rep.CriadorReposit + "," + rep.UltimaData;
            //System.IO.File.AppendAllText("Models/Dados/Repositorios.txt", linha);
            StreamWriter wr = System.IO.File.AppendText("Models/Dados/Repositorios.txt");
            wr.WriteLine(linha);
            wr.Close();

            return RedirectToAction("Create", "Github");
        }

        // POST: Github/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Github/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Github/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Github/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Github/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}