using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CapitaTest
{
    class Program
    {
        /// <summary>
        /// to overwrite the output file instead of appending to it.
        /// </summary>
        private const bool AppendMode = false;

        /// <summary>
        /// Buffer size for writer.
        /// </summary>
        private const int BufferSize = 65536;

        /// <summary>
        /// This files contains lines to be deleted
        /// </summary>
        private const string DeleteFile = "Delete.txt";

        /// <summary>
        /// we need to delete lines from this file
        /// </summary>
        private const string InputFile = "Input.txt";

        /// <summary>
        /// Output file
        /// </summary>
        private const string OutputFileName = "Output.txt";

        static void Main(string[] args)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            DeleteLines();

            watch.Stop();
            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");

        }

        /// <summary>
        /// Method to delete features specied in Delete.txt file from Input.txt file
        /// </summary>
        private static void DeleteLines()
        {
            //Create a hash set of features to be deleted to quickly determine if we need to delete a feature from features.txt
            HashSet<string> deleteList = new HashSet<string>(File.ReadAllLines(DeleteFile));

            //Readline is good for Large files. For smaller files we can use ReadAllLines.
            IEnumerable<string> inputFile = File.ReadLines(InputFile);

            using (TextWriter textWriter = new StreamWriter(OutputFileName, AppendMode, Encoding.UTF8, BufferSize))
            {
                foreach (string line in inputFile)
                {
                    //Write the feature if it is not in the delete list
                    if (!deleteList.Contains(line.Split(',')[0]))
                    {
                        textWriter.WriteLine(line);
                    }
                }
            }
        }
    }
}
