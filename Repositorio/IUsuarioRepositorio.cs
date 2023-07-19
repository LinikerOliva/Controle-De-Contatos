using ControleDeContatos.Models;

namespace ControleDeContatos.Repositorio
{
	public interface IUsuarioRepositorio
	{
		UsuarioModel BuscarPorLogin(string login);
		UsuarioModel Adicionar(UsuarioModel contato);
		UsuarioModel Edit(UsuarioModel contato);
		UsuarioModel ObterUsuarioPorId(int id);
		bool Apagar(int id);
        List<UsuarioModel> Visualizar();
	}
}
