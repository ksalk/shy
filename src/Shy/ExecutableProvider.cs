using System;
using System.IO;

namespace Shy;

public static class ExecutableProvider
{
    public static string? FindExecutablePathByName(string commandName)
    {
        const string pathEnvVarName = "PATH";
        var pathEnvVar = Environment.GetEnvironmentVariable(pathEnvVarName);

        if (string.IsNullOrWhiteSpace(pathEnvVar))
        {
            return null;
        }

        var pathDirectories = pathEnvVar.Split(Path.PathSeparator);

        foreach (var pathDirectory in pathDirectories)
        {
            var filePath = Path.Combine(pathDirectory, commandName);
            if (!Path.Exists(filePath))
                continue;

            // TODO: possibly check other execute modes
            // TODO: this is linux/macos only
            var unixFileMode = File.GetUnixFileMode(filePath);
            if (!unixFileMode.HasFlag(UnixFileMode.UserExecute))
                continue;

            return filePath;
        }

        return null;
    }
}