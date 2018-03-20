using System;
using Autofac;
using Autofac.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.IO;
using System.Runtime.Loader;
using System.Diagnostics;
using Assembly = System.Reflection.Assembly;
using System.Collections.Generic;


namespace AutoFac
{
    public class ServiceModule: Module
    {
	public string service { get; set; }
	protected override void Load(ContainerBuilder builder)
	{
	    Console.WriteLine(service);
		        var watch = System.Diagnostics.Stopwatch.StartNew();
	
	    var myAssembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(service);
	    builder.RegisterAssemblyTypes(myAssembly).AsImplementedInterfaces().PropertiesAutowired();
	    builder.RegisterAssemblyModules(myAssembly);
	    
	    		watch.Stop(); var elapsedMs = watch.Elapsed.TotalMilliseconds; Console.WriteLine("Load time (ms): " + elapsedMs);
	}
    }

    public static class ContainerConfig
    {
        public static IContainer Configure(string name, IContainer oldContainer)
        {
	    		var watch = System.Diagnostics.Stopwatch.StartNew();
        
            var builder = new ContainerBuilder();
            
        		watch.Stop(); var elapsedMs = watch.Elapsed.TotalMilliseconds; Console.WriteLine("ContainerBuilder time (ms): " + elapsedMs);
        		watch = System.Diagnostics.Stopwatch.StartNew();
            
	    var config = new ConfigurationBuilder()
		.SetBasePath(Directory.GetCurrentDirectory())
		.AddJsonFile(name, optional:false, reloadOnChange: true);

		    	watch.Stop(); elapsedMs = watch.Elapsed.TotalMilliseconds; Console.WriteLine("ConfigurationBuilder time (ms): " + elapsedMs);
			watch = System.Diagnostics.Stopwatch.StartNew();
	    
	    var configbuild = config.Build();

		    	watch.Stop(); elapsedMs = watch.Elapsed.TotalMilliseconds; Console.WriteLine("config.Build() time (ms): " + elapsedMs);
			watch = System.Diagnostics.Stopwatch.StartNew();

	    var module = new ConfigurationModule(configbuild);
	    
		    	watch.Stop(); elapsedMs = watch.Elapsed.TotalMilliseconds; Console.WriteLine("Create module time (ms): " + elapsedMs);
			watch = System.Diagnostics.Stopwatch.StartNew();
	    
	    builder.RegisterModule(module);
	    
		    	watch.Stop(); elapsedMs = watch.Elapsed.TotalMilliseconds; Console.WriteLine("Register module time (ms): " + elapsedMs);
			watch = System.Diagnostics.Stopwatch.StartNew();
	    
	    IContainer container;
	    
	    if (oldContainer == null)
	    {
		container = builder.Build();
		Console.WriteLine("BUILD");
	    } else {
		container = oldContainer;
		builder.Update(container);
		Console.WriteLine("UPDATE");
	    }
	    		watch.Stop(); elapsedMs = watch.Elapsed.TotalMilliseconds; Console.WriteLine("Build or Update time (ms): " + elapsedMs);
            return container;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
        	var watch = System.Diagnostics.Stopwatch.StartNew();
    	    string JsonName = "autofac.json";
            var container = ContainerConfig.Configure(JsonName, null);
        	watch.Stop(); var elapsedMs = watch.Elapsed.TotalMilliseconds; Console.WriteLine("Container work time (ms): " + elapsedMs);

	    Console.WriteLine("________________________________");

		watch = System.Diagnostics.Stopwatch.StartNew();
	    string JsonName2 = "autofac2.json";
	    var container2 = ContainerConfig.Configure(JsonName2, container);
		watch.Stop(); elapsedMs = watch.Elapsed.TotalMilliseconds; Console.WriteLine("Container work time (ms): " + elapsedMs);
		
        }
    }
}
