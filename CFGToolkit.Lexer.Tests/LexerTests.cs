using CFGToolkit.Lexer.Rules;
using System.Collections.Generic;
using Xunit;

namespace CFGToolkit.Lexer.Tests
{
    public class LexerTests
    {
        [Fact]
        public void EmptyGrammarEmptyText()
        {
            LexerGrammar<LexerState> grammar = new LexerGrammar<LexerState>();

            var state = new LexerState();
            var lexer = new Lexer<LexerState>(grammar);

            var tokens = lexer.GetTokens(string.Empty, state);
            Assert.Empty(tokens);
        }

        [Fact]
        public void EmptyGrammarNonEmptyText()
        {
            LexerGrammar<LexerState> grammar = new LexerGrammar<LexerState>();
            Lexer<LexerState> lexer = new Lexer<LexerState>(grammar);
            var state = new LexerState();

            Assert.Throws<LexerException>(() =>lexer.GetTokens("Line1\nLine2\n", state));
        }

        [Fact]
        public void NonEmptyGrammarNonEmptyText()
        {
            LexerGrammar<LexerState> grammar = new LexerGrammar<LexerState>();

            grammar.LexerRules.AddRange(
                new List<ILexerRule>()
                {
                    new LexerTokenRegexRule(1, "Text", "[a-zA-Z0-9]*"),
                    new LexerTokenRegexRule(
                        2,
                        "NewLine",
                        "\n",
                        (state, lexem) =>
                        {
                            state.LineNumber++;
                            return LexerRuleReturnDecision.ReturnToken;
                        }),
                });

            Lexer<LexerState> lexer = new Lexer<LexerState>(grammar);
            var s = new LexerState();
            var tokens = lexer.GetTokens("Line1\nLine2\n", s).ToArray();
            Assert.Equal(4, tokens.Length);

            Assert.Equal(3, s.LineNumber);
        }
    }
}