using UnityEngine;
using System.Collections;

namespace Chapter5
{
	public class DarkSplitter : MonoBehaviour {
		public static DarkSplitter Instance = null;
		void Awake()
		{
			Instance = this;
		}
	}
}