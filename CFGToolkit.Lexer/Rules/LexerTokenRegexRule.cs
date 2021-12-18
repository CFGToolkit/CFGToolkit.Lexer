using System;

namespace CFGToolkit.Lexer.Rules
{
    /// <summary>
    /// The lexer token rule class. It defines how and when a token will be generated for given regular expression pattern.
    /// </summary>
    /// <typeparam name="TLexerState">Type of lexer state.</typeparam>
    public class LexerTokenRegexRule : LexerRegexBaseRule
    {
        private Func<ILexerState, string, LexerRuleReturnDecision> _returnDecisionProvider;
        private Func<ILexerState, string, LexerRuleConsumeDecision> _useDecisionProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="LexerTokenRule{TLexerState}"/> class.
        /// </summary>
        /// <param name="tokenType">Token type.</param>
        /// <param name="ruleName">Rule name.</param>
        /// <param name="regularExpressionPattern">A token rule pattern.</param>
        /// <param name="returnDecisionProvider">A token rule return decision provider.</param>
        /// <param name="useDecisionProvider">A token rule use decision provider.</param>
        /// <param name="ignoreCase">Ignore case.</param>
        /// <param name="topRule">Is most important rule.</param>
        public LexerTokenRegexRule(
            int tokenType,
            string ruleName,
            string regularExpressionPattern,
            Func<ILexerState, string, LexerRuleReturnDecision> returnDecisionProvider = null,
            Func<ILexerState, string, LexerRuleConsumeDecision> useDecisionProvider = null,
            bool ignoreCase = true,
            bool finalRule = false,
            string lookBehind = null)
            : base(ruleName, regularExpressionPattern, ignoreCase)
        {
            TokenType = tokenType;
            _returnDecisionProvider = returnDecisionProvider ?? ((state, lexem) => LexerRuleReturnDecision.ReturnToken);
            _useDecisionProvider = useDecisionProvider ?? ((state, lexem) => LexerRuleConsumeDecision.Consume);
            FinalRule = finalRule;
        }

        /// <summary>
        /// Returns true if the rule is active or should be skipped.
        /// </summary>
        /// <param name="lexerState">The current lexer state.</param>
        /// <param name="value">A lexem value.</param>
        /// <returns>
        /// True if the lexer token rule is active or should be skipped.
        /// </returns>
        public override bool CanUse(ILexerState lexerState, string value)
        {
            return _useDecisionProvider(lexerState, value) == LexerRuleConsumeDecision.Consume;
        }

        /// <summary>
        /// Clones the rule.
        /// </summary>
        /// <returns>
        /// A clone of rule.
        /// </returns>
        public override object Clone()
        {
            return new LexerTokenRegexRule(
                TokenType,
                RuleName,
                RegularExpressionPattern,
                _returnDecisionProvider,
                _useDecisionProvider,
                IgnoreCase,
                FinalRule);
        }

        public override LexerRuleReturnDecision CanProduceToken(ILexerState state, string value)
        {
            return _returnDecisionProvider(state, value);
        }
    }
}