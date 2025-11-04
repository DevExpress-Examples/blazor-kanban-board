export function MoveCardContent(columnContainer) {
    var dragAnchors = columnContainer.getElementsByClassName("kanban-drag-anchor");
    for (var dragAnchor of dragAnchors) {
        var gridContentCell = dragAnchor.nextElementSibling;
        dragAnchor.innerHTML = gridContentCell.innerHTML;
        dragAnchor.firstChild.style.pointerEvents = "none";
    }
}