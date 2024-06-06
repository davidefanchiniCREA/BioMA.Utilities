using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JRC.IPSC.MARS.Utilities
{
    public class RelativePathUtility
    {
        /// <summary>
        /// Usage: GetRelativePathFromAbsolute("C:\temp", "C:\temp\temp2") returns "temp2"
        /// </summary>
        /// <param name="relPath"></param>
        /// <param name="absPath"></param>
        /// <returns></returns>
        public static string GetRelativePathFromAbsolute(string relPath, string absPath)
        {
            string[] directories = relPath.ToLowerInvariant().Split('\\');
            string[] absDirectories = absPath.ToLowerInvariant().Split('\\');

            //Get the shortest of the two paths
            int length = directories.Length < absDirectories.Length ? directories.Length : absDirectories.Length;

            //Use to determine where in the loop we exited
            int lastCommonRoot = -1;
            int index;

            //Find common root
            for (index = 0; index < length; index++)
                if (directories[index] == absDirectories[index])
                    lastCommonRoot = index;
                else
                    break;

            //If we didn't find a common prefix then throw
            if (lastCommonRoot == -1)
                throw new ArgumentException("Paths do not have a common base");

            //Build up the relative path
            StringBuilder relativePath = new StringBuilder();

            //Add on the ..
            for (index = lastCommonRoot + 1; index < directories.Length; index++)
                if (directories[index].Length > 0)
                    relativePath.Append("..\\");

            //Add on the folders
            string[] relativeDirs = absPath.Split('\\');
            for (index = lastCommonRoot + 1; index < relativeDirs.Length - 1; index++)
                relativePath.Append(relativeDirs[index] + "\\");
            relativePath.Append(relativeDirs[relativeDirs.Length - 1]);

            return relativePath.ToString();
        }
    }
}
