using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collision : MonoBehaviour
{
    
    public bool finished;

    public void Gate()
    {
        StartCoroutine(AtmRush.instance.UpgradeCollectable(this.gameObject));
   
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Feather")
        {
            if (!AtmRush.instance.feather.Contains(other.gameObject))
            {
                other.GetComponent<BoxCollider>().isTrigger = false;
                other.gameObject.AddComponent<Collision>();
                other.gameObject.AddComponent<Rigidbody>();
                other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                AtmRush.instance.StackCube(other.gameObject, AtmRush.instance.feather.Count - 1);
                other.gameObject.GetComponentInChildren<ParticleSystem>().Play();
                Handheld.Vibrate();
                

            }
        }
        if (other.tag == "Cat")
        {
            if(this.gameObject.CompareTag("Player"))
            {
                StartCoroutine(AtmRush.instance.FeatherCollect(other.gameObject));
            }
        }
        if (other.tag == "Machine")
        {
            if (this.gameObject.CompareTag("Feather"))
            {
                StartCoroutine(AtmRush.instance.UpgradeOnMachine(this.gameObject));
                Handheld.Vibrate();

            }
            else if (this.gameObject.CompareTag("Topak"))
            {
                StartCoroutine(AtmRush.instance.UpgradeCollectableAll(this.gameObject));
                Handheld.Vibrate();

            }

        }  
        if (other.tag == "Finished")
        {
            if (this.gameObject.tag == "Topak" || this.gameObject.tag == "Ýp" ||this.gameObject.tag == "Krem")
            {
                AtmRush.instance.FinishCollecterMoney(this.gameObject);
            }
            if (this.gameObject.CompareTag("Player"))
            {
                Movement.Instance.enabled = false;
                ShaderVariables.instance.Check();
                if (ShaderVariables.instance.restart ==true)
                {
                    UIManager.instance.UIElements[1].SetActive(true);
                }
            }
        }
        if (other.gameObject.tag == "Trap")
        {
            if (this.gameObject.CompareTag("Feather")||this.gameObject.CompareTag("Topak")||this.gameObject.CompareTag("Krem"))
            {
                for (int i = 0; i < AtmRush.instance.feather.Count - 1; i++)
                {
                    if (AtmRush.instance.feather[i] == this.gameObject)
                    {
                        AtmRush.instance.DistributeCollectibles(other.gameObject, i, this.gameObject);
                        break;
                    }
                }
            }

        }
    }
}
