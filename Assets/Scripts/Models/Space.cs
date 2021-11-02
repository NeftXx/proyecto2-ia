using System;

namespace Model
{
  public class Space
  {
    public long Id { get; set; }
    public string Name { get; set; }
    public long FlatId { get; set; }
    public long UpperLeftId { get; set; }
    public long UpperRightId { get; set; }
    public long CenterId { get; set; }
    public long LowerLeftId { get; set; }
    public long LowerRightId  { get; set; }

    public Space()
    {
    }

    public Space(
      long id,
      string name,
      long flatId,
      long upperLeftId,
      long upperRightId,
      long centerId,
      long lowerLeftId,
      long lowerRightId
    )
    {
      Id = id;
      Name = name;
      FlatId = flatId;
      UpperLeftId = upperLeftId;
      UpperRightId = upperRightId;
      CenterId = centerId;
      LowerLeftId = lowerLeftId;
      LowerRightId = lowerRightId;
    }
  }
}
