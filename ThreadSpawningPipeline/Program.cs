using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using BaseClasses;

namespace ThreadSpawningPipeline
{
    class Program
    {
        static void Main(string[] args)
        {
            // similar to BasicPipeline, except that LogTheCall will spawn an async Task which will need to be managed.
            // we have to kill it upon returning.

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
            Thread.Sleep(5000); // pretend that this is a time consuming task..
            File.WriteAllText("result.txt", context["input"].ToString());
            Console.WriteLine("result file created..");
            Next?.Call(context);
        }
    }

    class LogTheCall : Node
    {
        public override void Call(Dictionary<string, object> context)
        {
            // CancellationToken is the idiomatic way of cancelling a Task in C#.
            CancellationTokenSource source = new CancellationTokenSource();
            Logger l = new Logger(source.Token);
            // start async logging task (providing async context for it).. 
            // if we are inside an async method then we just need to call l.DoLog() directyle.
            Task.Run(async () => l.DoLog());

            try
            {
                Next?.Call(context);
                Thread.Sleep(2000); // wait for another 2 second before we cancel the logging task..
            }
            finally
            {
                // the finally block is important to make sure we cancel any running Task even if Next.Call() throws exception
                source.Cancel(false);
            }

        }

        
    }

    class Logger
    {
        private CancellationToken cancellationToken;

        public Logger(CancellationToken cancellationToken)
        {
            this.cancellationToken = cancellationToken;
        }

        public async Task DoLog()
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                Console.WriteLine("I will keep logging until the Task cancelled.");
                Thread.Sleep(500);
            }
        }
    }
}
