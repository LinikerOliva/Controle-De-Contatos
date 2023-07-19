using ControleDeContatos.Data;
using ControleDeContatos.Filters;
using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeContatos.Controllers
{
	[PaginaParaUsuarioLogado]
	public class ContatosController : Controller
	{
		private readonly IContatoRepositorio _contatoRepositorio;
		public ContatosController(IContatoRepositorio contatoRepositorio)
		{
			_contatoRepositorio = contatoRepositorio;
		}
		// GET: ContatosController
		public ActionResult Index()
		{
			var contato = _contatoRepositorio.Visualizar();
			return View(contato);
		}
		public IActionResult Criar()
		{
			return View();
		}
		public IActionResult Editar(int id)
		{
			var contato = _contatoRepositorio.ObterContatoPorId(id);
			return View(contato);
		}
		public IActionResult ApagarConfirmacao(int id)
		{
			var contato = _contatoRepositorio.ObterContatoPorId(id);
			return View(contato);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Criar(ContatoModel contato)
		{
			try
			{
				if (ModelState.IsValid)
				{
					_contatoRepositorio.Adicionar(contato);
					TempData["MensagemSucesso"] = "Contato cadastrado com sucesso";
					return RedirectToAction("Index");
				}
				return View(contato);
			}
			catch (Exception erro)
			{
				TempData["MensagemErro"] = $"Ops, não conseguimos cadastrar seu contato," +
					$" tente novamente, detalhe do erro: {erro.Message}";
				return RedirectToAction("Index");
			}
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Alterar(ContatoModel contato)
		{
			try
			{
				if (ModelState.IsValid)
				{
					_contatoRepositorio.Edit(contato);
					TempData["MensagemSucesso"] = "Contato alterado com sucesso";
					return RedirectToAction("Index");
				}
				// Como nas Views não existe uma View "Alterar", da erro
				// Para consertar o erro é necessário forçar o retorno da View
				// Editar junto com o objeto que vc quer retornar;
				return View("Editar", contato);
			}
			catch (Exception erro)
			{
				TempData["MensagemErro"] = $"Ops, não conseguimos alterar seu contato," +
					$" tente novamente, detalhe do erro: {erro.Message}";
				return RedirectToAction("Index");
			}
		}

		public IActionResult Apagar(int Id)
		{
			try
			{
				bool apagado = _contatoRepositorio.Apagar(Id);
				if (apagado == true)
				{
					TempData["MensagemSucesso"] = "Contato apagado com sucesso";
				}
				else
				{
					TempData["MensagemErro"] = $"Ops, não conseguimos Apagar seu contato!";
				}
				return RedirectToAction("Index");
			}
			catch (Exception erro)
			{
				TempData["MensagemErro"] = $"Ops, não conseguimos Apagar seu contato," +
					$" mais detalhes do erro: {erro.Message}";
				return RedirectToAction("Index");
			}
		}
	}
}

