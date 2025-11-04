using DevExpress.Blazor;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Collections;
using System.Reflection;

namespace blazor_kanban.Components.DxKanban;
public partial class DxKanban : ComponentBase, IAsyncDisposable {

    #region Fields
    private List<DxKanbanColumn> columns = new List<DxKanbanColumn>();
    private string containerID = $"c_{Guid.NewGuid()}";
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
    public string? GroupFieldName { get; set; }

    [Parameter]
    public RenderFragment? Columns { get; set; }

    [Parameter]
    public RenderFragment<object>? CardTemplate { get; set; }

    [Parameter]
    public EventCallback<KanbanItemsDroppedEventArgs> CardDropped { get; set; }
    #endregion

    #region Lifecycle Methods
    protected override async Task OnAfterRenderAsync(bool firstRender) {
        if(firstRender) {
            jsModule = await JS.InvokeAsync<IJSObjectReference>("import", "./Components/DxKanban/DxKanban.razor.js");
            StateHasChanged();
        }
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

    #region Event Handlers
    private void OnCustomizeElement(GridCustomizeElementEventArgs e) {
        if(e.ElementType == GridElementType.HeaderCell) {
            e.CssClass = "kanban-header-cell";
        }
        if(e.ElementType == GridElementType.DataCell) {
            e.CssClass = "kanban-data-cell";
        }
    }

    private void OnItemsDropped(GridItemsDroppedEventArgs e) {
        CardDropped.InvokeAsync(new KanbanItemsDroppedEventArgs(e));
    }

    #endregion

    #region Utility Methods
    private IList<object> GetColumnData(string? columnName) {
        if(Data is null) {
            return Array.Empty<object>();
        }
        if(string.IsNullOrEmpty(columnName)) {
            throw new ArgumentNullException(nameof(columnName));
        }

        var columnData = Data.OfType<object>()
            .GroupBy(item => GetGroupValue(item))
            .FirstOrDefault(item => item.Key.Equals(columnName));

        if(columnData is null) {
            return Array.Empty<object>();
        }
        return columnData.Select(item => item).ToList();
    }

    private object GetGroupValue(object item) {
        if(string.IsNullOrEmpty(GroupFieldName)) {
            throw new ArgumentNullException(nameof(GroupFieldName));
        }
        return item.GetType().GetProperty(GroupFieldName)?.GetValue(item)!;
    }

    public void AddColumn(DxKanbanColumn column) {
        columns.Add(column);
        StateHasChanged();
    }

    public async Task MoveCardContent(ElementReference container) {
        if(jsModule != null) {
            await jsModule.InvokeVoidAsync("MoveCardContent", container);
        }
    }
    #endregion
}
