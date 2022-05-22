using Common.Application.Validation;
using System.ComponentModel.DataAnnotations;

namespace Shop.Api.ViewModels.Auth;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Please Enter phone number")]
    [MaxLength(11, ErrorMessage = ValidationMessages.InvalidPhoneNumber)]
    [MinLength(11, ErrorMessage = ValidationMessages.InvalidPhoneNumber)]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "Please enter password")]
    [MinLength(6,ErrorMessage = "Password should be more than 5 characters")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Please enter re-password")]
    [MinLength(6, ErrorMessage = "Password should be more than 5 characters")]
    [Compare(nameof(Password),ErrorMessage = "Passwords are not the same")]
    public string ConfirmPassword { get; set; }
}