using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Calculator {
    public class ProcessEndCommand : IShuntingYardCommand {

        private readonly Dictionary<TokenType, Func<double, double, double>> _operators;

        public ProcessEndCommand( ) {
            _operators = new Dictionary<TokenType, Func<double, double, double>> {
                {TokenType.Add, ( d1, d2 ) => d1 + d2},
                {TokenType.Subtract, ( d1, d2 ) => d2 - d1},
                {TokenType.Multiply, ( d1, d2 ) => d1*d2},
                {TokenType.Divide, ( d1, d2 ) => d2/d1}
            };
        }

        public void Process( IShuntingYard calculator, Token token ) {
            if ( calculator.Operators.Any( o => o.IsParen( ) ) ) {
                throw new InvalidOperationException( Messages.ErrorMismatchedParenthesis );
            }

            while ( calculator.Operators.Count > 0 ) {
                var op = calculator.Operators.Pop( );
                calculator.Output.Enqueue( op );
            }
            var resultStack = new Stack<double>( );
            foreach ( var t in calculator.Output ) {
                if ( t.Type == TokenType.Number ) {
                    var number = ParseNumber( t.TokenValue );
                    resultStack.Push( number );
                } else {
                    if ( !_operators.ContainsKey( t.Type ) ) {
                        throw new InvalidOperationException( Messages.ErrorUnknownOperator + " " + t.Type );
                    }
                    if ( resultStack.Count < 2 ) {
                        throw new InvalidOperationException( Messages.ErrorInvalidExpression );
                    }
                    var result = _operators [ t.Type ]( resultStack.Pop( ), resultStack.Pop( ) );
                    resultStack.Push( result );
                }
            }
            if ( resultStack.Count == 0 ) {
                throw new InvalidOperationException( Messages.ErrorInvalidExpression );
            }
            calculator.Result = resultStack.Pop( );
        }

        private double ParseNumber( string str ) {
            return double.Parse( str, CultureInfo.CurrentCulture );
        }
    }
}