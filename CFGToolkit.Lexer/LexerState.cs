using System.Collections.Generic;

namespace CFGToolkit.Lexer
{
    /// <summary>
    /// A base class for lexer state classes. It contains a type of previous token.
    /// </summary>
    public class LexerState : ILexerState
    {
        /// <summary>
        /// Gets or sets a value indicating whether lexem is a full match.
        /// </summary>
        public bool FullMatch { get; set; }

        /// <summary>
        /// Gets or sets the current line number.
        /// </summary>
        public int LineNumber { get; set; } = 0;

        /// <summary>
        /// Gets or sets the start column index.
        /// </summary>
        public int StartColumnIndex { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the lexer is lexing new line.
        /// </summary>
        public bool NewLine { get; set; } = true;

        /// <summary>
        /// Gets or sets the list of types of produced tokens.
        /// </summary>
        public List<int> ProducedTokenTypes { get; set; } = new List<int>();

        public int MatchStartIndex { get; set; }

        public string Text { get; set; }
    }
}