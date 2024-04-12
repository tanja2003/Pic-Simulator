using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace PicSimulator.NewFolder
{
    public class Flags
    {
        public Flags()
        {
            Storage storage = new Storage();    
        }

        #region Register Flags

        public static byte ZeroFlagMask = 0x00;
        private byte statusRegister = Storage.dataMemory[3];
        public void SetStatusZ(bool status) 
        {
            short previousStatus = statusRegister;
            const byte ZeroFlagMask = 0x02;
            if (status)  // set Z Flag
            {
                statusRegister = (byte)(previousStatus | 1 << 2);
                statusRegister |= ZeroFlagMask;
            }
            else   // delete Z Flag
            {
                statusRegister = (byte)(statusRegister & ~( 1 << 2));
                statusRegister &= unchecked((byte)~ZeroFlagMask);

            }
        }

        public bool IsZeroFlagSet()
        {
            return (statusRegister & ZeroFlagMask) != 0;
        }
        #endregion
    }
}
