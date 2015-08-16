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
        var middleX = (rightX + leftX) / 2;
        var ratioX = (player.transform.position.x - middleX) / (rightX - leftX);
        var diffPosX = ratioX * 8; // (400 pixel() / (50 pixel per unit)) = 8 

        var middleY = (upY + downY) / 2;
        var ratioY = (player.transform.position.y - middleY) / (upY - downY);
        var diffPosY = ratioY * 5; // (250 pixel() / (50 pixel per unit)) = 8 
        cameraTransform.transform.position = new Vector3(cameraInitialPos.x + diffPosX,
        cameraInitialPos.y + diffPosY, cameraInitialPos.z);
    }
}
