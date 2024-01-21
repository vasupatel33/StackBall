using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject RingPrefab, parent, LastRing, GameOverPanel;

    [SerializeField] float RotateAngle, RingDistance;
    [SerializeField] private Transform mainCamera;
    [SerializeField] private Material BadMaterial;

    [SerializeField] TextMeshProUGUI LevelText;
    [SerializeField] ParticleSystem particle;
    GameObject ring;

    bool isCLicking;
    float playerPos = 4, downSpeed = 50;
    public float rotationAmount = 100f; 
    public float rotationDuration ;
    private void Start()
    {
        CurrentLevelIndex = PlayerPrefs.GetInt("Level",1);
        LevelText.text = CurrentLevelIndex.ToString();
        GenerateLevel();
        RotateRing();
    }
    //[ContextMenu("Generate")]
    //public void GenerateLevel()
    //{
    //    RingLength = PlayerPrefs.GetInt("Ring", 50);
    //    for (int i = 0; i < RingLength; i++)
    //    {
    //        if (i < 49)
    //        {
    //            ring = Instantiate(RingPrefab, new Vector3(0, i * -RingDistance, 0), Quaternion.Euler(0, i * RotateAngle, 0), parent.transform);
    //            for(int j = 0;j< ring.transform.childCount;j++)
    //            {
    //                    ring.transform.GetChild(j).gameObject.tag = "Good";
    //            }
    //        }
    //        else
    //        {
    //            ring = Instantiate(LastRing, new Vector3(0, i * -RingDistance, 0), Quaternion.Euler(0, i * RotateAngle, 0), parent.transform);
    //        }
    //        if (Random.Range(0, 100) < 20)
    //        {
    //            int val = Random.Range(0, ring.transform.childCount);
    //            Debug.Log("Val = " + val);
    //            for (int j = 0; j < ring.transform.childCount; j++)
    //            {
    //                if (val == j)
    //                {
    //                    Debug.Log("Equal");
    //                    continue;
    //                }
    //                //Debug.Log("1 = " + ring.gameObject.transform.GetChild(i).gameObject.name);
    //                ring.gameObject.transform.GetChild(j).GetComponent<MeshRenderer>().material = BadMaterial;
    //                ring.transform.GetChild(j).gameObject.tag = "Bad";
    //            }
    //        }
    //        else
    //        {
    //            Debug.Log("Else working");
    //        }
    //    }
    //}
    int val;
    int count = 0;
    public void GenerateLevel()
    {
        RingLength = PlayerPrefs.GetInt("Ring", 25);
        int goodCount = 0;
        int badCount = 0;

        for (int i = 0; i < RingLength; i++)
        {
            if (i < RingLength - 1)
            {
                ring = Instantiate(RingPrefab, new Vector3(0, i * -RingDistance, 0), Quaternion.Euler(0, i * RotateAngle, 0), parent.transform);
            }
            else
            {
                ring = Instantiate(LastRing, new Vector3(0, i * -RingDistance, 0), Quaternion.Euler(0, i * RotateAngle, 0), parent.transform);
                continue;
            }
            int value = Random.Range(3, 10);
            if (i % 10 < value) // Change pattern after every 10 objects (5 "Good", 5 "Bad")
            {
                Debug.Log("----------");
                if(count != 1)
                {
                    val = Random.Range(0, ring.transform.childCount);
                    Debug.Log("Val is = "+val);
                    count = 1;
                }
                ring.gameObject.transform.GetChild(val).GetComponent<MeshRenderer>().material = BadMaterial;
                ring.transform.GetChild(val).gameObject.tag = "Bad";
                goodCount++;
            }
            else
            {
                count = 0;
                badCount++;
            }
        }
        Debug.Log($"Good Count: {goodCount}, Bad Count: {badCount}");
    }

    [ContextMenu("Clear")]
    public void Clear()
    {
        int childCount = parent.transform.childCount;
        Debug.Log("Count = " + childCount);
        for (int i = 0; i < childCount; i++)
        {
            Debug.Log($"Destroying obj :: {parent.transform.GetChild(0).gameObject}");
            DestroyImmediate(parent.transform.GetChild(0).gameObject);
        }
    }
    public void RotateRing()
    {
        if (parent.transform.childCount > 0)
        {
            parent.transform.DORotate(new Vector3(0f, rotationAmount, 0f), rotationDuration, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear) // Set the easing function to linear
                .OnStepComplete(() => parent.transform.localEulerAngles = new Vector3(0f, rotationAmount, 0f)) // Set the rotation to the starting point at the end of each rotation
                .SetLoops(-1, LoopType.Restart);
        }

    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isCLicking = true;
            particle.Play();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isCLicking = false;
            particle.Stop();
        }
        if (isCLicking)
        {
            transform.DOMoveY(transform.position.y - 1.2f, 0.2f);
        }
        if (transform.position.y < Camera.main.transform.position.y - playerPos)
        {
            Vector3 newCameraPosition = new Vector3(mainCamera.position.x, mainCamera.position.y - downSpeed * Time.deltaTime, mainCamera.position.z);

            mainCamera.position = newCameraPosition;
        }
    }
    int RingLength;
    int CurrentLevelIndex;
    [SerializeField] float Exploforce, radius;
    private void OnCollisionEnter(Collision collision)
    {
        transform.DOMoveY(transform.position.y + 3, 0.6f);
        if (isCLicking)
        {
            if (collision.gameObject.tag == "Good")
            {
                GameObject parent = collision.transform.parent.gameObject;
                Debug.Log("Collision = " + parent.transform.childCount);
                //for (int i = 0; i < parent.transform.childCount; i++)
                //{
                //    Debug.Log("Fordce applied and clicking = " + isCLicking);
                //    Debug.Log("name is = " + parent.transform.GetChild(i).gameObject.name);
                //    //collision.transform.GetChild(i).AddComponent<Rigidbody>();
                //    parent.transform.GetChild(i).GetComponent<Rigidbody>().isKinematic = false;
                //    //parent.transform.GetChild(i).GetComponent<Rigidbody>().AddExplosionForce(10, parent.transform.GetChild(i).transform.position, 100,10,ForceMode.Acceleration);
                //    parent.transform.GetChild(i).GetComponent<Rigidbody>().AddExplosionForce(80, transform.position, 100, 20f, ForceMode.Impulse);
                //    parent.transform.GetChild(i).GetComponent<BoxCollider>().enabled = false;
                //}
                foreach (Transform child in parent.transform)
                {
                    Vector3 exploDir = new Vector3(child.transform.position.x, child.transform.position.y, child.transform.position.z + 100);
                    child.transform.gameObject.GetComponent<MeshCollider>().convex = true;
                    child.transform.gameObject.AddComponent<Rigidbody>();
                    child.transform.gameObject.GetComponent<Rigidbody>().AddExplosionForce(Exploforce, exploDir, radius, 100);
                    child.GetComponent<MeshCollider>().enabled = false;
                    Destroy(child.transform.gameObject, 1.5f);
                }
                //Destroy(collision.transform.parent.gameObject, 1.5f);
                //Debug.Log("If working");
            }
            if (collision.gameObject.tag == "Bad")
            {
                GameOverPanel.SetActive(true);
                Time.timeScale = 0;
            }
            if(collision.gameObject.tag == "Ground")
            {
                CurrentLevelIndex += 1;
                PlayerPrefs.SetInt("Level",CurrentLevelIndex);
                RingLength += 5;
                PlayerPrefs.SetInt("Ring",RingLength);
                SceneManager.LoadScene(0);
            }
        }
    }
}
