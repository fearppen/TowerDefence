using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefence.Components;

namespace TowerDefence.Enemy
{
    public enum Directions
    {
        Down,
        Up, 
        Left, 
        Right
    }

    public abstract class Enemy : Component
    {
        public int Hp;
        public int maxHp;
        public int speed;
        public Directions direction;

    }
}
