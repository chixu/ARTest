using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortraitPicture : MonoBehaviour {

	public Texture tx;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SelectPortraitPicture(){
		AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");   //获取unity的Java类,只能调用静态方法，获取静态属性
		AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");      //获取当前的Activity对象,能调用公开方法和公开属性

		//一开始就调用安卓选择图片
		jo.Call("SelectPhoto"); 
	}

	//给安卓使用，安卓那边存完后通知U3D
	void AndroidSaveHeadImageOver(string str)
	{
		GetComponent<RawImage>().texture = tx;
		StartCoroutine(LoadTexture(str));

	}
	//从安卓中读取贴图
	IEnumerator LoadTexture(string name)
	{
		
		string path = "file://" + Application.persistentDataPath + "/" + name;

		WWW www = new WWW(path);
		while (!www.isDone)
		{
			//没有完成什么都不做，等待
		}
		yield return www;
		//显示贴图
		GetComponent<RawImage>().texture = www.texture;
		//GetComponent<RawImage>().texture = tx;
	}
}
