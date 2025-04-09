using System.Drawing;

public class Controller(View view, Output output, Calculations calc)
{

    private readonly View _view = view;
    private readonly Output _output = output;
    private readonly Calculations _calc = calc;
    public int x = 5;
    public int y = 3;

    public void Left()
    {
        if (y == 0)
        {
            y = _output.rows;
        }
        else
        {
            y--;
        }
    }
    public void Right()
    {
        if (y == _output.rows)
        {
            y = 0;
        }
        else
        {
            y++;
        }
    }
    public void Down()
    {
        if (x == _output.columns)
        {
            x = 0;
        }
        else
        {
            x++;
        }
    }
    public void Up()
    {
        if (x == 0)
        {
            x = _output.columns;
        }
        else
        {
            x--;
        }
    }

    public void Enter()
    {

        switch (x, y)
        {
            case (0,0): //sqr
            case (0,1): //fib
            case (0,2): //!
            case (0,3): //notset
            case (1, 0):                                                                     // "%"
            case (1, 2):                                                                     // "/"
            case (2, 3):                                                                     // "*"
            case (3, 3):                                                                     // "-"
            case (4, 3): Operation(Output.buttons[x, y]); break;                               // "+"
            case (1, 3): Backspace(); break;                                                 // "<"
            case (1, 1): ClearAll(); break;                                                  // "C"
            case (5, 2): Dot(); break;
            case (5, 0): Negative(); break;                                                  // "+/-"
            case (5, 3): Total(); break;                                                              // "="
            default: _output.Line += Output.buttons[x, y]; break;                               // "0,1,2,3,4,5,6,7,8,9"
        }
    }
    public void Dot(){
        _output.Line += _output.Line!=null && !_output.Line.Contains('.') ? Output.buttons[5,2] : ""; 
    }
    public void Backspace()
    {
        if (!string.IsNullOrEmpty(_output.Line) && _output.Line.Length > 0)
        {
            _output.Line = _output.Line.Remove(_output.Line.Length - 1, 1);
        }
    }
    public void Operation(string @operator)
    {
        if (string.IsNullOrEmpty(_output.Line) && string.IsNullOrEmpty(_calc.FirstOperand))
        {
            if (@operator == "-")
            {
                Negative();
            }
        }
        else if (string.IsNullOrEmpty(_calc.Operation) && !string.IsNullOrEmpty(_output.Line))
        {
            _calc.Operation = @operator;
            _calc.FirstOperand = _output.Line;
            ClearOutput();
        }
        else if (!string.IsNullOrEmpty(_calc.FirstOperand) && string.IsNullOrEmpty(_output.Line))
        {
            _calc.Operation = @operator;
        }
        else if (!string.IsNullOrEmpty(_calc.SecondOperand))
        {
            _calc.FirstOperand = _calc.Result();
            _calc.Operation = @operator;
            _calc.SecondOperand = string.Empty;
            ClearOutput();
        }
        else
        {
            Total(@operator);
        }
    }
    public void Total(string? @operator = null)
    { // set argument for series of operations
        if (!string.IsNullOrEmpty(_output.Line) && !string.IsNullOrEmpty(_calc.FirstOperand) && !string.IsNullOrEmpty(_calc.Operation))
        {
            _calc.SecondOperand = _output.Line;
            _calc.FirstOperand = _calc.Result();
            _calc.Operation = @operator ?? string.Empty;
            _calc.SecondOperand = string.Empty;
            ClearOutput();
        }
    }
    public void Negative()
    {
        if (!string.IsNullOrEmpty(_output.Line))
        {
            _output.Line = _output.Line[0] != '-' ? "-" + _output.Line : _output.Line[1..];
        }
    }
    public void Digit(string digit)
    {
        _output.Line += digit;
    }
    public void ClearOutput()
    {
        _output.Line = "";
    }
    public void ClearAll()
    {
        _output.Line = "";
        _calc.FirstOperand = "";
        _calc.SecondOperand = "";
        _calc.Operation = "";
    }

    public void MainFlow()
    { 
        while (true)
        {
            Console.Clear();
            View.Log(_calc.FirstOperand, _calc.Operation, _calc.SecondOperand);
            View.OutputConsole(_output.Line);
            View.Welcome();
            View.DisplayStringMatrix(Output.buttons, (x, y));
            ConsoleKeyInfo key;
            key = Console.ReadKey(true);
            switch (key.Key)
            {
                case ConsoleKey.Escape: Console.Clear(); Environment.Exit(0); break;
                case ConsoleKey.LeftArrow or ConsoleKey.A: Left(); continue;
                case ConsoleKey.RightArrow or ConsoleKey.D: Right(); continue;
                case ConsoleKey.UpArrow or ConsoleKey.W: Up(); continue;
                case ConsoleKey.DownArrow or ConsoleKey.S: Down(); continue;
                case ConsoleKey.Enter or ConsoleKey.Spacebar: Enter(); continue;
                case ConsoleKey.Backspace: Backspace(); continue;
                case ConsoleKey.Add: Operation(Output.buttons[4, 3]); continue;
                case ConsoleKey.Divide: Operation(Output.buttons[1, 2]); continue;
                case ConsoleKey.Subtract: Operation(Output.buttons[3, 3]); continue;
                case ConsoleKey.Multiply: Operation(Output.buttons[2, 3]); continue;
                default: if (char.IsDigit(key.KeyChar)) Digit(key.KeyChar.ToString()); continue;
            }
        }
    }
}