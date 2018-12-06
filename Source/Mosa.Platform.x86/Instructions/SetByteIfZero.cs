// Copyright (c) MOSA Project. Licensed under the New BSD License.

// This code was generated by an automated template.

using Mosa.Compiler.Framework;

namespace Mosa.Platform.x86.Instructions
{
	/// <summary>
	/// SetByteIfZero
	/// </summary>
	/// <seealso cref="Mosa.Platform.x86.X86Instruction" />
	public sealed class SetByteIfZero : X86Instruction
	{
		public override int ID { get { return 333; } }

		internal SetByteIfZero()
			: base(1, 0)
		{
		}

		public override string AlternativeName { get { return "SetZ"; } }

		public override bool IsZeroFlagUsed { get { return true; } }

		public override BaseInstruction GetOpposite()
		{
			return X86.SetByteIfNotZero;
		}

		public override void Emit(InstructionNode node, BaseCodeEmitter emitter)
		{
			System.Diagnostics.Debug.Assert(node.ResultCount == 1);
			System.Diagnostics.Debug.Assert(node.OperandCount == 0);

			emitter.OpcodeEncoder.AppendByte(0x0F);
			emitter.OpcodeEncoder.AppendByte(0x94);
			emitter.OpcodeEncoder.Append2Bits(0b11);
			emitter.OpcodeEncoder.Append3Bits(0b000);
			emitter.OpcodeEncoder.Append3Bits(node.Result.Register.RegisterCode);
		}
	}
}
