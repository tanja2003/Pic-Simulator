using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace PicSimulator.Simulator
{
    public class Item : INotifyPropertyChanged
    {
        // to show the Programm from the file in the Listview. With this class we can show the columne with the binding Element
        private string _address;
        private string _opcode;
        private string _rowNumber;
        private string _label;
        private string _mnemonics;
        private bool _isExecuted;

        public Item(string address, string opcode, string rowNumber, string label, string mnemonics)
        {

        }


        public Item(string line)
        {
            _line = line;

        }

        public static string _line;


        public string Line
        {
            get { return _line; }
            set
            {
                _line = value;
                ;
                RaisePropertyChanged(nameof(Line));
            }
        }

        public string Address
        {
            get { return _address; }
            set { _address = value; RaisePropertyChanged(nameof(Address)); }
        }

        public string Opcode
        {
            get { return _opcode; }
            set { _opcode = value; RaisePropertyChanged(nameof(Opcode)); }
        }

        public string RowNumber
        {
            get { return _rowNumber; }
            set { _rowNumber = value; RaisePropertyChanged(nameof(RowNumber)); }
        }

        public string Label
        {
            get { return _label; }
            set { _label = value; RaisePropertyChanged(nameof(Label)); }
        }

        public string Mnemonics
        {
            get { return _mnemonics; }
            set { _mnemonics = value; RaisePropertyChanged(nameof(Mnemonics)); }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
