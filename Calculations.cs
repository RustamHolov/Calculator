
using System.Numerics;
using System.Reflection.Metadata;

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

    public string Add() {
        return _type switch{
            "bigInt" => ParseBigInt(out BigInteger b1, out BigInteger b2) ? (b1 + b2).ToString() : "",
            "long" => ParseLong(out long l1, out long l2) ? (l1 + l2).ToString() : "",
            "int" => ParseInt(out int i1, out int i2) ? (i1 + i2).ToString() : "",
            "double" => ParseDouble(out double d1, out double d2) ? (d1 + d2).ToString() : "",
            _ => ""
        };
    }
    public string Minus(){
        return _type switch
        {
            "bigInt" => ParseBigInt(out BigInteger b1, out BigInteger b2) ? (b1 - b2).ToString() : "",
            "long" => ParseLong(out long l1, out long l2) ? (l1 - l2).ToString() : "",
            "int" => ParseInt(out int i1, out int i2) ? (i1 - i2).ToString() : "",
            "double" => ParseDouble(out double d1, out double d2) ? (d1 - d2).ToString() : "",
            _ => ""
        };
    }
    public string Multiply(){
        return _type switch
        {
            "bigInt" => ParseBigInt(out BigInteger b1, out BigInteger b2) ? (b1 * b2).ToString() : "",
            "long" => ParseLong(out long l1, out long l2) ? (l1 * l2).ToString() : "",
            "int" => ParseInt(out int i1, out int i2) ? (i1 * i2).ToString() : "",
            "double" => ParseDouble(out double d1, out double d2) ? (d1 * d2).ToString() : "",
            _ => ""
        };
    }
    public string Divide(){
        if(ParseLong(out long l1, out long l2) && l1%l2 != 0 ||
           ParseInt(out int i1, out int i2) && i1%i2 != 0 ||
           ParseBigInt(out BigInteger b1, out BigInteger b2) && b1 % b2 != 0)
        { // divided completely
            ParseDouble(out double d1, out double d2);
            return (d1/d2).ToString();
        }else{
            return _type switch
            {
                "long" => (l1 / l2).ToString() ,
                "int" => (i1 / i2).ToString() ,
                "bigInt" => (b1 / b2).ToString(),
                _ => ""
            };
        }
    }
    public string Precentage()
    {
        return ParseDouble(out double d1, out double d2) ? (d2 / 100 * d1).ToString() : "";
    }



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
    public bool ParseLong(out long operand1, out long operand2)
    {
        if (long.TryParse(_operand1, out long l1) && long.TryParse(_operand2, out long l2))
        {
            operand1 = l1;
            operand2 = l2;
            return true;
        }
        else
        {
            operand1 = 0;
            operand2 = 0;
            return false;
        }
    }
    public bool ParseBigInt(out BigInteger operand1, out BigInteger operand2)
    {
        if (BigInteger.TryParse(_operand1, out BigInteger b1) && BigInteger.TryParse(_operand2, out BigInteger b2))
        {
            operand1 = b1;
            operand2 = b2;
            return true;
        }
        else
        {
            operand1 = 0;
            operand2 = 0;
            return false;
        }
    }

    // BigInteger
    public void ConfirmTheType(){
        bool _bigInt = _operand1.Length > 17 || _operand2.Length > 17;
        bool _long = _operand1.Length > 9 || _operand2.Length > 9 ;
        bool _double = _operand1.Contains('.') || _operand2.Contains('.');
        _type = "" switch{
            _ when _double => "double",
            _ when _long => "long",
            _ when _bigInt => "bigInt",
            _ => "int"
        };
    }
    public string Result(){
        if(!string.IsNullOrEmpty(_operand1) && !string.IsNullOrEmpty(_operand2)){
            ConfirmTheType();
            return _operation switch
            {
                "+" => Add(),
                "-" => Minus(),
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
    public void Log(){
        Console.WriteLine(FirstOperand);
        Console.WriteLine(Operation);
        Console.WriteLine(SecondOperand);
    }
}