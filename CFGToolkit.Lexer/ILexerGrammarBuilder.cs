namespace CFGToolkit.Lexer
{
    public interface ILexerGrammarBuilder<TLexerState> where TLexerState : ILexerState
    {
        void AddRule(ILexerRule rule);

        void Clear();

        LexerGrammar<TLexerState> GetGrammar();
    }
}