using System.Linq;

namespace Calculator {
    public static class TokenTypeExt {
        public static bool In( this TokenType tokenType, params TokenType[] targetValues ) {
            return targetValues.Any( t => t == tokenType );
        }
    }
}