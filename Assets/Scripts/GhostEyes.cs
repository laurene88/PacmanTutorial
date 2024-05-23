using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEyes : MonoBehaviour
{
    public Sprite up;
    public Sprite down;
    public Sprite left;
    public Sprite right;
    public SpriteRenderer eyeRenderer { get; private set; }
    public Movement movement { get; private set; }

    public void Awake()
    {
        this.eyeRenderer = GetComponent<SpriteRenderer>();
        this.movement = GetComponentInParent<Movement>();

    }

    public void Update()
    {
        if (this.movement.direction == Vector2.up)
        {
            eyeRenderer.sprite = this.up;
        }
        if (this.movement.direction == Vector2.down)
        {
            eyeRenderer.sprite = this.down;
        }
        if (this.movement.direction == Vector2.left)
        {
            eyeRenderer.sprite = this.left;
        }
        if (this.movement.direction == Vector2.right)
        {
            eyeRenderer.sprite = this.right;
        }
    }


}
