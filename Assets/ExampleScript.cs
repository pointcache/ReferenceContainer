using System.Collections;
using System.Collections.Generic;
using pointcache.ReferenceContainer;
using UnityEngine;

public class ExampleScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //Directly set ui text, by getting the light component ref
        gameObject.RefContainer().SetText("text", gameObject.RefContainer().GetFrom<Light>("light").intensity.ToString());
        //Directly instantiate resource object
        Instantiate(gameObject.RefContainer("prefab")).transform.localScale = new Vector3(100,100,100);

	}

    private void Update() {
        //You have to cache component ofc, but you still get decent performance with repeating direct calls as they are cached in a dictionary.
        gameObject.RefContainer("camera").transform.Rotate(new Vector3(.1f, 0, 0));
    }
}
