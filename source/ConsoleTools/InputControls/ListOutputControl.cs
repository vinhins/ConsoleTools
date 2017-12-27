﻿// ConsoleTools
// Copyright (C) 2017 Dust in the Wind
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

using System.Collections.Generic;
using System.Text;

namespace DustInTheWind.ConsoleTools.InputControls
{
    public class ListOutputControl
    {
        public string Separator { get; set; } = ":";
        public int ItemsIndentation { get; set; } = 1;
        public string Bullet { get; set; } = "-";
        public int BulletSpace { get; set; } = 1;

        public void Write(string label, IEnumerable<string> items)
        {
            CustomConsole.WriteEmphasies(label);
            CustomConsole.WriteLineEmphasies(Separator);

            string leftpart = BuildItemLeftPart();

            foreach (string value in items)
            {
                CustomConsole.Write(leftpart);
                CustomConsole.WriteLine(value);
            }
        }

        private string BuildItemLeftPart()
        {
            StringBuilder sb = new StringBuilder();

            if (ItemsIndentation > 0)
            {
                string indentation = new string(' ', ItemsIndentation);
                sb.Append(indentation);
            }

            if (Bullet != null)
            {
                sb.Append(Bullet);

                if (BulletSpace > 0)
                {
                    string bulletSpace = new string(' ', BulletSpace);
                    sb.Append(bulletSpace);
                }
            }

            return sb.ToString();
        }
    }
}