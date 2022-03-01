using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createObj : MonoBehaviour
{

	public objLoader loader;

	void Start () {
		loader.Load ("Assets/Objects/", "teacup.obj");
	}
}