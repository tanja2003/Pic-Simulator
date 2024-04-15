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

        #region Register Flags
        private byte statusRegister = Storage.dataMemory[3];
        public void SetStatusZ(bool status = true) 
        {
            if (status)  // set Z Flag
            {
                statusRegister |= 1 << 2;  // 0000 0100   <- Position of Zeroflag in Statusregister
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


        public void SetStatusCarry(bool status = true)
        {

            if (status)
            {
                statusRegister |= 1 << 2;
            }
            else
            {

            }
        }
        #endregion
    }
}
