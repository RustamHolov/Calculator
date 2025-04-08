
using System.Numerics;
using System.Globalization;

public class Calculations
{
    private string _operation = string.Empty;
    private string _operand1 = string.Empty;
    private string _operand2 = string.Empty;
    private string _res = string.Empty;
    private string _type = string.Empty;
    public Calculations()
    {
    }
    public string Operation {get => _operation; set => _operation = value ;} 
    public string FirstOperand {get => _operand1; set => _operand1 = value;}
    public string SecondOperand { get => _operand2; set => _operand2 = value; }
    public string Res { get => _res; set => _res = value; }
    public string Type {get => _type; set => _type = value;}

    public static T Add<T> (T a, T b) where T : INumber<T> => a + b;
    public static T Subtract<T> (T a, T b) where T : INumber<T> => a - b;
    public static T Multiply<T> (T a, T b) where T : INumber<T> => a * b;
    public static T Divide<T> (T a, T b) where T : INumber<T> => a / b;
    public static T Precentage<T> (T a, T b) where T : INumber<T> => b % a;
    public bool ParseGeneric<T>(out T? o1, out T? o2) where T : INumber<T>
    {
        bool success1 = T.TryParse(_operand1, CultureInfo.InvariantCulture, out o1);
        bool success2 = T.TryParse(_operand2, CultureInfo.InvariantCulture, out o2);
        return success1 && success2;
    }
    public delegate bool ParseDelegate<T> (out T o1, out T o2);
    public string Operate<T>(Func<T?,T?,T> operation) where T: INumber<T>{
         return  ParseGeneric<T>(out T? o1, out T? o2) ? $"{operation(o1, o2)}" : "fail calculation";
    }
    public string Add() {
        return _type switch{
            "bigInt" => Operate<BigInteger>(Add),
            "long" => Operate<long>(Add),
            "int" => Operate<int>(Add),
            "double" => Operate<double>(Add),
            _ => ""
        };
    }

    public string Subtract(){
        return _type switch
        {
            "bigInt" => Operate<BigInteger>(Subtract),
            "long" => Operate<long>(Subtract),
            "int" => Operate<int>(Subtract),
            "double" => Operate<double>(Subtract),
            _ => ""
        };
    }
    public string Multiply(){
        return _type switch
        {
            "bigInt" => Operate<BigInteger>(Multiply),
            "long" => Operate<long>(Multiply),
            "int" => Operate<int>(Multiply),
            "double" => Operate<double>(Multiply),
            _ => ""
        };
    }
    public string Divide() => Operate<decimal>(Divide);
    public string Precentage() => ParseGeneric(out double dc1, out double dc2) ? $"{dc2/100 * dc1 :N}" : "fail calculation";


    
    // BigInteger
    public void ConfirmTheType(){
        bool _bigInt = _operand1.Length > 17 || _operand2.Length > 17;
        bool _long = _operand1.Length > 9 || _operand2.Length > 9 ;
        bool _double = _operand1.Contains('.') || _operand2.Contains('.');
        _type = "" switch{
            _ when _double => "double",
            _ when _bigInt => "bigInt",
            _ when _long => "long",
            _ => "int"
        };
    }
    public string Result(){
        if(!string.IsNullOrEmpty(_operand1) && !string.IsNullOrEmpty(_operand2)){
            ConfirmTheType();
            return _operation switch
            {
                "+" => Add(),
                "-" => Subtract(),
                "*" => Multiply(),
                "/" => Divide(),
                "%" => Precentage(),
                _ => throw new ArgumentException("Fail converting"),
            };
        }
        else{
            throw new ArgumentException("Missing Argument");
        }
    }
    
}