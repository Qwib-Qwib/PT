using System.Collections.Generic;
using System.Threading;

namespace ConsoleApplication1
{
    public class SuperQueue<T>
    {
        private readonly Queue<T> _queue;

        private readonly object _syncRoot;

        public SuperQueue( ) {
            _queue = new Queue<T>( );
            _syncRoot = new object( );
        }

        public void Push( T item ) {
            lock ( _syncRoot ) {
                _queue.Enqueue( item );
                Monitor.PulseAll( _syncRoot );
            }
        }

        public bool Pop( out T item ) {
            item = default( T );
            lock ( _syncRoot ) {
                while ( _queue.Count == 0 ) {
                    Monitor.Wait( _syncRoot );
                }
                if ( _queue.Count == 0 ) {
                    return false;
                }
                item = _queue.Dequeue( );
                Monitor.PulseAll( _syncRoot );
            }
            return true;
        }
    }
}