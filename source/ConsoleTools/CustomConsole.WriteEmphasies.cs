﻿// ConsoleTools
// Copyright (C) 2017-2018 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;

namespace DustInTheWind.ConsoleTools
{
    public partial class CustomConsole
    {
        public static void WriteEmphasies(string text)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = EmphasiesColor;
            Console.Write(text);
            Console.ForegroundColor = oldColor;
        }

        public static void WriteEmphasies(string format, params object[] arg)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = EmphasiesColor;
            Console.Write(format, arg);
            Console.ForegroundColor = oldColor;
        }

        public static void WriteEmphasies(object o)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = EmphasiesColor;
            Console.Write(o);
            Console.ForegroundColor = oldColor;
        }

        public static void WriteLineEmphasies(string text)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = EmphasiesColor;
            Console.WriteLine(text);
            Console.ForegroundColor = oldColor;
        }

        public static void WriteLineEmphasies(string format, params object[] arg)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = EmphasiesColor;
            Console.WriteLine(format, arg);
            Console.ForegroundColor = oldColor;
        }

        public static void WriteLineEmphasies(object o)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = EmphasiesColor;
            Console.WriteLine(o);
            Console.ForegroundColor = oldColor;
        }
    }
}