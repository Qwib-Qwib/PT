using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mindbox;

namespace Tests
{
    [TestClass]
    public class UnitTest1 {
        private IFigureFactory _factory;

        [TestInitialize]
        public void Init( ) {
            _factory = FigureFactory.Instance;
        }

        private const string RightTriangle = "right_triangle";

        private const string Circle = "circle";

        [TestMethod]
        public void GetTriangle_NameOkParamsOk_ReturnsRightTriangle( ) {
            var figure = _factory.GetFigure( RightTriangle, 2, 3, Math.Sqrt( 13 ) );
            Assert.IsInstanceOfType( figure, typeof ( RightTriangle ) );
        }

        [TestMethod]
        public void TriangleSquare_ValidParams_Random( ) {
            var rand = new Random( );
            for ( int i = 0; i < 100; i++ ) {
                var a = (double)rand.Next( 1, 100 );
                var b = (double)rand.Next( 1, 100 );
                var c = Math.Sqrt( a*a + b*b );
                Assert.AreEqual( ( a*b )/2, new RightTriangle( a, b, c ).Square, 0.00000000001 );
            }
        }

        [TestMethod]
        public void GetTriangle_ValidParamsButRandomOrder_ValidSquare( ) {
            var triangle = _factory.GetFigure( RightTriangle, Math.Sqrt( 13 ), 2, 3 );
            Assert.AreEqual( 3, triangle.Square );
            triangle = _factory.GetFigure( RightTriangle, 2, Math.Sqrt( 13 ), 3 );
            Assert.AreEqual( 3, triangle.Square );
            triangle = _factory.GetFigure( RightTriangle, 3, 2, Math.Sqrt( 13 ) );
            Assert.AreEqual( 3, triangle.Square );
            triangle = _factory.GetFigure( RightTriangle, 3, Math.Sqrt( 13 ), 2 );
            Assert.AreEqual( 3, triangle.Square );
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetTriangle_ParamsEmpty_Throws( ) {
            _factory.GetFigure( RightTriangle, Enumerable.Empty<double>(  ).ToArray(  ) );
        }

        [TestMethod]
        [ExpectedException( typeof ( ArgumentException ) )]
        public void GetTriangle_ParamsNull_Throws( ) {
            _factory.GetFigure( RightTriangle );
        }

        [TestMethod]
        [ExpectedException( typeof ( ArgumentException ) )]
        public void GetTriangle_TooManyParams_Throws( ) {
            _factory.GetFigure( RightTriangle, 1, 2, 3, 4 );
        }

        [TestMethod]
        [ExpectedException( typeof ( ArgumentException ) )]
        public void GetTriangle_TooFewParams_Throws( ) {
            _factory.GetFigure( RightTriangle, 1, 2 );
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetTriangle_InvalidParams_Throws() {
            _factory.GetFigure( RightTriangle, 1, 1, 2 );
        }

        [TestMethod]
        [ExpectedException( typeof ( ArgumentException ) )]
        public void GetTriangle_Zero1Params_Throws( ) {
            _factory.GetFigure( RightTriangle, 0, 1, 2 );
        }

        [TestMethod]
        [ExpectedException( typeof ( ArgumentException ) )]
        public void GetTriangle_Zero2Params_Throws( ) {
            _factory.GetFigure( RightTriangle, 1, 0, 2 );
        }

        [TestMethod]
        [ExpectedException( typeof ( ArgumentException ) )]
        public void GetTriangle_Zero3Params_Throws( ) {
            _factory.GetFigure( RightTriangle, 1, 2, 0 );
        }

        [TestMethod]
        public void GetCircle_NameOkParamsOk_ReturnsCircle( ) {
            var circle = _factory.GetFigure( Circle, 1 );
            Assert.IsInstanceOfType( circle, typeof ( Circle ) );
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetFigure_NameNotOk_ThrowsError( ) {
            _factory.GetFigure( "abc" );
        }

        [TestMethod]
        public void CircleSquare_ValidParamsRandom( ) {
            var rand = new Random( );
            for ( int i = 0; i < 100; i++ ) {
                var r = rand.Next( 1, 100 );
                var c = _factory.GetFigure( Circle, r );
                Assert.AreEqual( Math.PI*r*r, c.Square, 0.00000000001 );
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetCircle_ParamsEmpty_ThrowsError( ) {
            _factory.GetFigure( Circle, Enumerable.Empty<double>( ).ToArray( ) );
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetCircle_ParamsNull_ThrowsError( ) {
            _factory.GetFigure( Circle );
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetCirle_TooManyParams_ThrowsError( ) {
            _factory.GetFigure( Circle, new double[] {1, 2} );
        }

        [TestMethod]
        [ExpectedException( typeof ( ArgumentException ) )]
        public void GetCirle_ZeroParams_ThrowsError( ) {
            _factory.GetFigure( Circle, new double[] {0} );
        }
    }
}
