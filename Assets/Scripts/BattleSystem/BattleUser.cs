using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BattleUser
{
    public Character Character { get; set; }
    public Deck Deck { get; set; }
    public HandCards HandCards { get; set; }

    /// <summary>
    /// 初始化数据
    /// </summary>
    public void Init()
    {

    }
}
