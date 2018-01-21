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

// --------------------------------------------------------------------------------
// Bugs or feature requests
// --------------------------------------------------------------------------------
// Note: For any bug or feature request please add a new issue on GitHub: https://github.com/lastunicorn/ConsoleTools/issues/new

namespace DustInTheWind.ConsoleTools.TabularData
{
    /// <summary>
    /// A control that renders a table with data into the console.
    /// </summary>
    public class DataGrid
    {
        /// <summary>
        /// Gets the <see cref="TitleRow"/> instance that represence the title row of the table.
        /// </summary>
        public TitleRow TitleRow { get; }

        /// <summary>
        /// Gets or sets the title of the current instance of the <see cref="DataGrid"/>.
        /// </summary>
        public MultilineText Title
        {
            get { return TitleRow.Content; }
            set { TitleRow.Content = value; }
        }

        /// <summary>
        /// Gets or sets a value that specifies if the title is displayed.
        /// </summary>
        public bool DisplayTitle { get; set; } = true;

        /// <summary>
        /// Gets or sets the padding applyed to the left side of every cell.
        /// </summary>
        public int? PaddingLeft { get; set; } = 1;

        /// <summary>
        /// Gets or sets the padding applyed to the right side of every cell.
        /// </summary>
        public int? PaddingRight { get; set; } = 1;

        /// <summary>
        /// Gets a value that specifies if border lines should be drawn between rows.
        /// Default value: false
        /// </summary>
        public bool DisplayBorderBetweenRows { get; set; }

        /// <summary>
        /// Gets or sets the horizontal alignment for the content of the cells contained by the current table.
        /// </summary>
        public HorizontalAlignment CellHorizontalAlignment { get; set; } = HorizontalAlignment.Default;

        /// <summary>
        /// Gets the list of columns contained by the current table.
        /// </summary>
        public ColumnList Columns { get; }

        /// <summary>
        /// The list of rows contained by the current table.
        /// </summary>
        public DataRowList Rows { get; }

        /// <summary>
        /// Gets or sets the minimum width of the table.
        /// </summary>
        public int MinWidth { get; set; }

        /// <summary>
        /// Gets or sets a value that specifies if the column headers are displayed.
        /// </summary>
        public bool DisplayColumnHeaders { get; set; } = true;

        /// <summary>
        /// Gets the row at the specified index.
        /// </summary>
        /// <param name="rowIndex">The zero-based index of the row to get.</param>
        /// <returns>The row at the specified index.</returns>
        public DataRow this[int rowIndex] => Rows[rowIndex];

        /// <summary>
        /// Gets the cell at the specified location.
        /// </summary>
        /// <param name="rowIndex">The zero-based row index of the cell to get.</param>
        /// <param name="columnIndex">The zero-based column index of the cell to get.</param>
        /// <returns>The cell at the specified location.</returns>
        public DataCell this[int rowIndex, int columnIndex] => Rows[rowIndex][columnIndex];

        /// <summary>
        /// Gets or sets a value that specifies if the borders are visible.
        /// </summary>
        public bool DisplayBorder { get; set; } = true;

        /// <summary>
        /// Gets or sets the table borders.
        /// </summary>
        public BorderTemplate BorderTemplate { get; set; } = BorderTemplate.PlusMinusBorderTemplate;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataGrid"/> class.
        /// </summary>
        public DataGrid()
        {
            Rows = new DataRowList(this);
            Columns = new ColumnList(this);

            TitleRow = new TitleRow
            {
                ParentDataGrid = this
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataGrid"/> class with
        /// the table title.
        /// </summary>
        public DataGrid(string title)
        {
            Rows = new DataRowList(this);
            Columns = new ColumnList(this);

            TitleRow = new TitleRow(title)
            {
                ParentDataGrid = this
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataGrid"/> class with
        /// the table title.
        /// </summary>
        public DataGrid(MultilineText title)
        {
            Rows = new DataRowList(this);
            Columns = new ColumnList(this);

            TitleRow = new TitleRow(title)
            {
                ParentDataGrid = this
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataGrid"/> class with
        /// the table title.
        /// </summary>
        public DataGrid(object title)
        {
            Rows = new DataRowList(this);
            Columns = new ColumnList(this);

            TitleRow = new TitleRow(title)
            {
                ParentDataGrid = this
            };
        }

        /// <summary>
        /// Returns the string representation of the current instance.
        /// </summary>
        /// <returns>The string representation of the current instance.</returns>
        public override string ToString()
        {
            StringTablePrinter tablePrinter = new StringTablePrinter();
            Render(tablePrinter);

            return tablePrinter.ToString();
        }

        /// <summary>
        /// Renders the current instance into the console.
        /// </summary>
        public void Render()
        {
            ConsoleTablePrinter consoleTablePrinter = new ConsoleTablePrinter();
            Render(consoleTablePrinter);
        }

        /// <summary>
        /// Renders the current instance into the specified <see cref="ITablePrinter"/>.
        /// </summary>
        /// <param name="tablePrinter">The <see cref="ITablePrinter"/> instacne used to render the data.</param>
        public void Render(ITablePrinter tablePrinter)
        {
            TableRenderer tableRenderer = new TableRenderer
            {
                TitleRow = TitleRow,
                DisplayTitle = DisplayTitle,
                Columns = Columns,
                Rows = Rows,
                BorderTemplate = BorderTemplate,
                DisplayBorder = DisplayBorder,
                DrawBordersBetweenRows = DisplayBorderBetweenRows,
                MinWidth = MinWidth,
                DisplayColumnHeaders = DisplayColumnHeaders,
                CellHorizontalAlignment = CellHorizontalAlignment
            };
            tableRenderer.Render(tablePrinter);
        }
    }
}