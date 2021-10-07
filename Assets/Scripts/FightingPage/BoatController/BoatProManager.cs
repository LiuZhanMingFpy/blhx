using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatProManager : MonoBehaviour {

    private Transform point1_1;
    private Transform point2_1;
    private Transform point3_1;
    private Transform point4_1;
    private Transform point5_1;
    private Transform point1_2;
    private Transform point2_2;
    private Transform point3_2;
    private Transform point4_2;
    private Transform point5_2;

    void Start () {
        point1_1 = transform.Find("boat_Pro/boat_ProPoint1_1");
        point2_1 = transform.Find("boat_Pro/boat_ProPoint2_1");

        point3_1 = transform.Find("boat_Pro/boat_ProPoint3_1");
        point4_1 = transform.Find("boat_Pro/boat_ProPoint4_1");
        point5_1 = transform.Find("boat_Pro/boat_ProPoint5_1");

        point1_2 = transform.Find("boat_Pro/boat_ProPoint1_2");
        point2_2 = transform.Find("boat_Pro/boat_ProPoint2_2");

        point3_2 = transform.Find("boat_Pro/boat_ProPoint3_2");
        point4_2 = transform.Find("boat_Pro/boat_ProPoint4_2");
        point5_2 = transform.Find("boat_Pro/boat_ProPoint5_2");
       
    }
    //是否已经产生boss
    private bool isProBoss = false;

    //是否已经播放过boss警告动画
    private bool isProWarning = false;

    private float Toltime = 0;

    private float time1 = 0;
    private float time2 = 0;

    void Update () {
        Toltime += Time.deltaTime;

        if (isProBoss)
        {          
            return;
        }

        if (Toltime < 5)
        {
            //
            _Boat1(point1_1, point2_1);
            
        }
        else if (Toltime < 10)
        {
            //
            _Boat2(point2_2, point1_2);

        }
        else if (Toltime < 15)
        {
            //
            _Boat3(point3_1, point5_2);
        }
        else if (Toltime < 20)
        {
            //
            _Boat4(point4_1, point5_2);

        }
        else if (Toltime < 25)
        {
            //
            _Boat5(point4_2, point5_1);

        }
        else if (Toltime < 30)
        {
            _Boat3(point3_1, point3_2);

            _Boat3(point5_1, point5_2);
        }

        if (Toltime>36&& !isProWarning)
        {
            //boss警告动画
            transform.parent.parent.Find("BossWarning").gameObject.SetActive(true);
            isProWarning = true;
        }

        if (Toltime>40)
        {          
            BoatPro("Prefabs/UI/FightingPage/Enemic/boss",point5_2);
            isProBoss = true;
        }
        
	}

    //生成2波船1
    private void _Boat1(Transform point1,Transform point2)
    {    
        time1 += Time.deltaTime;
        time2 += Time.deltaTime;
           
        if (time1 > 3)
        {
            BoatPro("Prefabs/UI/FightingPage/Enemic/boat2", point1, new Vector3(150, 150, 1));
            time1 = 0;
        }
        if (time2 > 3)
        {
            BoatPro("Prefabs/UI/FightingPage/Enemic/boat2", point2, new Vector3(150, 150, 1));
            time2 = 0;
        }
        
    }

    //生成2波船2
    private void _Boat2(Transform point1, Transform point2)
    {
        time1 += Time.deltaTime;
        time2 += Time.deltaTime;
        if (time1 > 3)
        {
            BoatPro("Prefabs/UI/FightingPage/Enemic/boat1", point1, new Vector3(150, 150, 1));
            time1 = 0;
        }
        if (time2 > 4f)
        {
            BoatPro("Prefabs/UI/FightingPage/Enemic/boat1", point2, new Vector3(150, 150, 1));
            time2 = 0;
        }
    }

    //生成2波船3
    private void _Boat3(Transform point1, Transform point2)
    {
        time1 += Time.deltaTime;
        time2 += Time.deltaTime;
        if (time1 > 3f)
        {
            BoatPro("Prefabs/UI/FightingPage/Enemic/boat3", point1, new Vector3(120, 120, 1));
            time1 = 0;
        }
        if (time2 > 3.5f)
        {
            BoatPro("Prefabs/UI/FightingPage/Enemic/boat3", point2, new Vector3(120, 120, 1));
            time2 = 0;
        }
    }

    //生成2波船4
    private void _Boat4(Transform point1, Transform point2)
    {
        time1 += Time.deltaTime;
        time2 += Time.deltaTime;
        if (time1 > 3f)
        {
            BoatPro("Prefabs/UI/FightingPage/Enemic/boat4", point1, new Vector3(150, 150, 1));
            time1 = 0;
        }
        if (time2 > 4f)
        {
            BoatPro("Prefabs/UI/FightingPage/Enemic/boat4", point2, new Vector3(150, 150, 1));
            time2 = 0;
        }
    }

    //生成2波船5
    private void _Boat5(Transform point1, Transform point2)
    {
        time1 += Time.deltaTime;
        time2 += Time.deltaTime;
        if (time1 > 3f)
        {
            BoatPro("Prefabs/UI/FightingPage/Enemic/boat5", point1, new Vector3(150, 150, 1));
            time1 = 0;
        }
        if (time2 > 4f)
        {
            BoatPro("Prefabs/UI/FightingPage/Enemic/boat5", point2, new Vector3(150, 150, 1));
            time2 = 0;
        }
    }


    public void BoatPro(string path,Transform point)
    {
        GameObject go = Resources.Load<GameObject>(path);

        GameObject bullet = Object.Instantiate(go) as GameObject;

        bullet.name = go.name;

        bullet.transform.SetParent(GameObject.Find("/Canvas").transform);

        bullet.transform.SetAsLastSibling();

        bullet.transform.localScale = new Vector3(1, 1, 1);

        (bullet.transform as RectTransform).position = (point as RectTransform).position;

        (bullet.transform as RectTransform).rotation = (point as RectTransform).rotation;

    }

    public void BoatPro(string path, Transform point,Vector3 scale)
    {
        GameObject go = Resources.Load<GameObject>(path);

        GameObject bullet = Object.Instantiate(go) as GameObject;

        bullet.name = go.name;

        bullet.transform.SetParent(GameObject.Find("/Canvas/FightingPage/TolScence/Visuable").transform);

        bullet.transform.SetAsLastSibling();

        bullet.transform.localScale = scale;

        (bullet.transform as RectTransform).position = (point as RectTransform).position;

        (bullet.transform as RectTransform).rotation = (point as RectTransform).rotation;
    }
}
