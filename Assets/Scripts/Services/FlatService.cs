using Util;
using System.Collections.Generic;

namespace Service
{
  public class FlatService
  {
    private FlatConnection _connection;

    public FlatService()
    {
      _connection = new FlatConnection();
    }

    public List<Model.Flat> ListAll() => _connection.ListAll();

    public Model.Flat GetById(long id) => _connection.GetById(id);
  }
}
