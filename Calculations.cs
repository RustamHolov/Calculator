
using System.Numerics;

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
            "bigInt" => ParseBigInt(out BigInteger b1, out BigInteger b2) ? $"{b1 + b2}" : "",
            "long" => ParseLong(out long l1, out long l2) ? $"{l1 + l2}" : "",
            "int" => ParseInt(out int i1, out int i2) ? $"{i1 + i2}" : "",
            "double" => ParseDouble(out double d1, out double d2) ? $"{d1 + d2}" : "",
            _ => ""
        };
    }  

    public string Subtract(){
        return _type switch
        {
            "bigInt" => ParseBigInt(out BigInteger b1, out BigInteger b2) ? $"{b1 - b2}" : "",
            "long" => ParseLong(out long l1, out long l2) ? $"{l1 - l2}" : "",
            "int" => ParseInt(out int i1, out int i2) ? $"{i1 - i2}" : "",
            "double" => ParseDouble(out double d1, out double d2) ? $"{d1 - d2}" : "",
            _ => ""
        };
    }
    public string Multiply(){
        return _type switch
        {
            "bigInt" => ParseBigInt(out BigInteger b1, out BigInteger b2) ? $"{b1 * b2}" : "",
            "long" => ParseLong(out long l1, out long l2) ? $"{l1 * l2}" : "",
            "int" => ParseInt(out int i1, out int i2) ? $"{i1 * i2}" : "",
            "double" => ParseDouble(out double d1, out double d2) ? $"{d1 * d2}" : "",
            _ => ""
        };
    }
    public string Divide(){
        if(ParseLong(out long l1, out long l2) && l1%l2 != 0 ||
           ParseInt(out int i1, out int i2) && i1%i2 != 0 ||
           ParseBigInt(out BigInteger b1, out BigInteger b2) && b1 % b2 != 0)
        { // divided completely
            ParseDouble(out double d1, out double d2);
            return $"{d1/d2}";
        }else{
            return _type switch
            {
                "long" => $"{l1 / l2}" ,
                "int" => $"{i1 / i2}",
                "bigInt" => $"{b1 / b2}",
                _ => ""
            };
        }
    }
    public string Precentage()
    {
        return ParseDouble(out double d1, out double d2) ? (d2 / 100 * d1).ToString() : "";
    }



    public bool ParseDouble(out double operand1, out double operand2){
        bool success1 = double.TryParse(_operand1, out operand1);
        bool success2 = double.TryParse(_operand2,out operand2);
        return success1 && success2;
    }
    public bool ParseInt(out int operand1, out int operand2)
    {
        bool success1 = int.TryParse(_operand1, out operand1);
        bool success2 = int.TryParse(_operand2, out operand2);
        return success1 && success2;
    }
    public bool ParseLong(out long operand1, out long operand2)
    {
        bool success1 = long.TryParse(_operand1, out operand1);
        bool success2 = long.TryParse(_operand2, out operand2);
        return success1 && success2;
    }
    public bool ParseBigInt(out BigInteger operand1, out BigInteger operand2)
    {
        bool success1 = BigInteger.TryParse(_operand1, out operand1);
        bool success2 = BigInteger.TryParse(_operand2, out operand2);
        return success1 && success2;
    }

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