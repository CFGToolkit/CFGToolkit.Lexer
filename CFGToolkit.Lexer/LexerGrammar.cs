using System.Collections.Generic;

namespace CFGToolkit.Lexer
{
    /// <summary>
    /// Lexer grammar. It contains a collection of lexer rules.
    /// </summary>
    /// <typeparam name="TLexerState">A type of lexer state.</typeparam>
    public class LexerGrammar<TLexerState> where TLexerState : ILexerState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LexerGrammar{TLexerState}"/> class.
        /// </summary>
        /// <param name="lexerRules">A collection of token lexer rules.</param>
        /// <param name="customRules">A collection of custom lexer rules.</param>
        public LexerGrammar()
        {
        }

        /// <summary>
        /// Gets lexer rules.
        /// </summary>
        public List<ILexerRule> LexerRules { get; set; } = new List<ILexerRule>();
    }
}