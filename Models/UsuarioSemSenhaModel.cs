using ControleDeContatos.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleDeContatos.Models
{
	public class UsuarioSemSenhaModel
	{
		[Key]
		public int UserId { get; set; }
		[Required(ErrorMessage = "Digite o nome do usuario!")]
		public string Nome { get; set; }
		[Required(ErrorMessage = "Digite o login do usuario!")]
		public string Login { get; set; }
		[Required(ErrorMessage = "Digite o email do usuario!")]
		[EmailAddress(ErrorMessage = "O email informado não é válido")]
		public string UserEmail { get; set; }
		[Required(ErrorMessage = "Informe o perfil do usuário!")]
		public PerfilEnum? Perfil { get; set; }

	}
}
