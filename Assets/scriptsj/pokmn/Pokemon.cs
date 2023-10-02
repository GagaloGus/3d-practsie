using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Tipo { Normal, Fire, Water, Grass, Flying, Fighting, Poison, Electric, Ground, Rock, Psychic, Ice, Bug, Ghost, Steel, Dragon, Dark, Fairy };
public abstract class Pokemon
{
    private int health, lvl;
    protected string name;
    protected Tipo type = Tipo.Water;

    public Pokemon()
    {
        health = 50;
        lvl = 1;
        name = "pkmn vacio";
        type = Tipo.Normal;
    }

    public Pokemon(int health, int lvl, string name, Tipo type)
    {
        this.health = health;
        this.lvl = lvl;
        this.name = name;
        this.type = type;
    }

    //tiene un ataque por defecto pero se puede cambiar en clases hijas
    public virtual void Attack()
    {
        Debug.Log($"*{name} base tipo {type}*");
    }

    public string pkmn_name { get { return name; } }
    public int pkmn_lvl { get { return lvl; } }
    public Tipo pkmn_type { get { return type; } }
    public int pkmn_health { get { return health; } set { health += value; } }
}


