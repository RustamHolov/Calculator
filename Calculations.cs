
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
    public enum OperationKind { Add, Subtract, Multiply, Divide };
    public enum DataType {BigInteger, Long, Int, Double, Decimal};
    public static T Add<T> (T a, T b) where T : INumber<T> => a + b;
    public static T Subtract<T> (T a, T b) where T : INumber<T> => a - b;
    public static T Multiply<T> (T a, T b) where T : INumber<T> => a * b;
    public static T Divide<T> (T a, T b) where T : INumber<T> => a / b;
    
    public string Precentage() => ParseGeneric(out decimal dc1, out decimal dc2) ? $"{dc2 / 100 * dc1:N}" : "fail calculation";
    public bool ParseGeneric<T>(out T o1, out T o2) where T : INumber<T>
    {
        bool success1 = T.TryParse(_operand1, CultureInfo.InvariantCulture, out T? parsed1);
        bool success2 = T.TryParse(_operand2, CultureInfo.InvariantCulture, out T? parsed2);
        if (success1 && success2){
            o1 = parsed1!;
            o2 = parsed2!;
            return true;
        }else{
            o1 = default!;
            o2 = default!;
            return false;
        }
    }
    public string Operate<T>(Func<T, T, T> operation) where T: INumber<T>
    {
        if (ParseGeneric(out T operand1Value, out T operand2Value))
        {
            try
            {
                T result = operation(operand1Value, operand2Value);
                return result.ToString() ?? throw new NotSupportedException("Unsupported data type");
            }
            catch (DivideByZeroException) // 
            {
                return "Error: Division by zero.";
            }
            catch (OverflowException )
            {

                return "Error: Arithmetic overflow during calculation.";
            }
            catch (Exception ex) 
            {
                return $"Error during calculation: {ex.Message}";
            }
        }
        else
        {
            return "Error: Failed to parse one or both operands.";
        }
    }
    
    public string Calculate(OperationKind kind){
        try{
            return ConfirmType() switch{
                DataType.BigInteger => ExecuteOperation<BigInteger>(kind),
                DataType.Long => ExecuteOperation<long>(kind),
                DataType.Int => ExecuteOperation<int>(kind),
                DataType.Double => ExecuteOperation<double>(kind),
                DataType.Decimal => ExecuteOperation<decimal>(kind),
                _ => throw new NotSupportedException($"Data type '{_type}' is not supported.")
            };
        }catch(Exception ex){
            return $"Error: {ex.Message}";
        }
    }
    public string ExecuteOperation<T>(OperationKind kind) where T: INumber<T>{
        Func<T,T,T> opeartion = kind switch{
            OperationKind.Add => Add,
            OperationKind.Subtract => Subtract,
            OperationKind.Multiply => Multiply,
            OperationKind.Divide => Divide,
            _ => throw new ArgumentOutOfRangeException($"Unknown or unsupported operation kind: {kind}")
        };
        return Operate(opeartion);
    }
    
    
    public DataType ConfirmType()
    {
        if (double.TryParse(_operand1, NumberStyles.Any, CultureInfo.InvariantCulture, out _) ||
            double.TryParse(_operand2, NumberStyles.Any, CultureInfo.InvariantCulture, out _))
        {
            return DataType.Double;
        }
        if (decimal.TryParse(_operand1, NumberStyles.Any, CultureInfo.InvariantCulture, out _) ||
            decimal.TryParse(_operand2, NumberStyles.Any, CultureInfo.InvariantCulture, out _))
        {
            return DataType.Decimal;
        }
        if (BigInteger.TryParse(_operand1, out _) || BigInteger.TryParse(_operand2, out _))
        {
            return DataType.BigInteger;
        }
        if (long.TryParse(_operand1, out _) || long.TryParse(_operand2, out _))
        {
            return DataType.Long;
        }
        if (int.TryParse(_operand1, out _) || int.TryParse(_operand2, out _))
        {
            return DataType.Int;
        }

        throw new ArgumentException("Invalid input: Cannot determine data type.");
    }
    public string Result(){
        if(!string.IsNullOrEmpty(_operand1) && !string.IsNullOrEmpty(_operand2)){
            return _operation switch
            {
                "+" => Calculate(OperationKind.Add),
                "-" => Calculate(OperationKind.Subtract),
                "*" => Calculate(OperationKind.Multiply),
                "/" => Calculate(OperationKind.Divide),
                "%" => Precentage(),
                _ => throw new ArgumentException("Fail converting"),
            };
        }
        else{
            throw new ArgumentException("Missing Argument");
        }
    }
    
}