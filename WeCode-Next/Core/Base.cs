using System;
using System.Threading.Tasks;
using Windows.Storage;

namespace WeCode_Next.Core
{
    public class Base
    {
        public const String VERSION = "0506";

        public async static Task<bool> IsFilePresent(string fileName)
        {
            var item = await ApplicationData.Current.LocalFolder.TryGetItemAsync(fileName);
            return item != null;
        }
    }
}
