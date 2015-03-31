# Introduction #

This is a list of featured classes and members provided by NLib.  This is not an exhaustive list.  For more details on the NLib API, see the the NLib.chm file.


# Classes #

  * **ArrayExtensions** - Provides a set of extension methods for manipulating arrays.
  * **ByteArrayExtensions** - Provides a set of extension methods tailored to manipulating byte arrays.
  * **ByteExtensions, Int16Extensions, Int32Extensions, Int64Extensions, UInt16Extensions, UInt32Extensions, UInt64Extensions** - Provides a set of extension methods for manipulating integrals.
  * **CharExtensions** - Provides a set of extension methods for manipulating char primitives.  Useful for parsing.
  * **SimpleThread** - Provides the simplest implementation of thread creation and management, with additional features for debugging.  See the detailed documentation for an example of how SimpleThread can be used to communicate narrowly-scoped type-checked data between threads.
  * **GenericDelegates** - Provides .NET 4's generic delegates for .NET 2 and .NET 3.5 projects.
  * **InterlockedHelper** - Provides a set of methods to help in manipulating multithreaded variables.
  * **OperationQueueThread** - Acts as a background thread which receives operations to complete from the main thread in order.  The operations are executed one at a time until the queue is empty.
  * **RandomExtensions** - Provides a set of extension methods to simplify the use of the class Random, namely the NextBoolean method.
  * **Range** - Similar to the Rectangle class, but works in fewer dimensions.  Useful for selecting a region of an array or string.  Range is similar to Point in structure, but Range's properties are better named for representing a one-dimensional region, and contains additional methods intuitive of regions.
  * **Shell** - Provides a set of methods to quickly interface with the command shell.
  * **StringExtensions** - Provides a set of extension methods for manipulating and searching strings.
  * **StringHelper** - Provides a set of helper methods for obtaining information about existing strings.
  * **StringParser** - Replaces the StringReader class.  Provides a set of methods for reading a string in various ways to reduce the need for surrounding logic.
  * **ControlExtensions** - Provides a set of extension methods to the Control class.  Some of the methods are used to move the control relative to other controls, making it easy to anchor controls in many different ways.

# Members #

#### ArrayExtensions ####
  * SubArray - Returns an array containing only the elements in the specified range of the source array.

#### ByteArrayExtensions ####
  * CompareTo - Compares two byte arrays for equality.  This method is highly-optimized for performance.

#### ByteExtensions ####
  * HighNibble - Returns the high-order nibble (4 bits) of the source byte.
  * LowNibble - Returns the low-order nibble (4 bits) of the source byte.
  * RotateRight - Rotates the bits of the source byte "right" by the specified number of places.
  * RotateLeft - Rotates the bits of the source byte "left" by the specified number of places.

#### Int16Extensions, UInt16Extensions ####
  * HighByte - Returns the high-byte of the source value.
  * LowByte - Returns the low-byte of the source value.
  * RotateRight - Rotates the bits of the source value "right" by the specified number of places.
  * RotateLeft - Rotates the bits of the source value "left" by the specified number of places.

#### Int32Extensions, UInt32Extensions ####
  * HighWord - Returns the high-word (16 bits) of the source value.
  * LowWord - Returns the low-word (16 bits) of the source value.
  * RotateRight - Rotates the bits of the source value "right" by the specified number of places.
  * RotateLeft - Rotates the bits of the source value "left" by the specified number of places.

#### Int64Extensions, UInt64Extensions ####
  * HighWord - Returns the high-dword (32 bits) of the source value.
  * LowWord - Returns the low-dword (32 bits) of the source value.
  * RotateRight - Rotates the bits of the source value "right" by the specified number of places.
  * RotateLeft - Rotates the bits of the source value "left" by the specified number of places.

#### CharExtensions ####
  * Equals - Determines if two characters are equal while attempting to allow case-insensitivity.
  * IsDigit - Mimicks the Char.IsDigit method, but implements it as an extension method.
  * IsHexDigit - Mimicks the Char.IsHexDigit method, but implements it as an extension method.
  * IsInMap - Quickly checks if a character falls within the specified table of characters.  In some cases, this can provide a performance improvement over checking each character.
  * IsNonLineBreakingWhiteSpaceLatin1 - Determines if a character is a white space character, excluding line-breaking characters.  This method is not compliant with all cultures.  Use with consideration.
  * IsWhiteSpace - Mimicks the Char.IsWhiteSpace method, but implements it as an extension method.
  * ToLower - Mimicks the Char.ToLower method, but implements it as an extension method.
  * ToLowerInvariant - Mimicks the Char.ToLowerInvariant method, but implements it as an extension method.
  * ToUpper - Mimicks the Char.ToUpper method, but implements it as an extension method.
  * ToUpperInvariant - Mimicks the Char.ToUpperInvariant method, but implements it as an extension method.
  * WhiteSpaceLatin1 - A property which returns a complete list of white space characters.  These characters do not apply to all cultures.  Use with consideration.
  * NonLineBreakingWhiteSpaceLatin1 - A property which returns a list of white space characters which are not used to indicate a new line.  These characters do not apply to all cultures.  Use with consideration.

