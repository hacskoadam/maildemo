using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace demo.Models
{
	public class DemoDB : DbContext
	{
		public DbSet<Block> Blocks { get; set; }
		public DbSet<Message> Mails { get; set; }
		public DbSet<Favorite> Favorites { get; set; }
	}
}