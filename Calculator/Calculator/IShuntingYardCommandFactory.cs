namespace Calculator {
    public interface IShuntingYardCommandFactory {
        IShuntingYardCommand GetCommand( TokenType tokenType );
    }
}