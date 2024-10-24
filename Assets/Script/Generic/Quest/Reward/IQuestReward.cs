using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyGame.QuestSystem
{
    public interface IQuestReward
    {
        void Grant(GameObject player);
        string GetDescription();
    }
}
