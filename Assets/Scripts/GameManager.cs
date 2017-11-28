using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    [SerializeField]
    private GameObject pearPrefab;

    [SerializeField]
    private Text pearText;

    private int collectedPears;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }

    public GameObject PearPrefab
    {
        get
        {
            return pearPrefab;
        }
    }

    public int CollectedPears
    {
        get
        {
            return collectedPears;
        }
        set
        {
            pearText.text = value.ToString();
            this.collectedPears = value;
        }
    }
}
