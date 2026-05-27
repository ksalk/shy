
using Shy;

public class ExecutableProviderTests
{
    [Fact]
    public void FindExecutablePathByName_ShouldReturnPath_WhenExecutableExists()
    {
        var path = ExecutableProvider.FindExecutablePathByName("sh");

        Assert.NotNull(path);
        Assert.EndsWith("sh", path);
    }

    [Fact]
    public void FindExecutablePathByName_ShouldReturnNull_WhenExecutableDoesNotExist()
    {
        var path = ExecutableProvider.FindExecutablePathByName("nonexistent_executable_xyz");

        Assert.Null(path);
    }
}
