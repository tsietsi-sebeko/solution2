using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace code
{
    class Program
    {

        private static int index = 0;
        private static string str = "";
        private string res = "";

       private ArrayList males = new ArrayList();
        private ArrayList females = new ArrayList();
        private ArrayList percentageArr = new ArrayList();
        private object v;

        static void Main(string[] args)
        {
            Program p = new Program();
            p.WriteResultsToTextFile();
        }

        public string runMatch(string str1, string str2)
        {
            var result = "";
            var tempStr = "";
            str = String.Concat((str1 + str2).Where(c => !Char.IsWhiteSpace(c)));

            if (isValideInput(str))
            {
                for (int i = 0; i <= str.Length - 1; i++)
                {
                    int index = 1;
                    for (int j = i + 1; j <= str.Length - 1; j++)
                        if (str[i].Equals(str[j]))
                        {
                            str = str.Remove(j, 1);
                            index++;
                        }
                    result = result + index + "";

                }
                var size = 0;
                tempStr = result;
                var counter = 0;
                size = result.Length;
                counter = 0;

                result = macthrecursively(result);
                result = result.Insert(result.Length, getmidchar(tempStr));
                tempStr = result;
                int n;
                do
                {

                    if (counter < 1)
                        tempStr = tempStr.Insert(tempStr.Length, getmidchar(result));

                    n = result.Length;
                    tempStr = macthrecursively(result);

                    if (counter < 1)
                    {
                        if (size % 2 != 0)
                            tempStr = tempStr.Substring(n - 1);
                        else
                            tempStr = tempStr.Substring(n);

                    }
                    tempStr = tempStr.Insert(tempStr.Length, getmidchar(result));
                    counter++;
                    result = tempStr;

                    res = "";
                } while (result.Length > 2);
                Console.WriteLine();

            }
            else
                Console.WriteLine("Invalid input");
            return tempStr;
        }
        public string getmidchar(string text)
        {
            if (text.Length % 2 != 0)
                return "" + text[text.Length / 2];
            else return "";
        }
        public string macthrecursively(string text)
        {
            str = res;
            if (text.Length <= 1)
            {
                text = str;
                return text;
            }
            else
            {

                res = res + ((int)char.GetNumericValue(text[0]) + (int)char.GetNumericValue(text[text.Length - 1])) + "";

                return macthrecursively(text.Substring(1, text.Length - 2));

            }

            
        }
        public Boolean isValideInput(string inputstring)
        {
            var regex = @"^[a-zA-Z]+$";
            Match match = Regex.Match(inputstring, regex, RegexOptions.IgnoreCase);
            if (match.Success)
                return true;
            else return false;
        }
        public void readCSVFile(StreamReader file)
        {
            var parser = new Microsoft.VisualBasic.FileIO.TextFieldParser(file);
            parser.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited;
            parser.SetDelimiters(new string[] { ";" });
            while(!parser.EndOfData)
            {
                string[] row = parser.ReadFields();
                for (int i = 0; i < row.Length; i++)
                {
                    
                    if (i % 2 != 0)
                    {
                        females.Add(row[i]);
                        Console.Write(row[i] + " ");
                    }
                    else
                    {
                        males.Add(row[i]);
                        Console.Write(row[i] + " ");
                    }
                 }
                Console.WriteLine();
            }
                     
            }
        /*write results to text file
         */ 
        public void WriteResultsToTextFile()
        {
            var matchresults = "";
          using  StreamReader file = new StreamReader("CSVFile.csv");
            readCSVFile(file);
            for(int i = 0; i < males.Count; i ++)
            {
                for (int j = 0; j < females.Count; j++)
                {
                    matchresults = runMatch((string)females[j], (string)males[i]);
                    percentageArr.Add(matchresults);
                    if (matchresults != "") 
                    if (Int32.Parse(matchresults) >= 80)
                        Console.Write((string)males[i]+" matches "+(string)females[j]+" "+ matchresults+"%" + ", good match");
                     
                 }
            }
            percentageArr.Sort(new NumericComparer());
            StreamWriter writer = new StreamWriter("output.txt") ;
            writer.WriteLine("Matching Percentage");
            for(int i = percentageArr.Count - 1; i >= 0; i --)
                writer.WriteLine(percentageArr[i]+ "%");
            writer.Close();
        }
        /*
         * sorting the arraylist
         */
        public class NumericComparer : IComparer
        {
            public int Compare(object x, object y)
            {
                string left = (string)x;
                string right = (string)y;
                int max = Math.Min(left.Length, right.Length);
                for (int i = 0; i < max; i++)
                {
                    if (left[i] != right[i])
                    {
                        return left[i] - right[i];
                    }
                }
                return left.Length - right.Length;
            }
        }
            }
           
        
    }
   
