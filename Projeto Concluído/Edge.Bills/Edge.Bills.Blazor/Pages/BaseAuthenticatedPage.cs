namespace Edge.Bills.Blazor.Pages
{
    public class BaseAuthenticatedPage : BasePage
    {
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender && !await IsAuthenticated)
            {
                NavigationManager.NavigateTo("/login");
            }
            await base.OnAfterRenderAsync(firstRender);
        }
    }
}
