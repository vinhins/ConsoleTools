// ConsoleTools
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
using System.Collections.ObjectModel;

namespace DustInTheWind.ConsoleTools.TabularData
{
    /// <summary>
    /// Represents a table cell that contains data.
    /// </summary>
    public class Cell
    {
        public static HorizontalAlignment DefaultHorizontalAlignment { get; } = HorizontalAlignment.Left;

        /// <summary>
        /// Gets or sets the content of the cell.
        /// </summary>
        public MultilineText Content { get; set; }

        /// <summary>
        /// Gets or sets the row that contains the current cell.
        /// </summary>
        public Row ParentRow { get; set; }

        /// <summary>
        /// Gets a value that specified if the cell contains no data.
        /// </summary>
        public bool IsEmpty => Content == null || Content.IsEmpty;

        /// <summary>
        /// Gets or sets the horizontal alignment of the content displayed in the cell.
        /// </summary>
        public HorizontalAlignment HorizontalAlignment { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Cell" /> class with
        /// the text contained by it.
        /// </summary>
        /// <param name="text">The text displayed in the cell.</param>
        public Cell(string text)
        {
            Content = new MultilineText(text);
            HorizontalAlignment = HorizontalAlignment.Default;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Cell" /> class with
        /// the text contained by it and its horizontal alignment.
        /// </summary>
        /// <param name="text">The text displayed in the cell.</param>
        /// <param name="horizontalAlignment">The horizontal alignment of the content of the new cell.</param>
        public Cell(string text, HorizontalAlignment horizontalAlignment)
        {
            Content = new MultilineText(text);
            HorizontalAlignment = horizontalAlignment;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Cell" /> class with
        /// the text contained by it and its horizontal alignment.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="horizontalAlignment">The horizontal alignment of the content of the new cell.</param>
        public Cell(MultilineText text, HorizontalAlignment horizontalAlignment)
        {
            Content = text;
            HorizontalAlignment = horizontalAlignment;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Cell" /> class with
        /// an object representing the content.
        /// </summary>
        public Cell(object content)
        {
            Content = new MultilineText(content.ToString());
            HorizontalAlignment = HorizontalAlignment.Default;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Cell" /> class with
        /// an object representing the content and its horizontal alignment.
        /// </summary>
        public Cell(object content, HorizontalAlignment horizontalAlignment)
        {
            Content = new MultilineText(content.ToString());
            HorizontalAlignment = horizontalAlignment;
        }

        /// <summary>
        /// Returns the size of the cell, including the padding.
        /// </summary>
        public Size CalculateDimensions()
        {
            int cellWidth;
            int cellHeight;

            int paddingLeft = CalculatePaddingLeft();
            int paddingRight = CalculatePaddingRight();

            if (IsEmpty)
            {
                cellWidth = paddingLeft + paddingRight;
                cellHeight = 0;
            }
            else
            {
                cellWidth = paddingLeft + Content.Size.Width + paddingRight;
                cellHeight = Content.Size.Height;
            }

            return new Size(cellWidth, cellHeight);
        }

        private int CalculatePaddingLeft()
        {
            return ParentRow?.ParentTable?.PaddingLeft ?? 0;
        }

        private int CalculatePaddingRight()
        {
            return ParentRow?.ParentTable?.PaddingRight ?? 0;
        }

        /// <summary>
        /// Returns the string representation of the content of the cell.
        /// </summary>
        public override string ToString()
        {
            return Content?.ToString() ?? string.Empty;
        }

        public string Render(int cellWidth, int rowLineIndex)
        {
            int paddingLeftLength = CalculatePaddingLeft();
            int paddingRightLength = CalculatePaddingRight();

            int cellContentWidth = cellWidth - paddingLeftLength - paddingRightLength;

            bool existsContentLine = rowLineIndex < Content.Size.Height;
            if (!existsContentLine)
                return new string(' ', cellWidth);

            // Build inner content.

            string innerContent = Content.Lines[rowLineIndex];

            HorizontalAlignment alignment = CalculateHorizontalAlignment();

            switch (alignment)
            {
                case HorizontalAlignment.Left:
                    innerContent = innerContent.PadRight(cellContentWidth, ' ');
                    break;

                case HorizontalAlignment.Right:
                    innerContent = innerContent.PadLeft(cellContentWidth, ' ');
                    break;

                case HorizontalAlignment.Center:
                    int totalSpaces = cellContentWidth - Content.Size.Width;
                    int rightSpaces = (int)Math.Ceiling((double)totalSpaces / 2);
                    innerContent = innerContent
                        .PadLeft(cellContentWidth - rightSpaces, ' ')
                        .PadRight(cellContentWidth, ' ');
                    break;

                default:
                    throw new ApplicationException("Internal error: Invalid calculated horizontal alignment.");
            }

            // Build paddings.

            string leftPadding = new string(' ', paddingLeftLength);
            string rightPadding = new string(' ', paddingRightLength);

            // Concatenate everything.

            return leftPadding + innerContent + rightPadding;
        }

        private HorizontalAlignment CalculateHorizontalAlignment()
        {
            HorizontalAlignment alignment = HorizontalAlignment;

            if (alignment == HorizontalAlignment.Default)
                alignment = CalculateHorizontalAlignmentAtColumnLevel();

            if (alignment == HorizontalAlignment.Default)
                alignment = CalculateHorizontalAlignmentAtTableLevel();

            if (alignment == HorizontalAlignment.Default)
                alignment = DefaultHorizontalAlignment;

            return alignment;
        }

        private HorizontalAlignment CalculateHorizontalAlignmentAtColumnLevel()
        {
            ReadOnlyCollection<Column> columns = ParentRow?.ParentTable?.Columns;
            int? columnIndex = ParentRow?.IndexOfCell(this);

            Column column = columns != null && columnIndex.HasValue && columnIndex < columns.Count
                ? columns[columnIndex.Value]
                : null;

            return column?.CellHorizontalAlignment ?? HorizontalAlignment.Default;
        }

        private HorizontalAlignment CalculateHorizontalAlignmentAtTableLevel()
        {
            Table table = ParentRow?.ParentTable;

            return table == null || table.CellHorizontalAlignment == HorizontalAlignment.Default
                ? DefaultHorizontalAlignment
                : table.CellHorizontalAlignment;
        }

        public static implicit operator Cell(string text)
        {
            MultilineText multilineText = new MultilineText(text);
            return new Cell(multilineText);
        }

        public static implicit operator string(Cell cell)
        {
            return cell.Content?.ToString() ?? string.Empty;
        }
    }
}