namespace Marimo.MauiBlazor.Models;

public class OperatorToken(char @operator) : Token
{
    public Char Operator { get; set; } = @operator;
}
