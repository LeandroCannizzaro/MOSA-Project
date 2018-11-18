// Copyright (c) MOSA Project. Licensed under the New BSD License.

// This code was generated by an automated template.

using Mosa.Compiler.Framework;

namespace Mosa.Platform.ARMv6.Instructions
{
	/// <summary>
	/// Sxth32 - Signed Extend Halfword
	/// </summary>
	/// <seealso cref="Mosa.Platform.ARMv6.ARMv6Instruction" />
	public sealed class Sxth32 : ARMv6Instruction
	{
		public override int ID { get { return 661; } }

		internal Sxth32()
			: base(1, 3)
		{
		}
	}
}
