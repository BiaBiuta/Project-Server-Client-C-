using System;
using System.Collections.Generic;
using System.Data;
using log4net;
using WindowsFormsApp1.domain;

namespace WindowsFormsApp1.repository;

public class RegistrationRepository:IRegistrationRepository
{
    private static readonly ILog Log = LogManager.GetLogger("ChildRepository");
    private IDictionary<string, string> props;
    private SampleRepository _sampleRepository;
    private ChildRepository _childRepository;

    public RegistrationRepository(IDictionary<string, string> props, SampleRepository sampleRepository, ChildRepository childRepository)
    {
        Log.Info("Create registrationRepository");
        this.props = props;
        _sampleRepository = sampleRepository;
        _childRepository = childRepository;
    }


    public Registration FindOne(int id_reg)
    {
        Log.InfoFormat("entry");
        Log.InfoFormat("entry in findAll in Redistration Repository");
        IDbConnection con = DBUtils.GetConnection(props);
        Registration reg = null;
        using(var preparedStatement=con.CreateCommand())
        {
            preparedStatement.CommandText = "select * from registration where id_registration=@id_registration";
            IDataParameter parameter = preparedStatement.CreateParameter();
            parameter.ParameterName = "@id_registration";
            parameter.Value = id_reg;
            preparedStatement.Parameters.Add(parameter);
            
            using (var result = preparedStatement.ExecuteReader()) {
            
                if (result.Read()) {
                    int id = result.GetInt32(result.GetOrdinal("id_registration"));
                    int idChild = result.GetInt32(result.GetOrdinal("id_child"));
                    int idSample = result.GetInt32(result.GetOrdinal("id_sample"));
                    Child child = _childRepository.FindOne(idChild);
                    Sample sample = _sampleRepository.FindOne(idSample);
                    reg= new Registration( child, sample);
                    reg.Id = id;
                    Log.InfoFormat("Found {0} instances", reg);
                }
            }
        }

        return reg;
    }
    public Registration FindOneByChildAndSample(int id_chid,int id_sample)
    {
        Log.InfoFormat("entry");
        Log.InfoFormat("entry in findAll in Redistration Repository");
        IDbConnection con = DBUtils.GetConnection(props);
        Registration reg = null;
        using(var preparedStatement=con.CreateCommand())
        {
            preparedStatement.CommandText = "select * from registration where id_child=@id_child and id_sample=@id_sample";
            IDataParameter parameter = preparedStatement.CreateParameter();
            parameter.ParameterName = "@id_child";
            parameter.Value = id_chid;
            preparedStatement.Parameters.Add(parameter);
            IDataParameter parameter2 = preparedStatement.CreateParameter();
            parameter2.ParameterName = "@id_sample";
            parameter2.Value = id_sample;
            preparedStatement.Parameters.Add(parameter2);
            
            using (var result = preparedStatement.ExecuteReader()) {
            
                if (result.Read()) {
                    int id = result.GetInt32(result.GetOrdinal("id_registration"));
                    int idChild = result.GetInt32(result.GetOrdinal("id_child"));
                    int idSample = result.GetInt32(result.GetOrdinal("id_sample"));
                    Child child = _childRepository.FindOne(idChild);
                    Sample sample = _sampleRepository.FindOne(idSample);
                    reg= new Registration( child, sample);
                    reg.Id = id;
                    Log.InfoFormat("Found {0} instances", reg);
                }
            }
        }

        return reg;
    }

    public IEnumerable<Registration> FindAll()
    {
        Log.InfoFormat("entry in findAll in Redistration Repository");
        IDbConnection con = DBUtils.GetConnection(props);
        List<Registration> regs = new List<Registration>();
        using(var preparedStatement=con.CreateCommand())
        {
            preparedStatement.CommandText = "select * from registration";
            using (var result = preparedStatement.ExecuteReader()) {
                while (result.Read()) {
                    int id = result.GetInt32(result.GetOrdinal("id_registration"));
                    int idChild = result.GetInt32(result.GetOrdinal("id_child"));
                    int idSample = result.GetInt32(result.GetOrdinal("id_sample"));
                    Child child = _childRepository.FindOne(idChild);
                    Sample sample = _sampleRepository.FindOne(idSample);
                    Registration reg= new Registration( child, sample);
                    reg.Id = id;
                    regs.Add(reg);
                }
            }
        } 
        return regs;
    }


    public Registration Save(Registration entityForAdd)
    {
        Log.InfoFormat("entry in Save method at RegistrationREpo ");
        IDbConnection con = DBUtils.GetConnection(props);
        using(var preparedStatement=con.CreateCommand())
        {
            preparedStatement.CommandText = "insert into registration(id_child,id_sample) values(@id_child,@id_sample)";
            IDataParameter parameter = preparedStatement.CreateParameter();
            parameter.ParameterName = "@id_child";
            parameter.Value = entityForAdd.Child.Id;
            preparedStatement.Parameters.Add(parameter);
            IDataParameter parameter1 = preparedStatement.CreateParameter();
            parameter1.ParameterName = "@id_sample";
            parameter1.Value = entityForAdd.Sample.Id;
            preparedStatement.Parameters.Add(parameter1);
            int rowsAffected = preparedStatement.ExecuteNonQuery();
            if (rowsAffected == 1) {
                {
                    // Obțineți cheile generate
                    preparedStatement.CommandText = "SELECT last_insert_rowid()";
                    int id = Convert.ToInt32(preparedStatement.ExecuteScalar());
                    entityForAdd.Id = id;
                }
            } else {
                Log.Error("Inserarea entității a eșuat");
            }
        }
        return entityForAdd;
    }
   

    public List<Child> ListChildrenForSample(Sample sample)
    {
        Log.InfoFormat("entry in ListChildrenForSAmple in registrationRepository");
        IDbConnection con = DBUtils.GetConnection(props);
        List<Child> children = new List<Child>();
        using(var preparedStatement=con.CreateCommand())
        {
            preparedStatement.CommandText =
                "select * from registration as r  inner join children as c on r.id_child=c.id where id_sample=@id_sample";
            IDataParameter parameter1=preparedStatement.CreateParameter();
            parameter1.ParameterName = "@id_sample";
            parameter1.Value = sample.Id;
            preparedStatement.Parameters.Add(parameter1);
            using (var result = preparedStatement.ExecuteReader()) {
                while (result.Read()) {
                    int idChild = result.GetInt32(0);
                    int age = result.GetInt32(result.GetOrdinal("age"));
                    String name = result.GetString(result.GetOrdinal("name"));
                    int numberOfSamples = result.GetInt32(result.GetOrdinal("number_of_samples"));
                    Child child = new Child(name, age);
                    child.Id = idChild;
                    child.NumberOfSamples = numberOfSamples;
                    children.Add(child);
                }
            }
        } 
        return children;
    }

    public int NumberOfChildrenForSample(Sample sample)
    {
        Log.Info("entry in NumberOfChildrenForSample in registrationRepository");
        using (var con = DBUtils.GetConnection(props))
        {
            int number = 0;
            using (var preparedStatement = con.CreateCommand())
            {
                preparedStatement.CommandText =
                    "select count(*) as number_children from registration where id_sample=@id_sample";
                var parameter1 = preparedStatement.CreateParameter();
                parameter1.ParameterName = "@id_sample";
                parameter1.Value = sample.Id;
                preparedStatement.Parameters.Add(parameter1);

                using (var result = preparedStatement.ExecuteReader())
                {
                    if (result.Read())
                    {
                        number = result.GetInt32(result.GetOrdinal("number_children"));
                    }
                }
            }
            return number;
        }
    }

}