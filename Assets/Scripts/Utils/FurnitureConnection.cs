using System;
using System.Collections.Generic;
using UnityEngine;

namespace Util
{
  public class FurnitureConnection : ConnectionDatabase
  {
    public FurnitureConnection() : base() { }

    public List<Model.Furniture> ListAll() 
    {
      try
      {
        string query = Configuration.Queries.Furniture.ListAll;
        return Factory<Model.Furniture>.CreateMany(ExecuteQuery(query));
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

    public Model.Furniture GetById(long id)
    {
      try
      {
        if (id <= 0) {
          throw new Exception(string.Format("Invalid Id -> {0}", id));
        }
        string query = string.Format(Configuration.Queries.Furniture.GetById, id);
        return Factory<Model.Furniture>.CreateOne(ExecuteQuery(query));
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
