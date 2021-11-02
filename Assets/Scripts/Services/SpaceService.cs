using Util;
using System.Collections.Generic;

namespace Service
{
  public class SpaceService
  {
    private SpaceConnection _connection;

    public SpaceService()
    {
      _connection = new SpaceConnection();
    }

    private bool Insert(Model.Space space) => _connection.Insert(space);

    private bool Update(Model.Space space) => _connection.Update(space);

    public bool Save(Model.Space space) => (space.Id <= 0 ? Insert(space) : Update(space));

    public bool DeleteById(long id) => _connection.Delete(id);

    public List<Model.Space> ListAll() => _connection.ListAll();

    public Model.Space GetById(long id) => _connection.GetById(id);
    public bool CheckRepeatedFlat(long flatId) => _connection.CheckRepeatedFlat(flatId);
    public long Count() => _connection.Count();
  }
}
