using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Calculator {
    public class TokenParser : ITokenParser {
        private readonly OrderedDictionary _tokenPatterns;

        private readonly OrderedDictionary _tokenMatches;

        public TokenParser( ) {
            _tokenMatches = new OrderedDictionary( );
            var s = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            _tokenPatterns = new OrderedDictionary {
                {TokenType.Number, string.Format( @"[0-9]*\{0}?[0-9]+([eE][-+]?[0-9]+)?", s )},
                {TokenType.Lparen, @"\("},
                {TokenType.Rparen, @"\)"},
                {TokenType.Multiply, @"\*"},
                {TokenType.Divide, @"\/"},
                {TokenType.Add, @"\+"},
                {TokenType.Subtract, @"\-"},
            };
        }

        private int _index;

        public IEnumerable<Token> Tokenize( string expression ) {
            _index = 0;
            _tokenMatches.Clear( );
            var cleanedExpr = CleanUp( expression );
            foreach ( DictionaryEntry pair in _tokenPatterns ) {
                var matches = Regex.Matches( cleanedExpr, Convert.ToString( pair.Value ) );
                if ( matches.Count != 0 ) {
                    _tokenMatches.Add( pair.Key, matches );
                }
            }
            yield return Token.New( TokenType.Start, string.Empty );
            var token = NextToken( );
            while ( _index < expression.Length ) {
                yield return token;
                token = NextToken( );
            }
            yield return Token.New( TokenType.End, string.Empty );
        }

        private string CleanUp( string expression ) {
            return Regex.Replace( expression, @"\s", string.Empty );
        }

        private Token NextToken( ) {
            foreach ( DictionaryEntry pair in _tokenMatches ) {
                foreach ( Match match in ( MatchCollection ) pair.Value ) {
                    if ( match.Index == _index ) {
                        _index += match.Length;
                        return Token.New( ( TokenType ) pair.Key, match.Value );
                    }
                    if ( match.Index > _index ) {
                        break;
                    }
                }
            }
            _index += 1;
            return Token.New( TokenType.Undefined, string.Empty );
        }
    }
}