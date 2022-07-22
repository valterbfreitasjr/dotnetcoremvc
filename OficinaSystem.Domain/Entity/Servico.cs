namespace OficinaSystem.Domain.Entity
{
    public class Servico
    {
        public int Id { get; set; }

        public decimal Preco { get; set; }

        public string Descricao { get; set; }

        public Servico()
        {

        }
            
        public Servico(int id, decimal preco, string descricao)
        {
            Id = id;
            Preco = preco;
            Descricao = descricao;
        }
    }
}
