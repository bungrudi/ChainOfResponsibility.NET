using System;
using System.Collections.Generic;

namespace BaseClasses
{
    public abstract class Node
    {
        /// <summary>
        /// Each implementor have the obligation to call Next.Call(context) if Next != null.
        /// </summary>
        /// <param name="context"></param>
        public abstract void Call(Dictionary<string, object> context);
        public Node Next { get; set; }
    }

    
}
