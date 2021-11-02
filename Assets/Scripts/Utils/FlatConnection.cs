using System;
using System.Collections.Generic;
using UnityEngine;

namespace Util
{
  public class FlatConnection : ConnectionDatabase
  {
    public FlatConnection() : base() { }

    public List<Model.Flat> ListAll() 
    {
      try
      {
        string query = Configuration.Queries.Flat.ListAll;
        return Factory<Model.Flat>.CreateMany(ExecuteQuery(query));
      }
      catch (Exception ex)
      {
        ProjectDebug.LogErrorFormat("ListAll -> {0}", ex.Message);
        throw ex;
      }
      finally
      {
        CloseConnection();
      }
    }

    public Model.Flat GetById(long id)
    {
      try
      {
        if (id <= 0) {
          throw new Exception(string.Format("Invalid Id -> {0}", id));
        }
        string query = string.Format(Configuration.Queries.Flat.GetById, id);
        return Factory<Model.Flat>.CreateOne(ExecuteQuery(query));
      }
      catch (Exception ex)
      {
        ProjectDebug.LogErrorFormat("GetById -> {0}", ex.Message);
        throw ex;
      }
      finally
      {
        CloseConnection();
      }
    }
  }
}
