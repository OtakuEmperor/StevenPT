﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SYS_ModeSwitcher : MonoBehaviour {
	public static SYS_ModeSwitcher Direct;

	public List<RectTransform> switcherRect = new List<RectTransform>();
	public List<Transform> switcherTrans = new List<Transform>();

	public float switchRectRate = 1280;
	public float switchTransRate = 16;

	public AnimationCurve switchAnim;
	public float animing = 0;
	public float animTime = 3;
	
	public GameMode gameMode;

	void Awake() {
		Direct = this;
	}

	// Update is called once per frame
	void Update() {
		if (animing != 0 && Time.timeSinceLevelLoad - animing <= animTime) {
			foreach (RectTransform listRect in switcherRect) {
				if (gameMode == GameMode.Home) {
					listRect.anchoredPosition3D = new Vector3(0, switchAnim.Evaluate(1 - (Time.timeSinceLevelLoad - animing) / animTime) * switchRectRate, 0);
				} else {
					listRect.anchoredPosition3D = new Vector3(0, switchAnim.Evaluate((Time.timeSinceLevelLoad - animing) / animTime) * switchRectRate, 0);					
				}
			}

			foreach (Transform listTrans in switcherTrans) {
				if (gameMode == GameMode.Home) {
					listTrans.localPosition = new Vector3(0, switchAnim.Evaluate(1 - (Time.timeSinceLevelLoad - animing) / animTime) * switchTransRate, 0);
				} else {
					listTrans.localPosition = new Vector3(0, switchAnim.Evaluate((Time.timeSinceLevelLoad - animing) / animTime) * switchTransRate, 0);
				}
			}
		} else if (animing != 0) {
			foreach (RectTransform listRect in switcherRect) {
				if (gameMode == GameMode.Home) {
					listRect.anchoredPosition3D = new Vector3(0, switchAnim.Evaluate(0) * switchRectRate, 0);
				} else {
					listRect.anchoredPosition3D = new Vector3(0, switchAnim.Evaluate(1) * switchRectRate, 0);
				}
			}

			foreach (Transform listTrans in switcherTrans) {
				if (gameMode == GameMode.Home) {
					listTrans.localPosition = new Vector3(0, switchAnim.Evaluate(0) * switchTransRate, 0);
				} else {
					listTrans.localPosition = new Vector3(0, switchAnim.Evaluate(1) * switchTransRate, 0);
				}
			}
			animing = 0;
		}
	}

	public void SetMode(int gameMode) {
		if (this.gameMode != (GameMode)gameMode) {		
			if ((GameMode)gameMode == GameMode.Home) {
				this.gameMode = (GameMode)gameMode;
				animing = Time.timeSinceLevelLoad;

				SYS_SpaceManager.Direct.Reset();
				SYS_WeatherManager.Direct.Reset();
				UI_Navigator.Direct.Reset();
				SYS_StarmapManager.Direct.Reset();
				SYS_ShipController.Direct.Reset();
				SYS_SelfDriving.Direct.Reset();

				SYS_StarmapManager.Direct.Init();

			} else if ((GameMode)gameMode == GameMode.Space) {
				if (SYS_StarmapManager.Direct.IsRouteComplete()) {
					this.gameMode = (GameMode)gameMode;
					animing = Time.timeSinceLevelLoad;

					SYS_SpaceManager.Direct.Init();
					SYS_WeatherManager.Direct.Init();
					SYS_ResourseManager.Direct.Reset();
					UI_Navigator.Direct.Init();
				} else {
					SYS_Logger.Direct.SystemMsg("請設定路徑至終點再出發");
				}
			}
		}
	}

	public void SetMode(GameMode gameMode) {
		SetMode((int)gameMode);
	}

	
}

public enum GameMode {
	Home,
	Space
}