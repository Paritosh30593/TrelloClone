import { useUpdateCards } from "@/features/card/hooks/useUpdateCards";
import { ICardResponse, ICardUpdateRequest } from "@/features/card/ICard";
import { IColumnWithCardsResponse } from "@/features/column/IColumn";
import { PriorityOptions } from "@/types/PriorityOptions";
import { DragEndEvent, DragOverEvent, DragStartEvent, PointerSensor, rectIntersection, useSensor, useSensors } from "@dnd-kit/core";
import { useQueryClient } from "@tanstack/react-query";
import { useRef, useState } from "react";


type UseDragAndDropHandlerProps = {
    id: string;
    columnsData: IColumnWithCardsResponse[] | undefined;
};

export const useDragAndDropHandler = ({ id, columnsData }: UseDragAndDropHandlerProps) => {
    const queryClient = useQueryClient();
    const columnsQueryKey = ['columns-with-cards', id];

    const { mutateAsync: updateCardsMutateAsync } = useUpdateCards(id);
    const [activeTask, setActiveTask] = useState<ICardResponse | null>(null);
    const dragStartSnapshotRef = useRef<Map<number, { columnId: number; sortOrder: number }>>(new Map());

    const sensors = useSensors(useSensor(PointerSensor, {
        activationConstraint: {
            distance: 8
        }
    }));

    // Handle drag start event to set the active task being dragged
    const handleDragStart = (event: DragStartEvent) => {
        const taskId = event.active.id as number;
        const cols = columnsData ?? [];
        const task = cols
            .flatMap(column => column.cards)
            .find(card => card.id === taskId);

        // Keep a stable snapshot from drag start to detect exactly what changed on drop.
        dragStartSnapshotRef.current = new Map(
            cols.flatMap(column =>
                column.cards.map(card => [card.id, { columnId: card.columnId, sortOrder: card.sortOrder }] as const)
            )
        );

        if (task) setActiveTask(task);
    };

    // Handle drag over event to manage the position of the dragged task
    const handleDragOver = (event: DragOverEvent) => {
        const { active, over } = event;

        // If there's no task being dragged over, exit early
        if (!over) return;

        // Get the ID of the task being dragged
        const activeTaskId = active.id as number;

        // Get the ID of the task being dragged over
        const overTaskId = over.id as number;

        if (activeTaskId === -1 || overTaskId === -1) return;

        queryClient.setQueryData<IColumnWithCardsResponse[]>(columnsQueryKey, (prevCols = []) => {
            const newCols = [...prevCols];

            const sourceColumnIndex = newCols.findIndex(column => column.cards.some(card => card.id === activeTaskId));
            const targetColumnIndex = newCols.findIndex(column => column.cards.length > 0
                ? column.cards.some(card => card.id === overTaskId)
                : column.id === overTaskId // Handle case where the column is empty and the task is being dragged over the column itself
            );

            if (sourceColumnIndex === -1 || targetColumnIndex === -1) return newCols;

            const sourceColumn = newCols[sourceColumnIndex];
            const targetColumn = newCols[targetColumnIndex];

            const sourceCardIndex = sourceColumn.cards.findIndex(card => card.id === activeTaskId);
            const targetCardIndex = targetColumn.cards.findIndex(card => card.id === overTaskId);

            if (sourceCardIndex === -1) return newCols;

            if (sourceColumnIndex === targetColumnIndex) {
                if (sourceCardIndex === targetCardIndex) return newCols;

                const updatedCards = [...sourceColumn.cards];
                const tempSortOrder = updatedCards[sourceCardIndex].sortOrder;
                updatedCards[sourceCardIndex].sortOrder = updatedCards[targetCardIndex].sortOrder;
                updatedCards[targetCardIndex].sortOrder = tempSortOrder;

                newCols[sourceColumnIndex] = {
                    ...sourceColumn,
                    cards: updatedCards.sort((a, b) => a.sortOrder - b.sortOrder),
                };
                return newCols;
            }
            else {
                sourceColumn.cards.forEach((card, index) => {
                    card.sortOrder = index > sourceCardIndex
                        ? card.sortOrder - 1
                        : card.sortOrder;
                });
                const movedCard = sourceColumn.cards.splice(sourceCardIndex, 1)[0];
                movedCard.columnId = targetColumn.id;

                if (targetCardIndex === -1) {
                    movedCard.sortOrder = targetColumn.cards.length + 1;
                    targetColumn.cards.push(movedCard);
                }
                else {
                    movedCard.sortOrder = targetColumn.cards[targetCardIndex].sortOrder;
                    targetColumn.cards.forEach((card, index) => {
                        card.sortOrder = index >= targetCardIndex
                            ? card.sortOrder + 1
                            : card.sortOrder;
                    });
                    targetColumn.cards.splice(targetCardIndex, 0, movedCard);
                }

                newCols[sourceColumnIndex] = {
                    ...sourceColumn,
                    cards: sourceColumn.cards,
                };
                newCols[targetColumnIndex] = {
                    ...targetColumn,
                    cards: targetColumn.cards,
                };

                return newCols;
            }
        });
    };

    // Handle drag end event to finalize the position of the dragged task
    const handleDragEnd = async (event: DragEndEvent) => {
        const { over } = event;

        setActiveTask(null);

        // If there's no task being dragged over, exit early
        if (!over) return;

        const finalCols = queryClient.getQueryData<IColumnWithCardsResponse[]>(columnsQueryKey) ?? [];
        const changedCards = finalCols
            .flatMap(column => column.cards)
            .filter(card => {
                const initial = dragStartSnapshotRef.current.get(card.id);
                return initial && (initial.columnId !== card.columnId || initial.sortOrder !== card.sortOrder);
            });

        if (changedCards.length === 0) return;

        const groupedCards = changedCards.reduce<Record<number, ICardUpdateRequest[]>>((acc, card) => {
            if (!acc[card.columnId]) {
                // Initialize an array for the column if it doesn't exist
                acc[card.columnId] = [];
            }

            // Create a new object for each card to avoid mutating the original data
            acc[card.columnId].push({
                id: card.id,
                columnId: card.columnId,
                title: card.title,
                description: card.description,
                assignee: card.assignee,
                dueDate: card.dueDate,
                priority: PriorityOptions[card.priority as keyof typeof PriorityOptions],
                sortOrder: card.sortOrder,
            });

            return acc;
        }, {});

        Object
            .values(groupedCards)
            .map((columnCards) => updateCardsMutateAsync(columnCards));
    };

    const handleDragCancel = () => {
        setActiveTask(null);
    };

    return {
        activeTask,
        sensors,
        handleDragStart,
        handleDragOver,
        handleDragEnd,
        handleDragCancel,
        rectIntersection
    }
}


