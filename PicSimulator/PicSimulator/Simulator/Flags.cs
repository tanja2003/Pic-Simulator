using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace PicSimulator.Simulator
{
    public class Flags
    {
        public Flags()
        {
            Storage storage = new Storage();    
        }

        #region Statusregister Flags
        private static byte statusRegister = Storage.dataMemory[3];

        /// <summary>
        /// set or delete Zeroflag in the statusregister
        /// use of bitwise or and the mask of the zeroflag(2) to set
        /// use of bitwise and and complemtn of the mask of the zeroflag (1111 1011)
        public static void SetStatusZ(bool status = true) 
        {
            if (status)  // set Z Flag
            {
                statusRegister |= (1 << 2);  // 0000 0100   <- Position of Zeroflag in Statusregister
            }
            else   // delete Z Flag
            {
                statusRegister &= unchecked((byte)~(1 << 2));  // negated mask 1111 1011 <- only zeroflag changes
            }
        }

        public bool IsZeroFlagSet()
        {
            return (statusRegister & 1 << 2) != 0;
        }

        /// <summary>
        /// set or delte Carryflag
        /// use of bitwise or and the mask of the carryflag(0) to set
        /// use of bitwise and and complemtn of the mask of the Carryflag (1111 1110)
        public static void SetStatusCarry(bool status = true)
        {

            if (status)  // set Carryflag
            {
                statusRegister |= 1;   // 0000 0001  <- Position of Carryflag in Statusregister
            }
            else   // delte Carryflag
            {
                statusRegister &= unchecked((byte)~(1));  // negated mask 1111 1110 to change only carybit
            }
        }

        public static bool IsCarrySet()
        {
            return (statusRegister |= 1) != 0;
        }

        /// <summary>
        /// set or delte Digitcarryflag
        /// use of bitwise or and the mask of the Digitcarryflag(1) to set
        /// use of bitwise and and complemtn of the mask of the Digitcarryflag (1111 1101)
        public static void SetStatusDigitCarry(bool status = true)
        {
            if (status)   // set Digitcarryflag
            {
                statusRegister |= (1 << 1); // 0000 0010 <- Position of DigitCarryfalg in Statusregister
            }
            else   // delte Digitcarryflag
            { 
                statusRegister &= unchecked((byte)~(1 <<1)); // negated mask 1111 1101 <- only digitcarrybit changes
            }
        }
        #endregion
    }
}
