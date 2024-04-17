﻿using System;
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
            selectTypeOfCommand(0x008c);
            selectTypeOfCommand(0x018c);
            selectTypeOfCommand(0x017c);
            foreach (int command in Storage.programmMemory)
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
            // to select the Type of command, we need bit 12 and 13
            int data = ((0x3000 & cmd) >> 12);  // shift to bit 1 and 2

            int literal = cmd & 0xff;   // 1111 1111 0000 0000
            int fileAdress = cmd & 0x7f;   // 0111 1111 0000 0000
            int programmAdress = cmd & 0x7ff; // komplete Addres beim goto und call   // 0111 1111 1111 0000   
            int bitAdress = (cmd & 0x0830) >> 7;// 7 mal schieben   // 0000 1000 0010 0000

            // now we can check which type the cmd is

            if (data == 0) // Byte-oriented operations
            {
                selectCommand00(cmd);

            }
            else if (data == 1)  // Bit-orienented operations
            { 
                selectCommand01(cmd);
            }
            else if (data == 2)   // control operations (Goto, call)
            {

            }
            else if(data == 3)  // literal operations
            {

            }
            else
            {
                // Fehler
            }

            // Runtime bool zum unterschieden ob 2 oder 1 takt lang
        }

        private bool selectCommand00(int cmd)
        {
            //int mask = 0x00ff;  // 0b0000 1111 0000 0000
            
            //int command = (int)((mask & cmd) >> 8);
            int fAdress = cmd & 0x7f;   // 0000 0000 0111 1111   // fAdress -> register f
            int literal = (cmd & 0x0f00);
            int destination = (cmd & 0x0080) >> 7;  // 0000 0000 1000 0000

            switch ( (cmd & 0x0f00) >> 8)
            {
                case 0:
                    if(destination == 1)
                    {
                        return instructions.Movwf(fAdress);

                    }
                    else if(destination == 0)
                    {
                        // NOP Befehl
                        // sleep
                        // ...
                    }
                    return false;
                    
                    
                case 1:
                    if(destination == 0)
                    {
                        return instructions.Clrw();
                    }
                    return instructions.Clrf(fAdress);
                    
                case 2:
                    return instructions.Subwf(fAdress);
                    
                case 3:
                    return instructions.Decf(fAdress);
                    
                case 4:
                    return instructions.Iorwf(fAdress);
                    
                case 5:
                    return instructions.Andwf(fAdress);
                    
                case 6:
                    return instructions.Xorwf(fAdress);
                    
                case 7:
                    return instructions.Addwf(fAdress, destination);
                    
                case 8:
                    return instructions.Movf(fAdress);
                    
                case 9:
                    return instructions.Comf(fAdress);
                    
                case 10:
                    return instructions.Incf(fAdress);
                    
                case 11:
                    return instructions.Decfsz(fAdress);
                    
                case 12:
                    return instructions.Rrf(fAdress);
                    
                case 13:
                    return instructions.Rlf(fAdress);
                    
                case 14:
                    return instructions.Swapf(fAdress);
                    
                case 15:
                    return instructions.Incfsz(fAdress);
                    
                default:
                    throw new Exception("instruction Error!");
                    


            }

        }

        private void selectCommand01(int cmd)
        {
            int fileAdress = cmd & 0x7f;   // 0111 1111 0000 0000
            int bitAdress = (cmd & 0x0830) >> 7;// 7 mal schieben   // 0000 1000 0010 0000
            int mask = cmd & 0x0cff;

        }
    }
}
