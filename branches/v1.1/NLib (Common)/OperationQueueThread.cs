using System;
using System.Collections;
using System.Text;
using System.Threading;
using System.Runtime.Remoting.Messaging;

namespace NLib
{
    /// <summary>
    /// Represents a thread which executes operations from a queue.
    /// </summary>
    public partial class OperationQueueThread : IDisposable
    {
        //--- Public Static Properties ---

        /// <summary>
        /// Gets or sets a value indicating whether calls to other methods in this class
        /// should be executed synchronously, and should not create other threads.
        /// </summary>
        /// <remarks>
        /// If this property is set to true, this class will not create other threads, and all
        /// methods will be executed synchronously. This can be useful for debugging when
        /// multiple threads interfere with the debugging process. The default value is false.
        /// </remarks>
        public static bool DisableThreading { get; set; }
        
        
        //--- Fields ---

        Queue _operationQueue = new Queue();
        IAsyncResult _threadAsyncResult = null;
        object _queue_SyncLock = new object();
        volatile int _queueThreadId = -1;


        //--- Constructors / Destructor ---

        /// <summary>
        /// Initializes a new instance of the <see cref="OperationQueueThread"/>.
        /// </summary>
        public OperationQueueThread() { }


        //--- Public Methods ---

        /// <summary>
        /// Removes all pending operations from the operation queue, and returns immediately.
        /// </summary>
        /// <remarks>
        /// This method removes all operations that have not begun processing from the operation
        /// queue, and returns immediately. If an operation has already been dequeued from the
        /// operation queue to be processed, it will not be interrupted. To remove all operations
        /// from the operation queue, and wait for the currently running operation to complete,
        /// use the ClearWait method of this class.
        /// </remarks>
        /// <exception cref="ObjectDisposedException">
        /// The <see cref="OperationQueueThread"/> has been disposed.
        /// </exception>
        public void Clear()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(this.GetType().Name);

            lock (_queue_SyncLock)
                _operationQueue.Clear();
        }

        /// <summary>
        /// Removes all pending operations from the operation queue, and waits for the current
        /// operation to complete before returning.
        /// </summary>
        /// <remarks>
        /// This method removes all operations that have not begun processing from the operation
        /// queue, and waits for the currently running operation to complete before returning.
        /// While this method is waiting for the operation thread to become idle, no new operations
        /// can be enqueued. If EnqueueOperation or EnqueueOperationWait is called while this method
        /// is waiting, it will be blocked until this method returns.
        /// </remarks>
        /// <exception cref="ObjectDisposedException">
        /// The <see cref="OperationQueueThread"/> has been disposed.
        /// </exception>
        public void ClearWait()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(this.GetType().Name);

