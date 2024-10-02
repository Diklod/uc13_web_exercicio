using Microsoft.AspNetCore.Mvc;
using uc13_web_exercicio.Database;
using uc13_web_exercicio.Models;

namespace uc13_web_exercicio.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly JoggDb _jogoDb =  new JoggDb();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult AdicionarJogo()
        {
            return View();
        }

        public IActionResult JogosCadastrados()
        {
            return View();
        }

        [HttpPost]
        public JsonResult CadastrarJogo([FromBody] Jogo jogo)
        {
            bool response = _jogoDb.Add(jogo);

            if (response)
            {
                return new JsonResult(new { success = true, data = "Cadastrado" });
            }

            else
            {
                return new JsonResult(new { success = false, data = "Erro" });
            }
        }

        [HttpPost]
        public JsonResult EditarJogo([FromBody]Jogo jogo)
        {
            bool response = _jogoDb.Update(jogo);

            if (response)
            {
                return new JsonResult(new { success = true, data = "Alterado com sucesso" });
            }

            else
            {
                return new JsonResult(new { success = false, data = "Erro" });
            }
        }

        [HttpGet]
        public JsonResult ListarJogos()
        {
            List<Jogo> response = _jogoDb.GetAll();

            if (response.Count > 0)
            {
                return new JsonResult(new { success = true, data = response });
            }

            else
            {
                return new JsonResult(new { success = false, data = "Erro" });
            }
        }

        [HttpDelete]
        public JsonResult Delete(int id)
        {
            bool response = _jogoDb.Delete(id);
            
            if (response)
            {
                return new JsonResult(new { success = true, message = "Excluído com sucesso" });
            }

            else
            {
                return new JsonResult(new { success = false, data = "Erro" });
            }
            
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                Jogo jogo = _jogoDb.Get(id);


                if (jogo.Id > 0)
                {
                    return View("AdicionarJogo", jogo);
                }
                else
                    return RedirectToAction("JogosCadastrados");
            }
            catch (Exception ex)
            {
                return RedirectToAction("JogosCadastrados");
            }
        }
    }
}