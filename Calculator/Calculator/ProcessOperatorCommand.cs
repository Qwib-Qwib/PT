namespace Calculator {
    public class ProcessOperatorCommand : IShuntingYardCommand {

        public void Process( IShuntingYard calculator, Token token ) {
            while ( calculator.Operators.Count > 0 ) {
                var operatorToken = calculator.Operators.Peek( );
                if ( operatorToken.IsParen( ) ) {
                    break;
                }
                if ( ( GetPrecedence( token ) <= GetPrecedence( operatorToken ) ) ) {
                    calculator.Output.Enqueue( calculator.Operators.Pop( ) );
                }
                else {
                    break;
                }
            }
            calculator.Operators.Push( token );
        }

        private int GetPrecedence( Token token ) {
            switch ( token.Type ) {
                case TokenType.Add:
                case TokenType.Subtract:
                    return 1;
                case TokenType.Multiply:
                case TokenType.Divide:
                case TokenType.Lparen:
                case TokenType.Rparen:
                    return 2;
                default:
                    return 0;
            }
        }
    }
}