#### SimpleThread ####
  * BeginInvoke - Starts a new thread calling the specified method.  The method should typically be an anonymous delegate.  See the detailed documentation for an example of how SimpleThread can be used to communicate narrowly-scoped type-checked data between threads.
  * DisableThreading - A property which allows threading to be disabled, causing all calls to SimpleThread.BeginInvoke to be executed synchronously.  This can aid in debugging cases where other threads interrupt the debugging process.

#### ExtensionAttribute ####
  * Allows extension methods to be used with the .NET 2 Framework.  This class will prevent .NET 3.5 or .NET 4 projects from compiling.

#### GenericDelegates ####
  * Action<...> - Provides the full set of generic Action delgates for older versions of .NET.
  * Func<...> - Provides the full set of generic Func delegates for older versions of .NET.

#### InterlockedHelper ####
  * Increment/Decrement - Similar to Interlocked.Increment/Decrement, but receives a UInt32 as a parameter, instead of an Int32.

#### OperationQueueThread ####
  * EnqueueOperation - Adds a method to the queue to be called when all prior operations in the queue are completed or otherwise removed.
  * EnqueueOperationWait - Adds a method to the queue, but does not return until the operation is completed.  This is useful when an operation must be run in sequence, but you wish to wait for the result.
  * Clear - Clears the queue removing all operations that have not yet been started.
  * ClearWait - Clears the queue, and waits for any currently running operations to finish before returning.

#### RandomExtensions ####
  * NextBoolean - Returns the next value in the sequence as a boolean value.

Range ====
  * GetBoundingRange - Gets the smallest Range that encompasses all of the specified ranges.
  * StartPos - A property which gets or sets the starting position of the range.
  * Length - A property which gets or sets the length of the range.
  * EndPos - A property which gets the end position of the range (i.e. StartPos + Length).

#### Shell ####
  * Execute - Executes the specified command-line.  The 'args' can be passed in list or string format.  This method waits until the process exits and returns its exit code.
  * ExecuteRedirected - Executes the specified program.  This process waits until the specified program exits, and then returns its standard output.
  * Join - Concatenates two command-line argument strings into a single command-line string.
  * ToCommandLine - Takes an 'args' array and converts it into single command-line string.

#### StringExtensions ####
  * CompareTo - Takes a StringComparison parameter with which to compare two strings.
  * Contains - Provides overloads for use with Char's.
  * ContainsAny - Allows you to optimally search a string several different characters at once and return the first match.
  * Replace - Mimicks String.Replace, but allows the option to specify a StringComparison parameter for the search string.
  * IndexOfAny - Returns the index of the first matching string from a specified array of strings.
  * IndexOfNotAny - Returns the first index in the source string that does not match any of the strings in the given array of strings.
  * LastIndexOfAny - Returns the index of the last matching string from a specified array of strings.

#### StringHelper ####
  * GetLineCount - Returns the number of lines in a string.
  * LineNumberOfIndex - Returns the line number the specified index lies on.
  * FirstIndexOfLine - Returns the index number of the first character on the specified line number.

#### StringParser ####
  * Consume - Simultaneously compares the source string while advancing the stream if necessary.
  * GetCurrentLine - Returns the current line of the string that the stream is positioned at.
  * Peek - Returns the next specified number of characters without advancing the stream.
  * ReadChar - Returns the next character in the stream without allocating a new string object.
  * ReadTo - Reads the stream until it encounters the specified string, then returns the string leading up to the match.
  * ReadToAnyOf - Similar to ReadTo, but will stop when any of several strings match.
  * RewindTo - Rewinds the stream backward until the specified character is found.
  * RewindToAnyOf - Similar to RewindTo, but will stop when any of several characters match.
  * SetRange - Allows StringParser to only work within the specified range of the source string.
  * Skip - Advances the stream the specified number of characters forward without returning or allocating any strings.
  * SkipLine - Skips to the next line.
  * SkipTo - Similar to ReadTo, but doesn't allocate any strings.
  * SkipToAnyOf - Similar to ReadToAnyOf, but doesn't allocate any strings.
  * SkipWhile - Advances the stream until none of the specified characters match.
  * SkipWhileAnyOf
  * StartsWith
  * Position - Gets or sets the current position of the stream within the source string.

#### ControlExtensions ####
  * GetMarginRectange - Returns a Rectangle representing the margins outlining the control within its container.
  * SnapToParent - Moves the control to a position relative to its container.
  * SnapToSibling - Moves the control to a position relative to a sibling control.
  * StretchToChild - Resizes and moves the control such that only the specified side of the control is moved to a position relative to a child control.
  * StretchToParent - Resizes and moves the control such that only the specified side of the control is moved to a position relative to the specified side of its container.
  * StretchToSibling - Resizes and moves the control such that only the specified side of the control is moved to a position relative to a sibling control.