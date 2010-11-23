using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NLib;

namespace NUnitTests.Common
{
    /// <summary>
    /// The purpose of this class is to ensure all of the generic delegates exist. It
    /// does this by referencing them. If any of the generic delegates are missing,
    /// or are defined multiple times a compile-time exception will be generated.
    /// </summary>
    [TestFixture]
    public class GenericDelegatesTests
    {
        [Test]
        public void ActionsExist()
        {
            Type type;
            type = typeof(Action);
            type = typeof(Action<int>);
            type = typeof(Action<int, int>);
            type = typeof(Action<int, int, int>);
            type = typeof(Action<int, int, int, int>);
            type = typeof(Action<int, int, int, int, int>);
            type = typeof(Action<int, int, int, int, int, int>);
            type = typeof(Action<int, int, int, int, int, int, int>);
            type = typeof(Action<int, int, int, int, int, int, int, int>);
            type = typeof(Action<int, int, int, int, int, int, int, int, int>);
            type = typeof(Action<int, int, int, int, int, int, int, int, int, int>);
            type = typeof(Action<int, int, int, int, int, int, int, int, int, int, int>);
            type = typeof(Action<int, int, int, int, int, int, int, int, int, int, int, int>);
            type = typeof(Action<int, int, int, int, int, int, int, int, int, int, int, int, int>);
            type = typeof(Action<int, int, int, int, int, int, int, int, int, int, int, int, int, int>);
            type = typeof(Action<int, int, int, int, int, int, int, int, int, int, int, int, int, int, int>);
            type = typeof(Action<int, int, int, int, int, int, int, int, int, int, int, int, int, int, int, int>);
        }

        [Test]
        public void FuncsExist()
        {
            Type type;
            type = typeof(Func<int>);
            type = typeof(Func<int, int>);
            type = typeof(Func<int, int, int>);
            type = typeof(Func<int, int, int, int>);
            type = typeof(Func<int, int, int, int, int>);
            type = typeof(Func<int, int, int, int, int, int>);
            type = typeof(Func<int, int, int, int, int, int, int>);
            type = typeof(Func<int, int, int, int, int, int, int, int>);
            type = typeof(Func<int, int, int, int, int, int, int, int, int>);
            type = typeof(Func<int, int, int, int, int, int, int, int, int, int>);
            type = typeof(Func<int, int, int, int, int, int, int, int, int, int, int>);
            type = typeof(Func<int, int, int, int, int, int, int, int, int, int, int, int>);
            type = typeof(Func<int, int, int, int, int, int, int, int, int, int, int, int, int>);
            type = typeof(Func<int, int, int, int, int, int, int, int, int, int, int, int, int, int>);
            type = typeof(Func<int, int, int, int, int, int, int, int, int, int, int, int, int, int, int>);
            type = typeof(Func<int, int, int, int, int, int, int, int, int, int, int, int, int, int, int, int>);
            type = typeof(Func<int, int, int, int, int, int, int, int, int, int, int, int, int, int, int, int, int>);
        }
    }
}
