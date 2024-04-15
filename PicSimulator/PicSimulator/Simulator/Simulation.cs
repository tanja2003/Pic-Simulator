using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PicSimulator.Simulator
{
    public class Simulation
    {
        public Simulation()
        {

            
        }

        //public Storage storage = new Storage();
        public void selectCommand()
        {
            
            foreach(short command in Storage.programmMemory)
            {
                selectTypeOfCommand(command);
            }
        }

        // Instruction types:
        // byte oriented operations
        // bit oriented operations
        // literal and control operations
        private void selectTypeOfCommand(int cmd)
        {
            // 0x3000 = 0b0011_0000_0000_0000 (cmd)
            // to slecect the Type of command, we need bit 12 and 13
            int data = ((0x3000 & cmd) >> 12);  // shift to bit 1 and 2

            // now we can check which type the cmd is

            if (data == 0) // Byte-oriented operations
            {
                //selectCommand
            }
            else if (data == 1)  // Bit-orienented operations
            { 
            }
            else if (data == 2)
            {

            }
            else if(data == 3)
            {

            }
            else
            {
                // Fehler
            }
        }
    }
}
