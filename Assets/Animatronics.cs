using System;
using System.Collections.Generic;
using System.Diagnostics;

public class Animatronics
{
    public string name;
    public List<int> route;
    public int level;

    public string Play()
    {
        return name + "가 소음을 일으킵니다.";
    }

    public void LevelUp()
    {
        level++;
    }
} 