            ClearWait(Timeout.Infinite);
        }

        /// <summary>
        ///     Removes all pending operations from the operation queue, and waits for the current
        ///     operation to complete before returning. A parameter specifies a timeout.
        /// </summary>
        /// <returns>
        ///     true if the current operation completed before the specified time elapsed; false
        ///     if the current operation did not complete before the specified time elapsed.
        /// </returns>
        /// <remarks>
        ///     This method removes all operations that have not begun processing from the operation
        ///     queue, and waits for the currently running operation to complete before returning, or
        ///     for the specified timeout to lapse, whichever comes first. 
        ///     While this method is waiting for the operation thread to become idle, no new operations
        ///     can be enqueued. If EnqueueOperation or EnqueueOperationWait is called while this method
        ///     is waiting, it will be blocked until this method returns.
        /// </remarks>
        /// <exception cref="ObjectDisposedException">
        /// The <see cref="OperationQueueThread"/> has been disposed.
        /// </exception>
        public bool ClearWait(int millisecondsTimeout)
        {
            if (IsDisposed)
                throw new ObjectDisposedException(this.GetType().Name);

            lock (_queue_SyncLock)
            {
                Clear();
                if (DisableThreading)
                    return true;
                else
                    return Monitor.Wait(_queue_SyncLock, millisecondsTimeout);  // Wait for operation iterator to go to sleep.
            }
        }

        /// <summary>
        /// Clears the operation queue, waits for any current operation to complete,
        /// and releases the resources used by the <see cref="OperationQueueThread"/>.
        /// </summary>
        /// <remarks>
        /// This method clears the operation queue, and waits for any currently running
        /// operations to complete. Then, it releases the resources used by this instance,
        /// and returns. While this method is waiting for the operation thread to become idle,
        /// no new operations can be enqueued. If EnqueueOperation or EnqueueOperationWait
        /// is called while this method is waiting, it will be blocked until this method returns,
        /// and then will throw an <see cref="ObjectDisposedException"/>.
        /// This method will block indefinitely until any currently running operations
        /// complete. If an operation has frozen and does not return, this method will
        /// not return either.
        /// </remarks>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Adds an operation to the operation queue, and returns immediately.
        /// </summary>
        /// <param name="method">
        /// The operation delegate to invoke.
        /// </param>
        /// <remarks>
        /// This method adds the specified delegate to the operation queue,
        /// and returns immediately.
        /// The operation queue is a first-in first-out (FIFO) buffer. The
        /// delegates added to the queue are executed synchronously and
        /// consecutively until the queue is empty. When more operations are
        /// added to the queue, they will be processed like-wise.
        /// </remarks>
        /// <exception cref="ObjectDisposedException">
        /// The <see cref="OperationQueueThread"/> has been disposed.
        /// </exception>
        public void EnqueueOperation(OperationQueueMethod method)
        {
            if (IsDisposed)
                throw new ObjectDisposedException(this.GetType().Name);

            if (DisableThreading)
                method();
            else
            {
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
            }
        }

        /// <summary>
        /// Adds an operation to the operation queue, and waits for the operation
        /// to finish before returning.
        /// </summary>
        /// <param name="method">
        /// The operation delegate to invoke.
        /// </param>
        /// <remarks>
        /// This method adds the specified delegate to the operation queue,
        /// and waits for the operation to be dequeued and complete before
        /// returning. If the operation does not complete, this method will
        /// not return.
        /// The operation queue is a first-in first-out (FIFO) buffer. The
        /// delegates added to the queue are executed synchronously and
        /// consecutively until the queue is empty. When more operations are
        /// added to the queue, they will be processed like-wise.
        /// </remarks>
        /// <exception cref="ObjectDisposedException">
        /// The <see cref="OperationQueueThread"/> has been disposed.
        /// </exception>
        public void EnqueueOperationWait(OperationQueueMethod method)
        {
            if (IsDisposed)
                throw new ObjectDisposedException(this.GetType().Name);

            if (DisableThreading)
                method();
            else
            {
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
            }
        }


        //--- Public Properties ---

        /// <summary>
        /// Gets a value indicating whether the <see cref="OperationQueueThread"/> has been disposed.
        /// </summary>
        public bool IsDisposed { get; private set; }


        //--- Protected Methods ---

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="OperationQueueThread"/>
        /// and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">
        /// true to release both managed and unmanaged resources; false to release only unmanaged resources.
        /// </param>
        /// <remarks>
        /// If disposing is true, this method clears the operation queue, and waits for any currently running
        /// operations to complete. Then, it releases the resources used by this instance,
        /// and returns. While this method is waiting for the operation thread to become idle,
        /// no new operations can be enqueued. If EnqueueOperation or EnqueueOperationWait
        /// is called while this method is waiting, it will be blocked until this method returns,
        /// and then will throw an <see cref="ObjectDisposedException"/>.
        /// This method will block indefinitely until any currently running operations
        /// complete. If an operation has frozen and does not return, this method will
        /// not return either.
        /// </remarks>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_operationQueue != null)
                {
                    lock (_queue_SyncLock)
                    {
                        _operationQueue.Clear();
                        _operationQueue = null;
                        IsDisposed = true;
                        if (!DisableThreading)
                            Monitor.Pulse(_queue_SyncLock);
                    }
                }
            }
        }


        //--- Private Methods ---
        
        private bool TryStartOperationQueueThread()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(this.GetType().Name);
            
            if (_threadAsyncResult != null)
                return false;

            var operationQueueThread = new OperationQueueMethod(OperationIteratorThread);
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
            var caller = (OperationQueueMethod)result.AsyncDelegate;

            caller.EndInvoke(ar);
        }
        

        //--- Nested Types ---

        class Operation
        {
            //--- Public Fields ---

            public OperationQueueMethod Method;


            //--- Constructors ---

            public Operation(OperationQueueMethod method)
            {
                Method = method;
            }
        }
    }

    /// <summary>
    /// Represents a method that the <see cref="OperationQueueThread"/> will call when it has reached the top of the queue.
    /// </summary>
    public delegate void OperationQueueMethod();
}
