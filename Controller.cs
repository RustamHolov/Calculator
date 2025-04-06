public class Controller
{

    private View _view;
    private Output _output;
    private Calculations _calc;
    public Controller( View view, Output output, Calculations calc)
    {
        _view = view;
        _output = output;
        _calc = calc;
    }
    static int[,] coords = new int[4, 5];
    
    public int x = 0;
    public int y = 0;

    public void LeftArrow()
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
    public void RightArrow()
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
    public void DownArrow()
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
    public void UpArrow()
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
    
    public void Enter() {

        switch (x,y){
            case (0,0):                                                                     // "%"
            case (0,2):                                                                     // "/"
            case (1,3):                                                                     // "*"
            case (2,3):                                                                     // "-"
            case (3,3): Calculate(Output.buttons[x,y]); break;                              // "+"
            case (0,3): Backspace(); break;                                                 // "<"
            case (0,1): ClearAll(); break;                                                  // "C"
            case (4,0): Negative(); break;                                                  // "+/-"
            case (4,3): Result(); break;                                                              // "="
            default: _output.Line += Output.buttons[x, y]; break;                               // ".,0,1,2,3,4,5,6,7,8,9"
        }
    }
    public void Backspace(){
        if(!string.IsNullOrEmpty(_output.Line) && _output.Line.Length > 0){
            _output.Line = _output.Line.Remove(_output.Line.Length - 1, 1);
        }
    }
    public void Calculate(string operation){
        if(string.IsNullOrEmpty(_output.Line) && string.IsNullOrEmpty(_calc.FirstOperand)){
            if(operation == "-"){
                Negative();
            }
        }
        else if (string.IsNullOrEmpty(_calc.Operation) && !string.IsNullOrEmpty(_output.Line))
        {
            _calc.Operation = operation;
            _calc.FirstOperand = _output.Line;
            ClearOutput();
        }else if(!string.IsNullOrEmpty(_calc.FirstOperand) && string.IsNullOrEmpty(_output.Line)){
            _calc.Operation = operation;
        }
        else if(!string.IsNullOrEmpty(_calc.SecondOperand))
        {
            _calc.FirstOperand = _calc.Result();
            _calc.Operation = operation;
            _calc.SecondOperand = string.Empty;
            ClearOutput();
        }else{
            Result(operation);
        }
    }
    public void Result(string? operation = null){ // set argument for series of operations
        if(!string.IsNullOrEmpty(_output.Line) && !string.IsNullOrEmpty(_calc.FirstOperand) && !string.IsNullOrEmpty(_calc.Operation)){
            _calc.SecondOperand = _output.Line;
            _calc.FirstOperand = _calc.Result();
            _calc.Operation = operation ?? string.Empty;
            _calc.SecondOperand = string.Empty;
            ClearOutput();
        }
    }
    public void Negative(){
        if(!string.IsNullOrEmpty(_output.Line)){
            _output.Line = _output.Line[0] != '-' ? "-" + _output.Line : _output.Line[1..];
        }
    }
    public void Digit(string digit){
        _output.Line += digit;
    }
    public void ClearOutput(){
        _output.Line = "";
    }
    public void ClearAll(){
        _output.Line = "";
        _calc.FirstOperand = "";
        _calc.SecondOperand = "";
        _calc.Operation = "";
    }

    public void MainFlow()
    {
        Console.Clear();
        _calc.Log();
        _view.OutputConsole(_output.Line);
        _view.Welcome();
        _view.DisplayStringMatrix( Output.buttons, (x, y));
        ConsoleKeyInfo key;
        while(true){
            key = Console.ReadKey(true);
            switch(key.Key){
                case ConsoleKey.Escape:Console.Clear(); Environment.Exit(0);break;
                case ConsoleKey.LeftArrow or ConsoleKey.A: LeftArrow(); break;
                case ConsoleKey.RightArrow or ConsoleKey.D: RightArrow();break;
                case ConsoleKey.UpArrow or ConsoleKey.W: UpArrow(); break;
                case ConsoleKey.DownArrow or ConsoleKey.S: DownArrow(); break;
                case ConsoleKey.Enter or ConsoleKey.Spacebar: Enter();break;
                case ConsoleKey.Backspace: Backspace();break;
                case ConsoleKey.Add: Calculate(Output.buttons[3, 3]); break;
                case ConsoleKey.Divide: Calculate(Output.buttons[0, 2]); break;
                case ConsoleKey.Subtract: Calculate(Output.buttons[2, 3]); break;
                case ConsoleKey.Multiply: Calculate(Output.buttons[1, 3]); break;
                default: if(char.IsDigit(key.KeyChar)) Digit(key.KeyChar.ToString()); break;
            }
            MainFlow();
        }        
    }
}