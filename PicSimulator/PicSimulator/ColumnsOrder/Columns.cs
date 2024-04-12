using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSimulator.ColumnsOrder
{
    public class Columns
    {
        public Columns(int address, int opcode, int rownumber, string label, string mnemonics)
        {
            Address = address;
            Opcode = opcode;
            Rownumber = rownumber;
            Label = label;
            Mnemonics = mnemonics;
        }
        public Columns(int rownumber, string label, string mnemonics) 
        { 
            Rownumber = rownumber;
            Label = label;  
            Mnemonics= mnemonics;
        }

        public int Address { get; set; }
        public int Opcode { get; set; }
        public int Rownumber { get; set; }
        public string Label { get; set; }
        public string Mnemonics { get; set;
        }
    }
}
