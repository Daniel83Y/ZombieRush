using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Rock : Object
{
    // Start is called before the first frame update
    [SerializeField] UnityEngine.Vector3 topPosition;
    [SerializeField] UnityEngine.Vector3 bottomPosition;
    [SerializeField] float Speed;
    void Start()
    {
        StartCoroutine(Move(bottomPosition));

    }

    // Update is called once per frame
    protected override void Update()
    {
        if (GameManager.instance.PlayerActive)
        {
            base.Update();
        }

    }

    IEnumerator Move(UnityEngine.Vector3 target)
    {
        while (Mathf.Abs((target - transform.localPosition).y) > 0.20f)
        {
            UnityEngine.Vector3 direction = target.y == topPosition.y ? UnityEngine.Vector3.up : UnityEngine.Vector3.down;
            transform.localPosition += direction * Time.deltaTime * Speed;
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);

        UnityEngine.Vector3 newTarget = target.y == topPosition.y ? bottomPosition : topPosition;
        StartCoroutine(Move(newTarget));
    }
}
