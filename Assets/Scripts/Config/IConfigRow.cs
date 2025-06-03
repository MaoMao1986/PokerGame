/// <summary>
/// 
/// </summary>
public interface IConfigRow
{
    string Id { get; }
    void ParseDataRow(string[] p_dataRowString, string[] p_Type);
}

public interface IConfigLoad
{
    public void LoadConfig(string p_Id);
}

