using System;

public class LoadingScope: IDisposable
{
    private readonly LoadingScreen loadingScreen;
    public LoadingScope(LoadingScreen loadingScreen)
    {
        this.loadingScreen = loadingScreen;
        loadingScreen.ShowLoading();
    }

    public void Dispose()
    {
        loadingScreen.HideLoading();
    }
}