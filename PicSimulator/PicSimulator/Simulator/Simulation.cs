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


        // Instruction types:
        // byte oriented operations
        // bit oriented operations
        // literal and control operations
        public void selectTypeOfCommand(int cmd)
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
            {   // 0b00_xxxx_xxxx_xxxx
                selectCommand00(cmd);

            }
            else if (data == 1)  // Bit-orienented operations
            {   // 0b01_xxxx_xxxx_xxxx
                selectCommand01(cmd);
            }
            else if (data == 2)   // control operations (Goto, call)
            {   // 0b10_xxxx_xxxx_xxxx
                selectCommand02(cmd);
            }
            else if(data == 3)  // literal operations
            {   // 0b11_xxxx_xxxx_xxxx
                selectCommand03(cmd);
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
            int fData = Storage.GetRegisterData(fAdress);

            switch ( (cmd & 0x0f00) >> 8)
            {
                case 0:
                    if(destination == 1)
                    {
                        return instructions.Movwf(fAdress, fData);

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
                    return instructions.Subwf(fAdress, destination, fData);
                    
                case 3:
                    return instructions.Decf(fAdress, destination, fData);
                    
                case 4:
                    return instructions.Iorwf(fAdress, destination, fData);
                    
                case 5:
                    return instructions.Andwf(fAdress, destination, fData);
                    
                case 6:
                    return instructions.Xorwf(fAdress, destination, fData);
                    
                case 7:
                    return instructions.Addwf(fAdress, destination, fData);
                    
                case 8:
                    return instructions.Movf(fAdress, destination, fData);
                    
                case 9:
                    return instructions.Comf(fAdress, destination, fData);
                    
                case 10:
                    return instructions.Incf(fAdress, destination, fData);
                    
                case 11:
                    return instructions.Decfsz(fAdress, destination, fData);
                    
                case 12:
                    return instructions.Rrf(fAdress, destination, fData);
                    
                case 13:
                    return instructions.Rlf(fAdress, destination, fData);
                    
                case 14:
                    return instructions.Swapf(fAdress, destination, fData);
                    
                case 15:
                    return instructions.Incfsz(fAdress, destination, fData);
                    
                default:
                    throw new Exception("instruction Error!");
                    


            }

        }

        private bool selectCommand01(int cmd)
        {
            int fAdress = cmd & 0x7f;   // 0111 1111
            int bitAdress = (cmd & 0x0380) >> 7;// 7 mal schieben   // 0000 0011 1000 0000
            int mask = cmd & 0x3ff;   // 0000 0011 1111 1111
            int fData = Storage.GetRegisterData(fAdress);

            switch ((cmd & 0x0C00) >> 10)   // 0000 1100 0000 0000
            {
                case 0:
                    return instructions.Bcf(fAdress, bitAdress, fData);
                case 1:
                    return instructions.Bsf(fAdress, bitAdress, fData);
                case 2:
                    return instructions.Btfsc(fAdress, bitAdress, fData);
                case 3:
                    return instructions.Btfss(fAdress, bitAdress, fData);
                default:
                    throw new Exception("Instruction Error!");
            }
        }

        private bool selectCommand02(int cmd)
        {
            int programmAdress = cmd & 0x7ff; // goto und call   // 0000 0111 1111 1111
            int literal = cmd & 0xff;

            switch ((cmd & 0x0800) >> 11)
            {
                case 0:
                    return instructions.Goto(programmAdress);
                case 1:
                    return instructions.Call(programmAdress, literal); 
                default:
                    throw new Exception("Instruction Error!");
            }
        }

        private bool selectCommand03(int cmd)
        {   // 0b11_xxxx_xxxx_xxxx
            int literal = cmd & 0xff;
            int dest = cmd & 0x02;
            int dest2 = cmd & 0x01;
            switch((cmd & 0x0C00 ) >> 10)
            {
                case 0:
                    return instructions.Movlw(literal);
                case 1:
                    return instructions.Retlw(literal);
                case 2:
                    if (dest2 == 0){
                        return instructions.Iorlw(literal);
                    }
                    return instructions.Andlw(literal);
                case 3:
                    if(dest == 0)
                    {
                        return instructions.Sublw(literal);
                    }
                    else
                    {
                        return instructions.Addlw(literal);
                    }
                default:
                    throw new Exception("Instruction Error!");
            }
        }
    }
}
