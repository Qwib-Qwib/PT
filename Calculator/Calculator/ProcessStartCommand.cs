namespace Calculator {
    public class ProcessStartCommand :IShuntingYardCommand {
        public void Process( IShuntingYard calculator, Token token ) {
            calculator.Output.Clear();
            calculator.Operators.Clear();
        }
    }
}