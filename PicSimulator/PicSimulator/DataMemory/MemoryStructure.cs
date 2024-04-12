using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSimulator.DataMemory
{
    enum MemoryStructur
    {
        indirectAdress1 = 0,
        TMR0 = 1,
        PCL1 = 2,
        Status1 = 3,
        FSR1 = 4,
        PORTA = 5,
        PORTB = 6,
        EEDATA = 8,
        EEADR = 9,
        PCLATH1 = 0x10,
        INTCON1 = 0x11,
        inirectAdress2 = 0x80,
        OPTION = 0x81,
        PCL2 = 0x82,
        STATUS2 = 0x83,
        FSR2 = 0x84,
        TRISA = 0x85,
        TRISB = 0x86,
        EECON1 =0x88,
        EECON2 = 0x89,
        PCLATH2 = 0x8A,
        INTCON2 = 0x8B
    }
}
