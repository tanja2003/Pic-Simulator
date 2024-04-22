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
        public static int[] dataMemory = new int[256];  // byte for Data between 0 and 255
        public static int[] stack = new int[8];
        public static int stackpointer = 0;


        public static int wRegister = 0;
        #region Initalize
        private void SetInitalizeStateDataMemory()
        {
            programmCounter = 0;
            for (int i = 0; i <= 255; i++)
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


        // Also der PRogramm Counter ist 13BIt breit
        // low Byte ist das PCL Register (PC<7:0>)
        // high byte ist das PCLATH Register (PC<12:8>)
        // Bei einem Befehl mit PCL als Destination: die unteren 5 Bits vom PCLATh werden in PC geladen
        // und das ERgbnis der ALu in das PCL
        // Bei Goto oder Call: unterne 11 Bits sind der Befehl, oberen 2 Bits sind PCLATH(<4:3>)

        public static int pclath = 0;

        // Fetch cycle behaviour of the Programm Counter
        // In fetch cycles, the program counter is simply incremented by one.
        // When it reaches its limit, it starts from 0 again.
        public static void IncrementProgrammCounter()
        {
            programmCounter++;

            if (programmCounter == 2048)
            {   // start at the begining again
                programmCounter = 0;
            }
            dataMemory[(int)MemoryStructur.PCL1] = (byte)(programmCounter & 0xFF);  // update PCL 

            // 
        }

        public static void SetProgrammCounter(int value)
        {
            if (value < 2047)
            {
                programmCounter = value;
                dataMemory[2] = programmCounter;
            }
            else
            {
                throw new Exception("ProgrammCounter to high!");
            }
        }
        public static void SetProgrammCounterPclath(int pcl)
        {   // max 2048 addresses (8K)
            // 0x18 Mask for bit 3 and 4 in Pclath
            // the total of 5 bits of the Pclath are moved by 8 to the left,
            // to move bits 3 and 4 to positions 12 and 13
            // at the end, PCL and Pclath are added together to represent the PC
            int pclath = (dataMemory[(int)MemoryStructur.PCLATH1] & 0x18) << 8;

            programmCounter = pclath + pcl;
            dataMemory[2] = programmCounter;
        }

        private int GetProgrammCounter()
        {
            return programmCounter;
        }

        private void SetPcLAth(int value)
        {


        }

        #endregion

        #region Stack


        /// <summary>
        /// with Every Call-Instruction the call-back adress is written on the stack. 
        /// Return: Stackpointer--
        /// More the 8 Calls: overwrite the call-back address
        public static void WriteStack(int value)
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

        public static int ReadStack()
        {
            return stack[stackpointer];
        }

        public static void DeleteTopOfStack()
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
            if (adress == 0x00)  // INDR --> Uses contents of FSR to address data memory (not a physical register)
            {
                adress = dataMemory[0x04];
                dataMemory[0x00] = dataMemory[adress];
                return dataMemory[adress];
            }

            // on which bank?
            int rp0 = (dataMemory[3] & 0x20) >> 5;  // 0010 0000
            if (rp0 == 0)
            {
                return dataMemory[adress];
            }
            else if (rp0 == 1)
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

        #region Timer, Watchdog and Prescaler

        public static void IncreaseTimer()
        {

        }

        public static void ResetWatchdogTimer()
        {

        }

        public static void IncrementWatchdogTimer()
        {

        }

        private static void CountUpTime()
        {

        }

        private static int GetPrescalerTimer0()
        {
            return 0;
        }

        private static int GetPrescalerWdt()
        {
            return 0;
        }


        #endregion

        #region  Input
        public static void SetInputRa(int input, bool value = true)
        {
            int previousValuePortA = dataMemory[5];  // 0000 000b

            if (value)
            {
                dataMemory[5] = (previousValuePortA | (1 << input));  // 0000 0b0b  <- both are inputs now
            }
            else
            {
                dataMemory[5] = (previousValuePortA & ~(1 << input)); // 1111b1b
            }
        }

        // 0000 0001
        // 0000 0100 --> 1111 1010

        public static void SetInputRb(int input, bool value = true)
        {
            int previousValuePortB = dataMemory[6];

            if (value)
            {
                dataMemory[6] = (previousValuePortB | (1 << input));
            }
            else
            {
                dataMemory[6] = (previousValuePortB & ~(1 << input));
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
