﻿// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Remoting.Messaging;
using System.Reflection;
using System.Diagnostics;

namespace NLib
{
    /// <summary>
    /// Provides a set of methods and properties to simplify thread creation.
    /// </summary>
    public static class SimpleThread
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
        ///     the delegate is called synchronously, and this method returns after the delegate
        ///     returns. This can be useful for debugging when multiple threads interfere
        ///     with the debugging process.
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">
        /// method is null.</exception>
        public static void BeginInvoke(SimpleThreadMethod method)
        {
            if (method == null)
                throw new ArgumentNullException("method");
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

        [DebuggerNonUserCode]
        private static void ThreadCallback(IAsyncResult ar)
        {
            AsyncResult result = (AsyncResult)ar;
            var caller = (SimpleThreadMethod)result.AsyncDelegate;

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
    /// Represents a method that the <see cref="SimpleThread"/> class will call in a new thread.
    /// </summary>
    public delegate void SimpleThreadMethod();
}
