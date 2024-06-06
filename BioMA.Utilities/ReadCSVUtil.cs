#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;

#endregion


namespace JRC.IPSC.MARS.Utilities
{

    ///<summary>
    /// Helper to read _csvStream files row by row (for big files)
    ///</summary>
    public class ReadCSVByRowUtil
    {
        private static CsvStream _csvStream;
        public static void SetTextReader(TextReader stream, bool headers)
        {

            _csvStream = new CsvStream(stream);
          
            if (headers)
            {
                _csvStream.GetNextRow();
            }
        }

        /// <summary>
        /// Returns null if EOF
        /// </summary>
        /// <returns></returns>
        public static string[] GetNextRow()
        {
            return _csvStream.GetNextRow();
        }

        /// <summary>
        /// Closes the TextReader
        /// </summary>
        public static void Close()
        {
            _csvStream.CloseTextReader();
        }
    }

    ///<summary>
    /// Helper to read csv files and store the file content into a DataTable
    ///</summary>
    public class ReadCSVUtil
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">The cvs text in form of a string</param>
        /// <param name="headers">Headers in the first row</param>
        /// <returns></returns>
        public static DataTable Parse(string data, bool headers)
        {
            return Parse(new StringReader(data), headers);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">The cvs text in form of a string</param>
        /// <returns></returns>
        public static DataTable Parse(string data)
        {
            return Parse(new StringReader(data));
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream">A textStream</param>
        /// <returns></returns>
        public static DataTable Parse(TextReader stream)
        {
            return Parse(stream, false);
        }

        /// <summary>
        /// Parses the text stram and puts the contents into a DataTable. Every value in the table is a String
        /// </summary>
        /// <param name="stream">A textStream</param>
        /// <param name="headers">True if there is an header in the first row</param>
        /// <returns></returns>
        public static DataTable Parse(TextReader stream, bool headers)
        {
            DataTable table = new DataTable();
            CsvStream csv=null;
            try
            {
                csv = new CsvStream(stream);
                string[] row = csv.GetNextRow();
                if (row == null)
                    return null;
                if (headers)
                {
                    foreach (string header in row)
                    {
                        if (header != null && header.Length > 0 && !table.Columns.Contains(header))
                            table.Columns.Add(header, typeof(string));
                        else
                            table.Columns.Add(GetNextColumnHeader(table), typeof(string));
                    }
                    row = csv.GetNextRow();
                }
                while (row != null)
                {
                    while (row.Length > table.Columns.Count)
                        table.Columns.Add(GetNextColumnHeader(table), typeof(string));
                    table.Rows.Add(row);
                    row = csv.GetNextRow();
                }
            }
            finally
            {
                csv.CloseTextReader();
            }
            return table;
        }


        /// <summary>
        /// Parses the text stram and puts the contents into a DataTable. The Type of the columns are defined on the basis of the content of the first row of data.
        /// The user has to provide a list of Types to check for.
        /// Column by column, the content of the first row is analyzed to find one of the Types. If the content of a cell is of one of the Types, the column type is set to that Type, and each value of the column is transformed to that Type. If there is an error in the transformation for that row the value will not be converted.
        /// All not converted values will remain of type String. 
        /// If more than a Type is suitable to be used as column type, the first in the array of Types is used.
        /// </summary>
        /// <param name="stream">A textStream</param>
        /// <param name="headers">True if there is an header in the first row</param>
        /// <param name="typesToCheckFor">The sequence of Types to check for (keys of the Dictionary), and the (eventual) IFormatProvider (values of the Dictionary)</param>
        /// <param name="cultureInfo">Culture</param>
        /// <returns></returns>
        public static DataTable ParseAndInferColumnTypesFromFirstRow(TextReader stream, bool headers,Dictionary<Type,IFormatProvider> typesToCheckFor, CultureInfo cultureInfo)
        {

            DataTable table = new DataTable();
            if (cultureInfo!=null)
            {
                table.Locale = cultureInfo;
            }
            
            CsvStream csv = null;
            try
            {
                csv = new CsvStream(stream);
                string[] row = csv.GetNextRow();
                if (row == null)
                    return null;
                if (headers)
                {
                    foreach (string header in row)
                    {
                        if (header != null && header.Length > 0 && !table.Columns.Contains(header))
                            table.Columns.Add(header, typeof(string));
                        else
                            table.Columns.Add(GetNextColumnHeader(table), typeof(string));
                    }
                    row = csv.GetNextRow();
                }

                bool firstDataRow = true;
                Dictionary<int, Type> TypeForColumns = new Dictionary<int, Type>();

                while (row != null)
                {
                    while (row.Length > table.Columns.Count)
                        table.Columns.Add(GetNextColumnHeader(table), typeof(string));

                    if(firstDataRow)
                    {

                        for (int i=0;i<table.Columns.Count;i++)
                        {
                            object obj=row.GetValue(i);
                            DataColumn col = table.Columns[i];

                            //look at the types in the array order
                            foreach (KeyValuePair<Type, IFormatProvider> t in typesToCheckFor)
                            {
                                if (!TypeForColumns.ContainsKey(i)) TypeForColumns.Add(i, null);
                             
                                try
                                {
                                    //without format provider
                                    if (t.Value == null)
                                    {
                                        Convert.ChangeType(obj, t.Key);
                                    }
                                    //with format provider
                                    else
                                    {
                                        Convert.ChangeType(obj, t.Key,t.Value);
                                    }
                                    TypeForColumns[i] = t.Key;
                                    break;
                                }
                                catch (Exception)
                                {

                                }
                            }


                            if(TypeForColumns[i]!=null)
                            {
                                col.DataType = TypeForColumns[i];                                
                            }
                        }
                    }

                    object[] newRow=new object[row.Length];
                    int j = 0;

                    //for each row convert the values
                    foreach (string o in row)
                    {
                        if (TypeForColumns[j]!=null && TypeForColumns[j] != typeof(String))
                        {
                            try
                            {
                                IFormatProvider fp = typesToCheckFor[TypeForColumns[j]];
                                //with format provider
                                if (fp != null)
                                {
                                    newRow[j] = Convert.ChangeType(row[j], TypeForColumns[j],fp);
                                }
                                //without format provider
                                else
                                {
                                    newRow[j] = Convert.ChangeType(row[j], TypeForColumns[j]);
                                }
                            }catch(Exception)
                            {
                                throw new Exception("The CSV parser found an error in converting value '" + row[j] + "'. Impossible to convert it into type '" + TypeForColumns[j] + "' (column name:'" + table.Columns[j] + "'). Try to parse the file using only Strings, to change the IFormatProvider, or to define a different list of types to look for.");
                            }
                        }
                        else
                        {
                            newRow[j] = row[j];
                        }
                        j++;
                    }

                    table.Rows.Add(row);
                    row = csv.GetNextRow();
                    firstDataRow = false;
                }
            }
            finally
            {
                csv.CloseTextReader();
            }
            return table;
        }


        private static string GetNextColumnHeader(DataTable table)
        {
            int c = 1;
            while (true)
            {
                string h = "Column" + c++;
                if (!table.Columns.Contains(h))
                    return h;
            }
        }

        /// <summary>
        /// By default is ','
        /// </summary>
        public static char SEPARATOR = ',';


    }

    #region Nested type: CsvStream

    internal class CsvStream
    {
        private char[] buffer = new char[4096];
        private bool EOL;
        private bool EOS;
        private int length;
        private int pos;
        private TextReader stream;

        internal CsvStream(TextReader s)
        {
            stream = s;
        }

        internal void CloseTextReader()
        {
            if(stream!=null)stream.Close();
        }

        internal string[] GetNextRow()
        {
            ArrayList row = new ArrayList();
            while (true)
            {
                string item = GetNextItem();
                if (item == null)
                    return row.Count == 0 ? null : (string[])row.ToArray(typeof(string));
                row.Add(item);
            }
        }

        private string GetNextItem()
        {
            if (EOL)
            {
                // previous item was last in line, start new line
                EOL = false;
                return null;
            }

            bool quoted = false;
            bool predata = true;
            bool postdata = false;
            StringBuilder item = new StringBuilder();

            while (true)
            {
                char c = GetNextChar(true);
                if (EOS)
                    return item.Length > 0 ? item.ToString() : null;

                if ((postdata || !quoted) && c == ReadCSVUtil.SEPARATOR)
                    // end of item, return
                    return item.ToString();

                if ((predata || postdata || !quoted) && (c == '\x0A' || c == '\x0D'))
                {
                    // we are at the end of the line, eat newline characters and exit
                    EOL = true;
                    if (c == '\x0D' && GetNextChar(false) == '\x0A')
                        // new line sequence is 0D0A
                        GetNextChar(true);
                    return item.ToString();
                }

                if (predata && c == ' ')
                    // whitespace preceeding data, discard
                    continue;

                if (predata && c == '"')
                {
                    // quoted data is starting
                    quoted = true;
                    predata = false;
                    continue;
                }

                if (predata)
                {
                    // data is starting without quotes
                    predata = false;
                    item.Append(c);
                    continue;
                }

                if (c == '"' && quoted)
                {
                    if (GetNextChar(false) == '"')
                        // double quotes within quoted string means add a quote       
                        item.Append(GetNextChar(true));
                    else
                        // end-quote reached
                        postdata = true;
                    continue;
                }

                // all cases covered, character must be data
                item.Append(c);
            }
        }

        private char GetNextChar(bool eat)
        {
            if (pos >= length)
            {
                length = stream.ReadBlock(buffer, 0, buffer.Length);
                if (length == 0)
                {
                    EOS = true;
                    return '\0';
                }
                pos = 0;
            }
            if (eat)
                return buffer[pos++];
            else
                return buffer[pos];
        }
    }

    #endregion
}