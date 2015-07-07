using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class Restarter
{
	private static IEnumerable<T> GetInterfaces<T>() where T : class
	{
		var monobehaviours = GameObject.FindObjectsOfType<MonoBehaviour> ();
		foreach (var monobehaviour in monobehaviours)
		{
			if (monobehaviour.GetType().GetInterfaces().Any(k => k == typeof(T)))
			{
				yield return monobehaviour as T;
			}
		}
	}

	public static void RestartAll()
	{
		foreach (IRestartable restartable in GetInterfaces<IRestartable>()) {
			restartable.Restart();
		}
	}
}
