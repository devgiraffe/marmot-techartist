using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour {

    public bool IsHitting
    {
        get
        {
            return hitting;
        }
    }

    public float speed;

    private Transform ownTransform;

    private bool dragging;
    private Vector3 updatedPosition;
    private Rect cameraBounds;

    private HammerSlot slot;

    private Marmot target;
    private Animator animator;

    private bool hitting = false;
    private Transform spriteObj;

    private void Awake()
    {
        ownTransform = transform;
        animator = GetComponent<Animator>();
        SetupCameraBounds();
    }

    public Hammer Setup(HammerSlot slot)
    {
        spriteObj = transform.Find("Sprite");
        this.slot = slot;
        ownTransform = transform;
        ownTransform.position = slot.transform.position + (Vector3.forward * -1f);
        updatedPosition = ownTransform.position - spriteObj.localPosition;

        return this;
    }

    private void SetupCameraBounds()
    {
        Camera camera = Camera.main;
        float height = 2 * Camera.main.orthographicSize;
        float width = height * Camera.main.aspect;

        cameraBounds = new Rect(camera.transform.position.x - (width / 2), camera.transform.position.y - (height / 2), width, height);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
     /*   if(collision.gameObject.tag == "Marmot")
        {
            target = collision.gameObject.GetComponent<Marmot>();
        }*/
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        /*if (!hitting)
        {
            target = null;
        }*/
    }

    private void OnMouseDown()
    {
        dragging = true;
    }

    private void OnMouseDrag()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (cameraBounds.Contains(mousePos) && dragging)
        {
            updatedPosition = mousePos;
            updatedPosition.z = -1;
        }
    }

    public void SetTarget(Marmot marmot)
    {
        target = marmot;
    }

    private void FixedUpdate()
    {
        UpdatePosition();
    }

    private void OnMouseUp()
    {
        dragging = false;

        if (target)
        {
            if (target.CanHit())
            {
                hitting = true;

                HitTheMarmot();
                return;
            }
        }

        updatedPosition = (slot.transform.position - spriteObj.localPosition) + (Vector3.forward * -1f);
    }

    private void HitTheMarmot()
    {
        updatedPosition = target.hammerPoint.position + (Vector3.forward * -1f);
        animator.SetTrigger("Hit");

        Invoke("WaitToPlayHit", 0.4f);
    }

    private void WaitToPlayHit()
    {
        target.TakeHit();
        Invoke("WaitDestroy", 1f);
    }

    private void WaitDestroy()
    {
        Destroy(gameObject);
        slot.Spawn();
    }

    private void UpdatePosition()
    {
        ownTransform.position = Vector3.Lerp(ownTransform.position, updatedPosition, (Time.fixedDeltaTime * speed));
    }

}
