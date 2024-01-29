using UnityEngine;

public class LevelSettings : MonoBehaviour
{

    public int level;

    void Awake()
    {
        PlayerPrefs.SetInt("currentLevel", this.level);
    }

}