using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeObjectGenerator : MonoBehaviour, IGenerate
{
    private GeneratableObject prefab;
    private Vector3 startPos;
    private Transform parent;

    public void Initialize(GeneratableObject pPrefab, Vector3 pStartPos, Transform pParent)
    {
        prefab = pPrefab;
        //startPos = pStartPos;
        parent = pParent;
    }

    public void Generate()
    {
        int randomBlock = Random.Range(0,100);

        if(randomBlock >= 50)
        {
            GameObject _generatedObject = Instantiate(prefab.Prefab, parent);

            if(prefab.GenerateOnHeight)
            {
                startPos.y += Random.Range(1.5f, 2.5f);
            }

            Renderer _renderer = prefab.Prefab.GetComponent<Renderer>();

            startPos.z += _renderer ? _renderer.bounds.size.z : 0.15f;

            _generatedObject.transform.localPosition = startPos;

            _generatedObject.transform.rotation = parent.rotation;

            if(prefab.Prefab.name.Contains("bed"))
            {
                _generatedObject.transform.eulerAngles += new Vector3(0, 90, 0);
            }

            IGenerate _generator = _generatedObject.GetComponent<IGenerate>();
            if (_generator != null)
                _generator.Generate();
        }
    }
}
