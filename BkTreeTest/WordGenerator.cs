using System.Collections.Generic;
using System.IO;
using System.Net;

namespace BkTreeTest
{
    public static class WordGenerator
    {
        public static IEnumerable<string> GetWords()
        {
            string allWords;
            using (var wc = new WebClient())
            {
                allWords = wc.DownloadString("https://raw.githubusercontent.com/first20hours/google-10000-english/master/google-10000-english.txt");
            }
            using (var wordReader = new StringReader(allWords))
            {
                for (;;)
                {
                    var word = wordReader.ReadLine();
                    if (word == null)
                    {
                        break;
                    }
                    yield return word;
                }
            }
        }
    }
}