using System;
using Autofac;

namespace lib1
{
    public class Lib1: IStartable
    {
        public void Start()
        {
    	    Console.WriteLine("Lib1");
        }
    }
}
