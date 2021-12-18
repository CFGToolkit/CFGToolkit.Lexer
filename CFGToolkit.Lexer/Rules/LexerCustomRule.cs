using System;
using System.Text.RegularExpressions;

namespace CFGToolkit.Lexer.Rules
{
    public class LexerCustomRule : ILexerRule
    {
        private Func<ILexerState, string, LexerRuleReturnDecision> _returnDecisionProvider;
        private Func<ILexerState, string, LexerRuleConsumeDecision> _useDecisionProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="LexerCustomRule"/> class.
        /// </summary>
        public LexerCustomRule(
            int tokenType,
            string ruleName,
            Func<ILexerState, string, int, IMatch> action,
            Func<ILexerState, string, LexerRuleReturnDecision> returnDecisionProvider = null,
            Func<ILexerState, string, LexerRuleConsumeDecision> useDecisionProvider = null,
            bool finalRule = false)
        {
            TokenType = tokenType;
            RuleName = ruleName ?? throw new ArgumentNullException(nameof(ruleName));
            Action = action ?? throw new ArgumentNullException(nameof(action));
            _returnDecisionProvider = returnDecisionProvider ?? ((state, lexem) => LexerRuleReturnDecision.ReturnToken);
            _useDecisionProvider = useDecisionProvider ?? ((state, lexem) => LexerRuleConsumeDecision.Consume);
            FinalRule = finalRule;
        }

        public int TokenType { get; set; }

        public string RuleName { get; set; }

        public Func<ILexerState, string, int, IMatch> Action { get; }

        public bool FinalRule { get; set; }

        public LexerRuleReturnDecision CanProduceToken(ILexerState state, string value)
        {
            return _returnDecisionProvider(state, value);
        }

        public bool CanUse(ILexerState state, string value)
        {
            return _useDecisionProvider(state, value) == LexerRuleConsumeDecision.Consume;
        }

        public object Clone()
        {
            return new LexerCustomRule(TokenType, RuleName, Action, _returnDecisionProvider, _useDecisionProvider, FinalRule);
        }

        public IMatch Match(ILexerState state, string text, int index)
        {
            return Action(state, text, index);
        }
    }
}