using ControleDeContatos.Data;
using ControleDeContatos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControleDeContatos.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
	{
        private readonly BancoContext _bancoContext;
        public UsuarioRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public UsuarioModel BuscarPorLogin(string login)
        {
            return _bancoContext.Usuarios.FirstOrDefault(x => x.Login.ToUpper() == login.ToUpper());
        }
        public UsuarioModel Adicionar(UsuarioModel usuario)
        {
            usuario.DataCadastro = DateTime.Now;
            // Inserir no banco de dados
            _bancoContext.Usuarios.Add(usuario);
            // Salvando dados no banco de dados
            _bancoContext.SaveChanges();
            return usuario;
        }
        // Ver a tabela do banco de dados no grid feito 
        public List<UsuarioModel> Visualizar()
        {
            var usuario = _bancoContext.Usuarios.ToList();
            return usuario;
        }
        // Obter id para ser editado ou removido
        public UsuarioModel ObterUsuarioPorId(int id)
        {
            return _bancoContext.Usuarios.FirstOrDefault(x => x.UserId == id);
        }

        // Editar/Atualizar registro
        public UsuarioModel Edit(UsuarioModel usuario)
        {
            // Obter id do registro que quer editar
            var usuarioExistente = ObterUsuarioPorId(usuario.UserId);
            // Condição se não existir registro
            if (usuarioExistente == null) throw new System.Exception("Houve um erro na atualização do seu usuário!");
            // trocando informações entre a velha pra nova
            usuarioExistente.Nome = usuario.Nome;
            usuarioExistente.UserEmail = usuario.UserEmail;
            usuarioExistente.Login = usuario.Login;
            usuarioExistente.Perfil = usuario.Perfil;
			usuarioExistente.DataAtualizacao = DateTime.Now;
			// fazendo a atualização 
			_bancoContext.Usuarios.Update(usuarioExistente);
            // salvando os dados
            _bancoContext.SaveChanges();
            return usuarioExistente;
        }


        public bool Apagar(int Id)
        {
            var usuarioExistente = ObterUsuarioPorId(Id);
            // Condição se não existir registro
            if (usuarioExistente == null) throw new System.Exception("Houve um erro na exclusão do seu usuário!");

            _bancoContext.Usuarios.Remove(usuarioExistente);
            _bancoContext.SaveChanges();
            return true;
        }
    }
}
