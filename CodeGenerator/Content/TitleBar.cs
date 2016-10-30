using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Controls.Local
{
    /// <summary>
    /// ========================================
    /// .NET Framework 3.0 Custom Control
    /// ========================================
    ///
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:TestAlex"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:TestAlex;assembly=TestAlex"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file. Note that Intellisense in the
    /// XML editor does not currently work on custom controls and its child elements.
    ///
    ///     <MyNamespace:TitleBar/>
    ///
    /// </summary>
    /// 

    public class TitleBar : System.Windows.Controls.Control
    {
        ImageButton closeButton;
        ImageButton maxButton;
        ImageButton minButton;
        double Yposition;
        bool ISMouseDown = false;

        public TitleBar()
        {
            this.MouseLeftButtonDown += OnTitleBarLeftButtonDown;
            this.MouseMove += win1_MouseMove_1;
            this.MouseDoubleClick += TitleBar_MouseDoubleClick;
            this.Loaded += TitleBar_Loaded;
            Window window = this.TemplatedParent as Window;
            if (window != null) window.StateChanged += window_StateChanged;
        }

        void window_StateChanged(object sender, EventArgs e)
        {
            Window window = this.TemplatedParent as Window;
            if (window.WindowState != WindowState.Maximized)
            {
                maxButton.ImageNormal = "/Content/images/defaultmax.png";
            }
            else
            {
                maxButton.ImageNormal = "/Content/images/Restore.png";
            }
        }

        void TitleBar_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MaxButton_Click(sender, e);
        }

        void TitleBar_Loaded(object sender, RoutedEventArgs e)
        {

            closeButton = (ImageButton)this.Template.FindName("CloseButton", this);
            minButton = (ImageButton)this.Template.FindName("MinButton", this);
            maxButton = (ImageButton)this.Template.FindName("MaxButton", this);

            closeButton.Click += new RoutedEventHandler(CloseButton_Click);
            minButton.Click += new RoutedEventHandler(MinButton_Click);
            maxButton.Click += new RoutedEventHandler(MaxButton_Click);

            minButton.MouseEnter += minButton_MouseEnter;
            minButton.MouseLeave += minButton_MouseLeave;
            minButton.PreviewMouseLeftButtonDown += minButton_PreviewMouseLeftButtonDown;

            maxButton.MouseEnter += maxButton_MouseEnter;
            maxButton.MouseLeave += maxButton_MouseLeave;
            maxButton.PreviewMouseLeftButtonDown += maxButton_PreviewMouseLeftButtonDown;

            closeButton.MouseEnter += closeButton_MouseEnter;
            closeButton.MouseLeave += closeButton_MouseLeave;
            closeButton.PreviewMouseLeftButtonDown += closeButton_PreviewMouseLeftButtonDown;

            Window window = this.TemplatedParent as Window;
            if (window != null) window.StateChanged += window_StateChanged;
        }

        void minButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            minButton.ImageNormal = "/Content/images/defaultMinPres.png";
        }
        void minButton_MouseLeave(object sender, MouseEventArgs e)
        {
            minButton.ImageNormal = "/Content/images/defaultMin.png";
        }
        void minButton_MouseEnter(object sender, MouseEventArgs e)
        {
            minButton.ImageNormal = "/Content/images/defaultMinHover.png";
        }

        void maxButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Window window = this.TemplatedParent as Window;
            if (window.WindowState != WindowState.Maximized)
            {
                maxButton.ImageNormal = "/Content/images/defaultmaxPres.png";
            }
            else
            {
                maxButton.ImageNormal = "/Content/images/RestorePress.png";
            }

            //maxButton.ImageNormal = "/Content/images/defaultmaxPres.png";
        }
        void maxButton_MouseLeave(object sender, MouseEventArgs e)
        {
            Window window = this.TemplatedParent as Window;
            if (window.WindowState != WindowState.Maximized)
            {
                maxButton.ImageNormal = "/Content/images/defaultmax.png";
            }
            else
            {
                maxButton.ImageNormal = "/Content/images/Restore.png";
            }
            //maxButton.ImageNormal = "/Content/images/defaultmax.png";
        }
        void maxButton_MouseEnter(object sender, MouseEventArgs e)
        {
            Window window = this.TemplatedParent as Window;
            if (window.WindowState != WindowState.Maximized)
            {
                maxButton.ImageNormal = "/Content/images/defaultmaxHover.png";
            }
            else
            {
                maxButton.ImageNormal = "/Content/images/RestoreHover.png";
            }
            //maxButton.ImageNormal = "/Content/images/defaultmaxHover.png";
        }

        void closeButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            closeButton.ImageNormal = "/Content/images/defaultclosePres.png";
        }
        void closeButton_MouseLeave(object sender, MouseEventArgs e)
        {
            closeButton.ImageNormal = "/Content/images/defaultclose.png";
        }
        void closeButton_MouseEnter(object sender, MouseEventArgs e)
        {
            closeButton.ImageNormal = "/Content/images/defaultcloseHover.png";
        }


        static TitleBar()
        {
            //This OverrideMetadata call tells the system that this element wants to provide a style that is different than its base class.
            //This style is defined in themes\generic.xaml
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TitleBar), new FrameworkPropertyMetadata(typeof(TitleBar)));
        }

        #region event handlers

        void OnTitleBarLeftButtonDown(object sender, MouseEventArgs e)
        {
            Window window = this.TemplatedParent as Window;
            if (window != null)
            {
                Point p = e.GetPosition(window);
                Yposition = p.Y;
                ISMouseDown = true;
                window.DragMove();
            }
        }

        private void win1_MouseMove_1(object sender, MouseEventArgs e)
        {
            Window window = this.TemplatedParent as Window;
            Point p = e.GetPosition(window);
            if ((p.Y - Yposition > 10) && (0 != Yposition) && (ISMouseDown))
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    ISMouseDown = false;
                    MaxButton_Click(sender, e);
                }
            }

        }

        void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Window window = this.TemplatedParent as Window;
            if (window != null)
            {
                Application CurrApp = Application.Current;
                CurrApp.Shutdown();
            }
        }

        void MinButton_Click(object sender, RoutedEventArgs e)
        {
            Window window = this.TemplatedParent as Window;
            if (window != null)
            {
                window.WindowState = WindowState.Minimized;
                //WpfApp.Properties.Settings.Default.WindowState = WindowState.Minimized.ToString();

                //foreach (Window win in System.Windows.Application.Current.Windows)
                //{
                //    //win.WindowState = WindowState.Minimized;
                //    if (win != window) 
                //        win.Visibility = System.Windows.Visibility.Hidden;
                //}
            }
        }

        void MaxButton_Click(object sender, RoutedEventArgs e)
        {

            Window window = this.TemplatedParent as Window;
            if (window != null)
            {
                if (window.WindowState == WindowState.Maximized)
                {
                    if (maxButton != null) maxButton.ImageDown = "/Content/images/defaultmaxPres.png";
                    if (maxButton != null) maxButton.ImageNormal = "/Content/images/defaultmax.png";
                    if (maxButton != null) maxButton.ImageOver = "/Content/images/defaultmaxHover.png";

                    window.WindowState = WindowState.Normal;
                }
                else
                {
                    if (maxButton != null) maxButton.ImageDown = "/Content/images/RestorePress.png";
                    if (maxButton != null) maxButton.ImageNormal = "/Content/images/Restore.png";
                    if (maxButton != null) maxButton.ImageOver = "/Content/images/RestoreHover.png";

                    window.WindowState = WindowState.Maximized;
                }
            }
        }

        #endregion

        #region properties

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public ImageSource Icon
        {
            get { return (ImageSource)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }


        #endregion

        #region dependency properties

        public static readonly DependencyProperty TitleProperty =
           DependencyProperty.Register(
               "Title", typeof(string), typeof(TitleBar));

        public static readonly DependencyProperty IconProperty =
           DependencyProperty.Register(
               "Icon", typeof(ImageSource), typeof(TitleBar));

        #endregion
    }
}
