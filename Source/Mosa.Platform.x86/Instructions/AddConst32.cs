// Copyright (c) MOSA Project. Licensed under the New BSD License.

// This code was generated by an automated template.

using Mosa.Compiler.Framework;

namespace Mosa.Platform.x86.Instructions
{
	/// <summary>
	/// AddConst32
	/// </summary>
	/// <seealso cref="Mosa.Platform.x86.X86Instruction" />
	public sealed class AddConst32 : X86Instruction
	{
		public static readonly LegacyOpCode LegacyOpcode = new LegacyOpCode(new byte[] { 0x81 } , 0x00);

		public AddConst32()
			: base(1, 2)
		{
		}

		public override bool IsCommutative { get { return true; } }

		internal override void EmitLegacy(InstructionNode node, X86CodeEmitter emitter)
		{
			System.Diagnostics.Debug.Assert(node.ResultCount == DefaultResultCount);
			System.Diagnostics.Debug.Assert(node.OperandCount == DefaultOperandCount);
			System.Diagnostics.Debug.Assert(node.Result == node.Operand1);
			System.Diagnostics.Debug.Assert(node.Result.IsCPURegister);
			System.Diagnostics.Debug.Assert(node.Operand1.IsCPURegister);

			emitter.Emit(LegacyOpcode, node.Result, node.Operand2);
		}

		// The following is used by the automated code generator.

		public override LegacyOpCode __legacyopcode { get { return LegacyOpcode; } }

		public override string __legacyOpcodeOperandOrder { get { return "r2"; } }
	}
}

