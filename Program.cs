// See https://aka.ms/new-console-template for more information
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;
using System.Runtime.Loader;
using MyAssemblyDependency;

internal class Program
{
    private static void Main(string[] args)
    {
        try
        {
            AssemblyDependencyGetter.ShowInfo();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading assembly : {ex.Message}");
        }
    }

}