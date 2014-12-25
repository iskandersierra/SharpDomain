using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Magnum.Monads.Parser;

namespace SharpDomain.Reflection
{
    public static class ExtensionsOfType
    {
        #region [ GetTypeName ]
        public static string GetTypeName(this Type type, bool fullName = false, bool includeGenerics = false)
        {
            if (type == null) throw new ArgumentNullException("type");

            string name;
            if (CSharpBuiltInTypeNames.TryGetValue(type, out name))
                return name;

            if (type.IsGenericParameter)
            {
                return type.Name;
            }

            var sb = new StringBuilder();
            // Declaring type
            if (type.DeclaringType != null)
            {
                sb.Append(type.DeclaringType.GetTypeName(fullName, includeGenerics)).Append(".");
            }

            // Namespace
            if (type.DeclaringType == null && fullName)
            {
                if (!string.IsNullOrWhiteSpace(type.Namespace))
                    sb.Append(type.Namespace).Append(".");
            }

            if (type.IsGenericType || type.IsGenericTypeDefinition)
            {
                name = type.Name.Substring(0, type.Name.IndexOf('`'));
                sb.Append(name);
                if (includeGenerics)
                {
                    sb.Append("<");
                    var first = true;
                    foreach (var argument in type.GetGenericArguments())
                    {
                        if (!first) sb.Append(", ");
                        sb.Append(argument.GetTypeName(fullName, true));
                        first = false;
                    }
                    sb.Append(">");
                }
            }
            else
            {
                sb.Append(type.Name);
            }

            return sb.ToString();
        }

        private static readonly Dictionary<Type, string> CSharpBuiltInTypeNames = new Dictionary<Type, string>
        {
            { typeof(bool), "bool" },
            { typeof(byte), "byte" },
            { typeof(sbyte), "sbyte" },
            { typeof(char), "char" },
            { typeof(decimal), "decimal" },
            { typeof(double), "double" },
            { typeof(float), "float" },
            { typeof(int), "int" },
            { typeof(uint), "uint" },
            { typeof(long), "long" },
            { typeof(ulong), "ulong" },
            { typeof(object), "object" },
            { typeof(short), "short" },
            { typeof(ushort), "ushort" },
            { typeof(string), "string" },
        };
        #endregion [ GetTypeName ]

        #region [ CSharpName ]
        public static bool IsValidCSharpName(this string identifier, bool allowDots = false, bool avoidKeywords = true, bool avoidContextualKeywords = false)
        {
            if (string.IsNullOrWhiteSpace(identifier)) return false;
            var regex = allowDots ? ValidQualifiedCSharpIdentifierRegex : ValidCSharpIdentifierRegex;
            if (!regex.IsMatch(identifier)) return false;
            if (avoidKeywords && IsCSharpKeyword(identifier)) return false;
            if (avoidContextualKeywords && IsCSharpContextualKeyword(identifier)) return false;
            return true;
        }

        public static void ValidateCSharpName(this string identifier, bool allowDots = false, bool avoidKeywords = true, bool avoidContextualKeywords = false)
        {
            if (string.IsNullOrWhiteSpace(identifier)) 
                throw new ArgumentNullException("identifier");
            var regex = allowDots ? ValidQualifiedCSharpIdentifierRegex : ValidCSharpIdentifierRegex;
            if (!regex.IsMatch(identifier)) 
                throw new ArgumentException(string.Format("'{0}' is an invalid C# identifier", identifier));
            if (avoidKeywords && IsCSharpKeyword(identifier)) 
                throw new ArgumentException(string.Format("Identifier '{0}' is a C# keyword", identifier));
            if (avoidContextualKeywords && IsCSharpContextualKeyword(identifier))
                throw new ArgumentException(string.Format("Identifier '{0}' is a C# contextual keyword", identifier));
        }

        public static string TurnToCSharpIdentifier(
            this string identifier, 
            bool allowDots = false, 
            bool avoidKeywords = true, 
            bool avoidContextualKeywords = false, 
            bool removeAlienCharacters = true, 
            bool removeSpaces = true,
            bool collapseToOne = true,
            char collapsingChar = '_',
            bool trimCollapsingChar = false)
        {
            if (string.IsNullOrEmpty(identifier))
                return collapsingChar.ToString(CultureInfo.InvariantCulture);

            var regex = allowDots ? ValidQualifiedCSharpIdentifierRegex : ValidCSharpIdentifierRegex;
            if (regex.IsMatch(identifier)) return identifier;

            var sb = new StringBuilder();

            var length = identifier.Length;
            var isFirstCharInWord = true;

            for (int i = 0; i < length; i++)
            {
                var ch = identifier[i];
                var chToStr = ch.ToString(CultureInfo.InvariantCulture);
                var charRegex = isFirstCharInWord ? IdentifierStartCharacterRegex : IdentifierPartCharacterRegex;
                var isDot = ch == '.' && allowDots;
                var isWS = WhitespaceCharacterRegex.IsMatch(chToStr);
                var isAlien = !isDot && !isWS && !charRegex.IsMatch(chToStr);
                if (isDot)
                {
                    if (sb.Length > 0 && sb[sb.Length - 1] != '.')
                    {
                        sb.Append('.');
                        isFirstCharInWord = true;
                    }
                }
                else if (isWS)
                {
                    if (!removeSpaces && (!collapseToOne || sb.Length == 0 || sb[sb.Length - 1] != collapsingChar))
                    {
                        sb.Append(collapsingChar);
                        isFirstCharInWord = true;
                    }
                }
                else if (isAlien)
                {
                    if (!removeAlienCharacters && (!collapseToOne || sb.Length == 0 || sb[sb.Length - 1] != collapsingChar))
                    {
                        sb.Append(collapsingChar);
                        isFirstCharInWord = true;
                    }
                }
                else
                {
                    sb.Append(ch);
                    isFirstCharInWord = false;
                }
            }

            var result = sb.ToString();

            if (trimCollapsingChar) result = result.Trim(collapsingChar);
            if (avoidKeywords && IsCSharpKeyword(result) || 
                avoidContextualKeywords && IsCSharpContextualKeyword(result) || 
                result.Length == 0)
                result = collapsingChar + result;

            return result;
        }

