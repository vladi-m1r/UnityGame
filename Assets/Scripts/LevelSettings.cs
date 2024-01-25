using UnityEngine;

public class LevelSettings : MonoBehaviour
{

    public int level;

    void Start()
    {
        PlayerPrefs.SetInt("currentLevel", this.level + 3);
    }

}