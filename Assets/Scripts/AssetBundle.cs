using UnityEngine;
using System.Collections;

public class AssetBundle : MonoBehaviour {
	public GUIText guiText;
	
	// Use this for initialization
	void Start () {
		StartCoroutine( LoadCorutin("http://www6355ue.sakura.ne.jp/unitytest/AssetBundleTest.unity3d") );
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	IEnumerator LoadCorutin(string url) {
		print("Load");
		
		WWW www = WWW.LoadFromCacheOrDownload(url, 0);
		yield return www;
		if (www.error != null) {
			print(www.error);
		}
		else {
			print("Load Success");
		    GameObject go = Instantiate(www.assetBundle.mainAsset) as GameObject;
		}
	}
}