namespace BeautySalon.Services.Extensions;
public static class PhoneNumberExtensions
{

    public static string NormalizePhoneNumber(this string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
            return string.Empty;

        phone = phone.Trim()
                     .Replace(" ", "")
                     .Replace("-", "")
                     .Replace("(", "")
                     .Replace(")", "");

        // اگر با +98 شروع شده، همان را برگردان
        if (phone.StartsWith("+98"))
            return phone;

        // اگر با 0098 شروع شده، به +98 تبدیل کن
        if (phone.StartsWith("0098"))
            return "+" + phone.Substring(2);

        // اگر با 0 شروع شده، 0 را حذف و +98 اضافه کن
        if (phone.StartsWith("0"))
            return "+98" + phone.Substring(1);

        // اگر هیچکدام نبود، فرض کن شماره داخلی است و +98 اضافه کن
        return "+98" + phone;
    }
}
