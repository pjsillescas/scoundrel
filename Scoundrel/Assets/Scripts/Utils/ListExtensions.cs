using UnityEngine;
using System.Collections.Generic;

public static class ListExtensions
{
	// Fisher-Yates algorithm
	public static void Shuffle<T>(this IList<T> list)
	{
		int n = list.Count;
		while (n > 1)
		{
			n--;
			int k = Random.Range(0, n + 1);
			(list[k], list[n]) = (list[n], list[k]);
		}
	}
}
