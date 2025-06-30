using Microsoft.AspNetCore.Mvc;
using trataForms.Models;

namespace repositorio.Controllers
{
    public class Cadastro : Controller
    {
        [HttpPost]
        public IActionResult RegistroConsumidor(string email, string senha)
        {
            var form = new FomularioViewModel
            {
                email = email,
                senha = senha,
                TipoUsuario = "consumidor"
            };

            bool sucesso = RepositorioFomularios.AddFormulario(form);
            if (sucesso)
                return RedirectToAction("Sucesso");
            else
                return View("Erro");
        }

        [HttpPost]
        public IActionResult RegistroVendedor(string email, string senha, string nome_loja, string descricao)
        {
            var form = new FomularioViewModel
            {
                email = email,
                senha = senha,
                nome_loja = nome_loja,
                descricao = descricao,
                TipoUsuario = "vendedor"
            };

            bool sucesso = RepositorioFomularios.AddFormulario(form);
            if (sucesso)
                return RedirectToAction("FormLoja");
            else
                return View("Erro");
        }
    }
}