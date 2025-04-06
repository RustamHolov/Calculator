namespace Calculator;

class Program
{
    static void Main(string[] args)
    {
        Controller controller = new Controller(new View(), new Output(), new Calculations());
        controller.MainFlow();
    }
}
