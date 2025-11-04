using DevExpress.Blazor;

namespace blazor_kanban.Components.DxKanban;
public class KanbanItemsDroppedEventArgs {
    public KanbanItemsDroppedEventArgs(GridItemsDroppedEventArgs e) {
        DroppedItem = e.DroppedItems.First();
        TargetItem = e.TargetItem;
        OperationBetweenColumns = e.SourceComponent != e.Grid;
        TargetColumnName = e.Grid.KeyFieldName;
        DropPosition = e.DropPosition;
    }
    public GridItemDropPosition DropPosition { get; private set; }
    public object DroppedItem { get; private set; }
    public object TargetItem { get; private set; }
    public bool OperationBetweenColumns { get; private set; }
    public string TargetColumnName { get; private set; }
}
