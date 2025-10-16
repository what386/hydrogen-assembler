namespace Assembler.Models.Formats;

using System.Collections.Immutable;

public static class NameTable
{
    public static ImmutableDictionary<string, string> BranchConditions{ get; }
    public static ImmutableDictionary<string, string> Settings { get; }
    public static ImmutableDictionary<string, string> SpecialRegisters{ get; }

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

        Settings = new Dictionary<string, string>
        {
            ["altb"] = "000", // alt branch conditions
            ["ainc"] = "001", // auto-increment mode
            ["lsrc"] = "010", // loop source
            ["lcnt"] = "011", // loop count
            ["pgsw"] = "100", // memory page swap
            ["pgjm"] = "101", // page jump mode
            ["imsk"] = "110", // interrupt mask
            ["intr"] = "111", // call interrupt
        }.ToImmutableDictionary();
        
        SpecialRegisters = new Dictionary<string, string>
        {
            ["ap"] = "000", // address pointer
            ["sp"] = "001", // stack pointer
            ["sw"] = "010", // status word
            ["cw"] = "011", // control word 
            ["lp"] = "100", // loop pointer
            ["bo"] = "101", // branch offset
            ["pcl"] = "110", // program counter low
            ["pch"] = "111", // program counter high
        }.ToImmutableDictionary();
    }

}
