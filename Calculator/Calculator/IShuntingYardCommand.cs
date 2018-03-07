namespace Calculator
{
    public interface IShuntingYardCommand {
        void Process( IShuntingYard calculator, Token token );
    }
}
