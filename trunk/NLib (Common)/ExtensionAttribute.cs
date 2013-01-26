// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)

using System;
using System.Collections.Generic;
using System.Text;

namespace System.Runtime.CompilerServices
{
    /// <summary>
    /// Indicates that a method is an extension method, or that a class or assembly
    /// contains extension methods.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method)]
    sealed class ExtensionAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the System.Runtime.CompilerServices.ExtensionAttribute
        /// class.
        /// </summary>
        public ExtensionAttribute() { }
    }
}
