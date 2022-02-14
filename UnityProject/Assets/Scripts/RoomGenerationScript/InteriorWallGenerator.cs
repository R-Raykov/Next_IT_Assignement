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
                int _dirToSeparate = GeneratorManager.Instance.GetVerticalLenght <= GeneratorManager.Instance.GetHorizontalLenght ?
                    GeneratorManager.Instance.GetVerticalLenght : GeneratorManager.Instance.GetHorizontalLenght;

                Renderer _wallRenderer = prefab.GetComponent<Renderer>();
                float _wallOffset = _wallRenderer.bounds.size.x;

                for (int i = 0; i < _dirToSeparate; i++)
                {

                    Vector3 _newPos = Vector3.zero;

                    if(_dirToSeparate == GeneratorManager.Instance.GetHorizontalLenght)
                    {
                        _newPos = new Vector3((i * _wallOffset), 0.0f, startPos.z);
                    }
                    else if(_dirToSeparate == GeneratorManager.Instance.GetVerticalLenght)
                    {
                        _newPos = new Vector3(startPos.x, 0.0f, (i * _wallOffset));
                    }

                    GameObject newWall = Instantiate(prefab, _newPos, Quaternion.Euler(startRot));

                    GeneratorManager.Instance.SeparatingWalls.Add(newWall);


                    if (Mathf.RoundToInt(startRot.y) == 0)
                    {
                        _newPos = newWall.transform.localPosition;
                        _newPos.x += _wallOffset;

                        newWall.transform.localPosition = _newPos;
                    }
                    else if (Mathf.RoundToInt(startRot.y) == 270)
                    {
                        _newPos = newWall.transform.localPosition;
                        _newPos.z += _wallOffset;

                        newWall.transform.localPosition = _newPos;
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
