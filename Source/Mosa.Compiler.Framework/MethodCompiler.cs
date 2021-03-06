// Copyright (c) MOSA Project. Licensed under the New BSD License.

using Mosa.Compiler.Common;
using Mosa.Compiler.Framework.Analysis;
using Mosa.Compiler.Framework.IR;
using Mosa.Compiler.Framework.Linker;
using Mosa.Compiler.Framework.Trace;
using Mosa.Compiler.MosaTypeSystem;
using System.Collections.Generic;
using System.Diagnostics;

namespace Mosa.Compiler.Framework
{
	/// <summary>
	/// Base class of a method compiler.
	/// </summary>
	/// <remarks>
	/// A method compiler is responsible for compiling a single method
	/// of an object.
	/// </remarks>
	public sealed class MethodCompiler
	{
		#region Data Members

		/// <summary>
		/// The empty operand list
		/// </summary>
		private static readonly Operand[] emptyOperandList = new Operand[0];

		private readonly Stopwatch Stopwatch;

		#endregion Data Members

		#region Properties

		/// <summary>
		/// Gets the Architecture to compile for.
		/// </summary>
		public BaseArchitecture Architecture { get; }

		/// <summary>
		/// Gets the linker used to resolve external symbols.
		/// </summary>
		public MosaLinker Linker { get; }

		/// <summary>
		/// Gets the method implementation being compiled.
		/// </summary>
		public MosaMethod Method { get; }

		/// <summary>
		/// Gets the owner type of the method.
		/// </summary>
		public MosaType Type { get; }

		/// <summary>
		/// Gets the basic blocks.
		/// </summary>
		/// <value>The basic blocks.</value>
		public BasicBlocks BasicBlocks { get; }

		/// <summary>
		/// Retrieves the compilation scheduler.
		/// </summary>
		/// <value>The compilation scheduler.</value>
		public MethodScheduler MethodScheduler { get; }

		/// <summary>
		/// Provides access to the pipeline of this compiler.
		/// </summary>
		public Pipeline<BaseMethodCompilerStage> Pipeline { get; set; }

		/// <summary>
		/// Gets the type system.
		/// </summary>
		/// <value>The type system.</value>
		public TypeSystem TypeSystem { get; }

		/// <summary>
		/// Gets the type layout.
		/// </summary>
		/// <value>The type layout.</value>
		public MosaTypeLayout TypeLayout { get; }

		/// <summary>
		/// Gets the compiler trace handle
		/// </summary>
		/// <value>The log.</value>
		public CompilerTrace Trace { get; }

		/// <summary>
		/// Gets the local variables.
		/// </summary>
		public Operand[] LocalVariables { get; private set; }

		/// <summary>
		/// Gets the assembly compiler.
		/// </summary>
		public Compiler Compiler { get; }

		/// <summary>
		/// Gets the stack.
		/// </summary>
		public List<Operand> LocalStack { get; }

		/// <summary>
		/// Gets or sets the size of the stack.
		/// </summary>
		/// <value>
		/// The size of the stack memory.
		/// </value>
		public int StackSize { get; set; }

		/// <summary>
		/// Gets the virtual register layout.
		/// </summary>
		public VirtualRegisters VirtualRegisters { get; }

		/// <summary>
		/// Gets the parameters.
		/// </summary>
		public Operand[] Parameters { get; }

		/// <summary>
		/// Gets the protected regions.
		/// </summary>
		/// <value>
		/// The protected regions.
		/// </value>
		public List<ProtectedRegion> ProtectedRegions { get; set; }

		/// <summary>
		/// The labels
		/// </summary>
		public Dictionary<int, int> Labels { get; set; }

		/// <summary>
		/// Gets the thread identifier.
		/// </summary>
		public int ThreadID { get; }

		/// <summary>
		/// Gets the compiler method data.
		/// </summary>
		public MethodData MethodData { get; }

		/// <summary>
		/// The stack frame
		/// </summary>
		public Operand StackFrame { get; }

		/// <summary>
		/// The stack frame
		/// </summary>
		public Operand StackPointer { get; }

		/// <summary>
		/// Gets the platform constant zero
		/// </summary>
		public Operand ConstantZero { get; }

		/// <summary>
		/// Gets the 32-bit constant zero.
		/// </summary>
		public Operand ConstantZero32 { get; }

		/// <summary>
		/// Gets the 64-bit constant zero.
		/// </summary>
		public Operand ConstantZero64 { get; }

