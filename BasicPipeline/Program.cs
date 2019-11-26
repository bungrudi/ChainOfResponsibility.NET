using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using BaseClasses;

namespace BasicPipeline
{
    class Program
    {
        static void Main(string[] args)
        {
            // imagine a pipeline is like a production pipeline, where each production node have single and specific function i.e. assemble, coloring, packaging.
            // the difference is that the flow of material is going through once from start to end, and then reversed.
            // the reverse flow happens naturally as method returns.

            // we will create a very simple pipeline which end goal is to have a given string converted to upper case and then save to a file.
            // this function will be implemented by 2 nodes, one for converting the input to upper case and one for storing to file.
            // in between those 2 node we will insert additional node which responsibility is to log to console.
            // the parameter will look like this: 
            // context["input"] -> the string to process
            
            Node first = new StringToUppercase();
            Node second = new LogTheCall();
            Node third = new StringStoreToFile();
            first.Next = second;
            second.Next = third;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param["input"] = "".AddRandom(20);
            Console.WriteLine($"input is {param["input"]}");

            first.Call(param);
        }
    }

    class StringToUppercase : Node
    {
        public override void Call(Dictionary<string, object> context)
        {
            context["input"] = context["input"].ToString().ToUpper();
            Next?.Call(context);
        }
    }

    class StringStoreToFile : Node
    {
        public override void Call(Dictionary<string, object> context)
        {
            File.WriteAllText("result.txt",context["input"].ToString());
            Console.WriteLine("result file created..");
            Next?.Call(context);
        }
    }

    class LogTheCall : Node
    {
        public override void Call(Dictionary<string, object> context)
        {
            Console.WriteLine("before calling next Call()");
            Next?.Call(context);
            Console.WriteLine("after calling next Call()");
        }
    }
}
