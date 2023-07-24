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

namespace CommitList
{
    public class Config
    {
        /// <summary>Arguments of the form --a=b</summary>
        public SortedList<string, string> Options { get; private set; }
        /// <summary>Arguments with no leading dashes</summary>
        public List<string> Values { get; private set; }
        /// <summary>Arguments of the form --a</summary>
        public List<string> Flags { get; private set; }

        /// <summary>
        /// Parses command line arguments.
        /// Single or double dashes are supported.
        /// See the example in the XML summary.
        /// </summary>
        /// <example>
        /// Flags:    --only-once
        /// Options:  --days=3
        /// Values:   something
        /// </example>
        /// <param name="args">As passed into your Program.Main static method.</param>
        public Config(string[] args)
        {
            Options = new SortedList<string, string>();
            Values = new List<string>();
            Flags = new List<string>();

            foreach (var arg in args)
            {
                if (arg.StartsWith("-", StringComparison.CurrentCulture))
                {
                    var item = arg.TrimStart('-').Trim();
                    if (item.HasNoValue()) throw new Exception("Unexpected dash (-)");
                    if (item.IndexOf('=') == -1)
                    {
                        Flags.Add(item.ToLowerInvariant());
                    }
                    else
                    {
                        var bits = item.Split('=', 2, StringSplitOptions.RemoveEmptyEntries);
                        bits[0] = bits[0].Trim().ToLowerInvariant();
                        if (bits.Length != 2)  throw new Exception($"Missing parameter for \"{bits[0]}\"");
                        Options.Add(bits[0], bits[1].Trim());
                    }
                }
                else
                {
                    Values.Add(arg);
                }
            }
        }

        public void Dump()
        {
            Console.WriteLine("OPTIONS");
            foreach (var option in Options)
            {
                Console.WriteLine($" =>  {Display.Bold(option.Key)} = {option.Value}");
            }
            Console.WriteLine("FLAGS");
            foreach (var flag in Flags)
            {
                Console.WriteLine($" =>  {Display.Bold(flag)}");
            }
            Console.WriteLine("VALUES");
            foreach (var value in Values)
            {
                Console.WriteLine($" =>  {value}");
            }
        }
    }
}
