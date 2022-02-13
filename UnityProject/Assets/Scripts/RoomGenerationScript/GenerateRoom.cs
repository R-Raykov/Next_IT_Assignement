using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRoom : MonoBehaviour, IGenerate
{
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameObject groundPrefab;
    [SerializeField] private GameObject doorPrefab;

    private void Start()
    {
        Generate();
    }

    public void Generate()
    {
        GameObject _wallParent = new GameObject("WallParent");
        GameObject _floorParent = new GameObject("FloorParent");

        Renderer _wallRenderer = wallPrefab.GetComponent<Renderer>();
        Renderer _groundRenderer = groundPrefab.GetComponent<Renderer>();

        float _wallOffset = _wallRenderer.bounds.size.x;

        float _groundOffset = _groundRenderer.bounds.size.x;

        for (int w = 0; w <= GeneratorManager.Instance.GetHorizontalLenght; w++)
        {
            for (int h = 0; h <= GeneratorManager.Instance.GetVerticalLenght; h++)
            {
                if (w == 0 || h == 0 ||
                    w == GeneratorManager.Instance.GetHorizontalLenght || h == GeneratorManager.Instance.GetVerticalLenght)
                {
                    Vector3 _newRot = Vector3.zero;

                    if (w == GeneratorManager.Instance.GetHorizontalLenght)
                    {
                        _newRot.y = -90;
                    }
                    else if (w == 0)
                    {
                        _newRot.y = 90;
                    }

                    if (h == GeneratorManager.Instance.GetVerticalLenght && w != GeneratorManager.Instance.GetHorizontalLenght)
                    {
                        _newRot.y = 180;
                    }
                    else if (h == 0 && w != 0)
                    {
                        _newRot.y = 0;
                    }

                    GameObject newWall = Instantiate(wallPrefab, new Vector3(w * _wallOffset, 0, h * _wallOffset),
                                                        Quaternion.Euler(_newRot), _wallParent.transform);

                    if (h == GeneratorManager.Instance.GetVerticalLenght / 2)
                    {
                        InteriorWallGenerator _wallGenerator = new InteriorWallGenerator();
                        _newRot.y += 90; 
                        _wallGenerator.Initialize(wallPrefab, newWall.transform.position, _newRot);
                        _wallGenerator.Generate();
                    }


                    LargeObjectGenerator _randomItem = new LargeObjectGenerator();

                    _randomItem.Initialize(GenerateRandomItem(), newWall.transform.position, newWall.transform);
                    _randomItem.Generate();

                    if (w == 0 || h == 0)
                        continue;

                }


                GameObject newGround = Instantiate(groundPrefab, new Vector3(w * _groundOffset, 0, h * _groundOffset),
                                                        Quaternion.identity, _floorParent.transform);

            }
        }

        int _separatingWallDoor = Random.Range(GeneratorManager.Instance.SeparatingWalls.Count/2 + 2, 
            GeneratorManager.Instance.SeparatingWalls.Count/2 + GeneratorManager.Instance.GetHorizontalLenght - 1);

        GeneratorManager.Instance.SeparatingWalls[_separatingWallDoor].SetActive(false);
        GeneratorManager.Instance.SeparatingWalls[_separatingWallDoor - GeneratorManager.Instance.GetHorizontalLenght].SetActive(false);

        Vector3 _doorPos = GeneratorManager.Instance.SeparatingWalls[_separatingWallDoor].transform.position;
        _doorPos.x -= _wallOffset / 2;
        _doorPos.z += _wallRenderer.bounds.extents.z;

        GenerateDoor(_doorPos, GeneratorManager.Instance.SeparatingWalls[_separatingWallDoor].transform.rotation);

    }

    private GeneratableObject GenerateRandomItem()
    {
        int _count = GeneratorManager.Instance.GetGenerators.Count;

        int _rng = Random.Range(0, _count);

        GeneratableObject generatable = GeneratorManager.Instance.GetGenerators[_rng];

        return generatable;
    }

    private void GenerateDoor(Vector3 pPos, Quaternion pRotation)
    {
        Instantiate(doorPrefab, pPos, pRotation);
    }
}
