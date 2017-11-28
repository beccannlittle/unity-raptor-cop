using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleRotate : MonoBehaviour {
	void Update(){
		transform.Rotate (0f, 0.03f, 0f, Space.World);

	}
}
