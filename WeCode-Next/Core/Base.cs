using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.StartScreen;

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

        public static void JumpListBuilder(ref JumpList jumpList, string arg, string name, string icon, string groupname = "Functions", string description = "")
        {
            var Item = JumpListItem.CreateWithArguments(arg, name);
            Item.Description = (description == "") ? name : description;
            Item.GroupName = groupname;
            Item.Logo = new Uri(icon);
            jumpList.Items.Add(Item);
        }
    }
}
