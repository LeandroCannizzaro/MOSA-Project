// Copyright (c) MOSA Project. Licensed under the New BSD License.

// This code was generated by an automated template.

using Mosa.Compiler.Framework;

namespace Mosa.Platform.ESP32.Instructions
{
	/// <summary>
	/// Andb - Boolean And, ANDB performs the logical and of Boolean registers bs and bt and writes the result to Boolean register br. When the sense of one of the source Booleans is inverted (0 → true, 1 → false), use ANDBC. When the sense of both of the source Booleans is inverted, use ORB and an inverted test of the result.
	/// </summary>
	/// <seealso cref="Mosa.Platform.ESP32.ESP32Instruction" />
	public sealed class Andb : ESP32Instruction
	{
		public override int ID { get { return 704; } }

		internal Andb()
			: base(1, 3)
		{
		}

		public override void Emit(InstructionNode node, BaseCodeEmitter emitter)
		{
			System.Diagnostics.Debug.Assert(node.ResultCount == 1);
			System.Diagnostics.Debug.Assert(node.OperandCount == 3);

			emitter.OpcodeEncoder.AppendNibble(0b0000);
			emitter.OpcodeEncoder.AppendNibble(0b0010);
			emitter.OpcodeEncoder.AppendNibble(node.Result.Register.RegisterCode);
			emitter.OpcodeEncoder.AppendNibble(node.Operand1.Register.RegisterCode);
			emitter.OpcodeEncoder.AppendNibble(node.Operand2.Register.RegisterCode);
			emitter.OpcodeEncoder.AppendNibble(0b0000);
		}
	}
}
