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
using System.Collections.Generic;
using DustInTheWind.ConsoleTools.Demo.ScrollableMenu.Commands;
using DustInTheWind.ConsoleTools.MenuControl;
using DustInTheWind.ConsoleTools.MenuControl.MenuItems;

namespace DustInTheWind.ConsoleTools.Demo.ScrollableMenu
{
    internal class MainMenu : MenuControl.ScrollableMenu
    {
        public MainMenu(GameApplication application)
        {
            if (application == null) throw new ArgumentNullException(nameof(application));

            MarginTop = 1;
            MarginBottom = 1;

            IEnumerable<IMenuItem> menuItems = CreateMenuItems(application);
            AddItems(menuItems);
            

            // You can play with the following values.

            // This automatically selects the first item when the menu is displayed.
            //SelectFirstByDefault = true;

            // 
            //HorizontalAlignment = HorizontalAlignment.Left;
            //ItemsHorizontalAlignment = HorizontalAlignment.Right;
        }

        private IEnumerable<IMenuItem> CreateMenuItems(GameApplication gameApplication)
        {
            return new IMenuItem[]
            {
                new LabelMenuItem
                {
                    Text = "New Game",
                    Command = new NewGameCommand(gameApplication.GameBoard)
                },
                new YesNoMenuItem
                {
                    Text = "Save Game",
                    VisibilityProvider = () => gameApplication.GameBoard.IsGameStarted,
                    Command = new SaveGameCommand()
                },
                new LabelMenuItem
                {
                    Text = "Load Game",
                    Command = new LoadGameCommand(gameApplication.GameBoard)
                },

                new SeparatorMenuItem(),

                new LabelMenuItem
                {
                    Text = "Settings",
                    Command = new SettingsCommand()
                },
                new LabelMenuItem
                {
                    Text = "Credits",
                    Command = new CreditsCommand()
                },

                new SeparatorMenuItem(),

                new LabelMenuItem
                {
                    Text = "Exit",
                    ShortcutKey = ConsoleKey.X,
                    Command = new ExitCommand(gameApplication)
                }
            };
        }
    }
}