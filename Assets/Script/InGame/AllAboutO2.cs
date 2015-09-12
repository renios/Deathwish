using UnityEngine;
using System.Collections;
using Enums;

public class AllAboutO2 : MonoBehaviour, IRestartable {

	public GameObject O2Bar;
	public GameObject O2Text;
	public GameObject O2Background;
	public GameObject O2TipMarker;

	// O2 decrease speed : 1/s
	public float maxAmountO2 = 15;
	private float currentAmountO2;
	private SpriteRenderer O2BarBgRenderer;
	private SpriteRenderer O2BarRenderer;
	private SpriteRenderer O2TextRenderer;
	private SpriteRenderer O2BackgroundRenderer;
	private SpriteRenderer O2TipMarkerRenderer;
	private Vector3 initBarScale;
	private float initTipPosX;
	private bool isActive = false;

	// Use this for initialization
	void Start () {
		O2BarBgRenderer = GetComponent<SpriteRenderer>();
		O2BarRenderer = O2Bar.GetComponent<SpriteRenderer>();
		O2TextRenderer = O2Text.GetComponent<SpriteRenderer>();
		O2BackgroundRenderer = O2Background.GetComponent<SpriteRenderer>();
		O2TipMarkerRenderer = O2TipMarker.GetComponent<SpriteRenderer>();

		initBarScale = O2BarRenderer.gameObject.transform.localScale;
		initTipPosX = O2BarRenderer.bounds.max.x;

		Initialize();
	}

	// Update is called once per frame
	void Update () {
		if (isActive && (currentAmountO2 > 0))
		{
			currentAmountO2 -= Time.deltaTime;
			UpdateO2Bar();
		}
		if (currentAmountO2 <= 0)
			FindObjectOfType<Player>().PlayDieAnimSoundAndRestart(SoundType.None);
	}
	
	public bool IsActive()
	{
		return isActive;
	}

	public void Active()
	{
		O2BarBgRenderer.enabled = true;
		O2BarRenderer.enabled = true;
		O2TextRenderer.enabled = true;
		O2BackgroundRenderer.enabled = true;
		O2TipMarkerRenderer.enabled = true;
		isActive = true;
	}

	public void Deactive()
	{
		isActive = false;
		Initialize();
	}

	void Initialize()
	{
		O2BarBgRenderer.enabled = false;
		O2BarRenderer.enabled = false;
		O2TextRenderer.enabled = false;
		O2BackgroundRenderer.enabled = false;
		O2TipMarkerRenderer.enabled = false;
		currentAmountO2 = maxAmountO2;
		O2Bar.transform.localScale = initBarScale;
		O2TipMarker.transform.position = new Vector2 (initTipPosX, O2Bar.transform.position.y);
	}
	
	void UpdateO2Bar()
	{
		O2Bar.transform.localScale = new Vector3(initBarScale.x * currentAmountO2 / maxAmountO2, initBarScale.y, O2BarRenderer.gameObject.transform.localScale.z);
		O2TipMarker.transform.position = new Vector2 (O2BarRenderer.bounds.max.x, O2Bar.transform.position.y);
	}

	void IRestartable.Restart()
	{
		Initialize();
		isActive = false;
	}
}
