using System;

namespace Model
{
  public class Furniture
  {
    public int Id { get; set; }
    public string Name { get; set; }

    public Furniture(int id, string name)
    {
      Id = id;
      Name = name;
    }

    public Furniture()
    {
    }
  }
}
