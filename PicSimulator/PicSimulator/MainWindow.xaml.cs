using Microsoft.Win32;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Threading;
using PicSimulator.Simulator;
using PicSimulator.ColumnsOrder;
using System.Collections.ObjectModel;

namespace PicSimulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Storage storage = new Storage();
            Flags flags = new Flags();
            flags.SetStatusZ(false);
            //Simulation simulation = new Simulation();

        }

        #region Menu
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();  // to select a file for the user
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); // Standardverzeichnis
            openFileDialog.Filter = "LST files (*.LST)|*.LST|Text files (*.txt)|*.txt|All Files (*.*)|*.*";  // only lst and txt files
            openFileDialog.Multiselect = false;   // to select only one file

            if(openFileDialog.ShowDialog() == true)
            {   // load file
                string fileName = openFileDialog.FileName;
                ProgrammFile programmFile = new ProgrammFile(fileName);
                ProgrammDataViewer.ItemsSource = programmFile.ColumnData;
                ProgrammDataViewer.Items.Refresh();
            }
        }

        #endregion

        public ObservableCollection<Columns> Columns { get;  set; } = new ObservableCollection<Columns>();

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            // check if File is loadad
            Simulation simulation = new Simulation();
            simulation.selectCommand();

        }
    }


}