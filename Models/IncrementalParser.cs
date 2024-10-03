using Marimo.MauiBlazor.Models.Tokens;

namespace Marimo.MauiBlazor.Models;

public class IncrementalParser
{
    public Token? ActiveToken { get; private set; } = null;

    public void InputCharactor(char charactor)
    {
        switch (charactor)
        {
            case >= '0' and <= '9':
                ActiveToken = new NumberToken() { Number = int.Parse(charactor.ToString()) };
                break;
        }
    }
    
}
