using System;

namespace Calculator {
    public class ProcessParentisisCommand : IShuntingYardCommand {
        public void Process( IShuntingYard calculator, Token token ) {
            if ( token.Type == TokenType.Lparen ) {
                calculator.Operators.Push( token );
            }
            else if ( token.Type == TokenType.Rparen ) {
                if ( calculator.Operators.Count == 0 ) {
                    throw new InvalidOperationException( Messages.ErrorMismatchedParenthesis );
                }
                var op = calculator.Operators.Pop( );
                while ( op.Type != TokenType.Lparen ) {
                    calculator.Output.Enqueue( op );
                    if ( calculator.Operators.Count == 0 ) {
                        throw new InvalidOperationException( Messages.ErrorMismatchedParenthesis );
                    }
                    op = calculator.Operators.Pop( );
                }
            }
        }
    }
}