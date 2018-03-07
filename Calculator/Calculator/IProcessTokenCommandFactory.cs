namespace Calculator {
    public interface IProcessTokenCommandFactory {
        IShuntingYardCommand GetCommand( TokenType tokenType );
    }
}