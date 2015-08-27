using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class StageButton : MonoBehaviour {
		public string levelTag;
		public GameObject lockImage;
		public Button button;

		private LevelTag parsedLevelTag;

		void Awake()
		{
			parsedLevelTag = new LevelTag(levelTag);
		}
		
		public bool IsLocked()
		{
			if (parsedLevelTag.Chapter == 0)
			{
				return false;
			}
			return PlayerPrefs.GetInt(levelTag) == 1;
		}

		public void Lock()
		{
			lockImage.SetActive(true);
			button.interactable = false;
		}

		public void Unlock()
		{
			lockImage.SetActive(false);
			button.interactable = true;
		}
	}

    class LevelTag
    {
		private int chapter;
		public int Chapter
		{
			get {
				return chapter;
			}
		}

		private int stage;
		public int Stage
		{
			get {
				return stage;
			}
		}

		public LevelTag(string input)
		{
			var tokens = input.Split('-');
			chapter = int.Parse(tokens[0]);
			stage = int.Parse(tokens[1]);
		}
    }
}
