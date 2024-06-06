using System;
using System.IO;

namespace BioMA.Utilities.NetFramework
{
    public static class SecurityUtils
    {
        public static string ReplacePassword(string sqlCredentials, string password)
        {
            return sqlCredentials.Replace("<password>", password);
        }

        public static void CheckSingleWord(params string[] words)
        {
            foreach (var word in words)
            {
                if (word.Split(new char[] { ' ', '\t', '\n' }).Length > 1)
                {
                    throw new Exception("Attempted sql injection");
                }
            }
        }

        public static string GetDateRepresentation(DateTime date)
        {
            return date.Year + "-" + date.Month + "-" + date.Day;
        }

        /// <summary>
        /// Builds a <see cref="Stream">Stream</see> from which the <see cref="string">string</see> passed as parameter can be read
        /// </summary>
        /// <param name="s">The <see cref="string">string to be read as <see cref="Stream"/>Stream</see></param>
        /// <returns>The <see cref="Stream">Stream</see> from which te <see cref="string">string</see> passed as parameter can be read</returns>
        public static Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}
