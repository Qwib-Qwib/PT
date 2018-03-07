using System;
using System.Collections.Generic;

namespace Calculator {
    public class Calculator : ICalculator, IShuntingYard {
        Queue<Token> IShuntingYard.Output {
            get { return _outputQueue; }
        }

        private readonly Queue<Token> _outputQueue;

        private readonly Stack<Token> _operatorStack;

        Stack<Token> IShuntingYard.Operators {
            get { return _operatorStack; }
        }

        double IShuntingYard.Result { get; set; }

        private readonly ITokenParser _tokenParser;

        private readonly IProcessTokenCommandFactory _commandFactory;

        public Calculator( ITokenParser tokenParser, IProcessTokenCommandFactory commandFactory) {
            if ( tokenParser == null ) {
                throw new ArgumentNullException( "tokenParser" );
            }
            if ( commandFactory == null ) {
                throw new ArgumentNullException( "commandFactory" );
            }
            _tokenParser = tokenParser;
            _commandFactory = commandFactory;
            _outputQueue = new Queue<Token>( );
            _operatorStack = new Stack<Token>( );
        }

        double ICalculator.Evaluate( string expression ) {
            foreach ( var token in _tokenParser.Tokenize( expression ) ) {
                _commandFactory.GetCommand( token.Type ).Process( this, token );
            }
            return ( ( IShuntingYard ) this ).Result;
        }
    }
}