using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSimulator.Simulator
{
    public class Instructions
    {
        public Instructions() 
        {
            Storage storage = new Storage();    

        }
        public bool clrf(short cmd)
        {
            Storage.wRegister = 0;
            // SetStatusZ()
            // Programmzahler erhöhen

            return false;
        }
    }
}
