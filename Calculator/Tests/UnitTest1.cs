using System;
using Calculator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests {
    [TestClass]
    public class UnitTest1 {
        private ICalculator _calculator;

        [TestInitialize]
        public void Init( ) {
            _calculator = new Calculator.Calculator( new TokenParser( ), ShuntingYardCommandFactory.Instance );
        }

        [TestMethod]
        public void TestMethod1( ) { }

        [TestMethod]
        public void EasyTests( ) {
            Assert.AreEqual( true, Close( Calculate( "3 + 5" ), 8 ) );
            Assert.AreEqual( true, Close( Calculate( "5 + 41" ), 46 ) );
            Assert.AreEqual( true, Close( Calculate( "5 - 3" ), 2 ) );
            Assert.AreEqual( true, Close( Calculate( "5 - 5" ), 0 ) );
            Assert.AreEqual( true, Close( Calculate( "3 * 5" ), 15 ) );
            Assert.AreEqual( true, Close( Calculate( "2 * 23" ), 46 ) );
            Assert.AreEqual( true, Close( Calculate( "123 / 3" ), 41 ) );
            Assert.AreEqual( true, Close( Calculate( "22 / 1" ), 22 ) );
        }

        [TestMethod]
        public void MediumTests( ) {
            Assert.AreEqual( true, Close( Calculate( "3 + 5 * 2" ), 13 ) );
            Assert.AreEqual( true, Close( Calculate( "5 - 3 * 8 / 8" ), 2 ) );
            Assert.AreEqual( true, Close( Calculate( "6*(2 + 3)" ), 30 ) );
            Assert.AreEqual( true, Close( Calculate( "23,2- 15,2" ), 8 ) );
            Assert.AreEqual( true, Close( Calculate( "22 / 5" ), 22.0/5 ) );
        }

        [TestMethod]
        public void HardTests( ) {
            Assert.AreEqual( true, Close( Calculate( "3 * (4 +       (2 / 3) * 6 - 5)" ), 9 ) );
            Assert.AreEqual( true, Close( Calculate( "23 * 2 + 45 - 24 / 5" ), 86.2 ) );
        }

        [TestMethod]
        public void RandomTest( ) {
            string[] f = new string[] {"a * b   -c", "a + b/   (b +  c)", "(a   + c+  b)  * b  *a"};
            Random rnd = new Random( );
            for ( int i = 1; i < 51; i++ ) {
                int r = rnd.Next( 0, 3 ), a = rnd.Next( 0, 100 ), b = rnd.Next( 0, 100 ), c = rnd.Next( 0, 100 );
                string t = f [ r ].Replace( "a", a + "" ).Replace( "b", b + "" ).Replace( "c", c + "" );
                double s1 = Calculate( t ), s2 = Calculate2( t );
                Console.WriteLine( i + ". Tested for: " + t + ", Expected: " + s2 + ", Got: " + s1 );
                Assert.AreEqual( true, Close( s1, s2 ) );
            }
        }


        // https://www.codewars.com/users/DerekJedral   
        static int[] priority = new int[11] { 1, 2, 0, -1, -1, 1, 0, 0, 0, 0, 1 };
        static double[] numStack = new double[256];
        static char[] buffer = new char[256];

        public static double Calculate2( string input ) {
            input = input.Replace( " ", "" ).Replace( "^", "&" ) + ")";
            string number = "";
            char ch, chBefore = '(';
            int prio, numCount = 0, opCount = 1;
            buffer [ 0 ] = '(';
            buffer [ 1 ] = '(';
            for ( int i = 0; i < input.Length; i++ ) {
                if ( ( ( ch = input [ i ] ) >= '0' ) || ( ch == '.' ) ) number += ch;
                else {
                    if ( chBefore < '0' ) if ( chBefore != ')' ) if ( ch == '-' ) ch = '%';
                    if ( number.Length > 0 ) {
                        numStack [ ++numCount ] = Convert.ToDouble( number );
                        number = "";
                    }
                    if ( ch > '(' ) {
                        prio = priority [ ch - 37 ];
                        while ( opCount > 0 && prio <= priority [ buffer [ opCount ] - 37 ] ) {
                            --numCount;
                            switch ( buffer [ opCount ] ) {
                                case '+':
                                    numStack [ numCount ] = numStack [ numCount ] + numStack [ numCount + 1 ];
                                    break;
                                case '-':
                                    numStack [ numCount ] = numStack [ numCount ] - numStack [ numCount + 1 ];
                                    break;
                                case '*':
                                    numStack [ numCount ] = numStack [ numCount ]*numStack [ numCount + 1 ];
                                    break;
                                case '/':
                                    numStack [ numCount ] = numStack [ numCount ]/numStack [ numCount + 1 ];
                                    break;
                                case '&':
                                    numStack [ numCount ] = Math.Pow( numStack [ numCount ], numStack [ numCount + 1 ] );
                                    break;
                                case '%':
                                    numCount++;
                                    numStack [ numCount ] = -numStack [ numCount ];
                                    break;
                                case '(':
                                    numCount++;
                                    prio = 5;
                                    break;
                            }
                            opCount--;
                        }
                    }
                    if ( ch != ')' ) buffer [ ++opCount ] = ch;
                }
                chBefore = ch;
            }
            return numStack [ 1 ];
        }

        public bool Close( double a, double b ) {
            if ( Math.Abs( a - b ) < 0.000000001 ) return true;
            return false;
        }

        public double Calculate( string expression ) {
            return _calculator.Evaluate( expression );
        }
    }
}
