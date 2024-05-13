namespace Application.Common.Constants
{
	public static class AccountOptions
	{
		public static TimeSpan LoginDuration = TimeSpan.FromDays(2);
		public static TimeSpan RememberMeLoginDuration = TimeSpan.FromDays(7);

		public const string InvalidCredentialsErrorMessage = "Your username or password is invalid";
	}
}
