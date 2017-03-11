using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ConsoleApplication1
{
    public class Demo1
    {
        public Demo1( ) {
            _random = new Random( );
        }

        public void Run( ) {
            const int numberCount = 16;
            const int maxNumber = 10;

            var numbers = Enumerable.Repeat( 0, numberCount ).Select( _ => _random.Next( maxNumber ) ).ToArray( );
            PrintNumbers( numbers );

            var x = _random.Next( maxNumber );
            PrintX( x );

            var pairs = GetPairs( numbers, x );
            PrintPairs( pairs );

            Console.ReadKey( );
        }

        internal IEnumerable<Tuple<int, int>> GetPairs( IEnumerable<int> numbers, int x ) {
            Debug.Assert( numbers != null );

            var pairs = new Dictionary<int, int>( );
            foreach ( var n in numbers ) {
                if ( pairs.ContainsKey( n ) ) {
                    yield return new Tuple<int, int>( n, pairs [ n ] );
                }
                else {
                    pairs [ x - n ] = n;
                }
            }
        }

        private readonly Random _random;

        private void PrintNumbers( IEnumerable<int> numbers ) {
            Debug.Assert( numbers != null );

            Console.Write( " Numbers = [ " );
            foreach ( var number in numbers ) {
                Console.Write( "{0} ", number );
            }
            Console.WriteLine( "];" );
        }

        private void PrintX( int x ) {
            Console.WriteLine( " X = {0};", x );
        }

        private void PrintPairs( IEnumerable<Tuple<int, int>> pairs ) {
            Debug.Assert( pairs != null );

            Console.WriteLine( " Pairs = " );
            foreach ( var pair in pairs.Distinct( ) ) {
                Console.WriteLine( @"  [ {0}, {1} ]", pair.Item1, pair.Item2 );
                if ( pair.Item1 != pair.Item2 ) {
                    Console.WriteLine( @"  [ {0}, {1} ]", pair.Item2, pair.Item1 );
                }
            }
        }
    }
}
