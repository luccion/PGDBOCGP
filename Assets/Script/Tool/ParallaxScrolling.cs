using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ParallaxScrolling : MonoBehaviour
{

    private float _startingPos, //This is the starting position of the sprites.
        _lengthOfSprite; //This is the length of the sprites.
    public float AmountOfParallax; //This is amount of parallax scroll. 
    public Camera MainCamera; //Reference of the camera.
                              //sprite的像素单位
    [SerializeField] float PixelsPerUnit = 48;
    [SerializeField] float tempOffset;
    [SerializeField] float disOffset;



    private void Start()
    {
        //Getting the starting X position of sprite.
        _startingPos = transform.position.x;
        //Getting the length of the sprites.
        _lengthOfSprite = GetComponent<SpriteRenderer>().bounds.size.x;
    }



    private void LateUpdate()
    {
        Vector3 Position = MainCamera.transform.position;
        float Temp = Position.x * (1 - AmountOfParallax) + tempOffset;
        float Distance = Position.x * AmountOfParallax + disOffset;

        Vector3 newPosition = new Vector3(_startingPos + Distance, transform.position.y, transform.position.z);

        //transform.position = NewPosition;
        transform.position = PixelPerfectClamp(newPosition, PixelsPerUnit);
        //Debug.Log(PixelPerfectClamp(newPosition, PixelsPerUnit));
        if (Temp > _startingPos + (_lengthOfSprite / 2))
        {
            _startingPos += _lengthOfSprite;
        }
        else if (Temp < _startingPos - (_lengthOfSprite / 2))
        {
            _startingPos -= _lengthOfSprite;
        }
    }

    private Vector3 PixelPerfectClamp(Vector3 locationVector, float pixelsPerUnit)
    {
        Vector3 vectorInPixels = new Vector3(
            Mathf.RoundToInt(locationVector.x * pixelsPerUnit),
            Mathf.RoundToInt(locationVector.y * pixelsPerUnit),
            Mathf.RoundToInt(locationVector.z * pixelsPerUnit)
        );

        // 将转换回到世界单位
        return vectorInPixels / pixelsPerUnit;
    }

}


