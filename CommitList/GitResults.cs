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
    public class GitResults
    {
        public List<GitResult> Results = new List<GitResult>();
        public int CommitWidth = 0;
        public int CommitDateWidth = 0;
        public int AuthorWidth = 0;
        public int MessageWidth = 0;

        public GitResults(string rawText)
        {
            var lines = rawText.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                var bits = line.Split("||", 4, StringSplitOptions.RemoveEmptyEntries);
                if (bits.Length != 4) throw new Exception($"Unexpected: {line}");

                var gr = new GitResult
                {
                    Commit = bits[0].Trim(),
                    CommitDate = bits[1].Trim(),
                    Author = bits[2].Trim(),
                    Message = bits[3].Trim()
                };
                Results.Add(gr);
                CommitWidth = Math.Max(CommitWidth, gr.Commit.Length);
                CommitDateWidth = Math.Max(CommitDateWidth, gr.CommitDate.Length);
                AuthorWidth = Math.Max(AuthorWidth, gr.Author.Length);
                MessageWidth = Math.Max(MessageWidth, gr.Message.Length);
            }
        }
    }
}
