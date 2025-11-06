using DevExpress.Blazor;
using DevExpress.ClipboardSource.SpreadsheetML;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Reflection;

namespace blazor_kanban.Components.DxKanban;
public partial class DxKanbanColumn : ComponentBase {
    #region Parameters
    [CascadingParameter]
    private DxKanban? Kanban { get; set; }

    [Parameter]
    public string? ColumnName { get; set; }
    #endregion

    #region Event Handlers
    protected override bool ShouldRender() => false; //t1135370
    #endregion
}
