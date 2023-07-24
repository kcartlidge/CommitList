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
    class Program
    {
        private const string VERSION = "v1.0.0";

        static void Main(string[] args)
        {
            try
            {
                Display.ShowLicense(VERSION);
                Scanner.Scan(new Config(args));
            }
            catch (Exception ex)
            {
                Display.Error(ex);
            }
        }
    }
}
