namespace Assembler.Models.Formats;

using System.Collections.Immutable;

public static class NameTable
{
    public static ImmutableDictionary<string, string> BranchConditions{ get; }
    public static ImmutableDictionary<string, string> Settings { get; }
    public static ImmutableDictionary<string, string> Commands { get; }

    const string UNUSED = "";

    static NameTable()
    {
        BranchConditions = new Dictionary<string, string>
        {
            // standard branchConditions
            ["eq"] = "000", // equal (z=1)
            ["ne"] = "001", // not equal (z=0)
            ["cs"] = "010", ["hs"] = "010", // carry set / higher same (c=1)
            ["cc"] = "011", ["lo"] = "010", // carry clear / lower (c=0)
            ["hi"] = "100", // higher (c=1 && z=0)
            ["ls"] = "101", // lower same (c=0 || z=1)
            ["hc"] = "110", // half carry (h) 
            ["al"] = "111", ["tr"] = "111", // always, true 

            // alternate branchconditions
            ["vs"] = "000", // overflow set (v=1)
            ["vc"] = "001", // overflow clear (v=0)
            ["ge"] = "010", // greater equal (n=v)
            ["lt"] = "011", // less than (n≠v)
            ["gt"] = "100", // greater (z=0 && n=v)
            ["le"] = "101", // less equal (z=1 || n≠v)
            ["mi"] = "110", // minus / negative (n=1)
            ["pl"] = "111", // plus / positive (n=0)
        }.ToImmutableDictionary();

        Commands = new Dictionary<string, string>
        {
            ["noop"] = "000", // no-op
            ["halt"] = "001", // halt
            ["word"] = "010", // status / ctrl word
            ["pc"] = "011", // program counter
            ["intv"] = "100", // interrupt vector
            [UNUSED] = "101",
            ["drq"] = "110", // dma request
            ["irq"] = "111", // interrupt request
        }.ToImmutableDictionary();

        Settings = new Dictionary<string, string>
        {
            ["ctrl"] = "000", // control word
            ["stat"] = "001", // status word
            [UNUSED] = "010",
            ["bank"] = "011", // active bank
            [UNUSED] = "100",
            [UNUSED] = "101",
            ["dmam"] = "110", // dma mask
            ["intm"] = "111", // interrupt mask
        }.ToImmutableDictionary();

    }
}
