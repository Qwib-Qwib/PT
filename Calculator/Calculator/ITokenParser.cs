using System.Collections.Generic;

namespace Calculator {
    public interface ITokenParser {
        IEnumerable<Token> Tokenize( string expression );
    }
}