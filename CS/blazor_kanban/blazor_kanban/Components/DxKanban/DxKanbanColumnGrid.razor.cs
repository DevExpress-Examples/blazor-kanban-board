using DevExpress.Blazor;
using Microsoft.AspNetCore.Components;
using System.Reflection;

namespace blazor_kanban.Components.DxKanban;
public partial class DxKanbanColumnGrid : ComponentBase {
    #region Parameters
    [Parameter, EditorRequired]
    public DxKanban Kanban { get; set; }

    [Parameter]
    public string? ColumnName { get; set; }
    #endregion

    #region Event Handlers
    private void ApplyCssClassesToDragAnchorsAndHints(GridCustomizeElementEventArgs e) {
        if(e.ElementType == GridElementType.RowDragAnchorCell) {
            e.CssClass = "kanban-drag-anchor";
        }
        if(e.ElementType == GridElementType.DragHint) {
            e.CssClass = "kanban-drag-hint";
        }
    }
    #endregion

    #region Utility Methods
    private IEnumerable<object> GetDataFilteredByColumnName(string? columnName) {
        if(Kanban.Data is null) {
            return Array.Empty<object>();
        }

        return Kanban.Data.OfType<object>()
            .Where(item => GetColumnNameFromDataItem(item) == columnName);
    }

    private string? GetColumnNameFromDataItem(object item) {
        ArgumentNullException.ThrowIfNullOrEmpty(Kanban.ColumnNameFieldName);
        PropertyInfo columnNameProperty = item.GetType().GetProperty(Kanban.ColumnNameFieldName)
            ?? throw new MissingMemberException($"The data item does not contain a property named '{Kanban.ColumnNameFieldName}'.");
        return columnNameProperty.GetValue(item)?.ToString();
    }
    #endregion
}
