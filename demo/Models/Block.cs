using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace demo.Models
{
	public class Block
	{
		[Key]
		public int Id { get; set; }

		public string Who { get; set; }

		public string Whom { get; set; }
	}
}