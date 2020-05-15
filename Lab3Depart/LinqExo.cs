using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Lab3Depart
{
    public class LinqExo
    {
        public IEnumerable<Fruit> ContientA(IEnumerable<Fruit> fruits)
        {
            return fruits.Where(f => f.Nom.ToUpper().Contains("A"));
        }
        public IEnumerable<Personne> Enfants(IEnumerable<Personne> personnes)
        {
            return personnes.Where(p => p.Age < 18);
        }
        public Personne LaPlusVieille(IEnumerable<Personne> personnes)
        {
            return personnes.OrderByDescending(z => z.Age).First();
        }
        public IEnumerable<Fruit> PlusPopulaire(IEnumerable<Personne> personnes)
        {
            var fruits = personnes.SelectMany(p => p.FruitsAimes);
            return fruits.GroupBy(p => p.Nom).OrderByDescending(f => f.Count()).Select(f => f.First());
        }
        public void ParGenre(IEnumerable<Personne> personnes)
        {
            var queryGenres = from p in personnes
                              group p by p.Genre into g
                              select new { Char = g.Key, Min = g.Min(p => p.Age), Max = g.Max(p => p.Age), Count = g.Count() };

            foreach (var genre in queryGenres)
            {
                Console.WriteLine("Genre: " + genre.Char);
                Console.WriteLine("Nombre de personnes: " + genre.Count);
                Console.WriteLine("Personne la plus agée: " + genre.Max);
                Console.WriteLine("Personne la plus jeune " + genre.Min);
            }
        }
    }
}
