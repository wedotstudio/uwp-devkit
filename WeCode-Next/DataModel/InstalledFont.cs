using SharpDX.DirectWrite;
using System.Collections.Generic;
using System.Globalization;

namespace WeCode_Next.DataModel
{
    public class InstalledFont
    {
        public string Name { get; set; }

        public int FamilyIndex { get; set; }

        public int Index { get; set; }

        public static List<InstalledFont> GetFonts()
        {
            var fontList = new List<InstalledFont>();

            var factory = new Factory();
            var fontCollection = factory.GetSystemFontCollection(false);
            var familyCount = fontCollection.FontFamilyCount;

            for (int i = 0; i < familyCount; i++)
            {
                var fontFamily = fontCollection.GetFontFamily(i);
                var familyNames = fontFamily.FamilyNames;
                int index;

                if (!familyNames.FindLocaleName(CultureInfo.CurrentCulture.Name, out index))
                {
                    if (!familyNames.FindLocaleName("en-us", out index))
                    {
                        index = 0;
                    }
                }

                string name = familyNames.GetString(index);
                if (name == "Segoe UI Symbol" || name == "Segoe UI Emoji" || name == "Segoe MDL2 Assets")
                {
                    fontList.Add(new InstalledFont()
                    {
                        Name = name,
                        FamilyIndex = i,
                        Index = index
                    });
                }
            }

            return fontList;
        }

        public List<Character> GetCharacters()
        {
            var factory = new Factory();
            var fontCollection = factory.GetSystemFontCollection(false);
            var fontFamily = fontCollection.GetFontFamily(FamilyIndex);
            int ini = 0;

            var font = fontFamily.GetFont(Index);
            var characters = new List<Character>();
            var count = 65535;
            if (Name == "Segoe UI Symbol")
            {
                ini = 161;
            }
            else if (Name == "Segoe UI Emoji")
            {
                ini = 8252;
            }
            else if (Name == "Segoe MDL2 Assets")
            {
                ini = 54375;
            }
            for (var i = ini; i < count; i++)
            {
                if (font.HasCharacter(i))
                {
                    characters.Add(new Character()
                    {
                        Char = char.ConvertFromUtf32(i),
                        UnicodeIndex = i,
                        Font = Name
                    });
                }
            }

            return characters;
        }
    }
    public class Character
    {
        public string Font { get; set; }
        public string Char { get; set; }
        public int UnicodeIndex { get; set; }
    }
}
