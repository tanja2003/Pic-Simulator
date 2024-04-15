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

        public Instructions instructions = new Instructions();
        //public Storage storage = new Storage();


        // varibale literal = Befehl und ff   -> die unteren 8 bit vom gesamten Befehl
        public void selectCommand()
        {
            
            foreach(int command in Storage.programmMemory)
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
                selectCommand00(cmd);

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

        private void selectCommand00(int cmd)
        {
            int mask = 0x00ff;  // 0b0000 1111 0000 0000

            //int command = (int)((mask & cmd) >> 8);
            int literal = cmd & 0xff;   // 1111 1111 0000 0000

            int fileAdress = cmd & 0x7f;   // 0111 1111 0000 0000
            int programmAdress = cmd & 0x7ff; // komplete Addres beim goto und call   // 0111 1111 1111 0000   
            int bitAdress = (cmd & 0x0830) >> 7;// 7 mal schieben   // 0000 1000 0010 0000
            int destination = (cmd & 0080) >> 7;

            switch (cmd)
            {
                case 0:
                    if() 
                    instructions.MoveWf(cmd);
                    break;
                
                case 7:
                    instructions.Clrf(cmd);
                    break;


            }

        }
    }
}
