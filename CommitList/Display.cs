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

namespace CommitList
{
    internal static class Display
    {
        public static void ShowLicense(string version)
        {
            Console.WriteLine();
            Console.WriteLine(Bold($"CommitList {version}"));
            Console.WriteLine();
            Console.WriteLine("Copyright(C) 2023 K Cartlidge.");
            Console.WriteLine("This program comes with ABSOLUTELY NO WARRANTY.");
            Console.WriteLine();
            Console.WriteLine("USAGE");
            Console.WriteLine("CommitList [-days=<days>] [-author=<author>] [--deep] [--showcommand] <folder>");
            Console.WriteLine();
            Console.WriteLine("  -days=7          inclusive number of previous days to show (defaults to yesterday)");
            Console.WriteLine("  -author=\"Dave\"   match part of the committer name");
            Console.WriteLine("  --deep           check within subfolders of non-repos");
            Console.WriteLine("  --showcommand    show the 'git log' command used");
            Console.WriteLine("  ~/Documents/Src  the parent folder holding your projects");
            Console.WriteLine();
        }

        public static void Error(Exception ex)
        {
            Console.WriteLine($"{Bold("ERROR:")} {ex.Message}");
            Console.WriteLine();
        }

        public static string Bold(string text)
        {
            // MOST Windows terminals by default don't do ANSI bold.
            // When they do it is often disabled by default.
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                return text;

            return $"\u001b[1m{text}\u001b[0m";
        }
    }
}
