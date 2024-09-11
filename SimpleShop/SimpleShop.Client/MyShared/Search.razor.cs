using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace SimpleShop.Client.MyShared;

public partial class Search
{
    private Timer _timer;

    [Parameter]
    public EventCallback<string> SearchValueChanged { get; set; }

    public string SearchValue { get; set; }

    private void OnSearchValueChanged(KeyboardEventArgs e)
    {
        if (_timer != null) 
            _timer.Dispose();

        _timer = new Timer(OnTimerCallback, null, 500, 0);
    }

    private void OnTimerCallback(object state)
    {
        SearchValueChanged.InvokeAsync(SearchValue);

        _timer.Dispose();
    }
}
