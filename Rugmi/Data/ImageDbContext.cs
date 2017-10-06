using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rugmi.Models;

namespace Rugmi.Data
{
    public partial class ImageDbContext : DbContext
    {

        public DbSet<Image> Images { get; set; }


        public ImageDbContext(DbContextOptions<ImageDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { }



    }
}
