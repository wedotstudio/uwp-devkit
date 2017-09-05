using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WeCode_Next.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RegularExpression : Page
    {
        public RegularExpression()
        {
            this.InitializeComponent();
        }

        private void re_TextChanged(object sender, TextChangedEventArgs e)
        {
            RegExpFinder();
        }

        private void RegExpFinder()
        {
            try
            {
                string input;
                tb.Document.GetText(TextGetOptions.None, out input);
                var myRichEditLength = input.Length;
                tb.Document.Selection.SetRange(0, myRichEditLength);
                ITextSelection clearText = tb.Document.Selection;
                if (clearText != null)
                {
                    clearText.CharacterFormat.BackgroundColor = Colors.Transparent;
                }
                if (re.Text != "")
                {
                    string pattern = @re.Text;
                    Regex rgx = new Regex(pattern, RegexOptions.Multiline);
                    MatchCollection matches = rgx.Matches(input);
                    int tmp = 0;
                    if (matches.Count > 0)
                    {
                        foreach (Match match in matches)
                        {
                            tb.Document.Selection.SetRange(tmp, myRichEditLength);
                            int i = 1;
                            while (i > 0)
                            {
                                i = tb.Document.Selection.FindText(match.Value, myRichEditLength, FindOptions.Case);
                                tmp = i;
                                ITextSelection selectedText = tb.Document.Selection;
                                if (selectedText != null)
                                {
                                    selectedText.CharacterFormat.BackgroundColor = Colors.LightBlue;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            { }
        }
    }
}
