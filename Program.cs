// See https://aka.ms/new-console-template for more information
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Loader;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        string directoryPath = @"C:\Path\To\Directory";

        string[] assemblyFiles = Directory.GetFiles(directoryPath, "*.dll");

        foreach (string assemblyFile in assemblyFiles)
        {
            try
            {
                AssemblyLoadContext context = new AssemblyLoadContext(null, true);
                Assembly assembly = context.LoadFromAssemblyPath(assemblyFile);
                CustomAttributeData targetPlatformAttribute = GetTargetPlatformAttribute(assembly);

                if (targetPlatformAttribute != null)
                {
                    Console.WriteLine($"Assembly: {assembly.FullName}");
                    Console.WriteLine($"Target Platform: {targetPlatformAttribute.ConstructorArguments[0].Value}");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine($"not get the Target Platform into from assembly : {assembly.FullName}");
                }

                context.Unload();
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
}