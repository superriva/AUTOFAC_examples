using System;
using Autofac;

namespace lib2
{
    public class Lib2: IStartable
    {
        public void Start()
        {
    	    Console.WriteLine("Lib2");
        }
    }
}
