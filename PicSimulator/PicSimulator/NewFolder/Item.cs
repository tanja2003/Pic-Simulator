using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSimulator.NewFolder
{
    public class Item: INotifyPropertyChanged
    {
        // to show the Programm from the file in the Listview. With this class we can show the columne with the binding Element
        public Item(string line)
        {
            _line = line;
        }


        public string _line;
        public string Line
        {
            get { return _line; }
            set 
            { 
                _line = value; 
                RaisePropertyChanged(nameof(Line));
                    }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));  
            }
        }
    }
}
