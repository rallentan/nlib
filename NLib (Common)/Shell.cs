// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)

using System;
using System.Diagnostics;
using System.Text;
using System.Collections.Generic;

namespace NLib
{
    public static class Shell
    {
        //--- Public Static Methods ---

        public static int Execute(string commandWithoutArgs)
        {
            return Execute(commandWithoutArgs, (string)null);
        }

        public static int Execute(string command, params string[] args)
        {
            return Execute(command, ToCommandLine(args));
        }

        public static int Execute(string command, string args)
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo(command, args);
            processStartInfo.UseShellExecute = false;

            Process process = new Process();
            process.StartInfo = processStartInfo;
            process.Start();

            process.WaitForExit();
            return process.ExitCode;
        }

        public static string ExecuteRedirected(string commandWithoutArgs)
        {
            return ExecuteRedirected(commandWithoutArgs, (string)null);
        }

        public static string ExecuteRedirected(string command, params string[] args)
        {
            return ExecuteRedirected(command, ToCommandLine(args));
        }

        public static string ExecuteRedirected(string command, string args)
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo(command, args);

            processStartInfo.RedirectStandardOutput = true;
            processStartInfo.UseShellExecute = false;

            Process process = new Process();
            process.StartInfo = processStartInfo;
            process.Start();

            process.WaitForExit();
            return process.StandardOutput.ReadToEnd();
        }

        public static string Join(string parameterA, string parameterB)
        {
            return parameterA + ' ' + parameterB;
        }

        public static string ToCommandLine(params string[] args)
        {
            int argsLength = args.Length;

            if (argsLength == 0)
                return string.Empty;

            StringBuilder commandLine = new StringBuilder();

            char[] commandLineSeparators = new char[CommandLineSeparators.Count];
            CommandLineSeparators.CopyTo(commandLineSeparators, 0);
            
            CommandLineAppendInternal(commandLine, args[0], commandLineSeparators);

            for (int i = 1; i < argsLength; i++)
            {
                commandLine.Append(' ');
                CommandLineAppendInternal(commandLine, args[i], commandLineSeparators);
            }

            return commandLine.ToString();
        }

        //--- Public Static Properties ---

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
