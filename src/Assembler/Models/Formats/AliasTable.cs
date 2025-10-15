namespace Assembler.Models.Formats;

using System.Collections.Immutable;

public static class AliasTable 
{
    public static ImmutableDictionary<string, (string, string)> InstructionAliases{ get; }

    static AliasTable()
    {
        InstructionAliases = new Dictionary<string, (string, string)>
        {
            ["exit"] = ("hlt", "1"),

            ["bra"] = ("bra", "11"),
            ["brn"] = ("bra", "01"),
            ["brt"] = ("bra", "10"),
            ["brp"] = ("bra", "11"),

            ["pop"] = ("pop", "00"),
            ["peek"] = ("pop", "01"),
            ["popf"] = ("pop", "10"),
            ["dsp"] = ("pop", "11"),

            ["psh"] = ("psh", "00"),
            ["poke"] = ("psh", "01"),
            ["pshf"] = ("psh", "10"),
            ["dsp"] = ("psh", "11"),

            ["mov"] = ("mov", "00"),
            ["cpy"] = ("cpy", "01"),
            ["swp"] = ("swp", "10"),

            ["add"] = ("add", "00"),
            ["adc"] = ("add", "01"),
            ["adv"] = ("add", "10"),
            ["advc"] = ("add", "11"),

            ["sub"] = ("sub", "00"),
            ["sbb"] = ("sub", "01"),
            ["sbv"] = ("sub", "10"),
            ["sbvb"] = ("sub", "11"),

            ["and"] = ("bit", "00"),
            ["or"] = ("bit", "01"),
            ["xor"] = ("bit", "10"),
            ["imp"] = ("bit", "11"),

            ["nand"] = ("bnt", "00"),
            ["nor"] = ("bnt", "01"),
            ["xnor"] = ("bnt", "10"),
            ["nimp"] = ("bnt", "11"),

            ["bsl"] = ("bsh", "00"),
            ["bsr"] = ("bsh", "01"),
            ["ror"] = ("bsh", "10"),
            ["bsxr"] = ("bsh", "11"),

            ["bsli"] = ("bsi", "00"),
            ["bsri"] = ("bsi", "01"),
            ["rori"] = ("bsi", "10"),
            ["bsxri"] = ("bsi", "11"),

            ["mul"] = ("mdo", "00"),
            ["mulu"] = ("mdo", "01"),
            ["div"] = ("mdo", "10"),
            ["mod"] = ("mdo", "11"),

            ["sqrt"] = ("btc", "00"),
            ["clz"] = ("btc", "01"),
            ["ctz"] = ("btc", "10"),
            ["cto"] = ("btc", "11"),
        }.ToImmutableDictionary();
    }
}
