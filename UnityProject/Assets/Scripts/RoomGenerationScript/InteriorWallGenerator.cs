using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InteriorWallGenerator : MonoBehaviour, IGenerate
{
    public enum WallType { Separator, Closet, LargeInteriorRoom}

    private GameObject prefab;
    private Vector3 startPos;
    private Vector3 startRot;

    private WallType typeOfWall;

    // Start is called before the first frame update
    void Start()
    {
        typeOfWall = WallType.Separator;
        //typeOfWall = (WallType)UnityEngine.Random.Range(0, Enum.GetNames(typeof(WallType)).Length);
    }

    public void Initialize(GameObject pPrefab, Vector3 pStartPos, Vector3 pStartRot)
    {
        prefab = pPrefab;
        startPos = pStartPos;
        startRot = pStartRot;
    }

    public void Generate()
    {
        switch (typeOfWall)
        {
            case WallType.Separator:
                int _dirToSeparate = GeneratorManager.Instance.GetVerticalLenght < GeneratorManager.Instance.GetHorizontalLenght ?
                    GeneratorManager.Instance.GetVerticalLenght : GeneratorManager.Instance.GetHorizontalLenght;

                Renderer _wallRenderer = prefab.GetComponent<Renderer>();
                float _wallOffset = _wallRenderer.bounds.size.x;

                for (int i = 0; i < _dirToSeparate; i++)
                {

                    GameObject newWall = Instantiate(prefab, new Vector3((i * _wallOffset), 0.0f, startPos.z), Quaternion.Euler(startRot));

                    GeneratorManager.Instance.SeparatingWalls.Add(newWall);
                    
                                       
                    if (startRot.y == 0)
                    {
                        Vector3 _newPos = newWall.transform.position;
                        _newPos.x += _wallOffset;

                        newWall.transform.position = _newPos;
                    }
                }

                break;
            case WallType.Closet:
                break;
            case WallType.LargeInteriorRoom:
                break;
            default:
                break;
        }
    }
}
