using System;
using System.Linq;

namespace BkTree
{
    public class DamerauLevenshteinDistanceMetric : IDistanceMetric<string>
    {
        
        //https://gist.github.com/wickedshimmy/449595
        public int CalculateDistance(string value1, string value2)
        {
            int lenOrig = value1.Length;
            int lenDiff = value2.Length;

            var matrix = new int[lenOrig + 1, lenDiff + 1];
            for (int i = 0; i <= lenOrig; i++)
                matrix[i, 0] = i;
            for (int j = 0; j <= lenDiff; j++)
                matrix[0, j] = j;

            for (int i = 1; i <= lenOrig; i++)
            {
                for (int j = 1; j <= lenDiff; j++)
                {
                    int cost = value2[j - 1] == value1[i - 1] ? 0 : 1;
                    var vals = new int[]
                    {
                        matrix[i - 1, j] + 1,
                        matrix[i, j - 1] + 1,
                        matrix[i - 1, j - 1] + cost
                    };
                    matrix[i, j] = vals.Min();
                    if (i > 1 && j > 1 && value1[i - 1] == value2[j - 2] && value1[i - 2] == value2[j - 1])
                        matrix[i, j] = Math.Min(matrix[i, j], matrix[i - 2, j - 2] + cost);
                }
            }
            return matrix[lenOrig, lenDiff];
        }
    }
}