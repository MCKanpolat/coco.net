namespace Coco.Web.Parsers
{
    using System;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;

    // Adapted from http://stackoverflow.com/a/19524290/248164
    public class DefaultHtmlParser : IHtmlParser
    {
        private static readonly Regex Tags = new Regex(@"<[^>]+?>", RegexOptions.Multiline | RegexOptions.Compiled);

        // Add characters that are should not be removed to this regex
        private static readonly Regex NotOkCharacter = new Regex(@"[^\w;&#@.:/\\?=|%!() -]", RegexOptions.Compiled);

        public string Parse(string html)
        {
            html = HttpUtility.UrlDecode(html);
            html = HttpUtility.HtmlDecode(html);

            html = RemoveTag(html, "<!--", "-->");
            html = RemoveTag(html, "<script", "</script>");
            html = RemoveTag(html, "<style", "</style>");

            // Replace matches of these regexes with space
            html = Tags.Replace(html, " ");
            html = NotOkCharacter.Replace(html, " ");
            html = SingleSpacedTrim(html);

            return html;
        }

        private static string RemoveTag(string html, string startTag, string endTag)
        {
            bool again;
            do
            {
                again = false;
                var startTagPos = html.IndexOf(startTag, 0, StringComparison.CurrentCultureIgnoreCase);
                if (startTagPos < 0)
                {
                    continue;
                }

                var endTagPos = html.IndexOf(endTag, startTagPos + 1, StringComparison.CurrentCultureIgnoreCase);
                if (endTagPos <= startTagPos)
                {
                    continue;
                }
                html = html.Remove(startTagPos, endTagPos - startTagPos + endTag.Length);
                again = true;
            }
            while (again);

            return html;
        }

        private static String SingleSpacedTrim(String inString)
        {
            var sb = new StringBuilder();

            var inBlanks = false;
            foreach (var c in inString)
            {
                switch (c)
                {
                    case '\r':
                    case '\n':
                    case '\t':
                    case ' ':
                        if (!inBlanks)
                        {
                            inBlanks = true;
                            sb.Append(' ');
                        }

                        continue;
                    default:
                        inBlanks = false;
                        sb.Append(c);
                        break;
                }
            }

            return sb.ToString().Trim();
        }
    }
}