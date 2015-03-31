NLib aims to make common coding tasks easier by extending the core .NET Framework. NLib is free and open-source, licensed under the [BSL (Boost Software License)](License.md) which is similar to the MIT license but less restrictive. Here is a list of particularly useful features. For a more complete list of what NLib has to offer see the [Overview](Overview.md), or the documentation (in NLib.chm).

**Arrays**
  * Copy an array from the middle of an existing array.
  * High-performance byte array comparison.

**Integrals**
  * Bit operations (e.g. high/low nibble/byte/word/dword, rotate left/right).

**Threading**
  * Simple thread creation and management.
  * Interlocked helper methods for synchronization.
  * OperationQueueThread for queuing background operations.

**Shell**
  * Shell.Execute(...)
  * Shell.ExecuteRedirected(...) - Captures the output of the new process.
  * Shell.Join(...) - Similar to Path.Join; concatenates two strings into separate arguments in a single command-line.
  * Shell.ToCommandLine(...) - Generates a command-line from an args[.md](.md) array.

**Strings**
  * CompareTo(string, string, StringComparison)
  * Contains(string source, char value, bool ignoreCase)
  * Contains(string source, string value, StringComparison comparisonType)
  * ContainsAny(string source, char[.md](.md) anyOf, bool ignoreCase)
  * Replace(string source, string oldValue, string newValue, StringComparison comparisonType)
  * IndexOf(string source, char value, int startIndex, int count, bool ignoreCase)
  * IndexOfAny(string source, char[.md](.md) anyOf, int startIndex, int count, bool ignoreCase)
  * IndexOfAny(string source, string[.md](.md) anyOf, int startIndex, int count, StringComparison comparisonType)
  * IndexOfNotAny(string source, char[.md](.md) anyOf, int startIndex, int count, bool ignoreCase)
  * LastIndexOfAny(string source, char[.md](.md) anyOf, int startIndex, int count, bool ignoreCase)
  * LastIndexOfNotAny(string source, char[.md](.md) anyOf, int startIndex, int count, bool ignoreCase)
  * GetLineCount(string source, string newLineSequence)
  * LineNumberOfIndex(string source, int index, string newLineSequence)
  * FirstIndexOfLine(string source, int line, string newLineSequence)

**Classes**
  * StringParser - Replaces StringReader with much more powerful string parsing methods.
  * Range - A one-dimensional version of Rectangle.

**Forms**
  * Anchor functions to allow programmatic anchoring of controls, not only to the container, but to sibling controls as well, simplifying common GUI logic.

**Other**
  * Random extension method NextBoolean.

**XNA Classes & Extensions**
  * Extensions for Quaternion, Matrix, and more.
  * New classes to streamline common computations.