using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XueGuanController : MonoBehaviour {

	public List<GameObject> dongmaiGOs;
	public List<GameObject> jingmaiGOs;

	public Button btnDongMai;
	public Button btnJingMai;

	private bool dongMaiVisible = true;
	private bool jingMaiVisible = true;

	private Color showColor = Color.green;
	private Color hideColor = Color.yellow;


	// Use this for initialization
	void Awake () {
		btnDongMai.onClick.AddListener(delegate() {  
			this.OnBtnClick(btnDongMai.gameObject);   
		}); 

		btnJingMai.onClick.AddListener(delegate() {  
			this.OnBtnClick(btnJingMai.gameObject);   
		}); 
	}

	void Start(){
		ShowDongMai (false);
		ShowJingMai (true);
	}

	public void OnBtnClick(GameObject sender){
		if (sender == btnDongMai.gameObject) {
			this.ShowDongMai (!dongMaiVisible);
		}else if(sender == btnJingMai.gameObject) {
			this.ShowJingMai (!jingMaiVisible);
		}
	}

	public void ShowDongMai(bool _show){
	
		foreach (GameObject go in dongmaiGOs) {
			go.SetActive (_show);
		}

		dongMaiVisible = _show;

		Text dongmaiText = btnDongMai.GetComponentInChildren<Text> ();
		if (dongMaiVisible) {
			dongmaiText.text = "隐藏动脉";
		} else {
			dongmaiText.text = "显示动脉";
		}

	}

	public void ShowJingMai(bool _show){

		foreach (GameObject go in jingmaiGOs) {
			go.SetActive (_show);
		}

		jingMaiVisible = _show;

		Text jingmaiText = btnJingMai.GetComponentInChildren<Text> ();
		if (jingMaiVisible) {
			jingmaiText.text = "隐藏静脉";
		} else {
			jingmaiText.text = "显示静脉";
		}


	}


}
