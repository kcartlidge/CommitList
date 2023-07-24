/*
COMMIT-LIST - get a nice and readable commit summary for multiple repos
Copyright(C) 2023 K Cartlidge

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.If not, see<http://www.gnu.org/licenses/>.
*/

using System;
using System.Diagnostics;

namespace CommitList
{
    public class GitCommand
    {
        public const string OneLineFormatted = "log --pretty=\"%h || %cd || %an || %s\" --date=format:\"%d-%b\"";
        public const string SinceYesterday = "--since=\"day.before.yesterday.midnight\"";
        public string CommandArgs { get; private set; }

        private Process process;

        public GitCommand(string workingFolder, string args, string[] extraParams)
        {
            CommandArgs = $"{args} {string.Join(" ", extraParams)}";
            process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                    FileName = "git",
                    WorkingDirectory = workingFolder
                }
            };
            process.StartInfo.Arguments = CommandArgs;
        }

        public GitCommandResult Run()
        {
            var result = new GitCommandResult
            {
                CommandArgs = this.CommandArgs,
                StandardOutput = string.Empty,
                StandardError = string.Empty
            };

            process.Start();
            result.StandardOutput = process.StandardOutput.ReadToEnd().Trim();
            result.StandardError = process.StandardError.ReadToEnd().Trim();
            process.WaitForExit();

            return result;
        }

        public static string WithinLast(string days)
        {
            return $"--since=\"{days}.days\"";
        }

        public static string ForAuthor(string author)
        {
            return $"--author=\"{author}\"";
        }
    }
}
