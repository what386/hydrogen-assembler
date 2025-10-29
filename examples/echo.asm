#define handle_keyboard_address .kb_int

PAGE0:
    ldi r0, !0x08 
    sys $sint, handle_keyboard_address
loop:
    nop
    jmp .loop
kb_int:
    psh r1, !0
    inp r1, @0
    out @0, r1
    pop r1, !0
    iret

