using ControleDeContatos.Filters;
using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeContatos.Controllers
{
    [PaginaRestritaAdmin]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }
        public IActionResult Index()
        {
            List<UsuarioModel> usuario = _usuarioRepositorio.Visualizar();
            return View(usuario);
        }


        public IActionResult Criar()
        {
            return View();
        }


        public IActionResult Editar(int id)
        {
            var usuario = _usuarioRepositorio.ObterUsuarioPorId(id);
            return View(usuario);
        }


        public IActionResult ApagarConfirmacao(int id)
        {
            var usuario = _usuarioRepositorio.ObterUsuarioPorId(id);
            return View(usuario);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Criar(UsuarioModel usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _usuarioRepositorio.Adicionar(usuario);
                    TempData["MensagemSucesso"] = "Usuário cadastrado com sucesso";
                    return RedirectToAction("Index");
                }
                return View(usuario);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos cadastrar seu usuário," +
                    $" tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Alterar(UsuarioSemSenhaModel usuarioSemSenha)
        {
            UsuarioModel usuario = null;
            try
            {
                if (ModelState.IsValid)
                {
                    usuario = new UsuarioModel
                    {
                        UserId = usuarioSemSenha.UserId,
                        Nome = usuarioSemSenha.Nome,
                        Login = usuarioSemSenha.Login,
                        UserEmail = usuarioSemSenha.UserEmail,
                        Perfil = usuarioSemSenha.Perfil,
                    };

                    usuario = _usuarioRepositorio.Edit(usuario);
                    TempData["MensagemSucesso"] = "Usuário alterado com sucesso";
                    return RedirectToAction("Index");
                }
                // Como nas Views não existe uma View "Alterar", da erro
                // Para consertar o erro é necessário forçar o retorno da View
                // Editar junto com o objeto que vc quer retornar;
                return View("Editar", usuario);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos alterar seu usuário," +
                    $" tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        public IActionResult Apagar(int Id)
        {
            try
            {
                bool apagado = _usuarioRepositorio.Apagar(Id);
                if (apagado == true)
                {
                    TempData["MensagemSucesso"] = "Usuário apagado com sucesso";
                }
                else
                {
                    TempData["MensagemErro"] = $"Ops, não conseguimos Apagar seu usuário!";
                }
                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos Apagar seu usuário," +
                    $" mais detalhes do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
