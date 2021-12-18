namespace CFGToolkit.Lexer
{
    public interface IMatch
    {
        string Value { get; set; }

        bool Success { get; set; }
    }
}