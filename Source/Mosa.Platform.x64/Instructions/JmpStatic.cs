// Copyright (c) MOSA Project. Licensed under the New BSD License.

// This code was generated by an automated template.

using Mosa.Compiler.Framework;

namespace Mosa.Platform.x64.Instructions
{
	/// <summary>
	/// JmpStatic
	/// </summary>
	/// <seealso cref="Mosa.Platform.x64.X64Instruction" />
	public sealed class JmpStatic : X64Instruction
	{
		public override int ID { get { return 418; } }

		internal JmpStatic()
			: base(0, 1)
		{
		}

		public static readonly LegacyOpCode LegacyOpcode = new LegacyOpCode(new byte[] { 0xE9 });

		public override FlowControl FlowControl { get { return FlowControl.UnconditionalBranch; } }

		public override bool IsZeroFlagUnchanged { get { return true; } }

		public override bool IsZeroFlagUndefined { get { return true; } }

		public override bool IsCarryFlagUnchanged { get { return true; } }

		public override bool IsCarryFlagUndefined { get { return true; } }

		public override bool IsSignFlagUnchanged { get { return true; } }

		public override bool IsSignFlagUndefined { get { return true; } }

		public override bool IsOverflowFlagUnchanged { get { return true; } }

		public override bool IsOverflowFlagUndefined { get { return true; } }

		public override bool IsParityFlagUnchanged { get { return true; } }

		public override bool IsParityFlagUndefined { get { return true; } }

		public override void Emit(InstructionNode node, BaseCodeEmitter emitter)
		{
			System.Diagnostics.Debug.Assert(node.ResultCount == DefaultResultCount);
			System.Diagnostics.Debug.Assert(node.OperandCount == DefaultOperandCount);

			//StaticEmitters.EmitJmpStatic(node, emitter);
		}
	}
}
