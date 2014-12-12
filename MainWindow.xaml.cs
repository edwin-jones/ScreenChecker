using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

//This program let's us check the quality of a display by covering it in a rectangle of a pure color.
//We can cycle through the colors with a mouse click and exit by pressing escape. The window is resizable so we can move it to secondary monitors.
namespace ScreenChecker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Static brush collection and enumerator.
        static List<Brush> LocalBrushes = new List<Brush>() { Brushes.White, Brushes.Black, Brushes.Red, Brushes.Green, Brushes.Blue };
        static List<Brush>.Enumerator ColorEnumerator = LocalBrushes.GetEnumerator();

        /// <summary>
        /// Ctor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            //Get the first brush the enumerator exposes.
            GetNextBrush();

            //Make the window the size of the primary screen.
            this.Width = System.Windows.SystemParameters.PrimaryScreenWidth;
            this.Height = System.Windows.SystemParameters.PrimaryScreenHeight;

            //Center the window on the screen.
            CenterWindowOnScreen();
        }

        /// <summary>
        /// This method will center the main window on the screen.
        /// </summary>
        private void CenterWindowOnScreen()
        {
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
        }

        /// <summary>
        /// This method will change the color of the rect/screen to the next brush in the collection.
        /// </summary>
        private void GetNextBrush()
        {
            //Attempt to move to the next item in the iterator. If we can't, dispose/reset the iterator.
            if (ColorEnumerator.MoveNext() == false)
            {
                ColorEnumerator.Dispose();
                ColorEnumerator = LocalBrushes.GetEnumerator();
                ColorEnumerator.MoveNext();
            }

            //Update the rectangle color.
            MainRectangle.Fill = ColorEnumerator.Current;
        }

        /// <summary>
        /// This method will trigger when we press a key when the main window has focus. We capture escape key presses so the user can exit the program.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                ColorEnumerator.Dispose();
                this.Close();
            }

            e.Handled = true;
        }

        /// <summary>
        /// This method will trigger on mouse clicks so we know when to cycle colors.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            GetNextBrush();
        }
    }
}
