using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public RawImage ReferenceImage;

    public float Speed;
    public float ZoomMultiplier;
    public float BoostMultiplier;

    private bool isInitialized = false;

    private float maxWidth;
    private float maxHeight;
    private float minDepth;


    private void Start()
    {
        StartCoroutine(LoadBoundaries());
    }

    IEnumerator LoadBoundaries()
    {
        while(ReferenceImage.texture == null)
        {
            yield return new WaitForEndOfFrame();
        }

        maxWidth = ReferenceImage.texture.width;
        maxHeight = ReferenceImage.texture.height;
        minDepth = -(maxWidth + maxHeight) / 2;
        isInitialized = true;

        transform.position = new Vector3(maxWidth / 2, maxHeight / 2, minDepth / 2);
    }


    // Update is called once per frame
    private void Update()
    {
        if (!isInitialized)
            return;

        Move();
        ApplyBoundaries();
    }

    private void Move()
    {
        float currentBoost = Input.GetKey(KeyCode.LeftShift) ? BoostMultiplier : 1;

        Vector3 input = new Vector3(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical"),
            Input.mouseScrollDelta.y * ZoomMultiplier);

        input *= currentBoost;

        transform.Translate(input * Speed * Time.deltaTime);
    }

    private void ApplyBoundaries()
    {
        Vector3 position = transform.position;
        if(position.x < 0)
        {
            position.x = 0;
        }
        else if (position.x > maxWidth)
        {
            position.x = maxWidth;
        }


        if (position.y < 0)
        {
            position.y = 0;
        }
        else if (position.y > maxHeight)
        {
            position.y = maxHeight;
        }

        if (position.z > 0)
        {
            position.z = 0;
        }
        else if (position.z < minDepth)
        {
            position.z = minDepth;
        }

        transform.position = position;
    }
}
