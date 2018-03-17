using System;

namespace Mindbox {
    public class RightTriangle : IFigure {
        internal static RightTriangle GetRightTriangle( double[] parameters ) {
            if ( parameters == null || parameters.Length != 3 ) {
                throw new ArgumentException(
                    "Неверно заданы параметры: у прямоугольного треуогольника должны быть заданы три стороны",
                    "parameters" );
            }
            var catetA = parameters [ 0 ];
            var catetB = parameters [ 1 ];
            var hypotenuse = parameters [ 2 ];
            return new RightTriangle( catetA, catetB, hypotenuse );
        }

        public RightTriangle( double catetA, double catetB, double hypotenuse ) {
            if ( catetA <= 0 || catetB <= 0 || hypotenuse <= 0 ) {
                throw new ArgumentException(
                    "Неверно заданы параметры: стороны прямоугольного треугольника должны быть больше нуля" );
            }
            if ( !FitPythagorianTheorem( catetA, catetB, hypotenuse ) ) {
                throw new ArgumentException(
                    "Неверно заданы параметры: стороны прямоугольного треугольника должны удовлетворять теореме Пифагора" );
            }

            CatetA = catetA;
            CatetB = catetB;
            Hypotenuse = hypotenuse;
        }

        private static bool FitPythagorianTheorem( double catetA, double catetB, double hypotenuse ) {
            const double epsilon = 0.00000000001;
            return Math.Abs( hypotenuse*hypotenuse - ( catetA*catetA + catetB*catetB ) ) < epsilon;
        }
        
        public double CatetA { get; private set; }

        public double CatetB { get; private set; }

        public double Hypotenuse { get; private set; }

        public double Square {
            get { return ( CatetA*CatetB )/2.0; }
        }
    }
}