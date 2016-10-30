using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro.Controls;

namespace CodeGenerator
{
    /// <summary>
    /// Interaction logic for SelectionTable.xaml
    /// </summary>
    public partial class SelectionTable : MetroWindow
    {
        public SelectionTable()
        {
            InitializeComponent();
            this.Loaded += SelectionTable_Loaded;
        }
        StackPanel innerStack;



        private void SelectionTable_Loaded(object sender, RoutedEventArgs e)
        {
            innerStack = new StackPanel { Orientation = Orientation.Vertical };

            foreach (var c in MVC.TablesList)
            {
                CheckBox cb = new CheckBox();
                cb.Name = c;
                cb.Content = c;

                MainSSl.Children.Add(cb);
            }

        }

        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            //MVC.SelectedTable.Clear();

            foreach (var item in MainSSl.Children)
            {
                var CheckBox = (CheckBox)item;
                if (CheckBox != null)
                {
                    if (CheckBox.IsChecked == true)
                    {
                        MVC.SelectedTable=(CheckBox.Content.ToString());
                        break;
                    }
                }
            }

            this.Close();
        }

        private void btnSelectAllTable_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in MainSSl.Children)
            {
                var CheckBox = (CheckBox)item;
                if (CheckBox != null)
                {
                    CheckBox.IsChecked = true;
                    //MainWindow.SelectedTable.Add(CheckBox.Content.ToString());
                }
            }
        }

    }
}
