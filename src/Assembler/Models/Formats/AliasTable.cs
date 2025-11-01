namespace Assembler.Models.Formats;

using System.Collections.Immutable;

public static class AliasTable 
{
    public static ImmutableDictionary<string, (string Target, string Arg)> InstructionAliases{ get; }

    static AliasTable()
    {
        InstructionAliases = new Dictionary<string, (string Target, string Arg)>
        {
            ["nop"] = ("hlt", "00"),
            ["yld"] = ("hlt", "01"),
            ["wait"] = ("hlt", "10"),
            ["hlt"] = ("hlt", "11"),

            ["brn"] = ("bra", "00"),
            ["brt"] = ("bra", "01"),
            ["brnp"] = ("bra", "10"),
            ["brtp"] = ("bra", "11"),

            ["ret"] = ("ret", "00"),
            ["skp"] = ("ret", "01"),
            ["brk"] = ("ret", "10"),
            ["iret"] = ("ret", "11"),

            ["pop"] = ("pop", "00"),
            ["peek"] = ("pop", "01"),
            ["popf"] = ("pop", "10"),
            ["dsp"] = ("pop", "11"),

            ["psh"] = ("psh", "00"),
            ["poke"] = ("psh", "01"),
            ["pshf"] = ("psh", "10"),
            ["isp"] = ("psh", "11"),

            ["instr"] = ("inst", "00"),
            ["instw"] = ("inst", "01"),

            ["blit-cpy"] = ("blit", "00"),
            ["blit-or"] = ("blit", "01"),
            ["blit-and"] = ("blit", "10"),
            ["blit-xor"] = ("blit", "11"),

            ["mov"] = ("mov", "00"),
            ["cpy"] = ("mov", "01"),
            ["swp"] = ("mov", "10"),
            ["nbl"] = ("mov", "11"),

            ["cmov"] = ("cmov", "00"),
            ["ccpy"] = ("cmov", "01"),
            ["cswp"] = ("cmov", "10"),
            ["cnbl"] = ("cmov", "11"),

            ["add"] = ("add", "00"),
            ["adc"] = ("add", "01"),
            ["adv"] = ("add", "10"),
            ["advc"] = ("add", "11"),

            ["sub"] = ("sub", "00"),
            ["sbb"] = ("sub", "01"),
            ["sbv"] = ("sub", "10"),
            ["sbvb"] = ("sub", "11"),

            ["or"] = ("bit", "00"),
            ["and"] = ("bit", "01"),
            ["xor"] = ("bit", "10"),
            ["imp"] = ("bit", "11"),

            ["nor"] = ("bnt", "00"),
            ["nand"] = ("bnt", "01"),
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
