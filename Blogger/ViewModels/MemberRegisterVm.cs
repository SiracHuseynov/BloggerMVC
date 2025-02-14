﻿using System.ComponentModel.DataAnnotations;

namespace Blogger.ViewModels
{
	public class MemberRegisterVm
	{
		[Required]
		public string Fullname { get; set; }
		[Required]
		public string Username { get; set; } 
		[Required]
		[EmailAddress]
		public string Email { get; set; }
		[Required]
		[DataType(DataType.Password)]
		[MinLength(8)]
		public string Password { get; set; }
		[Required]
		[DataType(DataType.Password)]
		[MinLength(8)]
		[Compare(nameof(Password))]	
		public string RepeatPassword { get; set; }

	}
}
