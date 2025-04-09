public class Output
{
    public string? line;
    public string? Line{
        get{ return line;}
        set { line = !string.IsNullOrEmpty(value) ? value : "";}
    }
    public static string[,] buttons = new string[6, 4]
        {
            { "sqr", "fib", "!", ""},
            { "%", "C", "/", "<"},
            { "7", "8", "9", "*"},
            { "4", "5", "6" , "-"},
            { "1", "2", "3" , "+"},
            { "+/-", "0", "." , "="}
        };
    public int columns = buttons.GetLength(0) - 1;
    public int rows = buttons.GetLength(1) - 1;
}