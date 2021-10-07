using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPoorManager : MonoBehaviour
{
    /*
     它是一个池子，里面很多资源，所以属于管理类 应该写成单例模式

     */
    //把所有的资源存到数据结构中  list

    //蓝色子弹
    List<GameObject> bulletList_blue = new List<GameObject>();
    List<GameObject> useList_blue = new List<GameObject>();

    //绿色子弹
    List<GameObject> bulletList_green = new List<GameObject>();
    List<GameObject> useList_green = new List<GameObject>();

    //橙色子弹
    List<GameObject> bulletList_orange = new List<GameObject>();
    List<GameObject> useList_orange = new List<GameObject>();

    //紫色子弹
    List<GameObject> bulletList_purple = new List<GameObject>();
    List<GameObject> useList_purple = new List<GameObject>();

    //红色子弹
    List<GameObject> bulletList_red = new List<GameObject>();
    List<GameObject> useList_red = new List<GameObject>();

    //黄色子弹
    List<GameObject> bulletList_yellow = new List<GameObject>();
    List<GameObject> useList_yellow = new List<GameObject>();

    //boss子弹1
    List<GameObject> bulletList_boss1 = new List<GameObject>();
    List<GameObject> useList_boss1 = new List<GameObject>();

    //boss子弹2
    List<GameObject> bulletList_boss2 = new List<GameObject>();
    List<GameObject> useList_boss2 = new List<GameObject>();

    static BulletPoorManager instance;
    public static BulletPoorManager Instance
    {
        get
        {
            return instance;
        }
    }
    private void Awake()
    {
        instance = this;//instance就是当前对象，脚本挂在谁身上，指的就是谁身上的ObjectPool脚本     
    }
    
    //蓝色子弹使用
    public GameObject GetGo_blue(string path,Transform pos)
    {
        GameObject go = null;
        if (0 == bulletList_blue.Count)//如果池子里没有，就自己实例化吧
        {
            go=BulletProManager.Instance.Bullet_Produce(path,pos);
            go.transform.parent=this.transform;            
        }
        else//如果池子里有，池子是个队列，从第一个开始拿
        {
            go = bulletList_blue[0];
            bulletList_blue.Remove(go);//从资源池子移除
            useList_blue.Add(go); //使用池子增加一枚成员
            go.SetActive(true);
        }
        return go;
    }

    //蓝色子弹回收
    public void ReturnGo_blue(GameObject go)//把谁还回来，肯定有参数
    {
        if (!go)
        {
            return;
        }
        go.SetActive(false);
        useList_blue.Remove(go);
        bulletList_blue.Add(go);
    }

    //绿色子弹使用
    public GameObject GetGo_green(string path, Transform pos)
    {
        GameObject go = null;
        if (0 == bulletList_green.Count)//如果池子里没有，就自己实例化吧
        {
            go = BulletProManager.Instance.Bullet_Produce(path, pos);
            go.transform.parent = this.transform;
        }
        else//如果池子里有，池子是个队列，从第一个开始拿
        {
            go = bulletList_green[0];
            bulletList_green.Remove(go);//从资源池子移除
            useList_green.Add(go); //使用池子增加一枚成员
            go.SetActive(true);
        }
        return go;
    }

    //绿色子弹回收
    public void ReturnGo_green(GameObject go)//把谁还回来，肯定有参数
    {
        if (!go)
        {
            return;
        }
        go.SetActive(false);
        useList_green.Remove(go);
        bulletList_green.Add(go);
    }

    //橙色子弹使用
    public GameObject GetGo_orange(string path, Transform pos)
    {
        GameObject go = null;
        if (0 == bulletList_orange.Count)//如果池子里没有，就自己实例化吧
        {
            go = BulletProManager.Instance.Bullet_Produce(path, pos);
            go.transform.parent = this.transform;
        }
        else//如果池子里有，池子是个队列，从第一个开始拿
        {
            go = bulletList_orange[0];
            bulletList_orange.Remove(go);//从资源池子移除
            useList_orange.Add(go); //使用池子增加一枚成员
            go.SetActive(true);
        }
        return go;
    }

    //橙色子弹回收
    public void ReturnGo_orange(GameObject go)//把谁还回来，肯定有参数
    {
        if (!go)
        {
            return;
        }
        go.SetActive(false);
        useList_orange.Remove(go);
        bulletList_orange.Add(go);
    }


    //紫色子弹使用
    public GameObject GetGo_purple(string path, Transform pos)
    {
        GameObject go = null;
        if (0 == bulletList_purple.Count)//如果池子里没有，就自己实例化吧
        {
            go = BulletProManager.Instance.Bullet_Produce(path, pos);
            go.transform.parent = this.transform;
        }
        else//如果池子里有，池子是个队列，从第一个开始拿
        {
            go = bulletList_purple[0];
            bulletList_purple.Remove(go);//从资源池子移除
            useList_purple.Add(go); //使用池子增加一枚成员
            go.SetActive(true);
        }
        return go;
    }

    //紫色子弹回收
    public void ReturnGo_purple(GameObject go)//把谁还回来，肯定有参数
    {
        if (!go)
        {
            return;
        }
        go.SetActive(false);
        useList_purple.Remove(go);
        bulletList_purple.Add(go);
    }

    //红色子弹使用
    public GameObject GetGo_red(string path, Transform pos)
    {
        GameObject go = null;
        if (0 == bulletList_red.Count)//如果池子里没有，就自己实例化吧
        {
            go = BulletProManager.Instance.Bullet_Produce(path, pos);
            go.transform.parent = this.transform;
        }
        else//如果池子里有，池子是个队列，从第一个开始拿
        {
            go = bulletList_red[0];
            bulletList_red.Remove(go);//从资源池子移除
            useList_red.Add(go); //使用池子增加一枚成员
            go.SetActive(true);
        }
        return go;
    }

    //红色子弹回收
    public void ReturnGo_red(GameObject go)//把谁还回来，肯定有参数
    {
        if (!go)
        {
            return;
        }
        go.SetActive(false);
        useList_red.Remove(go);
        bulletList_red.Add(go);
    }

    //黄色子弹使用
    public GameObject GetGo_yellow(string path, Transform pos)
    {
        GameObject go = null;
        if (0 == bulletList_yellow.Count)//如果池子里没有，就自己实例化吧
        {
            go = BulletProManager.Instance.Bullet_Produce(path, pos);
            go.transform.parent = this.transform;
        }
        else//如果池子里有，池子是个队列，从第一个开始拿
        {
            go = bulletList_yellow[0];
            bulletList_yellow.Remove(go);//从资源池子移除
            useList_yellow.Add(go); //使用池子增加一枚成员
            go.SetActive(true);
        }
        return go;
    }

    //黄色子弹回收
    public void ReturnGo_yellow(GameObject go)//把谁还回来，肯定有参数
    {
        if (!go)
        {
            return;
        }
        go.SetActive(false);
        useList_yellow.Remove(go);
        bulletList_yellow.Add(go);
    }

    //boss子弹1使用
    public GameObject GetGo_boss1(string path, Transform pos)
    {
        GameObject go = null;
        if (0 == bulletList_boss1.Count)//如果池子里没有，就自己实例化吧
        {
            go = BulletProManager.Instance.Bullet_Produce(path, pos);
            go.transform.parent = this.transform;
        }
        else//如果池子里有，池子是个队列，从第一个开始拿
        {
            go = bulletList_boss1[0];
            bulletList_boss1.Remove(go);//从资源池子移除
            useList_boss1.Add(go); //使用池子增加一枚成员
            go.SetActive(true);
        }
        return go;
    }

    //boss子弹1回收
    public void ReturnGo_boss1(GameObject go)//把谁还回来，肯定有参数
    {
        if (!go)
        {
            return;
        }
        go.SetActive(false);
        useList_boss1.Remove(go);
        bulletList_boss1.Add(go);
    }

    //boss子弹2使用
    public GameObject GetGo_boss2(string path, Transform pos)
    {
        GameObject go = null;
        if (0 == bulletList_boss2.Count)//如果池子里没有，就自己实例化吧
        {
            go = BulletProManager.Instance.Bullet_Produce(path, pos);
            go.transform.parent = this.transform;
        }
        else//如果池子里有，池子是个队列，从第一个开始拿
        {
            go = bulletList_boss2[0];
            bulletList_boss2.Remove(go);//从资源池子移除
            useList_boss2.Add(go); //使用池子增加一枚成员
            go.SetActive(true);
        }
        return go;
    }

    //boss子弹1回收
    public void ReturnGo_boss2(GameObject go)//把谁还回来，肯定有参数
    {
        if (!go)
        {
            return;
        }
        go.SetActive(false);
        useList_boss2.Remove(go);
        bulletList_boss2.Add(go);
    }
}
