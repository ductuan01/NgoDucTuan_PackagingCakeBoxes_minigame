using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Transform UpPos;
    public Transform DownPos;
    public Transform LeftPos;
    public Transform RightPos;

    public void MoveUp()
    {
        if (this.UpPos == null) return;
        StartCoroutine(Move(this.UpPos));
    }
    public void MoveDown()
    {
        if (this.DownPos == null) return;
        StartCoroutine(Move(this.DownPos));
    }
    public void MoveLeft()
    {
        if (this.LeftPos == null) return;
        StartCoroutine(Move(this.LeftPos));
    }
    public void MoveRight()
    {
        if (this.RightPos == null) return;
        StartCoroutine(Move(this.RightPos));
    }

    private IEnumerator Move(Transform targetPosition)
    {
        FrameGrid.Instance._canMove = false;
        float moveDuration = 0.5f;
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            float t = elapsedTime / moveDuration;
            transform.position = Vector3.Lerp(transform.position, targetPosition.position, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition.position;
        transform.SetParent(targetPosition);
        FrameGrid.Instance.WinCheck();
        FrameGrid.Instance._canMove = true;
    }
}
