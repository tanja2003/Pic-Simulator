using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
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




        // 
        // 00 0000 1fff ffff
        public void Movewf(int cmd)
        {

        }

        // 00 0001 0fff ffff
        public bool Clrw (int cmd)
        {
            return false;
        }

         // 00 0001 1fff ffff
        public bool Clrf(int cmd)
        {
            Storage.wRegister = 0;
            // SetStatusZ()
            // Programmzahler erhöhen

            return false;
        }

        // 00 0010 dfff ffff
        public bool Subwf(int cmd)
        {
            return false;
        }

        // 00 0011 dfff ffff
        public bool Decf(int cmd)
        {
            return false;
        }

        // 00 0100 dfff ffff
        public bool Iorwf(int cmd) 
        { 
        }


        // 00 0101 dfff ffff
        public bool Comf (int cmd)
        {
            return false;
        }

        // 00 0101 dfff ffff
        public bool 
    }
}
