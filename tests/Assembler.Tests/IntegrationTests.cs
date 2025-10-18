using Xunit;
using Assembler.Core;

namespace Assembler.Tests;

public class IntegrationTests
{
    string[] instructions = 
    {
        "nop",
        "exit",
        "adi r7 !4",
        "nano r7 r1 r3",
        "brt ?eq !20",
        "jmp !2047"
    };

    string[] expectedBinary =
    {
        "0000000000000000",
        "0000100100000000",
        "1001011100000100",
        "1101111100101011",
        "0010100010010100",
        "0010011111111111"
    };
}
