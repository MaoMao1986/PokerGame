using System.Collections.Generic;
using System;
using System.Linq;

public static class RandomLogic
{
    public static Dictionary<Tuple<Enum_RewardType, string>, Reward> m_RewardList;

    public static List<Reward> RandomReward(string p_Id, int p_Num = 1)
    {
        m_RewardList = new();
        for (int i = 1; i <= p_Num; i++)
        {
            m_RandomLogic(p_Id);
        }
        return m_RewardList.Values.ToList();
    }

    private static void m_RandomLogic(string p_LootId)
    {
        DRRandom t_Row = ConfigManager.GetRow<DRRandom>(p_LootId);
        string[,] t_ConfigList = t_Row.Randomconfig;

        switch (t_Row.Randomtype)
        {
            case Enum_RandomType.Probility:
                {
                    for (int i=0;i<t_ConfigList.GetUpperBound(0);i++)
                    {
                        Enum_RewardType t_RewardType = (Enum_RewardType)Convert.ToInt32(t_ConfigList[i, 0]);
                        string t_RewardId = t_ConfigList[i, 1];
                        int t_RewardMin = Convert.ToInt32(t_ConfigList[i, 2]);
                        int t_RewardMax = Convert.ToInt32(t_ConfigList[i, 3]);
                        int t_RewardWeight = Convert.ToInt32(t_ConfigList[i, 4]);
                        if (RandomManager.GetRandom(1, 10000) <= t_RewardWeight)
                        {
                            int t_Num = RandomManager.GetRandom(t_RewardMin, t_RewardMax);
                            if (t_RewardType == Enum_RewardType.RandomGroup)
                            {
                                for(int j = 0; j < t_Num; j++)
                                {
                                    m_RandomLogic(t_RewardId);
                                }
                            }
                            else
                            {
                                Reward t_Reward = new()
                                {
                                    Type = t_RewardType,
                                    Id = t_RewardId,
                                    Num = t_Num
                                };
                                m_AddReward(t_Reward);
                            }
                        }
                    }
                }
                break;
            case Enum_RandomType.Weight:
                {
                    int t_TotalWeight = 0;
                    for (int i = 0; i < t_ConfigList.GetUpperBound(0); i++)
                    {
                        t_TotalWeight += Convert.ToInt32(t_ConfigList[i, 4]);
                    }
                    int t_RandomNum = RandomManager.GetRandom(1, t_TotalWeight);

                    for (int i = 0; i < t_ConfigList.GetUpperBound(0); i++)
                    {
                        Enum_RewardType t_RewardType = (Enum_RewardType)Convert.ToInt32(t_ConfigList[i, 0]);
                        string t_RewardId = t_ConfigList[i, 1];
                        int t_RewardMin = Convert.ToInt32(t_ConfigList[i, 2]);
                        int t_RewardMax = Convert.ToInt32(t_ConfigList[i, 3]);
                        int t_RewardWeight = Convert.ToInt32(t_ConfigList[i, 4]);
                        if (t_RandomNum > t_RewardWeight)
                        {
                            t_RandomNum = t_RandomNum - t_RewardWeight;
                        }
                        else
                        {
                            int t_Num = RandomManager.GetRandom(t_RewardMin, t_RewardMax);
                            if (t_RewardType == Enum_RewardType.RandomGroup)
                            {
                                for (int j = 0; j < t_Num; j++)
                                {
                                    m_RandomLogic(t_ConfigList[i, 1]);
                                }
                            }
                            else
                            {
                                Reward t_Reward = new()
                                {
                                    Type = t_RewardType,
                                    Id = t_RewardId,
                                    Num = t_Num
                                };
                                m_AddReward(t_Reward);
                            }
                            break;
                        }
                    }
                }
                break;
        }
    }

    private static void m_AddReward(Reward p_Reward)
    {
         Tuple<Enum_RewardType, string> t_Key = Tuple.Create(p_Reward.Type, p_Reward.Id);
        if (m_RewardList.ContainsKey(t_Key))
        {
            m_RewardList[t_Key].Num += p_Reward.Num;
        }
        else
        {
            m_RewardList.Add(t_Key, p_Reward);
        }
    }
}
