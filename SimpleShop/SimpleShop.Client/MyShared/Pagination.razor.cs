using Microsoft.AspNetCore.Components;
using SimpleShop.Client.Models;

namespace SimpleShop.Client.MyShared;

public partial class Pagination
{
    private List<PaginationLink> _links;
    private int _linkCount = 1;

    [Parameter]
    public PaginationInfo PaginationInfo { get; set; }

    [Parameter]
    public EventCallback<int> SelectedPage { get; set; }

    protected override void OnParametersSet()
    {
        _links = new List<PaginationLink>();

        //poprzednia
        _links.Add(new PaginationLink { Text = "Poprzednia", PageIndex = PaginationInfo.PageIndex - 1, Enabled = PaginationInfo.HasPreviousPage });

        //1 2 3
        for (int i = 1; i <= PaginationInfo.TotalPages; i++)
        {
            if (PaginationInfo.PageIndex - _linkCount <= i && PaginationInfo.PageIndex + _linkCount >= i)
            {
                _links.Add(new PaginationLink { Text = i.ToString(), PageIndex = i, Enabled = true, Active = PaginationInfo.PageIndex == i });
            }
        }

        //nastepna
        _links.Add(new PaginationLink { Text = "Następna", PageIndex = PaginationInfo.PageIndex + 1, Enabled = PaginationInfo.HasNextPage });
    }

    private async Task OnSelectedPage(PaginationLink item)
    {
        if (item.PageIndex == PaginationInfo.PageIndex ||
                !item.Enabled)
            return;

        PaginationInfo.PageIndex = item.PageIndex;

        await SelectedPage.InvokeAsync(item.PageIndex);
    }
}
