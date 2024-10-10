using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillNode
{
    public string Id { get; private set; }
    public string Name { get; private set; }
    public object Skill { get; private set; }
    public List<string> RequiredSkillds { get; private set; }
    public bool isUnlocked { get; set; }
    public Vector2 Position { get; set; }
    public string SkillSerise { get; private set; }
    public int SkillLevel { get; private set; }
    public bool IsMaxLevel { get; set; }

    public SkillNode(string id, string name, object skill, Vector2 position, string skillSerise, int skillLevel, List<string> requiredSkillds = null)
    {
        Id = id;
        Name = name;
        Skill = skill;
        Position = position;
        SkillSerise = skillSerise;
        RequiredSkillds = requiredSkillds ?? new List<string>();
        isUnlocked = false;
    }
}

public class SkillTree
{
    public List<SkillNode> Nodes { get; private set; } = new List<SkillNode>();
    private Dictionary<string, SkillNode> nodeDictionary;

    public SkillTree()
    {
        Nodes = new List<SkillNode>();
        nodeDictionary = new Dictionary<string, SkillNode>();
    }

    public void AddNode(SkillNode node)
    {
        Nodes.Add(node);
        nodeDictionary[node.Id] = node;
    }

    public bool UnlockSkill(string skillId)
    {
        if(nodeDictionary.TryGetValue(skillId, out SkillNode node))
        {
            if(node.isUnlocked) return false;
            
            foreach(var requiredSkillId in node.RequiredSkillds)
            {
                if (!nodeDictionary[requiredSkillId].isUnlocked)
                {
                    return false;
                }
            }
            node.isUnlocked = true;
            return true;
        }
        return false;
    }

    public bool LockSkill(string skillId)
    {
        if (nodeDictionary.TryGetValue(skillId, out SkillNode node))
        {
            if (!node.isUnlocked) return false;

            foreach (var otherNode in Nodes)
            {
                if (otherNode.isUnlocked && otherNode.RequiredSkillds.Contains(skillId))
                {
                    return false;
                }
            }

            node.isUnlocked = false;
            return true;
        }
        return false;
    }

    public bool IsSkillUnlock(string skillId)
    {
        return nodeDictionary.TryGetValue(skillId,out SkillNode node) && node.isUnlocked;
    }

    public SkillNode GetNode(string skillId)
    {
        nodeDictionary.TryGetValue(skillId, out SkillNode node);
        return node;
    }

    public List<SkillNode> GetAllNodes()
    {
        return new List<SkillNode>(Nodes);
    }
}
