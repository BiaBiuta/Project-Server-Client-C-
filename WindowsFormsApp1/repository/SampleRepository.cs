using System;
using System.Collections.Generic;
using System.Data;
using log4net;
using WindowsFormsApp1.domain;

namespace WindowsFormsApp1.repository;

public class SampleRepository:ISampleRepository
{
    private static readonly ILog log = LogManager.GetLogger("ChildRepository");
    private IDictionary<string, string> props;

    public SampleRepository(IDictionary<string, string> props)
    {
        log.Info("Create organizingRepository");
        this.props = props;
        
    }
    public Sample FindOne(int id_sam)
    {
        log.InfoFormat("entry");
        IDbConnection con  = DBUtils.GetConnection(props);
        Sample sample = null;
        using(var preparedStatement=con.CreateCommand())
        {
            preparedStatement.CommandText = "select * from samples where id_sample=@id_sample";
            IDataParameter parameter = preparedStatement.CreateParameter();
            parameter.ParameterName = "@id_sample";
            parameter.Value = id_sam;
            preparedStatement.Parameters.Add(parameter);
            using (var result = preparedStatement.ExecuteReader()){
                if (result.Read()) {
                    int id = result.GetInt32(result.GetOrdinal("id_sample"));
                    String name = result.GetString(result.GetOrdinal("sample_categories"));
                    String age = result.GetString(result.GetOrdinal("age_category"));
                    sample = new Sample(SampleCategoryExtensions.FromString(name), AgeCategoryExtensions.FromString(age));
                    sample.Id = id;
                    log.InfoFormat("Found {} instances",sample);
                }
            }
        } 
        return sample;
    }

    public IEnumerable<Sample> FindAll()
    {
        log.InfoFormat("entry");
        IDbConnection con = DBUtils.GetConnection(props);
        List<Sample>samples= new List<Sample>();
        using(var preparedStatement=con.CreateCommand())
        {
            preparedStatement.CommandText = "select * from samples";
            using (var result = preparedStatement.ExecuteReader()) {
                while (result.Read()) {
                    int id = result.GetInt32(result.GetOrdinal("id_sample"));
                    String name = result.GetString(result.GetOrdinal("sample_categories"));
                    String age = result.GetString(result.GetOrdinal("age_category"));
                    Sample sample = new Sample(SampleCategoryExtensions.FromString(name), AgeCategoryExtensions.FromString(age));
                    sample.Id=id;
                    samples.Add(sample);
                }
            }
        } 
        return samples;
    }

    public Sample Save(Sample entity)
    {
        throw new NotImplementedException();
    }

    public Sample FindOneByCategoryAndAge(string category, string ageCategory)
    {
        log.InfoFormat("entry");
        IDbConnection con  = DBUtils.GetConnection(props);
        Sample sample = null;
        using (var preparedStatement = con.CreateCommand()){
            preparedStatement.CommandText =
                "select * from samples where sample_categories=@sample_categories and age_category=@age_category";
            IDataParameter parameter = preparedStatement.CreateParameter();
            parameter.ParameterName = "@sample_categories";
            parameter.Value = category;
            preparedStatement.Parameters.Add(parameter);
            IDataParameter parameter2 = preparedStatement.CreateParameter();
            parameter2.ParameterName = "@age_category";
            parameter2.Value = ageCategory;
            preparedStatement.Parameters.Add(parameter2);
        
            using (var result = preparedStatement.ExecuteReader()) {
                if (result.Read()) {
                    int id = result.GetInt32(result.GetOrdinal("id_sample"));
                    String name = result.GetString(result.GetOrdinal("sample_categories"));
                    String age = result.GetString(result.GetOrdinal("age_category"));
                    sample = new Sample(SampleCategoryExtensions.FromString(name), AgeCategoryExtensions.FromString(age));
                    sample.Id=id;
                    log.InfoFormat("Found {0} instances",sample);
                }
            }
        } 
        log.InfoFormat("retun {0}",sample);
        return sample;
    }
}