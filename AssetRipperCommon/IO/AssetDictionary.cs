﻿using AssetRipper.Core.Classes.Misc;
using AssetRipper.Core.Interfaces;
using AssetRipper.Core.Parser.Asset;
using System.Collections.Generic;

namespace AssetRipper.Core.IO
{
	/// <summary>
	/// A dictionary class supporting non-unique keys
	/// </summary>
	/// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
	/// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
	public class AssetDictionary<TKey, TValue> : List<NullableKeyValuePair<TKey, TValue>>, IDependent
	{
		public void Add(TKey key, TValue value) => Add(new NullableKeyValuePair<TKey, TValue>(key, value));

		public IEnumerable<PPtr<IUnityObjectBase>> FetchDependencies(DependencyContext context)
		{
			foreach(NullableKeyValuePair<TKey, TValue> keyValuePair in this)
			{
				if(keyValuePair != null)
				{
					foreach (PPtr<IUnityObjectBase> dependency in keyValuePair.FetchDependencies(context))
					{
						yield return dependency;
					}
				}
			}
		}
	}
}
