using ControleDeContatos.Models;

namespace ControleDeContatos.Repositorio
{
	public interface IContatoRepositorio
	{
		ContatoModel Adicionar(ContatoModel contato);
		ContatoModel Edit(ContatoModel contato);
		ContatoModel ObterContatoPorId(int id);
		bool Apagar(int Id);
        List<ContatoModel> Visualizar();
	}
}
