using System;

namespace CFGToolkit.Lexer
{
    /// <summary>
    /// Exception during lexing.
    /// </summary>
    public class LexerException : Exception
    {
        public LexerException(string message)
            : base(message)
        {
        }

        public LexerException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}