using ControleDeContatos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Newtonsoft.Json;

namespace ControleDeContatos.Filters
{
	public class PaginaParaUsuarioLogado : ActionFilterAttribute
	{
		// Filtros para ver está logado
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			// Vai pegar a string da sessão e ver se está na sessão
			string sessaoUsuario = context.HttpContext.Session.GetString("sessaoUsuarioLogado");
			if (string.IsNullOrEmpty(sessaoUsuario))
			{
				//  se não estiver vai para a página de login
				context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "SessaoNaoIniciada"}, {"action", "Index" } });
			}
			else
			{
				// Se estiver 
				UsuarioModel usuario = JsonConvert.DeserializeObject<UsuarioModel>(sessaoUsuario);
				if(usuario == null)
				{
					context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "SessaoNaoIniciada" }, { "action", "Index" } });
				}
			}
			base.OnActionExecuting(context);
		}
	}
}
