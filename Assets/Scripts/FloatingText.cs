using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float destroyTime = 10.1f;
    public Transform parentTransformPosition;
    public string text;
    public Color color;

    // Start is called before the first frame update
    void Start()
    {
        GameObject tmpfloatingText = this.gameObject.GetNamedChild("Floating Text");
        tmpfloatingText.GetComponent<TextMesh>().text = this.text;
        tmpfloatingText.GetComponent<TextMesh>().color = this.color;
        Destroy(this.gameObject, this.destroyTime);
    }

    void Update()
    {
        this.transform.position = this.parentTransformPosition.position;
    }

}
