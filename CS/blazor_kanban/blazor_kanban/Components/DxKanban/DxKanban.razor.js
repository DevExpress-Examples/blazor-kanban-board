export function moveGridDataCellContentToAnchors() {
    for (var columnContainer of document.getElementsByClassName("kanban-column-grid")) {
        var dragAnchors = columnContainer.getElementsByClassName("kanban-drag-anchor");
        for (var dragAnchor of dragAnchors) {
            var gridContentCell = dragAnchor.nextElementSibling;
            dragAnchor.innerHTML = gridContentCell.innerHTML;
        }
    }
}