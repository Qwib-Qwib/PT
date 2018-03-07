using System.Collections.Generic;

namespace Calculator {
    public interface IShuntingYard {
        Queue<Token> Output { get; }

        Stack<Token> Operators { get; }

        double Result { get; set; }
    }
}