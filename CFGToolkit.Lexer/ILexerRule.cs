using CFGToolkit.Lexer.Rules;

namespace CFGToolkit.Lexer
{
    public interface ILexerRule
    {
        string RuleName { get; set; }

        int TokenType { get; set; }

        bool FinalRule { get; set; }

        object Clone();

        IMatch Match(ILexerState state, string text, int index);

        bool CanUse(ILexerState state, string value);

        LexerRuleReturnDecision CanProduceToken(ILexerState state, string bestMatch);
    }
}