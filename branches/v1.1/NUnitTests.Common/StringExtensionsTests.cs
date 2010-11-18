// Todo:
//  Create tests for overflows
//  Create tests for exact error messages
//  Create a random input test
//  Write tests to readably follow every pathway
//  Create tests for empty strings
//  Improve tests for the CompareTo, Contains, and Replace methods
//  Test invalid values of StringComparison
//
using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NLib;
using System.Reflection;

namespace NUnitTests.Common
{
    [TestFixture]
    public class StringExtensionsTests
    {
        [Test]
        public void CompareTo_P1_DiffCaseThan_P2()
        {
            Assert.AreNotEqual(StringExtensions.CompareTo("aaaa", "AAAA", StringComparison.Ordinal), 0);
            Assert.AreNotEqual(StringExtensions.CompareTo("aaaa", "AAAA", StringComparison.CurrentCulture), 0);
            Assert.AreNotEqual(StringExtensions.CompareTo("aaaa", "AAAA", StringComparison.InvariantCulture), 0);

            Assert.AreEqual(StringExtensions.CompareTo("aaaa", "AAAA", StringComparison.OrdinalIgnoreCase), 0);
            Assert.AreEqual(StringExtensions.CompareTo("aaaa", "AAAA", StringComparison.InvariantCultureIgnoreCase), 0);
            Assert.AreEqual(StringExtensions.CompareTo("aaaa", "AAAA", StringComparison.CurrentCultureIgnoreCase), 0);
        }

        [Test]
        public void CompareTo_P1_Equals_P2()
        {
            Assert.AreEqual(StringExtensions.CompareTo("aaaa", "aaaa", StringComparison.Ordinal), 0);
            Assert.AreEqual(StringExtensions.CompareTo("aaaa", "aaaa", StringComparison.CurrentCulture), 0);
            Assert.AreEqual(StringExtensions.CompareTo("aaaa", "aaaa", StringComparison.InvariantCulture), 0);

            Assert.AreEqual(StringExtensions.CompareTo("aaaa", "aaaa", StringComparison.OrdinalIgnoreCase), 0);
            Assert.AreEqual(StringExtensions.CompareTo("aaaa", "aaaa", StringComparison.CurrentCultureIgnoreCase), 0);
            Assert.AreEqual(StringExtensions.CompareTo("aaaa", "aaaa", StringComparison.InvariantCultureIgnoreCase), 0);
        }

        [Test]
        public void CompareTo_P1_GreaterThan_P2()
        {
            Assert.Greater(StringExtensions.CompareTo("bbbb", "aaaa", StringComparison.Ordinal), 0);
            Assert.Greater(StringExtensions.CompareTo("bbbb", "aaaa", StringComparison.CurrentCulture), 0);
            Assert.Greater(StringExtensions.CompareTo("bbbb", "aaaa", StringComparison.InvariantCulture), 0);

            Assert.Greater(StringExtensions.CompareTo("bbbb", "aaaa", StringComparison.OrdinalIgnoreCase), 0);
            Assert.Greater(StringExtensions.CompareTo("bbbb", "aaaa", StringComparison.CurrentCultureIgnoreCase), 0);
            Assert.Greater(StringExtensions.CompareTo("bbbb", "aaaa", StringComparison.InvariantCultureIgnoreCase), 0);
        }

        [Test]
        public void CompareTo_P1_IsEmptyString()
        {
            Assert.Less(StringExtensions.CompareTo(string.Empty, "aaaa", StringComparison.Ordinal), 0);
        }

        [Test]
        public void CompareTo_P1_IsEmptyString_P2_IsEmptyString()
        {
            Assert.AreEqual(StringExtensions.CompareTo(string.Empty, string.Empty, StringComparison.Ordinal), 0);
        }

        [Test]
        public void CompareTo_P1_IsNull()
        {
            Assert.Less(StringExtensions.CompareTo(null, "aaaa", StringComparison.Ordinal), 0);
        }

        [Test]
        public void CompareTo_P1_LessThan_P2()
        {
            Assert.Less(StringExtensions.CompareTo("aaaa", "bbbb", StringComparison.Ordinal), 0);
            Assert.Less(StringExtensions.CompareTo("aaaa", "bbbb", StringComparison.CurrentCulture), 0);
            Assert.Less(StringExtensions.CompareTo("aaaa", "bbbb", StringComparison.InvariantCulture), 0);

            Assert.Less(StringExtensions.CompareTo("aaaa", "bbbb", StringComparison.OrdinalIgnoreCase), 0);
            Assert.Less(StringExtensions.CompareTo("aaaa", "bbbb", StringComparison.CurrentCultureIgnoreCase), 0);
            Assert.Less(StringExtensions.CompareTo("aaaa", "bbbb", StringComparison.InvariantCultureIgnoreCase), 0);
        }

        [Test]
        public void CompareTo_P2_IsEmptyString()
        {
            Assert.Greater(StringExtensions.CompareTo("aaaa", string.Empty, StringComparison.Ordinal), 0);
        }

        [Test]
        public void CompareTo_P2_IsNull()
        {
            Assert.Greater(StringExtensions.CompareTo("aaaa", null, StringComparison.Ordinal), 0);
        }

        [Test]
        public void Contains_P1_Contains_P2()
        {
            Assert.IsTrue(StringExtensions.Contains("aaaabbbb", "aabb", StringComparison.Ordinal));
            Assert.IsTrue(StringExtensions.Contains("aaaabbbb", "aabb", StringComparison.CurrentCulture));
            Assert.IsTrue(StringExtensions.Contains("aaaabbbb", "aabb", StringComparison.InvariantCulture));

            Assert.IsTrue(StringExtensions.Contains("aaaabbbb", "aabb", StringComparison.OrdinalIgnoreCase));
            Assert.IsTrue(StringExtensions.Contains("aaaabbbb", "aabb", StringComparison.CurrentCultureIgnoreCase));
            Assert.IsTrue(StringExtensions.Contains("aaaabbbb", "aabb", StringComparison.InvariantCultureIgnoreCase));
        }

        [Test]
        public void Contains_P1_Contains_P2_WithDiffCase()
        {
            Assert.IsFalse(StringExtensions.Contains("aaaabbbb", "AABB", StringComparison.Ordinal));
            Assert.IsFalse(StringExtensions.Contains("aaaabbbb", "AABB", StringComparison.CurrentCulture));
            Assert.IsFalse(StringExtensions.Contains("aaaabbbb", "AABB", StringComparison.InvariantCulture));

            Assert.IsTrue(StringExtensions.Contains("aaaabbbb", "AABB", StringComparison.OrdinalIgnoreCase));
            Assert.IsTrue(StringExtensions.Contains("aaaabbbb", "AABB", StringComparison.CurrentCultureIgnoreCase));
            Assert.IsTrue(StringExtensions.Contains("aaaabbbb", "AABB", StringComparison.InvariantCultureIgnoreCase));
        }

        [Test]
        public void Contains_P1_DoesNotContain_P2()
        {
            Assert.IsFalse(StringExtensions.Contains("aaaabbbb", "cccc", StringComparison.Ordinal));
            Assert.IsFalse(StringExtensions.Contains("aaaabbbb", "cccc", StringComparison.CurrentCulture));
            Assert.IsFalse(StringExtensions.Contains("aaaabbbb", "cccc", StringComparison.InvariantCulture));

            Assert.IsFalse(StringExtensions.Contains("aaaabbbb", "cccc", StringComparison.OrdinalIgnoreCase));
            Assert.IsFalse(StringExtensions.Contains("aaaabbbb", "cccc", StringComparison.CurrentCultureIgnoreCase));
            Assert.IsFalse(StringExtensions.Contains("aaaabbbb", "cccc", StringComparison.InvariantCultureIgnoreCase));
        }

        [Test]
        public void Contains_P1_IsEmptyString()
        {
            Assert.False(StringExtensions.Contains(string.Empty, "aaaa", StringComparison.Ordinal));
        }

