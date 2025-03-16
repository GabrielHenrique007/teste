using System;

public class Personagem
{
    public string Nome { get; set; }
    public int Vida { get; set; }
    public int Ataque { get; set; }
    public Personagem Proximo { get; set; }

    public Personagem(string nome, int vida, int ataque)
    {
        Nome = nome;
        Vida = vida;
        Ataque = ataque;
        Proximo = null;
    }

    public void Atacar(Personagem inimigo)
    {
        inimigo.Vida -= Ataque;
        Console.WriteLine($"{Nome} ataca {inimigo.Nome} causando {Ataque} de dano!");
        Console.WriteLine($"{inimigo.Nome} agora tem {inimigo.Vida} de vida.");
    }
}

public class ListaPersonagens
{
    private Personagem cabeca;

    public void AdicionarPersonagem(string nome, int vida, int ataque)
    {
        Personagem novoPersonagem = new Personagem(nome, vida, ataque);
        if (cabeca == null)
        {
            cabeca = novoPersonagem;
        }
        else
        {
            Personagem atual = cabeca;
            while (atual.Proximo != null)
            {
                atual = atual.Proximo;
            }
            atual.Proximo = novoPersonagem;
        }
    }

    public Personagem ObterPersonagem(string nome)
    {
        Personagem atual = cabeca;
        while (atual != null)
        {
            if (atual.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase))
            {
                return atual;
            }
            atual = atual.Proximo;
        }
        return null;
    }

    public void ListarPersonagens()
    {
        Personagem atual = cabeca;
        while (atual != null)
        {
            Console.WriteLine($"Nome: {atual.Nome}, Vida: {atual.Vida}, Ataque: {atual.Ataque}");
            atual = atual.Proximo;
        }
    }
}

class Jogo
{
    private ListaPersonagens personagens;

    public Jogo()
    {
        personagens = new ListaPersonagens();
    }

    public void Iniciar()
    {
        // Adicionando personagens
        personagens.AdicionarPersonagem("Guerreiro", 100, 20);
        personagens.AdicionarPersonagem("Mago", 80, 25);
        personagens.AdicionarPersonagem("Arqueiro", 90, 15);

        Console.WriteLine("Personagens disponíveis:");
        personagens.ListarPersonagens();

        // Iniciar combate
        Combater();
    }

    private void Combater()
    {
        Personagem guerreiro = personagens.ObterPersonagem("Guerreiro");
        Personagem mago = personagens.ObterPersonagem("Mago");

        Console.WriteLine("\nIniciando combate entre Guerreiro e Mago...\n");

        while (guerreiro.Vida > 0 && mago.Vida > 0)
        {
            guerreiro.Atacar(mago);
            if (mago.Vida > 0)
            {
                mago.Atacar(guerreiro);
            }
        }

        if (guerreiro.Vida <= 0)
        {
            Console.WriteLine($"{guerreiro.Nome} foi derrotado!");
        }
        else
        {
            Console.WriteLine($"{mago.Nome} foi derrotado!");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Jogo jogo = new Jogo();
        jogo.Iniciar();
    }
}