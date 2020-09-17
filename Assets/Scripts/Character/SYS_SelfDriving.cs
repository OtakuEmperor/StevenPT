﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SYS_SelfDriving : MonoBehaviour {
	public static SYS_SelfDriving Direct;
	public SelfDriveTGT tgt;
	public float driveDis = 5;

	void Awake() {
		Direct = this;
	}

	// Update is called once per frame
	void Update() {
		if (tgt != null) {
			SYS_ShipController.Direct.OnUpdateDirection((tgt.GetPos() - (Vector2)SYS_ShipController.Direct.transform.position));

			if (Vector2.Distance(tgt.GetPos(),(Vector2)SYS_ShipController.Direct.transform.position) < driveDis) {
				Reset();
				Stop();
			}
		}
	}

	public void Regist(Vector2 tgtPos) {
		tgt = new SelfDriveTGT(tgtPos);
		SYS_ShipController.Direct.OnBeginMove();
	}

	public void Regist(Transform tgtTrans) {
		tgt = new SelfDriveTGT(tgtTrans);
		SYS_ShipController.Direct.OnBeginMove();
	}

	public void Reset() {
		tgt = null;
	}

	public void Stop() {
		SYS_ShipController.Direct.OnEndMove();
	}
}

public class SelfDriveTGT {
	public Vector2 tgtPos;
	public Transform tgtTrans;
	public bool posTGT = false;

	public SelfDriveTGT(Vector2 tgtPos) {
		this.tgtPos = tgtPos;
		posTGT = true;
	}

	public SelfDriveTGT(Transform tgtTrans) {
		this.tgtTrans = tgtTrans;
		posTGT = false;
	}

	public Vector2 GetPos() {
		if (posTGT) {
			return tgtPos;
		} else {
			return tgtTrans.position;
		}
	}
}
