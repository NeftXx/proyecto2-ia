using System;
using System.Collections.Generic;
using UnityEngine;

namespace Util
{
  public class SpaceConnection : ConnectionDatabase
  {
    public SpaceConnection() : base() { }

    public bool Insert(Model.Space space)
    {
      try
      {
        string query = string.Format(
          Configuration.Queries.Space.Insert,
          space.Name,
          space.FlatId,
          space.UpperLeftId,
          space.UpperRightId,
          space.CenterId,
          space.LowerLeftId,
          space.LowerRightId
        );
        return (ExecuteNonQuery(query) == 1);
      }
      catch (Exception ex)
      {
        ProjectDebug.LogErrorFormat("Insert -> {0}", ex.Message);
        throw ex;
      }
      finally
      {
        CloseConnection();
      }
    }

    public bool Update(Model.Space space)
    {
      try
      {
        string query = string.Format(
          Configuration.Queries.Space.Update,
          space.Name,
          space.FlatId,
          space.UpperLeftId,
          space.UpperRightId,
          space.CenterId,
          space.LowerLeftId,
          space.LowerRightId,
          space.Id
        );
        return (ExecuteNonQuery(query) == 1);
      }
      catch (Exception ex)
      {
        ProjectDebug.LogErrorFormat("Update -> {0}", ex.Message);
        throw ex;
      }
      finally
      {
        CloseConnection();
      }
    }

    public bool Delete(long id)
    {
      try
      {
        string query = string.Format(Configuration.Queries.Space.Delete, id);
        return (ExecuteNonQuery(query) == 1);
      }
      catch (Exception ex)
      {
        ProjectDebug.LogErrorFormat("Delete -> {0}", ex.Message);
        throw ex;
      }
      finally
      {
        CloseConnection();
      }
    }

    public List<Model.Space> ListAll()
    {
      try
      {
        string query = Configuration.Queries.Space.ListAll;
        return Factory<Model.Space>.CreateMany(ExecuteQuery(query));
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

    public Model.Space GetById(long id)
    {
      try
      {
        if (id <= 0)
        {
          throw new Exception(string.Format("Invalid Id -> {0}", id));
        }
        string query = string.Format(Configuration.Queries.Space.GetById, id);
        return Factory<Model.Space>.CreateOne(ExecuteQuery(query));
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

    public bool CheckRepeatedFlat(long flatId)
    {
      try
      {
        if (flatId <= 0)
        {
          throw new Exception(string.Format("Invalid Id -> {0}", flatId));
        }
        string query = string.Format(Configuration.Queries.Space.CheckRepeatedFlat, flatId);
        long result = (long)ExecuteScalar(query);
        return (result > 0);
      }
      catch (Exception ex)
      {
        ProjectDebug.LogErrorFormat("CheckRepeatedFlat -> {0}", ex.Message);
        throw ex;
      }
      finally
      {
        CloseConnection();
      }
    }

    public long Count()
    {
      try
      {
        string query = Configuration.Queries.Space.Count;
        return (long)ExecuteScalar(query);
      }
      catch (Exception ex)
      {
        ProjectDebug.LogErrorFormat("Count -> {0}", ex.Message);
        throw ex;
      }
      finally
      {
        CloseConnection();
      }
    }
  }
}
