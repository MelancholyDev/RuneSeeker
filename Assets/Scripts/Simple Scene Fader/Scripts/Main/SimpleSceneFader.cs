﻿using UnityEngine;using System.Collections;using UnityEngine.SceneManagement;using UnityEngine.UI;public class SimpleSceneFader : MonoBehaviour {	private Image img;	private static bool isSceneChanging = false;	private bool sceneChanged = false;	private static float defaultTime = 1.0f;	private static float alphaAdd = 0f;	// errors are coming, becasue you are using static variables.	void Start ( )
    {		DontDestroyOnLoad (gameObject);		ResetData ( );	}	void Update ( ) {		if (SceneManager.GetActiveScene ( ).name == PlayerPrefs.GetString ("SceneToLoad")) {			sceneChanged = true;		} else {			sceneChanged = false;		}	}	void FixedUpdate ( ) {		if (isSceneChanging) {			float newAlpha;			if (sceneChanged == false) {				newAlpha = img.color.a + alphaAdd;			} else {				newAlpha = img.color.a - alphaAdd;			}			img.color = new Color (0f, 0f, 0f, newAlpha);			if (newAlpha <= 0f) {				ResetData ( );			}		}	}	public static void ChangeSceneWithFade (string sceneName) {		GameObject go = GameObject.FindGameObjectWithTag ("SceneFader");		isSceneChanging = true;		alphaAdd = 1.0f / (defaultTime / 2.0f * 50.0f); // here 1.0f is for full alpha value.not of default time.50.0f is the numner of frames in fixed upadte per second		PlayerPrefs.SetString ("SceneToLoad", sceneName);		go.GetComponent<SimpleSceneFader> ( ).Invoke ("ChangeScene", defaultTime / 2.0f);		go.GetComponent<Canvas> ( ).sortingOrder = 100;	}	public static void ChangeSceneWithFade (string sceneName, float time) {		GameObject go = GameObject.FindGameObjectWithTag ("SceneFader");		defaultTime = time;		isSceneChanging = true;		alphaAdd = 1.0f / (defaultTime / 2.0f * 50.0f); // here 1.0f is for full alpha value.not of default time.50.0f is the numner of frames in fixed upadte per second		PlayerPrefs.SetString ("SceneToLoad", sceneName);		go.GetComponent<SimpleSceneFader> ( ).Invoke ("ChangeScene", defaultTime / 2.0f);		go.GetComponent<Canvas> ( ).sortingOrder = 100;	}	void ChangeScene ( ) {		SceneManager.LoadScene ("" + PlayerPrefs.GetString ("SceneToLoad"));	}	public void ResetData ( ) {		img = transform.GetChild (0).gameObject.GetComponent<Image> ( );		isSceneChanging = false;		sceneChanged = false;		defaultTime = 1.0f;		alphaAdd = 0f;		gameObject.GetComponent<Canvas> ( ).sortingOrder = -1;		PlayerPrefs.DeleteKey ("SceneToLoad");	}}