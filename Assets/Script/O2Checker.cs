using UnityEngine;
using System.Collections;

public class O2Checker : MonoBehaviour, IRestartable {

	public GameObject O2Bar;

	// O2 decrease speed : 1/s
	public float maxAmountO2 = 15;
	private float currentAmountO2;
	private SpriteRenderer O2BarBgRenderer;
	private SpriteRenderer O2BarRenderer;
	private Vector3 initScale;
	private bool isActive = false;

	// Use this for initialization
	void Start () {
		O2BarBgRenderer = GetComponent<SpriteRenderer>();
		O2BarRenderer = O2Bar.GetComponent<SpriteRenderer>();
		initScale = O2BarRenderer.gameObject.transform.localScale;

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
			Restarter.RestartAll();	
	}
	
	public bool IsActive()
	{
		return isActive;
	}

	public void Active()
	{
		O2BarBgRenderer.enabled = true;
		O2BarRenderer.enabled = true;
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
		currentAmountO2 = maxAmountO2;
		O2BarRenderer.gameObject.transform.localScale = initScale;
	}
	
	void UpdateO2Bar()
	{
		O2BarRenderer.gameObject.transform.localScale = new Vector3(initScale.x * currentAmountO2 / maxAmountO2, initScale.y, O2BarRenderer.gameObject.transform.localScale.z);
	}

	void IRestartable.Restart()
	{
		Initialize();
	}
}
