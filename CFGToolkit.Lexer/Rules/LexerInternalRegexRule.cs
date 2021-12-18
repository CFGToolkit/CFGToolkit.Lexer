namespace CFGToolkit.Lexer.Rules
{
    /// <summary>
    /// Internal rule of lexer. It is use for creating common regular expression to use with LexerTokeRules.
    /// </summary>
    public class LexerInternalRegexRule : LexerRegexBaseRule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LexerInternalRegexRule"/> class.
        /// </summary>
        /// <param name="ruleName">Name of the rule.</param>
        /// <param name="regularExpression">Regular expression.</param>
        public LexerInternalRegexRule(string ruleName, string regularExpression)
            : base(ruleName, regularExpression, false)
        {
        }

        public override LexerRuleReturnDecision CanProduceToken(ILexerState state, string bestMatch)
        {
            return LexerRuleReturnDecision.ReturnToken;
        }

        public override bool CanUse(ILexerState state, string value)
        {
            return true;
        }

        /// <summary>
        /// Clones the current instance.
        /// </summary>
        /// <returns>
        /// A new instance of <see cref="LexerInternalRegexRule"/>.
        /// </returns>
        public override object Clone()
        {
            return new LexerInternalRegexRule(RuleName, RegularExpressionPattern);
        }
    }
}