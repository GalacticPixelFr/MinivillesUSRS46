using System;
using System.Collections.Generic;

public class Element
{
    public string[] text;
    public Coordinates coordinates;
    public Animation animation;
    public Placement placement;
    public ConsoleColor foreground;
    public ConsoleColor background;

    public Element(Coordinates coordinates, string text)
    {
        this.text = new string[1] {text};
        this.coordinates = coordinates;

        this.animation = Animation.None;
        this.placement = Placement.topLeft;
        this.foreground = ConsoleColor.White;
        this.background = ConsoleColor.Black;

    }

    public Element(string[] text, Coordinates coordinates, Animation animation, Placement placement, ConsoleColor foreground, ConsoleColor background)
    {
        this.text = text;
        this.coordinates = coordinates;
        this.animation = animation;
        this.placement = placement;
        this.foreground = foreground;
        this.background = background;
    }
}

public enum Animation
{
    None,
}

public enum Placement
{
    topLeft,
    topRight,
    botLeft,
    botRight,
    mid
}


public class Coordinates
{
    public int x;
    public int y;

    public Coordinates(int x, int y) {
        this.x = x;
        this.y = y;
    }
}