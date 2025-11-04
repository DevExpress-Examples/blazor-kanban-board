using Microsoft.AspNetCore.Components;

namespace blazor_kanban.Components.DxKanban;
public partial class DxKanbanColumn : ComponentBase {
    #region Parameters
    [Parameter]
    public string? ColumnName { get; set; }

    [CascadingParameter]
    private DxKanban? Kanban { get; set; }
    #endregion

    #region Lifecycle Methods
    protected override void OnInitialized() {
        if(Kanban != null) {
            Kanban.AddColumn(this);
        }
    }
    #endregion
}
