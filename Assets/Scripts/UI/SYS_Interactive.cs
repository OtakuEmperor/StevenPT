﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SYS_Interactive : MonoBehaviour
{
	public static SYS_Interactive Direct;

	void Awake() {
		Direct = this;
	}

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void Interactive(int type) {
		if (type == 0) {

		} else if (type == 1) {

		} else {

		}
	}
}
