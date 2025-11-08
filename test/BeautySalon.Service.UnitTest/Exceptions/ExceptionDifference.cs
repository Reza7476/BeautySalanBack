using BeautySalon.Common.Exceptions;
using BeautySalon.Services.Users.Exceptions;
using Xunit;

namespace BeautySalon.Service.UnitTest.Exceptions;
public class ExceptionDifference
{
    [Fact(Skip ="skip")]
    public void ExceptionGenerator()
    {
        var assembly = System.Reflection.Assembly
            .GetAssembly(typeof(UserNotFoundException));

        var exceptions = assembly!.GetTypes();

        string strException = string.Empty;

        foreach (var i in exceptions)
        {
            if (i.IsSubclassOf(typeof(CustomException)))
            {
                strException += $"\"{i.Name.Replace("Exception", "")}\": " +
                    "\"Translate Exception here\"," +
                    Environment.NewLine;
            }
        }
        strException += "\"UnknownError\": " +
            "\"خطایی رخ داده است، با پشتیبانی تماس بگیرید\"";

        var rootDirectory = TryGetSolutionDirectoryInfo();
        System.IO.File.WriteAllText(
            $"C:/Users/Reza/Desktop/Exceptions/exception.json",
            strException);
    }

    public static DirectoryInfo TryGetSolutionDirectoryInfo()
    {
        var directory = new DirectoryInfo(Directory.GetCurrentDirectory());
        while (!directory!.GetFiles("*.sln").Any())
        {
            directory = directory.Parent;
        }
        return directory;
    }
}
