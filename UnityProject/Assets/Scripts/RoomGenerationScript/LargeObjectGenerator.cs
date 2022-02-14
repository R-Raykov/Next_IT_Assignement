using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeObjectGenerator : MonoBehaviour, IGeneratable
{
    private GeneratableObject prefab;
    private Vector3 startPos;
    private Transform parent;

    public void Initialize(GeneratableObject pPrefab, Transform pParent)
    {
        prefab = pPrefab;
        parent = pParent;
    }

    /// <summary>
    /// Generates a large object (large objects are everything except lights, chairs, carpets)
    /// </summary>
    public void Generate()
    {
        if(GameManager.Instance.GetRandomNumber() >= 50)
        {
            GameObject _generatedObject = Instantiate(prefab.Prefab, parent);
            
            startPos = Vector3.zero;

            // Adjust for parent pivot
            startPos.x = -1.0f;

            // Randomize height 
            if (prefab.GenerateOnHeight)
            {
                startPos.y = Random.Range(1.5f, 2.5f);
            }


            Renderer _renderer = prefab.Prefab.GetComponent<Renderer>();

            if(prefab.Name.Equals("Shelf"))
                startPos.z = _renderer ? _renderer.bounds.extents.x : 0.15f;
            else
                startPos.z = _renderer ? _renderer.bounds.extents.z : 0.15f;


            _generatedObject.transform.localPosition = startPos;

            _generatedObject.transform.rotation = parent.rotation;

            // Fix rotation
            if(prefab.Name.Equals("Bed"))
            {
                _generatedObject.transform.eulerAngles += new Vector3(0, 90, 0);
            }

            // If the object implements the generatable interface call it's generate as well
            IGeneratable _generator = _generatedObject.GetComponent<IGeneratable>();
            if (_generator != null)
                _generator.Generate();
        }
    }
}
