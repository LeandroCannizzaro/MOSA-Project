/*
 * (c) 2008 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Michael Ruck (grover) <sharpos@michaelruck.de>
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

using Mosa.Runtime.TypeSystem;
using Mosa.Runtime.Metadata;
using Mosa.Runtime.Metadata.Loader;
using Mosa.Runtime.Metadata.Tables;
using Mosa.Runtime.Metadata.Signatures;

namespace Mosa.Runtime.TypeSystem.Cil
{
	/// <summary>
	/// Runtime representation of a CIL type.
	/// </summary>
	sealed class CilRuntimeType : RuntimeType
	{
		#region Data Members

		/// <summary>
		/// Holds the token of the base type of this type.
		/// </summary>
		private TokenTypes baseTypeToken;

		/// <summary>
		/// The name index of the defined type.
		/// </summary>
		private TokenTypes nameIdx;

		/// <summary>
		/// The namespace index of the defined type.
		/// </summary>
		private TokenTypes namespaceIdx;

		#endregion // Data Members

		#region Construction

		/// <summary>
		/// Initializes a new instance of the <see cref="CilRuntimeType"/> class.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="typenamespace">The typenamespace.</param>
		/// <param name="packing">The packing.</param>
		/// <param name="size">The size.</param>
		/// <param name="token">The token.</param>
		/// <param name="baseType">Type of the base.</param>
		/// <param name="typeDefRow">The type def row.</param>
		public CilRuntimeType(string name, string typenamespace, int packing, int size, TokenTypes token, RuntimeType baseType, TypeDefRow typeDefRow) :
			base(token, baseType)
		{
			this.baseTypeToken = typeDefRow.Extends;
			this.nameIdx = typeDefRow.TypeNameIdx;
			this.namespaceIdx = typeDefRow.TypeNamespaceIdx;
			base.Attributes = typeDefRow.Flags;
			base.Pack = packing;
			base.Size = size;
			this.Name = name;
			this.Namespace = typenamespace;
		}

		#endregion // Construction

		#region Methods

		/// <summary>
		/// Called to retrieve the namespace of the type.
		/// </summary>
		/// <param name="metadataProvider">The metadata provider.</param>
		/// <returns>The namespace of the type.</returns>
		private string GetNamespace(IMetadataProvider metadataProvider)
		{
			//TODO
			if (IsNested)
			{
				TokenTypes enclosingType = GetEnclosingType(metadataProvider, Token);
				TypeDefRow typeDef = metadataProvider.ReadTypeDefRow(enclosingType);

				string enclosingNamespace = metadataProvider.ReadString(typeDef.TypeNamespaceIdx);
				string enclosingTypeName = metadataProvider.ReadString(typeDef.TypeNameIdx);

				return enclosingNamespace + "." + enclosingTypeName;
			}
			else
			{
				return metadataProvider.ReadString(this.namespaceIdx);
			}
		}

		private TokenTypes GetEnclosingType(IMetadataProvider metadataProvider, TokenTypes token)
		{
			//TODO
			for (int i = 1; ; i++)
			{
				NestedClassRow row = metadataProvider.ReadNestedClassRow(TokenTypes.NestedClass + i);
				if (row.NestedClassTableIdx == token)
					return row.EnclosingClassTableIdx;
			}
		}

		public bool IsNested
		{
			get
			{
				if ((Attributes & TypeAttributes.NestedPublic) == TypeAttributes.NestedPublic) return true;
				if ((Attributes & TypeAttributes.NestedPrivate) == TypeAttributes.NestedPrivate) return true;
				if ((Attributes & TypeAttributes.NestedFamily) == TypeAttributes.NestedFamily) return true;
				return false;
			}
		}

		#endregion // Methods
	}
}
