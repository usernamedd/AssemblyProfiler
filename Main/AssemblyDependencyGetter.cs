namespace MyAssemblyDependency
{
    using Mono.Cecil;
    using System.Reflection;
    using System.Reflection.PortableExecutable;
    using System.Runtime.InteropServices;
    using System.Runtime.Loader;

    public class AssemblyDependencyGetter
    {
        public static string TagetDll = "";
        public static void ShowInfo()
        {
            //Load the Assembly: Load the assembly you want to inspect using Mono.Cecil.
            AssemblyDefinition assembly = AssemblyDefinition.ReadAssembly(TagetDll);
            /*
            To get a list of assemblies referenced by a.dll, you can loop through the assembly.MainModule.AssemblyReferences collection.
            */
            System.Console.WriteLine($"all dependent assemblies-------------------");
            foreach (var reference in assembly.MainModule.AssemblyReferences)
            {
                Console.WriteLine($"Reference: {reference.FullName}");
            }
            /*
            To find methods, properties, and types within a.dll, you can traverse the assembly's modules, types, 
            and methods using Mono.Cecil. For example, to find methods:
            */
            foreach (var module in assembly.Modules)
            {
                foreach (var type in module.Types)
                {
                    foreach (var method in type.Methods)
                    {
                        Console.WriteLine($"Method: {method.FullName}");
                    }
                }
            }

        }
        /// <summary>
        /// 得到程序集的目标平台
        /// </summary>
        /// <param name="assembly">目标程序集</param>
        /// <returns>特性信息</returns>
        public static CustomAttributeData GetTargetPlatformAttribute(Assembly assembly)
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
        /// <summary>
        /// CPU架构
        /// </summary>
        /// <param name="assemblyPath"></param>
        /// <returns></returns>
        public static string GetTargetCpuArch(string assemblyPath)
        {
            using (var stream = System.IO.File.OpenRead(assemblyPath))
            using (var peReader = new PEReader(stream))
            {
                // Get the COFF header of the assembly
                CoffHeader coffHeader = peReader.PEHeaders.CoffHeader;

                // Get the target CPU architecture
                Machine targetMachine = coffHeader.Machine;
                return targetMachine.ToString();
            }
        }
    }
}