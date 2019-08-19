using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furniture : MonoBehaviour
{
    [SerializeField]
    private Renderer renderer;

    private Material original;
    public Material highlight;
    private Material[] materials;
    
    public int posicion;

    // Start is called before the first frame update
    void Start()
    {
        //renderer = GetComponentInChildren<Renderer>();
        Creator creator = this.GetComponent<Creator>();
        if(creator == null)
        {
            materials = renderer.materials;
            original = materials[posicion];
        }
    }

    public void Highlight()
    {
        materials[posicion] = highlight;
        renderer.materials = materials;
    }

    public void removeHighlight()
    {
        materials[posicion] = original;
        renderer.materials = materials;

    }
}
