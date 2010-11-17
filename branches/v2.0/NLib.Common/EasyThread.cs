using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Remoting.Messaging;
using System.Reflection;

namespace NLib
{
    public static class EasyThread
    {
        //--- Public Static Methods ---

        public static void BeginInvoke(EasyThreadDelegate method)
        {
#if DISABLE_THREADING
            method();
#else
            method.BeginInvoke(new AsyncCallback(ThreadCallback), null);
#endif
        }


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

    public delegate void EasyThreadDelegate();
}
