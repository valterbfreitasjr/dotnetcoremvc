namespace OficinaSystem.Domain.Entity
{
    public class Produto
    {
        public int Id { get; set; }

        public decimal Preco { get; set; }

        public string Descricao { get; set; }

        public Produto()
        {

        }

        public void SetarDadosIniciais(decimal preco, string descricao)
        {
            Preco = preco;
            Descricao = descricao;
        }

        public Produto(int id, decimal preco, string descricao)
        {
            Id = id;
            Preco = preco;
            Descricao = descricao;
        }
    }
}
