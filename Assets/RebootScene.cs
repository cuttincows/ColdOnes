using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RebootScene : MonoBehaviour {

	void Update () {
		if (Input.GetButtonDown("Reboot"))
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
