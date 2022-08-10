using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShaderVariables : MonoBehaviour
{
    public static ShaderVariables instance;
    public Material disolve;
    public float addValue;
    public bool restart = false;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
     
    }
    private void Update()
    {
        //Debug.Log(disolve.GetFloat("Disolve"));
        if (disolve.GetFloat("Disolve") == 0)
        {
            disolve.SetFloat("Disolve", 0.1f);


        }
    }
    public IEnumerator SetValue(float value)
    {
        addValue = disolve.GetFloat("Disolve");
        addValue +=value *Time.deltaTime;
        disolve.SetFloat("Disolve", addValue);
       
        yield return null;
        
    }
    public void Check()
    {
        if (disolve.GetFloat("Disolve") < 15.1f)
        {
            restart = true;
        }
        if (disolve.GetFloat("Disolve") >= 15.1f)
        {
            UIManager.instance.anim.SetBool("Win",true);
            StartCoroutine(WaitAnim());

        }
    }
    public IEnumerator WaitAnim()
    {
        yield return new WaitForSeconds(2f);
        UIManager.instance.UIElements[2].SetActive(true);
    }
}
