using System;
using System.Collections;
using System.Text;
using System.Threading;
using System.Runtime.Remoting.Messaging;

namespace NLib
{
    public partial class OperationQueueThread : IDisposable
    {
        //--- Fields ---

        Queue _operationQueue = new Queue();
        IAsyncResult _threadAsyncResult = null;
        object _queue_SyncLock = new object();
        volatile int _queueThreadId = -1;
        object _operationCompleted_SyncLock = new object();


        //--- Constructors ---

        public OperationQueueThread() { }

        ~OperationQueueThread() { Dispose(); }


        //--- Public Methods ---

        public void Clear()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(this.GetType().Name);

            lock (_queue_SyncLock)
                _operationQueue.Clear();
        }
        
        public void ClearWait()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(this.GetType().Name);

            ClearWait(Timeout.Infinite);
        }
        
        public bool ClearWait(int millisecondsTimeout)
        {
            if (IsDisposed)
                throw new ObjectDisposedException(this.GetType().Name);

            lock (_queue_SyncLock)
            {
                Clear();
#if DISABLE_THREADING
                    return true;
#else
                return Monitor.Wait(_queue_SyncLock, millisecondsTimeout);  // Wait for operation iterator to go to sleep.
#endif
            }
        }

        public void Dispose()
        {
            if (IsDisposed)
                return;

            lock (_queue_SyncLock)
            {
                _operationQueue.Clear();
                _operationQueue = null;
                IsDisposed = true;
#if !DISABLE_THREADING
                Monitor.Pulse(_queue_SyncLock);
#endif
            }

            GC.SuppressFinalize(this);
        }

        public void EnqueueOperation(OperationQueueDelegate method)
        {
            if (IsDisposed)
                throw new ObjectDisposedException(this.GetType().Name);

#if DISABLE_THREADING
            method();
#else
            if (Thread.CurrentThread.ManagedThreadId == _queueThreadId)
                throw new InvalidOperationException("Attempted to enqueue an operation from within the operation queue thread. This can cause a deadlock.");
            
            Operation operation;

            lock (_queue_SyncLock)
            {
                TryStartOperationQueueThread();

                operation = new Operation(method);
                _operationQueue.Enqueue(operation);
                Monitor.Pulse(_queue_SyncLock);  // Wake up operation iterator.
            }
#endif
        }

        public void EnqueueOperationWait(OperationQueueDelegate method)
        {
            if (IsDisposed)
                throw new ObjectDisposedException(this.GetType().Name);

#if DISABLE_THREADING
                method();
#else
            if (Thread.CurrentThread.ManagedThreadId == _queueThreadId)
                throw new InvalidOperationException("Attempted to enqueue an operation from within the operation queue thread. This can cause a deadlock.");
            
            Operation operation = new Operation(method);
            lock (operation)
            {
                lock (_queue_SyncLock)
                {
                    TryStartOperationQueueThread();

                    _operationQueue.Enqueue(operation);
                    Monitor.Pulse(_queue_SyncLock);  // Wake up operation iterator
                }

                Monitor.Wait(operation);  // Wait for operation to complete
            }

#endif
        }


        //--- Public Properties ---

        public bool IsDisposed { get; private set; }


        //--- Private Methods ---
        
        private bool TryStartOperationQueueThread()
        {
            if (_threadAsyncResult != null)
                return false;

            var operationQueueThread = new OperationQueueDelegate(OperationIteratorThread);
            AsyncCallback operationQueueThreadCompleted = new AsyncCallback(OperationIteratorThreadCompleted);

            _threadAsyncResult = operationQueueThread.BeginInvoke(operationQueueThreadCompleted, null);
            return true;
        }
        
        private void OperationIteratorThread()
        {
            _queueThreadId = Thread.CurrentThread.ManagedThreadId;
            Thread.MemoryBarrier();

            while (true)
            {
                Operation operation;

                lock (_queue_SyncLock)
                {
                    while (true)
                    {
                        if (IsDisposed)
                            return;

                        if (_operationQueue.Count != 0)
                            break;

                        Monitor.PulseAll(_queue_SyncLock);  // Report that operation iterator is sleeping.
                        Monitor.Wait(_queue_SyncLock);  // Wait for more operations.
                    }

                    operation = (Operation)_operationQueue.Dequeue();
                }

                try
                {
                    operation.Method();
                }
                catch (OperationCanceledException)
                {
                }

                lock (operation)
                {
                    Monitor.Pulse(operation);
                }
            }
        }
        
        private void OperationIteratorThreadCompleted(IAsyncResult ar)
        {
            AsyncResult result = (AsyncResult)ar;
            var caller = (OperationQueueDelegate)result.AsyncDelegate;

            caller.EndInvoke(ar);
        }


        //--- IDisposable Interface Methods ---

        void IDisposable.Dispose()
        {
            Dispose();
        }
        

        //--- Nested Types ---

        class Operation
        {
            //--- Public Fields ---

            public OperationQueueDelegate Method;


            //--- Constructors ---

            public Operation(OperationQueueDelegate method)
            {
                Method = method;
            }
        }
    }

    public delegate void OperationQueueDelegate();
}
