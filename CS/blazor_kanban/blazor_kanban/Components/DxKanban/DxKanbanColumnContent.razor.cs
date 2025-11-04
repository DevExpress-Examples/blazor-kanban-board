using DevExpress.Blazor;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace blazor_kanban.Components.DxKanban;
public partial class DxKanbanColumnContent : ComponentBase {
    #region Fields
    private ElementReference columnContainer;
    #endregion

    #region Parameters
    [CascadingParameter]
    private DxKanban? Kanban { get; set; }

    [Parameter]
    public string? ColumnName { get; set; }

    [Parameter]
    public IList<object>? ColumnData { get; set; }

    [Parameter]
    public EventCallback<GridItemsDroppedEventArgs> ItemsDropped { get; set; }
    #endregion

    #region Event Handlers
    private void OnCustomizeElement(GridCustomizeElementEventArgs e) {
        if(e.ElementType == GridElementType.RowDragAnchorCell) {
            e.CssClass = "kanban-drag-anchor";
        }
        if(e.ElementType == GridElementType.DragHint) {
            e.CssClass = "kanban-drag-hint";
        }
    }
    #endregion

    #region Lifecycle Methods
    protected override async Task OnAfterRenderAsync(bool firstRender) {
        if(Kanban != null) {
            await Kanban.MoveCardContent(columnContainer);
        }
    }
    #endregion
}
