using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEst : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(5f);

        Destroy(gameObject);
    }
}
