using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleDeContatos.Models
{
	public class ContatoModel
	{

		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "Digite o nome do contato!")]
		public string Nome { get; set; }

		[Required(ErrorMessage = "Digite o email do contato!")]
		[EmailAddress(ErrorMessage = "O email informado não é válido")]
        public string Email { get; set; }

		[Required(ErrorMessage = "Digite o celular do contato!")]
		[Phone(ErrorMessage = "O Celular informado não é válido!")]
        public string Celular { get; set; }

    }
}
