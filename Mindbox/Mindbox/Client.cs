using System;
using System.Diagnostics;

namespace Mindbox {
    public class Client {
        private readonly IFigureFactory _factory;

        public Client( IFigureFactory factory ) {
            if ( factory == null ) {
                throw new ArgumentNullException( "factory" );
            }
            _factory = factory;
        }

        public double Work( ) {
            var triangle = _factory.GetFigure( "right_triangle", 1, 1, 2 );
            return triangle.Square;
        }
    }
}