// Copyright (c) MOSA Project. Licensed under the New BSD License.

// This code was generated by an automated template.

using Mosa.Compiler.Framework;

namespace Mosa.Platform.x64.Instructions
{
	/// <summary>
	/// CMovSigned32
	/// </summary>
	/// <seealso cref="Mosa.Platform.x64.X64Instruction" />
	public sealed class CMovSigned32 : X64Instruction
	{
		public override int ID { get { return 579; } }

		internal CMovSigned32()
			: base(1, 1)
		{
		}

		public override string AlternativeName { get { return "CMovS32"; } }

		public override bool IsSignFlagUsed { get { return true; } }

		public override BaseInstruction GetOpposite()
		{
			return X64.CMovNotSigned32;
		}
	}
}
