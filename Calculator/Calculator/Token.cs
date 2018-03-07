namespace Calculator {
    public struct Token {
        public TokenType Type { get; set; }

        public string TokenValue { get; set; }

        public static Token New( TokenType type, string value ) {
            return new Token {Type = type, TokenValue = value};
        }

        public bool IsEmpty( ) {
            return Type == TokenType.Undefined && string.IsNullOrEmpty( TokenValue );
        }

        public bool IsParen( ) {
            return Type.In( TokenType.Lparen, TokenType.Rparen );
        }

        
    }
}