using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Demo2
    {
        public Demo2( ) {
            _random = new Random( );
            _superQueue = new SuperQueue<int>( );
        }

        public void Run( ) {
            RunProducer( );
            foreach ( var number in Enumerable.Range( 1, ConsumersCount ) ) {
                RunConsumer( number );
            }
            Console.ReadKey( );
        }

        private const int ConsumersCount = 5;

        private const int MilliSecondsConsumerTimeout = 5000;

        private const int ProducerItemsCount = 16;

        private readonly Random _random;

        private readonly SuperQueue<int> _superQueue;

        private void RunProducer( ) {
            Task.Factory.StartNew( ( ) => {
                foreach ( var item in Enumerable.Repeat( 0, ProducerItemsCount ).Select( _ => _random.Next( 100, 1000 ) ) ) {
                    _superQueue.Push( item );
                    Console.WriteLine( "Producer push new item {0}", item );
                }
                Console.WriteLine( "  Producer finished" );
            } );
        }

        private async void RunConsumer( int consumerNumber ) {
            var consumer = Task.Factory.StartNew( ( ) => {
                while ( true ) {
                    var item = _superQueue.Pop( );
                    var milliSecondsHardWork = _random.Next( 100, 500 );
                    Thread.Sleep( milliSecondsHardWork );
                    Console.WriteLine( "Consumer {0} process item {1}", consumerNumber, item );
                }
// ReSharper disable once FunctionNeverReturns
            } );
            await Task.WhenAny( consumer, Task.Delay( MilliSecondsConsumerTimeout ) );
            Console.WriteLine( "  Consumer {0} stop waiting", consumerNumber );
        }
    }
}
