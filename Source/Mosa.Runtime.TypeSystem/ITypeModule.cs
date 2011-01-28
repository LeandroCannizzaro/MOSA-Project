﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mosa.Runtime.Metadata;
using Mosa.Runtime.Metadata.Loader;

namespace Mosa.Runtime.TypeSystem
{
	public interface ITypeModule
	{

		/// <summary>
		/// Gets the metadata module.
		/// </summary>
		/// <value>The metadata module.</value>
		IMetadataModule MetadataModule { get; }

		/// <summary>
		/// Gets all types from module.
		/// </summary>
		/// <returns></returns>
		IEnumerable<RuntimeType> GetAllTypes();

		/// <summary>
		/// Gets the runtime type for the given type name and namespace
		/// </summary>
		/// <param name="nameSpace">The name space.</param>
		/// <param name="name">The name.</param>
		/// <returns></returns>
		RuntimeType GetType(string nameSpace, string name);

		/// <summary>
		/// Retrieves the runtime type for a given metadata token.
		/// </summary>
		/// <param name="token">The token of the type to load. This can represent a typeref, typedef or typespec token.</param>
		/// <returns>The runtime type of the specified token.</returns>
		RuntimeType GetType(TokenTypes token);

		/// <summary>
		/// Retrieves the field definition identified by the given token in the scope.
		/// </summary>
		/// <param name="token">The token of the field to retrieve.</param>
		/// <returns></returns>
		RuntimeField GetField(TokenTypes token);

		/// <summary>
		/// Retrieves the method definition identified by the given token in the scope.
		/// </summary>
		/// <param name="token">The token of the method to retrieve.</param>
		/// <returns></returns>
		RuntimeMethod GetMethod(TokenTypes token);
	}
}
