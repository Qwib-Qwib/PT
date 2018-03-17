namespace Mindbox {
    public interface IFigureFactory {
        IFigure GetFigure( string figureName, params double[] parameters );
    }
}