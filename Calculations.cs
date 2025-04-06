
public class Calculations
{

    private string _operation = string.Empty;
    private string _operand1 = string.Empty;
    private string _operand2 = string.Empty;
    private string _res = string.Empty;
    private bool _isDouble = false;
    public Calculations()
    {
    }
    public string Operation {get => _operation; set => _operation = value ;} 
    public string FirstOperand {get => _operand1; set => _operand1 = value;}
    public string SecondOperand { get => _operand2; set => _operand2 = value; }
    public string Res { get => _res; set => _res = value; }
    public bool IsDouble {get => _isDouble; set => IsDouble = value;}

    public static int Add(int a, int b) => a + b;
    public static double Add(double a, double b)  => a + b;
    public bool ParseDouble(out double operand1, out double operand2){
        if(Double.TryParse(_operand1, out double d1) && Double.TryParse(_operand2, out double d2)){
            operand1 = d1;
            operand2 = d2;
            return true;
        }else{
            operand1 = 0;
            operand2 = 0;
            return false;
        }
    }
    public bool ParseInt(out int operand1, out int operand2)
    {
        if (int.TryParse(_operand1, out int i1) && int.TryParse(_operand2, out int i2))
        {
            operand1 = i1;
            operand2 = i2;
            return true;
        }
        else
        {
            operand1 = 0;
            operand2 = 0;
            return false;
        }
    }

    public string Result(){
        if(!string.IsNullOrEmpty(_operand1) && !string.IsNullOrEmpty(_operand2)){
            //IsDouble = _operand1.Contains('.') || _operand2.Contains('.');
            ParseDouble(out double d1, out double d2);
            ParseInt(out int i1, out int i2);
            return _operation switch
            {
                "+" => IsDouble ? Add(d1, d2).ToString() : Add(i1, i2).ToString(),

                _ => throw new ArgumentException("Fail converting"),
            };
        }
        else{
            throw new ArgumentException("Missing Argument");
        }
    }
    public void Log(){
        Console.WriteLine(FirstOperand);
        Console.WriteLine(Operation);
        Console.WriteLine(SecondOperand);
    }
}