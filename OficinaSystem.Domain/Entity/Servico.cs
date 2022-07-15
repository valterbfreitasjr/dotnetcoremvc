namespace OficinaSystem.Domain.Entity
{
    public class Servico
    {
        public int Id { get; set; }

        public double Preco { get; set; }

        public string Descricao { get; set; }

        public Servico()
        {

        }
            
        public Servico(int id, double preco, string descricao)
        {
            Id = id;
            Preco = preco;
            Descricao = descricao;
        }
    }
}
