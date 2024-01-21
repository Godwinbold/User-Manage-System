using System.ComponentModel.DataAnnotations;

namespace UserManagement_CodeWithSL.Models.ViewModel
{
	public class UserToRegisterViewModel
	{
		[Required]
		[StringLength (15, MinimumLength = 2, ErrorMessage =(" 2 - 15 Character allowed"))]
		public string FirstName { get; set; } = string.Empty;
		[Required]
		[StringLength(15, MinimumLength = 2, ErrorMessage = "2 - 15 Character allowed")]
		public string LastName { get; set; } = string.Empty;
		[Required]
		[EmailAddress]
		public string Email { get; set; } = string.Empty;
		public string PhoneNumber { get; set; } = string.Empty;
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; } = string.Empty;
		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Confirm Password")]
		[Compare ("Password")]
		public string PasswordConfirm { get; set; } = string.Empty;

	}
}