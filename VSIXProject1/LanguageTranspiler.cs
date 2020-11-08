using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TextTemplating.VSHost;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSIXProject1
{
    public static class StringHelper
    {
        public static (string consumed,string rest) Consume(this string text, string token)
        { 
            if (text.Contains(token))
            {
                var after = text.IndexOf(token) + token.Length;
                return (text.Substring(0,after), text.Substring(after));
            }

            return (string.Empty, string.Empty);
        }

        public static (string consumed, string rest) Consume(this string text, string token, char escapeChar)
        {
            if (text.Contains(token))
            {
                var after = text.IndexOf(token) + token.Length;
                var charbefore = text[text.IndexOf(token) - 1];
                if (charbefore == escapeChar)
                {
                    var sub = (text.Substring(0, after), text.Substring(after));
                    var subRes = sub.Item2.Consume(token, escapeChar);
                    return (sub.Item1 + subRes.consumed, subRes.rest);
                }

                return (text.Substring(0, after), text.Substring(after));
            }

            return (string.Empty, string.Empty);
        }

        public static (string res,string rest) Between(this string text, string open, string close)
        {
            var res = text.Consume(open).rest.Consume(close);
            return (res.consumed.TrimEnd(close.ToCharArray()), res.rest);
        }

        public static (string res, string rest) Between(this string text, string open, string close, char escapeChar)
        {
            var res = text.Consume(open,escapeChar).rest.Consume(close,escapeChar);
            return (res.consumed.TrimEnd(close.ToCharArray()), res.rest);
        }
    }

    public class LanguageTranspiler : BaseCodeGeneratorWithSite
    {
        public const string Name = nameof(LanguageTranspiler);
        public const string Desc = "A very specific language transpiler that takes every line in a file that contains ReadString and writes the first two quoted strings as a key=value text file";
        public override string GetDefaultExtension()
            => ".txt";

        protected override byte[] GenerateCode(string inputFileName, string inputFileContent)
        {
            var lines = File.ReadAllLines(inputFileName);

            var res = new List<string>();
            foreach (var line in lines)
            {
                var first = line.Consume("ReadString").rest.Between("\"", "\"");
                var second = first.rest.Between("\"", "\"");
                if(first.res.Length > 0 && second.res.Length > 0)
                    res.Add($"\"{first.res}\"=\"{second.res}\"");
            }

            return UTF8Encoding.UTF8.GetBytes(String.Join("\n",res));
        }
    }
}
