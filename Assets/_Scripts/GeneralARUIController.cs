using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneralARUIController : MonoBehaviour
{

	public static GeneralARUIController inst;

	public Canvas uiCanvas;
	public Button btnClose;
	public Button btn1;
	public Button btn2;

	public GameObject activeArTarget;
	public Vuforia.MyArDemoTrackableHandler activeArTargetHandler;

	void Awake ()
	{
		GeneralARUIController.inst = this;
		ShowUI (false);
		btnClose.onClick.AddListener (delegate() {  
			this.OnBtnCloseClick ();   
		}); 
	}

	public void OnTrackingFound (Vuforia.MyArDemoTrackableHandler theHandler)
	{
		this.SetActiveArTarget (theHandler);
	}

	public void OnTrackingLost (Vuforia.MyArDemoTrackableHandler theHandler)
	{
		if (activeArTargetHandler == theHandler) {
			this.SetActiveArTarget (null);
		}
	}

	private void SetActiveArTarget (Vuforia.MyArDemoTrackableHandler theHandler)
	{
	
		if (theHandler == null) {
		
			activeArTarget = null;
			activeArTargetHandler = null;
			ShowUI (false);
			return;
		}

		if (theHandler != null) {
		
			activeArTargetHandler = theHandler;
			activeArTarget = theHandler.gameObject;
			ShowUI ();
			ShowButtons (theHandler.name == "xueguan");
		}
	}

	void OnBtnCloseClick ()
	{
		if (activeArTargetHandler != null) {
			activeArTargetHandler.OnForcedLostTracking ();
		}

		SetActiveArTarget (null);
	}

	void ShowUI(bool v = true){
		this.uiCanvas.gameObject.SetActive (v);
	}

	void ShowButtons (bool v)
	{
		this.btn1.gameObject.SetActive (v);
		this.btn2.gameObject.SetActive (v);
	}

	public void SetFakeNumber (float _number)
	{
		Text txt = this.btnClose.GetComponentInChildren<Text> ();
		txt.text = _number.ToString ();
	}

}
