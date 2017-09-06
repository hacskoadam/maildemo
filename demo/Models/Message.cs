using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace demo.Models
{
	public class Message
	{
		[Key]
		public int Id { get; set; }

		public string From { get; set; }

		public string To { get; set; }

		[Display(Name ="Subject")]
		public string Title { get; set; }

		public bool IsReaded { get; set; }

		public DateTime SendDate { get; set; }

		[Display(Name ="Message body")]
		[DataType(DataType.MultilineText)]
		public String Body { get; set; }
	}
}