        [Test]
        public void Contains_P1_IsEmptyString_P2_IsEmptyString()
        {
            Assert.True(StringExtensions.Contains(string.Empty, string.Empty, StringComparison.Ordinal));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Contains_P1_IsNull()
        {
            StringExtensions.Contains(null, "aaaa", StringComparison.Ordinal);
        }

        [Test]
        public void Contains_P2_IsEmptyString()
        {
            Assert.True(StringExtensions.Contains("aaaa", string.Empty, StringComparison.Ordinal));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Contains_P2_IsNull()
        {
            StringExtensions.Contains("aaaa", null, StringComparison.Ordinal);
        }

        [Test]
        public void Replace_P1_Contains_P2()
        {
            Assert.AreEqual(StringExtensions.Replace("aaaabbbb", "aabb", "cccc", StringComparison.Ordinal), "aaccccbb");
            Assert.AreEqual(StringExtensions.Replace("aaaabbbb", "aabb", "cccc", StringComparison.CurrentCulture), "aaccccbb");
            Assert.AreEqual(StringExtensions.Replace("aaaabbbb", "aabb", "cccc", StringComparison.InvariantCulture), "aaccccbb");

            Assert.AreEqual(StringExtensions.Replace("aaaabbbb", "aabb", "cccc", StringComparison.OrdinalIgnoreCase), "aaccccbb");
            Assert.AreEqual(StringExtensions.Replace("aaaabbbb", "aabb", "cccc", StringComparison.CurrentCultureIgnoreCase), "aaccccbb");
            Assert.AreEqual(StringExtensions.Replace("aaaabbbb", "aabb", "cccc", StringComparison.InvariantCultureIgnoreCase), "aaccccbb");
        }

        [Test]
        public void Replace_P1_Contains_P2_InDiffCase()
        {
            Assert.AreEqual(StringExtensions.Replace("aaaabbbb", "AABB", "cccc", StringComparison.Ordinal), "aaaabbbb");
            Assert.AreEqual(StringExtensions.Replace("aaaabbbb", "AABB", "cccc", StringComparison.CurrentCulture), "aaaabbbb");
            Assert.AreEqual(StringExtensions.Replace("aaaabbbb", "AABB", "cccc", StringComparison.InvariantCulture), "aaaabbbb");

            Assert.AreEqual(StringExtensions.Replace("aaaabbbb", "AABB", "cccc", StringComparison.OrdinalIgnoreCase), "aaccccbb");
            Assert.AreEqual(StringExtensions.Replace("aaaabbbb", "AABB", "cccc", StringComparison.CurrentCultureIgnoreCase), "aaccccbb");
            Assert.AreEqual(StringExtensions.Replace("aaaabbbb", "AABB", "cccc", StringComparison.InvariantCultureIgnoreCase), "aaccccbb");
        }

        [Test]
        public void Replace_P1_DoesNotContain_P2()
        {
            string param1 = "aaaabbbb";

            Assert.AreSame(StringExtensions.Replace(param1, "cccc", "dddd", StringComparison.Ordinal), param1);
            Assert.AreSame(StringExtensions.Replace(param1, "cccc", "dddd", StringComparison.CurrentCulture), param1);
            Assert.AreSame(StringExtensions.Replace(param1, "cccc", "dddd", StringComparison.InvariantCulture), param1);

            Assert.AreSame(StringExtensions.Replace(param1, "cccc", "dddd", StringComparison.OrdinalIgnoreCase), param1);
            Assert.AreSame(StringExtensions.Replace(param1, "cccc", "dddd", StringComparison.CurrentCultureIgnoreCase), param1);
            Assert.AreSame(StringExtensions.Replace(param1, "cccc", "dddd", StringComparison.InvariantCultureIgnoreCase), param1);
        }

        [Test]
        public void Replace_P1_IsEmptyString()
        {
            Assert.AreSame(StringExtensions.Replace(string.Empty, "bbbb", "cccc", StringComparison.Ordinal), string.Empty);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Replace_P1_IsNull()
        {
            StringExtensions.Replace(null, "bbbb", "cccc", StringComparison.Ordinal);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Replace_P2_IsEmpty()
        {
            StringExtensions.Replace("aaaa", string.Empty, "cccc", StringComparison.Ordinal);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Replace_P2_IsNull()
        {
            StringExtensions.Replace("aaaa", null, "cccc", StringComparison.Ordinal);
        }

        [Test]
        public void Replace_P3_IsEmptyString()
        {
            Assert.AreEqual(StringExtensions.Replace("aaaabbbb", "aabb", string.Empty, StringComparison.Ordinal), "aabb");
            Assert.AreEqual(StringExtensions.Replace("aaaabbbb", "aabb", string.Empty, StringComparison.CurrentCulture), "aabb");
            Assert.AreEqual(StringExtensions.Replace("aaaabbbb", "aabb", string.Empty, StringComparison.InvariantCulture), "aabb");

            Assert.AreEqual(StringExtensions.Replace("aaaabbbb", "aabb", string.Empty, StringComparison.OrdinalIgnoreCase), "aabb");
            Assert.AreEqual(StringExtensions.Replace("aaaabbbb", "aabb", string.Empty, StringComparison.CurrentCultureIgnoreCase), "aabb");
            Assert.AreEqual(StringExtensions.Replace("aaaabbbb", "aabb", string.Empty, StringComparison.InvariantCultureIgnoreCase), "aabb");
        }

        [Test]
        public void Replace_P3_IsNull()
        {
            Assert.AreEqual(StringExtensions.Replace("aaaabbbb", "aabb", null, StringComparison.Ordinal), "aabb");
            Assert.AreEqual(StringExtensions.Replace("aaaabbbb", "aabb", null, StringComparison.CurrentCulture), "aabb");
            Assert.AreEqual(StringExtensions.Replace("aaaabbbb", "aabb", null, StringComparison.InvariantCulture), "aabb");

            Assert.AreEqual(StringExtensions.Replace("aaaabbbb", "aabb", null, StringComparison.OrdinalIgnoreCase), "aabb");
            Assert.AreEqual(StringExtensions.Replace("aaaabbbb", "aabb", null, StringComparison.CurrentCultureIgnoreCase), "aabb");
            Assert.AreEqual(StringExtensions.Replace("aaaabbbb", "aabb", null, StringComparison.InvariantCultureIgnoreCase), "aabb");
        }

        [Test]
        public void IndexOf()
        {
            // 1st Overload

            Assert.AreEqual(0, IndexOfExceptionHelper<ArgumentNullException>(new IndexOf1(StringExtensions.IndexOf), null, 'a'));

            Assert.AreEqual(0, IndexOfHelper(4, new IndexOf1(StringExtensions.IndexOf), "aaaabbbb", 'b'));
            Assert.AreEqual(0, IndexOfHelper(-1, 4, new IndexOf1(StringExtensions.IndexOf), "aaaabbbb", 'B'));
            Assert.AreEqual(0, IndexOfHelper(-1, new IndexOf1(StringExtensions.IndexOf), "aaaabbbb", 'c'));
            Assert.AreEqual(0, IndexOfHelper(12, new IndexOf1(StringExtensions.IndexOf), "ccccccccccccaaaa", 'a'));
            Assert.AreEqual(0, IndexOfHelper(-1, 12, new IndexOf1(StringExtensions.IndexOf), "ccccccccccccaaaa", 'A'));


            // 2nd Overload

            Assert.AreEqual(0, IndexOfExceptionHelper<ArgumentNullException>(new IndexOf2(StringExtensions.IndexOf), null, 'a', 0));
            Assert.AreEqual(0, IndexOfExceptionHelper<ArgumentOutOfRangeException>(new IndexOf2(StringExtensions.IndexOf), "aaaa", 'a', -1));
            Assert.AreEqual(0, IndexOfExceptionHelper<ArgumentOutOfRangeException>(new IndexOf2(StringExtensions.IndexOf), "aaaa", 'a', 5));

            Assert.AreEqual(0, IndexOfHelper(-1, new IndexOf2(StringExtensions.IndexOf), "aaaa", 'a', 4));
            Assert.AreEqual(0, IndexOfHelper(6, new IndexOf2(StringExtensions.IndexOf), "aabbccbb", 'b', 4));
            Assert.AreEqual(0, IndexOfHelper(-1, 6, new IndexOf2(StringExtensions.IndexOf), "aaBBccbb", 'B', 4));
            Assert.AreEqual(0, IndexOfHelper(-1, new IndexOf2(StringExtensions.IndexOf), "ddddccbb", 'd', 4));
            Assert.AreEqual(0, IndexOfHelper(12, new IndexOf2(StringExtensions.IndexOf), "acccccccccccaaaa", 'a', 1));
            Assert.AreEqual(0, IndexOfHelper(-1, 12, new IndexOf2(StringExtensions.IndexOf), "acccccccccccaaaa", 'A', 1));


            // 3rd Overload

            Assert.AreEqual(0, IndexOfExceptionHelper<ArgumentNullException>(new IndexOf3(StringExtensions.IndexOf), null, 'a', 0, 0));
            Assert.AreEqual(0, IndexOfExceptionHelper<ArgumentOutOfRangeException>(new IndexOf3(StringExtensions.IndexOf), "aaaa", 'a', -1, 1));
            Assert.AreEqual(0, IndexOfExceptionHelper<ArgumentOutOfRangeException>(new IndexOf3(StringExtensions.IndexOf), "aaaa", 'a', 5, 0));
            Assert.AreEqual(0, IndexOfExceptionHelper<ArgumentOutOfRangeException>(new IndexOf3(StringExtensions.IndexOf), "aaaa", 'a', 5, -1));
            Assert.AreEqual(0, IndexOfExceptionHelper<ArgumentOutOfRangeException>(new IndexOf3(StringExtensions.IndexOf), "aaaa", 'a', 0, -1));
            Assert.AreEqual(0, IndexOfExceptionHelper<ArgumentOutOfRangeException>(new IndexOf3(StringExtensions.IndexOf), "aaaa", 'a', 2, 3));

            Assert.AreEqual(0, IndexOfHelper(-1, new IndexOf3(StringExtensions.IndexOf), "aaaa", 'a', 4, 0));
            Assert.AreEqual(0, IndexOfHelper(-1, new IndexOf3(StringExtensions.IndexOf), "aaaa", 'a', 0, 0));
            Assert.AreEqual(0, IndexOfHelper(6, new IndexOf3(StringExtensions.IndexOf), "ccbbccbbccbb", 'b', 4, 4));
            Assert.AreEqual(0, IndexOfHelper(-1, 6, new IndexOf3(StringExtensions.IndexOf), "CCBBccbbCCBB", 'B', 4, 4));
            Assert.AreEqual(0, IndexOfHelper(-1, new IndexOf3(StringExtensions.IndexOf), "ddddccbbdddd", 'd', 4, 4));
            Assert.AreEqual(0, IndexOfHelper(12, new IndexOf3(StringExtensions.IndexOf), "acccccccccccaaaa", 'a', 1, 13));
            Assert.AreEqual(0, IndexOfHelper(-1, 12, new IndexOf3(StringExtensions.IndexOf), "acccccccccccaaAA", 'A', 1, 13));
        }

        [Test]
        public void IndexOfAny()
        {
            char[] emptyCharArr = new char[0];
            char[] validCharArr = new char[] { 'a' };
            char[] simpleCharArr = new char[] { 'b', 'c' };
            char[] simpleCharArrUpper = new char[] { 'B', 'C' };
            char[] notFoundCharArr = new char[] { 'd' };
            char[] largeCharArr = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l' };
            char[] largeCharArrUpper = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L' };

            // IndexOfAny(string, char[], StringComparison)

            Assert.AreEqual(0, IndexOfExceptionHelper<ArgumentNullException>(new IndexOfAny1(StringExtensions.IndexOfAny), null, validCharArr));
            Assert.AreEqual(0, IndexOfExceptionHelper<ArgumentNullException>(new IndexOfAny1(StringExtensions.IndexOfAny), "aaaa", null));

            Assert.AreEqual(0, IndexOfHelper(-1, new IndexOfAny1(StringExtensions.IndexOfAny), "aaaa", emptyCharArr));
            Assert.AreEqual(0, IndexOfHelper(4, new IndexOfAny1(StringExtensions.IndexOfAny), "aaaabbbbcccc", simpleCharArr));
            Assert.AreEqual(0, IndexOfHelper(4, new IndexOfAny1(StringExtensions.IndexOfAny), "aaaaccccbbbb", simpleCharArr));
            Assert.AreEqual(0, IndexOfHelper(-1, 4, new IndexOfAny1(StringExtensions.IndexOfAny), "aaaabbbbcccc", simpleCharArrUpper));
            Assert.AreEqual(0, IndexOfHelper(-1, 4, new IndexOfAny1(StringExtensions.IndexOfAny), "aaaaccccbbbb", simpleCharArrUpper));
            Assert.AreEqual(0, IndexOfHelper(-1, new IndexOfAny1(StringExtensions.IndexOfAny), "aaaabbbbcccc", notFoundCharArr));
            // Testing loop folding pathway
            Assert.AreEqual(0, IndexOfHelper(12, new IndexOfAny1(StringExtensions.IndexOfAny), "zzzzzzzzzzzzdddd", largeCharArr));
            Assert.AreEqual(0, IndexOfHelper(12, new IndexOfAny1(StringExtensions.IndexOfAny), "zzzzzzzzzzzzkkkk", largeCharArr));
            Assert.AreEqual(0, IndexOfHelper(-1, 12, new IndexOfAny1(StringExtensions.IndexOfAny), "zzzzzzzzzzzzdddd", largeCharArrUpper));
            Assert.AreEqual(0, IndexOfHelper(-1, 12, new IndexOfAny1(StringExtensions.IndexOfAny), "zzzzzzzzzzzzkkkk", largeCharArrUpper));


            // IndexOfAny(string, char[], int, StringComparison)

            Assert.AreEqual(0, IndexOfExceptionHelper<ArgumentNullException>(new IndexOfAny2(StringExtensions.IndexOfAny), null, validCharArr, 0));
            Assert.AreEqual(0, IndexOfExceptionHelper<ArgumentNullException>(new IndexOfAny2(StringExtensions.IndexOfAny), "aaaa", null, 0));
            Assert.AreEqual(0, IndexOfExceptionHelper<ArgumentOutOfRangeException>(new IndexOfAny2(StringExtensions.IndexOfAny), "aaaa", validCharArr, -1));
            Assert.AreEqual(0, IndexOfExceptionHelper<ArgumentOutOfRangeException>(new IndexOfAny2(StringExtensions.IndexOfAny), "aaaa", validCharArr, 5));

            Assert.AreEqual(0, IndexOfHelper(-1, new IndexOfAny2(StringExtensions.IndexOfAny), "aaaa", emptyCharArr, 0));
            Assert.AreEqual(0, IndexOfHelper(-1, new IndexOfAny2(StringExtensions.IndexOfAny), "aaaa", validCharArr, 4));
            Assert.AreEqual(0, IndexOfHelper(4, new IndexOfAny2(StringExtensions.IndexOfAny), "bbccbbcc", simpleCharArr, 4));
            Assert.AreEqual(0, IndexOfHelper(4, new IndexOfAny2(StringExtensions.IndexOfAny), "ccbbccbb", simpleCharArr, 4));
            Assert.AreEqual(0, IndexOfHelper(-1, 4, new IndexOfAny2(StringExtensions.IndexOfAny), "BBCCbbcc", simpleCharArrUpper, 4));
            Assert.AreEqual(0, IndexOfHelper(-1, 4, new IndexOfAny2(StringExtensions.IndexOfAny), "CCBBccbb", simpleCharArrUpper, 4));
            Assert.AreEqual(0, IndexOfHelper(-1, new IndexOfAny2(StringExtensions.IndexOfAny), "ddddbbcc", notFoundCharArr, 4));
            // Testing loop folding pathway
            Assert.AreEqual(0, IndexOfHelper(12, new IndexOfAny2(StringExtensions.IndexOfAny), "dzzzzzzzzzzzdddd", largeCharArr, 1));
            Assert.AreEqual(0, IndexOfHelper(12, new IndexOfAny2(StringExtensions.IndexOfAny), "kzzzzzzzzzzzkkkk", largeCharArr, 1));
            Assert.AreEqual(0, IndexOfHelper(-1, 12, new IndexOfAny2(StringExtensions.IndexOfAny), "Dzzzzzzzzzzzdddd", largeCharArrUpper, 1));
            Assert.AreEqual(0, IndexOfHelper(-1, 12, new IndexOfAny2(StringExtensions.IndexOfAny), "Kzzzzzzzzzzzkkkk", largeCharArrUpper, 1));


            // IndexOfAny(string, char[], int, int, StringComparison)

            Assert.AreEqual(0, IndexOfExceptionHelper<ArgumentNullException>(new IndexOfAny3(StringExtensions.IndexOfAny), null, validCharArr, 0, 0));
            Assert.AreEqual(0, IndexOfExceptionHelper<ArgumentNullException>(new IndexOfAny3(StringExtensions.IndexOfAny), "aaaa", null, 0, 0));
            Assert.AreEqual(0, IndexOfExceptionHelper<ArgumentOutOfRangeException>(new IndexOfAny3(StringExtensions.IndexOfAny), "aaaa", validCharArr, -1, 1));
            Assert.AreEqual(0, IndexOfExceptionHelper<ArgumentOutOfRangeException>(new IndexOfAny3(StringExtensions.IndexOfAny), "aaaa", validCharArr, 2, 3));
            Assert.AreEqual(0, IndexOfExceptionHelper<ArgumentOutOfRangeException>(new IndexOfAny3(StringExtensions.IndexOfAny), "aaaa", validCharArr, 1, -1));

            Assert.AreEqual(0, IndexOfHelper(-1, new IndexOfAny3(StringExtensions.IndexOfAny), "aaaa", emptyCharArr, 0, 4));
            Assert.AreEqual(0, IndexOfHelper(-1, new IndexOfAny3(StringExtensions.IndexOfAny), "aaaa", validCharArr, 4, 0));
            Assert.AreEqual(0, IndexOfHelper(-1, new IndexOfAny3(StringExtensions.IndexOfAny), "aaaa", validCharArr, 0, 0));
            Assert.AreEqual(0, IndexOfHelper(4, new IndexOfAny3(StringExtensions.IndexOfAny), "bbccbbccbbcc", simpleCharArr, 4, 4));
            Assert.AreEqual(0, IndexOfHelper(4, new IndexOfAny3(StringExtensions.IndexOfAny), "ccbbccbbccbb", simpleCharArr, 4, 4));
            Assert.AreEqual(0, IndexOfHelper(-1, 4, new IndexOfAny3(StringExtensions.IndexOfAny), "BBCCbbccBBCC", simpleCharArrUpper, 4, 4));
            Assert.AreEqual(0, IndexOfHelper(-1, 4, new IndexOfAny3(StringExtensions.IndexOfAny), "CCBBccbbCCBB", simpleCharArrUpper, 4, 4));
            Assert.AreEqual(0, IndexOfHelper(-1, new IndexOfAny3(StringExtensions.IndexOfAny), "ddddbbccdddd", notFoundCharArr, 4, 4));
            // Testing loop folding pathway
            Assert.AreEqual(0, IndexOfHelper(12, new IndexOfAny3(StringExtensions.IndexOfAny), "dzzzzzzzzzzzdddd", largeCharArr, 1, 13));
            Assert.AreEqual(0, IndexOfHelper(12, new IndexOfAny3(StringExtensions.IndexOfAny), "kzzzzzzzzzzzkkkk", largeCharArr, 1, 13));
            Assert.AreEqual(0, IndexOfHelper(-1, 12, new IndexOfAny3(StringExtensions.IndexOfAny), "DzzzzzzzzzzzddDD", largeCharArrUpper, 1, 13));
            Assert.AreEqual(0, IndexOfHelper(-1, 12, new IndexOfAny3(StringExtensions.IndexOfAny), "KzzzzzzzzzzzkkKK", largeCharArrUpper, 1, 13));


            // IndexOfAny(string, string[], ...) Variables

            string[] emptyStrArr = new string[0];
            string[] validStrArr = new string[] { "aa" };
            string[] simpleStrArr = new string[] { "bb", "cc" };
            string[] simpleStrArrUpper = new string[] { "BB", "CC" };
            string[] notFoundStrArr = new string[] { "dd" };
            string[] singleCharStrArr = new string[] { "b", "c" };
            string[] largeStrArr = new string[] { "aa", "bb", "cc", "dd", "ee", "ff", "gg", "hh", "ii", "jj", "kk", "ll" };
            string[] largeStrArrUpper = new string[] { "AA", "BB", "CC", "DD", "EE", "FF", "GG", "HH", "II", "JJ", "KK", "LL" };


            // IndexOfAny(string, string[])

            Assert.Throws<ArgumentNullException>(new TestDelegate(() => StringExtensions.IndexOfAny(null, validStrArr)));
            Assert.Throws<ArgumentNullException>(new TestDelegate(() => StringExtensions.IndexOfAny("aaaa", (string[])null)));

            Assert.AreEqual(-1, StringExtensions.IndexOfAny("aaaa", emptyStrArr));
            Assert.AreEqual(4, StringExtensions.IndexOfAny("aaaabbbbcccc", simpleStrArr));
            Assert.AreEqual(4, StringExtensions.IndexOfAny("aaaaccccbbbb", simpleStrArr));
            Assert.AreEqual(-1, StringExtensions.IndexOfAny("aaaabbbbcccc", simpleStrArrUpper));
            Assert.AreEqual(-1, StringExtensions.IndexOfAny("aaaabbbbcccc", notFoundStrArr));
            Assert.AreEqual(4, StringExtensions.IndexOfAny("aaaabbbbcccc", singleCharStrArr));
            Assert.AreEqual(4, StringExtensions.IndexOfAny("aaaaccccbbbb", singleCharStrArr));
            // Testing loop folding pathway
            Assert.AreEqual(12, StringExtensions.IndexOfAny("zzzzdzzzzzzkdddd", largeStrArr));
            Assert.AreEqual(12, StringExtensions.IndexOfAny("zzzzkzzzzzzdkkkk", largeStrArr));
            Assert.AreEqual(-1, StringExtensions.IndexOfAny("zzzzDzzzzzzkdddd", largeStrArrUpper));
            Assert.AreEqual(-1, StringExtensions.IndexOfAny("zzzzKzzzzzzdkkkk", largeStrArrUpper));


            // IndexOfAny(string, string[], int)

            Assert.Throws<ArgumentNullException>(new TestDelegate(() => StringExtensions.IndexOfAny(null, validStrArr, 0)));
            Assert.Throws<ArgumentNullException>(new TestDelegate(() => StringExtensions.IndexOfAny("aaaa", (string[])null, 0)));
            Assert.Throws<ArgumentOutOfRangeException>(new TestDelegate(() => StringExtensions.IndexOfAny("aaaa", validStrArr, -1)));
            Assert.Throws<ArgumentOutOfRangeException>(new TestDelegate(() => StringExtensions.IndexOfAny("aaaa", validStrArr, 5)));

            Assert.AreEqual(-1, StringExtensions.IndexOfAny("aaaa", emptyStrArr, 0));
            Assert.AreEqual(-1, StringExtensions.IndexOfAny("aaaa", validStrArr, 4));
            Assert.AreEqual(4, StringExtensions.IndexOfAny("bbccbbcc", simpleStrArr, 4));
            Assert.AreEqual(4, StringExtensions.IndexOfAny("ccbbccbb", simpleStrArr, 4));
            Assert.AreEqual(-1, StringExtensions.IndexOfAny("BBCCbbcc", simpleStrArrUpper, 4));
            Assert.AreEqual(-1, StringExtensions.IndexOfAny("ddddbbcc", notFoundStrArr, 4));
            Assert.AreEqual(4, StringExtensions.IndexOfAny("bbccbbcc", singleCharStrArr, 4));
            Assert.AreEqual(4, StringExtensions.IndexOfAny("bbccbbcc", singleCharStrArr, 4));
            // Testing loop folding pathway
            Assert.AreEqual(12, StringExtensions.IndexOfAny("ddzzzzdzzzzkdddd", largeStrArr, 1));
            Assert.AreEqual(12, StringExtensions.IndexOfAny("kkzzzzkzzzzdkkkk", largeStrArr, 1));
            Assert.AreEqual(-1, StringExtensions.IndexOfAny("DDzzzzDzzzzkdddd", largeStrArrUpper, 1));
            Assert.AreEqual(-1, StringExtensions.IndexOfAny("KKzzzzKzzzzdkkkk", largeStrArrUpper, 1));


            // IndexOfAny(string, string[], int, int)

            Assert.Throws<ArgumentNullException>(new TestDelegate(() => StringExtensions.IndexOfAny(null, validStrArr, 0, 0)));
            Assert.Throws<ArgumentNullException>(new TestDelegate(() => StringExtensions.IndexOfAny("aaaa", (string[])null, 0, 0)));
            Assert.Throws<ArgumentOutOfRangeException>(new TestDelegate(() => StringExtensions.IndexOfAny("aaaa", validStrArr, -1, 1)));
            Assert.Throws<ArgumentOutOfRangeException>(new TestDelegate(() => StringExtensions.IndexOfAny("aaaa", validStrArr, 5, 0)));
            Assert.Throws<ArgumentOutOfRangeException>(new TestDelegate(() => StringExtensions.IndexOfAny("aaaa", validStrArr, 5, -1)));

            Assert.AreEqual(-1, StringExtensions.IndexOfAny("aaaa", emptyStrArr, 0, 4));
            Assert.AreEqual(-1, StringExtensions.IndexOfAny("aaaa", validStrArr, 4, 0));
            Assert.AreEqual(4, StringExtensions.IndexOfAny("bbccbbccbbcc", simpleStrArr, 4, 4));
            Assert.AreEqual(4, StringExtensions.IndexOfAny("ccbbccbbccbb", simpleStrArr, 4, 4));
            Assert.AreEqual(-1, StringExtensions.IndexOfAny("BBCCbbccBBCC", simpleStrArrUpper, 4, 4));
            Assert.AreEqual(-1, StringExtensions.IndexOfAny("ddddbbccdddd", notFoundStrArr, 4, 4));
            Assert.AreEqual(4, StringExtensions.IndexOfAny("bbccbbccbbcc", singleCharStrArr, 4, 4));
            Assert.AreEqual(4, StringExtensions.IndexOfAny("ccbbccbbccbb", singleCharStrArr, 4, 4));
            // Testing loop folding pathway
            Assert.AreEqual(12, StringExtensions.IndexOfAny("ddzzzzdzzzzkddDD", largeStrArr, 1, 13));
            Assert.AreEqual(12, StringExtensions.IndexOfAny("kkzzzzkzzzzdkkKK", largeStrArr, 1, 13));
            Assert.AreEqual(-1, StringExtensions.IndexOfAny("DDzzzzDzzzzkddDD", largeStrArrUpper, 1, 13));
            Assert.AreEqual(-1, StringExtensions.IndexOfAny("KKzzzzKzzzzdkkKK", largeStrArrUpper, 1, 13));


            // IndexOfAny(string, string[], StringComparison)

            Assert.AreEqual(0, IndexOfExceptionHelper<ArgumentNullException>(new IndexOfAny4(StringExtensions.IndexOfAny), null, validStrArr));
            Assert.AreEqual(0, IndexOfExceptionHelper<ArgumentNullException>(new IndexOfAny4(StringExtensions.IndexOfAny), "aaaa", null));
            
            Assert.AreEqual(0, IndexOfHelper(-1, new IndexOfAny4(StringExtensions.IndexOfAny), "aaaa", emptyStrArr));
            Assert.AreEqual(0, IndexOfHelper(4, new IndexOfAny4(StringExtensions.IndexOfAny), "aaaabbbbcccc", simpleStrArr));
            Assert.AreEqual(0, IndexOfHelper(4, new IndexOfAny4(StringExtensions.IndexOfAny), "aaaaccccbbbb", simpleStrArr));
            Assert.AreEqual(0, IndexOfHelper(-1, 4, new IndexOfAny4(StringExtensions.IndexOfAny), "aaaabbbbcccc", simpleStrArrUpper));
            Assert.AreEqual(0, IndexOfHelper(-1, 4, new IndexOfAny4(StringExtensions.IndexOfAny), "aaaaccccbbbb", simpleStrArrUpper));
            Assert.AreEqual(0, IndexOfHelper(-1, new IndexOfAny4(StringExtensions.IndexOfAny), "aaaabbbbcccc", notFoundStrArr));
            Assert.AreEqual(0, IndexOfHelper(4, new IndexOfAny4(StringExtensions.IndexOfAny), "aaaabbbbcccc", singleCharStrArr));
            Assert.AreEqual(0, IndexOfHelper(4, new IndexOfAny4(StringExtensions.IndexOfAny), "aaaaccccbbbb", singleCharStrArr));
            // Testing loop folding pathway
            Assert.AreEqual(0, IndexOfHelper(12, new IndexOfAny4(StringExtensions.IndexOfAny), "zzzzzzdzzzzkdddd", largeStrArr));
            Assert.AreEqual(0, IndexOfHelper(12, new IndexOfAny4(StringExtensions.IndexOfAny), "zzzzzzkzzzzdkkkk", largeStrArr));
            Assert.AreEqual(0, IndexOfHelper(-1, 12, new IndexOfAny4(StringExtensions.IndexOfAny), "zzzzzzDzzzzkdddd", largeStrArrUpper));
            Assert.AreEqual(0, IndexOfHelper(-1, 12, new IndexOfAny4(StringExtensions.IndexOfAny), "zzzzzzKzzzzdkkkk", largeStrArrUpper));


            // IndexOfAny(string, string[], int, StringComparison)

            Assert.AreEqual(0, IndexOfExceptionHelper<ArgumentNullException>(new IndexOfAny5(StringExtensions.IndexOfAny), null, validStrArr, 0));
            Assert.AreEqual(0, IndexOfExceptionHelper<ArgumentNullException>(new IndexOfAny5(StringExtensions.IndexOfAny), "aaaa", null, 0));
            Assert.AreEqual(0, IndexOfExceptionHelper<ArgumentOutOfRangeException>(new IndexOfAny5(StringExtensions.IndexOfAny), "aaaa", validStrArr, -1));
            Assert.AreEqual(0, IndexOfExceptionHelper<ArgumentOutOfRangeException>(new IndexOfAny5(StringExtensions.IndexOfAny), "aaaa", validStrArr, 5));

            Assert.AreEqual(0, IndexOfHelper(-1, new IndexOfAny5(StringExtensions.IndexOfAny), "aaaa", emptyStrArr, 0));
            Assert.AreEqual(0, IndexOfHelper(-1, new IndexOfAny5(StringExtensions.IndexOfAny), "aaaa", validStrArr, 4));
            Assert.AreEqual(0, IndexOfHelper(6, new IndexOfAny5(StringExtensions.IndexOfAny), "bbccaabbccbbcc", simpleStrArr, 4));
            Assert.AreEqual(0, IndexOfHelper(6, new IndexOfAny5(StringExtensions.IndexOfAny), "ccbbaaccbbccbb", simpleStrArr, 4));
            Assert.AreEqual(0, IndexOfHelper(-1, 6, new IndexOfAny5(StringExtensions.IndexOfAny), "BBCCaabbcc", simpleStrArrUpper, 4));
            Assert.AreEqual(0, IndexOfHelper(-1, 6, new IndexOfAny5(StringExtensions.IndexOfAny), "BBCCaaccbb", simpleStrArrUpper, 4));
            Assert.AreEqual(0, IndexOfHelper(-1, new IndexOfAny5(StringExtensions.IndexOfAny), "ddddbbcc", notFoundStrArr, 4));
            Assert.AreEqual(0, IndexOfHelper(6, new IndexOfAny5(StringExtensions.IndexOfAny), "bbccaabbccbbcc", singleCharStrArr, 4));
            Assert.AreEqual(0, IndexOfHelper(6, new IndexOfAny5(StringExtensions.IndexOfAny), "ccbbaaccbbccbb", singleCharStrArr, 4));
            // Testing loop folding pathway
            Assert.AreEqual(0, IndexOfHelper(12, new IndexOfAny5(StringExtensions.IndexOfAny), "ddzzzzdzzzzkdddd", largeStrArr, 1));
            Assert.AreEqual(0, IndexOfHelper(12, new IndexOfAny5(StringExtensions.IndexOfAny), "kkzzzzkzzzzdkkkk", largeStrArr, 1));
            Assert.AreEqual(0, IndexOfHelper(-1, 12, new IndexOfAny5(StringExtensions.IndexOfAny), "DDzzzzDzzzzkdddd", largeStrArrUpper, 1));
            Assert.AreEqual(0, IndexOfHelper(-1, 12, new IndexOfAny5(StringExtensions.IndexOfAny), "KKzzzzKzzzzdkkkk", largeStrArrUpper, 1));


            // IndexOfAny(string, string[], int, int, StringComparison)

            Assert.AreEqual(0, IndexOfExceptionHelper<ArgumentNullException>(new IndexOfAny6(StringExtensions.IndexOfAny), null, validStrArr, 0, 0));
            Assert.AreEqual(0, IndexOfExceptionHelper<ArgumentNullException>(new IndexOfAny6(StringExtensions.IndexOfAny), "aaaa", null, 0, 0));
            Assert.AreEqual(0, IndexOfExceptionHelper<ArgumentOutOfRangeException>(new IndexOfAny6(StringExtensions.IndexOfAny), "aaaa", validStrArr, -1, 1));
            Assert.AreEqual(0, IndexOfExceptionHelper<ArgumentOutOfRangeException>(new IndexOfAny6(StringExtensions.IndexOfAny), "aaaa", validStrArr, 5, 0));
            Assert.AreEqual(0, IndexOfExceptionHelper<ArgumentOutOfRangeException>(new IndexOfAny6(StringExtensions.IndexOfAny), "aaaa", validStrArr, 5, -1));

            Assert.AreEqual(0, IndexOfHelper(-1, new IndexOfAny6(StringExtensions.IndexOfAny), "aaaa", emptyStrArr, 0, 4));
            Assert.AreEqual(0, IndexOfHelper(-1, new IndexOfAny6(StringExtensions.IndexOfAny), "aaaa", validStrArr, 4, 0));
            Assert.AreEqual(0, IndexOfHelper(6, new IndexOfAny6(StringExtensions.IndexOfAny), "bbccaabbccbbcc", simpleStrArr, 4, 4));
            Assert.AreEqual(0, IndexOfHelper(6, new IndexOfAny6(StringExtensions.IndexOfAny), "ccbbaaccbbccbb", simpleStrArr, 4, 4));
            Assert.AreEqual(0, IndexOfHelper(-1, 6, new IndexOfAny6(StringExtensions.IndexOfAny), "BBCCaabbccBBCC", simpleStrArrUpper, 4, 4));
            Assert.AreEqual(0, IndexOfHelper(-1, 6, new IndexOfAny6(StringExtensions.IndexOfAny), "BBCCaaccbbBBCC", simpleStrArrUpper, 4, 4));
            Assert.AreEqual(0, IndexOfHelper(-1, new IndexOfAny6(StringExtensions.IndexOfAny), "ddddbbccdddd", notFoundStrArr, 4, 4));
            Assert.AreEqual(0, IndexOfHelper(6, new IndexOfAny6(StringExtensions.IndexOfAny), "bbccaabbccbbcc", singleCharStrArr, 4, 4));
            Assert.AreEqual(0, IndexOfHelper(6, new IndexOfAny6(StringExtensions.IndexOfAny), "ccbbaaccbbccbb", singleCharStrArr, 4, 4));
            // Testing loop folding pathway
            Assert.AreEqual(0, IndexOfHelper(12, new IndexOfAny6(StringExtensions.IndexOfAny), "ddzzzzdzzzzkdddd", largeStrArr, 1, 13));
            Assert.AreEqual(0, IndexOfHelper(12, new IndexOfAny6(StringExtensions.IndexOfAny), "kkzzzzkzzzzdkkkk", largeStrArr, 1, 13));
            Assert.AreEqual(0, IndexOfHelper(-1, 12, new IndexOfAny6(StringExtensions.IndexOfAny), "DDzzzzDzzzzkddDD", largeStrArrUpper, 1, 13));
            Assert.AreEqual(0, IndexOfHelper(-1, 12, new IndexOfAny6(StringExtensions.IndexOfAny), "KKzzzzKzzzzdkkKK", largeStrArrUpper, 1, 13));
        }

        [Test]
        public void IndexOfNotAny()
        {
            char[] emptyCharArr = new char[0];
            char[] validCharArr = new char[] { 'a' };
            char[] simpleCharArr = new char[] { 'b', 'c' };
            char[] simpleCharArrUpper = new char[] { 'B', 'C' };
            char[] notFoundCharArr = new char[] { 'a', 'b', 'c' };
            char[] largeCharArr = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l' };
            char[] largeCharArrUpper = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L' };

            // IndexOfNotAny(string, char[], StringComparison)

            Assert.AreEqual(0, IndexOfExceptionHelper<ArgumentNullException>(new IndexOfNotAny1(StringExtensions.IndexOfNotAny), null, validCharArr));
            Assert.AreEqual(0, IndexOfExceptionHelper<ArgumentNullException>(new IndexOfNotAny1(StringExtensions.IndexOfNotAny), "aaaa", null));

            Assert.AreEqual(0, IndexOfHelper(0, new IndexOfNotAny1(StringExtensions.IndexOfNotAny), "aaaa", emptyCharArr));
            Assert.AreEqual(0, IndexOfHelper(8, new IndexOfNotAny1(StringExtensions.IndexOfNotAny), "ccccbbbbaaaa", simpleCharArr));
            Assert.AreEqual(0, IndexOfHelper(0, 8, new IndexOfNotAny1(StringExtensions.IndexOfNotAny), "ccccbbbbaaaa", simpleCharArrUpper));
            Assert.AreEqual(0, IndexOfHelper(-1, new IndexOfNotAny1(StringExtensions.IndexOfNotAny), "aaaabbbbcccc", notFoundCharArr));
            // Testing loop folding pathway
            Assert.AreEqual(0, IndexOfHelper(12, new IndexOfNotAny1(StringExtensions.IndexOfNotAny), "abcdefghijklzzzz", largeCharArr));
            Assert.AreEqual(0, IndexOfHelper(0, 12, new IndexOfNotAny1(StringExtensions.IndexOfNotAny), "abcdefghijklzzzz", largeCharArrUpper));


            // IndexOfNotAny(string, char[], int, StringComparison)

            Assert.AreEqual(0, IndexOfExceptionHelper<ArgumentNullException>(new IndexOfNotAny2(StringExtensions.IndexOfNotAny), null, validCharArr, 0));
            Assert.AreEqual(0, IndexOfExceptionHelper<ArgumentNullException>(new IndexOfNotAny2(StringExtensions.IndexOfNotAny), "aaaa", null, 0));
            Assert.AreEqual(0, IndexOfExceptionHelper<ArgumentOutOfRangeException>(new IndexOfNotAny2(StringExtensions.IndexOfNotAny), "aaaa", validCharArr, -1));
            Assert.AreEqual(0, IndexOfExceptionHelper<ArgumentOutOfRangeException>(new IndexOfNotAny2(StringExtensions.IndexOfNotAny), "aaaa", validCharArr, 5));

            Assert.AreEqual(0, IndexOfHelper(0, new IndexOfNotAny2(StringExtensions.IndexOfNotAny), "aaaa", emptyCharArr, 0));
            Assert.AreEqual(0, IndexOfHelper(-1, new IndexOfNotAny2(StringExtensions.IndexOfNotAny), "aaaa", validCharArr, 4));
            Assert.AreEqual(0, IndexOfHelper(-1, new IndexOfNotAny2(StringExtensions.IndexOfNotAny), "bbccbbcc", simpleCharArr, 4));
            Assert.AreEqual(0, IndexOfHelper(8, new IndexOfNotAny2(StringExtensions.IndexOfNotAny), "bbccbbccaaaa", simpleCharArr, 4));
            Assert.AreEqual(0, IndexOfHelper(4, -1, new IndexOfNotAny2(StringExtensions.IndexOfNotAny), "BBCCbbcc", simpleCharArrUpper, 4));
            Assert.AreEqual(0, IndexOfHelper(4, 8, new IndexOfNotAny2(StringExtensions.IndexOfNotAny), "BBCCbbccaaaa", simpleCharArrUpper, 4));
            Assert.AreEqual(0, IndexOfHelper(-1, new IndexOfNotAny2(StringExtensions.IndexOfNotAny), "ddddbbccaa", notFoundCharArr, 4));
            // Testing loop folding pathway
            Assert.AreEqual(0, IndexOfHelper(13, new IndexOfNotAny2(StringExtensions.IndexOfNotAny), "zabcdefghijklzzzz", largeCharArr, 1));
            Assert.AreEqual(0, IndexOfHelper(1, 13, new IndexOfNotAny2(StringExtensions.IndexOfNotAny), "zabcdefghijklzzzz", largeCharArrUpper, 1));


            // IndexOfNotAny(string, char[], int, int, StringComparison)

            Assert.AreEqual(0, IndexOfExceptionHelper<ArgumentNullException>(new IndexOfNotAny3(StringExtensions.IndexOfNotAny), null, validCharArr, 0, 0));
            Assert.AreEqual(0, IndexOfExceptionHelper<ArgumentNullException>(new IndexOfNotAny3(StringExtensions.IndexOfNotAny), "aaaa", null, 0, 0));
            Assert.AreEqual(0, IndexOfExceptionHelper<ArgumentOutOfRangeException>(new IndexOfNotAny3(StringExtensions.IndexOfNotAny), "aaaa", validCharArr, -1, 1));
            Assert.AreEqual(0, IndexOfExceptionHelper<ArgumentOutOfRangeException>(new IndexOfNotAny3(StringExtensions.IndexOfNotAny), "aaaa", validCharArr, 2, 3));
            Assert.AreEqual(0, IndexOfExceptionHelper<ArgumentOutOfRangeException>(new IndexOfNotAny3(StringExtensions.IndexOfNotAny), "aaaa", validCharArr, 1, -1));

            Assert.AreEqual(0, IndexOfHelper(0, new IndexOfNotAny3(StringExtensions.IndexOfNotAny), "aaaa", emptyCharArr, 0, 4));
            Assert.AreEqual(0, IndexOfHelper(-1, new IndexOfNotAny3(StringExtensions.IndexOfNotAny), "aaaa", validCharArr, 4, 0));
            Assert.AreEqual(0, IndexOfHelper(-1, new IndexOfNotAny3(StringExtensions.IndexOfNotAny), "aaaa", validCharArr, 0, 0));
            Assert.AreEqual(0, IndexOfHelper(-1, new IndexOfNotAny3(StringExtensions.IndexOfNotAny), "aaaabbccaaaa", simpleCharArr, 4, 4));
            Assert.AreEqual(0, IndexOfHelper(8, new IndexOfNotAny3(StringExtensions.IndexOfNotAny), "aaaabbccaaaa", simpleCharArr, 4, 6));
            Assert.AreEqual(0, IndexOfHelper(6, 10, new IndexOfNotAny3(StringExtensions.IndexOfNotAny), "BBCCAAbbccaaBBCCAA", simpleCharArrUpper, 6, 6));
            Assert.AreEqual(0, IndexOfHelper(-1, new IndexOfNotAny3(StringExtensions.IndexOfNotAny), "ddddbbccaadddd", notFoundCharArr, 4, 6));
            // Testing loop folding pathway
            Assert.AreEqual(0, IndexOfHelper(13, new IndexOfNotAny3(StringExtensions.IndexOfNotAny), "zabcdefghijklzzzz", largeCharArr, 1, 13));
            Assert.AreEqual(0, IndexOfHelper(1, 13, new IndexOfNotAny3(StringExtensions.IndexOfNotAny), "zabcdefghijklzzzz", largeCharArrUpper, 1, 13));


            // IndexOfNotAny(string, char[])

            Assert.Throws<ArgumentNullException>(new TestDelegate(() => StringExtensions.IndexOfNotAny(null, validCharArr)));
            Assert.Throws<ArgumentNullException>(new TestDelegate(() => StringExtensions.IndexOfNotAny("aaaa", (char[])null)));

            Assert.AreEqual(0, StringExtensions.IndexOfNotAny("aaaa", emptyCharArr));
            Assert.AreEqual(8, StringExtensions.IndexOfNotAny("ccccbbbbaaaa", simpleCharArr));
            Assert.AreEqual(0, StringExtensions.IndexOfNotAny("ccccbbbb", simpleCharArrUpper));
            Assert.AreEqual(-1, StringExtensions.IndexOfNotAny("aaaabbbbcccc", notFoundCharArr));
            // Testing loop folding pathway
            Assert.AreEqual(12, StringExtensions.IndexOfNotAny("abcdefghijklzzzz", largeCharArr));
            Assert.AreEqual(0, StringExtensions.IndexOfNotAny("abcdefghijklzzzz", largeCharArrUpper));


            // IndexOfNotAny(string, char[], int)

            Assert.Throws<ArgumentNullException>(new TestDelegate(() => StringExtensions.IndexOfNotAny(null, validCharArr, 0)));
            Assert.Throws<ArgumentNullException>(new TestDelegate(() => StringExtensions.IndexOfNotAny("aaaa", (char[])null, 0)));
            Assert.Throws<ArgumentOutOfRangeException>(new TestDelegate(() => StringExtensions.IndexOfNotAny("aaaa", validCharArr, -1)));
            Assert.Throws<ArgumentOutOfRangeException>(new TestDelegate(() => StringExtensions.IndexOfNotAny("aaaa", validCharArr, 5)));

            Assert.AreEqual(0, StringExtensions.IndexOfNotAny("aaaa", emptyCharArr, 0));
            Assert.AreEqual(-1, StringExtensions.IndexOfNotAny("aaaa", validCharArr, 4));
            Assert.AreEqual(10, StringExtensions.IndexOfNotAny("bbccaabbccaa", simpleCharArr, 6));
            Assert.AreEqual(4, StringExtensions.IndexOfNotAny("BBCCbbcc", simpleCharArrUpper, 4));
            Assert.AreEqual(-1, StringExtensions.IndexOfNotAny("ddddbbcc", notFoundCharArr, 4));
            // Testing loop folding pathway
            Assert.AreEqual(13, StringExtensions.IndexOfNotAny("zabcdefghijklzzzz", largeCharArr, 1));
            Assert.AreEqual(1, StringExtensions.IndexOfNotAny("zabcdefghijklzzzz", largeCharArrUpper, 1));


            // IndexOfNotAny(string, char[], int, int)

            Assert.Throws<ArgumentNullException>(new TestDelegate(() => StringExtensions.IndexOfNotAny(null, validCharArr, 0, 0)));
            Assert.Throws<ArgumentNullException>(new TestDelegate(() => StringExtensions.IndexOfNotAny("aaaa", (char[])null, 0, 0)));
            Assert.Throws<ArgumentOutOfRangeException>(new TestDelegate(() => StringExtensions.IndexOfNotAny("aaaa", validCharArr, -1, 1)));
            Assert.Throws<ArgumentOutOfRangeException>(new TestDelegate(() => StringExtensions.IndexOfNotAny("aaaa", validCharArr, 5, 0)));
            Assert.Throws<ArgumentOutOfRangeException>(new TestDelegate(() => StringExtensions.IndexOfNotAny("aaaa", validCharArr, 5, -1)));

            Assert.AreEqual(0, StringExtensions.IndexOfNotAny("aaaa", emptyCharArr, 0, 4));
            Assert.AreEqual(-1, StringExtensions.IndexOfNotAny("aaaa", validCharArr, 4, 0));
            Assert.AreEqual(8, StringExtensions.IndexOfNotAny("aaaabbccyyaaaa", simpleCharArr, 4, 6));
            Assert.AreEqual(4, StringExtensions.IndexOfNotAny("AAAAbbccaaAAAA", simpleCharArrUpper, 4, 6));
            Assert.AreEqual(-1, StringExtensions.IndexOfNotAny("ddddbbccdddd", notFoundCharArr, 4, 4));
            // Testing loop folding pathway
            Assert.AreEqual(13, StringExtensions.IndexOfNotAny("zabcdefghijklzzzz", largeCharArr, 1, 13));
            Assert.AreEqual(1, StringExtensions.IndexOfNotAny("zabcdefghijklzzzz", largeCharArrUpper, 1, 13));
        }

        [Test]
        public void LastIndexOfAny()
        {
            char[] emptyCharArr = new char[0];
            char[] validCharArr = new char[] { 'a' };
            char[] simpleCharArr = new char[] { 'b', 'c' };
            char[] simpleCharArrUpper = new char[] { 'B', 'C' };
            char[] notFoundCharArr = new char[] { 'd' };
            char[] largeCharArr = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l' };
            char[] largeCharArrUpper = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L' };

            // LastIndexOfAny(string, char[], StringComparison)

            Assert.AreEqual(0, IndexOfExceptionHelper<ArgumentNullException>(new LastIndexOfAny1(StringExtensions.LastIndexOfAny), null, validCharArr));
            Assert.AreEqual(0, IndexOfExceptionHelper<ArgumentNullException>(new LastIndexOfAny1(StringExtensions.LastIndexOfAny), "aaaa", null));

            Assert.AreEqual(0, IndexOfHelper(-1, new LastIndexOfAny1(StringExtensions.LastIndexOfAny), "aaaa", emptyCharArr));
            Assert.AreEqual(0, IndexOfHelper(3, new LastIndexOfAny1(StringExtensions.LastIndexOfAny), "aaaa", validCharArr));
            Assert.AreEqual(0, IndexOfHelper(7, new LastIndexOfAny1(StringExtensions.LastIndexOfAny), "ccccbbbbaaaa", simpleCharArr));
            Assert.AreEqual(0, IndexOfHelper(7, new LastIndexOfAny1(StringExtensions.LastIndexOfAny), "bbbbccccaaaa", simpleCharArr));
            Assert.AreEqual(0, IndexOfHelper(-1, 7, new LastIndexOfAny1(StringExtensions.LastIndexOfAny), "ccccbbbbaaaa", simpleCharArrUpper));
            Assert.AreEqual(0, IndexOfHelper(-1, 7, new LastIndexOfAny1(StringExtensions.LastIndexOfAny), "bbbbccccaaaa", simpleCharArrUpper));
            Assert.AreEqual(0, IndexOfHelper(-1, new LastIndexOfAny1(StringExtensions.LastIndexOfAny), "aaaabbbbcccc", notFoundCharArr));
            // Testing loop folding pathway
            Assert.AreEqual(0, IndexOfHelper(3, new LastIndexOfAny1(StringExtensions.LastIndexOfAny), "ddddzzzzzzzzzzzz", largeCharArr));
            Assert.AreEqual(0, IndexOfHelper(3, new LastIndexOfAny1(StringExtensions.LastIndexOfAny), "kkkkzzzzzzzzzzzz", largeCharArr));
            Assert.AreEqual(0, IndexOfHelper(-1, 3, new LastIndexOfAny1(StringExtensions.LastIndexOfAny), "ddddzzzzzzzzzzzz", largeCharArrUpper));
            Assert.AreEqual(0, IndexOfHelper(-1, 3, new LastIndexOfAny1(StringExtensions.LastIndexOfAny), "kkkkzzzzzzzzzzzz", largeCharArrUpper));


            // LastIndexOfAny(string, char[], int, StringComparison)

            Assert.AreEqual(0, IndexOfExceptionHelper<ArgumentNullException>(new LastIndexOfAny2(StringExtensions.LastIndexOfAny), null, validCharArr, 0));
            Assert.AreEqual(0, IndexOfExceptionHelper<ArgumentNullException>(new LastIndexOfAny2(StringExtensions.LastIndexOfAny), "aaaa", null, 0));
            Assert.AreEqual(0, IndexOfExceptionHelper<ArgumentOutOfRangeException>(new LastIndexOfAny2(StringExtensions.LastIndexOfAny), "aaaa", validCharArr, -1));
            Assert.AreEqual(0, IndexOfExceptionHelper<ArgumentOutOfRangeException>(new LastIndexOfAny2(StringExtensions.LastIndexOfAny), "aaaa", validCharArr, 4));

            Assert.AreEqual(0, IndexOfHelper(-1, new LastIndexOfAny2(StringExtensions.LastIndexOfAny), "aaaa", emptyCharArr, 0));
            Assert.AreEqual(0, IndexOfHelper(3, new LastIndexOfAny2(StringExtensions.LastIndexOfAny), "aaaa", validCharArr, 3));
            Assert.AreEqual(0, IndexOfHelper(3, new LastIndexOfAny2(StringExtensions.LastIndexOfAny), "bbccbbcc", simpleCharArr, 3));
            Assert.AreEqual(0, IndexOfHelper(3, new LastIndexOfAny2(StringExtensions.LastIndexOfAny), "ccbbccbb", simpleCharArr, 3));
            Assert.AreEqual(0, IndexOfHelper(-1, 3, new LastIndexOfAny2(StringExtensions.LastIndexOfAny), "bbccBBCC", simpleCharArrUpper, 3));
            Assert.AreEqual(0, IndexOfHelper(-1, 3, new LastIndexOfAny2(StringExtensions.LastIndexOfAny), "ccbbCCBB", simpleCharArrUpper, 3));
            Assert.AreEqual(0, IndexOfHelper(-1, new LastIndexOfAny2(StringExtensions.LastIndexOfAny), "bbccdddd", notFoundCharArr, 3));
            // Testing loop folding pathway
            Assert.AreEqual(0, IndexOfHelper(3, new LastIndexOfAny2(StringExtensions.LastIndexOfAny), "ddddzzzzzzzzzzzd", largeCharArr, 14));
            Assert.AreEqual(0, IndexOfHelper(3, new LastIndexOfAny2(StringExtensions.LastIndexOfAny), "kkkkzzzzzzzzzzzk", largeCharArr, 14));
            Assert.AreEqual(0, IndexOfHelper(-1, 3, new LastIndexOfAny2(StringExtensions.LastIndexOfAny), "ddddzzzzzzzzzzzD", largeCharArrUpper, 14));
            Assert.AreEqual(0, IndexOfHelper(-1, 3, new LastIndexOfAny2(StringExtensions.LastIndexOfAny), "kkkkzzzzzzzzzzzK", largeCharArrUpper, 14));


            // LastIndexOfAny(string, char[], int, int, StringComparison)

            Assert.AreEqual(0, IndexOfExceptionHelper<ArgumentNullException>(new LastIndexOfAny3(StringExtensions.LastIndexOfAny), null, validCharArr, 0, 0));
            Assert.AreEqual(0, IndexOfExceptionHelper<ArgumentNullException>(new LastIndexOfAny3(StringExtensions.LastIndexOfAny), "aaaa", null, 0, 0));
            Assert.AreEqual(0, IndexOfExceptionHelper<ArgumentOutOfRangeException>(new LastIndexOfAny3(StringExtensions.LastIndexOfAny), "aaaa", validCharArr, -1, 1));
            Assert.AreEqual(0, IndexOfExceptionHelper<ArgumentOutOfRangeException>(new LastIndexOfAny3(StringExtensions.LastIndexOfAny), "aaaa", validCharArr, 2, 4));
            Assert.AreEqual(0, IndexOfExceptionHelper<ArgumentOutOfRangeException>(new LastIndexOfAny3(StringExtensions.LastIndexOfAny), "aaaa", validCharArr, 4, 0));
            Assert.AreEqual(0, IndexOfExceptionHelper<ArgumentOutOfRangeException>(new LastIndexOfAny3(StringExtensions.LastIndexOfAny), "aaaa", validCharArr, 1, -1));

            Assert.AreEqual(0, IndexOfHelper(-1, new LastIndexOfAny3(StringExtensions.LastIndexOfAny), "aaaa", emptyCharArr, 3, 4));
            Assert.AreEqual(0, IndexOfHelper(-1, new LastIndexOfAny3(StringExtensions.LastIndexOfAny), "aaaa", validCharArr, 3, 0));
            Assert.AreEqual(0, IndexOfHelper(-1, new LastIndexOfAny3(StringExtensions.LastIndexOfAny), "aaaa", validCharArr, 0, 0));
            Assert.AreEqual(0, IndexOfHelper(0, new LastIndexOfAny3(StringExtensions.LastIndexOfAny), "addd", validCharArr, 3, 4));
            Assert.AreEqual(0, IndexOfHelper(7, new LastIndexOfAny3(StringExtensions.LastIndexOfAny), "bbccbbccbbcc", simpleCharArr, 7, 4));
            Assert.AreEqual(0, IndexOfHelper(7, new LastIndexOfAny3(StringExtensions.LastIndexOfAny), "ccbbccbbccbb", simpleCharArr, 7, 4));
            Assert.AreEqual(0, IndexOfHelper(-1, 7, new LastIndexOfAny3(StringExtensions.LastIndexOfAny), "BBCCbbccBBCC", simpleCharArrUpper, 7, 4));
            Assert.AreEqual(0, IndexOfHelper(-1, 7, new LastIndexOfAny3(StringExtensions.LastIndexOfAny), "CCBBccbbCCBB", simpleCharArrUpper, 7, 4));
            Assert.AreEqual(0, IndexOfHelper(-1, new LastIndexOfAny3(StringExtensions.LastIndexOfAny), "ddddbbccdddd", notFoundCharArr, 7, 4));
            // Testing loop folding pathway
            Assert.AreEqual(0, IndexOfHelper(3, new LastIndexOfAny3(StringExtensions.LastIndexOfAny), "ddddzzzzzzzzzzzd", largeCharArr, 14, 12));
            Assert.AreEqual(0, IndexOfHelper(3, new LastIndexOfAny3(StringExtensions.LastIndexOfAny), "kkkkzzzzzzzzzzzk", largeCharArr, 14, 12));
            Assert.AreEqual(0, IndexOfHelper(-1, 3, new LastIndexOfAny3(StringExtensions.LastIndexOfAny), "ddddzzzzzzzzzzzD", largeCharArrUpper, 14, 12));
            Assert.AreEqual(0, IndexOfHelper(-1, 3, new LastIndexOfAny3(StringExtensions.LastIndexOfAny), "kkkkzzzzzzzzzzzK", largeCharArrUpper, 14, 12));
        }

        [Test]
        public void LastIndexOfNotAny()
        {
            char[] emptyCharArr = new char[0];
            char[] validCharArr = new char[] { 'a' };
            char[] simpleCharArr = new char[] { 'b', 'c' };
            char[] simpleCharArrUpper = new char[] { 'B', 'C' };
            char[] notFoundCharArr = new char[] { 'a', 'b', 'c' };


            // LastIndexOfNotAny(string, char[])

            Assert.Throws<ArgumentNullException>(new TestDelegate(() => StringExtensions.LastIndexOfNotAny(null, validCharArr)));
            Assert.Throws<ArgumentNullException>(new TestDelegate(() => StringExtensions.LastIndexOfNotAny("aaaa", (char[])null)));

            Assert.AreEqual(3, StringExtensions.LastIndexOfNotAny("aaaa", emptyCharArr));
            Assert.AreEqual(3, StringExtensions.LastIndexOfNotAny("aaaabbbbcccc", simpleCharArr));
            Assert.AreEqual(7, StringExtensions.LastIndexOfNotAny("ccccbbbb", simpleCharArrUpper));
            Assert.AreEqual(-1, StringExtensions.LastIndexOfNotAny("aaaabbbbcccc", notFoundCharArr));


            // LastIndexOfNotAny(string, char[], int)

            Assert.Throws<ArgumentNullException>(new TestDelegate(() => StringExtensions.LastIndexOfNotAny(null, validCharArr, 0)));
            Assert.Throws<ArgumentNullException>(new TestDelegate(() => StringExtensions.LastIndexOfNotAny("aaaa", (char[])null, 0)));
            Assert.Throws<ArgumentOutOfRangeException>(new TestDelegate(() => StringExtensions.LastIndexOfNotAny("aaaa", validCharArr, -1)));
            Assert.Throws<ArgumentOutOfRangeException>(new TestDelegate(() => StringExtensions.LastIndexOfNotAny("aaaa", validCharArr, 4)));

            Assert.AreEqual(3, StringExtensions.LastIndexOfNotAny("aaaa", emptyCharArr, 3));
            Assert.AreEqual(-1, StringExtensions.LastIndexOfNotAny("aaaa", validCharArr, 3));
            Assert.AreEqual(1, StringExtensions.LastIndexOfNotAny("aaccbbaaccbb", simpleCharArr, 5));
            Assert.AreEqual(3, StringExtensions.LastIndexOfNotAny("bbccBBCC", simpleCharArrUpper, 3));
            Assert.AreEqual(-1, StringExtensions.LastIndexOfNotAny("bbccdddd", notFoundCharArr, 3));


            // LastIndexOfNotAny(string, char[], int, int)

            Assert.Throws<ArgumentNullException>(new TestDelegate(() => StringExtensions.LastIndexOfNotAny(null, validCharArr, 0, 0)));
            Assert.Throws<ArgumentNullException>(new TestDelegate(() => StringExtensions.LastIndexOfNotAny("aaaa", (char[])null, 0, 0)));
            Assert.Throws<ArgumentOutOfRangeException>(new TestDelegate(() => StringExtensions.LastIndexOfNotAny("aaaa", validCharArr, -1, 1)));
            Assert.Throws<ArgumentOutOfRangeException>(new TestDelegate(() => StringExtensions.LastIndexOfNotAny("aaaa", validCharArr, 2, 4)));
            Assert.Throws<ArgumentOutOfRangeException>(new TestDelegate(() => StringExtensions.LastIndexOfNotAny("aaaa", validCharArr, 4, 0)));
            Assert.Throws<ArgumentOutOfRangeException>(new TestDelegate(() => StringExtensions.LastIndexOfNotAny("aaaa", validCharArr, 4, -1)));

            Assert.AreEqual(3, StringExtensions.LastIndexOfNotAny("aaaa", emptyCharArr, 3, 4));
            Assert.AreEqual(-1, StringExtensions.LastIndexOfNotAny("aaaa", emptyCharArr, 0, 0));
            Assert.AreEqual(5, StringExtensions.LastIndexOfNotAny("aaaayybbccaaaa", simpleCharArr, 9, 6));
            Assert.AreEqual(9, StringExtensions.LastIndexOfNotAny("AAAAaabbccAAAA", simpleCharArrUpper, 9, 6));
            Assert.AreEqual(-1, StringExtensions.LastIndexOfNotAny("ddddbbccdddd", notFoundCharArr, 7, 4));
        }


        //--- Private Methods ---

        int IndexOfHelper(int expected, Delegate method, params object[] args)
        {
            return IndexOfHelper(expected, expected, method, args);
        }

        int IndexOfHelper(int expectedForCaseSensitive, int expectedForIgnoreCase, Delegate method, params object[] args)
        {
            object[] newArgs = new object[args.Length + 1];
            args.CopyTo(newArgs, 0);

            newArgs[newArgs.Length - 1] = StringComparison.Ordinal;
            if (expectedForCaseSensitive != (int)method.DynamicInvoke(newArgs))
                return 1;

            newArgs[newArgs.Length - 1] = StringComparison.CurrentCulture;
            if (expectedForCaseSensitive != (int)method.DynamicInvoke(newArgs))
                return 2;

            newArgs[newArgs.Length - 1] = StringComparison.InvariantCulture;
            if (expectedForCaseSensitive != (int)method.DynamicInvoke(newArgs))
                return 3;

            newArgs[newArgs.Length - 1] = StringComparison.OrdinalIgnoreCase;
            if (expectedForIgnoreCase != (int)method.DynamicInvoke(newArgs))
                return 4;

            newArgs[newArgs.Length - 1] = StringComparison.CurrentCultureIgnoreCase;
            if (expectedForIgnoreCase != (int)method.DynamicInvoke(newArgs))
                return 5;

            newArgs[newArgs.Length - 1] = StringComparison.InvariantCultureIgnoreCase;
            if (expectedForIgnoreCase != (int)method.DynamicInvoke(newArgs))
                return 6;

            return 0;
        }

        int IndexOfExceptionHelper<T>(Delegate method, params object[] args)
            where T:Exception
        {
            object[] newArgs = new object[args.Length + 1];
            args.CopyTo(newArgs, 0);

            newArgs[newArgs.Length - 1] = StringComparison.Ordinal;
            try
            {
                method.DynamicInvoke(newArgs);
                return 1;
            }
            catch (TargetInvocationException ex)
            {
                if (!(ex.InnerException is T))
                    return 1;
            }
            catch
            {
                return 1;
            }

            newArgs[newArgs.Length - 1] = StringComparison.CurrentCulture;
            try
            {
                method.DynamicInvoke(newArgs);
                return 2;
            }
            catch (TargetInvocationException ex)
            {
                if (!(ex.InnerException is T))
                    return 2;
            }
            catch
            {
                return 2;
            }

            newArgs[newArgs.Length - 1] = StringComparison.InvariantCulture;
            try
            {
                method.DynamicInvoke(newArgs);
                return 3;
            }
            catch (TargetInvocationException ex)
            {
                if (!(ex.InnerException is T))
                    return 3;
            }
            catch
            {
                return 3;
            }

            newArgs[newArgs.Length - 1] = StringComparison.OrdinalIgnoreCase;
            try
            {
                method.DynamicInvoke(newArgs);
                return 4;
            }
            catch (TargetInvocationException ex)
            {
                if (!(ex.InnerException is T))
                    return 4;
            }
            catch
            {
                return 4;
            }

            newArgs[newArgs.Length - 1] = StringComparison.CurrentCultureIgnoreCase;
            try
            {
                method.DynamicInvoke(newArgs);
                return 5;
            }
            catch (TargetInvocationException ex)
            {
                if (!(ex.InnerException is T))
                    return 5;
            }
            catch
            {
                return 5;
            }

            newArgs[newArgs.Length - 1] = StringComparison.InvariantCultureIgnoreCase;
            try
            {
                method.DynamicInvoke(newArgs);
                return 6;
            }
            catch (TargetInvocationException ex)
            {
                if (!(ex.InnerException is T))
                    return 6;
            }
            catch
            {
                return 6;
            }

            return 0;
        }

        //--- Nested Types ---
        delegate int IndexOf1(string s, char c, StringComparison comparisonType);
        delegate int IndexOf2(string s, char c, int startIndex, StringComparison comparisonType);
        delegate int IndexOf3(string s, char c, int startIndex, int count, StringComparison comparisonType);
        delegate int IndexOfAny1(string s, char[] anyOf, StringComparison comparisonType);
        delegate int IndexOfAny2(string s, char[] anyOf, int startIndex, StringComparison comparisonType);
        delegate int IndexOfAny3(string s, char[] anyOf, int startIndex, int count, StringComparison comparisonType);
        delegate int IndexOfAny4(string s, string[] anyOf, StringComparison comparisonType);
        delegate int IndexOfAny5(string s, string[] anyOf, int startIndex, StringComparison comparisonType);
        delegate int IndexOfAny6(string s, string[] anyOf, int startIndex, int count, StringComparison comparisonType);
        delegate int IndexOfNotAny1(string s, char[] anyOf, StringComparison comparisonType);
        delegate int IndexOfNotAny2(string s, char[] anyOf, int startIndex, StringComparison comparisonType);
        delegate int IndexOfNotAny3(string s, char[] anyOf, int startIndex, int count, StringComparison comparisonType);
        delegate int LastIndexOfAny1(string s, char[] anyOf, StringComparison comparisonType);
        delegate int LastIndexOfAny2(string s, char[] anyOf, int startIndex, StringComparison comparisonType);
        delegate int LastIndexOfAny3(string s, char[] anyOf, int startIndex, int count, StringComparison comparisonType);
    }
}
