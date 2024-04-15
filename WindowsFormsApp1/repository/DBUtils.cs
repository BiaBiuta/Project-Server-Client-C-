using System.Collections.Generic;
using System.Data;
using WindowsFormsApp2;

namespace WindowsFormsApp1.repository;

public static class DBUtils
{
		

    private static IDbConnection instance = null;


    public static IDbConnection GetConnection(IDictionary<string,string> props)
    {
       
            instance = GetNewConnection(props);
            instance.Open();
       
        return instance;
    }

    private static IDbConnection GetNewConnection(IDictionary<string,string> props)
    {
			
        return ConnectionFactory.getInstance().createConnection(props);


    }
}