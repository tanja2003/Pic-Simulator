using Microsoft.VisualBasic;
using Microsoft.Win32;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.ConstrainedExecution;
using System.Security.Permissions;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using static System.Net.Mime.MediaTypeNames;

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
        public bool Movwf(int f, int data)
        {
            Storage.wRegister = 5;
            int result = data + Storage.wRegister;
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
        public bool Subwf(int f, int d, int data)
        {
            int result = data - Storage.wRegister;
            

            if (result > 255)
            {
                result %= 256;
            }

            if ( Storage.wRegister == 0 ) // Subtract with Value 0 sets C and DC
            {
                Flags.SetStatusCarry(true);
                Flags.SetStatusDigitCarry(true);
            }
            else
            {
                Flags.SetStatusZ(result == 0);
                Flags.SetStatusCarry(result >= 0);  // result is postive: c = 1
                Flags.SetStatusDigitCarry((data & MaskDC - Storage.wRegister & MaskDC) >= 0); // operation positive: dc = 1
            }

            DestinationOfData(result, f, d);

            Storage.IncrementProgrammCounter();
            return false;
        }

        /// <summary>
        /// 00 0011 dfff ffff
        /// Status Affected: Z
        /// Decrement contents of register ’f’. If ’d’ is 0 the result is stored in the W register. 
        /// If ’d’ is 1 the result is stored back in register ’f’.
        /// Cycles: 1
        public bool Decf(int f, int d, int data)
        {
            data--;
            if (data == 0)
            {
                Flags.SetStatusZ(true);
            }

            if(data == -1)  // in a Circle or happens nothing in this case???????
            {
                data = 255;
            }
            DestinationOfData(data, f, d);
            return false;
        }


        /// <summary>
        /// 00 0100 dfff ffff
        /// Status Affected: Z
        /// inclusive OR the W register with contents of register ’f’. If ’d’ is 0 the result is 
        /// placed in the W register.If ’d’ is 1 the result is placed back in register ’f’
        public bool Iorwf(int f, int d, int data)
        {
            int result = Storage.wRegister | data;
            if (result == 0)
            {
                Flags.SetStatusZ(true);
            }
            DestinationOfData(result, f, d);
            return false;
        }

        /// <summary>
        /// 00 0101 dfff ffff
        /// Status Affected: Z
        /// AND the W register with contents of register 'f'.If 'd' is 0 the result is stored in
        /// the W register. If 'd' is 1 the result is stored back in register 'f'.
        /// Cylces: 1
        public bool Andwf(int f, int d, int data)
        {
            int result = Storage.wRegister & data;
            if (result == 0)
            {
                Flags.SetStatusZ(true);
            }
            DestinationOfData(result, f, d);
            return false;
        }


        /// <summary>
        /// 00 0110 dfff ffff
        /// Status Affected: Z
        /// Exclusive OR the contents of the W register with contents of register 'f'. If 'd' is 0
        /// the result is stored in the W register. If 'd' is 1 the result is stored back in register 'f'.
        /// Cycles: 1
        public bool Xorwf(int f, int d, int data)
        {
            int result = Storage.wRegister ^ data;
            if (result == 0)
            {
                Flags.SetStatusZ(true);
            }
            DestinationOfData(result, f, d);
            return false;
        }


        /// <summary>
        /// 00 0111 dfff ffff
        /// Status affected: C, DC, Z
        /// Add the contents of the W register with the contents of register ’f’. If ’d’ is 0 the result is 
        /// stored in the W register.If ’d’ is 1 the result is stored back in register ’f’
        /// Cycles: 1
        public bool Addwf(int f, int d, int data) 
        {        
            int result = Storage.wRegister + data;

            if (result > 255)
            {
                result %= 256;
            }

            Flags.SetStatusZ(result == 0);
            Flags.SetStatusCarry(result > 255);
            Flags.SetStatusDigitCarry((Storage.wRegister & MaskDC) + (data & MaskDC) >= 16);

            if (Storage.wRegister == 0 || data == 0)  // special case if one operand = 0
            {
                Flags.SetStatusDigitCarry(false);
                Flags.SetStatusCarry(false);
            }
            DestinationOfData(result, f, d);

            return false;
        }

        /// <summary>
        /// 00 1000 dfff ffff
        /// Status Affected: Z
        /// The contents of register f is moved to a destination dependant upon the of d.
        /// If d = 0, destination is W register.If d = 1, the destination is file register f
        /// itself. d = 1 is useful to test a file register since status flag Z is affected.
        /// Cycles: 1
        public bool Movf(int f, int d, int data)
        {
            if (data == 0)
            {
                Flags.SetStatusZ(true);
            }
            DestinationOfData(data, f, d);

            return false;
        }
        /// <summary>
        /// 00 1001 dfff ffff
        /// Status Affected: Z
        /// contents of register ’f’ are complemented. If ’d’ is 0 the result is stored in 
        /// W.If ’d’ is 1 the result is stored back in register ’f’
        /// Cycles: 1
        public bool Comf(int f, int d, int data)
        {
            
            int result = data ^ 0xFF; // 1111 1111
            if (result == 0)
            {
                Flags.SetStatusZ(true);
            }
            DestinationOfData(result, f, d);
            return false;
        }

        /// <summary>
        /// 00 1010 dfff ffff
        /// Status Affected: Z
        /// the contents of register ’f’ are incremented. If ’d’ is 0 the result is placed 
        /// in the W register.If ’d’ is 1 the result is placed back in register ’f’
        /// Cycles: 1
        public bool Incf(int f, int d, int data)
        {            
            data++;

            if (data == 256)  // in a Circle or happens nothing in this case???????
            {
                Flags.SetStatusZ(true);
                data = 0;
            }
            DestinationOfData(data, f, d);

            return false;
        }

        /// <summary>
        /// 00 1011 dfff ffff
        /// Status Affected: None
        /// the contents of register ’f’ are decremented. If ’d’ is 0 the result is placed in the W register.
        /// If ’d’ is 1 the result is back in register ’f’. If the result is not 0, the next instruction, 
        /// is executed. If the result is 0, then a NOP is executed instead making it a 2TCY instruction.
        /// Cycles: 1 (2)
        public bool Decfsz(int f, int d, int data)
        {
            
            data--;
            DestinationOfData(data, f, d);
            if (data == 0)  // skip the next instruction!
            {
                Storage.IncrementProgrammCounter();
                return true;
            }
            // if data != 0 the next instruction is executed
            return false;
        }

        /// <summary>
        /// 00 1100 dfff ffff
        /// Status Affected: C
        /// The contents of register ’f’ are rotated one bit to the right through the Carry Flag. If ’d’
        /// is 0 the result is placed in the W register.If ’d’ is 1 the result is placed back in register ’f’
        /// Cycles: 1
        public bool Rrf(int f, int d, int data)
        {
             // 1 000 1001 --> 1 100 0100
            data = 9;
            int result = data >> 1;  // rotate alle other bits to the right
            if (Flags.IsCarrySet())
            {   // rotate carry to bit 7
                result |= 0x40; // 0100 0000  
            }
            if ((data & 0x0001) == 1)
            {   // rotate bit 1 to the carry bit
                Flags.SetStatusCarry(true);
            }
            DestinationOfData(result, f, d);

            return false;
        }

        /// <summary>
        /// 00 1101 dfff ffff
        /// Status Affected: C
        /// The contents of register ’f’ are rotated one bit to the left through the Carry Flag.If ’d’ is
        /// 0 the result is placed in the W register.If ’d’ is 1 the result is stored back in register ’f’
        /// Cycles:0
        public bool Rlf(int f, int d, int data)
        {
             // 1 100 1000 --> 1 001 0001
            int result = data >> 1;  // rotate alle other bits to the right
            if (Flags.IsCarrySet())
            {   // rotate carry to bit 1
                result |= 0x01; // 0000 0001  
            }
            if ((data & 0x40) == 64)
            {   // rotate bit 7 to the carry bit
                Flags.SetStatusCarry(true);
            }
            DestinationOfData(result, f, d);

            return false;
        }

        /// <summary>
        /// 00 1110 dfff ffff
        /// Staus Affected: None
        /// The upper and lower nibbles of contents of register 'f' are exchanged.If 'd' is 0 the result
        /// is placed in W register. If 'd' is 1 the result is placed in register 'f'.
        /// Cycles: 1
        public bool Swapf(int f, int d, int data)
        {
             //1100 0011
            int lowerNibbles = data & 0xf;    // 0000 0011
            int upperNibbles = data & 0xf0;  // 1100 0000
            int result = (lowerNibbles << 4) + (upperNibbles >> 4);  // 0011 1100
            DestinationOfData(result,f,d);

            return false;
        }


        /// <summary>
        /// 00 1111 dfff ffff
        /// Status Affected: None
        /// the contents of register ’f’ are incremented. If ’d’ is 0 the result is placed in the W
        /// register. If ’d’ is 1 the result is placed back in register ’f’. If the result is not 0, the next
        /// instruction is executed.If the result is 0, a NOP is executed instead making it a 2TCY instruction.
        /// Cycles: 1
        public bool Incfsz(int f, int d, int data)   ////////////////////////////CAse == 0 //// ???????????
        {
            
            data++;
            DestinationOfData(data, f, d);
            if (data == 0)  // skip the next instruction!
            {
                Storage.IncrementProgrammCounter();
                return true;
            }
            // if data != 0 the next instruction is executed
            return false;
        }

        #endregion

        #region Helper

        /// <summary>
        /// for bitwise Operation: iorwf, andwf and xorwf
        /// sets Zeroflag if result = 0
        /// and stores value in w-Register oder in f (d)
        private void DestinationOfData(int result, int f, int d)
        {
            if (result == 0)
            {
                Flags.SetStatusZ(true);
            }

            if (d == 0)
            {
                Storage.wRegister = result;
            }
            else if (d == 1)
            {
                Storage.SetRegisterData(result, f);
            }
            Storage.IncrementProgrammCounter();
        }
        #endregion
    }
}
