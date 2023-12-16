namespace ACEditor.Table;

public abstract class TableRow
{
    //public int ID { get; set; }
    public string Property { get; set; }
    public string OriginalValue { get; set; }

    public virtual void Render() { }
}

public class BoolRow : TableRow
{

}
public class IntRow : TableRow
{

}
public class FloatRow : TableRow
{

}
public class StringRow : TableRow
{

}