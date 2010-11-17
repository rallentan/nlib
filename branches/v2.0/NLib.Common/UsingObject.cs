using System;
using System.Collections.Generic;
using System.Text;

namespace NLib
{
    public partial class UsingObject : IDisposable
    {
        //--- Fields ---

        UsingObjectOutOfScopeDelegate _func;


        //--- Constructors ---

        public UsingObject(UsingObjectOutOfScopeDelegate func)
        {
            _func = func;
        }

        ~UsingObject()
        {
            Dispose();
        }


        //--- Public Methods ---

        public void Dispose()
        {
            if (IsDisposed)
                return;
            IsDisposed = true;

            _func();

            GC.SuppressFinalize(this);
        }


        //--- Public Properties ---

        public bool IsDisposed { get; private set; }


        //--- Private Methods ---

        void InvokeFunc() { _func(); }


        //--- IDisposable Interface Methods ---

        void IDisposable.Dispose()
        {
            Dispose();
        }
    }

    public delegate void UsingObjectOutOfScopeDelegate();
}
