using Microsoft.AspNetCore.Components;

namespace SimpleShop.Client.MyShared;

public partial class Sort
{
    [Parameter]
    public EventCallback<string> SortChanged { get; set; }

    private void OnSortChanged(ChangeEventArgs e)
    {
        SortChanged.InvokeAsync(e.Value.ToString());
    }
}
