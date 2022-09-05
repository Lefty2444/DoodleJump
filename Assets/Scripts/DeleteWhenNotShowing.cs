using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteWhenNotShowing : MonoBehaviour
{
    public bool hasBeenShown = false;

    private void OnBecameVisible()
    {
        hasBeenShown = true;
    }

    private void OnBecameInvisible()
    {
        if (hasBeenShown)
        {
            Destroy(gameObject);
        }
    }
}
