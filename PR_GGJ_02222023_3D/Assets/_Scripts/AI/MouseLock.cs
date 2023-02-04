using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLock : MonoBehaviour
{

    // Update is called once per frame
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

		ServiceLocator.SceneManager.OnSceneUnloaded.AddListener(() =>
			{
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			}
		);

    }
}
