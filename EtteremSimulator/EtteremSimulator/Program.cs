namespace EtteremSimulator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Etterem etterem = new Etterem();
            UI ui = new UI(etterem);
            ui.Menu();
        }
    }
}
