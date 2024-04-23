
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
using System.Security.Cryptography.X509Certificates;
using System.ComponentModel;

namespace PicSimulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();

        }

        private static bool _run = false;

        #region Menu

        /// <summary>
        /// Select Programm and store the data into the programmMemory
        /// </summary>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();  // to select a file for the user
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); // Standardverzeichnis
            openFileDialog.Filter = "LST files (*.LST)|*.LST|Text files (*.txt)|*.txt|All Files (*.*)|*.*";  // only lst and txt files
            openFileDialog.Multiselect = false;   // to select only one file

            if (openFileDialog.ShowDialog() == true)
            {   // load file
                string fileName = openFileDialog.FileName;
                ProgrammFile programmFile = new ProgrammFile(fileName);
                ProgrammDataViewer.ItemsSource = programmFile.ColumnData;
                ProgrammDataViewer.Items.Refresh();
                Simulation.dataLoaded = true;
            }
        }

        #endregion

        public ObservableCollection<Columns> Columns { get; set; } = new ObservableCollection<Columns>();


        public void selectCommand()
        {
            Simulation simulation = new Simulation();


            simulation.selectTypeOfCommand(Storage.programmMemory[Storage.programmCounter]);
            //Item._rowNumber = 
            //Item.rowNumber();
            //ProgrammDataViewer.SelectedIndex =  Storage.programmCounter;  // zeigt aktuelle zeile, aber auch noch falsch
            //ProgrammDataViewer.ScrollIntoView(ProgrammDataViewer.SelectedItem);
            
        }
        private delegate void UpdateUi();
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            // check if File is loadad and programm is not in runtime
            if(Simulation.dataLoaded && _run == false)
            {
                _run = true;
                new Thread(() =>
                {
                    while(_run)
                    {
                        selectCommand();
                        Dispatcher.BeginInvoke(new UpdateUi(RefreshData));
                        // dispatcher
                        // Programmsteps
                        
                        _run = false;
                    }
                }).Start();
            }


            

        }

        #region Refresh

        public void RefreshData()
        {
            RunTime = SystemOperations.runTimeCounter;
            RunTime = 5;
            //ShowRuntime
        }
        #endregion

        private void StopButtonClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        #region Input/Output
        private void CheckBoxRa0_Click(object sender, RoutedEventArgs e)
        {
            if(CheckBoxRa0 != null)
            {
                var checkBox = CheckBoxRa0.IsChecked;
                bool value;
                if(checkBox == null)
                {
                    value = false;
                }
                else
                {
                    value = (bool)checkBox;
                }
                Storage.SetInputRa(0, value);
            }
        }

        private void CheckBoxRa1_Click(object sender, RoutedEventArgs e)
        {
            if (CheckBoxRa1 != null)
            {
                var checkBox = CheckBoxRa1.IsChecked;
                bool value;
                if (checkBox == null)
                {
                    value = false;
                }
                else
                {
                    value = (bool)checkBox;
                }
                Storage.SetInputRa(1, value);
            }
        }

        private void CheckBoxRa2_Click(object sender, RoutedEventArgs e)
        {
            if (CheckBoxRa2 != null)
            {
                var checkBox = CheckBoxRa2.IsChecked;
                bool value;
                if (checkBox == null)
                {
                    value = false;
                }
                else
                {
                    value = (bool)checkBox;
                }
                Storage.SetInputRa(2, value);
            }
        }

        private void CheckBoxRa3_Click(object sender, RoutedEventArgs e)
        {
            if (CheckBoxRa3 != null)
            {
                var checkBox = CheckBoxRa0.IsChecked;
                bool value;
                if (checkBox == null)
                {
                    value = false;
                }
                else
                {
                    value = (bool)checkBox;
                }
                Storage.SetInputRa(3, value);
            }
        }

        private void CheckBoxRa4_Click(object sender, RoutedEventArgs e)
        {
            if (CheckBoxRa4 != null)
            {
                var checkBox = CheckBoxRa4.IsChecked;
                bool value;
                if (checkBox == null)
                {
                    value = false;
                }
                else
                {
                    value = (bool)checkBox;
                }
                Storage.SetInputRa(4, value);
            }
        }

        private void CheckBoxRb0_Click(object sender, RoutedEventArgs e)
        {
            if (CheckBoxRb0 != null)
            {
                var checkBox = CheckBoxRb0.IsChecked;
                bool value;
                if (checkBox == null)
                {
                    value = false;
                }
                else
                {
                    value = (bool)checkBox;
                }
                Storage.SetInputRb(0, value);
            }
        }

        private void CheckBoxRb1_Click(object sender, RoutedEventArgs e)
        {
            if (CheckBoxRb1 != null)
            {
                var checkBox = CheckBoxRb1.IsChecked;
                bool value;
                if (checkBox == null)
                {
                    value = false;
                }
                else
                {
                    value = (bool)checkBox;
                }
                Storage.SetInputRb(1, value);
            }
        }

        private void CheckBoxRb2_Click(object sender, RoutedEventArgs e)
        {
            if (CheckBoxRb2 != null)
            {
                var checkBox = CheckBoxRb2.IsChecked;
                bool value;
                if (checkBox == null)
                {
                    value = false;
                }
                else
                {
                    value = (bool)checkBox;
                }
                Storage.SetInputRb(2, value);
            }
        }

        private void CheckBoxRb3_Click(object sender, RoutedEventArgs e)
        {
            if (CheckBoxRb3 != null)
            {
                var checkBox = CheckBoxRb3.IsChecked;
                bool value;
                if (checkBox == null)
                {
                    value = false;
                }
                else
                {
                    value = (bool)checkBox;
                }
                Storage.SetInputRb(3, value);
            }
        }

        private void CheckBoxRb4_Click(object sender, RoutedEventArgs e)
        {
            if (CheckBoxRb4 != null)
            {
                var checkBox = CheckBoxRb4.IsChecked;
                bool value;
                if (checkBox == null)
                {
                    value = false;
                }
                else
                {
                    value = (bool)checkBox;
                }
                Storage.SetInputRb(4, value);
            }
        }

        private void CheckBoxRb5_Click(object sender, RoutedEventArgs e)
        {
            if (CheckBoxRb5 != null)
            {
                var checkBox = CheckBoxRb5.IsChecked;
                bool value;
                if (checkBox == null)
                {
                    value = false;
                }
                else
                {
                    value = (bool)checkBox;
                }
                Storage.SetInputRb(5, value);
            }
        }

        private void CheckBoxRb6_Click(object sender, RoutedEventArgs e)
        {
            if (CheckBoxRb6 != null)
            {
                var checkBox = CheckBoxRb6.IsChecked;
                bool value;
                if (checkBox == null)
                {
                    value = false;  // if not checked
                }
                else
                {
                    value = (bool)checkBox;
                }
                Storage.SetInputRb(6, value);
            }
        }

        private void CheckBoxRb7_Click(object sender, RoutedEventArgs e)
        {
            if (CheckBoxRb7 != null)
            {
                var checkBox = CheckBoxRb7.IsChecked;
                bool value;
                if (checkBox == null)
                {
                    value = false;
                }
                else
                {
                    value = (bool)checkBox;
                }
                Storage.SetInputRb(7, value);
            }
        }
        #endregion

        #region Settings
        public double RunTime
        {
            get { return SystemOperations.runTimeCounter; }
            set
            {
                if (value != SystemOperations.runTimeCounter)
                {
                    SystemOperations.runTimeCounter = value;
                    OnPropertyChanged(nameof(RunTime));
                }
            }
        }
        #endregion


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
