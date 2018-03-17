using System;
using System.Collections.Generic;
using System.Linq;

namespace Mindbox {
    public class FigureFactory : IFigureFactory {

        private FigureFactory( ) { }

        private static Lazy<FigureFactory> _instance = new Lazy<FigureFactory>( ( ) => new FigureFactory( ));

        public static FigureFactory Instance {
            get { return _instance.Value; }
        }

        private Dictionary<string, Func<double[], IFigure>> _figures = new Dictionary<string, Func<double[], IFigure>> {
            {"right_triangle", p => RightTriangle.GetRightTriangle( p.OrderBy( _ => _ ).ToArray( ) )},
            {"circle", Circle.GetCircle},
            {"triangle", Triangle.GetTriangle}
        };
        
        public IFigure GetFigure( string figureName, params double[] parameters ) {
            if ( !_figures.ContainsKey( figureName ) ) {
                throw new ArgumentException( string.Format( "Неизвестная фигура: {0}", figureName ) );
            }
            return _figures [ figureName ]( parameters );
        }

    }
}