namespace Calculator {
    public class DoNothingCommand : IShuntingYardCommand {
        public void Process( IShuntingYard calculator, Token token ) { }
    }
}