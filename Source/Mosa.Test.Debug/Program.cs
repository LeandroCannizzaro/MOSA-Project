﻿
using Mosa.Test.Cases.CIL;
using Mosa.Test.Cases.IL;
using Mosa.Test.Cases.FIX.IL;
using Mosa.Test.Cases.OLD.IL;

namespace Mosa.Test.Debug
{
	class Program
	{
		public static void Main()
		{
			ConditionalOperator s = new ConditionalOperator();

			s.CompareEqualI1((sbyte)0x02, (sbyte)0x02, (sbyte)0x05, (sbyte)0x01);
			s.CompareEqualI1((sbyte)0x60, (sbyte)0x00, (sbyte)0x60, (sbyte)0x06);
			s.CompareEqualI1((sbyte)1, (sbyte)0, (sbyte)1, (sbyte)2);
			s.CompareEqualI1((sbyte)2, (sbyte)2, (sbyte)2, (sbyte)1);

			//Add add = new Add();

			//add.AddConstantCLeft('a', 'Z');
			//add.AddConstantCLeft('a', 'Z');
			//add.AddConstantCLeft('a', 'Z');

			Int32Fixture int32Fixture = new Int32Fixture();
			BooleanFixture booleanFixture = new BooleanFixture();
			CallVirtualFixture callVirtualFixture = new CallVirtualFixture();
			Call call = new Call();

			for (int i = 0; i < 2000; i++)
			{
				call.CallI4(i);
				call.CallU4((uint)i);
				int32Fixture.Ldlen(i);
				int32Fixture.Newarr();
				int32Fixture.LdelemaI4(i, i);

				booleanFixture.Ldlen(i);
				booleanFixture.Newarr();
				booleanFixture.LdelemB(i, true);
				booleanFixture.LdelemaB(i, true);
				booleanFixture.StelemB(i, true);

				callVirtualFixture.TestBaseCall();
				callVirtualFixture.TestVirtualCall();
			}

			return;
		}
	}
}
