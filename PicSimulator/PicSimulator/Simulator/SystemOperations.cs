using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSimulator.Simulator
{
    /// <summary>
    /// Calculates the runtime, Watchdog abd Interrupts
    /// </summary>
    public class SystemOperations
    {
        public SystemOperations()
        {
            
        }

        #region Runtime
        public static double runTimeCounter = 0.0;
        public static int quarzFrequency = 4; // in Mhz
        private static double programmStepTime = 0.0;  // in µs


        /// <summary>
        /// if the Instruction is 1 Cycle then the counter is increment by 1
        /// if the Instruction is 2 Cycle then the counter is increment by 2
        public static void UpdateRuntime(bool result)
        {
            if(result == false)
            {
                runTimeCounter += 1;
            }
            else
            {
                runTimeCounter += programmStepTime * 2;
            }
        }

        public static double GetRuntime()
        {
            return runTimeCounter;
        }
        // 4000 / 4 = 1000  1
        // 4000 / 8 = 500   0.5
        // 4000 / 16 = 250  2.5
        // 4000 / 32 = 125  1.25

        public static void SetQuarzFrequency(int frequency)
        {
            quarzFrequency = frequency;
            programmStepTime = 4 / frequency;  // in µs
        }
        #endregion
    }
}

