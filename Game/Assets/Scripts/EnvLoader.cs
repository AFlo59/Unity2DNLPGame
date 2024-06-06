using System;
using System.IO;

public static class EnvLoader
{
    public static void LoadEnv(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"Env file not found at path: {filePath}");
        }

        var lines = File.ReadAllLines(filePath);
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
            {
                continue;
            }

            var parts = line.Split('=', 2);
            if (parts.Length != 2)
            {
                throw new FormatException($"Invalid line format in env file: {line}");
            }

            Environment.SetEnvironmentVariable(parts[0], parts[1]);
        }
    }
}
