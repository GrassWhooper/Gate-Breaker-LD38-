using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollUV : MonoBehaviour
{
    [SerializeField] float xOffsetMultipllier = 0.1f;
    [SerializeField] float yOffSetMultiplier = 0.1f;
    MeshRenderer meshRenderer;
	void Start () {
        meshRenderer = GetComponent<MeshRenderer>();
	}

    void Update ()
    {
        Material m = meshRenderer.material;
        Vector2 offSet = m.mainTextureOffset;
        offSet.x = transform.position.x * xOffsetMultipllier;
        offSet.y = transform.position.y * yOffSetMultiplier;
        
        m.mainTextureOffset = offSet;
	}
}