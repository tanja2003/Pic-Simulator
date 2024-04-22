using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Enumeration;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using PicSimulator.ColumnsOrder;

namespace PicSimulator.Simulator
{
    class ProgrammFile
    {
        public ProgrammFile(string filename)
        {
            
            loadProgrammText(filename);
        }

        /// <summary>
        /// streams every line in the file and store the opcode into the programmMemory
        /// </summary>
        private void loadProgrammText(string filename)
        {
            try
            {
                if (ColumnData != null)
                {
                    ColumnData.Clear();
                }
                ColumnData = new List<Item>();
                using (StreamReader strReader = new StreamReader(filename))
                {
                    string line;
                    while ((line = strReader.ReadLine()) != null)
                    {
                        int len = line.Length;
                        // first take the Substrings, then convert to int
                        // only the first two columns are important!
                        var address = line.Substring(0,4);
                        var opcode = line.Substring(5,4);
                        var rowNumber = line.Substring(20,5).Trim();
                        var label = line.Substring(26, 8).Trim();
                        var mnemonics = line.Substring(36).Trim();  // to the end of the line
                        if (address != "    ")
                        {
                            Int16 addressInt16 = Int16.Parse(address, System.Globalization.NumberStyles.HexNumber); // first two bits always zero
                            Int16 opcodeInt16 = Int16.Parse(opcode, System.Globalization.NumberStyles.HexNumber);
                            //Int16 rowNumberInt6 = Int16.Parse(rowNumber);
                            Storage.programmMemory[addressInt16] = opcodeInt16;  // store the data in the programmMemory
                        }
                        ColumnData.Add(new Item(address, opcode, rowNumber, label, mnemonics));
                        //ColumnData.Add(new Item(line));
                        Console.WriteLine(line);
                        Console.WriteLine(ColumnData);

                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public List<Item> ColumnData { get; set; }
    }
}
