
using System;

namespace ConsoleApplication1
{
    internal class Program
    {
        private static void Main( ) {
            new Demo1( ).Run( );
            Console.WriteLine( );
            Console.WriteLine( new string( '-', 50 ) );
            Console.WriteLine( );
            new Demo2( ).Run( );
        }
    }
}
