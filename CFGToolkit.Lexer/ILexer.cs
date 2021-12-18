using System.Collections.Generic;

namespace CFGToolkit.Lexer
{
    public interface ILexer<TLexerState> where TLexerState : LexerState
    {
        List<Token> GetTokens(string text, TLexerState state);
    }
}