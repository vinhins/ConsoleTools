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
using DustInTheWind.ConsoleTools.Spinners.Templates;

namespace DustInTheWind.ConsoleTools.Demo.Spinners
{
    internal class Program
    {
        private static void Main()
        {
            DisplayApplicationHeader();

            while (true)
            {
                Worker worker = CreateWorker();

                if (worker == null)
                    break;

                CustomConsole.WriteLine();
                worker.Run();
            }
        }

        private static void DisplayApplicationHeader()
        {
            CustomConsole.WriteLineEmphasies("ConsoleTools Demo - Progress spinner");
            CustomConsole.WriteLine("===============================================================================");
            CustomConsole.WriteLine();
            CustomConsole.WriteLine("Step 1: Select a template for the spinner.");
            CustomConsole.WriteLine("Step 2: The application will simulate an asyn work and display the spinner.");
            CustomConsole.WriteLine("-------------------------------------------------------------------------------");
            CustomConsole.WriteLine();
        }

        private static Worker CreateWorker()
        {
            CustomConsole.WriteLine("1  - arrow");
            CustomConsole.WriteLine("2  - stick");
            CustomConsole.WriteLine("3  - bubble");
            CustomConsole.WriteLine("4  - half-block rotate");
            CustomConsole.WriteLine("5  - half-block vertical");
            CustomConsole.WriteLine("6  - fan");
            CustomConsole.WriteLine("11 - fill (dot, empty from start) - default");
            CustomConsole.WriteLine("12 - fill (dot, empty from end)");
            CustomConsole.WriteLine("13 - fill (dot, sudden empty)");
            CustomConsole.WriteLine("14 - fill (dot, with borders)");
            CustomConsole.WriteLine("15 - fill (block, length: 10 chars, step: 100ms)");
            CustomConsole.WriteLine("0 - exit");

            CustomConsole.WriteLine();

            while (true)
            {
                CustomConsole.WriteEmphasies("Select spinner template: ");
                string rawValue = Console.ReadLine();

                switch (rawValue)
                {
                    case "0":
                        return null;

                    case "1":
                        return new Worker
                        {
                            SpinnerTemplate = new ArrowTemplate(),
                            SpinnerStepMilliseconds = 400,
                            WorkInterval = TimeSpan.FromSeconds(5)
                        };
                    case "2":
                        return new Worker
                        {
                            SpinnerTemplate = new StickTemplate(),
                            SpinnerStepMilliseconds = 400,
                            WorkInterval = TimeSpan.FromSeconds(5)
                        };
                    case "3":
                        return new Worker
                        {
                            SpinnerTemplate = new BubbleTemplate(),
                            SpinnerStepMilliseconds = 400,
                            WorkInterval = TimeSpan.FromSeconds(5)
                        };
                    case "4":
                        return new Worker
                        {
                            SpinnerTemplate = new HalfBlockRotateTemplate(),
                            SpinnerStepMilliseconds = 400,
                            WorkInterval = TimeSpan.FromSeconds(5)
                        };
                    case "5":
                        return new Worker
                        {
                            SpinnerTemplate = new HalfBlockBlinkTemplate(),
                            SpinnerStepMilliseconds = 400,
                            WorkInterval = TimeSpan.FromSeconds(5)
                        };
                    case "6":
                        return new Worker
                        {
                            SpinnerTemplate = new FanTemplate(),
                            SpinnerStepMilliseconds = 400,
                            WorkInterval = TimeSpan.FromSeconds(5)
                        };
                    case "11":
                        return new Worker
                        {
                            SpinnerTemplate = new FillTemplate(),
                            SpinnerStepMilliseconds = 400,
                            WorkInterval = TimeSpan.FromSeconds(5)
                        };
                    case "12":
                        return new Worker
                        {
                            SpinnerTemplate = new FillTemplate { FilledBehavior = FilledBehavior.EmptyFromEnd },
                            SpinnerStepMilliseconds = 400,
                            WorkInterval = TimeSpan.FromSeconds(5)
                        };
                    case "13":
                        return new Worker
                        {
                            SpinnerTemplate = new FillTemplate { FilledBehavior = FilledBehavior.SuddenEmpty },
                            SpinnerStepMilliseconds = 400,
                            WorkInterval = TimeSpan.FromSeconds(5)
                        };
                    case "14":
                        return new Worker
                        {
                            SpinnerTemplate = new FillTemplate { ShowBorders = true },
                            SpinnerStepMilliseconds = 400,
                            WorkInterval = TimeSpan.FromSeconds(5)
                        };
                    case "15":
                        return new Worker
                        {
                            SpinnerTemplate = new FillTemplate('▓', 10),
                            SpinnerStepMilliseconds = 100,
                            WorkInterval = TimeSpan.FromSeconds(10)
                        };
                }
            }
        }
    }
}
