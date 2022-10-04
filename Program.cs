namespace CA221003
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Fish f = new Fish(
                weight: 25f,
                predator: true,
                swimTop: 30,
                swimDepth: 23,
                species: "Harcsaaaaaaa"
                );
            f.Weight += 5;
        }
    }
}