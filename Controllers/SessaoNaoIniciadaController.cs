using ControleDeContatos.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeContatos.Controllers
{
	[PaginaParaUsuarioLogado]
	public class SessaoNaoIniciadaController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
