using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pot : MonoBehaviour
{
    public bool isBroken;
    public BoolValue boolValue;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        isBroken = boolValue.RuntimeValue;
        anim = GetComponent<Animator>();
        if (isBroken)
        {
            this.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Smash()
    {
        if (!isBroken)
        {
            boolValue.RuntimeValue = isBroken;
            anim.SetBool("smash", true);
            StartCoroutine(breakCo());
        }
    }

    IEnumerator breakCo()
    {
        yield return new WaitForSeconds(.3f);
        this.gameObject.SetActive(false);
    }
}
