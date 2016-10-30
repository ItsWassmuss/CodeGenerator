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
    /// Interaction logic for SelectionField.xaml
    /// </summary>
    public partial class SelectionField : MetroWindow
    {
        public SelectionField()
        {
            InitializeComponent();
            this.Loaded += SelectionField_Loaded;
        }
        StackPanel innerStack;



        private void SelectionField_Loaded(object sender, RoutedEventArgs e)
        {
            innerStack = new StackPanel { Orientation = Orientation.Vertical };

            foreach (var c in MVC.FiledsList)
            {
                CheckBox cb = new CheckBox();
                cb.Name = c;
                cb.Content = c;

                MainSSl.Children.Add(cb);
            }

        }

        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            MVC.GenerateFiledList.Clear();
            foreach (var item in MainSSl.Children)
            {
                var CheckBox = (CheckBox)item;
                if (CheckBox != null)
                {
                    if (CheckBox.IsChecked == true)
                    {
                        MVC.GenerateFiledList.Add(CheckBox.Content.ToString());
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
                }
            }
        }

    }
}
