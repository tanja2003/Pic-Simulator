using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PicSimulator.DataMemory;

namespace PicSimulator.NewFolder
{
    public class Storage
    {

        public Storage()
        {
            SetInitalizeStateDataMemory();
        }
        /* 0*/

        public Int16[] programmMemory = new Int16[1024];
        public int programmCounter = 0;
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

        private void IncrementProgrammCounter()
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
        // Initalisieren
        // Programmzahler
        // Stack
        // Timer, Watchdog und Vorlaufer

        // Eingange und Ausgange festlegen
    }
}
