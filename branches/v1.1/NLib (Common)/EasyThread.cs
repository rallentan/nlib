using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Remoting.Messaging;
using System.Reflection;

namespace NLib
{
    /// <summary>
    /// Provides a set of methods and properties to simplify thread creation.
    /// </summary>
    public static class EasyThread
    {
        //--- Public Static Methods ---

        /// <summary>
        ///     Executes the specified delegate asynchronously on a thread
        ///     from the threadpool.
        /// </summary>
        /// <param name="method">
        ///     A delegate to a method that takes no parameters.
        /// </param>
        /// <remarks>
        ///     The delegate is called asynchronously, and this method returns immediately.
        ///     If the DisableThreading property is set to true. No thread is created,
        ///     the delegate is synchronously, and this method returns after the delegate
        ///     returns. This can be useful for debugging when multiple threads interfere
        ///     with the debugging process.
        /// </remarks>
        public static void BeginInvoke(EasyThreadDelegate method)
        {
            if (DisableThreading)
                method();
            else
                method.BeginInvoke(new AsyncCallback(ThreadCallback), null);
        }


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


        //--- Private Static Methods ---

        private static void ThreadCallback(IAsyncResult ar)
        {
            AsyncResult result = (AsyncResult)ar;
            var caller = (EasyThreadDelegate)result.AsyncDelegate;

            try
            {
                caller.EndInvoke(ar);
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception ex)
            {
                throw new TargetInvocationException(ex);
            }
        }
    }

    /// <summary>
    /// Represents a method that the <see cref="EasyThread"/> class will call in a new thread.
    /// </summary>
    public delegate void EasyThreadDelegate();
}
