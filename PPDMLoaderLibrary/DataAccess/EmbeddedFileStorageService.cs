using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PPDMLoaderLibrary.DataAccess
{
    public class EmbeddedFileStorageService : IFileStorageService
    {
        public async Task<string> ReadFile(string fileShare, string fileName)
        {
            string fileContent = "";
            try
            {
                Assembly asm = Assembly.GetExecutingAssembly();
                string[] names = asm.GetManifestResourceNames();
                string streamName = @"PPDMLoaderLibrary." + fileName;
                using Stream stream = asm.GetManifestResourceStream(streamName);
                using StreamReader reader = new(stream);
                fileContent = await reader.ReadToEndAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading plotsymbol file; {ex}");

            }
            
            return fileContent;
        }
    }
}
