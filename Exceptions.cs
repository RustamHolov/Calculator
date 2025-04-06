[Serializable]
internal class EnterException:Exception
{
    public EnterException(){}
}
internal class LeftArrowException : Exception
{
    public LeftArrowException(){}
}

internal class RightArrowException : Exception
{
    public RightArrowException()
    {
    }
}

internal class DownArrowException : Exception
{
    public DownArrowException()
    {
    }

}
internal class UpArrowException : Exception
{
    public UpArrowException()
    {
    }
}
internal class BackspaceException : Exception
{
    public BackspaceException()
    {
    }
}
internal class DigitException : Exception
{
    public DigitException()
    {
    }
    public DigitException(string message) : base(message){}
}