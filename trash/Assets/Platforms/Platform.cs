using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class Platform : MonoBehaviour{
    public GameObject platformSprite;
    public Sprite[] sprites;
    public int xUnits = 3;
    public int yUnits = 3;
    Vector2 spriteSize { get { return sprites[0].bounds.size; } }
    Vector2 platformSize { get { return new Vector2(spriteSize.x * xUnits, spriteSize.y * yUnits); } }

    void Start() {
        GetComponent<BoxCollider2D>().size = platformSize;

        //it would start spawning the sprites from top left
        float xPos = spriteSize.x/2 - platformSize.x/2;
        float yPos = platformSize.y/2 - spriteSize.y/2;

        for(int i = 0; i < yUnits; i++) { 
            for(int t = 0; t < xUnits; t++) {
                GameObject temp = Instantiate(platformSprite, 
                                              new Vector2(xPos + transform.position.x, yPos + transform.position.y), 
                                              Quaternion.identity, 
                                              transform);
                xPos += spriteSize.x;

                Sprite tempSprite = sprites[0];
                if (i == 0 && t < xUnits - 1 && t > 0) { tempSprite = sprites[1]; }
                if (i == 0 && t == xUnits - 1) { tempSprite = sprites[2]; }
                if (i > 0 && i < yUnits - 1 && t == 0) { tempSprite = sprites[3]; }
                if(i > 0 && i < yUnits - 1 && t > 0 && t < xUnits - 1) { tempSprite = sprites[4]; }
                if (i > 0 && i < yUnits - 1 && t == xUnits - 1) { tempSprite = sprites[5]; }
                if (i == yUnits - 1 && t == 0) { tempSprite = sprites[6]; }
                if (i == yUnits - 1 && t > 0 && t < xUnits - 1) { tempSprite = sprites[7]; }
                if (i == yUnits - 1 && t == xUnits - 1) { tempSprite = sprites[8]; }

                temp.GetComponent<SpriteRenderer>().sprite = tempSprite;
            }
            xPos = spriteSize.x/2 - platformSize.x/2;
            yPos -= spriteSize.y;
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.cyan;

        Vector2 topLeft = new Vector2(transform.position.x - platformSize.x/2, transform.position.y + platformSize.y/2);
        Vector2 topRight = new Vector2(transform.position.x + platformSize.x / 2, transform.position.y + platformSize.y / 2);
        Vector2 bottomLeft = new Vector2(transform.position.x - platformSize.x / 2, transform.position.y - platformSize.y / 2);
        Vector2 bottomRight = new Vector2(transform.position.x + platformSize.x / 2, transform.position.y - platformSize.y / 2);

        Gizmos.DrawLine(topLeft, bottomLeft);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(bottomLeft, bottomRight);
    }
}
