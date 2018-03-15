using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bitwise.Tests
{
    public class CodeGenerator
    {
        [Test]
        public void GenerateCodeForAlternateNumericTypes()
        {
            var testBinDirectory = Path.GetDirectoryName(this.GetType().Assembly.Location);

            GenerateCodeForAlternateNumericTypes(Path.Combine(testBinDirectory, @"..\..\..\..\Bitwise\Bits.cs"));
        }

        private static void GenerateCodeForAlternateNumericTypes(string path)
        {
            var parsed = ParseFile(File.ReadAllText(path));

            var alternateNumericTypes = new[]
            {
                typeof(sbyte),
                typeof(byte),
                typeof(short),
                typeof(ushort),
                typeof(int),
                typeof(uint),
                typeof(ulong)
            };

            foreach (var type in alternateNumericTypes)
            {
                var typePath = Path.Combine(Path.GetDirectoryName(path), $"{Path.GetFileNameWithoutExtension(path)}.{type.Name}{Path.GetExtension(path)}");
                using (var writer = new StreamWriter(typePath))
                {
                    writer.Write(parsed.Header);

                    foreach (var member in parsed.Members.GroupBy(m => m.Name))
                    {
                        // todo
                    }

                    writer.Write(parsed.Footer);
                }
            }
        }

        private static FileParseResult ParseFile(string content)
        {
            const string Delimiter = "/// <summary>";
            var split = Regex.Split(content, $"({Delimiter})|(// END MEMBERS)", RegexOptions.ExplicitCapture);
            var headerSectionCount = split[0].Contains("partial class") ? 1 : 2;

            var memberSections = split.Skip(headerSectionCount).Take(split.Length - headerSectionCount - 1);

            var result = new FileParseResult
            {
                Header = string.Join(Delimiter, split.Take(headerSectionCount)),
                Footer = split.Last(),
                Members = memberSections.Select(s => ParseMember(Delimiter + s)).ToList(),
            };

            return result;
        }

        private static MemberParseResult ParseMember(string memberContent)
        {
            var nameMatches = Regex.Matches(memberContent, @"\n\s+(public|private|internal).*?(?<name>[A-Z]\w*)");
            if (nameMatches.Count != 1) { throw new FormatException("Failed to parse name of member: " + memberContent); }

            var memberForMatches = Regex.Matches(memberContent, @"\[MemberFor\(typeof\((?<type>\w+)\)\)\]");
            if (memberForMatches.Count > 1) { throw new FormatException("Failed to parse memberfor of member: " + memberContent); }
            
            return new MemberParseResult
            {
                Body = memberContent,
                Name = nameMatches[0].Groups["name"].Value,
                MemberFor = memberForMatches.Count == 0 
                    ? typeof(long)
                    : FromAlias(memberForMatches[0].Groups["type"].Value)
            };
        }

        private static Type FromAlias(string alias)
        {
            switch (alias)
            {
                case "sbyte": return typeof(sbyte);
                case "byte": return typeof(byte);
                case "short": return typeof(short);
                case "ushort": return typeof(ushort);
                case "int": return typeof(int);
                case "uint": return typeof(uint);
                case "long": return typeof(long);
                case "ulong": return typeof(ulong);
                default: throw new ArgumentException(alias);
            }
        }

        private class FileParseResult
        {
            public string Header { get; set; }
            public string Footer { get; set; }
            public List<MemberParseResult> Members { get; set; }
        }

        private class MemberParseResult
        {
            public string Body { get; set; }
            public string Name { get; set; }
            public Type MemberFor { get; set; }
        }
    }
}
