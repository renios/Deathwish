using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class StageButton : MonoBehaviour {
		private string levelTag
		{
			get {
				return gameObject.name;
			}
		}

		private string mapName
		{
			get {
				return gameObject.name;
			}
		}

		public GameObject lockImage;
		public Button button;

		private LevelTag parsedLevelTag;
		public Image mapImage;

		void Awake()
		{
			parsedLevelTag = new LevelTag(levelTag);
			var spriteImage = Resources.Load<Sprite>("icons/" + levelTag);
			if (spriteImage == null)
			{
				spriteImage = Resources.Load<Sprite>("icons/default");
			}
			if (spriteImage != null)
			{
				mapImage.sprite = spriteImage;	
			}
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
