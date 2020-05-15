using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab3Depart
{
    public class Program
    {
        static void Main(string[] args)
        {
            var fruits = new List<Fruit>()
            {
                new Fruit() { Nom = "Abricot"},   new Fruit() { Nom = "Banane"},    new Fruit() { Nom = "Cerise"},  new Fruit() { Nom = "Datte"},
                new Fruit() { Nom = "Framboise"}, new Fruit() { Nom = "Grenade"},   new Fruit() { Nom = "Kiwi"},    new Fruit() { Nom = "Lime"},
                new Fruit() { Nom = "Mangue"},    new Fruit() { Nom = "Nectarine"}, new Fruit() { Nom = "Olive"},   new Fruit() { Nom = "Pomme"}
            };

            var personnes = new List<Personne>()
            {
                new Personne() { Nom = "Alice", Genre = 'F', Age = 22,   FruitsAimes = new List<Fruit>() { fruits[0], fruits[1], fruits[10] } },
                new Personne() { Nom = "Bob", Genre = 'M', Age = 12,     FruitsAimes = new List<Fruit>() { fruits[4], fruits[5], fruits[6], fruits[7], fruits[8] } },
                new Personne() { Nom = "Charlie", Genre = 'M', Age = 31, FruitsAimes = new List<Fruit>() { fruits[0], fruits[1], fruits[4], fruits[11] } },
                new Personne() { Nom = "Diane", Genre = 'F', Age = 45,   FruitsAimes = new List<Fruit>() { fruits[2], fruits[4] } },
                new Personne() { Nom = "Eve", Genre = 'F', Age = 4,      FruitsAimes = new List<Fruit>() { } },
            };

            LinqExo le = new LinqExo();

            Console.WriteLine("Les fruits qui contiennent la lettre A sont : ");
            IEnumerable<Fruit> reponse = le.ContientA(fruits);
            Console.WriteLine($"{string.Join(separator: ", ", values: reponse)}");
            //1//
            Console.WriteLine("les personnes mineures sont:");
            var queryEnfant = from p in personnes
                              where p.Age < 18
                              select p.Nom;
            Console.WriteLine($"{string.Join(separator: ", ", values: queryEnfant)}");
            //2//
            Console.WriteLine("La personne la plus agée est:");
            var queryOldest = (from p in personnes
                               orderby p.Age descending
                               select p).Take(1);
            Console.WriteLine($"{string.Join(separator: ", ", values: queryOldest)}");
            //3//
            Console.WriteLine("Liste des fruits en ordre de popularité : ");
            var queryPopular = from p in personnes
                               from f in p.FruitsAimes
                               group f by f.Nom into f1
                               orderby f1.Count() descending
                               select f1.First();
            Console.WriteLine($"{string.Join(separator: ", ", values: queryPopular)}");
            //4//
            le.ParGenre(personnes);

            Console.ReadKey();
        }
    }
}
