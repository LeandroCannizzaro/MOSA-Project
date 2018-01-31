﻿// Copyright (c) MOSA Project. Licensed under the New BSD License.

// This code was generated by an automated template.

using System.Collections.Generic;

namespace Mosa.Platform.x86
{
	/// <summary>
	/// X86 Instruction Map
	/// </summary>
	public static class X86InstructionMap
	{
		public static readonly Dictionary<string, X86Instruction> Map = new Dictionary<string, X86Instruction>() {
			{ "Adc32", X86.Adc32 },
			{ "AdcConst32", X86.AdcConst32 },
			{ "Add32", X86.Add32 },
			{ "AddConst32", X86.AddConst32 },
			{ "Addsd", X86.Addsd },
			{ "Addss", X86.Addss },
			{ "And32", X86.And32 },
			{ "AndConst32", X86.AndConst32 },
			{ "Branch", X86.Branch },
			{ "Break", X86.Break },
			{ "Btr32", X86.Btr32 },
			{ "BtrConst32", X86.BtrConst32 },
			{ "Bts32", X86.Bts32 },
			{ "BtsConst32", X86.BtsConst32 },
			{ "Call", X86.Call },
			{ "Cdq", X86.Cdq },
			{ "Cli", X86.Cli },
			{ "Cmovcc", X86.Cmovcc },
			{ "Cmp", X86.Cmp },
			{ "CmpXchgLoad32", X86.CmpXchgLoad32 },
			{ "Comisd", X86.Comisd },
			{ "Comiss", X86.Comiss },
			{ "CpuId", X86.CpuId },
			{ "Cvtsd2ss", X86.Cvtsd2ss },
			{ "Cvtsi2sd", X86.Cvtsi2sd },
			{ "Cvtsi2ss", X86.Cvtsi2ss },
			{ "Cvtss2sd", X86.Cvtss2sd },
			{ "Cvttsd2si", X86.Cvttsd2si },
			{ "Cvttss2si", X86.Cvttss2si },
			{ "Dec", X86.Dec },
			{ "Div", X86.Div },
			{ "Divsd", X86.Divsd },
			{ "Divss", X86.Divss },
			{ "FarJmp", X86.FarJmp },
			{ "Hlt", X86.Hlt },
			{ "IDiv", X86.IDiv },
			{ "IMul", X86.IMul },
			{ "In", X86.In },
			{ "Inc", X86.Inc },
			{ "Int", X86.Int },
			{ "Invlpg", X86.Invlpg },
			{ "IRetd", X86.IRetd },
			{ "Jmp", X86.Jmp },
			{ "Lea", X86.Lea },
			{ "Leave", X86.Leave },
			{ "Lgdt", X86.Lgdt },
			{ "Lidt", X86.Lidt },
			{ "Lock", X86.Lock },
			{ "Mov", X86.Mov },
			{ "Movaps", X86.Movaps },
			{ "MovapsLoad", X86.MovapsLoad },
			{ "MovCRLoad", X86.MovCRLoad },
			{ "MovCRStore", X86.MovCRStore },
			{ "Movd", X86.Movd },
			{ "MovLoad", X86.MovLoad },
			{ "Movsd", X86.Movsd },
			{ "MovsdLoad", X86.MovsdLoad },
			{ "MovsdStore", X86.MovsdStore },
			{ "Movss", X86.Movss },
			{ "MovssLoad", X86.MovssLoad },
			{ "MovssStore", X86.MovssStore },
			{ "MovStore", X86.MovStore },
			{ "Movsx", X86.Movsx },
			{ "MovsxLoad", X86.MovsxLoad },
			{ "Movups", X86.Movups },
			{ "MovupsLoad", X86.MovupsLoad },
			{ "MovupsStore", X86.MovupsStore },
			{ "Movzx", X86.Movzx },
			{ "MovzxLoad", X86.MovzxLoad },
			{ "Mul", X86.Mul },
			{ "Mulsd", X86.Mulsd },
			{ "Mulss", X86.Mulss },
			{ "Neg", X86.Neg },
			{ "Nop", X86.Nop },
			{ "Not", X86.Not },
			{ "Or", X86.Or },
			{ "Out", X86.Out },
			{ "Pause", X86.Pause },
			{ "Pextrd", X86.Pextrd },
			{ "Pop", X86.Pop },
			{ "Popad", X86.Popad },
			{ "Push", X86.Push },
			{ "Pushad", X86.Pushad },
			{ "PXor", X86.PXor },
			{ "Rcr", X86.Rcr },
			{ "Rep", X86.Rep },
			{ "Ret", X86.Ret },
			{ "Roundsd", X86.Roundsd },
			{ "Roundss", X86.Roundss },
			{ "Sar", X86.Sar },
			{ "Sbb", X86.Sbb },
			{ "Setcc", X86.Setcc },
			{ "Shl", X86.Shl },
			{ "Shld32", X86.Shld32 },
			{ "ShldConst32", X86.ShldConst32 },
			{ "Shr", X86.Shr },
			{ "Shrd32", X86.Shrd32 },
			{ "ShrdConst32", X86.ShrdConst32 },
			{ "Sti", X86.Sti },
			{ "Stos", X86.Stos },
			{ "Sub32", X86.Sub32 },
			{ "SubConst32", X86.SubConst32 },
			{ "Subsd", X86.Subsd },
			{ "Subss", X86.Subss },
			{ "Test32", X86.Test32 },
			{ "TestConst32", X86.TestConst32 },
			{ "Ucomisd", X86.Ucomisd },
			{ "Ucomiss", X86.Ucomiss },
			{ "Xchg32", X86.Xchg32 },
			{ "Xor32", X86.Xor32 },
			{ "XorConst32", X86.XorConst32 },
			{ "BranchOverflow", X86.BranchOverflow },
			{ "BranchNoOverflow", X86.BranchNoOverflow },
			{ "BranchCarry", X86.BranchCarry },
			{ "BranchUnsignedLessThan", X86.BranchUnsignedLessThan },
			{ "BranchUnsignedGreaterOrEqual", X86.BranchUnsignedGreaterOrEqual },
			{ "BranchNoCarry", X86.BranchNoCarry },
			{ "BranchEqual", X86.BranchEqual },
			{ "BranchZero", X86.BranchZero },
			{ "BranchNotEqual", X86.BranchNotEqual },
			{ "BranchNotZero", X86.BranchNotZero },
			{ "BranchUnsignedLessOrEqual", X86.BranchUnsignedLessOrEqual },
			{ "BranchUnsignedGreaterThan", X86.BranchUnsignedGreaterThan },
			{ "BranchSigned", X86.BranchSigned },
			{ "BranchNotSigned", X86.BranchNotSigned },
			{ "BranchParity", X86.BranchParity },
			{ "BranchNoParity", X86.BranchNoParity },
			{ "BranchLessThan", X86.BranchLessThan },
			{ "BranchGreaterOrEqual", X86.BranchGreaterOrEqual },
			{ "BranchLessOrEqual", X86.BranchLessOrEqual },
			{ "BranchGreaterThan", X86.BranchGreaterThan },
		};
	}
}
