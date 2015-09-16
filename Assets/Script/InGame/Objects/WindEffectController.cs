using UnityEngine;
using System.Collections;
using Enums;

public class WindEffectController : MonoBehaviour
{
    private WindDirection windDirection;

    void Start()
    {
        GetComponent<Wind>().windDirection = windDirection;

        if (windDirection == WindDirection.Left)
        {
            transform.Rotate(0, 270, 270);
        }
        else if (windDirection == WindDirection.Right)
        {
            transform.Rotate(0, 90, 270);
        }
        else if (windDirection == WindDirection.Up)
        {
            transform.Rotate(0, 270, 270);
        }
        else if (windDirection == WindDirection.Down)
        {
            transform.Rotate(0, 90, 270);
        }
    }
}
