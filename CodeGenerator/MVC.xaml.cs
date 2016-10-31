using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
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
using CodeGenerator.Model;
using MahApps.Metro.Controls;
using CodeGenerator.Model.Views;
using System;

namespace CodeGenerator
{
    /// <summary>
    /// Interaction logic for MVC.xaml
    /// </summary>
    public partial class MVC : MetroWindow
    {
        public MVC()
        {
            InitializeComponent();

        }

        private static readonly List<string> IAdminIndexListNameSpaces = new List<string>
        {
            "using System;",
            //"using System.Collections.Generic;", 
            //"using System.Data;", 
            //"using System.Linq;", 
            //"using System.Text;", 
            //"using System.Threading.Tasks;",
        };

        private static readonly List<string> AdminIndexListNameSpaces = new List<string>
        {
            "using System;",
            //"using System.Collections.Generic;", 
            //"using System.Data;", 
            //"using System.Linq;", 
            //"using System.Text;", 
            //"using System.Threading.Tasks;",
            "using System.Collections.Generic;",
            "using System.ComponentModel.DataAnnotations;",
        };

        private static readonly List<string> AdminOlIndexListNameSpaces = new List<string>
        {
            "using System;",
            "using System.ComponentModel.DataAnnotations;",
        };

        private static readonly List<string> AdminIndexNameSpaces = new List<string>
        {
            "using System.Collections.Generic;",
        };

        private static readonly List<string> AdminOlIndexNameSpaces = new List<string>
        {
            "using System.Collections.Generic;",
            "using System.ComponentModel.DataAnnotations;",
        };

        private static readonly List<string> AdminCreateNameSpaces = new List<string>
        {
            "using System;",
            "using System.ComponentModel.DataAnnotations;",
            "using System.Web.Mvc;",
        };

        private static readonly List<string> AdminOlCreateNameSpaces = new List<string>
        {
            "using System;",
            "using System.ComponentModel.DataAnnotations;",
            "using System.Web.Mvc;",
            "using System.Collections.Generic;",
        };

        private static readonly List<string> AdminEditNameSpaces = new List<string>
        {
            "using System;",
            "using System.ComponentModel.DataAnnotations;",
            "using System.Web.Mvc;",
        };

        private static readonly List<string> AdminOlEditNameSpaces = new List<string>
        {
            "using System;",
            "using System.ComponentModel.DataAnnotations;",
            "using System.Web.Mvc;",
            "using System.Collections.Generic;",
        };

        private static readonly List<string> AdminDetailsNameSpaces = new List<string>
        {
            "using System;",
            "using System.ComponentModel.DataAnnotations;",
        };


        public static List<string> TablesList = new List<string>();
        public static string SelectedTable = "";
        public static List<string> FiledsList = new List<string>();
        public static List<string> GenerateFiledList = new List<string>();

        private const string MainRoot = @"D:\Temp";
        private static string _tableName = "";
        private static string _modelName = "";
        private static string _modelRoot = MainRoot + @"\" + _modelName;
        private static string _viewModelRoot = _modelRoot + @"\ViewModels";
        private static string _contractsRoot = _viewModelRoot + @"\Interface";

        private static string IAdminIndexListName = "I" + _modelName + "AdminIndexListViewModel";
        private static string IAdminIndexListFileName = "I" + _modelName + "AdminIndexListViewModel.cs";
        private string IAdminIndexListRoot = _contractsRoot + @"\" + IAdminIndexListFileName;

        private static string AdminIndexListName = _modelName + "AdminIndexListViewModel";
        private static string AdminIndexName = _modelName + "AdminIndexViewModel";
        private static string AdminIndexListOlName = _modelName + "AdminIndexListOlViewModel";
        private static string AdminIndexListFileName = _modelName + "AdminIndexListViewModel.cs";
        private static string AdminIndexFileName = _modelName + "AdminIndexViewModel.cs";
        private static string AdminIndexListOlFileName = _modelName + "AdminIndexListOlViewModel.cs";
        private string AdminIndexListRoot = _viewModelRoot + @"\" + AdminIndexListFileName;
        private string AdminIndexRoot = _viewModelRoot + @"\" + AdminIndexFileName;
        private string AdminIndexListOlRoot = _viewModelRoot + @"\" + AdminIndexListOlFileName;
        private const string IAdminIndexListBaseViewModel = "IAdminIndexListBaseViewModel";
        private const string IAdminIndexBaseViewModel = "IAdminIndexBaseViewModel";

        private static string AdminCreateName = _modelName + "AdminCreateViewModel";
        private static string AdminCreateFileName = _modelName + "AdminCreateViewModel.cs";
        private string AdminCreateRoot = _viewModelRoot + @"\" + AdminCreateFileName;

        private static string AdminEditName = _modelName + "AdminEditViewModel";
        private static string AdminEditFileName = _modelName + "AdminEditViewModel.cs";
        private string AdminEditRoot = _viewModelRoot + @"\" + AdminEditFileName;

        private static string AdminDetailsName = _modelName + "AdminDetailsViewModel";
        private static string AdminDetailsFileName = _modelName + "AdminDetailsViewModel.cs";
        private string AdminDetailsRoot = _viewModelRoot + @"\" + AdminDetailsFileName;


        private static string AdminOlIndexListName = _modelName + "AdminOlIndexListViewModel";
        private static string AdminOlIndexName = _modelName + "AdminOlIndexViewModel";
        private static string AdminOlIndexListOlName = _modelName + "AdminOlIndexListOlViewModel";
        private static string AdminOlIndexListFileName = _modelName + "AdminOlIndexListViewModel.cs";
        private static string AdminOlIndexFileName = _modelName + "AdminOlIndexViewModel.cs";
        private static string AdminOlIndexListOlFileName = _modelName + "AdminOlIndexListOlViewModel.cs";
        private string AdminOlIndexListRoot = _viewModelRoot + @"\" + AdminOlIndexListFileName;
        private string AdminOlIndexRoot = _viewModelRoot + @"\" + AdminOlIndexFileName;
        private string AdminOlIndexListOlRoot = _viewModelRoot + @"\" + AdminOlIndexListOlFileName;
        private const string IAdminOlIndexListBaseViewModel = "IAdminOlIndexListBaseViewModel";
        //private const string IAdminIndexBaseViewModel = "IAdminIndexBaseViewModel";

        private static string AdminOlCreateName = _modelName + "AdminOlCreateViewModel";
        private static string AdminOlCreateFileName = _modelName + "AdminOlCreateViewModel.cs";
        private string AdminOlCreateRoot = _viewModelRoot + @"\" + AdminOlCreateFileName;


        private static string AdminOlEditName = _modelName + "AdminOlEditViewModel";
        private static string AdminOlEditFileName = _modelName + "AdminOlEditViewModel.cs";
        private string AdminOlEditRoot = _viewModelRoot + @"\" + AdminOlEditFileName;


        //            "***************************"      Views      "***************************"

        private static string _controllerName = "***************************"; //txtController.Text
        private static string _viewRoot = _modelRoot + @"\Views" + @"\" + _controllerName;
        private static string _viewOlRoot = _modelRoot + @"\ViewsOl" + @"\" + _controllerName;

        private static string AdminIndexViewFileName = "Index.cshtml";
        private static string AdminListViewFileName = "_List.cshtml";
        private string AdminIndexViewRoot = _viewRoot + @"\" + AdminIndexViewFileName;
        private string AdminListViewRoot = _viewRoot + @"\" + AdminListViewFileName;
        private string AdminOlIndexViewRoot = _viewOlRoot + @"\" + AdminIndexViewFileName;
        private string AdminOlListViewRoot = _viewRoot + @"\" + AdminListViewFileName;
        private static string ModelPersianName = "مقاله"; //txtModelPersianName.Text;
        private static string AreaName = "Article"; //txtArea.Text;

        private static string AdminCreateViewFileName = "Create.cshtml";
        private string AdminCreateViewRoot = _viewRoot + @"\" + AdminCreateViewFileName;

        private static string AdminEditViewFileName = "Edit.cshtml";
        private string AdminEditViewRoot = _viewRoot + @"\" + AdminEditViewFileName;


        private static string AdminDetailsViewFileName = "Details.cshtml";
        private string AdminDetailsViewRoot = _viewRoot + @"\" + AdminDetailsViewFileName;



        //            "***************************"      Views      "***************************"

        private static string _jSRoot = _modelRoot + @"\JS" + @"\" + _controllerName;
        private static string _jSOlRoot = _modelRoot + @"\JS" + @"\" + _controllerName + "Ol";




        private static string AdminIndexJSFileName = "Index.js";
        private string AdminIndexJSRoot = _jSRoot + @"\" + AdminIndexJSFileName;
        private string AdminOlIndexJSRoot = _jSOlRoot + @"\" + AdminIndexJSFileName;


        private static string AdminCreateJSFileName = "Create.js";
        private string AdminCreateJSRoot = _jSRoot + @"\" + AdminCreateJSFileName;

        private static string AdminEditJSFileName = "Edit.js";
        private string AdminEditJSRoot = _jSRoot + @"\" + AdminEditJSFileName;





