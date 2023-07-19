using ControleDeContatos.Helper;
using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeContatos.Controllers
{
	public class LoginController : Controller
	{
		public readonly IUsuarioRepositorio _usuarioRepositorio;
		public readonly ISessao _sessao;
		public LoginController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao)
		{
			_usuarioRepositorio = usuarioRepositorio;
			_sessao = sessao;
		}
		public IActionResult Index()
		{
			// Se o usuário estiver logado, redirecionar para home
			if (_sessao.BuscarSessaoDoUsuario() != null) return RedirectToAction("Index", "Home");
			return View();
		}

		public IActionResult Sair()
		{
			_sessao.RemoverSessaoUsuario();
			return RedirectToAction("Index", "Login");
		}

		[HttpPost]
		public IActionResult Entrar(LoginModel loginModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
					UsuarioModel usuario = _usuarioRepositorio.BuscarPorLogin(loginModel.Login);
					if(usuario != null)
					{
						if (usuario.SenhaValida(loginModel.Senha))
						{
							_sessao.CriarSessaoDoUsuario(usuario);
                            return RedirectToAction("Index", "Home");
                        }
                        TempData["MensagemErro"] = $"A senha do usuário é inválido, por favor, tente novamente";
                    }
					TempData["MensagemErro"] = $"Usuário e/ou senha inválido(s), por favor, tente novamente";
				}
				return View("Index");
			}
			catch (Exception erro)
			{
				TempData["MensagemErro"] = $"Ops, não conseguimos realizar seu login, " +
					$"tente novamente, detalhe do erro: {erro.Message}";
				return RedirectToAction("Index");
			}
		}
	}
}
