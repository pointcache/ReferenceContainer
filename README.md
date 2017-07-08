# ReferenceContainer
Unity script allowing string based access to gameobjects, allows binding independent of name and hierarchy.

![](https://kek.gg/i/62W2Sw.png)


# But Why

Sometimes you want to avoid creating direct resource references in your scripts and you just want to have a simple and fast way to access objects in code. 

# Usage

```csharp

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

```

Check the class itself it has number of useful setter methods for various unity components, add your own as needed.
