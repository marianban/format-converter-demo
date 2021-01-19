using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace Moravia.Homework
{
    public class Document
    {
        public string Title { get; set; }
        public string Text { get; set; }
    }

    // problem 6
    // the whole program is missing a global exception handler
    // where we log the exception and store it into a log file for a later diagnostic
    // https://stackoverflow.com/questions/3133199/net-global-exception-handler-in-console-application
    // problem 7
    // because everything inside of the main function the code isn't properly testable
    // it would be possible to write some kind of e2e test which will call the program directly but unit testing would be easier and more effective
    class Program
    {
        static void Main(string[] args)
        {
            // problem 1
            // use System.AppDomain.CurrentDomain.BaseDirectory instead of Environment.CurrentDirectory
            // Environment.CurrentDirectory may not work correctly in some environments
            // problem 8
            // hard coded file paths better to put them into config or read from command line
            var sourceFileName = Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\Source Files\\Document1.xml");
            var targetFileName = Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\Target Files\\Document1.json");

            try
            {
                // problem 2
                // using block or calling stream.Close is missing (prefer using because it properly disposing also in case of exceptions), which leads to memory leaks and resources not being properly disposed
                // problem 10
                // inconsistent code style - use var instead of concrete type
                FileStream sourceStream = File.Open(sourceFileName, FileMode.Open);
                // problem 9
                // files may be temporarily unavailable. we can use a retry logic to mitigate those issues.
                var reader = new StreamReader(sourceStream);
                string input = reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                // problem 3
                // inner exception not passed new Exception("Unable to read file", ex)
				// logging exceptions to file will also help with error investigation
                throw new Exception(ex.Message);
            }

            // problem 4
            // this can throw exception in case of invalid xml
            // we should probably deal with that an dprovide a user ot 
            var xdoc = XDocument.Parse(input);
            var doc = new Document
            {
                // problem 5 there are better ways how to deserialize XML
                // we can deserialize directly into the document object and do not use XDocument.Parse
                Title = xdoc.Root.Element("title").Value,
                Text = xdoc.Root.Element("text").Value
            };

            var serializedDoc = JsonConvert.SerializeObject(doc);

            var targetStream = File.Open(targetFileName, FileMode.Create, FileAccess.Write);
            // missing usings
            var sw = new StreamWriter(targetStream);
            sw.Write(serializedDoc);



        }
    }
}
