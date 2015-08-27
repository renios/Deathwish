using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour
{
    float leftX;
    float rightX;
    float downY;
    float upY;
    Player player;

    Vector3 cameraInitialPos = Vector3.zero;
    public Transform cameraTransform;
    public bool scrollY = true;

    void Start()
    {
        leftX = ObjectFinder.FindLeftmost().transform.position.x;
        rightX = ObjectFinder.FindRightmost().transform.position.x;
        downY = ObjectFinder.FindLowest().transform.position.y;
        upY = ObjectFinder.FindUpmost().transform.position.y;
        player = Global.ingame.GetPlayer();
        cameraInitialPos = cameraTransform.transform.position;
    }

    void Update()
    {
        var diffPosX = calculateXScrollDiff();
        var diffPosY = calculateYScrollDiff();
        cameraTransform.transform.position = new Vector3(cameraInitialPos.x + diffPosX,
            cameraInitialPos.y + diffPosY, cameraInitialPos.z);
    }
    
    float calculateXScrollDiff()
    {
        var middleX = (rightX + leftX) / 2;
        var ratioX = Constrain(
          (player.transform.position.x - middleX) / (rightX - leftX),
          min: -1, max: 1);
        var diffPosX = ratioX * 4; // (200 pixel() / (50 pixel per unit)) = 4
        return diffPosX;
    }
    
    float calculateYScrollDiff()
    {
        if (scrollY == false)
        {
            return 0;
        }
        
        var middleY = (upY + downY) / 2;
        var ratioY = Constrain(
            (player.transform.position.y - middleY) / (upY - downY),
            min: -1, max: 1);
        var diffPosY = ratioY * 2.5f; // (125 pixel() / (50 pixel per unit)) = 2.5
        return diffPosY;
    }

    float Constrain(float value, float min, float max)
    {
        if (value <= min)
        {
            return min;
        }
        else if (value >= max)
        {
            return max;
        }
        else
        {
            return value;
        }
    }
}
