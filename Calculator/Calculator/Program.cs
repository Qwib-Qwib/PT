using System;

namespace Calculator {
    internal class Program {
        private static void Main( string[] args ) {
            ICalculator calculator = new Calculator( new TokenParser( ), ShuntingYardCommandFactory.Instance );
            string expression;
            do {
                expression = Console.ReadLine( );
                try {
                    var result = calculator.Evaluate( expression );
                    Console.WriteLine( "= {0}", result );
                }
                catch ( Exception ex ) {
                    Console.WriteLine( "Ошибка: {0}", ex.Message );
                }

            } while ( expression != "stop" );
        }
    }
}
