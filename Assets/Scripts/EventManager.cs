using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
	public event EventHandler<OnRestartEventArgs> OnRestart;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.isAlive == false)
		{
			if (OnRestart != null) OnRestart(this, new OnRestartEventArgs { position = Vector3.zero });
		}
    }

	public class OnRestartEventArgs
	{
		public Vector3 position;
	}
}