        private List<string> GetDatabaseList()
        {
            var list = new List<string>();

            // Open connection to the database
            var conString = "server=" + txtServer.Text + ";uid=" + txtUserName.Text + ";pwd=" + txtPassword.Text + "; ";

            using (var con = new SqlConnection(conString))
            {
                con.Open();

                // Set up a command with the given query and associate
                // this with the current connection.
                using (var cmd = new SqlCommand("SELECT name from sys.databases d WHERE d.database_id > 4", con))
                {
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (!dr[0].ToString().StartsWith("ReportServer$"))
                                list.Add(dr[0].ToString());
                        }
                    }
                }
            }
            return list;

        }

        private void btnGetDatabase_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cmbDatabases.ItemsSource = GetDatabaseList();
                if (cmbDatabases.Items.Count > 0)
                {
                    cmbDatabases.SelectedIndex = 0;
                    btnGetTables.IsEnabled = true;
                }
                //MessageBox.Show("Filled");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection failed.");
            }

        }

        private void btnGetTables_Click(object sender, RoutedEventArgs e)
        {
            var sda = SqlDatabaseAdapter;

            TablesList = sda.GetTables(cmbDatabases.SelectedItem.ToString());

            cmbTables.ItemsSource = TablesList;
            if (cmbTables.Items.Count > 0)
            {
                cmbTables.SelectedIndex = 0;
            }
            //new SelectionTable().ShowDialog();
            //StackPanel.Children.Clear();
            ////foreach (var table in SelectedTable)
            //{
            //    StackPanel.Children.Add(
            //        new Border()
            //        {
            //            BorderBrush = Brushes.Blue,
            //            BorderThickness = new Thickness(1),
            //            Background = Brushes.White,
            //            Child = new TextBlock() { Text = SelectedTable, Foreground = Brushes.Blue },
            //            Margin = new Thickness(5, 0, 0, 0),
            //            Padding = new Thickness(5),
            //        }
            //        );
            //}
            //btnGetField.IsEnabled = true;
            //if (SelectedTable.Any())
            {
                txtModelName.Text = SelectedTable.EndsWith("s")
                    ? SelectedTable.Substring(0, SelectedTable.Length - 1)
                    : SelectedTable;
            }
        }

        private void CmbDatabases_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var sda = SqlDatabaseAdapter;

            TablesList = sda.GetTables(cmbDatabases.SelectedItem.ToString());

            cmbTables.ItemsSource = TablesList;
            if (cmbTables.Items.Count > 0)
            {
                cmbTables.SelectedIndex = 0;
            }

        }

        private void CmbTables_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbTables.SelectedIndex < 0)
                return;

            SelectedTable = cmbTables.SelectedItem.ToString();
            txtModelName.Text = SelectedTable.EndsWith("s")
                ? SelectedTable.Substring(0, SelectedTable.Length - 1)
                : SelectedTable;

        }

        private void btnGetField_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTable.Length == 0)
                return;

            CallSelectionFieldWindow();

            FillIAdminIndexList();

        }

        private void CallSelectionFieldWindow()
        {
            var conString = "server=" + txtServer.Text + ";uid=" + txtUserName.Text + ";pwd=" + txtPassword.Text + "; ";
            var sda = new SqlDatabaseAdapter(conString);

            var columns = sda.GetTablesColumn(SelectedTable, cmbDatabases.SelectedValue.ToString());
            FiledsList.Clear();
            foreach (DataRow row in columns.Rows)
            {
                //if (row["Name"].ToString() != "ID")
                FiledsList.Add(row["Name"].ToString());
            }
            new SelectionField().ShowDialog();
            //grdField.ItemsSource = ConvertToTable(GenerateFiledList).DefaultView;
            //cmbFilterField.ItemsSource = GenerateFiledList;
            //btnGetField.IsEnabled = true;
        }

        private void CheckTempFolder()
        {
            if (!Directory.Exists(MainRoot))
                Directory.CreateDirectory(MainRoot);
        }

        private void CheckModelFolder()
        {
            if (!Directory.Exists(_modelRoot))
                Directory.CreateDirectory(_modelRoot);
        }

        private void CheckViewModelFolder()
        {
            if (!Directory.Exists(_viewModelRoot))
                Directory.CreateDirectory(_viewModelRoot);
        }

        private void CheckViewFolder()
        {
            if (!Directory.Exists(_viewRoot))
                Directory.CreateDirectory(_viewRoot);
        }

        private void CheckViewOlFolder()
        {
            if (!Directory.Exists(_viewOlRoot))
                Directory.CreateDirectory(_viewOlRoot);
        }

        private void CheckJSFolder()
        {
            if (!Directory.Exists(_jSRoot))
                Directory.CreateDirectory(_jSRoot);
        }

        private void CheckContractsFolder()
        {
            if (!Directory.Exists(_contractsRoot))
                Directory.CreateDirectory(_contractsRoot);
        }

        private void CreateFile(string path)
        {
            if (!File.Exists(path))
            {
                var myFile = File.Create(path);

                var info = new UTF8Encoding(true).GetBytes("//Generate by CodeGenerator ");
                myFile.Write(info, 0, info.Length);

                myFile.Close();
            }
        }

        private void FillRootDirectory()
        {
            _modelName = txtModelName.Text;
            _tableName = SelectedTable;
            _modelRoot = MainRoot + @"\" + _modelName;
            _viewModelRoot = _modelRoot + @"\ViewModels";
            _contractsRoot = _viewModelRoot + @"\Contracts";

            IAdminIndexListName = "I" + _modelName + "AdminIndexListViewModel";
            IAdminIndexListFileName = "I" + _modelName + "AdminIndexListViewModel.cs";
            IAdminIndexListRoot = _contractsRoot + @"\" + IAdminIndexListFileName;

            AdminIndexListName = _modelName + "AdminIndexListViewModel";
            AdminIndexName = _modelName + "AdminIndexViewModel";
            AdminIndexListOlName = _modelName + "AdminIndexListOlViewModel";
            AdminIndexListFileName = _modelName + "AdminIndexListViewModel.cs";
            AdminIndexFileName = _modelName + "AdminIndexViewModel.cs";
            AdminIndexListOlFileName = _modelName + "AdminIndexListOlViewModel.cs";
            AdminIndexListRoot = _viewModelRoot + @"\" + AdminIndexListFileName;
            AdminIndexRoot = _viewModelRoot + @"\" + AdminIndexFileName;
            AdminIndexListOlRoot = _viewModelRoot + @"\" + AdminIndexListOlFileName;

            AdminCreateName = _modelName + "AdminCreateViewModel";
            AdminCreateFileName = _modelName + "AdminCreateViewModel.cs";
            AdminCreateRoot = _viewModelRoot + @"\" + AdminCreateFileName;

            AdminEditName = _modelName + "AdminEditViewModel";
            AdminEditFileName = _modelName + "AdminEditViewModel.cs";
            AdminEditRoot = _viewModelRoot + @"\" + AdminEditFileName;


            AdminDetailsName = _modelName + "AdminDetailsViewModel";
            AdminDetailsFileName = _modelName + "AdminDetailsViewModel.cs";
            AdminDetailsRoot = _viewModelRoot + @"\" + AdminDetailsFileName;


            AdminOlIndexListName = _modelName + "AdminOlIndexListViewModel";
            AdminOlIndexName = _modelName + "AdminOlIndexViewModel";
            AdminOlIndexListOlName = _modelName + "AdminOlIndexListOlViewModel";
            AdminOlIndexListFileName = _modelName + "AdminOlIndexListViewModel.cs";
            AdminOlIndexFileName = _modelName + "AdminOlIndexViewModel.cs";
            AdminOlIndexListOlFileName = _modelName + "AdminOlIndexListOlViewModel.cs";
            AdminOlIndexListRoot = _viewModelRoot + @"\" + AdminOlIndexListFileName;
            AdminOlIndexRoot = _viewModelRoot + @"\" + AdminOlIndexFileName;
            AdminOlIndexListOlRoot = _viewModelRoot + @"\" + AdminOlIndexListOlFileName;
            //private const string IAdminIndexBaseViewModel = "IAdminIndexBaseViewModel";


            AdminOlCreateName = _modelName + "AdminOlCreateViewModel";
            AdminOlCreateFileName = _modelName + "AdminOlCreateViewModel.cs";
            AdminOlCreateRoot = _viewModelRoot + @"\" + AdminOlCreateFileName;

            AdminOlEditName = _modelName + "AdminOlEditViewModel";
            AdminOlEditFileName = _modelName + "AdminOlEditViewModel.cs";
            AdminOlEditRoot = _viewModelRoot + @"\" + AdminOlEditFileName;



            _controllerName = txtController.Text;
            _viewRoot = _modelRoot + @"\Views" + @"\" + _controllerName + @"\Views";
            _viewOlRoot = _modelRoot + @"\ViewsOl" + @"\" + _controllerName;

            AdminIndexViewFileName = "Index.cshtml";
            AdminListViewFileName = "_List.cshtml";
            AdminIndexViewRoot = _viewRoot + @"\" + AdminIndexViewFileName;
            AdminListViewRoot = _viewRoot + @"\" + AdminListViewFileName;
            AdminOlIndexViewRoot = _viewOlRoot + @"\" + AdminIndexViewFileName;
            AdminOlListViewRoot = _viewOlRoot + @"\" + AdminListViewFileName;
            ModelPersianName = txtModelPersianName.Text;
            AreaName = txtArea.Text;

            AdminCreateViewRoot = _viewRoot + @"\" + AdminCreateViewFileName;

            AdminEditViewRoot = _viewRoot + @"\" + AdminEditViewFileName;
            AdminDetailsViewRoot = _viewRoot + @"\" + AdminDetailsViewFileName;

            _jSRoot = _modelRoot + @"\JS" + @"\" + _controllerName;
            AdminIndexJSRoot = _jSRoot + @"\" + AdminIndexJSFileName;
            AdminOlIndexJSRoot = _jSOlRoot + @"\" + AdminIndexJSFileName;
            AdminCreateJSRoot = _jSRoot + @"\" + AdminCreateJSFileName;
            AdminEditJSRoot = _jSRoot + @"\" + AdminEditJSFileName;


        }

        private SqlDatabaseAdapter SqlDatabaseAdapter
        {
            get
            {
                var conString = "server=" + txtServer.Text + ";uid=" + txtUserName.Text + ";pwd=" + txtPassword.Text +
                                "; ";
                var sda = new SqlDatabaseAdapter(conString);
                return sda;
            }
        }

        private void FillIAdminIndexList()
        {
            //StackPanelField.Children.Clear();

            FilldgIAdminIndexList();



        }

        private ObservableCollection<IAdminIndexList> _selectedFieldForIAdminIndexListObser =
            new ObservableCollection<IAdminIndexList>();

        private void FilldgIAdminIndexList()
        {
            var counter = 0;
            _selectedFieldForIAdminIndexListObser =
                new ObservableCollection<IAdminIndexList>(GenerateFiledList.Select(x => new IAdminIndexList()
                {
                    Field = x,
                    Display = x,
                    Order = counter++,
                }).ToList());
            dgIAdminIndexList.ItemsSource = _selectedFieldForIAdminIndexListObser;
            FillAdminIndexList();
            FillAdminIndexView();
        }

        private void btnUp_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedColumn = dgIAdminIndexList.SelectedItem as IAdminIndexList;

            if (selectedColumn == null) return;
            if (selectedColumn.Order == 0) return;

            GenerateFiledList.Swap(selectedColumn.Order, selectedColumn.Order - 1);
            FilldgIAdminIndexList();

        }

        private void btnDown_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedColumn = dgIAdminIndexList.SelectedItem as IAdminIndexList;

            if (selectedColumn == null) return;
            if (selectedColumn.Order == GenerateFiledList.Count - 1) return;

            GenerateFiledList.Swap(selectedColumn.Order, selectedColumn.Order + 1);
            FilldgIAdminIndexList();
        }


        private void FillAdminIndexList()
        {
            FilldgAdminIndexList();
        }

        private ObservableCollection<AdminIndexList> _adminIndexListList = new ObservableCollection<AdminIndexList>();

        private void FilldgAdminIndexList()
        {
            _adminIndexListList =
                new ObservableCollection<AdminIndexList>(GenerateFiledList.Select(x => new AdminIndexList()
                {
                    Field = x,
                    IsKey = false,
                    IsFilterText = false,
                    FilterText = 0,
                    IsCleanHtml = false,
                    IsBoolean = false,
                    NoMessage = "",
                    YesMessage = ""
                }).ToList());
            dgAdminIndexList.ItemsSource = _adminIndexListList;
        }


        private void FillAdminIndexView()
        {
            FilldgAdminIndexView();
        }

        private AdminIndexView _AdminIndexViewList = new AdminIndexView()
        {
            isHaveHeadButton = true,
            headButtonCssClass = "col-lg-2 col-sm-2 col-xs-2"
        };

        private void FilldgAdminIndexView()
        {

            _AdminIndexViewList._items =
                new ObservableCollection<AdminIndexViewItem>(GenerateFiledList.Select(x => new AdminIndexViewItem()
                {
                    Key = x,
                    headTcss = " col-lg-3 col-sm-3 col-xs-4  ",
                    headDivcss = " col-sm-9 ",
                    headIcss = " col-sm-2 "
                }).ToList());
            dgAdminIndexView.ItemsSource = _AdminIndexViewList._items;
        }


        // IAdminIndexList
        private void BtnGenerateIAdminIndexList_OnClick(object sender, RoutedEventArgs e)
        {
            FillRootDirectory();
            CheckTempFolder();
            CheckModelFolder();
            CheckViewModelFolder();
            CheckContractsFolder();
            CreateFile(IAdminIndexListRoot);

            WriteIAdminIndexListElements(IAdminIndexListRoot, _tableName);
        }

        private void WriteIAdminIndexListElements(string fileName, string tableName)
        {
            WriteIAdminIndexListUsings(fileName);
            var sda = SqlDatabaseAdapter;

            using (var sw = File.AppendText(fileName))
            {
                var columns = sda.GetTablesColumn(tableName, cmbDatabases.SelectedValue.ToString());

                //namespace
                sw.WriteLine("namespace " + txtProjectName.Text + "." + "ViewModels" + "." + _modelName + "." +
                             "Contracts");
                sw.WriteLine("{");
                sw.WriteLine("    public interface " + IAdminIndexListName);
                sw.WriteLine("    {");
                sw.WriteLine("");

                foreach (DataRow property in columns.Rows)
                {
                    if (_selectedFieldForIAdminIndexListObser.Any(x => x.Field == property["Name"].ToString()))
                    {
                        var currentIAdmin =
                            _selectedFieldForIAdminIndexListObser.First(x => x.Field == property["Name"].ToString());
                        //sw.WriteLine("        " + property["Type"] + " " + property["Name"] + " { get; set; }");
                        sw.WriteLine("        " + (currentIAdmin.IsKey ? property["Type"] : "string") + " " +
                                     property["Name"] + " { get; set; }");
                    }
                }

                sw.WriteLine("    }");
                sw.WriteLine("}");
                sw.WriteLine("");

            }
        }

        private void WriteIAdminIndexListUsings(string fileName)
        {

            using (var sw = File.AppendText(fileName))
            {
                sw.WriteLine("//Generate by CodeGenerator 2.0.0");

                foreach (var str in IAdminIndexListNameSpaces)
                    sw.WriteLine(str);
                sw.WriteLine("");

            }
        }


        // AdminIndexList
        private void btnGenerateAdminIndexList_Click(object sender, RoutedEventArgs e)
        {
            FillRootDirectory();
            CheckTempFolder();
            CheckModelFolder();
            CheckViewModelFolder();
            CreateFile(AdminIndexListRoot);

            WriteAdminIndexListElements(AdminIndexListRoot, _tableName);

            if (chkHaveOtherLanguage.IsChecked == true)
                WriteAdminIndexListOlElements(AdminIndexListOlRoot, _tableName);

            if (chkHaveAdminIndexViewModel.IsChecked == true)
                WriteAdminIndexElements(AdminIndexRoot);

            if (chkHaveAdminOlIndexViewModel.IsChecked == true)
                BtnGenerateAdminOlIndexList();
        }

        private void WriteAdminIndexListElements(string fileName, string tableName)
        {
            WriteAdminIndexListUsings(fileName);
            var sda = SqlDatabaseAdapter;

            using (var sw = File.AppendText(fileName))
            {
                var columns = sda.GetTablesColumn(tableName, cmbDatabases.SelectedValue.ToString());

                //namespace
                sw.WriteLine("namespace " + txtProjectName.Text + "." + "ViewModels" + "." + _modelName);
                sw.WriteLine("{");
                sw.WriteLine("    public class " + AdminIndexListName + " : " + IAdminIndexListBaseViewModel + "," +
                             IAdminIndexListName);
                sw.WriteLine("    {");
                sw.WriteLine("");

                WriteBaseId(sw);

                foreach (DataRow property in columns.Rows)
                {
                    if (_selectedFieldForIAdminIndexListObser.Any(x => x.Field == property["Name"].ToString()))
                    {
                        var currentIAdmin =
                            _selectedFieldForIAdminIndexListObser.First(x => x.Field == property["Name"].ToString());
                        var currentAdmin = _adminIndexListList.First(x => x.Field == property["Name"].ToString());

                        WriteDisplayName(sw, currentIAdmin.Display);
                        if (currentAdmin.IsFilterText || currentAdmin.IsCleanHtml || currentAdmin.IsBoolean)
                        {
                            var privateName = GeneratePrivateName(property);


                            sw.WriteLine("        public " + (currentAdmin.IsBoolean ? "string" : property["Type"]) +
                                         " " + property["Name"]);
                            sw.WriteLine("        {");
                            sw.WriteLine("            set { " + privateName + " = value; }");
                            sw.WriteLine("            get");
                            sw.WriteLine("            {");

                            if (currentAdmin.IsBoolean)
                            {
                                WriteBooleanProperty(sw, privateName, currentAdmin.YesMessage, currentAdmin.NoMessage);
                            }
                            else
                            {
                                if (currentAdmin.IsCleanHtml)
                                {
                                    IsCleanHtml(currentAdmin.FilterText, sw, privateName);
                                }
                                else
                                    FilterText(currentAdmin.FilterText, sw, privateName);
                            }

                            sw.WriteLine("            }");
                            sw.WriteLine("        }");
                            sw.WriteLine("        private " + (currentAdmin.IsBoolean ? "string" : property["Type"]) +
                                         " " + privateName + ";");
                        }
                        else
                        {
                            WriteNormalProperty(sw, currentAdmin.IsKey, property);
                        }
                        sw.WriteLine("");
                    }
                }

                sw.WriteLine("    }");
                sw.WriteLine("}");
                sw.WriteLine("");

            }
        }

        private void WriteAdminIndexListOlElements(string fileName, string tableName)
        {
            WriteAdminIndexListUsings(fileName);
            var sda = SqlDatabaseAdapter;

            using (var sw = File.AppendText(fileName))
            {
                var columns = sda.GetTablesColumn(tableName, cmbDatabases.SelectedValue.ToString());

                //namespace
                sw.WriteLine("namespace " + txtProjectName.Text + "." + "ViewModels" + "." + _modelName);
                sw.WriteLine("{");
                sw.WriteLine("    public class " + AdminIndexListOlName + " : " + IAdminIndexListBaseViewModel + "," +
                             IAdminIndexListName);
                sw.WriteLine("    {");
                sw.WriteLine("");

                WriteBaseId(sw);

                foreach (DataRow property in columns.Rows)
                {
                    if (_selectedFieldForIAdminIndexListObser.Any(x => x.Field == property["Name"].ToString()))
                    {
                        var currentIAdmin =
                            _selectedFieldForIAdminIndexListObser.First(x => x.Field == property["Name"].ToString());
                        var currentAdmin = _adminIndexListList.First(x => x.Field == property["Name"].ToString());

                        WriteDisplayName(sw, currentIAdmin.Display);
                        if (currentAdmin.IsFilterText || currentAdmin.IsCleanHtml || currentAdmin.IsBoolean)
                        {
                            var privateName = GeneratePrivateName(property);


                            sw.WriteLine("        public " + (currentAdmin.IsBoolean ? "string" : property["Type"]) +
                                         " " + property["Name"]);
                            sw.WriteLine("        {");
                            sw.WriteLine("            set { " + privateName + " = value; }");
                            sw.WriteLine("            get");
                            sw.WriteLine("            {");

                            if (currentAdmin.IsBoolean)
                            {
                                WriteBooleanProperty(sw, privateName, currentAdmin.YesMessage, currentAdmin.NoMessage);
                            }
                            else
                            {
                                if (currentAdmin.IsCleanHtml)
                                {
                                    IsCleanHtml(currentAdmin.FilterText, sw, privateName);
                                }
                                else
                                    FilterText(currentAdmin.FilterText, sw, privateName);
                            }

                            sw.WriteLine("            }");
                            sw.WriteLine("        }");
                            sw.WriteLine("        private " + (currentAdmin.IsBoolean ? "string" : property["Type"]) +
                                         " " + privateName + ";");
                        }
                        else
                        {
                            WriteNormalProperty(sw, currentAdmin.IsKey, property);
                        }
                        sw.WriteLine("");
                    }
                }

                sw.WriteLine("    }");
                sw.WriteLine("}");
                sw.WriteLine("");

            }
        }

        private void WriteAdminIndexElements(string fileName)
        {
            WriteAdminIndexUsings(fileName);

            using (var sw = File.AppendText(fileName))
            {

                //namespace
                sw.WriteLine("namespace " + txtProjectName.Text + "." + "ViewModels" + "." + _modelName);
                sw.WriteLine("{");
                sw.WriteLine("    public class " + AdminIndexName + " : " + IAdminIndexBaseViewModel);
                sw.WriteLine("    {");


                sw.WriteLine("        public int PageSize { get; set; }");
                sw.WriteLine("");
                sw.WriteLine("        public int PageNum { get; set; }");
                sw.WriteLine("");
                sw.WriteLine("        public long RowCount { get; set; }");
                sw.WriteLine("");
                sw.WriteLine("        public int CurrentRowCount { get; set; }");
                sw.WriteLine("");
                sw.WriteLine("        public string SortFieldName { get; set; }");
                sw.WriteLine("");
                sw.WriteLine("        public bool IsAsc { get; set; }");
                sw.WriteLine("");
                sw.WriteLine("        public string[] FilterFieldsName { get; set; }");
                sw.WriteLine("");
                sw.WriteLine("        public IEnumerable<" + AdminIndexListName + "> RowList { get; set; }");


                sw.WriteLine("    }");
                sw.WriteLine("}");
                sw.WriteLine("");

            }
        }

        private void WriteNormalProperty(StreamWriter sw, bool IsKey, DataRow property)
        {
            sw.WriteLine("        public " + (IsKey ? property["Type"] : "string") + " " + property["Name"] +
                         " { get; set; }");
        }

        private void WriteAdminIndexListUsings(string fileName)
        {
            using (var sw = File.AppendText(fileName))
            {
                sw.WriteLine("//Generate by CodeGenerator 2.0.0");

                foreach (var str in AdminIndexListNameSpaces)
                    sw.WriteLine(str);
                sw.WriteLine("");

            }
        }

        private void WriteAdminIndexUsings(string fileName)
        {
            using (var sw = File.AppendText(fileName))
            {
                sw.WriteLine("//Generate by CodeGenerator 2.0.0");

                foreach (var str in AdminIndexNameSpaces)
                    sw.WriteLine(str);
                sw.WriteLine("");

            }
        }


        private void FilterText(int FilterText, StreamWriter sw, string privateName)
        {
            sw.WriteLine("                return " + privateName + " != null ? " + privateName + ".Length > " +
                         FilterText + " ? (" + privateName + ".Substring(0, " + FilterText +
                         ") + \"...\") : " + privateName + " : " + privateName + ";");
        }

        private void IsCleanHtml(int FilterText, StreamWriter sw, string privateName)
        {
            sw.WriteLine("                var cleanText = Html.HtmlDel(" + privateName + ");");
            sw.WriteLine("                return cleanText != null ? cleanText.Length > " + FilterText +
                         " ? (cleanText.Substring(0, " + FilterText + ") + \"...\") : cleanText : " +
                         privateName + ";");
        }

        private void WriteBooleanProperty(StreamWriter sw, string privateName, string YesMessage, string NoMessage)
        {
            sw.WriteLine("                if (" + privateName + " != null)");
            sw.WriteLine("                    if (" + privateName + ".ToLower() == \"true\" || " + privateName +
                         ".ToLower() == \"false\")");
            sw.WriteLine("                        return Convert.ToBoolean(" + privateName + ") ? \"" + YesMessage +
                         "\" : \"" + NoMessage + "\";");
            sw.WriteLine("                return " + privateName + ";");
        }

        private string GeneratePrivateName(DataRow property)
        {
            var privateName = "_" + property["Name"].ToString().Substring(0, 1).ToLower() +
                              property["Name"].ToString().Substring(1);
            return privateName;
        }

        private void WriteDisplayName(StreamWriter sw, string display)
        {
            sw.WriteLine("        [Display(Name = @\"" + display + "\")]");
        }

        private void WriteBaseId(StreamWriter sw)
        {
            sw.WriteLine("        public override int BaseId");
            sw.WriteLine("        {");
            sw.WriteLine("            set { _baseId = value; }");
            sw.WriteLine("            get { return Id; }");
            sw.WriteLine("        }");
            sw.WriteLine("        private int _baseId;");
            sw.WriteLine("");
        }


        // AdminCreate
        private void BtnSelectCreateField_OnClick(object sender, RoutedEventArgs e)
        {
            if (SelectedTable.Length == 0)
                return;

            CallSelectionFieldWindow();

            FillAdminCreate();
        }

        private void FillAdminCreate()
        {
            FilldgAdminCreate();
            FillAdminCreateView();
        }

        private ObservableCollection<AdminCreate> _selectedFieldForAdminCreateObser =
            new ObservableCollection<AdminCreate>();

        private void FilldgAdminCreate()
        {
            var counter = 0;
            _selectedFieldForAdminCreateObser =
                new ObservableCollection<AdminCreate>(GenerateFiledList.Select(x => new AdminCreate()
                {
                    Field = x,
                    Display = x,

                }).ToList());
            TabControlAdminCreateViewModel.ItemsSource = _selectedFieldForAdminCreateObser;

        }

        private void BtnGenerateAdminCreate_OnClick(object sender, RoutedEventArgs e)
        {
            FillRootDirectory();
            CheckTempFolder();
            CheckModelFolder();
            CheckViewModelFolder();
            CreateFile(AdminCreateRoot);

            WriteAdminCreateElements(AdminCreateRoot, _tableName);
            if (chkGenerateEditVM.IsChecked == true)
                BtnGenerateAdminEdit();
        }

        private void WriteAdminCreateElements(string fileName, string tableName)
        {
            WriteAdminCreateUsings(fileName);
            var sda = SqlDatabaseAdapter;

            using (var sw = File.AppendText(fileName))
            {
                var columns = sda.GetTablesColumn(tableName, cmbDatabases.SelectedValue.ToString());

                //namespace
                sw.WriteLine("namespace " + txtProjectName.Text + "." + "ViewModels" + "." + _modelName);
                sw.WriteLine("{");
                sw.WriteLine("    public class " + AdminCreateName + (chkVmHaveOtherSeo.IsChecked == true ? " : BaseSeoViewModel" : ""));
                sw.WriteLine("    {");
                sw.WriteLine("");

                foreach (DataRow property in columns.Rows)
                {
                    if (_selectedFieldForAdminCreateObser.Any(x => x.Field == property["Name"].ToString()))
                    {
                        var currentAdmin =
                            _selectedFieldForAdminCreateObser.First(x => x.Field == property["Name"].ToString());

                        WriteDisplayName(sw, currentAdmin.Display);

                        WriteFilterRequired(currentAdmin.IsRequired, currentAdmin.RequiredMessage, sw);

                        WriteFilterStringLength(currentAdmin.IsStringLength, currentAdmin.MinStringLength,
                            currentAdmin.MaxStringLength, currentAdmin.StringLengthMessage, sw);

                        WriteFilterAllowHtml(currentAdmin.IsAllowHtml, sw);

                        WriteFilterScaffoldColumnFalse(currentAdmin.IsScaffoldColumnFalse, sw);

                        WriteFilterPhone(currentAdmin.IsPhone, currentAdmin.PhoneMessage, sw);

                        WriteFilterRange(currentAdmin.IsRange, currentAdmin.MinRange, currentAdmin.MaxRange,
                            currentAdmin.RangeMessage, sw);

                        WriteFilterRegularExpression(currentAdmin.IsRegularExpression, currentAdmin.Pattern,
                            currentAdmin.RegularExpressionMessage, sw);

                        WriteNormalProperty(sw, property);
                        if (currentAdmin.IsLanguage)
                        {
                            WriteLanguageProperty(sw, property);
                        }
                        sw.WriteLine("");
                    }
                }

                sw.WriteLine("    }");
                sw.WriteLine("}");
                sw.WriteLine("");

            }
        }

        private void WriteAdminCreateUsings(string fileName)
        {
            using (var sw = File.AppendText(fileName))
            {
                sw.WriteLine("//Generate by CodeGenerator 2.0.0");

                foreach (var str in AdminCreateNameSpaces)
                    sw.WriteLine(str);
                sw.WriteLine("");

            }
        }

        private void WriteAdminCreateOlElements(string fileName, string tableName)
        {
            WriteAdminCreateUsings(fileName);
            var sda = SqlDatabaseAdapter;

            using (var sw = File.AppendText(fileName))
            {
                var columns = sda.GetTablesColumn(tableName, cmbDatabases.SelectedValue.ToString());

                //namespace
                sw.WriteLine("namespace " + txtProjectName.Text + "." + "ViewModels" + "." + _modelName);
                sw.WriteLine("{");
                sw.WriteLine("    public class " + AdminCreateName + (chkVmHaveOtherSeo.IsChecked == true ? " : BaseSeoViewModel" : ""));
                sw.WriteLine("    {");
                sw.WriteLine("");

                foreach (DataRow property in columns.Rows)
                {
                    if (_selectedFieldForAdminCreateObser.Any(x => x.Field == property["Name"].ToString()))
                    {
                        var currentAdmin =
                            _selectedFieldForAdminCreateObser.First(x => x.Field == property["Name"].ToString());

                        WriteDisplayName(sw, currentAdmin.Display);

                        WriteFilterRequired(currentAdmin.IsRequired, currentAdmin.RequiredMessage, sw);

                        WriteFilterStringLength(currentAdmin.IsStringLength, currentAdmin.MinStringLength,
                            currentAdmin.MaxStringLength, currentAdmin.StringLengthMessage, sw);

                        WriteFilterAllowHtml(currentAdmin.IsAllowHtml, sw);

                        WriteFilterScaffoldColumnFalse(currentAdmin.IsScaffoldColumnFalse, sw);

                        WriteFilterPhone(currentAdmin.IsPhone, currentAdmin.PhoneMessage, sw);

                        WriteFilterRange(currentAdmin.IsRange, currentAdmin.MinRange, currentAdmin.MaxRange,
                            currentAdmin.RangeMessage, sw);

                        WriteFilterRegularExpression(currentAdmin.IsRegularExpression, currentAdmin.Pattern,
                            currentAdmin.RegularExpressionMessage, sw);

                        WriteNormalProperty(sw, property);
                        if (currentAdmin.IsLanguage)
                        {
                            WriteLanguageProperty(sw, property);
                        }
                        sw.WriteLine("");
                    }
                }

                sw.WriteLine("    }");
                sw.WriteLine("}");
                sw.WriteLine("");

            }
        }

        private void WriteAdminCreateOlUsings(string fileName)
        {
            using (var sw = File.AppendText(fileName))
            {
                sw.WriteLine("//Generate by CodeGenerator 2.0.0");

                foreach (var str in AdminCreateNameSpaces)
                    sw.WriteLine(str);
                sw.WriteLine("");

            }
        }



        private void WriteFilterRegularExpression(bool isRegularExpression, string pattern,
            string regularExpressionMessage, StreamWriter sw)
        {
            if (isRegularExpression)
                sw.WriteLine("        [RegularExpression(\"" + pattern +
                             "\", ErrorMessage = @\"" + regularExpressionMessage + "\")]");
        }

        private void WriteFilterRange(bool isRange, int minRange, int maxRange, string rangeMessage, StreamWriter sw)
        {
            if (isRange)
                sw.WriteLine("        [Range(" + minRange + ", " + maxRange +
                             "," + " ErrorMessage = @\"" + rangeMessage + "\")]");
        }

        private void WriteFilterPhone(bool isPhone, string phoneMessage, StreamWriter sw)
        {
            if (isPhone)
                sw.WriteLine("        [Phone(ErrorMessage = @\"" + phoneMessage + "\")]");
        }

        private void WriteFilterScaffoldColumnFalse(bool isScaffoldColumnFalse, StreamWriter sw)
        {
            if (isScaffoldColumnFalse)
                sw.WriteLine("        [ScaffoldColumn(false)]");
        }

        private void WriteFilterAllowHtml(bool isAllowHtml, StreamWriter sw)
        {
            if (isAllowHtml)
                sw.WriteLine("        [AllowHtml]");
        }

        private void WriteFilterStringLength(bool isStringLength, int minStringLength, int maxStringLength,
            string stringLengthMessage, StreamWriter sw)
        {
            if (isStringLength)
            {
                sw.WriteLine("        [StringLength(" + maxStringLength + "," +
                             (minStringLength > 0 ? " MinimumLength = " + minStringLength + ", " : "") +
                             " ErrorMessage = @\"" + stringLengthMessage + "\")]");
            }
        }

        private void WriteFilterRequired(bool isRequired, string requiredMessage, StreamWriter sw)
        {
            if (isRequired)
                sw.WriteLine("        [Required(ErrorMessage = @\"" + requiredMessage + "\")]");
        }

        private void WriteNormalProperty(StreamWriter sw, DataRow property)
        {
            sw.WriteLine("        public " + (property["Type"]) + " " + property["Name"] + " { get; set; }");
        }

        private void WriteImageProperty(StreamWriter sw, DataRow property)
        {
            sw.WriteLine("        [ScaffoldColumn(false)]");
            sw.WriteLine("        public bool Is" + property["Name"] + "Remove { get; set; }");
            sw.WriteLine("        [ScaffoldColumn(false)]");
            sw.WriteLine("        public string " + property["Name"] + "New { get; set; }");
        }

        private void WriteLanguageProperty(StreamWriter sw, DataRow property)
        {
            sw.WriteLine("        public IList<LanguageViewModel> LanguageList;");
        }


        private void FillAdminCreateView()
        {
            FilldgAdminCreateView();
        }

        private AdminCreateView _AdminCreateViewList = new AdminCreateView()
        {
            isHaveHeadButton = true,
            headButtonCssClass = "col-lg-2 col-sm-2 col-xs-2"
        };

        private void FilldgAdminCreateView()
        {

            _AdminCreateViewList._items =
                new ObservableCollection<AdminCreateViewItem>(GenerateFiledList.Select(x => new AdminCreateViewItem()
                {
                    Key = x,
                    //headTcss = " col-lg-3 col-sm-3 col-xs-4  ",
                    //headDivcss = " col-sm-9 ",
                    //headIcss = " col-sm-2 "
                }).ToList());
            dgAdminCreateView.ItemsSource = _AdminCreateViewList._items;
        }

        // AdminEdit
        private void BtnSelectEditField_OnClick(object sender, RoutedEventArgs e)
        {
            //if (SelectedTable.Length == 0)
            //    return;

            //CallSelectionFieldWindow();

            //FillAdminEdit();
        }

        //private void FillAdminEdit()
        //{
        //    FilldgAdminEdit();
        //}

        //private ObservableCollection<AdminCreate> _selectedFieldForAdminEditObser = new ObservableCollection<AdminCreate>();

        //private void FilldgAdminEdit()
        //{
        //    _selectedFieldForAdminEditObser =
        //        new ObservableCollection<AdminCreate>(GenerateFiledList.Select(x => new AdminCreate()
        //        {
        //            Field = x,
        //            Display = x,
        //        }).ToList());
        //    //TabControlAdminEditViewModel.ItemsSource = _selectedFieldForAdminEditObser;

        //}

        private void BtnGenerateAdminEdit()
        {
            FillRootDirectory();
            CheckTempFolder();
            CheckModelFolder();
            CheckViewModelFolder();
            CreateFile(AdminEditRoot);

            WriteAdminEditElements(AdminEditRoot, _tableName);
        }

        private void WriteAdminEditElements(string fileName, string tableName)
        {
            WriteAdminEditUsings(fileName);
            var sda = SqlDatabaseAdapter;

            using (var sw = File.AppendText(fileName))
            {
                var columns = sda.GetTablesColumn(tableName, cmbDatabases.SelectedValue.ToString());

                //namespace
                sw.WriteLine("namespace " + txtProjectName.Text + "." + "ViewModels" + "." + _modelName);
                sw.WriteLine("{");
                //sw.WriteLine("    public class " + AdminEditName);
                sw.WriteLine("    public class " + AdminEditName + (chkVmHaveOtherSeo.IsChecked == true ? " : BaseSeoViewModel" : ""));
                sw.WriteLine("    {");
                sw.WriteLine("");

                foreach (DataRow property in columns.Rows)
                {
                    if (_selectedFieldForAdminCreateObser.Any(x => x.Field == property["Name"].ToString()))
                    {
                        var currentAdmin =
                            _selectedFieldForAdminCreateObser.First(x => x.Field == property["Name"].ToString());

                        WriteDisplayName(sw, currentAdmin.Display);

                        WriteFilterRequired(currentAdmin.IsRequired, currentAdmin.RequiredMessage, sw);

                        WriteFilterStringLength(currentAdmin.IsStringLength, currentAdmin.MinStringLength,
                            currentAdmin.MaxStringLength, currentAdmin.StringLengthMessage, sw);

                        WriteFilterAllowHtml(currentAdmin.IsAllowHtml, sw);

                        WriteFilterScaffoldColumnFalse(currentAdmin.IsScaffoldColumnFalse, sw);

                        WriteFilterPhone(currentAdmin.IsPhone, currentAdmin.PhoneMessage, sw);

                        WriteFilterRange(currentAdmin.IsRange, currentAdmin.MinRange, currentAdmin.MaxRange,
                            currentAdmin.RangeMessage, sw);

                        WriteFilterRegularExpression(currentAdmin.IsRegularExpression, currentAdmin.Pattern,
                            currentAdmin.RegularExpressionMessage, sw);

                        WriteNormalProperty(sw, property);

                        if (currentAdmin.IsImage)
                            WriteImageProperty(sw, property);

                        if (currentAdmin.IsLanguage)
                            WriteLanguageProperty(sw, property);

                        sw.WriteLine("");
                    }
                }

                sw.WriteLine("    }");
                sw.WriteLine("}");
                sw.WriteLine("");

            }
        }

        private void WriteAdminEditUsings(string fileName)
        {
            using (var sw = File.AppendText(fileName))
            {
                sw.WriteLine("//Generate by CodeGenerator 2.0.0");

                foreach (var str in AdminEditNameSpaces)
                    sw.WriteLine(str);
                sw.WriteLine("");

            }
        }


        // AdminDetails
        private void BtnSelectDetailsField_OnClick(object sender, RoutedEventArgs e)
        {
            if (SelectedTable.Length == 0)
                return;

            CallSelectionFieldWindow();

            FillAdminDetails();
        }

        private void FillAdminDetails()
        {
            FilldgAdminDetails();
        }

        private ObservableCollection<AdminDetails> _selectedFieldForAdminDetailsObser =
            new ObservableCollection<AdminDetails>();

        private void FilldgAdminDetails()
        {
            _selectedFieldForAdminDetailsObser =
                new ObservableCollection<AdminDetails>(GenerateFiledList.Select(x => new AdminDetails()
                {
                    Field = x,
                    Display = x,
                }).ToList());
            dgAdminDetails.ItemsSource = _selectedFieldForAdminDetailsObser;

        }

        private void BtnGenerateAdminDetails_OnClick(object sender, RoutedEventArgs e)
        {
            FillRootDirectory();
            CheckTempFolder();
            CheckModelFolder();
            CheckViewModelFolder();
            CheckContractsFolder();
            CreateFile(AdminDetailsRoot);

            WriteAdminDetailsElements(AdminDetailsRoot, _tableName);
            if (chkAdminDetailsView.IsChecked == true)
            {
                CheckViewFolder();
                CreateFile(AdminDetailsViewRoot);
                WriteAdminDetailsViewElements(AdminDetailsViewRoot, _tableName);
            }
        }

        private void WriteAdminDetailsElements(string fileName, string tableName)
        {
            WriteAdminDetailsUsings(fileName);
            var sda = SqlDatabaseAdapter;

            using (var sw = File.AppendText(fileName))
            {
                var columns = sda.GetTablesColumn(tableName, cmbDatabases.SelectedValue.ToString());

                //namespace
                sw.WriteLine("namespace " + txtProjectName.Text + "." + "ViewModels" + "." + _modelName);
                sw.WriteLine("{");
                //sw.WriteLine("    public class " + AdminDetailsName);
                sw.WriteLine("    public class " + AdminDetailsName + (chkAdminDetailsHaveSeo.IsChecked == true ? " : BaseSeoViewModel" : ""));
                sw.WriteLine("    {");
                sw.WriteLine("");

                foreach (DataRow property in columns.Rows)
                {
                    if (_selectedFieldForAdminDetailsObser.Any(x => x.Field == property["Name"].ToString()))
                    {
                        var currentAdmin =
                            _selectedFieldForAdminDetailsObser.First(x => x.Field == property["Name"].ToString());
                        //if (!currentAdmin.IsKey)
                        WriteDisplayName(sw, currentAdmin.Display);
                        sw.WriteLine("        public " + property["Type"] + " " + property["Name"] + " { get; set; }");
                        sw.WriteLine("");
                    }
                }
                if (chkHaveOtherLanguageList.IsChecked == true)
                {
                    sw.WriteLine("");
                    sw.WriteLine("        public System.Collections.Generic.List<" + _modelName +
                                 "AdminOlDetailsViewModel> OlDetails;");
                }
                sw.WriteLine("    }");
                sw.WriteLine("}");
                sw.WriteLine("");

            }
        }

        private void WriteAdminDetailsUsings(string fileName)
        {

            using (var sw = File.AppendText(fileName))
            {
                sw.WriteLine("//Generate by CodeGenerator 2.0.0");

                foreach (var str in AdminDetailsNameSpaces)
                    sw.WriteLine(str);
                sw.WriteLine("");

            }
        }


        // AdminOlIndexList
        private void BtnSelectAdminOlIndexListField_OnClick(object sender, RoutedEventArgs e)
        {
            if (SelectedTable.Length == 0)
                return;

            CallSelectionFieldWindow();

            FillAdminOlIndexList();
        }

        private void FillAdminOlIndexList()
        {
            FilldgAdminOlIndexList();
        }

        private ObservableCollection<AdminOlIndexList> _selectedFieldForAdminOlIndexListObser =
            new ObservableCollection<AdminOlIndexList>();

        private void FilldgAdminOlIndexList()
        {
            //var counter = 0;
            //_selectedFieldForAdminOlIndexListObser =
            //    new ObservableCollection<AdminOlIndexList>(GenerateFiledList.Select(x => new AdminOlIndexList()
            //    {
            //        Field = x,
            //        Display = x,
            //        Order = counter++,
            //        IsKey = false,
            //        IsFilterText = false,
            //        FilterText = 0,
            //        IsCleanHtml = false,
            //        IsBoolean = false,
            //        NoMessage = "",
            //        YesMessage = ""
            //    }).ToList());
            //dgAdminOlIndexList.ItemsSource = _selectedFieldForAdminOlIndexListObser;

        }

        //private void BtnGenerateAdminOlIndexList_OnClick(object sender, RoutedEventArgs e)
        private void BtnGenerateAdminOlIndexList()
        {
            FillRootDirectory();
            CheckTempFolder();
            CheckModelFolder();
            CheckViewModelFolder();
            CreateFile(AdminOlIndexListRoot);

            WriteAdminOlIndexListElements(AdminOlIndexListRoot, _tableName);

            //if (chkHaveOtherLanguage.IsChecked == true)
            //    WriteAdminOlIndexListOlElements(AdminOlIndexListOlRoot, _tableName);

            //if (chkHaveAdminOlIndexViewModel.IsChecked == true)
            if (chkHaveAdminIndexViewModel.IsChecked == true)
                WriteAdminOlIndexElements(AdminOlIndexRoot);
        }

        private void WriteAdminOlIndexListElements(string fileName, string tableName)
        {
            WriteAdminOlIndexListUsings(fileName);
            var sda = SqlDatabaseAdapter;

            using (var sw = File.AppendText(fileName))
            {
                var columns = sda.GetTablesColumn(tableName, cmbDatabases.SelectedValue.ToString());

                //namespace
                sw.WriteLine("namespace " + txtProjectName.Text + "." + "ViewModels" + "." + _modelName);
                sw.WriteLine("{");
                sw.WriteLine("    public class " + AdminOlIndexListName + " : " + IAdminIndexListBaseViewModel);
                sw.WriteLine("    {");
                sw.WriteLine("");

                WriteBaseId(sw);

                foreach (DataRow property in columns.Rows)
                {
                    if (_selectedFieldForIAdminIndexListObser.Any(x => x.Field == property["Name"].ToString() &&
                        ((string)x.UseType.Content == UseType.justOtherLanguage.ToString() || (string)x.UseType.Content == UseType.Multiple.ToString())))
                    {
                        var currentIAdmin =
                            _selectedFieldForIAdminIndexListObser.First(x => x.Field == property["Name"].ToString());
                        var currentAdmin =
                            _adminIndexListList.First(x => x.Field == property["Name"].ToString());

                        if (currentAdmin.IsKey)
                        {
                            WriteNormalProperty(sw, currentAdmin.IsKey, property);
                            sw.WriteLine("");
                            continue;
                        }

                        WriteDisplayName(sw, currentIAdmin.Display);
                        if (currentAdmin.IsFilterText || currentAdmin.IsCleanHtml || currentAdmin.IsBoolean)
                        {
                            var privateName = GeneratePrivateName(property);


                            sw.WriteLine("        public " + (currentAdmin.IsBoolean ? "string" : property["Type"]) +
                                         " " + property["Name"]);
                            sw.WriteLine("        {");
                            sw.WriteLine("            set { " + privateName + " = value; }");
                            sw.WriteLine("            get");
                            sw.WriteLine("            {");

                            if (currentAdmin.IsBoolean)
                            {
                                WriteBooleanProperty(sw, privateName, currentAdmin.YesMessage, currentAdmin.NoMessage);
                            }
                            else
                            {
                                if (currentAdmin.IsCleanHtml)
                                {
                                    IsCleanHtml(currentAdmin.FilterText, sw, privateName);
                                }
                                else
                                    FilterText(currentAdmin.FilterText, sw, privateName);
                            }

                            sw.WriteLine("            }");
                            sw.WriteLine("        }");
                            sw.WriteLine("        private " + (currentAdmin.IsBoolean ? "string" : property["Type"]) +
                                         " " + privateName + ";");
                        }
                        else
                        {
                            WriteNormalProperty(sw, currentAdmin.IsKey, property);
                        }
                        sw.WriteLine("");
                    }
                }

                sw.WriteLine("");
                sw.WriteLine("        [Display(Name = @\"" + "زبان" + "\")]");
                sw.WriteLine("        public string Language");
                sw.WriteLine("        {");
                sw.WriteLine("            set { _language = value; }");
                sw.WriteLine("            get");
                sw.WriteLine("            {");
                sw.WriteLine(
                    "                return _language != null ? _language.Length > 20 ? (_language.Substring(0, 20) + \"" +
                    "..." + "\") : _language : _language;");
                sw.WriteLine("            }");
                sw.WriteLine("        }");
                sw.WriteLine("        private string _language;");
                sw.WriteLine("");


                sw.WriteLine("    }");
                sw.WriteLine("}");
                sw.WriteLine("");

            }
        }

        private void WriteAdminOlIndexElements(string fileName)
        {
            WriteAdminOlIndexUsings(fileName);

            using (var sw = File.AppendText(fileName))
            {

                //namespace
                sw.WriteLine("namespace " + txtProjectName.Text + "." + "ViewModels" + "." + _modelName);
                sw.WriteLine("{");
                sw.WriteLine("    public class " + AdminOlIndexName + " : " + IAdminIndexBaseViewModel);
                sw.WriteLine("    {");

                sw.WriteLine("        public int " + _modelName + "Id { get; set; }");
                sw.WriteLine("");
                sw.WriteLine("        public IList<" + AdminOlIndexListName + "> " + _modelName + "List { get; set; }");
                sw.WriteLine("");
                sw.WriteLine("        public bool CanAddNewLanguage { get; set; }");
                sw.WriteLine("");
                sw.WriteLine("        public int PageSize { get; set; }");
                sw.WriteLine("");
                sw.WriteLine("        public int PageNum { get; set; }");
                sw.WriteLine("");
                sw.WriteLine("        public long RowCount { get; set; }");
                sw.WriteLine("");
                sw.WriteLine("        public int CurrentRowCount { get; set; }");
                sw.WriteLine("");
                sw.WriteLine("        public string SortFieldName { get; set; }");
                sw.WriteLine("");
                sw.WriteLine("        public bool IsAsc { get; set; }");
                sw.WriteLine("");
                sw.WriteLine("        public string[] FilterFieldsName { get; set; }");
                sw.WriteLine("");
                sw.WriteLine("        public IEnumerable<" + IAdminIndexListBaseViewModel + "> RowList { get; set; }");


                sw.WriteLine("    }");
                sw.WriteLine("}");
                sw.WriteLine("");

            }
        }

        private void WriteAdminOlIndexListUsings(string fileName)
        {
            using (var sw = File.AppendText(fileName))
            {
                sw.WriteLine("//Generate by CodeGenerator 2.0.0");

                foreach (var str in AdminOlIndexListNameSpaces)
                    sw.WriteLine(str);
                sw.WriteLine("");

            }
        }

        private void WriteAdminOlIndexUsings(string fileName)
        {
            using (var sw = File.AppendText(fileName))
            {
                sw.WriteLine("//Generate by CodeGenerator 2.0.0");

                foreach (var str in AdminOlIndexNameSpaces)
                    sw.WriteLine(str);
                sw.WriteLine("");

            }
        }

        private void btnUpOl_OnClick(object sender, RoutedEventArgs e)
        {
            //var Columns = dgAdminOlIndexList.ItemsSource as ObservableCollection<AdminOlIndexList>;
            //var selectedColumn = dgAdminOlIndexList.SelectedItem as AdminOlIndexList;

            //if (selectedColumn == null) return;
            //if (selectedColumn.Order == 0) return;

            //Columns.Swap(selectedColumn.Order, selectedColumn.Order - 1);
            //for (int i = 0; i < Columns.Count; i++)
            //    Columns[i].Order = i;

            //dgAdminOlIndexList.ItemsSource =
            //    new ObservableCollection<AdminOlIndexList>(Columns.OrderBy(x => x.Order).ToList());

        }

        private void btnDownOl_OnClick(object sender, RoutedEventArgs e)
        {
            //var Columns = dgAdminOlIndexList.ItemsSource as ObservableCollection<AdminOlIndexList>;
            //var selectedColumn = dgAdminOlIndexList.SelectedItem as AdminOlIndexList;

            //if (selectedColumn == null) return;
            //if (selectedColumn.Order == GenerateFiledList.Count - 1) return;

            //Columns.Swap(selectedColumn.Order, selectedColumn.Order + 1);
            //for (int i = 0; i < Columns.Count; i++)
            //    Columns[i].Order = i;

            //dgAdminOlIndexList.ItemsSource =
            //    new ObservableCollection<AdminOlIndexList>(Columns.OrderBy(x => x.Order).ToList());

        }



        // AdminOlCreate
        private void BtnSelectCreateOlField_OnClick(object sender, RoutedEventArgs e)
        {
            if (SelectedTable.Length == 0)
                return;

            CallSelectionFieldWindow();

            FillAdminOlCreate();
        }

        private void FillAdminOlCreate()
        {
            FilldgAdminOlCreate();
        }

        private ObservableCollection<AdminOlCreate> _selectedFieldForAdminOlCreateObser =
            new ObservableCollection<AdminOlCreate>();

        private void FilldgAdminOlCreate()
        {
            _selectedFieldForAdminOlCreateObser =
                new ObservableCollection<AdminOlCreate>(GenerateFiledList.Select(x => new AdminOlCreate()
                {
                    Field = x,
                    Display = x,

                }).ToList());
            TabControlAdminOlCreate.ItemsSource = _selectedFieldForAdminOlCreateObser;

        }

        private void BtnGenerateAdminOlCreate_OnClick(object sender, RoutedEventArgs e)
        {
            FillRootDirectory();
            CheckTempFolder();
            CheckModelFolder();
            CheckViewModelFolder();
            CreateFile(AdminOlCreateRoot);

            WriteAdminOlCreateElements(AdminOlCreateRoot, _tableName);
        }

        private void WriteAdminOlCreateElements(string fileName, string tableName)
        {
            WriteAdminOlCreateUsings(fileName);
            var sda = SqlDatabaseAdapter;

            using (var sw = File.AppendText(fileName))
            {
                var columns = sda.GetTablesColumn(tableName, cmbDatabases.SelectedValue.ToString());

                //namespace
                sw.WriteLine("namespace " + txtProjectName.Text + "." + "ViewModels" + "." + _modelName);
                sw.WriteLine("{");
                sw.WriteLine("    public class " + AdminOlCreateName);
                sw.WriteLine("    {");
                sw.WriteLine("");

                foreach (DataRow property in columns.Rows)
                {
                    if (_selectedFieldForAdminOlCreateObser.Any(x => x.Field == property["Name"].ToString()))
                    {
                        var currentAdmin =
                            _selectedFieldForAdminOlCreateObser.First(x => x.Field == property["Name"].ToString());

                        if (currentAdmin.IsKey)
                        {
                            WriteNormalProperty(sw, property);
                            sw.WriteLine("");
                            continue;
                        }

                        WriteDisplayName(sw, currentAdmin.Display);

                        WriteFilterRequired(currentAdmin.IsRequired, currentAdmin.RequiredMessage, sw);

                        WriteFilterStringLength(currentAdmin.IsStringLength, currentAdmin.MinStringLength,
                            currentAdmin.MaxStringLength, currentAdmin.StringLengthMessage, sw);

                        WriteFilterAllowHtml(currentAdmin.IsAllowHtml, sw);

                        WriteFilterScaffoldColumnFalse(currentAdmin.IsScaffoldColumnFalse, sw);

                        WriteFilterPhone(currentAdmin.IsPhone, currentAdmin.PhoneMessage, sw);

                        WriteFilterRange(currentAdmin.IsRange, currentAdmin.MinRange, currentAdmin.MaxRange,
                            currentAdmin.RangeMessage, sw);

                        WriteFilterRegularExpression(currentAdmin.IsRegularExpression, currentAdmin.Pattern,
                            currentAdmin.RegularExpressionMessage, sw);

                        WriteNormalProperty(sw, property);
                        sw.WriteLine("");
                    }
                }


                sw.WriteLine("");
                sw.WriteLine("        [Display(Name = @\"" + "زبان" + "\")]");
                sw.WriteLine("        [Required(ErrorMessage = @\"" + "زبان را انتخاب نمایید" + "\")]");
                sw.WriteLine("        public short Language { get; set; }");
                sw.WriteLine("");
                sw.WriteLine("        public List<OthereLanguage> CurrentLanguageList = new List<OthereLanguage>();");
                sw.WriteLine("");


                sw.WriteLine("    }");
                sw.WriteLine("}");
                sw.WriteLine("");

            }
        }

        private void WriteAdminOlCreateUsings(string fileName)
        {
            using (var sw = File.AppendText(fileName))
            {
                sw.WriteLine("//Generate by CodeGenerator 2.0.0");

                foreach (var str in AdminOlCreateNameSpaces)
                    sw.WriteLine(str);
                sw.WriteLine("");

            }
        }


        // AdminOlEdit
        private void BtnSelectEditOlField_OnClick(object sender, RoutedEventArgs e)
        {
            if (SelectedTable.Length == 0)
                return;

            CallSelectionFieldWindow();

            FillAdminOlEdit();
        }

        private void FillAdminOlEdit()
        {
            FilldgAdminOlEdit();
        }

        private ObservableCollection<AdminOlEdit> _selectedFieldForAdminOlEditObser =
            new ObservableCollection<AdminOlEdit>();

        private void FilldgAdminOlEdit()
        {
            _selectedFieldForAdminOlEditObser =
                new ObservableCollection<AdminOlEdit>(GenerateFiledList.Select(x => new AdminOlEdit()
                {
                    Field = x,
                    Display = x,

                }).ToList());
            TabControlAdminOlEdit.ItemsSource = _selectedFieldForAdminOlEditObser;

        }

        private void BtnGenerateAdminOlEdit_OnClick(object sender, RoutedEventArgs e)
        {
            FillRootDirectory();
            CheckTempFolder();
            CheckModelFolder();
            CheckViewModelFolder();
            CreateFile(AdminOlEditRoot);

            WriteAdminOlEditElements(AdminOlEditRoot, _tableName);
        }

        private void WriteAdminOlEditElements(string fileName, string tableName)
        {
            WriteAdminOlEditUsings(fileName);
            var sda = SqlDatabaseAdapter;

            using (var sw = File.AppendText(fileName))
            {
                var columns = sda.GetTablesColumn(tableName, cmbDatabases.SelectedValue.ToString());

                //namespace
                sw.WriteLine("namespace " + txtProjectName.Text + "." + "ViewModels" + "." + _modelName);
                sw.WriteLine("{");
                sw.WriteLine("    public class " + AdminOlEditName);
                sw.WriteLine("    {");
                sw.WriteLine("");

                foreach (DataRow property in columns.Rows)
                {
                    if (_selectedFieldForAdminOlEditObser.Any(x => x.Field == property["Name"].ToString()))
                    {
                        var currentAdmin =
                            _selectedFieldForAdminOlEditObser.First(x => x.Field == property["Name"].ToString());

                        if (currentAdmin.IsKey)
                        {
                            WriteNormalProperty(sw, property);
                            sw.WriteLine("");
                            continue;
                        }

                        WriteDisplayName(sw, currentAdmin.Display);

                        WriteFilterRequired(currentAdmin.IsRequired, currentAdmin.RequiredMessage, sw);

                        WriteFilterStringLength(currentAdmin.IsStringLength, currentAdmin.MinStringLength,
                            currentAdmin.MaxStringLength, currentAdmin.StringLengthMessage, sw);

                        WriteFilterAllowHtml(currentAdmin.IsAllowHtml, sw);

                        WriteFilterScaffoldColumnFalse(currentAdmin.IsScaffoldColumnFalse, sw);

                        WriteFilterPhone(currentAdmin.IsPhone, currentAdmin.PhoneMessage, sw);

                        WriteFilterRange(currentAdmin.IsRange, currentAdmin.MinRange, currentAdmin.MaxRange,
                            currentAdmin.RangeMessage, sw);

                        WriteFilterRegularExpression(currentAdmin.IsRegularExpression, currentAdmin.Pattern,
                            currentAdmin.RegularExpressionMessage, sw);

                        WriteNormalProperty(sw, property);
                        sw.WriteLine("");
                    }
                }


                sw.WriteLine("");
                sw.WriteLine("        [Display(Name = @\"" + "زبان" + "\")]");
                sw.WriteLine("        [Required(ErrorMessage = @\"" + "زبان را انتخاب نمایید" + "\")]");
                sw.WriteLine("        public short Language { get; set; }");
                sw.WriteLine("");
                sw.WriteLine("        public List<OthereLanguage> CurrentLanguageList = new List<OthereLanguage>();");
                sw.WriteLine("");


                sw.WriteLine("    }");
                sw.WriteLine("}");
                sw.WriteLine("");

            }
        }

        private void WriteAdminOlEditUsings(string fileName)
        {
            using (var sw = File.AppendText(fileName))
            {
                sw.WriteLine("//Generate by CodeGenerator 2.0.0");

                foreach (var str in AdminOlEditNameSpaces)
                    sw.WriteLine(str);
                sw.WriteLine("");

            }
        }




        /***********************************************************        Views       ***********************************************************/


        // AdminIndexView
        private void btnGenerateAdminIndexView_Click(object sender, RoutedEventArgs e)
        {
            FillRootDirectory();
            CheckTempFolder();
            CheckModelFolder();
            CheckViewFolder();
            CreateFile(AdminIndexViewRoot);

            WriteAdminIndexViewElements(AdminIndexViewRoot);
            WriteAdminListViewElements(AdminListViewRoot);
            btnGenerateAdminIndexJS();

            if (chkHaveAdminOlIndexView.IsChecked == true)
                btnGenerateAdminOlIndexView();

            //if (chkHaveAdminIndexViewModel.IsChecked == true)
            //    WriteAdminIndexElements(AdminIndexRoot);
        }
        private void WriteAdminIndexViewElements(string fileName)
        {
            using (var sw = File.AppendText(fileName))
            {

                sw.WriteLine("@model " + txtProjectName.Text + ".ViewModels." + _modelName + "." + AdminIndexName);
                sw.WriteLine("");

                sw.WriteLine("@section Title{");
                sw.WriteLine(ModelPersianName);
                sw.WriteLine("}");
                sw.WriteLine("@section PageHeader{");
                sw.WriteLine(" لیست " + ModelPersianName);
                sw.WriteLine("}");
                sw.WriteLine("@section RenderStyles{");
                sw.WriteLine("");
                sw.WriteLine("}");
                sw.WriteLine("@section RenderScripts{");
                sw.WriteLine("");
                sw.WriteLine("}");
                sw.WriteLine("");

                sw.WriteLine("@{");
                sw.WriteLine("    Layout = Url.Content(MVC." + AreaName + ".Shared.Views._" + AreaName + "Layout);");
                sw.WriteLine("");
                sw.WriteLine("        <script>");
                sw.WriteLine("        var rowCount = '@Model.RowCount';");
                sw.WriteLine("        var urlReFillPage = '@Url.Action(MVC." + AreaName + "." + _controllerName +
                             ".ActionNames.IndexPaging, MVC." + AreaName + "." + _controllerName + ".Name)';");
                sw.WriteLine("        var urlDelete" + _modelName + " = '@Url.Action(MVC." + AreaName + "." +
                             _controllerName + ".ActionNames.Delete, MVC." + AreaName + "." + _controllerName +
                             ".Name, new { area = MVC." + AreaName + ".Name })';");
                sw.WriteLine("");
                sw.WriteLine("    </script>");
                sw.WriteLine("}");
                sw.WriteLine("");

                sw.WriteLine(
                    "<div id=\"bs-example-modal-sm-DeleteContainer\" class=\"modal fade bs-example-modal-sm-DeleteContainer\" tabindex=\"-1\" role=\"dialog\" aria-labelledby=\"mySmallModalLabel\" aria-hidden=\"true\" style=\"display: none;\">");
                sw.WriteLine("</div>");
                sw.WriteLine("");


                sw.WriteLine("<div class=\"col-xs-12 col-md-12\">");
                sw.WriteLine("    <div class=\"widget\">");
                sw.WriteLine("        <div class=\"widget-header \">");
                sw.WriteLine("            <span class=\"widget-caption\" id=\"mahsoolat\">لیست " + ModelPersianName +
                             "</span>");
                sw.WriteLine("        </div>");
                sw.WriteLine("        <div class=\"widget-body\">");
                sw.WriteLine("            <div class=\"alert alert-info fade in\" style=\"display: none\">");
                sw.WriteLine("                <button class=\"close\" data-dismiss=\"alert\">");
                sw.WriteLine("                    ×");
                sw.WriteLine("                </button>");
                sw.WriteLine("                <i class=\"fa-fw fa fa-info\"></i>");
                sw.WriteLine("                <strong>تاکنون " + ModelPersianName +
                             " در سیستم ثبت نگردیده است.</strong>");
                sw.WriteLine("            </div>");
                sw.WriteLine("            <div class=\"row\" id=\"" + _modelName + "FilterRow\">");
                sw.WriteLine("                <div class=\"col-xs-12 col-md-12\">");
                sw.WriteLine("                    <div class=\"col-xs-6 col-md-6\">");
                sw.WriteLine("                        <div class=\"col-xs-3 col-md-3 \">");
                sw.WriteLine("                            <div id=\"" + _modelName +
                             "GridLanguage\" class=\"dataTables_Language\">");
                sw.WriteLine(
                    "                                @Html.Action(MVC.Language.ActionNames.SimpleIndex, MVC.Language.Name, new { area = \"\" })");
                sw.WriteLine("                            </div>");
                sw.WriteLine("                        </div>");
                sw.WriteLine("                        <div class=\"col-xs-2 col-md-2\">");
                sw.WriteLine("                            @Html.Partial(MVC.Shared.Views.Partial._PageSelectorDropdown)");
                sw.WriteLine("                        </div>");
                sw.WriteLine("                        <div class=\"col-xs-7 col-md-7\">");
                sw.WriteLine("");
                sw.WriteLine("                        </div>");
                sw.WriteLine("                    </div>");
                sw.WriteLine("                    <div class=\"col-xs-6 col-md-6\">");
                sw.WriteLine("                        <div id=\"simpledatatable_filter\" class=\"dataTables_filter\">");
                sw.WriteLine("                            <label>");
                sw.WriteLine(
                    "                                <input id=\"filterText\" type=\"search\" class=\"form-control input-sm\" placeholder=\"\" aria-controls=\"simpledatatable\">");
                sw.WriteLine("                            </label>");
                sw.WriteLine("                        </div>");
                sw.WriteLine("                    </div>");
                sw.WriteLine("                </div>");
                sw.WriteLine("            </div>");
                sw.WriteLine(
                    "            <div id=\"editabledatatable_wrapper\" class=\"dataTables_wrapper form-inline no-footer\">");
                sw.WriteLine("                <div class=\"ajaxContainer\">");
                sw.WriteLine("");
                sw.WriteLine("                    @Html.Partial(MVC." + AreaName + "." + _controllerName +
                             ".Views._List, Model)");
                sw.WriteLine("");
                sw.WriteLine("                </div>");
                sw.WriteLine("            </div>");
                sw.WriteLine("        </div>");
                sw.WriteLine("    </div>");
                sw.WriteLine("</div>");

                sw.WriteLine("");
                sw.WriteLine("@Styles.Render(Url.Content(\"~/assets/css/dataTables.bootstrap.css\"))");
                sw.WriteLine("@Scripts.Render(Url.Content(\"~/assets/js/datatable/dataTables.bootstrap.min.js\"))");
                sw.WriteLine("@Scripts.Render(Url.Content(\"~/Content/ViewJs/" + AreaName + "/" + _controllerName +
                             "/Index.js\"))");





            }
        }
        private void WriteAdminListViewElements(string fileName)
        {
            var model = new AdminIndexView()
            {
                isHaveHeadButton = true,
                _items = (ObservableCollection<AdminIndexViewItem>)dgAdminIndexView.ItemsSource
            };
            using (var sw = File.AppendText(fileName))
            {

                sw.WriteLine("@using " + txtProjectName.Text + ".Helpers");
                sw.WriteLine("@using " + txtProjectName.Text + ".ViewModels." + _modelName);
                sw.WriteLine("@using " + txtProjectName.Text + ".ViewModels.Shared");
                sw.WriteLine("@model " + txtProjectName.Text + ".ViewModels." + _modelName + "." + AdminIndexName);
                sw.WriteLine("");

                sw.WriteLine("@{");
                sw.WriteLine("    Layout = \"\";");
                sw.WriteLine("");
                sw.WriteLine("    string[] headTcss =");
                sw.WriteLine("    {");
                for (int i = 0; i < model._items.Count; i++)
                {
                    if (!model._items[i].exclude)
                        sw.WriteLine("        \" " + model._items[i].headTcss + "  \",");
                }
                sw.WriteLine("    };");

                sw.WriteLine("    string[] headIcss =");
                sw.WriteLine("    {");
                for (int i = 0; i < model._items.Count; i++)
                {
                    if (!model._items[i].exclude)
                        sw.WriteLine("        \" " + model._items[i].headIcss + "  \",");
                }
                sw.WriteLine("    };");

                sw.WriteLine("    string[] canFilterFields = new[]");
                sw.WriteLine("    {");
                for (int i = 0; i < model._items.Count; i++)
                {
                    if (model._items[i].canFilterFields)
                    {
                        sw.WriteLine("        \"" + model._items[i].Key.ToLower() + "\", ");
                    }
                }
                sw.WriteLine("    };");

                sw.WriteLine("    string[] filterFields = Model.FilterFieldsName;");

                sw.WriteLine("    string[] headDivcss =");
                sw.WriteLine("    {");
                for (int i = 0; i < model._items.Count; i++)
                {
                    if (!model._items[i].exclude)
                        sw.WriteLine("        \" " + model._items[i].headDivcss + "  \",");
                }
                sw.WriteLine("    };");

                sw.WriteLine("    string[] canSortFields = new[]");
                sw.WriteLine("    {");
                for (int i = 0; i < model._items.Count; i++)
                {
                    if (model._items[i].canSortFields)
                    {
                        sw.WriteLine("        \"" + model._items[i].Key.ToLower() + "\", ");
                    }
                }
                sw.WriteLine("    };");

                sw.WriteLine(
                    "    var sortFields = string.IsNullOrEmpty(Model.SortFieldName) ? Html.NameFor(x => x.RowList.First().Id).ToString() : Model.SortFieldName;");
                sw.WriteLine("    var isAsc = Model.IsAsc;");
                sw.WriteLine("    const string headButtonCssClass = \" " + model.headButtonCssClass + "  \";");
                sw.WriteLine("    const string deleteButtonId = \"delete" + _modelName + "Button\";");
                sw.WriteLine("    const bool isHaveHeadButton = " + (model.isHaveHeadButton ? "true" : "false") + ";");

                sw.WriteLine("    string[] inclue = new string[]");
                sw.WriteLine("    {");
                for (int i = 0; i < model._items.Count; i++)
                {
                    if (model._items[i].inclue)
                    {
                        sw.WriteLine("        \"" + model._items[i].Key.ToLower() + "\", ");
                    }
                }
                sw.WriteLine("    };");

                sw.WriteLine("    string[] exclude = new string[]");
                sw.WriteLine("    {");
                for (int i = 0; i < model._items.Count; i++)
                {
                    if (model._items[i].exclude)
                    {
                        sw.WriteLine("        \"" + model._items[i].Key.ToLower() + "\", ");
                    }
                }
                sw.WriteLine("    };");


                sw.WriteLine("    var otherButton = new List<Tuple<string, string>>()");
                sw.WriteLine("    {");
                sw.WriteLine("");
                sw.WriteLine("    };");

                sw.WriteLine("    var deleteUrl = Url.Action(MVC." + AreaName + "." + _controllerName +
                             ".ActionNames.Delete, MVC." + AreaName + "." + _controllerName + ".Name, new { area = MVC." +
                             AreaName + ".Name });");
                sw.WriteLine("    var editUrl = Url.Action(MVC." + AreaName + "." + _controllerName +
                             ".ActionNames.Edit, MVC." + AreaName + "." + _controllerName + ".Name, new { area = MVC." +
                             AreaName + ".Name });");
                sw.WriteLine("    var detailsUrl = Url.Action(MVC." + AreaName + "." + _controllerName +
                             ".ActionNames.Details, MVC." + AreaName + "." + _controllerName +
                             ".Name, new { area = MVC." + AreaName + ".Name });");

                sw.WriteLine("}");



                sw.WriteLine(
                    "@Html.Partial(MVC.Shared.Views.Partial._Grid, GridEctensions.GenerateGenericList(Model.RowList, new " +
                    AdminIndexListName + "(),");
                sw.WriteLine("    headTcss,");
                sw.WriteLine("    headIcss,");
                sw.WriteLine("    filterFields,");
                sw.WriteLine("    canFilterFields,");
                sw.WriteLine("    headDivcss,");
                sw.WriteLine("    sortFields,");
                sw.WriteLine("    canSortFields,");
                sw.WriteLine("    isAsc,");
                sw.WriteLine("    headButtonCssClass,");
                sw.WriteLine("    deleteButtonId,");
                sw.WriteLine("    isHaveHeadButton,");
                sw.WriteLine("    exclude: exclude,");
                sw.WriteLine("    deleteUrl: deleteUrl,");
                sw.WriteLine("    detailsUrl: detailsUrl,");
                sw.WriteLine("    editUrl: editUrl,");
                sw.WriteLine("    nameUrlButton: otherButton");
                sw.WriteLine("    ))");


                sw.WriteLine("@Html.Partial(MVC.Shared.Views.Partial._Paging, new PagingViewModel()");
                sw.WriteLine("{");
                sw.WriteLine("    CssClassName = \"GridPaging\",");
                sw.WriteLine("    CurrentRowCount = Model.CurrentRowCount,");
                sw.WriteLine("    PageSize = Model.PageSize,");
                sw.WriteLine("    PageNum = Model.PageNum,");
                sw.WriteLine("    RowCount = Model.RowCount,");
                sw.WriteLine("    Url = Url.Action(MVC." + AreaName + "." + _controllerName +
                             ".ActionNames.IndexPaging, MVC." + AreaName + "." + _controllerName +
                             ".Name, new { area = MVC." + AreaName + ".Name })");
                sw.WriteLine("})");






            }
        }


        // AdminOlIndexView
        private void btnGenerateAdminOlIndexView()
        {
            FillRootDirectory();
            CheckTempFolder();
            CheckModelFolder();
            CheckViewFolder();
            CheckViewOlFolder();
            CreateFile(AdminOlIndexViewRoot);

            WriteAdminOlIndexViewElements(AdminOlIndexViewRoot);
            WriteAdminOlListViewElements(AdminOlListViewRoot);
            btnGenerateAdminOlIndexJS();

        }
        private void WriteAdminOlIndexViewElements(string fileName)
        {
            using (var sw = File.AppendText(fileName))
            {

                sw.WriteLine("@using Panberes.Helpers");
                sw.WriteLine("@model " + txtProjectName.Text + ".ViewModels." + _modelName + "." + AdminOlIndexName);
                sw.WriteLine("");

                sw.WriteLine("@section Title{");
                sw.WriteLine(ModelPersianName);
                sw.WriteLine("}");
                sw.WriteLine("@section PageHeader{");
                sw.WriteLine(" لیست " + ModelPersianName);
                sw.WriteLine("}");
                sw.WriteLine("@section RenderStyles{");
                sw.WriteLine("");
                sw.WriteLine("}");
                sw.WriteLine("@section RenderScripts{");
                sw.WriteLine("");
                sw.WriteLine("}");
                sw.WriteLine("");

                sw.WriteLine("@{");
                sw.WriteLine("    Layout = Url.Content(MVC." + AreaName + ".Shared.Views._" + AreaName + "Layout);");
                sw.WriteLine("    var lanCount = Model." + _modelName + "List.Count;");
                sw.WriteLine("");
                sw.WriteLine("        <script>");
                sw.WriteLine("        var urlDelete" + _modelName + " = '@Url.Action(MVC." + AreaName + "." +
                             _controllerName + ".ActionNames.DeleteLanguage, MVC." + AreaName + "." + _controllerName +
                             ".Name, new { area = MVC." + AreaName + ".Name })';");
                sw.WriteLine("");
                sw.WriteLine("    </script>");
                sw.WriteLine("}");
                sw.WriteLine("");

                sw.WriteLine(
                    "<div id=\"bs-example-modal-sm-DeleteContainer\" class=\"modal fade bs-example-modal-sm-DeleteContainer\" tabindex=\"-1\" role=\"dialog\" aria-labelledby=\"mySmallModalLabel\" aria-hidden=\"true\" style=\"display: none;\">");
                sw.WriteLine("</div>");
                sw.WriteLine("");


                sw.WriteLine("<div class=\"col-xs-12 col-md-12\">");
                sw.WriteLine("    <div class=\"widget\">");
                sw.WriteLine("        <div class=\"widget-header \">");
                sw.WriteLine("            <span class=\"widget-caption\" id=\"mahsoolat\">لیست  زبان های دیگر " + ModelPersianName +
                             "</span>");
                sw.WriteLine("        </div>");
                sw.WriteLine("        <div class=\"widget-body\">");
                sw.WriteLine("            <div class=\"alert alert-info fade in\" @(lanCount == 0 ? \"style=display:block\" : \"style=display:none\") >");
                sw.WriteLine("                <button class=\"close\" data-dismiss=\"alert\">");
                sw.WriteLine("                    ×");
                sw.WriteLine("                </button>");
                sw.WriteLine("                <i class=\"fa-fw fa fa-info\"></i>");
                sw.WriteLine("                <strong> " + ModelPersianName + " فاقد زبان دیگری می باشد.</strong>");
                sw.WriteLine("            </div>");

                sw.WriteLine("            @if (lanCount > 0)");
                sw.WriteLine("            {");

                sw.WriteLine("            <div id=\"editabledatatable_wrapper\" class=\"dataTables_wrapper form-inline no-footer\">");
                sw.WriteLine("                <div class=\"ajaxContainer\">");
                sw.WriteLine("                    @Html.Partial(MVC." + AreaName + "." + _controllerName + "Ol.Views._List, Model)");
                sw.WriteLine("                </div>");
                sw.WriteLine("            </div>");

                sw.WriteLine("            }");

                sw.WriteLine("            @if (Model.CanAddNewLanguage)");
                sw.WriteLine("            {");
                sw.WriteLine("                <div class=\"row\">");
                sw.WriteLine("                    <div class=\"col-xs-12 col-md-12\">");
                sw.WriteLine("                        @Html.EditImageButton(Url.Action(");
                sw.WriteLine("                            MVC." + AreaName + "." + _controllerName + ".ActionNames.CreateLanguage,");
                sw.WriteLine("                            MVC." + AreaName + "." + _controllerName + ".Name,");
                sw.WriteLine("                            new { area = MVC." + AreaName + ".Name, id = Model." + _modelName + "Id }), \"افزودن زبان جدید\", \"style='float: left;margin-top: 10px;'\")");
                sw.WriteLine("                    </div>");
                sw.WriteLine("                </div>");
                sw.WriteLine("            }");
                sw.WriteLine("");

                sw.WriteLine("        </div>");
                sw.WriteLine("    </div>");
                sw.WriteLine("</div>");

                sw.WriteLine("");
                //sw.WriteLine("@Styles.Render(Url.Content(\"~/assets/css/dataTables.bootstrap.css\"))");
                //sw.WriteLine("@Scripts.Render(Url.Content(\"~/assets/js/datatable/dataTables.bootstrap.min.js\"))");
                sw.WriteLine("@Scripts.Render(Url.Content(\"~/Content/ViewJs/Ol" + AreaName + "/" + _controllerName + "/Index.js\"))");





            }
        }
        private void WriteAdminOlListViewElements(string fileName)
        {
            var model = new AdminIndexView()
            {
                isHaveHeadButton = true,
                _items = (ObservableCollection<AdminIndexViewItem>)dgAdminIndexView.ItemsSource
            };
            using (var sw = File.AppendText(fileName))
            {

                sw.WriteLine("@using " + txtProjectName.Text + ".Helpers");
                sw.WriteLine("@using " + txtProjectName.Text + ".ViewModels." + _modelName);
                sw.WriteLine("@using " + txtProjectName.Text + ".ViewModels.Shared");
                sw.WriteLine("@model " + txtProjectName.Text + ".ViewModels." + _modelName + "." + AdminOlIndexName);
                sw.WriteLine("");

                sw.WriteLine("@{");
                sw.WriteLine("    Layout = \"\";");
                sw.WriteLine("");
                sw.WriteLine("    string[] headTcss =");
                sw.WriteLine("    {");
                for (int i = 0; i < model._items.Count; i++)
                {
                    if (!model._items[i].exclude)
                        sw.WriteLine("        \" " + model._items[i].headTcss + "  \",");
                }
                sw.WriteLine("    };");

                sw.WriteLine("    string[] headIcss =");
                sw.WriteLine("    {");
                for (int i = 0; i < model._items.Count; i++)
                {
                    if (!model._items[i].exclude)
                        sw.WriteLine("        \" " + model._items[i].headIcss + "  \",");
                }
                sw.WriteLine("    };");

                sw.WriteLine("    string[] canFilterFields = null;");
                sw.WriteLine("    string[] filterFields = null;");

                sw.WriteLine("    string[] headDivcss =");
                sw.WriteLine("    {");
                for (int i = 0; i < model._items.Count; i++)
                {
                    if (!model._items[i].exclude)
                        sw.WriteLine("        \" " + model._items[i].headDivcss + "  \",");
                }
                sw.WriteLine("    };");

                sw.WriteLine("    string[] canSortFields = null");

                sw.WriteLine(
                    "    var sortFields = string.IsNullOrEmpty(Model.SortFieldName) ? Html.NameFor(x => x.RowList.First().BaseId).ToString() : Model.SortFieldName;");
                sw.WriteLine("    var isAsc = Model.IsAsc;");
                sw.WriteLine("    const string headButtonCssClass = \" " + model.headButtonCssClass + "  \";");
                sw.WriteLine("    const string deleteButtonId = \"delete" + _modelName + "OlButton\";");
                sw.WriteLine("    const bool isHaveHeadButton = " + (model.isHaveHeadButton ? "true" : "false") + ";");

                sw.WriteLine("    string[] inclue = new string[]");
                sw.WriteLine("    {");
                for (int i = 0; i < model._items.Count; i++)
                {
                    if (model._items[i].inclue)
                    {
                        sw.WriteLine("        \"" + model._items[i].Key.ToLower() + "\", ");
                    }
                }
                sw.WriteLine("    };");

                sw.WriteLine("    string[] exclude = new string[]");
                sw.WriteLine("    {");
                sw.WriteLine("        \"id\", ");
                sw.WriteLine("        \"BaseId\", ");
                for (int i = 0; i < model._items.Count; i++)
                {
                    if (model._items[i].exclude)
                    {
                        sw.WriteLine("        \"" + model._items[i].Key.ToLower() + "\", ");
                    }
                }
                sw.WriteLine("    };");


                sw.WriteLine("    var otherButton = new List<Tuple<string, string>>()");
                sw.WriteLine("    {");
                sw.WriteLine("");
                sw.WriteLine("    };");

                sw.WriteLine("    var deleteUrl = Url.Action(MVC." + AreaName + "." + _controllerName +
                             ".ActionNames.DeleteLanguage, MVC." + AreaName + "." + _controllerName + ".Name, new { area = MVC." +
                             AreaName + ".Name });");
                sw.WriteLine("    var editUrl = Url.Action(MVC." + AreaName + "." + _controllerName +
                             ".ActionNames.EditLanguage, MVC." + AreaName + "." + _controllerName + ".Name, new { area = MVC." +
                             AreaName + ".Name });");

                sw.WriteLine("}");



                sw.WriteLine(
                    "@Html.Partial(MVC.Shared.Views.Partial._Grid, GridEctensions.GenerateGenericList(Model." + _modelName + "List, new " +
                    AdminOlIndexListName + "(),");
                sw.WriteLine("    headTcss,");
                sw.WriteLine("    headIcss,");
                sw.WriteLine("    filterFields,");
                sw.WriteLine("    canFilterFields,");
                sw.WriteLine("    headDivcss,");
                sw.WriteLine("    sortFields,");
                sw.WriteLine("    canSortFields,");
                sw.WriteLine("    isAsc,");
                sw.WriteLine("    headButtonCssClass,");
                sw.WriteLine("    deleteButtonId,");
                sw.WriteLine("    isHaveHeadButton,");
                sw.WriteLine("    exclude: exclude,");
                sw.WriteLine("    deleteUrl: deleteUrl,");
                sw.WriteLine("    detailsUrl: detailsUrl,");
                sw.WriteLine("    editUrl: editUrl,");
                sw.WriteLine("    nameUrlButton: otherButton");
                sw.WriteLine("    ))");



            }
        }



        // AdminCreateView
        private void btnGenerateAdminCreateView_Click(object sender, RoutedEventArgs e)
        {
            FillRootDirectory();
            CheckTempFolder();
            CheckModelFolder();
            CheckViewFolder();
            CreateFile(AdminCreateViewRoot);

            WriteAdminCreateViewElements(AdminCreateViewRoot);
            btnGenerateAdminCreateJS();
            if (chkAdminEditView.IsChecked == true)
            {
                CreateFile(AdminEditViewRoot);
                WriteAdminEditViewElements(AdminEditViewRoot);
                btnGenerateAdminEditJS();
            }
        }

        private void WriteAdminCreateViewElements(string fileName)
        {
            var model = new AdminCreateView()
            {
                _items = (ObservableCollection<AdminCreateViewItem>)dgAdminCreateView.ItemsSource
            };

            using (var sw = File.AppendText(fileName))
            {

                sw.WriteLine("@model " + txtProjectName.Text + ".ViewModels." + _modelName + "." + AdminCreateName);
                sw.WriteLine("");

                sw.WriteLine("@{");
                sw.WriteLine("    Layout = Url.Content(MVC." + AreaName + ".Shared.Views._" + AreaName + "Layout);");

                if (model._items.Any(x => x.IsImage))
                {
                    sw.WriteLine("    <link href=\"@Url.Content(\"~/Content/Css/imageUplaod.css\")\" rel=\"stylesheet\" />");
                    sw.WriteLine("    <script>");

                    foreach (var field in model._items.Where(x => x.IsImage).ToList())
                    {

                        sw.WriteLine("        var " + field.Key + " = '@Model." + field.Key + "';");
                        sw.WriteLine("        var " + field.Key + "FileName = '@(Path.GetFileName(Model." + field.Key + "))';");
                        sw.WriteLine("        var urlRemove" + field.Key + " = '@Url.Action(MVC." + AreaName + "." + _controllerName + ".ActionNames.RemoveImageCreate, MVC." + AreaName + "." + _controllerName + ".Name, new {area = MVC." + AreaName + ".Name, url = \"" + field.Key + "\"})';");
                        sw.WriteLine("        var urlUpload" + field.Key + " = '@Url.Action(MVC." + AreaName + "." + _controllerName + ".ActionNames.UploadImage, MVC." + AreaName + "." + _controllerName + ".Name, new");
                        sw.WriteLine("        {");
                        sw.WriteLine("            area = MVC." + AreaName + ".Name,");
                        sw.WriteLine("            name = \"" + field.Key + "Name\"");
                        sw.WriteLine("        })';");

                    }

                    sw.WriteLine("");
                    sw.WriteLine("    </script>");

                }

                sw.WriteLine("}");

                sw.WriteLine("");

                sw.WriteLine("@section Title{");
                sw.WriteLine(" ثبت " + ModelPersianName);
                sw.WriteLine("}");
                sw.WriteLine("@section PageHeader{");
                sw.WriteLine(" ثبت " + ModelPersianName);
                sw.WriteLine("}");




                sw.WriteLine("<div class=\"col-lg-12 col-sm-12 col-xs-12\">");
                sw.WriteLine("    <div class=\"widget radius-bordered\">");
                sw.WriteLine("        <div class=\"widget-header\">");
                sw.WriteLine("            <span class=\"widget-caption\">ثبت " + ModelPersianName + " جدید</span>");
                sw.WriteLine("        </div>");
                sw.WriteLine("        <div class=\"widget-body\">");
                sw.WriteLine("");
                sw.WriteLine("            @using (Html.BeginForm(");
                sw.WriteLine("                MVC." + AreaName + "." + _controllerName + ".ActionNames.Create,");
                sw.WriteLine("                MVC." + AreaName + "." + _controllerName + ".Name,");
                sw.WriteLine("                FormMethod.Post,");
                sw.WriteLine("                new");
                sw.WriteLine("                {");
                sw.WriteLine("                    area = MVC." + AreaName + ".Name,");
                sw.WriteLine("                    @class = \"form-horizontal bv-form\"");
                sw.WriteLine("                }))");
                sw.WriteLine("            {");
                sw.WriteLine("                @Html.AntiForgeryToken()");
                sw.WriteLine("                @Html.ValidationSummary(true)");
                sw.WriteLine("");


                foreach (var field in model._items)
                {

                    if (field.IsTextBox)
                    {
                        sw.WriteLine("                <div class=\"form-group has-feedback \">");
                        sw.WriteLine("                    @Html.LabelFor(model => model." + field.Key + ", new { @class = \"col-lg-2 control-label\" })");
                        sw.WriteLine("                    <div class=\"col-lg-10\">");
                        sw.WriteLine("                        @Html.TextBoxFor(model => model." + field.Key + ", new { @class = \"form-control\" })");
                        sw.WriteLine("                        @Html.ValidationMessageFor(model => model." + field.Key + ", null, new { @class = \"help-block\" })");
                        sw.WriteLine("                    </div>");
                        sw.WriteLine("                </div>");
                    }

                    if (field.IsTextArea)
                    {
                        sw.WriteLine("                <div class=\"form-group has-feedback \">");
                        sw.WriteLine("                    @Html.LabelFor(model => model." + field.Key + ", new { @class = \"col-lg-2 control-label\" })");
                        sw.WriteLine("                    <div class=\"col-lg-10\">");
                        sw.WriteLine("                        @Html.TextAreaFor(model => model." + field.Key + ", new { @class = \"form-control\" })");
                        sw.WriteLine("                        @Html.ValidationMessageFor(model => model." + field.Key + ", null, new { @class = \"help-block\" })");
                        sw.WriteLine("                    </div>");
                        sw.WriteLine("                </div>");
                    }

                    if (field.IsDropDownList)
                    {
                        sw.WriteLine("                <div class=\"form-group has-feedback \">");
                        sw.WriteLine("                    @Html.LabelFor(model => model." + field.Key + ", new { @class = \"col-lg-2 control-label\" })");
                        sw.WriteLine("                    <div class=\"col-lg-10\">");
                        sw.WriteLine("                        @Html.DropDownListFor(model => model." + field.Key + ", new SelectList(Model.LanguageList.ToList(), \"Id\", \"Title\") )");
                        sw.WriteLine("                    </div>");
                        sw.WriteLine("                </div>");
                    }

                    if (field.IsLabel)
                    {
                        sw.WriteLine("                <div class=\"form-group has-feedback \">");
                        sw.WriteLine("                    @Html.DisplayFor(model => model." + field.Key + ", new { @class = \"col-lg-2 control-label\" })");
                        sw.WriteLine("                    <div class=\"col-lg-10\">");
                        sw.WriteLine("                        @Html.LabelFor(model => model." + field.Key + ", new { @class = \"col-lg-2 control-label\" })");
                        sw.WriteLine("                    </div>");
                        sw.WriteLine("                </div>");
                    }

                    if (field.IsCheckBox)
                    {
                        sw.WriteLine("                <div class=\"form-group has-feedback \">");
                        sw.WriteLine("                    @Html.LabelFor(model => model." + field.Key + ", new { @class = \"col-lg-2 control-label\" })");
                        sw.WriteLine("                    <div class=\"col-lg-10\">");
                        sw.WriteLine("                        @Html.CheckBoxFor(model => model." + field.Key + ", new { @class = \" checkbox-slider toggle colored-blue \" })");
                        sw.WriteLine("                        <span class=\"text\"></span>");
                        sw.WriteLine("                    </div>");
                        sw.WriteLine("                </div>");
                    }

                    if (field.IsNestedDropDownList)
                    { }

                    if (field.IsImage)
                    {

                        sw.WriteLine("                @Html.HiddenFor(model => model." + field.Key + ")");
                        sw.WriteLine("                @Html.Hidden(\"" + field.Key + "Name\", \"\")");
                        sw.WriteLine("                <div class=\"form-group has-feedback\">");
                        sw.WriteLine("                    @Html.LabelFor(model => model." + field.Key + ", new { @class = \"col-lg-2 control-label\" })");
                        sw.WriteLine("");
                        sw.WriteLine("                    <div class=\"col-lg-10\">");
                        sw.WriteLine("                        @Html.ValidationMessageFor(model => model." + field.Key + ", null, new { @class = \"help-block\" })");
                        sw.WriteLine("                        <div class=\"image-upload float-left\">");
                        sw.WriteLine("                            <div class=\"image-upload-container\">");
                        sw.WriteLine("                                <img src=\"@Url.Content(\"~/Content/Icons/empty.png\")\" alt=\"image upload\" id=\"img" + field.Key + "\" ");
                        sw.WriteLine("                                     style=\"max-width: 379px; min-width: 379px; max-height: 209px;\" />");
                        sw.WriteLine("                            </div>");
                        sw.WriteLine("                            <div class=\"image-hover-container\">");
                        sw.WriteLine("                                <input type=\"file\" name=\"fileToUpload" + field.Key + "\" id=\"fileToUpload" + field.Key + "\"");
                        sw.WriteLine("                                       style=\"width: 399px; height: 209px; overflow: hidden; position: absolute; top: 0; right: 0; margin: 0; opacity: 0; -ms-filter: 'alpha(opacity=0)'; font-size: 200px; direction: ltr; cursor: pointer; z-index: 20\" />");
                        sw.WriteLine("                                <span class=\"image-hover\" style=\"z-index: 0\"></span>");
                        sw.WriteLine("                                <span class=\"hover-text\" style=\"z-index: 0;\">تصویر " + ModelPersianName + "</span>");
                        sw.WriteLine("                            </div>");
                        sw.WriteLine("                        </div>");
                        sw.WriteLine("                        <div class=\"col-lg-6 col-lg-offset-1  \">");
                        sw.WriteLine("                            <a class=\"btn btn-default btn-sm shiny icon-only danger\" id=\"clearImage" + field.Key + "\"><i class=\"fa fa-times \"></i></a>");
                        sw.WriteLine("                            <span class=\"image-size\"> 1024 * 1024  </span>");
                        sw.WriteLine("                        </div>");
                        sw.WriteLine("                        <div class=\"upload-bar col-lg-6 col-lg-offset-1 \" style=\"opacity: 0; width: 399px; float: right;\" id=\"P_LoadingDiv" + field.Key + "\">");
                        sw.WriteLine("                            <span class=\"upload-bar-percent\" style=\"width: 0%;\" id=\"P_Loading" + field.Key + "\"></span>");
                        sw.WriteLine("                        </div>");
                        sw.WriteLine("                    </div>");
                        sw.WriteLine("                </div>");
                        sw.WriteLine("");

                    }
                }


                sw.WriteLine("");



                if (chkViewHaveOtherSeo.IsChecked == true)
                {
                    sw.WriteLine("                @Html.Partial(MVC.Shared.Views.Partial._BaseSeoView, Model)");
                }

                sw.WriteLine("");
                sw.WriteLine("");
                sw.WriteLine("                <div class=\"form-group\">");
                sw.WriteLine("                    <div class=\"col-lg-offset-4 col-lg-8\">");
                sw.WriteLine("                        <input class=\"btn btn-palegreen col-lg-3\" type=\"submit\" value=\"ثبت\">");
                sw.WriteLine("                    </div>");
                sw.WriteLine("                </div>");
                sw.WriteLine("");
                sw.WriteLine("            }");
                sw.WriteLine("        </div>");
                sw.WriteLine("    </div>");
                sw.WriteLine("</div>");
                sw.WriteLine("");


                sw.WriteLine("@section Scripts {");
                sw.WriteLine("   @Scripts.Render(\"~/bundles/jqueryval\")");
                if (model._items.Any(x => x.IsCkEditor))
                {
                    sw.WriteLine("   @Scripts.Render(\"~/ckeditor/ckeditor.js\")");
                    sw.WriteLine("   @Scripts.Render(\"~/ckeditor/adapters/jquery.js\")");
                }
                sw.WriteLine("   @Scripts.Render(Url.Content(\"~/Content/ViewJs/" + AreaName + "/" + _controllerName + "/Create.js\"))");
                sw.WriteLine("}");

            }
        }

        // AdminEditView
        private void WriteAdminEditViewElements(string fileName)
        {
            var model = new AdminCreateView()
            {
                _items = (ObservableCollection<AdminCreateViewItem>)dgAdminCreateView.ItemsSource
            };

            using (var sw = File.AppendText(fileName))
            {

                sw.WriteLine("@model " + txtProjectName.Text + ".ViewModels." + _modelName + "." + AdminEditName);
                sw.WriteLine("");

                sw.WriteLine("@{");
                sw.WriteLine("    Layout = Url.Content(MVC." + AreaName + ".Shared.Views._" + AreaName + "Layout);");

                if (model._items.Any(x => x.IsImage))
                {
                    sw.WriteLine("    <link href=\"@Url.Content(\"~/Content/Css/imageUplaod.css\")\" rel=\"stylesheet\" />");
                    sw.WriteLine("    <script>");

                    foreach (var field in model._items.Where(x => x.IsImage).ToList())
                    {

                        sw.WriteLine("        var " + field.Key + " = '@Model." + field.Key + "';");
                        sw.WriteLine("        var " + field.Key + "FileName = '@(Path.GetFileName(Model." + field.Key + "))';");
                        sw.WriteLine("        var urlRemove" + field.Key + " = '@Url.Action(MVC." + AreaName + "." + _controllerName + ".ActionNames.RemoveImageCreate, MVC." + AreaName + "." + _controllerName + ".Name, new {area = MVC." + AreaName + ".Name, url = \"" + field.Key + "\"})';");
                        sw.WriteLine("        var urlUpload" + field.Key + " = '@Url.Action(MVC." + AreaName + "." + _controllerName + ".ActionNames.UploadImage, MVC." + AreaName + "." + _controllerName + ".Name, new");
                        sw.WriteLine("        {");
                        sw.WriteLine("            area = MVC." + AreaName + ".Name,");
                        sw.WriteLine("            name = \"" + field.Key + "Name\"");
                        sw.WriteLine("        })';");

                    }

                    sw.WriteLine("");
                    sw.WriteLine("    </script>");

                }

                sw.WriteLine("}");

                sw.WriteLine("");

                sw.WriteLine("@section Title{");
                sw.WriteLine(" ویرایش " + ModelPersianName);
                sw.WriteLine("}");
                sw.WriteLine("@section PageHeader{");
                sw.WriteLine(" ویرایش " + ModelPersianName);
                sw.WriteLine("}");




                sw.WriteLine("<div class=\"col-lg-12 col-sm-12 col-xs-12\">");
                sw.WriteLine("    <div class=\"widget radius-bordered\">");
                sw.WriteLine("        <div class=\"widget-header\">");
                sw.WriteLine("            <span class=\"widget-caption\">ویرایش " + ModelPersianName + " جدید</span>");
                sw.WriteLine("        </div>");
                sw.WriteLine("        <div class=\"widget-body\">");
                sw.WriteLine("");
                sw.WriteLine("            @using (Html.BeginForm(");
                sw.WriteLine("                MVC." + AreaName + "." + _controllerName + ".ActionNames.Edit,");
                sw.WriteLine("                MVC." + AreaName + "." + _controllerName + ".Name,");
                sw.WriteLine("                FormMethod.Post,");
                sw.WriteLine("                new");
                sw.WriteLine("                {");
                sw.WriteLine("                    area = MVC." + AreaName + ".Name,");
                sw.WriteLine("                    @class = \"form-horizontal bv-form\"");
                sw.WriteLine("                }))");
                sw.WriteLine("            {");
                sw.WriteLine("                @Html.AntiForgeryToken()");
                sw.WriteLine("                @Html.ValidationSummary(true)");
                sw.WriteLine("                @Html.HiddenFor(model => model.Id)");
                sw.WriteLine("");


                foreach (var field in model._items)
                {

                    if (field.IsTextBox)
                    {
                        sw.WriteLine("                <div class=\"form-group has-feedback \">");
                        sw.WriteLine("                    @Html.LabelFor(model => model." + field.Key + ", new { @class = \"col-lg-2 control-label\" })");
                        sw.WriteLine("                    <div class=\"col-lg-10\">");
                        sw.WriteLine("                        @Html.TextBoxFor(model => model." + field.Key + ", new { @class = \"form-control\" })");
                        sw.WriteLine("                        @Html.ValidationMessageFor(model => model." + field.Key + ", null, new { @class = \"help-block\" })");
                        sw.WriteLine("                    </div>");
                        sw.WriteLine("                </div>");
                    }

                    if (field.IsTextArea)
                    {
                        sw.WriteLine("                <div class=\"form-group has-feedback \">");
                        sw.WriteLine("                    @Html.LabelFor(model => model." + field.Key + ", new { @class = \"col-lg-2 control-label\" })");
                        sw.WriteLine("                    <div class=\"col-lg-10\">");
                        sw.WriteLine("                        @Html.TextAreaFor(model => model." + field.Key + ", new { @class = \"form-control\" })");
                        sw.WriteLine("                        @Html.ValidationMessageFor(model => model." + field.Key + ", null, new { @class = \"help-block\" })");
                        sw.WriteLine("                    </div>");
                        sw.WriteLine("                </div>");
                    }

                    if (field.IsDropDownList)
                    {
                        sw.WriteLine("                <div class=\"form-group has-feedback \">");
                        sw.WriteLine("                    @Html.LabelFor(model => model." + field.Key + ", new { @class = \"col-lg-2 control-label\" })");
                        sw.WriteLine("                    <div class=\"col-lg-10\">");
                        sw.WriteLine("                        @Html.DropDownListFor(model => model." + field.Key + ", new SelectList(Model.LanguageList.ToList(), \"Id\", \"Title\") )");
                        sw.WriteLine("                    </div>");
                        sw.WriteLine("                </div>");
                    }

                    if (field.IsLabel)
                    {
                        sw.WriteLine("                <div class=\"form-group has-feedback \">");
                        sw.WriteLine("                    @Html.DisplayFor(model => model." + field.Key + ", new { @class = \"col-lg-2 control-label\" })");
                        sw.WriteLine("                    <div class=\"col-lg-10\">");
                        sw.WriteLine("                        @Html.LabelFor(model => model." + field.Key + ", new { @class = \"col-lg-2 control-label\" })");
                        sw.WriteLine("                    </div>");
                        sw.WriteLine("                </div>");
                    }

                    if (field.IsCheckBox)
                    {
                        sw.WriteLine("                <div class=\"form-group has-feedback \">");
                        sw.WriteLine("                    @Html.LabelFor(model => model." + field.Key + ", new { @class = \"col-lg-2 control-label\" })");
                        sw.WriteLine("                    <div class=\"col-lg-10\">");
                        sw.WriteLine("                        @Html.CheckBoxFor(model => model." + field.Key + ", new { @class = \" checkbox-slider toggle colored-blue \" })");
                        sw.WriteLine("                        <span class=\"text\"></span>");
                        sw.WriteLine("                    </div>");
                        sw.WriteLine("                </div>");
                    }

                    if (field.IsNestedDropDownList)
                    { }

                    if (field.IsImage)
                    {

                        sw.WriteLine("                @Html.HiddenFor(model => model." + field.Key + ")");
                        sw.WriteLine("                @Html.HiddenFor(model => model." + field.Key + "New)");
                        sw.WriteLine("                @Html.Hidden(\"" + field.Key + "NameNew\",\"\")");
                        sw.WriteLine("                @Html.HiddenFor(model => model.Is" + field.Key + "Remove)");
                        sw.WriteLine("                <div class=\"form-group has-feedback\">");
                        sw.WriteLine("                    @Html.LabelFor(model => model." + field.Key + ", new { @class = \"col-lg-2 control-label\" })");
                        sw.WriteLine("");
                        sw.WriteLine("                    <div class=\"col-lg-10\">");
                        sw.WriteLine("                        @Html.ValidationMessageFor(model => model." + field.Key + ", null, new { @class = \"help-block\" })");
                        sw.WriteLine("                        <div class=\"image-upload float-left\">");
                        sw.WriteLine("                            <div class=\"image-upload-container\">");
                        sw.WriteLine("                                <img src=\"@Url.Content(\"~/Content/Icons/empty.png\")\" alt=\"image upload\" id=\"img" + field.Key + "\" ");
                        sw.WriteLine("                                     style=\"max-width: 379px; min-width: 379px; max-height: 209px;\" />");
                        sw.WriteLine("                            </div>");
                        sw.WriteLine("                            <div class=\"image-hover-container\">");
                        sw.WriteLine("                                <input type=\"file\" name=\"fileToUpload" + field.Key + "\" id=\"fileToUpload" + field.Key + "\"");
                        sw.WriteLine("                                       style=\"width: 399px; height: 209px; overflow: hidden; position: absolute; top: 0; right: 0; margin: 0; opacity: 0; -ms-filter: 'alpha(opacity=0)'; font-size: 200px; direction: ltr; cursor: pointer; z-index: 20\" />");
                        sw.WriteLine("                                <span class=\"image-hover\" style=\"z-index: 0\"></span>");
                        sw.WriteLine("                                <span class=\"hover-text\" style=\"z-index: 0;\">تصویر " + ModelPersianName + "</span>");
                        sw.WriteLine("                            </div>");
                        sw.WriteLine("                        </div>");
                        sw.WriteLine("                        <div class=\"col-lg-6 col-lg-offset-1  \">");
                        sw.WriteLine("                            <a class=\"btn btn-default btn-sm shiny icon-only danger\" id=\"clearImage" + field.Key + "\"><i class=\"fa fa-times \"></i></a>");
                        sw.WriteLine("                            <span class=\"image-size\"> 1024 * 1024  </span>");
                        sw.WriteLine("                        </div>");
                        sw.WriteLine("                        <div class=\"upload-bar col-lg-6 col-lg-offset-1 \" style=\"opacity: 0; width: 399px; float: right;\" id=\"P_LoadingDiv" + field.Key + "\">");
                        sw.WriteLine("                            <span class=\"upload-bar-percent\" style=\"width: 0%;\" id=\"P_Loading" + field.Key + "\"></span>");
                        sw.WriteLine("                        </div>");
                        sw.WriteLine("                    </div>");
                        sw.WriteLine("                </div>");
                        sw.WriteLine("");

                    }
                }


                sw.WriteLine("");



                if (chkViewHaveOtherSeo.IsChecked == true)
                {
                    sw.WriteLine("                @Html.Partial(MVC.Shared.Views.Partial._BaseSeoView, Model)");
                }

                sw.WriteLine("");
                sw.WriteLine("");
                sw.WriteLine("                <div class=\"form-group\">");
                sw.WriteLine("                    <div class=\"col-lg-offset-4 col-lg-8\">");
                sw.WriteLine("                        <input class=\"btn btn-palegreen col-lg-3\" type=\"submit\" value=\"ویرایش\">");
                sw.WriteLine("                    </div>");
                sw.WriteLine("                </div>");
                sw.WriteLine("");
                sw.WriteLine("            }");
                sw.WriteLine("        </div>");
                sw.WriteLine("    </div>");
                sw.WriteLine("</div>");
                sw.WriteLine("");


                sw.WriteLine("@section Scripts {");
                sw.WriteLine("   @Scripts.Render(\"~/bundles/jqueryval\")");
                if (model._items.Any(x => x.IsCkEditor))
                {
                    sw.WriteLine("   @Scripts.Render(\"~/ckeditor/ckeditor.js\")");
                    sw.WriteLine("   @Scripts.Render(\"~/ckeditor/adapters/jquery.js\")");
                }
                sw.WriteLine("   @Scripts.Render(Url.Content(\"~/Content/ViewJs/" + AreaName + "/" + _controllerName + "/Edit.js\"))");
                sw.WriteLine("}");

            }
        }


        // AdminDetailsView
        private void WriteAdminDetailsViewElements(string fileName, string tableName)
        {
            var sda = SqlDatabaseAdapter;
            var columns = sda.GetTablesColumn(tableName, cmbDatabases.SelectedValue.ToString());
            using (var sw = File.AppendText(fileName))
            {

                sw.WriteLine("@model " + txtProjectName.Text + ".ViewModels." + _modelName + "." + AdminDetailsName);
                sw.WriteLine("");

                sw.WriteLine("@{");
                sw.WriteLine("    Layout = Url.Content(MVC." + AreaName + ".Shared.Views._" + AreaName + "Layout);");
                sw.WriteLine("}");

                sw.WriteLine("");

                sw.WriteLine("@section Title{");
                sw.WriteLine(" جزئیات " + ModelPersianName);
                sw.WriteLine("}");
                sw.WriteLine("@section PageHeader{");
                sw.WriteLine(" جزئیات " + ModelPersianName);
                sw.WriteLine("}");


                sw.WriteLine("<div class=\"col-lg-12 col-sm-12 col-xs-12\">");
                sw.WriteLine("    <div class=\"widget radius-bordered\">");
                sw.WriteLine("        <div class=\"widget-header\">");
                sw.WriteLine("            <span class=\"widget-caption\">جزئیات " + ModelPersianName + " </span>");
                sw.WriteLine("        </div>");
                sw.WriteLine("        <div class=\"widget-body\">");
                sw.WriteLine("");



                foreach (DataRow property in columns.Rows)
                {
                    if (_selectedFieldForAdminDetailsObser.Any(x => x.Field == property["Name"].ToString()))
                    {
                        var currentAdmin =
                            _selectedFieldForAdminDetailsObser.First(x => x.Field == property["Name"].ToString());

                        if (currentAdmin.IsCheckBox)
                        {
                            sw.WriteLine("            <div class=\"form-group row \">");
                            sw.WriteLine("                <div class=\"col-lg-2 control-label\">");
                            sw.WriteLine("                    @Html.DisplayNameFor(model => model." + currentAdmin.Field + ")");
                            sw.WriteLine("                </div>");
                            sw.WriteLine("                <div class=\"col-lg-10 control-label\">");
                            sw.WriteLine("                    @Html.LabelActiveDeactive(Model." + currentAdmin.Field + ", \"" + currentAdmin.TrueCheckBox + "\", \"" + currentAdmin.FalseCheckBox + "\")");
                            sw.WriteLine("                </div>");
                            sw.WriteLine("            </div>");
                        }
                        else if (currentAdmin.IsImage)
                        {
                            sw.WriteLine("            <div class=\"form-group row \">");
                            sw.WriteLine("                <div class=\"col-lg-4 control-label\">");
                            sw.WriteLine("                    @Html.DisplayNameFor(model => model." + currentAdmin.Field + ")");
                            sw.WriteLine("                </div>");
                            sw.WriteLine("                <div class=\"col-lg-8 text-center\">");
                            sw.WriteLine("                    @{");
                            sw.WriteLine("                        if (!string.IsNullOrEmpty(Model." + currentAdmin.Field + "))");
                            sw.WriteLine("                        {");
                            sw.WriteLine("                            <img src=\"@Url.Content(Model." + currentAdmin.Field + ")\" alt=\"Image\"  style=\"max-width: 379px; min-width: 379px; max-height: 209px;\" />");
                            sw.WriteLine("                        }");
                            sw.WriteLine("                    }");
                            sw.WriteLine("                </div>");
                            sw.WriteLine("            </div>");
                        }
                        else if (currentAdmin.IsHtml)
                        {
                            sw.WriteLine("            <div class=\"form-group row \">");
                            sw.WriteLine("                <div class=\"col-lg-2 control-label\">");
                            sw.WriteLine("                    @Html.DisplayNameFor(model => model." + currentAdmin.Field + ")");
                            sw.WriteLine("                </div>");
                            sw.WriteLine("                <div class=\"col-lg-10 control-label\">");
                            sw.WriteLine("                    @Html.Raw(model => model." + currentAdmin.Field + ")");
                            sw.WriteLine("                </div>");
                            sw.WriteLine("            </div>");
                        }
                        else
                        {
                            sw.WriteLine("            <div class=\"form-group row \">");
                            sw.WriteLine("                <div class=\"col-lg-2 control-label\">");
                            sw.WriteLine("                    @Html.DisplayNameFor(model => model." + currentAdmin.Field + ")");
                            sw.WriteLine("                </div>");
                            sw.WriteLine("                <div class=\"col-lg-10 control-label\">");
                            sw.WriteLine("                    @Html.DisplayFor(model => model." + currentAdmin.Field + ")");
                            sw.WriteLine("                </div>");
                            sw.WriteLine("            </div>");
                        }

                        sw.WriteLine("");
                    }
                }




                if (chkAdminDetailsHaveSeo.IsChecked == true)
                {
                    sw.WriteLine("                @Html.Partial(MVC.Shared.Views.Partial._BaseSeoDetailsView, Model)");
                }

                sw.WriteLine("");
                sw.WriteLine("");
                sw.WriteLine("        </div>");
                sw.WriteLine("    </div>");
                sw.WriteLine("</div>");
                sw.WriteLine("");
                sw.WriteLine("");

                if (chkHaveOtherLanguageList.IsChecked == true)
                {
                    sw.WriteLine("<div>");
                    sw.WriteLine("    @Html.Partial(MVC." + AreaName + "." + _controllerName + ".Views._Details, Model.OlDetails)");
                    sw.WriteLine("</div>");
                    sw.WriteLine("");
                }

            }
        }

        /***********************************************************        JavaScript       ***********************************************************/

        // AdminIndexJS
        private void btnGenerateAdminIndexJS()
        {
            FillRootDirectory();
            CheckTempFolder();
            CheckModelFolder();
            CheckJSFolder();
            CreateFile(AdminIndexJSRoot);

            WriteAdminIndexJSElements(AdminIndexJSRoot);
        }
        private void WriteAdminIndexJSElements(string fileName)
        {
            using (var sw = File.AppendText(fileName))
            {

                sw.WriteLine("");

                sw.WriteLine("(function () {");
                sw.WriteLine("    $(function () {");
                sw.WriteLine("        \"use strict \";");
                sw.WriteLine("");
                sw.WriteLine("");
                sw.WriteLine("        $(document).ready(function () {");
                sw.WriteLine("");
                sw.WriteLine("            $(document).on(\"click\", \".filterItem\", function () {");
                sw.WriteLine("                $(this).toggleClass(\"selectedFilterItem\");");
                sw.WriteLine("            });");
                sw.WriteLine("");
                sw.WriteLine("            if (Number(rowCount) == 0) {");
                sw.WriteLine("                $('#editabledatatable_wrapper').hide(10);");
                sw.WriteLine("                $('#" + _modelName + "FilterRow').hide(10);");
                sw.WriteLine("                $('.alert-info').fadeIn(10);");
                sw.WriteLine("            }");
                sw.WriteLine("");
                sw.WriteLine("");
                sw.WriteLine("            $(document).on(\"click\", \".GridPaging\", function () {");
                sw.WriteLine("                var url = $(this).data('url');");
                sw.WriteLine("                var pn = $(this).data('pn');");
                sw.WriteLine("                ReFillPage(url, $('#pageSelector').val(), pn);");
                sw.WriteLine("            });");
                sw.WriteLine("");
                sw.WriteLine("            $(document).on(\"click\", \".filterItem\", function () {");
                sw.WriteLine("                if ($('#filterText').val().length > 0) {");
                sw.WriteLine("                    ReFillPage();");
                sw.WriteLine("                }");
                sw.WriteLine("            });");
                sw.WriteLine("");
                sw.WriteLine("");
                sw.WriteLine("            $(document).on(\"keypress\", \"#filterText\", function (e) {");
                sw.WriteLine("                if (e.keyCode == 13 && $('.selectedFilterItem').length > 0) {");
                sw.WriteLine("                    ReFillPage();");
                sw.WriteLine("                }");
                sw.WriteLine("            });");
                sw.WriteLine("");
                sw.WriteLine("            $(document).on(\"click\", \"#delete" + _modelName + "Button\", function () {");
                sw.WriteLine("                var cel = $($($(this).parent().parent()).children()[0]).children()[0];");
                sw.WriteLine("");
                sw.WriteLine("                var " + _modelName.ToLower() + "id = $(this).data('rowid');");
                sw.WriteLine("                var rowindex = $(this).data('rowindex');");
                sw.WriteLine("                Delete" + _modelName + "(" + _modelName.ToLower() + "id, rowindex,");
                sw.WriteLine("                       $(cel)");
                sw.WriteLine("                            .clone()    //clone the element");
                sw.WriteLine("                            .children() //select all the children");
                sw.WriteLine("                            .remove()   //remove all the children");
                sw.WriteLine("                            .end()  //again go back to selected element");
                sw.WriteLine("                            .text()");
                sw.WriteLine("                            .replace(/(\\r\\n|\\n|\\r)/gm, \"\")  //clear ...");
                sw.WriteLine("                );");
                sw.WriteLine("            });");
                sw.WriteLine("");
                sw.WriteLine("            $(document).on(\"change\", \"#pageSelector\", function () {");
                sw.WriteLine("                ReFillPage();");
                sw.WriteLine("            });");
                sw.WriteLine("");
                sw.WriteLine("            $(document).on(\"change\", \"#GridLanguageSelector\", function () {");
                sw.WriteLine("                ReFillPage();");
                sw.WriteLine("            });");
                sw.WriteLine("");
                sw.WriteLine("            $(document).on(\"click\", \"#editabledatatable > thead  > tr > th > div\", function () {");
                sw.WriteLine("                if ($(this).hasClass('sorting') ||");
                sw.WriteLine("                    $(this).hasClass('sorting_desc') ||");
                sw.WriteLine("                    $(this).hasClass('sorting_asc')) {");
                sw.WriteLine("                    SetFilterClass($(this));");
                sw.WriteLine("                }");
                sw.WriteLine("            });");
                sw.WriteLine("");
                sw.WriteLine("        });");
                sw.WriteLine("");
                sw.WriteLine("        function SetFilterClass(source) {");
                sw.WriteLine("            var currentClass = '';");
                sw.WriteLine("            if ($(source).hasClass('sorting')) {");
                sw.WriteLine("                currentClass = 'sorting';");
                sw.WriteLine("            } else if ($(source).hasClass('sorting_desc')) {");
                sw.WriteLine("                currentClass = 'sorting_desc';");
                sw.WriteLine("            } else if ($(source).hasClass('sorting_asc')) {");
                sw.WriteLine("                currentClass = 'sorting_asc';");
                sw.WriteLine("            }");
                sw.WriteLine("");
                sw.WriteLine("            $('#editabledatatable > thead  > tr > th > div').each(function () {");
                sw.WriteLine("                if ($(this).hasClass('sorting') || $(this).hasClass('sorting_desc') || $(this).hasClass('sorting_asc')) {");
                sw.WriteLine("                    $(this).removeClass('sorting').removeClass('sorting_desc').removeClass('sorting_asc');");
                sw.WriteLine("                    $(this).addClass('sorting');");
                sw.WriteLine("                }");
                sw.WriteLine("            });");
                sw.WriteLine("");
                sw.WriteLine("            switch (currentClass) {");
                sw.WriteLine("                case 'sorting':");
                sw.WriteLine("                    $(source).addClass('sorting_asc');");
                sw.WriteLine("                    break;");
                sw.WriteLine("                case 'sorting_desc':");
                sw.WriteLine("                    $(source).addClass('sorting_asc');");
                sw.WriteLine("                    break;");
                sw.WriteLine("                case 'sorting_asc':");
                sw.WriteLine("                    $(source).addClass('sorting_desc');");
                sw.WriteLine("                    break;");
                sw.WriteLine("            }");
                sw.WriteLine("            $(source).removeClass(currentClass);");
                sw.WriteLine("            ReFillPage();");
                sw.WriteLine("");
                sw.WriteLine("        }");
                sw.WriteLine("");
                sw.WriteLine("");
                sw.WriteLine("        function ReFillPage(url, pageSize, pageNum, sortField, asc, searchField, search, culture) {");
                sw.WriteLine("");
                sw.WriteLine("            if (url == undefined) {");
                sw.WriteLine("                url = urlReFillPage;");
                sw.WriteLine("            }");
                sw.WriteLine("");
                sw.WriteLine("            if (sortField == undefined) {");
                sw.WriteLine("                if (parseInt($('.sorting_asc').length.toString()) > 0) {");
                sw.WriteLine("                    sortField = $('.sorting_asc').parent().data('kss');");
                sw.WriteLine("                    asc = true;");
                sw.WriteLine("                } else if (parseInt($('.sorting_desc').length.toString()) > 0) {");
                sw.WriteLine("                    sortField = $('.sorting_desc').parent().data('kss');");
                sw.WriteLine("                    asc = false;");
                sw.WriteLine("                }");
                sw.WriteLine("            }");
                sw.WriteLine("");
                sw.WriteLine("            if (searchField == undefined) {");
                sw.WriteLine("                searchField = new Array();");
                sw.WriteLine("                var filtersItem = $('.selectedFilterItem');");
                sw.WriteLine("                for (var i = 0; i < filtersItem.length; i++) {");
                sw.WriteLine("                    searchField.push($(filtersItem[i]).parent().data('kss'));");
                sw.WriteLine("                }");
                sw.WriteLine("            }");
                sw.WriteLine("");
                sw.WriteLine("            if (search == undefined) {");
                sw.WriteLine("                search = $('#filterText').val();");
                sw.WriteLine("            }");
                sw.WriteLine("");
                sw.WriteLine("            if (pageSize == undefined) {");
                sw.WriteLine("                pageSize = $('#pageSelector').val();");
                sw.WriteLine("            }");
                sw.WriteLine("");
                sw.WriteLine("            if (pageNum == undefined) {");
                sw.WriteLine("                pageNum = $('.active > a').text();");
                sw.WriteLine("            }");
                sw.WriteLine("");
                sw.WriteLine("            if (culture == undefined) {");
                sw.WriteLine("                culture = $('#GridLanguageSelector').val();");
                sw.WriteLine("            }");
                sw.WriteLine("");
                sw.WriteLine("            DisplayLoading();");
                sw.WriteLine("            $.ajax({");
                sw.WriteLine("                type: \"POST\",");
                sw.WriteLine("                url: url,");
                sw.WriteLine("                data: JSON.stringify({");
                sw.WriteLine("                    culture: culture,");
                sw.WriteLine("                    pageSize: pageSize,");
                sw.WriteLine("                    pageNum: pageNum,");
                sw.WriteLine("                    sortField: sortField,");
                sw.WriteLine("                    asc: asc,");
                sw.WriteLine("                    searchField: searchField,");
                sw.WriteLine("                    search: search");
                sw.WriteLine("                }),");
                sw.WriteLine("                contentType: \"application/json;charset = utf - 8\",");
                sw.WriteLine("                dataType: \"html\",");
                sw.WriteLine("                // controller is returning the json data");
                sw.WriteLine("                success: function (result) {");
                sw.WriteLine("                    $('.ajaxContainer').html(result);");
                sw.WriteLine("                },");
                sw.WriteLine("                error: function (result) {");
                sw.WriteLine("                    ShowModal('danger', 'خطا', 'فرایند با خطا مواجه گردید !', null);");
                sw.WriteLine("                }");
                sw.WriteLine("            });");
                sw.WriteLine("            HideLoading();");
                sw.WriteLine("");
                sw.WriteLine("        }");
                sw.WriteLine("");
                sw.WriteLine("        function Delete" + _modelName + "(" + _modelName.ToLower() + "Id, rowIndex, objectName) {");
                sw.WriteLine("");
                sw.WriteLine("            $.ajax({");
                sw.WriteLine("                type: \"GET\",");
                sw.WriteLine("                url: urlDelete" + _modelName + ",");
                sw.WriteLine("                data:{");
                sw.WriteLine("                    id: " + _modelName.ToLower() + "Id,");
                sw.WriteLine("                    rowIndex: rowIndex,");
                sw.WriteLine("                    ObjectName: objectName");
                sw.WriteLine("                },");
                sw.WriteLine("                dataType: \"html\",");
                sw.WriteLine("                success: function (response) {");
                sw.WriteLine("                    if (response !== '') {");
                sw.WriteLine("                        $('#bs-example-modal-sm-DeleteContainer').html(response);");
                sw.WriteLine("");
                sw.WriteLine("                    } else {");
                sw.WriteLine("                        ShowModal('danger', 'خطا', 'فرایند با خطا مواجه گردید !', null);");
                sw.WriteLine("                    }");
                sw.WriteLine("                },");
                sw.WriteLine("                error: function (response) {");
                sw.WriteLine("                    ShowModal('danger', 'خطا', 'فرایند با خطا مواجه گردید !', null);");
                sw.WriteLine("                }");
                sw.WriteLine("");
                sw.WriteLine("            });");
                sw.WriteLine("");
                sw.WriteLine("");
                sw.WriteLine("        }");
                sw.WriteLine("");
                sw.WriteLine("    });");
                sw.WriteLine("})();");

                sw.WriteLine("");






            }
        }

        // AdminOlIndexJS
        private void btnGenerateAdminOlIndexJS()
        {
            FillRootDirectory();
            CheckTempFolder();
            CheckModelFolder();
            CheckJSFolder();
            CreateFile(AdminOlIndexJSRoot);

            WriteAdminOlIndexJSElements(AdminOlIndexJSRoot);
        }
        private void WriteAdminOlIndexJSElements(string fileName)
        {
            using (var sw = File.AppendText(fileName))
            {

                sw.WriteLine("");

                sw.WriteLine("(function () {");
                sw.WriteLine("    $(function () {");
                sw.WriteLine("        \"use strict \";");
                sw.WriteLine("");

                sw.WriteLine("");
                sw.WriteLine("        function delete" + _modelName + "(languageid, rowIndex, objectName) {");
                sw.WriteLine("");
                sw.WriteLine("            $.ajax({");
                sw.WriteLine("                type: \"GET\",");
                sw.WriteLine("                url: urlDelete" + _modelName + ",");
                sw.WriteLine("                data:{");
                sw.WriteLine("                    id: languageid,");
                sw.WriteLine("                    rowIndex: rowIndex,");
                sw.WriteLine("                    ObjectName: objectName");
                sw.WriteLine("                },");
                sw.WriteLine("                dataType: \"html\",");
                sw.WriteLine("                success: function (response) {");
                sw.WriteLine("                    if (response !== '') {");
                sw.WriteLine("                        $('#bs-example-modal-sm-DeleteContainer').html(response);");
                sw.WriteLine("");
                sw.WriteLine("                    } else {");
                sw.WriteLine("                        ShowModal('danger', 'خطا', 'فرایند با خطا مواجه گردید !', null);");
                sw.WriteLine("                    }");
                sw.WriteLine("                },");
                sw.WriteLine("                error: function (response) {");
                sw.WriteLine("                    ShowModal('danger', 'خطا', 'فرایند با خطا مواجه گردید !', null);");
                sw.WriteLine("                }");
                sw.WriteLine("");
                sw.WriteLine("            });");
                sw.WriteLine("");
                sw.WriteLine("        }");
                sw.WriteLine("");

                sw.WriteLine("");
                sw.WriteLine("        $(document).ready(function () {");
                sw.WriteLine("");
                sw.WriteLine("            $(document).on(\"click\", \"#delete" + _modelName + "OlButton\", function () {");
                sw.WriteLine("                var childCount = $($(this).parent().parent()).children().length;");
                sw.WriteLine("                var cel = $($(this).parent().parent()).children()[childCount - 2];");
                sw.WriteLine("");
                sw.WriteLine("                var languageid = $(this).data('rowid');");
                sw.WriteLine("                var rowindex = $(this).data('rowindex');");
                sw.WriteLine("");
                sw.WriteLine("");
                sw.WriteLine("                delete" + _modelName + "(languageid, rowindex,' زبان ' + ");
                sw.WriteLine("                     $(cel).text().replace(/(\\r\\n|\\n|\\r)/gm, \"\") );");
                sw.WriteLine("            });");
                sw.WriteLine("");

                sw.WriteLine("");
                sw.WriteLine("");
                sw.WriteLine("    });");
                sw.WriteLine("})();");

                sw.WriteLine("");






            }
        }


        // AdminCreateJS
        private void btnGenerateAdminCreateJS()
        {
            FillRootDirectory();
            CheckTempFolder();
            CheckModelFolder();
            CheckJSFolder();
            CreateFile(AdminCreateJSRoot);

            WriteAdminCreateJSElements(AdminCreateJSRoot);
        }

        private void WriteAdminCreateJSElements(string fileName)
        {
            var model = new AdminCreateView()
            {
                _items = (ObservableCollection<AdminCreateViewItem>)dgAdminCreateView.ItemsSource
            };

            using (var sw = File.AppendText(fileName))
            {

                sw.WriteLine("");

                sw.WriteLine("(function () {");
                sw.WriteLine("    $(function () {");
                sw.WriteLine("        \"use strict \";");
                sw.WriteLine("");


                foreach (var field in model._items.Where(x => x.IsImage).ToList())
                {


                    sw.WriteLine("        function remove" + field.Key + "() {");
                    sw.WriteLine("            if (isDefaultPic('" + field.Key + "')) {");
                    sw.WriteLine("                return;");
                    sw.WriteLine("            }");
                    sw.WriteLine("");
                    sw.WriteLine("            DisplayLoading();");
                    sw.WriteLine("            $.ajax({");
                    sw.WriteLine("                type: \"POST\",");
                    sw.WriteLine("                url: urlRemove" + field.Key + ".replace('" + field.Key + "', document.getElementById('" + field.Key + "').value),");
                    sw.WriteLine("                data: {");
                    sw.WriteLine("                    __RequestVerificationToken: $('input[name=\"__RequestVerificationToken\"]').val()");
                    sw.WriteLine("");
                    sw.WriteLine("                }, //{ __RequestVerificationToken: token },");
                    sw.WriteLine("                dataType: \"json\",");
                    sw.WriteLine("                cache: false,");
                    sw.WriteLine("                success: function (response) {");
                    sw.WriteLine("                    if (response.success == true) {");
                    sw.WriteLine("                        document.getElementById('" + field.Key + "Name').value = response.name;");
                    sw.WriteLine("                        document.getElementById('" + field.Key + "').value = response.url;");
                    sw.WriteLine("                        $('#img" + field.Key + "').attr('src', response.url + \"?\" + new Date());");
                    sw.WriteLine("                    } else {");
                    sw.WriteLine("                        ShowModal('warning', 'اخطار', response.message, null);");
                    sw.WriteLine("                    }");
                    sw.WriteLine("                },");
                    sw.WriteLine("                error: function (jqXHR, textStatus, errorThrown) {");
                    sw.WriteLine("                    //alert(jqXHR.status);");
                    sw.WriteLine("                    //alert(textStatus);");
                    sw.WriteLine("                    //alert(errorThrown);");
                    sw.WriteLine("                    ShowModal('danger', 'خطا', 'فرایند با خطا مواجه گردید !', null);");
                    sw.WriteLine("                }");
                    sw.WriteLine("");
                    sw.WriteLine("            });");
                    sw.WriteLine("            HideLoading();");
                    sw.WriteLine("        }");

                    sw.WriteLine("");


                    sw.WriteLine("        function upload" + field.Key + "() {");
                    sw.WriteLine("");
                    sw.WriteLine("            var tfuData = $('#fileToUpload" + field.Key + "').val();");
                    sw.WriteLine("            document.getElementById('P_LoadingDiv" + field.Key + "').setAttribute(\"style\", \" width: 399px; float: right;opacity:1\");");
                    sw.WriteLine("");
                    sw.WriteLine("            if (tfuData.length == 0) {");
                    sw.WriteLine("                return;");
                    sw.WriteLine("            }");
                    sw.WriteLine("");
                    sw.WriteLine("            var tExtension = tfuData.substring(tfuData.lastIndexOf('.') + 1).toLowerCase();");
                    sw.WriteLine("            if (tExtension == \"jpeg\" || tExtension == \"jpg\" || tExtension == \"png\") {");
                    sw.WriteLine("                var data = new FormData($('form').get(0));");
                    sw.WriteLine("                data.append('file', document.getElementById('fileToUpload" + field.Key + "').files[0]);");
                    sw.WriteLine("");
                    sw.WriteLine("                $.ajax({");
                    sw.WriteLine("                    type: 'POST',");
                    sw.WriteLine("                    url: urlUpload" + field.Key + ".replace(\"" + field.Key + "NameNew\", document.getElementById('" + field.Key + "NameNew').value),");
                    sw.WriteLine("                    data: data,");
                    sw.WriteLine("                    //contentType: \"application/json; charset=utf-8\",");
                    sw.WriteLine("                    async: true,");
                    sw.WriteLine("                    processData: false,");
                    sw.WriteLine("                    contentType: false,");
                    sw.WriteLine("                    xhr: function () {");
                    sw.WriteLine("                        var req = $.ajaxSettings.xhr();");
                    sw.WriteLine("                        if (req) {");
                    sw.WriteLine("                            req.upload.addEventListener('progress', function (ev) {");
                    sw.WriteLine("                                progress = Math.round(ev.loaded * 100 / ev.total);");
                    sw.WriteLine("                                document.getElementById('P_Loading" + field.Key + "').setAttribute(\"style\", \"width:\" + progress + \"%\");");
                    sw.WriteLine("                            }, false);");
                    sw.WriteLine("                        }");
                    sw.WriteLine("                        return req;");
                    sw.WriteLine("                    },");
                    sw.WriteLine("                    success: function (response) {");
                    sw.WriteLine("                        if (response.success == true) {");
                    sw.WriteLine("                            document.getElementById('" + field.Key + "Name').value = response.name;");
                    sw.WriteLine("                            document.getElementById('" + field.Key + "').value = response.url;");
                    sw.WriteLine("                            $('#img" + field.Key + "').attr('src', response.url + \"?\" + new Date());");
                    sw.WriteLine("                        } else {");
                    sw.WriteLine("                            ShowModal('warning', 'اخطار', response.message, null);");
                    sw.WriteLine("                        }");
                    sw.WriteLine("");
                    sw.WriteLine("                        document.getElementById('P_LoadingDiv" + field.Key + "').setAttribute(\"style\", \" width: 399px; float: right;opacity:0\");");
                    sw.WriteLine("");
                    sw.WriteLine("                    },");
                    sw.WriteLine("                    error: function (jqXHR, textStatus, errorThrown) {");
                    sw.WriteLine("                        //alert(jqXHR.status);");
                    sw.WriteLine("                        //alert(textStatus);");
                    sw.WriteLine("                        //alert(errorThrown);");
                    sw.WriteLine("                        ShowModal('danger', 'خطا', 'فرایند با خطا مواجه گردید !', null);");
                    sw.WriteLine("                        document.getElementById('P_LoadingDiv" + field.Key + "').setAttribute(\"style\", \" width: 399px; float: right;opacity:0\");");
                    sw.WriteLine("                    }");
                    sw.WriteLine("                });");
                    sw.WriteLine("            } else {");
                    sw.WriteLine("                ShowModal('danger', 'خطا', 'لطفا فقط تصاویر با پسوند Jpg و یا  png و یا Jpeg انتخاب نمایید !', null);");
                    sw.WriteLine("            }");
                    sw.WriteLine("            //HideLoading();");
                    sw.WriteLine("        }");




                }

                if (model._items.Any(x => x.IsImage))
                {
                    sw.WriteLine("");
                    sw.WriteLine("        function isDefaultPic(source) {");
                    sw.WriteLine("            if (document.getElementById(source).value.length == 0 || document.getElementById(source).value.indexOf('empty.png') >= 0) {");
                    sw.WriteLine("                return true;");
                    sw.WriteLine("            }");
                    sw.WriteLine("            return false;");
                    sw.WriteLine("        }");
                    sw.WriteLine("");
                }

                sw.WriteLine("");

                sw.WriteLine("        $(document).ready(function () {");

                foreach (var field in model._items.Where(x => x.IsCkEditor).ToList())
                {
                    sw.WriteLine("            $('#" + field.Key + "').ckeditor();");
                }

                sw.WriteLine("");
                sw.WriteLine("");

                foreach (var field in model._items.Where(x => x.IsImage).ToList())
                {

                    sw.WriteLine("            if (" + field.Key + " != null) {");
                    sw.WriteLine("                if (" + field.Key + ".length > 0) {");
                    sw.WriteLine("                    $('#img" + field.Key + "').attr('src', " + field.Key + " + \"?\" + new Date());");
                    sw.WriteLine("                    document.getElementById('" + field.Key + "Name').value = " + field.Key + "FileName;");
                    sw.WriteLine("                }");
                    sw.WriteLine("            }");

                    sw.WriteLine("");

                    sw.WriteLine("            $(document).on(\"click\", \"#clearImage" + field.Key + "\", function () {");
                    sw.WriteLine("                remove" + field.Key + "();");
                    sw.WriteLine("            });");

                    sw.WriteLine("");

                    sw.WriteLine("            $(document).on(\"change\", \"#fileToUpload" + field.Key + "\", function () {");
                    sw.WriteLine("                var url = window.URL || window.webkitURL;");
                    sw.WriteLine("                var file, img;");
                    sw.WriteLine("                if ((file = this.files[0])) {");
                    sw.WriteLine("                    img = new Image();");
                    sw.WriteLine("                    img.onload = function () {");
                    if (field.IsLandscape)
                    {

                        sw.WriteLine("                        if (this.width < this.height) {");
                        sw.WriteLine("                            ShowModal('warning', 'warning', 'The image must be landscape.', null);");
                        sw.WriteLine("                        } else {");
                        sw.WriteLine("                            upload" + field.Key + "();");
                        sw.WriteLine("                        }");
                    }
                    else
                    {
                        sw.WriteLine("                            upload" + field.Key + "();");
                    }
                    sw.WriteLine("                    };");
                    sw.WriteLine("                    img.src = url.createObjectURL(file);");
                    sw.WriteLine("                }");
                    sw.WriteLine("            });");
                    sw.WriteLine("");



                }





                sw.WriteLine("");
                sw.WriteLine("");
                sw.WriteLine("");
                sw.WriteLine("");
                sw.WriteLine("        });");




                sw.WriteLine("");
                sw.WriteLine("    });");
                sw.WriteLine("})();");






            }
        }


        // AdminEditJS
        private void btnGenerateAdminEditJS()
        {
            FillRootDirectory();
            CheckTempFolder();
            CheckModelFolder();
            CheckJSFolder();
            CreateFile(AdminEditJSRoot);

            WriteAdminEditJSElements(AdminEditJSRoot);
        }

        private void WriteAdminEditJSElements(string fileName)
        {
            var model = new AdminCreateView()
            {
                _items = (ObservableCollection<AdminCreateViewItem>)dgAdminCreateView.ItemsSource
            };

            using (var sw = File.AppendText(fileName))
            {

                sw.WriteLine("");

                sw.WriteLine("(function () {");
                sw.WriteLine("    $(function () {");
                sw.WriteLine("        \"use strict \";");
                sw.WriteLine("");


                sw.WriteLine("        function clearNewImage() {");
                sw.WriteLine("");
                foreach (var field in model._items.Where(x => x.IsImage).ToList())
                {
                    sw.WriteLine("            document.getElementById('Is" + field.Key + "Remove').value = false;");
                    sw.WriteLine("            document.getElementById('" + field.Key + "NameNew').value = '';");
                    sw.WriteLine("            document.getElementById('" + field.Key + "New').value = '';");
                    sw.WriteLine("");
                }
                sw.WriteLine("        }");
                sw.WriteLine("");


                foreach (var field in model._items.Where(x => x.IsImage).ToList())
                {


                    sw.WriteLine("        function remove" + field.Key + "() {");
                    sw.WriteLine("            document.getElementById('Is" + field.Key + "Remove').value = false;");
                    sw.WriteLine("            document.getElementById('" + field.Key + "NameNew').value = '';");
                    sw.WriteLine("            document.getElementById('" + field.Key + "New').value = '';");
                    sw.WriteLine("            $('#img" + field.Key + "').attr('src', emptysrc + \"?\" + new Date());");
                    sw.WriteLine("        }");

                    sw.WriteLine("");


                    sw.WriteLine("        function upload" + field.Key + "() {");
                    sw.WriteLine("");
                    sw.WriteLine("            var tfuData = $('#fileToUpload" + field.Key + "').val();");
                    sw.WriteLine("            document.getElementById('P_LoadingDiv" + field.Key + "').setAttribute(\"style\", \" width: 399px; float: right;opacity:1\");");
                    sw.WriteLine("");
                    sw.WriteLine("            if (tfuData.length == 0) {");
                    sw.WriteLine("                return;");
                    sw.WriteLine("            }");
                    sw.WriteLine("");
                    sw.WriteLine("            var tExtension = tfuData.substring(tfuData.lastIndexOf('.') + 1).toLowerCase();");
                    sw.WriteLine("            if (tExtension == \"jpeg\" || tExtension == \"jpg\" || tExtension == \"png\") {");
                    sw.WriteLine("                var data = new FormData($('form').get(0));");
                    sw.WriteLine("                data.append('file', document.getElementById('fileToUpload" + field.Key + "').files[0]);");
                    sw.WriteLine("");
                    sw.WriteLine("                $.ajax({");
                    sw.WriteLine("                    type: 'POST',");
                    sw.WriteLine("                    url: urlUpload" + field.Key + ".replace(\"" + field.Key + "NameNew\", document.getElementById('" + field.Key + "NameNew').value),");
                    sw.WriteLine("                    data: data,");
                    sw.WriteLine("                    //contentType: \"application/json; charset=utf-8\",");
                    sw.WriteLine("                    async: true,");
                    sw.WriteLine("                    processData: false,");
                    sw.WriteLine("                    contentType: false,");
                    sw.WriteLine("                    xhr: function () {");
                    sw.WriteLine("                        var req = $.ajaxSettings.xhr();");
                    sw.WriteLine("                        if (req) {");
                    sw.WriteLine("                            req.upload.addEventListener('progress', function (ev) {");
                    sw.WriteLine("                                progress = Math.round(ev.loaded * 100 / ev.total);");
                    sw.WriteLine("                                document.getElementById('P_Loading" + field.Key + "').setAttribute(\"style\", \"width:\" + progress + \"%\");");
                    sw.WriteLine("                            }, false);");
                    sw.WriteLine("                        }");
                    sw.WriteLine("                        return req;");
                    sw.WriteLine("                    },");
                    sw.WriteLine("                    success: function (response) {");
                    sw.WriteLine("                        if (response.success == true) {");
                    sw.WriteLine("                            document.getElementById('" + field.Key + "NameNew').value = response.name;");
                    sw.WriteLine("                            document.getElementById('" + field.Key + "Name').value = response.url;");
                    sw.WriteLine("                            $('#img" + field.Key + "').attr('src', response.url + \"?\" + new Date());");
                    sw.WriteLine("                            document.getElementById('Is" + field.Key + "Remove').value = true;");
                    sw.WriteLine("                        } else {");
                    sw.WriteLine("                            ShowModal('warning', 'اخطار', response.message, null);");
                    sw.WriteLine("                        }");
                    sw.WriteLine("");
                    sw.WriteLine("                        document.getElementById('P_LoadingDiv" + field.Key + "').setAttribute(\"style\", \" width: 399px; float: right;opacity:0\");");
                    sw.WriteLine("");
                    sw.WriteLine("                    },");
                    sw.WriteLine("                    error: function (jqXHR, textStatus, errorThrown) {");
                    sw.WriteLine("                        ShowModal('danger', 'خطا', 'فرایند با خطا مواجه گردید !', null);");
                    sw.WriteLine("                        document.getElementById('P_LoadingDiv" + field.Key + "').setAttribute(\"style\", \" width: 399px; float: right;opacity:0\");");
                    sw.WriteLine("                    }");
                    sw.WriteLine("                });");
                    sw.WriteLine("            } else {");
                    sw.WriteLine("                ShowModal('danger', 'خطا', 'لطفا فقط تصاویر با پسوند Jpg و یا  png و یا Jpeg انتخاب نمایید !', null);");
                    sw.WriteLine("            }");
                    sw.WriteLine("            //HideLoading();");
                    sw.WriteLine("        }");




                }

                if (model._items.Any(x => x.IsImage))
                {
                    sw.WriteLine("");
                    sw.WriteLine("        function isDefaultPic(source) {");
                    sw.WriteLine("            if (document.getElementById(source).value.length == 0 || document.getElementById(source).value.indexOf('empty.png') >= 0) {");
                    sw.WriteLine("                return true;");
                    sw.WriteLine("            }");
                    sw.WriteLine("            return false;");
                    sw.WriteLine("        }");
                    sw.WriteLine("");
                }

                sw.WriteLine("");

                sw.WriteLine("        $(document).ready(function () {");

                foreach (var field in model._items.Where(x => x.IsCkEditor).ToList())
                {
                    sw.WriteLine("            $('#" + field.Key + "').ckeditor();");
                }

                sw.WriteLine("");
                sw.WriteLine("");

                foreach (var field in model._items.Where(x => x.IsImage).ToList())
                {

                    sw.WriteLine("            if (" + field.Key + " != null) {");
                    sw.WriteLine("                if (" + field.Key + ".length > 0) {");
                    sw.WriteLine("                    $('#img" + field.Key + "').attr('src', " + field.Key + " + \"?\" + new Date());");
                    //sw.WriteLine("                    document.getElementById('" + field.Key + "Name').value = " + field.Key + "FileName;");
                    sw.WriteLine("                }");
                    sw.WriteLine("            }");

                    sw.WriteLine("");

                    sw.WriteLine("            $(document).on(\"click\", \"#clearImage" + field.Key + "\", function () {");
                    sw.WriteLine("                remove" + field.Key + "();");
                    sw.WriteLine("            });");

                    sw.WriteLine("");

                    sw.WriteLine("            $(document).on(\"change\", \"#fileToUpload" + field.Key + "\", function () {");
                    sw.WriteLine("                var url = window.URL || window.webkitURL;");
                    sw.WriteLine("                var file, img;");
                    sw.WriteLine("                if ((file = this.files[0])) {");
                    sw.WriteLine("                    img = new Image();");
                    sw.WriteLine("                    img.onload = function () {");
                    if (field.IsLandscape)
                    {

                        sw.WriteLine("                        if (this.width < this.height) {");
                        sw.WriteLine("                            ShowModal('warning', 'warning', 'The image must be landscape.', null);");
                        sw.WriteLine("                        } else {");
                        sw.WriteLine("                            upload" + field.Key + "();");
                        sw.WriteLine("                        }");
                    }
                    else
                    {
                        sw.WriteLine("                            upload" + field.Key + "();");
                    }
                    sw.WriteLine("                    };");
                    sw.WriteLine("                    img.src = url.createObjectURL(file);");
                    sw.WriteLine("                }");
                    sw.WriteLine("            });");
                    sw.WriteLine("");



                }





                sw.WriteLine("");
                sw.WriteLine("");
                sw.WriteLine("            clearNewImage();");
                sw.WriteLine("");
                sw.WriteLine("        });");




                sw.WriteLine("");
                sw.WriteLine("    });");
                sw.WriteLine("})();");






            }
        }

    }
}
