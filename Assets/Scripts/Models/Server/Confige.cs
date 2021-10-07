using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using System.Data.SqlClient;


public  class Confige  {



    public  SqlConnection ConfigeServer()
    {
        SqlConnection con = null;
         
        con = new SqlConnection("Data Source=150.107.0.78;Initial Catalog=BlueRoute;Persist Security Info=True;User ID=sa;Password=hxsdgz=B");
        
        return con;

        //Debug.Log(i);

    }

}



