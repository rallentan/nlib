// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)

using System;
using System.Diagnostics;
using System.Text;
using System.Collections.Generic;

namespace NLib
{
    /// <summary>
    /// Provides a set of methods to quickly interface with the command shell.
    /// </summary>
    public static class Shell
    {
        //--- Public Static Methods ---

        /// <summary>
        /// Executes the specified application file, and waits for that process
        /// to exit before returning.
        /// </summary>
        /// <param name="commandWithoutArgs">
        /// An application with which to start a process.
        /// </param>
        /// <returns>the exit code of the process.</returns>
        /// <exception cref="System.InvalidOperationException">
        /// No file name was specified.</exception>
        /// <exception cref="System.ComponentModel.Win32Exception">
        /// There was an error in opening the associated file.</exception>
        public static int Execute(string commandWithoutArgs)
        {
            return Execute(commandWithoutArgs, (string)null);
        }

        /// <summary>
        /// Executes the specified application file, and waits for that process
        /// to exit before returning. A parameter specifies arguments to pass
        /// to the process.
        /// </summary>
        /// <param name="command">An application with which to start a process.</param>
        /// <param name="arguments">
        /// Command-line arguments to pass to the application when the process starts.
        /// </param>
        /// <returns>the exit code of the process.</returns>
        /// <exception cref="System.InvalidOperationException">
        /// No file name was specified.</exception>
        /// <exception cref="System.ComponentModel.Win32Exception">
        /// There was an error in opening the associated file.</exception>
        public static int Execute(string command, params string[] arguments)
        {
            return Execute(command, ToCommandLine(arguments));
        }

        /// <summary>
        /// Executes the specified application file, and waits for that process
        /// to exit before returning. A parameter specifies arguments to pass
        /// to the process.
        /// </summary>
        /// <param name="command">An application with which to start a process.</param>
        /// <param name="arguments">
        /// Command-line arguments to pass to the application when the process starts.
        /// </param>
        /// <returns>the exit code of the process.</returns>
        /// <exception cref="System.InvalidOperationException">
        /// No file name was specified.</exception>
        /// <exception cref="System.ComponentModel.Win32Exception">
        /// There was an error in opening the associated file.</exception>
        public static int Execute(string command, string arguments)
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo(command, arguments);
            processStartInfo.UseShellExecute = false;

            Process process = new Process();
            process.StartInfo = processStartInfo;
            process.Start();

            process.WaitForExit();
            return process.ExitCode;
        }

        /// <summary>
        /// Executes the specified application file, and waits for that process
        /// to exit before returning.
        /// </summary>
        /// <param name="commandWithoutArgs">An application with which to start a process.</param>
        /// <returns>the standard output of the process.</returns>
        /// <exception cref="System.InvalidOperationException">
        /// No file name was specified.</exception>
        /// <exception cref="System.ComponentModel.Win32Exception">
        /// There was an error in opening the associated file.</exception>
        public static string ExecuteRedirected(string commandWithoutArgs)
        {
            return ExecuteRedirected(commandWithoutArgs, (string)null);
        }

        /// <summary>
        /// Executes the specified application file, and waits for that process
        /// to exit before returning. A parameter specifies arguments to pass
        /// to the process.
        /// </summary>
        /// <param name="command">An application with which to start a process.</param>
        /// <param name="arguments">
        /// Command-line arguments to pass to the application when the process starts.
        /// </param>
        /// <returns>the standard output of the process.</returns>
        /// <exception cref="System.InvalidOperationException">
        /// No file name was specified.</exception>
        /// <exception cref="System.ComponentModel.Win32Exception">
        /// There was an error in opening the associated file.</exception>
        public static string ExecuteRedirected(string command, params string[] arguments)
        {
            return ExecuteRedirected(command, ToCommandLine(arguments));
        }

        /// <summary>
        /// Executes the specified application file, and waits for that process
        /// to exit before returning. A parameter specifies arguments to pass
        /// to the process.
        /// </summary>
        /// <param name="command">An application with which to start a process.</param>
        /// <param name="arguments">
        /// Command-line arguments to pass to the application when the process starts.
        /// </param>
        /// <returns>the standard output of the process.</returns>
        /// <exception cref="System.InvalidOperationException">
        /// No file name was specified.</exception>
        /// <exception cref="System.ComponentModel.Win32Exception">
        /// There was an error in opening the associated file.</exception>
        public static string ExecuteRedirected(string command, string arguments)
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo(command, arguments);

            processStartInfo.RedirectStandardOutput = true;
            processStartInfo.UseShellExecute = false;

            Process process = new Process();
            process.StartInfo = processStartInfo;
            process.Start();

            process.WaitForExit();
            return process.StandardOutput.ReadToEnd();
        }

        /// <summary>
        /// Combines two command-line parameter strings.
        /// </summary>
        /// <param name="parameterA">The first command-line parameter.</param>
        /// <param name="parameterB">
        /// The second command-line parameter.</param>
        /// <returns>
        /// A string containing the combined parameters. If one of the
        /// specified parameters is a zero-length string, this method returns
        /// the other parameter. Each parameter containing whitespace which is
        /// not already quoted is surrounded in double quotes.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// parameterA or parameterB is null.</exception>
        public static string Combine(string parameterA, string parameterB)
        {
            return parameterA + ' ' + parameterB;
        }

        /// <summary>
        /// Combines two arguments into a single command-line string.
        /// </summary>
        /// <param name="arguments">
        /// The command-line arguments to combine.
        /// </param>
        /// <returns>
        /// A string containing the combined parameters. If one of the
        /// specified parameters is a zero-length string, this method returns
        /// the other parameter. Each parameter containing whitespace which is
        /// not already quoted is surrounded in double quotes.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// One of the elements in arguments is null.</exception>
        public static string ToCommandLine(params string[] arguments)
        {
            int argsLength = arguments.Length;

            if (argsLength == 0)
                return string.Empty;

            StringBuilder commandLine = new StringBuilder();

            char[] commandLineSeparators = new char[CommandLineSeparators.Count];
            CommandLineSeparators.CopyTo(commandLineSeparators, 0);
            
            CommandLineAppendInternal(commandLine, arguments[0], commandLineSeparators);

            for (int i = 1; i < argsLength; i++)
            {
                commandLine.Append(' ');
                CommandLineAppendInternal(commandLine, arguments[i], commandLineSeparators);
            }

            return commandLine.ToString();
        }

        //--- Public Static Properties ---

        /// <summary>
        /// Gets a list of all Unicode characters that can separate arguments on a command-line
        /// on the currently running platform.
        /// </summary>
        public static IList<char> CommandLineSeparators
        {
            get
            {
                switch ((int)Environment.OSVersion.Platform)
                {
                    case 128:   // Unix/MacOS X in framework v1.0 and v1.1
                    case 4:     // Unix/MacOS X in framework v2.0, and Unix in framework v4.0
                    case 6:     // MacOS X in framework v4.0
                    default:
                        return CharExtensions.NonLineBreakingWhiteSpaceLatin1;
                }
            }
        }

        //--- Private Static Methods ---

        static void CommandLineAppendInternal(StringBuilder commandLine, string arg, char[] commandLineSeparators)
        {
            if (arg.ContainsAny(commandLineSeparators))
            {
                commandLine.Append('"');
                commandLine.Append(arg);
                commandLine.Append('"');
            }
            else
            {
                commandLine.Append(arg);
            }
        }
    }
}
