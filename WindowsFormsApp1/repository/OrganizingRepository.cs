using System;
using System.Collections.Generic;
using System.Data;
using log4net;
using WindowsFormsApp1.domain;

namespace WindowsFormsApp1.repository;

public class OrganizingRepository:IOrganizingRepository
{
    private static readonly ILog log = LogManager.GetLogger("ChildRepository");
    private IDictionary<string, string> props;

    public OrganizingRepository(IDictionary<string, string> props)
    {
        log.Info("Create organizingRepository");
        this.props = props;
        
    }

    public Organizing FindOne(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Organizing> FindAll()
    {
        log.InfoFormat("entry in find by name in OrganizinfRepo");
        IDbConnection con = DBUtils.GetConnection(props);
        List<Organizing> organizings = new List<Organizing>();
        using (var preStm = con.CreateCommand())
        {
            preStm.CommandText = "select * from organizings ";
           
            using (var result = preStm.ExecuteReader()) {
                while (result.Read()) {
                    int id = result.GetInt32(0);
                    String name = result.GetString(1);
                    String usernameToIntroduce = result.GetString(2);
                    String passwordToIntroduce = result.GetString(3);
                    Organizing org = new Organizing(name, usernameToIntroduce, passwordToIntroduce);
                    org.Id=id;
                    organizings.Add(org);
                    
                }
            }
        } 
        log.InfoFormat("return {0} instances", organizings);
        return organizings;
    }
    

    public Organizing Save(Organizing entity)
    {
        throw new NotImplementedException();
    }

    public Organizing FindByName(string username, string password)
    {
        log.InfoFormat("entry in find by name in OrganizinfRepo");
        IDbConnection con = DBUtils.GetConnection(props);
        Organizing org = null;
        using (var preStm = con.CreateCommand())
        {
            preStm.CommandText = "select * from organizings where username=@username and password=@password";
            IDataParameter parameter1=preStm.CreateParameter();
            parameter1.ParameterName = "@username";
            parameter1.Value = username;
            preStm.Parameters.Add(parameter1);
            IDataParameter parameter2=preStm.CreateParameter();
            parameter2.ParameterName = "@password";
            parameter2.Value = password;
            preStm.Parameters.Add(parameter2);
           
            using (var result = preStm.ExecuteReader()) {
                if (result.Read()) {
                    int id = result.GetInt32(0);
                    String name = result.GetString(1);
                    String usernameToIntroduce = result.GetString(2);
                    String passwordToIntroduce = result.GetString(3);
                    org = new Organizing(name, usernameToIntroduce, passwordToIntroduce);
                    org.Id=id;
                    log.InfoFormat("Found {0} instances", org);
                }
            }
        } 
     
        return org!;
    }
}