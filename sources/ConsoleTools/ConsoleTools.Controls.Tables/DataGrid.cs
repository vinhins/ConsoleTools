// ConsoleTools
// Copyright (C) 2017-2020 Dust in the Wind
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

using System;
using System.Collections.Generic;
using System.Data;
using DustInTheWind.ConsoleTools.Controls.Tables.Printers;
using DustInTheWind.ConsoleTools.Controls.Tables.RenderingModel;

namespace DustInTheWind.ConsoleTools.Controls.Tables
{
    /// <summary>
    /// A control that renders a table with data into the console.
    /// </summary>
    public class DataGrid : BlockControl
    {
        /// <summary>
        /// Gets the <see cref="TitleRow"/> instance that represents the title row of the table.
        /// </summary>
        public TitleRow TitleRow { get; }

        /// <summary>
        /// Gets or sets the title of the current instance of the <see cref="DataGrid"/>.
        /// </summary>
        public MultilineText Title
        {
            get => TitleRow.TitleCell.Content;
            set => TitleRow.TitleCell.Content = value;
        }

        /// <summary>
        /// Gets or sets a value that specifies if the title is displayed.
        /// </summary>
        public bool DisplayTitle { get; set; } = true;

        /// <summary>
        /// Gets or sets the padding applied to the left side of every cell.
        /// </summary>
        public int? PaddingLeft { get; set; } = 1;

        /// <summary>
        /// Gets or sets the padding applied to the right side of every cell.
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
        public HorizontalAlignment CellHorizontalAlignment { get; set; } = ConsoleTools.HorizontalAlignment.Default;

        /// <summary>
        /// Gets the list of columns contained by the current table.
        /// </summary>
        public ColumnList Columns { get; }

        /// <summary>
        /// The list of rows contained by the current table.
        /// </summary>
        public DataRowList Rows { get; }

        /// <summary>
        /// Gets or sets a value that specifies if the column headers are displayed.
        /// Default value: <c>true</c>
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
        /// Default value: <c>true</c>
        /// </summary>
        public bool DisplayBorder { get; set; } = true;

        /// <summary>
        /// Gets or sets the table borders.
        /// </summary>
        public BorderTemplate BorderTemplate { get; set; } = BorderTemplate.PlusMinusBorderTemplate;

        /// <summary>
        /// Gets or sets the foreground color for the borders.
        /// Default value: <c>null</c>
        /// </summary>
        public ConsoleColor? BorderColor { get; set; }

        /// <summary>
        /// Gets or sets the foreground color for the title.
        /// Default value: <c>null</c>
        /// </summary>
        public ConsoleColor? TitleColor { get; set; }

        /// <summary>
        /// Gets or sets the foreground color for the column headers.
        /// Default value: <c>null</c>
        /// </summary>
        public ConsoleColor? HeaderColor { get; set; }

        /// <summary>
        /// Gets or sets the background color for the borders.
        /// Default value: <c>null</c>
        /// </summary>
        public ConsoleColor? BorderBackgroundColor { get; set; }
        
        /// <summary>
        /// Gets or sets the background color for the title.
        /// Default value: <c>null</c>
        /// </summary>
        public ConsoleColor? TitleBackgroundColor { get; set; }
        
        /// <summary>
        /// Gets or sets the background color for the column headers.
        /// Default value: <c>null</c>
        /// </summary>
        public ConsoleColor? HeaderBackgroundColor { get; set; }

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
        /// Renders the current instance into the console.
        /// </summary>
        protected override void DoDisplayContent(ControlDisplay display)
        {
            ConsoleTablePrinter consoleTablePrinter = new ConsoleTablePrinter
            {
                ForegroundColor = ForegroundColor,
                BorderColor = BorderColor,
                TitleColor = TitleColor,
                HeaderColor = HeaderColor,

                BackgroundColor = BackgroundColor,
                BorderBackgroundColor = BorderBackgroundColor,
                TitleBackgroundColor = TitleBackgroundColor,
                HeaderBackgroundColor = HeaderBackgroundColor
            };

            RenderInternal(consoleTablePrinter);
        }

        /// <summary>
        /// Renders the current instance into the specified <see cref="ITablePrinter"/>.
        /// </summary>
        /// <param name="tablePrinter">The <see cref="ITablePrinter"/> instance used to render the data.</param>
        public void Render(ITablePrinter tablePrinter)
        {
            RenderInternal(tablePrinter);
        }

        private void RenderInternal(ITablePrinter tablePrinter)
        {
            DataGridXBuilder dataGridXBuilder = new DataGridXBuilder
            {
                MinWidth = MinWidth ?? 0,
                TitleRow = TitleRow,
                DisplayTitle = DisplayTitle,
                Columns = Columns,
                DisplayColumnHeaders = DisplayColumnHeaders,
                Rows = Rows,
                DisplayBorderBetweenRows = DisplayBorderBetweenRows,
                BorderTemplate = BorderTemplate,
                DisplayBorder = DisplayBorder
            };

            DataGridX dataGridX = dataGridXBuilder.Build();

            dataGridX.Render(tablePrinter);
        }

        /// <summary>
        /// Returns the string representation of the current instance.
        /// </summary>
        /// <returns>The string representation of the current instance.</returns>
        public override string ToString()
        {
            StringTablePrinter tablePrinter = new StringTablePrinter();
            RenderInternal(tablePrinter);

            return tablePrinter.ToString();
        }

        /// <summary>
        /// Creates a new <see cref="DataGrid"/> instance containing the data from the specified <see cref="DataTable"/>.
        /// </summary>
        /// <param name="dataTable">The <see cref="DataTable"/> instance that contains the data.</param>
        /// <returns>The newly created <see cref="DataGrid"/> instance.</returns>
        public static DataGrid BuildFrom(DataTable dataTable)
        {
            if (dataTable == null) throw new ArgumentNullException(nameof(dataTable));

            DataGridBuilderFromDataTable builder = new DataGridBuilderFromDataTable(dataTable);
            return builder.DataGrid;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="DataGrid"/> and populates it with the
        /// fields and properties from the specified collection of objects.
        /// </summary>
        /// <typeparam name="T">The type of the objects used to populate the <see cref="DataGrid"/>.</typeparam>
        /// <param name="data">The collection of objects to be added to the <see cref="DataGrid"/>.</param>
        /// <returns>The newly created <see cref="DataGrid"/> instance.</returns>
        public static DataGrid BuildFrom<T>(IEnumerable<T> data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));

            DataGridBuilderFromObject builder = new DataGridBuilderFromObject(typeof(T));
            builder.Add(data);

            return builder.DataGrid;
        }

        public static DataGrid BuildFrom<T>(T data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));

            DataGridBuilderFromObject builder = new DataGridBuilderFromObject(typeof(T));
            builder.Add(data);

            return builder.DataGrid;
        }
    }
}