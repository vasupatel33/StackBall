using UnityEngine;

public class DeserializerData : MonoBehaviour
{
    public BaseData data = new BaseData();
    public int currentLevel = 1;
    private void Start()
    {
        string jsonData = ((TextAsset)Resources.Load("jsonmeta/Level_1")).text;

        data = JsonUtility.FromJson<BaseData>(jsonData);

        Debug.Log($"JsonData:: {jsonData}");
    }
}
[System.Serializable]
public class BaseData
{
    public float angle;
    public float rad;
    public float rotatespeed;
    public string color;
    public string depthcolor;
    public float anglediff;
    public float bumpheight;
    public float blockdownspeed;
    public int[] blockconfigX;
    public int[] blockconfigY;
    public int[] blockconfigZ;
    public float pushdownspeed;
}