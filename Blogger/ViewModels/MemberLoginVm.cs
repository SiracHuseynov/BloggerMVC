﻿using System.ComponentModel.DataAnnotations;

namespace Blogger.ViewModels
{
	public class MemberLoginVm
	{
		[Required]
		public string Username { get; set; }
		[Required]
		[DataType(DataType.Password)]
		[MinLength(8)] 
		public string Password { get; set; }
	}
}
