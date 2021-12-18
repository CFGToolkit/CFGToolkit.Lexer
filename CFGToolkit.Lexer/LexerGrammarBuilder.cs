using CFGToolkit.Lexer.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CFGToolkit.Lexer
{
    /// <summary>
    /// Builder for LexerGrammar object from lexer rules either of type LexerTokenRole or LexerInternalRule.
    /// </summary>
    /// <typeparam name="TLexerState">A type of lexer state.</typeparam>
    public class LexerGrammarBuilder<TLexerState> : ILexerGrammarBuilder<TLexerState> where TLexerState : ILexerState
    {
        private readonly List<ILexerRule> _rules = new List<ILexerRule>();

        /// <summary>
        /// Clears the builder.
        /// </summary>
        public void Clear()
        {
            _rules.Clear();
        }

        /// <summary>
        /// Adds a rule to builder.
        /// </summary>
        /// <param name="rule">
        /// A lexer rule.
        /// </param>
        public void AddRule(ILexerRule rule)
        {
            if (rule == null)
            {
                throw new ArgumentNullException(nameof(rule));
            }

            _rules.Add(rule);
        }

        /// <summary>
        /// Gets the generated grammar.
        /// </summary>
        /// <returns>
        /// A new grammar that contains rules that were added.
        /// </returns>
        public LexerGrammar<TLexerState> GetGrammar()
        {
            var tokenRules = new List<ILexerRule>();

            tokenRules.AddRange(_rules.OfType<LexerCustomRule>());

            var internalTokenRules = _rules.OfType<LexerInternalRegexRule>().ToList();

            foreach (var lexerTokenRule in _rules.OfType<LexerTokenRegexRule>())
            {
                var lexerTokenRuleCloned = lexerTokenRule.Clone() as LexerTokenRegexRule;

                if (lexerTokenRuleCloned != null)
                {
                    foreach (var internalTokenRule in internalTokenRules)
                    {
                        lexerTokenRuleCloned.RegularExpressionPattern =
                            lexerTokenRuleCloned.RegularExpressionPattern.Replace(
                                $"<{internalTokenRule.RuleName}>",
                                internalTokenRule.RegularExpressionPattern);
                    }

                    tokenRules.Add(lexerTokenRuleCloned);
                }
            }

            return new LexerGrammar<TLexerState>() { LexerRules = tokenRules };
        }
    }
}