using System;

namespace Model
{
  public class Flat
  {
    public int Id { get; set; }
    public string Name { get; set; }

    public Flat(int id, string name)
    {
      Id = id;
      Name = name;
    }

    public Flat()
    {
    }
  }
}
