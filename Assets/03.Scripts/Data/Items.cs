using System;

[Serializable]
public class Items 
{
    public string Id;
    public Consts.ItemType Type; 
    public int Hp;
    public int Atk;
    public int Def;
    public int Cri;
    public int Enhanced;
    public long Price;
    public int Grade;                              
    public string UpgradeType;                                    
    public string Icon;                      
}
