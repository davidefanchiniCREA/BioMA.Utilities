using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace JRC.IPSC.MARS.Utilities
{
    public static class Util
    {
        public static IList<T> GetListFromFile<T>(string filePath, Func<string, T> parser)
        {
            return GetListFromFile(new StreamReader(filePath), parser);
        }

        public static IList<T> GetListFromFile<T>(StreamReader sr, Func<string, T> parser)
        {
            IList<T> resultList = new List<T>();
            string currValue = sr.ReadLine();
            while (currValue != null)
            {
                resultList.Add(parser(currValue));
                currValue = sr.ReadLine();
            }
            return resultList;
        }
    }
}
