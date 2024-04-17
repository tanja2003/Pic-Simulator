using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using PicSimulator.DataMemory;

namespace PicSimulator.Simulator
{
    public class Storage
    {

        public Storage()
        {
            SetInitalizeStateDataMemory();
        }
        /* 0*/

        public static int[] programmMemory = new int[1024];
        public static int programmCounter = 0;
        public static byte[] dataMemory = new byte[256];  // bytw for Data between 0 and 255
        public Int16[] stack = new Int16[8];
        public int stackpointer = 0;


        public static int wRegister = 0;
        #region Initalize
        private void SetInitalizeStateDataMemory()
        {
            programmCounter = 0;
            for(int i = 0; i <= 255; i++)
            {
                dataMemory[i] = 0;
            }
            dataMemory[0x03] = 0x18;  // 24 -> 0001 1000
            //dataMemory[0x06] = 0x80;  // 128 
            //dataMemory[0x07] = 0x80;
            //dataMemory[0x08] = 0x80;
            //dataMemory[0x09] = 0x80;
            dataMemory[0x81] = 0xff;
            dataMemory[0x83] = 0x18;
            dataMemory[0x85] = 0xff;
            dataMemory[0x86] = 0xff;
        }
        #endregion

        #region ProgrammCounter

        public static void IncrementProgrammCounter()
        {
            programmCounter++;
            dataMemory[(int)MemoryStructur.PCL1] = (byte)programmCounter;
        }

        private void SetProgrammCounter(Int16 value)
        {   // max 2048 addresses (8K)
            if (value <= 2047)
            {
                programmCounter = value;
            }
            else
            {   // PC starts to count with 0 again
                programmCounter = value - 2048;
            }
        }

        private int GetProgrammCounter()
        {
            return programmCounter;
        }

        #endregion

        #region Stack

        private void WriteStack(Int16 value)
        {
            stack[stackpointer] = value;
            if (stackpointer == 7)
            {
                stackpointer = 0;
            }
            else
            {
                stackpointer++;
            }
        }

        private Int16 ReadStack()
        {
            return stack[stackpointer];
        }

        private void DeleteTopOfStack()
        {
            stack[stackpointer] = 0;
            stackpointer--;
        }
        #endregion

        #region DataMemory

        /// <summary>
        /// returns content of the adress in the dataMemory
        public static int GetRegisterData(int adress)
        {
            if(adress == 0x00)  // INDR --> Uses contents of FSR to address data memory (not a physical register)
            {
                adress = dataMemory[0x04];
                dataMemory[0x00] = dataMemory[adress];
                return dataMemory[adress];
            }

            // on which bank?
            int rp0 = (dataMemory[3] & 0x20) >> 5;  // 0010 0000
            if(rp0 == 0)
            {
                return dataMemory[adress];
            }
            else if(rp0 == 1)
            {
                return dataMemory[adress + 0x80];  // other 128 bytes
            }
            throw new Exception("Wrong Rp0 Value!");
        }


        /// <summary>
        /// When the Data in the register (adress) was changed, 
        /// then this must be stored in the Datamemory
        public static void SetRegisterData(int valueOfAddress, int adress)
        {
            if (adress == 0x00)
            {
                adress = dataMemory[0x04];
            }
            int rp0 = (dataMemory[3] & 0x20) >> 5;  // rp0 is bit 5 --> mask: 0010 0000
            if (rp0 == 0 && adress <= 0x4F)  // the register > 0x04 are unimplemented data memory
            {
                dataMemory[adress] = (byte)valueOfAddress;
                if (adress == 2 || adress == 3 || adress == 4 || adress == 10 || adress == 11)
                {   // these registers are at bank0 and bank1!
                    dataMemory[adress + 0x80] = (byte)valueOfAddress;
                }
            }
            else if (rp0 == 1 && adress <= 0x4F)
            {
                dataMemory[adress + 0x80] = (byte)valueOfAddress;
                if (adress == 2 || adress == 3 || adress == 4 || adress == 10 || adress == 11)
                {
                    dataMemory[adress] |= (byte)valueOfAddress;
                }
            }
        }
        #endregion

        // Initalisieren
        // Programmzahler
        // Stack
        // Timer, Watchdog und Vorlaufer

        // Eingange und Ausgange festlegen
    }
}
