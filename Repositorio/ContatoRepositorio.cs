using ControleDeContatos.Data;
using ControleDeContatos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControleDeContatos.Repositorio
{
    public class ContatoRepositorio : IContatoRepositorio
    {
        private readonly BancoContext _bancoContext;
        public ContatoRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }
        public ContatoModel Adicionar(ContatoModel contato)
        {
            // Inserir no banco de dados
            _bancoContext.Contatos.Add(contato);
            // Salvando dados no banco de dados
            _bancoContext.SaveChanges();
            return contato;
        }
        // Ver a tabela do banco de dados no grid feito 
        public List<ContatoModel> Visualizar()
        {
            var contatos = _bancoContext.Contatos.ToList();
            return contatos;
        }
        // Obter id para ser editado ou removido
        public ContatoModel ObterContatoPorId(int id)
        {
            return _bancoContext.Contatos.FirstOrDefault(x => x.Id == id);
        }

        // Editar/Atualizar registro
        public ContatoModel Edit(ContatoModel contato)
        {
            // Obter id do registro que quer editar
            var contatoExistente = ObterContatoPorId(contato.Id);
            // Condição se não existir registro
            if (contatoExistente == null) throw new System.Exception("Houve um erro na atualização do seu contato!");
            // trocando informações entre a velha pra nova
            contatoExistente.Nome = contato.Nome;
            contatoExistente.Email = contato.Email;
            contatoExistente.Celular = contato.Celular;
            // fazendo a atualização 
            _bancoContext.Contatos.Update(contatoExistente);
            // salvando os dados
            _bancoContext.SaveChanges();
            return contatoExistente;
        }


        public bool Apagar(int Id)
        {
            var contatoExistente = ObterContatoPorId(Id);
            // Condição se não existir registro
            if (contatoExistente == null) throw new System.Exception("Houve um erro na exclusão do seu contato!");

            _bancoContext.Contatos.Remove(contatoExistente);
            _bancoContext.SaveChanges();
            return true;
        }
    }
}
