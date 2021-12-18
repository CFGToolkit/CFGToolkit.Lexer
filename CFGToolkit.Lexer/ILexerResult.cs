using System.Collections.Generic;

namespace CFGToolkit.Lexer
{
    public interface ILexerResult
    {
        bool IsValid { get; }
        LexerException LexerException { get; set; }
        List<Token> Tokens { get; }
    }
}