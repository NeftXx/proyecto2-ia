using Util;
using System.Collections.Generic;

namespace Service
{
  public class FurnitureService
  {
    private FurnitureConnection _connection;

    public FurnitureService()
    {
      _connection = new FurnitureConnection();
    }

    public List<Model.Furniture> ListAll() => _connection.ListAll();

    public Model.Furniture GetById(long id) => _connection.GetById(id);
  }
}
