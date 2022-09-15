using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Gun
{
    protected override IEnumerator TryFire()
    {
        while (true)
        {
            if (state == State.Ready)
            {
                if (curBullet > 0)
                {
                    yield return new WaitUntil(() => Input.GetMouseButton(0));
                    state = State.Shoot;
                    LeftClick();

                    yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
                    state = State.Ready;
                }
                else
                    StartCoroutine(Reloading());

            }
            yield return new WaitForSeconds(data.attackDelay);
        }
    }
}
