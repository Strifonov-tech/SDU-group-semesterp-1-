namespace ResQFlow;

public class Operator
{
    public string Name { get; set; }
    public string Password { get; set; }

    public Operator(string name, string password)
    {
        Name = name;
        Password = password;
    }

    public override string ToString()
    {
        return $"Operator {{ Name = '{Name}', Password = '[PROTECTED]' }}";
    }
}
