namespace uc13_web_exercicio.Models
{
    public class Jogo
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Genero { get; set; }
        public string Classificacao { get; set; }
        public string Idiomas { get; set; }
        public float Preco { get; set;}
        public bool Multiplayer { get; set; }
        public string ConfigMinimina { get; set; }
        public string ConfigRecomendada { get; set; }
        public string Resumo { get; set; }
    }
}
