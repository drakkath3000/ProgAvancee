using System;
using System.Collections.Generic;
using System.Text;

namespace Lab3Depart
{
    public class Fruit
    {
        public string Nom { get; set; }

        public override bool Equals(object obj)
        {
            Fruit fruit = obj as Fruit;
            return fruit != null &&
                   Nom == fruit.Nom;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Nom);
        }

        public override string ToString()
        {
            return Nom;
        }

    }
}
