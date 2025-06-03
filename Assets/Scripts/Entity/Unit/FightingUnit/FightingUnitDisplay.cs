public class FightingUnitDisplay
{
    public string Name { get;set; }
    public string Icon { get;set; }

    public static FightingUnitDisplay LoadConfig(string p_Id)
    {
        FightingUnitDisplay t_Display = new();
        DRFightingdisplay t_Row = ConfigManager.GetRow<DRFightingdisplay>(p_Id);
        if(t_Row != null)
        {
            t_Display.Name = t_Row.Name;
            t_Display.Icon = "UI/Head/" + t_Row.Icon;
        }
        return t_Display;
    }
}
