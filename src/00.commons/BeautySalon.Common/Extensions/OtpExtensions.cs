using System.Security.Cryptography;

namespace BeautySalon.Common.Extensions;
public static class OtpExtensions
{
    public static string GenerateOtpCode(this int length)
    {
        if (length <= 0)
            throw new ArgumentException("OTP length must be greater than zero.");

        const string digits = "0123456789";
        char[] otp = new char[length];

        using (var rng = RandomNumberGenerator.Create())
        {
            byte[] buffer = new byte[1];
            for (int i = 0; i < length; i++)
            {
                rng.GetBytes(buffer);
                int index = buffer[0] % digits.Length;
                otp[i] = digits[index];
            }
        }

        return new string(otp);
    }
}
