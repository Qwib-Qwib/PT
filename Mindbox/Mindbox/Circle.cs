using System;

namespace Mindbox {
    public sealed class Circle : IFigure {

        public double Radius { get; private set; }

        internal static Circle GetCircle( double[] parameters ) {
            if ( parameters == null || parameters.Length != 1 ) {
                throw new ArgumentException( "������� ������ ���������: � ����� ������ ���� ����� ������", "parameters" );
            }
            return new Circle( parameters [ 0 ] );
        }

        public Circle( double radius ) {
            if ( radius <= 0 ) {
                throw new ArgumentException( "������� ������ ���������: ������ ����� ������ ���� ������ ����" );
            }
            Radius = radius;
        }

        public double Square {
            get { return Math.PI*( Radius*Radius ); }
        }
    }
}