public class FightingUnitDisplay
{
    public string Name { get;set; }
    public string Icon { get;set; }

    public void LoadFromConfig(string p_Id)
    {
        DRFightingdisplay t_Row = ConfigManager.GetRow<DRFightingdisplay>(p_Id);
        if(t_Row != null)
        {
            Name = t_Row.Name;
            Icon = "UI/Head/" + t_Row.Icon;
        }
    }
}
