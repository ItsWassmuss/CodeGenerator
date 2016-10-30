using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Behaviors
{
    public class RotatedDataGrid : DataGrid
    {

        public RotatedDataGrid()
            : base()
        {

            /// Rotate the datagrid 90 degrees counter clockwise 
            TransformGroup transformDataGridGroup = new TransformGroup();
            transformDataGridGroup.Children.Add(new RotateTransform(90));
            transformDataGridGroup.Children.Add(new MatrixTransform(-1, 0, 0, 1, 0, 0));
            this.LayoutTransform = transformDataGridGroup;
            /// The column headings are at the left of the grid

            /// Rotate the column headings 90 degrees clockwise
            TransformGroup transformGroup = new TransformGroup();
            transformGroup.Children.Add(new RotateTransform(-90));
            transformGroup.Children.Add(new ScaleTransform(1, -1));
            Setter layoutTransformSetter = new Setter() { Property = DataGridColumnHeader.LayoutTransformProperty, Value = transformGroup };
            /// Set the ColumnHeaderStyle
            /// Create the column headers style and add the transform setter
            this.ColumnHeaderStyle = new Style() { TargetType = typeof(DataGridColumnHeader) };
            this.ColumnHeaderStyle.Setters.Add(layoutTransformSetter);

            /// Rotate the row headings 90 degrees clockwise
            Setter layoutRowTransformSetter = new Setter() { Property = DataGridRowHeader.LayoutTransformProperty, Value = transformGroup };
            /// Set the rowHeaderStyle
            /// Create the row headers style and add the transform setter
            this.RowHeaderStyle = new Style() { TargetType = typeof(DataGridRowHeader) };
            this.RowHeaderStyle.Setters.Add(layoutRowTransformSetter);

            /// Cell data runs from bottom to top 
            /// Rotate the cells 90 degrees clockwise
            Setter layoutCellTransformSetter = new Setter() { Property = DataGridCell.LayoutTransformProperty, Value = transformGroup };
            this.CellStyle = new Style() { TargetType = typeof(DataGridCell) };
            this.CellStyle.Setters.Add(layoutCellTransformSetter);
        
        }
    }
}
