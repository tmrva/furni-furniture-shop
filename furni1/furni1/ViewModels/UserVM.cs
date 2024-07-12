﻿using System.ComponentModel.DataAnnotations.Schema;

namespace furni1.ViewModels
{
	public class UserVM
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Username { get; set; }
		public string Email { get; set; }
		public string Role { get; set; }
		public bool IsDeactive { get; set; }

	}
}
