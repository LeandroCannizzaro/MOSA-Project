// Copyright (c) MOSA Project. Licensed under the New BSD License.

// This code was generated by an automated template.

using Mosa.Compiler.Framework;

namespace Mosa.Platform.x86.Instructions
{
	/// <summary>
	/// Roundsd
	/// </summary>
	/// <seealso cref="Mosa.Platform.x86.X86Instruction" />
	public sealed class Roundsd : X86Instruction
	{
		public static readonly LegacyOpCode LegacyOpcode = new LegacyOpCode(new byte[] { 0x66, 0x0F, 0x3A, 0x0B } );

		public Roundsd()
			: base(1, 2)
		{
		}

		internal override void EmitLegacy(InstructionNode node, X86CodeEmitter emitter)
		{
			System.Diagnostics.Debug.Assert(node.ResultCount == DefaultResultCount);
			System.Diagnostics.Debug.Assert(node.OperandCount == DefaultOperandCount);
			System.Diagnostics.Debug.Assert(node.Result == node.Operand1);
			System.Diagnostics.Debug.Assert(node.Result.IsCPURegister);
			System.Diagnostics.Debug.Assert(node.Operand1.IsCPURegister);

			emitter.Emit(LegacyOpcode, node.Result, node.Operand1, node.Operand2);
		}

		// The following is used by the automated code generator.

		public override LegacyOpCode __legacyopcode { get { return LegacyOpcode; } }

		public override string __legacyOpcodeOperandOrder { get { return "r12"; } }
	}
}

