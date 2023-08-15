// See https://aka.ms/new-console-template for more information
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;
using System.Runtime.Loader;

internal class Program
{
    private static void Main(string[] args)
    {
        // System.Console.WriteLine(args[0]);
        // Console.WriteLine("Hello, World!");
        string directoryPath = args[0];

        string[] assemblyFiles = Directory.GetFiles(directoryPath, "*.dll");

        foreach (string assemblyFile in assemblyFiles)
        {
            try
            {
                System.Console.WriteLine($"cpu machine : {GetTargetCpuArch(assemblyFile)}");
                // AssemblyLoadContext context = new AssemblyLoadContext(null, true);
                // Assembly assembly = context.LoadFromAssemblyPath(assemblyFile);
                // CustomAttributeData targetPlatformAttribute = GetTargetPlatformAttribute(assembly);
                // AssemblyName assemblyName = AssemblyName.GetAssemblyName(assemblyFile);

                // if (targetPlatformAttribute != null)
                // {
                //     Console.WriteLine($"Assembly: {assembly.FullName}");
                //     Console.WriteLine($"Target Platform: {targetPlatformAttribute.ConstructorArguments[0].Value}");
                //     System.Console.WriteLine($"cpu machine : {GetTargetCpuArch(assemblyFile)}");
                //     Console.WriteLine();
                // }
                // else
                // {
                //     Console.WriteLine($"not get the Target Platform into from assembly : {assembly.FullName}");
                // }

                // context.Unload();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading assembly {assemblyFile}: {ex.Message}");
            }
        }
    }
    static CustomAttributeData GetTargetPlatformAttribute(Assembly assembly)
    {
        foreach (CustomAttributeData attribute in assembly.GetCustomAttributesData())
        {
            if (attribute.AttributeType.FullName == "System.Runtime.Versioning.TargetFrameworkAttribute")
            {
                return attribute;
            }
        }
        return null;
    }

    static string GetTargetCpuArch(string assemblyPath)
    {
        using (var stream = System.IO.File.OpenRead(assemblyPath))
        using (var peReader = new PEReader(stream))
        {
            // Get the COFF header of the assembly
            CoffHeader coffHeader = peReader.PEHeaders.CoffHeader;

            // Get the target CPU architecture
            Machine targetMachine = coffHeader.Machine;
            return targetMachine.ToString();
            // Print the target CPU architecture
            Console.WriteLine("Target CPU architecture: " + targetMachine);
        }
    }
}