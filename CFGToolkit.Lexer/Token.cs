namespace CFGToolkit.Lexer
{
    /// <summary>
    /// A token produces by <see cref="Lexer{TLexerState}"/>.
    /// </summary>
    public class Token
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Token"/> class.
        /// </summary>
        public Token(int tokenType, string lexem, int lineNumber, int startColumnIndex)
        {
            Type = tokenType;
            Lexem = lexem;
            LineNumber = lineNumber;
            StartColumnIndex = startColumnIndex;
        }

        /// <summary>
        /// Gets token type.
        /// </summary>
        public int Type { get; }

        /// <summary>
        /// Gets or sets token line number.
        /// </summary>
        public int LineNumber { get; set; }

        /// <summary>
        /// Gets or sets start column index.
        /// </summary>
        public int StartColumnIndex { get; set; }

        /// <summary>
        /// Gets end column index.
        /// </summary>
        public int EndColumnIndex => StartColumnIndex + (Lexem?.Length ?? 0);

        /// <summary>
        /// Gets token lexem.
        /// </summary>
        public string Lexem { get; set; }
    }
}