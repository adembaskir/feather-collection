using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AtmRush : MonoBehaviour
{
    public static AtmRush instance;
    public float movementDelay = 0.25f;
    public List<GameObject> feather = new List<GameObject>();
    public Mesh ip;
    public Mesh topak;
    public MeshRenderer rope;
    public MeshRenderer top;
    public bool changed;
    private float score;
    public float addValue;
    public Transform target;
    public GameObject myPrefab;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        DOTween.SetTweensCapacity(2000,100);
    }
    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            MoveListElements();
        }
        if (Input.GetButtonUp("Fire1"))
        {
            MoveOrigin();
        }

    }

    public void StackCube(GameObject other, int index)
    {
        other.gameObject.transform.parent = transform;
        Vector3 newPos = feather[index].transform.localPosition;
        newPos.z += 1;
        other.transform.localPosition = newPos;

        other.GetComponent<BoxCollider>().size = new Vector3(1, 1, 1);
        feather.Add(other);
        StartCoroutine(MakeObjectsBigger(other));

    }
    public IEnumerator MakeObjectsBigger(GameObject o)
    {
        for (int i = feather.Count - 1; i > 0; i--)
        {
            int index = i;
            Vector3 scale = new Vector3(1, 1, 1);
            scale *= 1.5f;

            feather[index].transform.DOScale(scale, 0.1f).OnComplete(() =>
             feather[index].transform.DOScale(new Vector3(1, 1, feather[index].transform.localScale.z), 0.1f));
            yield return new WaitForSeconds(0.05f);

        }
    }
    private void MoveListElements()
    {
        for (int i = 1; i < feather.Count; i++)
        {
            Vector3 pos = feather[i].transform.localPosition;
            pos.x = feather[i - 1].transform.localPosition.x;
            feather[i].transform.DOLocalMove(pos, movementDelay);

        }
    }
    private void MoveOrigin()
    {
        for (int i = 1; i < feather.Count; i++)
        {
            Vector3 pos = feather[i].transform.localPosition;
            pos.x = feather[0].transform.localPosition.x;
            feather[i].transform.DOLocalMove(pos, 0.70f);

        }
    }
    public IEnumerator UpgradeCollectableAll(GameObject o)
    {
        
       o.GetComponent<MeshFilter>().mesh = ip;
       o.GetComponent<MeshRenderer>().sharedMaterials = rope.sharedMaterials;
       o.gameObject.tag = "Krem";
        //o.GetComponent<BoxCollider>().size = new Vector3(0.007f, 0.005f, 0.002f);
       o.transform.rotation = Quaternion.Euler(new Vector3(-90, 0, 0));
        //o.GetComponent<BoxCollider>().size = new Vector3(0.2018159f, 0.1987498f, 1.702143f);
       o.transform.localScale = new Vector3(1, 1, 1);
       yield return new WaitForSeconds(0.02f);

        if (o.CompareTag("Krem"))
        {
            score += addValue;
        }


    }
    public IEnumerator UpgradeCollectable(GameObject o)
    {
        o.GetComponent<MeshFilter>().mesh = topak;
        o.GetComponent<MeshRenderer>().sharedMaterials = top.sharedMaterials;
        o.gameObject.tag = "Topak";
        Vector3 scale = gameObject.transform.localScale;
        Vector3 doScale = scale * 1.8f;
        if (o.CompareTag("Topak"))
        {
            score += addValue;
        }

        o.transform.DOScale(doScale, 0.06f).OnComplete(() =>
             o.transform.DOScale(scale, 0.06f));
       
        yield return new WaitForSeconds(0.02f);
    }
    public IEnumerator FeatherCollect(GameObject o)
    {
        if (myPrefab != null)
            Instantiate(myPrefab, feather[0].transform.position, Quaternion.identity);
        o.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.02f);
    }
    public IEnumerator UpgradeOnMachine(GameObject o)
    {
        o.GetComponent<MeshFilter>().mesh = topak;
        o.GetComponent<MeshRenderer>().sharedMaterials = top.sharedMaterials;
        o.gameObject.tag = "Topak";
        Vector3 scale = gameObject.transform.localScale;
        Vector3 doScale = scale * 1.8f;
        o.transform.DOScale(doScale, 0.06f).OnComplete(() =>
             o.transform.DOScale(scale, 0.06f));
        yield return new WaitForSeconds(0.02f);
    }
    public void DistributeCollectibles(GameObject other, int index, GameObject obstacle)
    {
        if (index == 0)
        {
            index = 1;
        }
        for (int i = feather.Count - 1; i > index; i--)
        {
            GameObject gameObject = feather[i];
            feather.Remove(gameObject);
            Destroy(gameObject.GetComponent<Collision>());
            Destroy(gameObject.GetComponent<Rigidbody>());
            gameObject.SetActive(false);

            // BallLauncher ballLauncher=new BallLauncher(gameObject,new Vector3(Random.Range(-2f, 2f), -3.6f, obstacle.transform.position.z + Random.Range(2, 20)));
            // ballLauncher.Launch();
            //gameObject.SetActive(false);
        }
    }
    public void FinishCollecterMoney(GameObject gameObject)
    {
        feather.Remove(gameObject);
        gameObject.transform.DOMove(target.transform.position, movementDelay).OnComplete(() =>gameObject.SetActive(false));
       StartCoroutine(ShaderVariables.instance.SetValue(score));
        
    }
}

