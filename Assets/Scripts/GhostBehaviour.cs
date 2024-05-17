using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Ghost))]
public abstract class GhostBehaviour : MonoBehaviour
{
  
    public Ghost ghost { get; private set; }
    public float duration;

    private void Awake()
    {
        ghost = GetComponent<Ghost>();
    }

    public void Enable()
    {
       Enable(duration); //calls other method but with default duration
    }

    public virtual void Enable(float duration)
    {
        enabled = true;

        CancelInvoke(); //make sure it resets everytime
        Invoke(nameof(Disable), duration); //when behaviour is enabled, it automatically will disable again after the duration.

     }

    public virtual void Disable()
    {
        enabled = false;
        CancelInvoke(); //just in case
    }
}