		/// <summary>
		/// Gets a value indicating whether this instance is in SSA form.
		/// </summary>
		public bool IsInSSAForm { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is execute pipeline.
		/// </summary>
		public bool IsExecutePipeline { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this method requires CIL decoding .
		/// </summary>
		public bool IsCILDecodeRequired { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this method is plugged.
		/// </summary>
		public bool IsMethodPlugged { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is stack frame required.
		/// </summary>
		public bool IsStackFrameRequired { get; set; }

		/// <summary>
		/// Holds flag that will stop method compiler
		/// </summary>
		public bool IsStopped { get; private set; }

		/// <summary>
		/// Gets the method scanner.
		/// </summary>
		public MethodScanner MethodScanner { get; }

		#endregion Properties

		#region Construction

		/// <summary>
		/// Initializes a new instance of the <see cref="MethodCompiler" /> class.
		/// </summary>
		/// <param name="compiler">The assembly compiler.</param>
		/// <param name="method">The method to compile by this instance.</param>
		/// <param name="basicBlocks">The basic blocks.</param>
		/// <param name="threadID">The thread identifier.</param>
		public MethodCompiler(Compiler compiler, MosaMethod method, BasicBlocks basicBlocks, int threadID)
		{
			Stopwatch = Stopwatch.StartNew();

			Compiler = compiler;
			Method = method;
			Type = method.DeclaringType;
			MethodScheduler = compiler.MethodScheduler;
			Architecture = compiler.Architecture;
			TypeSystem = compiler.TypeSystem;
			TypeLayout = compiler.TypeLayout;
			Trace = compiler.CompilerTrace;
			Linker = compiler.Linker;
			MethodScanner = compiler.MethodScanner;

			BasicBlocks = basicBlocks ?? new BasicBlocks();
			LocalStack = new List<Operand>();
			VirtualRegisters = new VirtualRegisters();

			StackFrame = Operand.CreateCPURegister(TypeSystem.BuiltIn.Pointer, Architecture.StackFrameRegister);
			StackPointer = Operand.CreateCPURegister(TypeSystem.BuiltIn.Pointer, Architecture.StackPointerRegister);
			Parameters = new Operand[method.Signature.Parameters.Count + (method.HasThis || method.HasExplicitThis ? 1 : 0)];

			ConstantZero32 = CreateConstant((uint)0);
			ConstantZero64 = CreateConstant((ulong)0);
			ConstantZero = Architecture.Is32BitPlatform ? ConstantZero32 : ConstantZero64;

			LocalVariables = emptyOperandList;
			ThreadID = threadID;

			IsStopped = false;
			IsInSSAForm = false;
			IsExecutePipeline = true;
			IsCILDecodeRequired = true;
			IsStackFrameRequired = true;

			MethodData = compiler.CompilerData.GetMethodData(Method);

			MethodData.Counters.Reset();

			EvaluateParameterOperands();

			CalculateMethodParameterSize();

			MethodData.Counters.NewCountSkipLock("ExecutionTime.Setup.Ticks", (int)Stopwatch.ElapsedTicks);
		}

		#endregion Construction

		#region Methods

		private void CalculateMethodParameterSize()
		{
			int stacksize = 0;

			if (Method.HasThis)
			{
				stacksize = TypeLayout.NativePointerSize;
			}

			foreach (var parameter in Method.Signature.Parameters)
			{
				var size = parameter.ParameterType.IsValueType ? TypeLayout.GetTypeSize(parameter.ParameterType) : TypeLayout.NativePointerAlignment;
				stacksize += Alignment.AlignUp(size, TypeLayout.NativePointerAlignment);
			}

			MethodData.ParameterStackSize = stacksize;
		}

		/// <summary>
		/// Adds the stack local.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		public Operand AddStackLocal(MosaType type)
		{
			return AddStackLocal(type, false);
		}

		/// <summary>
		/// Adds the stack local.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="pinned">if set to <c>true</c> [pinned].</param>
		/// <returns></returns>
		public Operand AddStackLocal(MosaType type, bool pinned)
		{
			var local = Operand.CreateStackLocal(type, LocalStack.Count, pinned);
			LocalStack.Add(local);
			return local;
		}

		/// <summary>
		/// Sets the stack parameter.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="type">The type.</param>
		/// <param name="name">The name.</param>
		/// <param name="isThis">if set to <c>true</c> [is this].</param>
		/// <param name="offset">The offset.</param>
		/// <returns></returns>
		private Operand SetStackParameter(int index, MosaType type, string name, bool isThis, int offset)
		{
			var param = Operand.CreateStackParameter(type, index, name, isThis, offset);
			Parameters[index] = param;
			return param;
		}

		/// <summary>
		/// Evaluates the parameter operands.
		/// </summary>
		private void EvaluateParameterOperands()
		{
			int offset = Architecture.OffsetOfFirstParameter;

			if (MosaTypeLayout.IsStoredOnStack(Method.Signature.ReturnType))
			{
				offset += TypeLayout.GetTypeSize(Method.Signature.ReturnType);
			}

			int index = 0;

			if (Method.HasThis || Method.HasExplicitThis)
			{
				if (Type.IsValueType)
				{
					var ptr = Type.ToManagedPointer();
					SetStackParameter(index++, ptr, "this", true, offset);

					var size = GetReferenceOrTypeSize(ptr, true);
					offset += size;
				}
				else
				{
					SetStackParameter(index++, Type, "this", true, offset);

					var size = GetReferenceOrTypeSize(Type, true);
					offset += size;
				}
			}

			foreach (var parameter in Method.Signature.Parameters)
			{
				SetStackParameter(index++, parameter.ParameterType, parameter.Name, false, offset);

				var size = GetReferenceOrTypeSize(parameter.ParameterType, true);
				offset += size;
			}
		}

		/// <summary>
		/// Compiles the method referenced by this method compiler.
		/// </summary>
		public void Compile()
		{
			if (Method.IsCompilerGenerated)
			{
				IsCILDecodeRequired = false;
				IsStackFrameRequired = false;
			}

			PlugMethod();

			PatchDelegate();

			ExternalMethod();

			InternalMethod();

			ExecutePipeline();

			if (Compiler.CompilerOptions.EnableStatistics)
			{
				var log = new TraceLog(TraceType.MethodCounters, Method, string.Empty);
				log.Log(MethodData.Counters.Export());
				Trace.PostTraceLog(log);
			}
		}

		private void ExecutePipeline()
		{
			if (!IsExecutePipeline)
				return;

			var executionTimes = new long[Pipeline.Count];

			var startTicks = Stopwatch.ElapsedTicks;

			for (int i = 0; i < Pipeline.Count; i++)
			{
				var stage = Pipeline[i];

				stage.Setup(this, i);
				stage.Execute();

				executionTimes[i] = Stopwatch.ElapsedTicks;

				InstructionLogger.Run(this, stage);

				if (IsStopped)
					break;
			}

			if (Compiler.CompilerOptions.EnableStatistics && !IsStopped)
			{
				var totalTicks = Stopwatch.ElapsedTicks;

				MethodData.ElapsedTicks = totalTicks;

				MethodData.Counters.NewCountSkipLock("ExecutionTime.StageStart.Ticks", (int)startTicks);
				MethodData.Counters.NewCountSkipLock("ExecutionTime.Total.Ticks", (int)totalTicks);

				var executionTimeLog = new TraceLog(TraceType.MethodDebug, Method, "Execution Time/Ticks");

				long previousTicks = startTicks;

				for (int i = 0; i < Pipeline.Count; i++)
				{
					var pipelineTicks = executionTimes[i];
					var ticks = pipelineTicks - previousTicks;
					var percentage = (ticks * 100) / (double)(totalTicks - startTicks);
					previousTicks = pipelineTicks;

					int per = (int)percentage / 5;

					var entry = $"[{i:00}] {Pipeline[i].Name.PadRight(45)} : {percentage:00.00} % [{string.Empty.PadRight(per, '#').PadRight(20, ' ')}] ({ticks})";

					executionTimeLog.Log(entry);

					MethodData.Counters.NewCountSkipLock($"ExecutionTime.{i:00}.{Pipeline[i].Name}.Ticks", (int)ticks);
				}

				executionTimeLog.Log($"{"****Total Time".PadRight(57)}({totalTicks})");

				Trace.PostTraceLog(executionTimeLog);
			}
		}

		private void PlugMethod()
		{
			var plugMethod = Compiler.PlugSystem.GetReplacement(Method);

			if (plugMethod == null)
				return;

			Compiler.MethodScanner.MethodInvoked(plugMethod, Method);

			IsMethodPlugged = true;

			Debug.Assert(plugMethod != null);

			var plugSymbol = Operand.CreateSymbolFromMethod(plugMethod, TypeSystem);

			var block = BasicBlocks.CreateBlock(BasicBlock.PrologueLabel);
			BasicBlocks.AddHeadBlock(block);

			var ctx = new Context(block);

			ctx.AppendInstruction(IRInstruction.Jmp, null, plugSymbol);

			IsCILDecodeRequired = false;
			IsExecutePipeline = true;
			IsStackFrameRequired = false;
		}

		private void PatchDelegate()
		{
			if (Method.HasImplementation)
				return;

			if (!Method.DeclaringType.IsDelegate)
				return;

			if (!DelegatePatcher.PatchDelegate(this))
				return;

			IsCILDecodeRequired = false;
			IsExecutePipeline = true;
		}

		private void ExternalMethod()
		{
			if (!Method.IsExternal)
				return;

			IsCILDecodeRequired = false;
			IsExecutePipeline = false;
			IsStackFrameRequired = false;

			var intrinsic = Architecture.GetInstrinsicMethod(Method.ExternMethodModule);

			if (intrinsic != null)
				return;

			Linker.DefineExternalSymbol(Method.FullName, Method.ExternMethodName, SectionKind.Text);
		}

		private void InternalMethod()
		{
			if (!Method.IsInternal)
				return;

			IsCILDecodeRequired = false;
			IsExecutePipeline = false;
			IsStackFrameRequired = false;
		}

		/// <summary>
		/// Stops the method compiler.
		/// </summary>
		/// <returns></returns>
		public void Stop()
		{
			IsStopped = true;
		}

		/// <summary>
		/// Creates a new virtual register operand.
		/// </summary>
		/// <param name="type">The signature type of the virtual register.</param>
		/// <returns>
		/// An operand, which represents the virtual register.
		/// </returns>
		public Operand CreateVirtualRegister(MosaType type)
		{
			return VirtualRegisters.Allocate(type);
		}

		/// <summary>
		/// Splits the long operand.
		/// </summary>
		/// <param name="longOperand">The long operand.</param>
		public void SplitLongOperand(Operand longOperand)
		{
			VirtualRegisters.SplitLongOperand(TypeSystem, longOperand);
		}

		/// <summary>
		/// Splits the long operand.
		/// </summary>
		/// <param name="operand">The operand.</param>
		/// <param name="operandLow">The operand low.</param>
		/// <param name="operandHigh">The operand high.</param>
		public void SplitLongOperand(Operand operand, out Operand operandLow, out Operand operandHigh)
		{
			if (operand.Is64BitInteger)
			{
				SplitLongOperand(operand);
				operandLow = operand.Low;
				operandHigh = operand.High;
			}
			else
			{
				operandLow = operand;
				operandHigh = ConstantZero32;
			}
		}

		/// <summary>
		/// Allocates the virtual register or stack slot.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		public Operand AllocateVirtualRegisterOrStackSlot(MosaType type)
		{
			if (MosaTypeLayout.IsStoredOnStack(type))
			{
				return AddStackLocal(type);
			}
			else
			{
				var resultType = Compiler.GetStackType(type);
				return CreateVirtualRegister(resultType);
			}
		}

		/// <summary>
		/// Allocates the local variable virtual registers.
		/// </summary>
		/// <param name="locals">The locals.</param>
		public void SetLocalVariables(IList<MosaLocal> locals)
		{
			LocalVariables = new Operand[locals.Count];

			int index = 0;
			foreach (var local in locals)
			{
				if (MosaTypeLayout.IsStoredOnStack(local.Type) || local.IsPinned)
				{
					LocalVariables[index++] = AddStackLocal(local.Type, local.IsPinned);
				}
				else
				{
					var stacktype = Compiler.GetStackType(local.Type);
					LocalVariables[index++] = CreateVirtualRegister(stacktype);
				}
			}
		}

		/// <summary>
		/// Gets the size of the reference or type.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="aligned">if set to <c>true</c> [aligned].</param>
		/// <returns></returns>
		public int GetReferenceOrTypeSize(MosaType type, bool aligned)
		{
			if (type.IsValueType)
			{
				if (aligned)
					return Alignment.AlignUp(TypeLayout.GetTypeSize(type), Architecture.NativeAlignment);
				else
					return TypeLayout.GetTypeSize(type);
			}
			else
			{
				return Architecture.NativeAlignment;
			}
		}

		/// <summary>
		/// Gets the position.
		/// </summary>
		/// <param name="label">The label.</param>
		/// <returns></returns>
		public int GetPosition(int label)
		{
			return Labels[label];
		}

		/// <summary>
		/// Returns a <see cref="System.String" /> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String" /> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			return Method.ToString();
		}

		#endregion Methods

		#region Constant Helper Methods

		public Operand CreateConstant(byte value)
		{
			return Operand.CreateConstant(TypeSystem.BuiltIn.U1, value);
		}

		public Operand CreateConstant(int value)
		{
			return Operand.CreateConstant(TypeSystem.BuiltIn.I4, value);
		}

		public Operand CreateConstant(uint value)
		{
			return Operand.CreateConstant(TypeSystem.BuiltIn.U4, value);
		}

		public Operand CreateConstant(long value)
		{
			return Operand.CreateConstant(TypeSystem.BuiltIn.I8, value);
		}

		public Operand CreateConstant(ulong value)
		{
			return Operand.CreateConstant(TypeSystem.BuiltIn.U8, value);
		}

		public Operand CreateConstant(float value)
		{
			return Operand.CreateConstant(value, TypeSystem);
		}

		public Operand CreateConstant(double value)
		{
			return Operand.CreateConstant(value, TypeSystem);
		}

		#endregion Constant Helper Methods
	}
}
