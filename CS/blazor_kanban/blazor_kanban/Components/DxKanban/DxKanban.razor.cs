using DevExpress.Blazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System.Collections;
using System.ComponentModel;
using System.Reflection;

namespace blazor_kanban.Components.DxKanban;
public partial class DxKanban : ComponentBase, IAsyncDisposable {
    #region Fields
    private IEnumerable sampleSingleCellData = Enumerable.Range(0, 1);
    private IJSObjectReference? jsModule;
    #endregion

    #region Services
    [Inject]
    private IJSRuntime JS { get; set; } = default!;
    #endregion

    #region Parameters
    [Parameter]
    public IEnumerable? Data { get; set; }

    [Parameter]
    public string? CssClass { get; set; }

    [Parameter]
    public string? ColumnNameFieldName { get; set; }

    [Parameter]
    public RenderFragment? Columns { get; set; }

    [Parameter]
    public RenderFragment<object>? CardTemplate { get; set; }

    [Parameter]
    public EventCallback<GridItemsDroppedEventArgs> CardDropped { get; set; }
    #endregion

    #region Event Handlers
    private void ApplyCssClassesToHeaderAndDataCells(GridCustomizeElementEventArgs e) {
        switch(e.ElementType) {
            case GridElementType.HeaderCell:
                e.CssClass = "kanban-header-cell";
                break;
            case GridElementType.DataCell:
                e.CssClass = "kanban-data-cell";
                break;
        }
    }
    #endregion

    #region Lifecycle Methods
    protected override async Task OnAfterRenderAsync(bool firstRender) {
        if(jsModule is null) {
            jsModule = await JS.InvokeAsync<IJSObjectReference>("import", "./Components/DxKanban/DxKanban.razor.js");
        }
        await jsModule.InvokeVoidAsync("moveGridDataCellContentToAnchors");
    }

    public async ValueTask DisposeAsync() {
        try {
            if(jsModule != null) {
                await jsModule.DisposeAsync();
            }
        }
        catch(JSDisconnectedException) { }
    }
    #endregion

    #region Utility Methods
    public void Refresh() => StateHasChanged();

    private string GetGridCssClass() {
        return $"kanban-layout-grid {CssClass}";
    }
    #endregion
}
