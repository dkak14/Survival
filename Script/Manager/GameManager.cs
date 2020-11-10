using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public DataAssetManager Get;
    void Start()
    {
        Instance = this;
    }

    void Update()
    {
        
    }
}
