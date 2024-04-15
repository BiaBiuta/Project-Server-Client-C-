using System;
using System.Collections.Generic;
using System.Data;
using Domain.domain;
using log4net;
using log4net.Util;

namespace Persistence.repository;

public class ChildRepository :IChildRepository
{
    private static readonly ILog log = LogManager.GetLogger("ChildRepository");
    private IDictionary<string, string> props;

    public ChildRepository(IDictionary<string,string>props)
    {
        log.Info("Create childRepository");
        this.props = props;
    }

    public Child FindOne(int integer)
    {
        log.InfoFormat("Entering findOne childRepository with value {0}",integer);
        IDbConnection con = DBUtils.GetConnection(props);
        Child child = null;
        using (var preparedStatement = con.CreateCommand())
        {
            preparedStatement.CommandText="select * from children where id=@id";
            IDataParameter parameter = preparedStatement.CreateParameter();
            parameter.ParameterName = "@id";
            parameter.Value = integer;
            preparedStatement.Parameters.Add(parameter);
            using (var result = preparedStatement.ExecuteReader()) {
                if (result.Read()) {
                    int id = result.GetInt32(0);
                    String name = result.GetString(2);
                    int age = result.GetInt32(1);
                    child = new Child(name, age);
                    child.Id=id;
                    log.InfoFormat("Found {} instances", child);
                }
            }
        } 
        log.InfoExt(child);
        return child;
    }

    public IEnumerable<Child> FindAll()
    {
        throw new NotImplementedException();
    }

    public Child Save(Child entityForAdd)
    {
        log.InfoFormat("saving task {0} ",entityForAdd);
        IDbConnection con =DBUtils.GetConnection(props);
        using(var preparedStatement=con.CreateCommand())
        {
            preparedStatement.CommandText =
                "insert into children(age,name,number_of_samples) values(@age,@name,@number_of_samples)";
            IDataParameter parameter1 = preparedStatement.CreateParameter();
            parameter1.ParameterName = "@age";
            parameter1.Value = entityForAdd.Age;
            preparedStatement.Parameters.Add(parameter1);
            IDataParameter parameter2 = preparedStatement.CreateParameter();
            parameter2.ParameterName = "@name";
            parameter2.Value = entityForAdd.Name;
            preparedStatement.Parameters.Add(parameter2);
            IDataParameter parameter3 = preparedStatement.CreateParameter();
            parameter3.ParameterName = "@number_of_samples";
            parameter3.Value = entityForAdd.NumberOfSamples;
            preparedStatement.Parameters.Add(parameter3);

            var result = preparedStatement.ExecuteNonQuery();
            if (result == 1) {
                preparedStatement.CommandText = "SELECT last_insert_rowid()";
                int id = Convert.ToInt32(preparedStatement.ExecuteScalar());
                entityForAdd.Id = id;
            } else {
                log.Error("Inserarea entității a eșuat");
            }
            log.InfoFormat("Saved {0} instances",result);


        }
        log.InfoExt(entityForAdd);
        return entityForAdd;
    }

    public Child Update(Child entityForUpdate)
    {
        log.InfoFormat("update task {0} ",entityForUpdate);
        IDbConnection con =DBUtils.GetConnection(props);
        using(var preparedStatement=con.CreateCommand())
        {
            preparedStatement.CommandText = "update children set number_of_samples=@number_of_samples where id=@id";
            IDataParameter parameter1 = preparedStatement.CreateParameter();
            parameter1.ParameterName = "@number_of_samples";
            parameter1.Value = entityForUpdate.NumberOfSamples;
            preparedStatement.Parameters.Add(parameter1);
            
            IDataParameter parameter2 = preparedStatement.CreateParameter();
            parameter2.ParameterName = "@id";
            parameter2.Value = entityForUpdate.Id;
            preparedStatement.Parameters.Add(parameter2);
            var result = preparedStatement.ExecuteNonQuery();
           
            log.InfoFormat("Update {0} instances",result);
        }
        
        return entityForUpdate;
    }

    public Child FindByName(string name)
    {
        log.InfoFormat("ChildRepository entry in find by name  ");
        IDbConnection con = DBUtils.GetConnection(props);
        Child child = null;
        using (var preparedStatement = con.CreateCommand())
        {
            preparedStatement.CommandText = "select * from children where name=@name";
            IDataParameter parameter1 = preparedStatement.CreateParameter();
            parameter1.ParameterName = "@name";
            parameter1.Value = name;
            preparedStatement.Parameters.Add(parameter1);
                   
            using (var result = preparedStatement.ExecuteReader()) {
                if (result.Read()) {
                    int id = result.GetInt32(0);
                    String name1 = result.GetString(2);
                    int age = result.GetInt32(1);
                    child = new Child(name1, age);
                    child.Id=id;
                    log.InfoFormat("Found {0} instances", child);
                }
            }
        } 
        return child;
    }
}