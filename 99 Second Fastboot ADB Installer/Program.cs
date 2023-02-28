using System.IO.Compression;

Console.WriteLine("Give me some time we downloading and stuff");

HttpClient client = new();

using (var s = client.GetStreamAsync("https://dl.google.com/android/repository/platform-tools-latest-windows.zip"))
{
    using (var fs = new FileStream(Path.GetTempPath() + @"\ptools.zip", FileMode.CreateNew))
    {
         s.GetAwaiter().GetResult().CopyTo(fs);
    }
}

if (Directory.Exists(@"C:\platform-tools"))
{
    Directory.Delete(@"C:\platform-tools", true);
}

ZipFile.ExtractToDirectory(Path.GetTempPath() + @"\ptools.zip", Path.GetPathRoot(Environment.SystemDirectory), true);

Environment.SetEnvironmentVariable("PATH", Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Machine) + $@";{Path.GetPathRoot(Environment.SystemDirectory)}platform-tools\\", EnvironmentVariableTarget.Machine);

File.Delete(Path.GetTempPath() + @"\ptools.zip");