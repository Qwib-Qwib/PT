namespace Calculator {
    public class ProcessNumberCommand : IShuntingYardCommand {

        public void Process( IShuntingYard calculator, Token token ) {
            calculator.Output.Enqueue( token  );
        }
    }
}