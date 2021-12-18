using System.Collections.Generic;

namespace CFGToolkit.Lexer
{
    public interface ILexerState
    {
        bool FullMatch { get; set; }

        int LineNumber { get; set; }

        bool NewLine { get; set; }

        List<int> ProducedTokenTypes { get; set; }

        int StartColumnIndex { get; set; }
    }
}