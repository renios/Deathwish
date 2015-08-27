using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class StageButton : MonoBehaviour {
		public string levelTag;
		public string mapName;
		public GameObject lockImage;
		public Button button;

		private LevelTag parsedLevelTag;

		void Awake()
		{
			parsedLevelTag = new LevelTag(levelTag);
		}
		
		public LevelTag GetLevelTag()
		{
			return parsedLevelTag;
		}
		
		public MapName GetMapName()
		{
			return new MapName(mapName);
		}
		
		public void OnButtonClicked()
		{
			Scene.Load(mapName, Scene.SceneType.Stage);
		}

		public bool IsLocked()
		{
			if (parsedLevelTag.Chapter == 0)
			{
				return false;
			}
			var previousLevelTag = Scene.GetPreviousLevelTag(parsedLevelTag);
			return PlayerPrefs.GetInt(previousLevelTag.ToString()) == 0;
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

    public struct MapName
    {
		private string mapName;
		public MapName(string input)
		{
			this.mapName = input;
		}
		
		public override String ToString()
		{
			return mapName;
		}
    }

    public struct LevelTag : System.IComparable<LevelTag>
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
		
		public override string ToString()
		{
			return chapter + "-" + stage;
		}

        public int CompareTo(LevelTag other)
        {
            if (this.Chapter != other.Chapter)
			{
				return this.Chapter - other.Chapter;
			}
			
			return this.stage - other.stage;
        }
    }
}
