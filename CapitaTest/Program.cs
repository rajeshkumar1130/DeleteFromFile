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
        /// 
        /// </summary>
        private const string FeaturesToDeleteFileName = "FeaturesToDelete.txt";

        /// <summary>
        /// 
        /// </summary>
        private const string FeaturesFileName = "Features.txt";

        /// <summary>
        /// 
        /// </summary>
        private const string OutputFileName = "output5.txt";

        static void Main(string[] args)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            DeleteFeatures();

            watch.Stop();
            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
        }

        /// <summary>
        /// Method to delete features specied in FeaturesToDelete.txt file from Features.txt file
        /// </summary>
        private static void DeleteFeatures()
        {
            //Create a hash set of features to be deleted to quickly determine if we need to delete a feature from features.txt
            HashSet<string> deleteList = new HashSet<string>(File.ReadAllLines(FeaturesToDeleteFileName));

            //Readline is good for Large files. For smaller files we can use ReadAllLines.
            IEnumerable<string> features = File.ReadLines(FeaturesFileName);

            using (TextWriter textWriter = new StreamWriter(OutputFileName, AppendMode, Encoding.UTF8, BufferSize))
            {
                foreach (string feature in features)
                {
                    //Write the feature if it is not in the delete list
                    if (!deleteList.Contains(feature.Split(',')[0]))
                    {
                        textWriter.WriteLine(feature);
                    }
                }
            }
        }
    }
}
