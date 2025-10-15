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
            ["EQ"] = "000", // Equal (Z=1)
            ["NE"] = "001", // Not Equal (Z=0)
            ["CS"] = "010", ["HS"] = "010", // Carry Set / Higher Same (C=1)
            ["CC"] = "011", ["LO"] = "010", // Carry Clear / Lower (C=0)
            ["HI"] = "100", // Higher (C=1 && Z=0)
            ["LS"] = "101", // Lower Same (C=0 || Z=1)
            ["HC"] = "110", // Half Carry (H) 
            ["AL"] = "111", ["TR"] = "111", // Always, True 

            // alternate branchConditions
            ["VS"] = "000", // Overflow Set (V=1)
            ["VC"] = "001", // Overflow Clear (V=0)
            ["GE"] = "010", // Greater Equal (N=V)
            ["LT"] = "011", // Less Than (N≠V)
            ["GT"] = "100", // Greater (Z=0 && N=V)
            ["LE"] = "101", // Less Equal (Z=1 || N≠V)
            ["MI"] = "110", // Minus / Negative (N=1)
            ["PL"] = "111", // Plus / Positive (N=0)
        }.ToImmutableDictionary();

        Settings = new Dictionary<string, string>
        {
            ["ALTB"] = "000", // Alt branch conditions
            ["AINC"] = "001", // Auto-increment mode
            ["LSRC"] = "010", // Loop source
            ["LCNT"] = "011", // Loop count
            ["PGSW"] = "100", // Memory page swap
            ["PGJM"] = "101", // Page Jump mode
            ["IMSK"] = "110", // Interrupt mask
            ["INTR"] = "111", // Call interrupt
        }.ToImmutableDictionary();
        
        SpecialRegisters = new Dictionary<string, string>
        {
            ["AP"] = "000", // Address Pointer
            ["SP"] = "001", // Stack Pointer
            ["SW"] = "010", // Status Word
            ["CW"] = "011", // Control Word 
            ["LP"] = "100", // Loop Pointer
            ["BO"] = "101", // Branch Offset
            ["PCL"] = "110", // Program Counter Low
            ["PCH"] = "111", // Program Counter High
        }.ToImmutableDictionary();
    }

}
