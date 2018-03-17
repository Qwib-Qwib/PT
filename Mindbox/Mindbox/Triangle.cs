using System;

namespace Mindbox {
    public sealed class Triangle : IFigure {
        public double SideA { get; private set; }

        public double SideB { get; private set; }

        public double SideC { get; private set; }

        internal static Triangle GetTriangle( double[] parameters ) {
            if ( parameters == null || parameters.Length != 3 ) {
                throw new ArgumentException(
                    "Неверно заданы параметры: у прямоугольного треуогольника должны быть заданы три стороны",
                    "parameters" );
            }
            return new Triangle( parameters [ 0 ], parameters [ 1 ], parameters [ 2 ] );
        }

        public Triangle( double sideA, double sideB, double sideC ) {
            if ( sideA <= 0 || sideB <= 0 || sideC <= 0 ) {
                throw new ArgumentException(
                    "Неверно заданы параметры: стороны треугольника должны быть больше нуля" );
            }
            if ( !TriangleExists( sideA, sideB, sideC ) ) {
                throw new ArgumentException( "Неверно заданы параметры: треугольник с заданными сторонами не существует" );
            }
            SideA = sideA;
            SideB = sideB;
            SideC = sideC;
        }

        private static bool TriangleExists( double sideA, double sideB, double sideC ) {
            if ( sideA + sideB > sideC && sideA + sideC > sideB && sideB + sideC > sideA ) {
                return true;
            }
            return false;
        }

        public double Square {
            get {
                var p = ( SideA + SideB + SideC )/2.0;
                return Math.Sqrt( p*( p - SideA )*( p - SideB )*( p - SideC ) );
            }
        }
    }
}
