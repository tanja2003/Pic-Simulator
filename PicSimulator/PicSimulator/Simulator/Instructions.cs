using Microsoft.VisualBasic;
using Microsoft.Win32;
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

        private static int MaskDC = 15;


        #region Byte-oriented operations 

        /// <summary>
        /// 00 0000 1fff ffff
        /// Status affected: None
        /// Move data from W register to register 'f'.
        /// Cycles: 1
        public bool Movwf(int f)
        {
            Storage.wRegister = 5;
            int fRegister = Storage.GetRegisterData(f);
            int result = fRegister + Storage.wRegister;
            Storage.SetRegisterData(result, f);
            Storage.IncrementProgrammCounter();
            return false;
        }

        /// <summary>
        /// 00 0001 0fff ffff
        /// Status affected: Z
        /// W register is cleared. Zero bit (Z) is set.
        /// Cycles: 1
        public bool Clrw ()
        {
            Storage.wRegister = 0;
            Flags.SetStatusZ(true);
            Storage.IncrementProgrammCounter();
            return false;
        }

        /// <summary>
        /// 00 0001 1fff ffff
        /// Status affected: Z
        /// The contents of register ’f’ are cleared 
        /// and the Z bit is set.
        /// Cycles: 1
        public bool Clrf(int f)
        {
           Storage.SetRegisterData(0, f);
            Flags.SetStatusZ(true);
            Storage.IncrementProgrammCounter();
            return false;
        }

        /// <summary>
        /// 00 0010 dfff ffff
        /// Status affected: C, DC, Z
        /// Subtract (2’s complement method) of W register from register 'f'. If 'd' is 0 the
        /// result is stored in the W register.If 'd' is 1 the result is stored back in register 'f'.
        /// Cycles: 1
        public bool Subwf(int f, int d)
        {
            int fRegister = Storage.GetRegisterData(f);  // Inhalt von f ist im Datenspeicher
            int result = fRegister - Storage.wRegister;
            Flags.SetStatusZ(result % 256 == 0);
            Flags.SetStatusCarry( result >= 0);  // result is postive: c = 1
            Flags.SetStatusDigitCarry((Storage.wRegister & MaskDC + fRegister & MaskDC) >= 16);
            if (result > 255)
            {
                result %= 256;
            }

            if (d == 0)
            {
                Storage.wRegister = result;
            }
            else if (d == 1)
            {
                Storage.SetRegisterData(result, f);
            }
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

        /// <summary>
        /// 00 0111 dfff ffff
        /// Status affected: C, DC, Z
        /// Add the contents of the W register with the contents of register ’f’. If ’d’ is 0 the result is 
        /// stored in the W register.If ’d’ is 1 the result is stored back in register ’f’
        /// Cycles: 1
        public bool Addwf(int f, int d)
        {
            int fRegister = Storage.GetRegisterData(f);  // Inhalt von f ist im Datenspeicher
            int result = Storage.wRegister + fRegister;   
            Flags.SetStatusZ(result % 256 == 0);
            Flags.SetStatusCarry(result > 255 || result < 0);
            Flags.SetStatusDigitCarry((Storage.wRegister & MaskDC + fRegister & MaskDC) >= 16);
            if (result > 255) 
            {
                result %= 256;
            }
            
            if(d == 0)
            {
                Storage.wRegister = result;
                // z, c, dc
            }
            else if(d == 1)
            {
                Storage.SetRegisterData(result, f);
            }
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
