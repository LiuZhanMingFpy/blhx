using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AllCharacterCtl
{
    //角色边界限制
    public static void CharacterMove_BoardLimit(Transform character)
    {
        character.position = new Vector3(Mathf.Clamp(character.position.x, -3.5f, 11f), Mathf.Clamp(character.position.y, 2f, 7f), character.position.z);
    }

    //角色子弹边界限制
    public static void BulletMove_BoardLimit(Transform bullet)
    {
        bullet.position = new Vector3(Mathf.Clamp(bullet.position.x, -5f, 12.5f), Mathf.Clamp(bullet.position.y, 0.5f, 12f), bullet.position.z);
        if(bullet.position.x<=-5||bullet.position.x>=12.5f||bullet.position.y<1f||bullet.position.y>11f)
        {
            GameObject.Destroy(bullet.gameObject);
        }

        

    }
}
