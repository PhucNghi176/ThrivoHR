namespace EXE201_BE_ThrivoHR.Test.Base;

public class TestFixture : IDisposable
{
    public BaseTestController BaseTestController { get; }

    public TestFixture()
    {
        // Initialize the BaseTestController or other test setup here
        BaseTestController = new BaseTestController();
    }

    public void Dispose()
    {
        // Clean up if necessary
    }
}

