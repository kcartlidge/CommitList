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
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CommitList
{
    public static class Scanner
    {
        public static void Scan(Config config)
        {
            if (config.Values.Count != 1) throw new Exception("Expected exactly one path ('.' is fine)");

            var root = Path.GetFullPath(config.Values.First());
            if (!Directory.Exists(root)) throw new Exception($"Unknown path {root}");

            var extraParams = new List<string>();
            var showCommand = config.Flags.Contains("showcommand");
            var goDeep = config.Flags.Contains("deep");

            if (config.Options.ContainsKey("days"))
            {
                var days = config.Options["days"];
                Console.WriteLine($"Up to {Display.Bold(days + " day(s) ago")}");
                extraParams.Add(GitCommand.WithinLast(days));
            }
            else
            {
                Console.WriteLine($"Since {Display.Bold("yesterday")}");
                extraParams.Add(GitCommand.SinceYesterday);
            }

            if (config.Options.ContainsKey("author"))
            {
                var author = config.Options["author"];
                Console.WriteLine($"Author contains {Display.Bold(author)}");
                extraParams.Add(GitCommand.ForAuthor(author));
            }

            var deepText = goDeep ? " (deep)" : "";
            var first = true;
            Console.WriteLine($"Scanning " + Display.Bold(root + deepText));
            if (!showCommand) Console.WriteLine();
            DoFolder(root, ref first, extraParams, showCommand, goDeep);
            Console.WriteLine();
        }

        private static void DoFolder(
            string root,
            ref bool first,
            List<string> extraParams,
            bool showCommand,
            bool goDeep)
        {
            var folders = new DirectoryInfo(root);
            foreach (var folder in folders.EnumerateDirectories())
            {
                if (Directory.Exists(Path.Combine(folder.FullName, ".git")))
                {
                    var cmd = new GitCommand(
                        folder.FullName,
                        GitCommand.OneLineFormatted,
                        extraParams.ToArray()
                    );
                    if (first && showCommand)
                    {
                        Console.WriteLine($"Command: git {cmd.CommandArgs}");
                        Console.WriteLine();
                        first = false;
                    }
                    var output = cmd.Run();
                    if (output.StandardOutput.HasValue())
                    {
                        var gr = new GitResults(output.StandardOutput);
                        if (gr.Results.Any())
                        {
                            Console.WriteLine();
                            Console.WriteLine(Display.Bold(folder.Name));
                            foreach (var r in gr.Results)
                            {
                                Console.WriteLine(string.Format(
                                    "{0}  {1}  {2}  {3}",
                                    r.Commit.PadRight(gr.CommitWidth),
                                    Display.Bold(r.CommitDate.PadRight(gr.CommitDateWidth)),
                                    r.Author.PadRight(gr.AuthorWidth),
                                    r.Message.PadRight(gr.MessageWidth)
                                ));
                            }
                        }
                    }
                }
                else if (goDeep)
                {
                    DoFolder(folder.FullName, ref first, extraParams, showCommand, goDeep);
                }
            }
        }
    }
}
