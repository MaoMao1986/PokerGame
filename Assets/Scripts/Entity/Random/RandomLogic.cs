using System.Collections.Generic;
using System;
using Unity.VisualScripting;
using System.Linq;
using static UnityEditor.Progress;

public static class RandomLogic
{
    public static Dictionary<string, Reward> m_RewardList;

    public static List<Reward> RandomReward(int p_Id, int p_Num = 1)
    {
        m_RewardList = new Dictionary<string, Reward>();
        for (int i = 1; i <= p_Num; i++)
        {
            m_RandomLogic(p_Id);
        }
        return m_RewardList.Values.ToList();
    }

    private static void m_RandomLogic(int p_LootId)
    {
        DRRandom t_Row = ConfigManager.GetRow<DRRandom>(p_LootId);
        EmRandom_Type t_Type = (EmRandom_Type)t_Row.type;
        int[,] t_ConfigList = t_Row.config;

        switch (t_Type)
        {
            case EmRandom_Type.Probility:
                {
                    for (int i=0;i<t_ConfigList.GetUpperBound(0);i++)
                    {
                        if (RandomManager.GetRandom(1, 10000) <= t_ConfigList[i,4])
                        {
                            int t_Num = RandomManager.GetRandom(t_ConfigList[i, 2], t_ConfigList[i, 3]);
                            if ((EmReward_Type)t_ConfigList[i, 0] == EmReward_Type.RandomGroup)
                            {
                                for(int j = 0; j < t_Num; j++)
                                {
                                    m_RandomLogic(t_ConfigList[i, 1]);
                                }
                            }
                            else
                            {
                                Reward t_Reward = new Reward();
                                t_Reward.Type = (EmReward_Type)t_ConfigList[i, 0];
                                t_Reward.Id = t_ConfigList[i, 1];
                                t_Reward.Num = t_Num;
                                m_AddReward(t_Reward);
                            }
                        }
                    }
                }
                break;
            case EmRandom_Type.Weight:
                {
                    int t_TotalWeight = 0;
                    for (int i = 0; i < t_ConfigList.GetUpperBound(0); i++)
                    {
                        t_TotalWeight += t_ConfigList[i, 4];
                    }
                    int t_RandomNum = RandomManager.GetRandom(1, t_TotalWeight);

                    for (int i = 0; i < t_ConfigList.GetUpperBound(0); i++)
                    {
                        if (t_RandomNum > t_ConfigList[i,4])
                        {
                            t_RandomNum = t_RandomNum - t_ConfigList[i, 4];
                        }
                        else
                        {
                            int t_Num = RandomManager.GetRandom(t_ConfigList[i, 2], t_ConfigList[i, 3]);
                            if ((EmReward_Type)t_ConfigList[i, 0] == EmReward_Type.RandomGroup)
                            {
                                for (int j = 0; j < t_Num; j++)
                                {
                                    m_RandomLogic(t_ConfigList[i, 1]);
                                }
                            }
                            else
                            {
                                Reward t_Reward = new Reward();
                                t_Reward.Type = (EmReward_Type)t_ConfigList[i, 0];
                                t_Reward.Id = t_ConfigList[i, 1];
                                t_Reward.Num = t_Num;
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
        string t_Key = p_Reward.Type.ToString() + Settings.UnderLine + p_Reward.Id.ToString();
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
