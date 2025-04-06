
using System.Text;
public class Input{
    public void GetKeyReading()
    {
        StringBuilder input = new StringBuilder();
        ConsoleKeyInfo key;

        while (true)
        {
            key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Enter)
            {
                throw new EnterException();
            }
            else if (key.Key == ConsoleKey.Escape)
            {
                Console.WriteLine();
                return; //return null to make ?? syntax work
            }
            else if (key.Key == ConsoleKey.Backspace)
            {
                throw new BackspaceException();
            }
            // else if (!char.IsControl(key.KeyChar))
            // {
            //     input.Append(key.KeyChar);
            //     Console.Write(key.KeyChar);
            // }
            else if(key.Key == ConsoleKey.LeftArrow){
                throw new LeftArrowException();
            }
            else if(key.Key == ConsoleKey.RightArrow){
                throw new RightArrowException();
            }
            else if(key.Key == ConsoleKey.DownArrow){
                throw new DownArrowException();
            }
            else if(key.Key == ConsoleKey.UpArrow){
                throw new UpArrowException();
            }else if(char.IsDigit(key.KeyChar)){
                throw new DigitException(key.KeyChar.ToString());
            }
        }
    }
}


