using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSimulator.NewFolder
{
    public class Storage
    {

        public Storage()
        {
            programmMemory = new Int32[1024];
            stack = new int[8];

        }

        public Int32[] programmMemory;
        public int programmCounter = 0;
        public int[] stack;
    }
}
