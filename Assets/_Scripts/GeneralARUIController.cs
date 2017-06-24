using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneralARUIController : MonoBehaviour {

	public static GeneralARUIController inst;

	public Canvas uiCanvas;
	public Button btnClose;

	public GameObject activeArTarget;
	public Vuforia.MyArDemoTrackableHandler activeArTargetHandler;

	void Awake(){
		GeneralARUIController.inst = this;

		ShowUI (false);

		btnClose.onClick.AddListener(delegate() {  
			this.OnBtnCloseClick();   
		}); 
	}

	public void OnTrackingFound(Vuforia.MyArDemoTrackableHandler theHandler){
		this.SetActiveArTarget (theHandler);
	}

	public void OnTrackingLost(Vuforia.MyArDemoTrackableHandler theHandler){
		if (activeArTargetHandler == theHandler) {
			this.SetActiveArTarget (null);
		}
	}

	public void SetActiveArTarget(Vuforia.MyArDemoTrackableHandler theHandler){
	
		if (theHandler == null) {
		
			activeArTarget = null;
			activeArTargetHandler = null;
			ShowUI (false);
			return;
		}

		if (theHandler != null) {
		
			activeArTargetHandler = theHandler;
			activeArTarget = theHandler.gameObject;
			ShowUI (true);
		}
	}

	void OnBtnCloseClick(){
		if (activeArTargetHandler != null) {
			activeArTargetHandler.OnForcedLostTracking ();
		}

		SetActiveArTarget (null);
	}

	void ShowUI(bool _show){
		this.uiCanvas.gameObject.SetActive (_show);
	}

}
