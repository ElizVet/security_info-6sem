using System.Collections.Generic;

namespace Service.Constants
{
    public static class Constants
    {
        public static readonly Dictionary<char, string> RussianCharactersToCodes = new Dictionary<char, string>()
        {
            {'а', "00000"},
            {'б', "00001"},
            {'в', "00010"},
            {'г', "00011"},
            {'д', "00100"},
            {'е', "00101"},
            {'ж', "00110"},
            {'з', "00111"},
            {'и', "01000"},
            {'й', "01001"},
            {'к', "01010"},
            {'л', "01011"},
            {'м', "01100"},
            {'н', "01101"},
            {'о', "01110"},
            {'п', "01111"},
            {'р', "10000"},
            {'с', "10001"},
            {'т', "10010"},
            {'у', "10011"},
            {'ф', "10100"},
            {'х', "10101"},
            {'ц', "10110"},
            {'ч', "10111"},
            {'ш', "11000"},
            {'щ', "11001"},
            {'ъ', "11010"},
            {'ы', "11011"},
            {'ь', "11100"},
            {'э', "11101"},
            {'ю', "11110"},
            {'я', "11111"}
        };
        
        public static readonly Dictionary<char, char> EnglishToRussianCharacters = new Dictionary<char, char>()
        {
            {'a', 'а'},
            {'e', 'е'},
            {'o', 'о'},
            {'p', 'р'},
            {'c', 'с'},
            {'y', 'у'},
            {'x', 'х'}
        };
        
        public const int CodeWidth = 5;
        public const string DefaultTargetFilePath = @"../../../TextFiles/result.txt";
        public const string PathToTextFolder = @"../../../TextFiles/";
    }
}