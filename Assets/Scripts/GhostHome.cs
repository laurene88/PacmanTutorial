using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GhostHome : GhostBehaviour
{
    public Transform insideTransform;
    public Transform outsideTransform;


    private void OnEnable()
    {
        StopAllCoroutines();
    }

    private void OnDisable() //initially call to disable this script, it will run coroutine then disable.
    {
        if (gameObject.activeInHierarchy)
        { 
          //  Debug.Log("staring coroutine on " + this.name + " meaning ive been disabled");
            StartCoroutine(ExitTransition());
        }
    }

    private IEnumerator ExitTransition()
    {
        this.ghost.movement.SetDirection(Vector2.up, true); //forced
        this.ghost.movement.rigidbody.isKinematic = true;
        this.ghost.movement.enabled = false;

        //..animting position of ghost
        Vector3 currentPosition = this.transform.position;
        float duration = 0.5f;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            Vector3 newPosition = Vector3.Lerp(currentPosition, this.insideTransform.position, elapsed / duration);
            newPosition.z = currentPosition.z;
            this.ghost.transform.position = newPosition;
            elapsed += Time.deltaTime;
            yield return null;
        }

        elapsed = 0.0f;

        while (elapsed < duration)
        {
            Vector3 newPosition = Vector3.Lerp(insideTransform.position, outsideTransform.position, elapsed / duration);
            newPosition.z = currentPosition.z;
            this.ghost.transform.position = newPosition;
            elapsed += Time.deltaTime;
            yield return null;
        }

        ghost.movement.SetDirection(new Vector2(Random.value < 0.5f ? -1f : 1f, 0f), true);
        this.ghost.movement.rigidbody.isKinematic = false;
        this.ghost.movement.enabled = true;
    }

}
