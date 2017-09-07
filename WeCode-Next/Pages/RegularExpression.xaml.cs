using System;
using System.Text.RegularExpressions;
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml.Controls;

namespace WeCode_Next.Pages
{
    public sealed partial class RegularExpression : Page
    {
        public RegularExpression()
        {
            this.InitializeComponent();
        }

        private void re_TextChanged(object sender, TextChangedEventArgs e)
        {
            RegExpFinder(sender);
        }
        private void RegExpFinder(object receiver)
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
