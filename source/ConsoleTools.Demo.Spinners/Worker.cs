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
using System.Threading;
using DustInTheWind.ConsoleTools.Spinners;

namespace DustInTheWind.ConsoleTools.Demo.Spinners
{
    internal class Worker
    {
        public TimeSpan WorkPeriod { get; set; }
        public ISpinnerTemplate SpinnerTemplate { get; set; }
        public int SpinnerStepMilliseconds { get; set; }

        public void Run()
        {
            using (Spinner spinner = new Spinner(SpinnerTemplate))
            {
                spinner.StepMiliseconds = SpinnerStepMilliseconds;
                spinner.Text = "Doing some work";

                spinner.Start();

                try
                {
                    // Simulate work
                    Thread.Sleep(WorkPeriod);

                    spinner.Stop();
                    CustomConsole.WriteLineSuccess("[Done]");
                }
                catch
                {
                    spinner.Stop();
                    CustomConsole.WriteLineError("[Error]");
                }
                finally
                {
                    CustomConsole.WriteLine();
                    CustomConsole.WriteLine();
                }
            }
        }
    }
}