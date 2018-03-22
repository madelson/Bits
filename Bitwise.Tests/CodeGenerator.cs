using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
            GenerateCodeForAlternateNumericTypes(Path.Combine(testBinDirectory, @"..\..\..\BitsTest.cs"));
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
                var typePath = Path.Combine(Path.GetDirectoryName(path), "Generated", $"{Path.GetFileNameWithoutExtension(path)}.{type.Name}{Path.GetExtension(path)}");
                using (var writer = new StringWriter())
                {
                    writer.WriteLine("//");
                    writer.WriteLine("// AUTO-GENERATED");
                    writer.WriteLine("//");
                    writer.Write(parsed.Header);

                    foreach (var memberGroup in parsed.Members.GroupBy(m => m.Name))
                    {
                        if (!memberGroup.Any(m => m.MemberFor == type))
                        {
                            // must reference a type larger than yourself (since typically we specify int64 and special case smaller types
                            var referenceMember = memberGroup.Where(m => Marshal.SizeOf(m.MemberFor) >= Marshal.SizeOf(type))
                                // prefer to reference a member with the same signed-ness
                                .OrderByDescending(m => IsUnsigned(type) == IsUnsigned(m.MemberFor))
                                // break ties by referencing the member closest in size
                                .ThenBy(m => Marshal.SizeOf(m.MemberFor) - Marshal.SizeOf(type))
                                .First();
                            Console.WriteLine($"For member {memberGroup.Key} and type {type} choosing reference member {referenceMember.MemberFor}");
                            var typeMember = Regex.Replace(
                                referenceMember.Body,
                                $@"(?<alias>{ToAlias(referenceMember.MemberFor)})|(?<name>{referenceMember.MemberFor.Name})"
                                    + $@"|(?<signedAlias>{ToAlias(GetSignedVariant(referenceMember.MemberFor))})",
                                m => m.Groups["alias"].Success ? ToAlias(type)
                                    : m.Groups["name"].Success ? type.Name
                                    : m.Groups["signedAlias"].Success ? ToAlias(GetSignedVariant(type))
                                    : throw new InvalidOperationException(m.Value)
                            );

                            writer.Write(typeMember);
                        }
                    }

                    writer.Write(parsed.Footer);

                    var result = writer.ToString();
                    if (!File.Exists(typePath) || File.ReadAllText(typePath) != result)
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(typePath));
                        File.WriteAllText(typePath, result);
                    }
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
            var memberFor = memberForMatches.Count == 0
                ? typeof(long)
                : FromAlias(memberForMatches[0].Groups["type"].Value);

            var extractedName = nameMatches[0].Groups["name"].Value;

            return new MemberParseResult
            {
                Body = memberContent,
                // ReverseBytes does not get the base name "Reverses for the byte type
                Name = extractedName != nameof(Bits.ReverseBytes)
                    ? extractedName.Replace(memberFor.Name, string.Empty)
                    : extractedName,
                MemberFor = memberFor
            };
        }

        internal static bool IsUnsigned(Type type) => GetSignedVariant(type) != type;

        private static Type GetSignedVariant(Type type) => type.Name.StartsWith("U") ? Type.GetType($"{type.Namespace}.{type.Name.Substring(1)}", throwOnError: true)
            : type == typeof(byte) ? typeof(sbyte)
            : type;

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

        private static string ToAlias(Type type)
        {
            switch (type.Name)
            {
                case nameof(SByte): return "sbyte";
                case nameof(Byte): return "byte";
                case nameof(Int16): return "short";
                case nameof(UInt16): return "ushort";
                case nameof(Int32): return "int";
                case nameof(UInt32): return "uint";
                case nameof(Int64): return "long";
                case nameof(UInt64): return "ulong";
                default: throw new ArgumentException(type.ToString());
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
