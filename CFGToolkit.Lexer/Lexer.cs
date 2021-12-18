using CFGToolkit.Lexer.LineInfo;
using CFGToolkit.Lexer.Rules;
using System;
using System.Collections.Generic;

namespace CFGToolkit.Lexer
{
    /// <summary>
    /// General lexer. It produces tokens from given text.
    /// </summary>
    /// <typeparam name="TLexerState">Type of lexer state.</typeparam>
    public class Lexer<TLexerState> : ILexer<TLexerState> where TLexerState : LexerState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Lexer{TLexerState}"/> class.
        /// </summary>
        /// <param name="grammar">Lexer grammar.</param>
        public Lexer(LexerGrammar<TLexerState> grammar)
        {
            Grammar = grammar ?? throw new ArgumentNullException(nameof(grammar));
        }

        /// <summary>
        /// Gets lexer grammar.
        /// </summary>
        protected LexerGrammar<TLexerState> Grammar { get; }

        /// <summary>
        /// Gets tokens for grammar.
        /// </summary>
        /// <param name="text">A text for which tokens will be returned.</param>
        /// <param name="state">A state for lexer.</param>
        /// <returns>An enumerable of tokens.</returns>
        public List<Token> GetTokens(string text, TLexerState state)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }
            var tokens = new List<Token>();
            try
            {
                int currentTokenIndex = 0;
                var lineProvider = new LexerLineNumberProvider(text);

                if (state != null)
                {
                    state.LineNumber = 0;
                    state.Text = text;
                }

                while (currentTokenIndex < text.Length)
                {
                    if (state != null)
                    {
                        var currentLineIndex = lineProvider.GetLineForIndex(currentTokenIndex);
                        state.NewLine = state.LineNumber != currentLineIndex;
                        state.LineNumber = currentLineIndex;
                        state.StartColumnIndex = lineProvider.GetColumnForIndex(currentTokenIndex);
                    }

                    if (FindBestRule(text, currentTokenIndex, state, out var tokenType, out var returnToken, out var bestMatch, out var length))
                    {
                        if (returnToken)
                        {
                            if (state != null)
                            {
                                state.ProducedTokenTypes.Add(tokenType);
                            }
                            
                            tokens.Add(new Token(
                                tokenType,
                                bestMatch,
                                state?.LineNumber ?? 0,
                                state?.StartColumnIndex ?? 0));
                        }

                        currentTokenIndex += length;
                    }
                    else
                    {
                        throw new LexerException($"Can't get next token from text `{text.Substring(currentTokenIndex)}`, (line = { state?.LineNumber ?? 0}, column index = {state?.StartColumnIndex ?? 0}");
                    }
                }

                return tokens;
            }
            catch (Exception ex)
            {
                throw new LexerException("Exception during lexing", ex);
            }
        }

        /// <summary>
        /// Finds the best matched <see cref="LexerTokenRule{TLexerState}" /> for remaining text to generate new token.
        /// </summary>
        /// <returns>
        /// True if there is matching <see cref="LexerTokenRule{TLexerState}" />.
        /// </returns>
        private bool FindBestRule(
            string textToLex,
            int startIndex,
            TLexerState state,
            out int tokenType,
            out bool returnToken,
            out string bestMatch,
            out int bestMatchTotalLength)
        {
            returnToken = false;
            bestMatch = null;
            tokenType = 0;
            bestMatchTotalLength = 0;

            foreach (var rule in Grammar.LexerRules)
            {
                var match = rule.Match(state, textToLex, startIndex);

                if (match.Success && match.Value.Length > 0)
                {
                    state.FullMatch = match.Value.Length == textToLex.Length - startIndex;
                    state.MatchStartIndex = startIndex;

                    if (rule.CanUse(state, match.Value))
                    {
                        if (bestMatch == null || match.Value.Length > bestMatch.Length)
                        {
                            bestMatch = match.Value;
                            bestMatchTotalLength = bestMatch.Length;
                            returnToken = rule.CanProduceToken(state, bestMatch) == LexerRuleReturnDecision.ReturnToken;
                            tokenType = rule.TokenType;

                            if (rule.FinalRule)
                            {
                                break;
                            }
                        }
                    }
                }
            }

            return !string.IsNullOrEmpty(bestMatch);
        }
    }
}