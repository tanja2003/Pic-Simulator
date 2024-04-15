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




        #region Byte-oriented operations 
        // 00 0000 1fff ffff
        public bool Movewf(int cmd)
        {
            return false;
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
            return false;
        }

        // 00 0101 dfff ffff
        public bool  Andwf(int cmd)
        {
            return false;
        }

        // 00 0110 dfff ffff
        public bool Xorwf(int cmd)
        {
            return false;
        }

        // 00 0111 dfff ffff
        public bool Addwf(int cmd)
        {
            return false;
        }

        // 00 1000 dfff ffff
        public bool Movf(int cmd)
        {
            return false;
        }

        // 00 1001 dfff ffff
        public bool Comf(int cmd)
        {
            return false;
        }

        // 00 1010 dfff ffff
        public bool Incf(int cmd)
        {
            return false;
        }

        // 00 1011 dfff ffff
        public bool Decfsz(int cmd)
        {
            return false;
        }

        // 00 1100 dfff ffff
        public bool Rrf(int cmd)
        {
            return false;
        }

        // 00 1101 dfff ffff
        public bool Rlf(int cmd)
        {
            return false;
        }

        // 00 1110 dfff ffff
        public bool Swapf(int cmd)
        {
            return false;
        }

        // 00 1111 dfff ffff
        public bool Incfsz(int cmd)
        {
            return false;
        }

        #endregion
    }
}
