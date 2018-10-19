using System.Collections.Generic;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Folding;

namespace SmartDose.RestClientApp
{
    public class JsonFoldingStrategy
    {
        public void UpdateFoldings(FoldingManager manager, TextDocument document)
        {
            int firstErrorOffset;
            var newFoldings = CreateNewFoldings(document, out firstErrorOffset);
            manager.UpdateFoldings(newFoldings, firstErrorOffset);
        }

        private IEnumerable<NewFolding> CreateNewFoldings(TextDocument document, out int firstErrorOffset)
        {
            firstErrorOffset = -1;

            var foldings = new List<NewFolding>();

            var s = document.Text;
            CreateNewFoldings(foldings, s, 0);

            return foldings;
        }

        private static int CreateNewFoldings(List<NewFolding> foldings, string s, int start)
        {
            var i = start;
            while (i < s.Length && s[i] != '{')
                if (s[i] == '}')
                    return -1;
                else
                    i++;

            if (i > -1 && i < s.Length)
            {
                var fold = new NewFolding { StartOffset = i + 1 };
                foldings.Add(fold);

                var innerEnd = i;
                int innerStart;
                do
                {
                    innerStart = innerEnd + 1;
                    innerEnd = CreateNewFoldings(foldings, s, innerStart);
                } while (innerEnd > -1);

                fold.EndOffset = s.IndexOf(@"}", innerStart);
                if (fold.EndOffset < 0)
                    foldings.Remove(fold);
                else
                    return fold.EndOffset;
            }
            return -1;
        }
    }
}
