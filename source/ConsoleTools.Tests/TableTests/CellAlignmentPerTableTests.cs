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

using DustInTheWind.ConsoleTools.TabularData;
using NUnit.Framework;

namespace DustInTheWind.ConsoleTools.Tests.TableTests
{
    [TestFixture]
    public class CellAlignmentPerTableTests
    {
        [Test]
        public void whole_table_is_aligned_to_Right_without_explicit_columns()
        {
            Table table = new Table("This is a cell alignment test");
            table.DisplayColumnHeaders = false;
            table.CellHorizontalAlignment = HorizontalAlignment.Right;

            table.AddRow(new[] { "0,0", "0,1", "0,2" });
            table.AddRow(new[] { "1,0", "1,1", "1,2" });
            table.AddRow(new[] { "2,0", "2,1", "2,2" });

            string expected =
@"+-------------------------------+
| This is a cell alignment test |
+----------+----------+---------+
|      0,0 |      0,1 |     0,2 |
|      1,0 |      1,1 |     1,2 |
|      2,0 |      2,1 |     2,2 |
+----------+----------+---------+
";

            CustomAssert.TableRender(table, expected);
        }
        [Test]
        public void whole_table_is_aligned_to_Right_with_explicit_declared_columns()
        {
            Table table = new Table("This is a cell alignment test");
            table.DisplayColumnHeaders = false;
            table.CellHorizontalAlignment = HorizontalAlignment.Right;
            table.DisplayColumnHeaders = false;

            Column column0 = new Column("Col 0");
            table.Columns.Add(column0);

            Column column1 = new Column("Col 1");
            table.Columns.Add(column1);

            Column column2 = new Column("Col 2");
            table.Columns.Add(column2);

            table.AddRow(new[] { "0,0", "0,1", "0,2" });
            table.AddRow(new[] { "1,0", "1,1", "1,2" });
            table.AddRow(new[] { "2,0", "2,1", "2,2" });

            string expected =
@"+-------------------------------+
| This is a cell alignment test |
+----------+----------+---------+
|      0,0 |      0,1 |     0,2 |
|      1,0 |      1,1 |     1,2 |
|      2,0 |      2,1 |     2,2 |
+----------+----------+---------+
";

            CustomAssert.TableRender(table, expected);
        }
    }
}