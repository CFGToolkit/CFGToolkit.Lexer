using System;
using System.Text.RegularExpressions;

namespace CFGToolkit.Lexer.Rules
{
    /// <summary>
    /// Base class for token rules in <see cref="LexerGrammar{TLexerState}"/>.
    /// </summary>
    public abstract class LexerRegexBaseRule : ILexerRule
    {
        private Regex _regex;
        private string _regularExpressionPattern;

        /// <summary>
        /// Initializes a new instance of the <see cref="LexerRegexBaseRule"/> class.
        /// </summary>
        /// <param name="ruleName">A name of lexer rule.</param>
        /// <param name="regularExpressionPattern">A regular expression.</param>
        /// <param name="ignoreCase">Case is ignored.</param>
        protected LexerRegexBaseRule(string ruleName, string regularExpressionPattern, bool ignoreCase)
        {
            RuleName = ruleName ?? throw new ArgumentNullException(nameof(ruleName));
            RegularExpressionPattern = regularExpressionPattern ?? throw new ArgumentNullException(nameof(regularExpressionPattern));
            IgnoreCase = ignoreCase;
        }

        /// <summary>
        /// Gets a value indicating whether case of characters in regular expression is ignored.
        /// </summary>
        public bool IgnoreCase { get; }


        public string Prefix { get; set; } = "^";

        /// <summary>
        /// Gets name of lexer rule.
        /// </summary>
        public string RuleName { get; set; }

        /// <summary>
        /// Gets or sets the token type.
        /// </summary>
        public int TokenType { get; set; }

        /// <summary>
        /// Gets or sets a regular expression pattern of lexer rule.
        /// </summary>
        public string RegularExpressionPattern
        {
            get => _regularExpressionPattern;

            set
            {
                _regularExpressionPattern = value;
                _regex = null;
            }
        }

        /// <summary>
        /// Gets a regular expression of lexer rule.
        /// </summary>
        public Regex RegularExpression
        {
            get
            {
                if (_regex == null)
                {
                    RegexOptions options = RegexOptions.None;

                    if (IgnoreCase)
                    {
                        options |= RegexOptions.IgnoreCase;
                    }

                    _regex = new Regex($@"{Prefix}{RegularExpressionPattern}", options);
                }

                return _regex;
            }
        }

        public bool FinalRule { get; set; }

        public abstract LexerRuleReturnDecision CanProduceToken(ILexerState state, string bestMatch);
        
        public abstract bool CanUse(ILexerState state, string value);

        public abstract object Clone();

        public IMatch Match(ILexerState state, string text, int index)
        {
            var tokenMatch = RegularExpression.Match(text, index, text.Length - index);

            if (tokenMatch.Success && tokenMatch.Index == index)
            {
                return new Match { Value = tokenMatch.Value, Success = tokenMatch.Success };
            }
            else
            {
                return new Match { Success = false };
            }
        }
    }
}