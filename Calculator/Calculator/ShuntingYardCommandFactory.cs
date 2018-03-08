using System;
using System.Collections.Generic;

namespace Calculator {
    public class ShuntingYardCommandFactory : IShuntingYardCommandFactory {

        private static readonly Lazy<ShuntingYardCommandFactory> _instance =
            new Lazy<ShuntingYardCommandFactory>( ( ) => new ShuntingYardCommandFactory( ) );

        public static ShuntingYardCommandFactory Instance {
            get { return _instance.Value; }
        }

        private readonly Dictionary<TokenType, IShuntingYardCommand> _commands;

        private readonly IShuntingYardCommand _emptyCommand;

        private ShuntingYardCommandFactory( ) {
            var operatorCommand = new ProcessOperatorCommand( );
            var parentisisCommand = new ProcessParentisisCommand( );
            _commands = new Dictionary<TokenType, IShuntingYardCommand> {
                {TokenType.Start, new ProcessStartCommand( )},

                {TokenType.Number, new ProcessNumberCommand( )},

                {TokenType.Add, operatorCommand},
                {TokenType.Subtract, operatorCommand},
                {TokenType.Divide, operatorCommand},
                {TokenType.Multiply, operatorCommand},

                {TokenType.Lparen, parentisisCommand},
                {TokenType.Rparen, parentisisCommand},

                {TokenType.End, new ProcessEndCommand( )}
            };
            _emptyCommand = new DoNothingCommand( );
        }

        public IShuntingYardCommand GetCommand( TokenType tokenType ) {
            if ( _commands.ContainsKey( tokenType ) ) {
                return _commands [ tokenType ];
            }
            return _emptyCommand;
        }
    }
}