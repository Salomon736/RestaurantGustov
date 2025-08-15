namespace Restaurant.Domain.Models;

public abstract class BaseModel
{
    public int id { get; set; }

    protected BaseModel(int id)
    {
        this.id = id;
    }
}