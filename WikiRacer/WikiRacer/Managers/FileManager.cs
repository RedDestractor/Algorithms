using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;

namespace WikiRacer
{
    public static class FileManager
    {
        public static string CreateFile(string fileName)
        {
            var path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ""));
            var file = Path.Combine(path, fileName);

            if (!File.Exists(file))
            {
                try
                {
                    StreamWriter writer = new StreamWriter(File.Open(file, FileMode.OpenOrCreate, FileAccess.Write), Encoding.UTF8);
                    writer.Close();
                }
                catch (Exception ex)
                {

                }
            }

            return file;
        }

        public static UnmanagedMemoryStream GetFileFromResource(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var strResources = assembly.GetName().Name + ".g.resources";
            var rStream = assembly.GetManifestResourceStream(strResources);
            var resourceReader = new ResourceReader(rStream);
            var items = resourceReader.OfType<DictionaryEntry>();
            var stream = items.First(x => (x.Key as string) == resourceName.ToLower()).Value;

            return (UnmanagedMemoryStream)stream;
        }
    }
}