        public static string[] GetCSharpKeywords()
        {
            return CSharpKeywordsArray.ToArray();
        }

        public static bool IsCSharpKeyword(this string identifier)
        {
            return CSharpKeywordsSet.Contains(identifier);
        }

        public static string[] GetCSharpContextualKeywords()
        {
            return CSharpContextualKeywordsArray.ToArray();
        }

        public static bool IsCSharpContextualKeyword(this string identifier)
        {
            return CSharpContextualKeywordsSet.Contains(identifier);
        }

        private const string FormattingCharacter   = @"\p{Cf}";
        private const string ConnectingCharacter   = @"\p{Pc}";
        private const string DecimalDigitCharacter = @"\p{Nd}";
        private const string CombiningCharacter    = @"\p{Mn}\p{Mc}";
        private const string LetterCharacter       = @"\p{Lu}\p{Ll}\p{Lt}\p{Lm}\p{Lo}\p{Nl}";
        
        private const string IdentifierPartCharacter = "[" + LetterCharacter + DecimalDigitCharacter + ConnectingCharacter + CombiningCharacter + FormattingCharacter + "]";
        private const string IdentifierStartCharacter = "[" + LetterCharacter + "_]";
        private const string IdentifierPartCharacters = "(" + IdentifierPartCharacter + ")+";

        private const string IdentifierOrKeyword = IdentifierStartCharacter + "(" + IdentifierPartCharacters + ")*";
        private const string QualifiedIdentifierOrKeyword = IdentifierOrKeyword + @"(\." + IdentifierOrKeyword + @")*";

        private static readonly Regex WhitespaceCharacterRegex = new Regex(@"\s", RegexOptions.Compiled);
        private static readonly Regex IdentifierStartCharacterRegex = new Regex(IdentifierStartCharacter, RegexOptions.Compiled);
        private static readonly Regex IdentifierPartCharacterRegex = new Regex(IdentifierPartCharacter, RegexOptions.Compiled);
        private static readonly Regex ValidCSharpIdentifierRegex = new Regex("^" + IdentifierOrKeyword + "$", RegexOptions.Compiled);
        private static readonly Regex ValidQualifiedCSharpIdentifierRegex = new Regex("^" + QualifiedIdentifierOrKeyword + "$", RegexOptions.Compiled);


        // Thanks to https://gist.github.com/LordDawnhunter/5245476
        // Updated from http://msdn.microsoft.com/en-us/library/x53a06bb(v=vs.120).aspx
        private static readonly string[] CSharpKeywordsArray = new[]
            {
                "abstract", "as", "base", "bool", "break", "byte", "case", "catch", "char", "checked", "class",
                "const", "continue", "decimal", "default", "delegate", "do", "double", "else", "enum", "event",
                "explicit", "extern", "false", "finally", "fixed", "float", "for", "foreach", "goto", "if",
                "implicit", "in", "int", "interface", "internal", "is", "lock", "long", "namespace", "new",
                "null", "object", "operator", "out", "override", "params", "private", "protected", "public",
                "readonly", "ref", "return", "sbyte", "sealed", "short", "sizeof", "stackalloc", "static",
                "string", "struct", "switch", "this", "throw", "true", "try", "typeof", "uint", "ulong",
                "unchecked", "unsafe", "ushort", "using", "virtual", "void", "volatile", "while",
            };
        private static readonly HashSet<string> CSharpKeywordsSet = new HashSet<string>(CSharpKeywordsArray);


        private static readonly string[] CSharpContextualKeywordsArray = new[]
            {
                "add", "alias", "ascending", "async", "await", "descending", "dynamic", "from", "get", 
                "global", "group", "into", "join", "let", "orderby", "partial", "remove", "select", "set",
                "value", "var", "where", "yield",
            };
        private static readonly HashSet<string> CSharpContextualKeywordsSet = new HashSet<string>(CSharpContextualKeywordsArray);
        #endregion [ CSharpName ]
    }
}
