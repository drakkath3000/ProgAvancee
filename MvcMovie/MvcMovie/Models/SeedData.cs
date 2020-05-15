using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcMovie.Data;
using System;
using System.Linq;

namespace MvcMovie.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MvcMovieContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MvcMovieContext>>()))
            {
                // Look for any movies.
                if (context.Suckle.Any())
                {
                    return;   // DB has been seeded
                }

                context.Suckle.AddRange(
                    new Suckle
                    {
                        SuckleTime = DateTime.Parse("2020-5-14"),
                        Side = "Left",
                    }

                );
                context.SaveChanges();
            }
        }
    }
}