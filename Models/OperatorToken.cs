namespace Marimo.MauiBlazor.Models;

public class OperatorToken(Key @operator) : Token
{
    public Key Operator { get; set; } = @operator;
}
