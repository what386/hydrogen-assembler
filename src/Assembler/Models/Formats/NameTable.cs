namespace Assembler.Models.Formats;

using System.Collections.Immutable;

public static class NameTable
{
    public static ImmutableDictionary<string, string> BranchConditions{ get; }
    public static ImmutableDictionary<string, string> Settings { get; }

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
            ["ctrl"] = "000", // control word
            ["stat"] = "001", // status word
            ["tmrl"] = "010", // timer lower
            ["tmru"] = "011", // timer upper
            ["bank"] = "100", // bank swap
            ["sint"] = "101", // set interrupt
            ["imsk"] = "110", // interrupt mask
            ["intr"] = "111", // call interrupt
        }.ToImmutableDictionary();
    }
}
