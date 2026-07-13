"use client";

import { Navbar } from "@/components/common/navbar";
import { Button } from "@/components/ui/button";
import { Dialog, DialogContent, DialogHeader, DialogTitle, DialogTrigger } from "@/components/ui/dialog";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { ToggleGroup, ToggleGroupItem } from "@/components/ui/toggle-group";
import { Textarea } from "@/components/ui/textarea";
import { useBoard } from "@/features/board/hooks/useBoard";
import { useUpdateBoard } from "@/features/board/hooks/useUpdateBoard";
import { BOARD_COLORS } from "@/lib/constants";
import { IBoardResponse } from "@/features/board/IBoard";
import { PriorityOptions } from "@/types/PriorityOptions";
import { useParams } from "next/navigation";
import { Fragment, useEffect, useState } from "react";
import { nameof } from "@/lib/utils";
import { useColumnsWithCards } from "@/features/column/hooks/useColumnsWithCards";
import { Plus } from "lucide-react";
import { DroppableColumn } from "@/features/column/components/droppable-column";
import { useCreateCard } from "@/features/card/hooks/useCreateCard";
import { useUpdateCard } from "@/features/card/hooks/useUpdateCard";
import { SortableCard } from "@/features/card/components/sortable-card";
import { AddCardDialog } from "@/features/card/components/add-card-dialog";
import { DndContext, DragEndEvent, DragOverEvent, DragOverlay, DragStartEvent, PointerSensor, rectIntersection, useSensor, useSensors } from "@dnd-kit/core";
import { SortableContext, verticalListSortingStrategy } from "@dnd-kit/sortable";
import { ICardResponse } from "@/features/card/ICard";
import { OverlayCard } from "@/features/card/components/overlay-card";
import { IColumnWithCardsResponse } from "@/features/column/IColumn";
import { useQueryClient } from "@tanstack/react-query";


export default function Board() {
    const { id } = useParams<{ id: string }>();
    const queryClient = useQueryClient();
    const columnsQueryKey = ["columns-with-cards", id] as const;

    // Fetch board and columns data
    const { data: boardData, isSuccess: isBoardSuccess, error: fetchBoardError, isFetching: isFetchingBoard, isError: isFetchBoardError } = useBoard(id);
    const { data: columnsData, error: fetchColumnsError, isPending: isFetchingColumns, isError: isFetchColumnsError } = useColumnsWithCards(id, isBoardSuccess);

    // Mutations for updating board and creating card
    const { mutate: updateBoardMutate, isPending: isUpdatingBoard, isError: isUpdateBoardError } = useUpdateBoard();
    const { mutate: createCardMutate, isPending: isCreatingCard, isError: isCreateCardError } = useCreateCard(id);
    const { mutate: updateCardMutate } = useUpdateCard();

    // Local state for editing board and filtering tasks
    const [isEditingTitle, setIsEditingTitle] = useState(false);
    const [isFiltering, setIsFiltering] = useState(false);
    const [filterPriority, setFilterPriority] = useState<PriorityOptions>(PriorityOptions.All);
    const [editForm, setEditForm] = useState<Pick<IBoardResponse, "title" | "description" | "color">>({
        title: "",
        description: "",
        color: "",
    });

    // Local state for drag-and-drop functionality
    const [activeTask, setActiveTask] = useState<ICardResponse | null>(null);

    const sensors = useSensors(useSensor(PointerSensor, {
        activationConstraint: {
            distance: 8
        }
    }));

    // Set initial form values when board data is fetched
    useEffect(() => {
        const formSetter = () => {
            if (boardData) {
                setEditForm({
                    title: boardData.title,
                    description: boardData.description,
                    color: boardData.color,
                });
            }
        };
        formSetter();
    }, [boardData]);

    // Handle form submissions for updating board and creating tasks
    const handleUpdateBoard = async (e: React.SubmitEvent<HTMLFormElement>) => {
        e.preventDefault();

        if (!editForm.title || !boardData) return;

        updateBoardMutate({
            id: boardData.id,
            userId: boardData.userId,
            title: editForm.title,
            description: editForm.description,
            color: editForm.color,
        });

        if (!isUpdatingBoard && !isUpdateBoardError) {
            setIsEditingTitle(false);
        }
    }

    // Handle form submission for creating a new task
    const handleCreateTask = async (e: React.SubmitEvent<HTMLFormElement>) => {
        e.preventDefault();

        const formData = new FormData(e.currentTarget);
        const targetColumn = columnsData?.[0];

        if (!targetColumn || !formData.get("title")) return;

        createCardMutate({
            columnId: targetColumn.id,
            title: formData.get("title") as string,
            description: formData.get("description") as string || null,
            assignee: formData.get("assignee") as string || null,
            dueDate: formData.get("dueDate") as string || null,
            priority: (Number(formData.get("priority")) as PriorityOptions) || undefined,
            sortOrder: targetColumn.cards.length + 1 || 0, // New task will be added at the end
        });

        if (!isCreatingCard && !isCreateCardError) {
            const trigger = document.querySelector('[data-state="open"]') as HTMLElement | null;
            if (trigger) trigger.click();
        }
    }

    // Handle drag start event to set the active task being dragged
    const handleDragStart = (event: DragStartEvent) => {
        const taskId = event.active.id as number;
        const task = (columnsData ?? [])
            .flatMap(column => column.cards)
            .find(card => card.id === taskId);

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

        if ((activeTaskId === -1 || overTaskId === -1) || activeTaskId === overTaskId) return;

        queryClient.setQueryData<IColumnWithCardsResponse[]>(columnsQueryKey, (prevCols = []) => {
            const newCols = [...prevCols];
            console.log("Initial Columns before Drag Over:", newCols);

            const sourceColumnIndex = newCols.findIndex(column => column.cards.some(card => card.id === activeTaskId));
            const targetColumnIndex = newCols.findIndex(column => column.cards.some(card => card.id === overTaskId));

            if (sourceColumnIndex === -1 || targetColumnIndex === -1) return newCols;

            const sourceColumn = newCols[sourceColumnIndex];
            const targetColumn = newCols[targetColumnIndex];

            const sourceCardIndex = sourceColumn.cards.findIndex(card => card.id === activeTaskId);
            const targetCardIndex = targetColumn.cards.findIndex(card => card.id === overTaskId);

            if (sourceCardIndex === -1 && targetCardIndex === -1) return newCols;
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
                console.log("Updated Columns after Drag Over:", newCols);
                return newCols;
            }
            else {
                const movedCard = sourceColumn.cards.splice(sourceCardIndex, 1)[0];
                movedCard.sortOrder = targetColumn.cards[targetCardIndex].sortOrder;
                targetColumn.cards.forEach((card, index) => {
                    if (index >= targetCardIndex) {
                        card.sortOrder += 1;
                    }
                });
                targetColumn.cards.splice(targetCardIndex, 0, movedCard);

                newCols[sourceColumnIndex] = {
                    ...sourceColumn,
                    cards: sourceColumn.cards,
                };
                newCols[targetColumnIndex] = {
                    ...targetColumn,
                    cards: targetColumn.cards,
                };

                console.log("Updated Columns after Drag Over:", newCols);
                return newCols;
            }
        });
    };

    // Handle drag end event to finalize the position of the dragged task
    const handleDragEnd = (event: DragEndEvent) => {
        const { active, over } = event;

        // If there's no task being dragged over, exit early
        if (!over) return;

        // Get the ID of the task being dragged
        const activeTaskId = active.id as number;

        // Get the ID of the task being dragged over
        const overTaskId = over.id as number;


    };

    // Render loading and error states
    if (isFetchingBoard && !boardData) {
        return (
            <div className="flex items-center justify-center h-screen">
                <p>Loading board...</p>
            </div>
        );
    }

    // Render error state if board fetch fails
    if (isFetchBoardError) {
        return (
            <div className="flex items-center justify-center h-screen">
                <p>{fetchBoardError?.message}</p>
            </div>
        );
    }

    return (
        <Fragment>
            <Navbar
                boardTitle={editForm.title}
                setIsEditingTitle={setIsEditingTitle}
                filterCount={2}
                onFilterClick={() => setIsFiltering(!isFiltering)}
            />

            {/* Dialog for editing board */}
            <Dialog open={isEditingTitle} onOpenChange={setIsEditingTitle}>
                <DialogContent className="w-[95vw] min-w-max mx-auto">
                    <DialogHeader>
                        <DialogTitle>Edit Board</DialogTitle>
                        <hr className="my-1" />
                        <p className="text-sm sm:text-xs text-gray-500">Update the details of your board below</p>
                    </DialogHeader>

                    <form className="space-y-4" onSubmit={handleUpdateBoard}>
                        <div className="space-y-1">
                            <Label htmlFor="board-title" className="block text-sm font-medium text-gray-500">Board Title</Label>
                            <Input
                                id="board-title"
                                name="board-title"
                                placeholder="Enter board title"
                                value={editForm.title}
                                onChange={(e) => setEditForm(prev => ({ ...prev, title: e.target.value }))}
                                className="mt-1 block w-full rounded-sm focus:border-purple-500 focus:ring-purple-500 sm:text-sm"
                            />
                        </div>
                        <div className="space-y-1">
                            <Label htmlFor="board-description" className="block text-sm font-medium text-gray-500">Board Description</Label>
                            <Textarea
                                id="board-description"
                                name="board-description"
                                placeholder="Enter board description"
                                value={editForm.description ?? ""}
                                onChange={(e) => setEditForm(prev => ({ ...prev, description: e.target.value }))}
                                className="mt-1 block w-full rounded-sm focus:border-purple-500 focus:ring-purple-500 sm:text-sm"
                            />
                        </div>
                        <div className="space-y-2">
                            <Label htmlFor="board-color" className="block text-sm font-medium text-gray-500">Board Color</Label>
                            <div className="grid grid-cols-6 sm:grid-cols-9 gap-2 justify-items-center">
                                {
                                    BOARD_COLORS.map((color) => (
                                        <button
                                            type="button"
                                            key={color.code}
                                            onClick={() => setEditForm(prev => ({ ...prev, color: color.code }))}
                                            className={`w-8 h-8 rounded-full ${color.code} 
                                                    ${color.code === editForm.color
                                                    ? `ring-2 ring-offset-2 ${color.name}`
                                                    : ""
                                                }`
                                            }
                                        />
                                    ))
                                }
                            </div>
                        </div>
                        <div className="flex justify-end space-x-2 mt-8">
                            <Button type="button" size="lg" variant="outline" onClick={() => setIsEditingTitle(false)}>Cancel</Button>
                            <Button type="submit" size="lg" className="bg-purple-500">Save Changes</Button>
                        </div>
                    </form>
                </DialogContent>
            </Dialog>

            {/* Dialog for filtering */}
            <Dialog open={isFiltering} onOpenChange={setIsFiltering}>
                <DialogContent className="w-[95vw] min-w-max mx-auto">
                    <DialogHeader>
                        <DialogTitle>Filter Tasks</DialogTitle>
                        <hr className="my-1" />
                        <p className="text-sm sm:text-xs text-gray-500">Filter tasks by priority, due date, or assignee</p>
                    </DialogHeader>
                    <form className="space-y-4" onSubmit={(e) => { e.preventDefault(); setIsFiltering(false); }}>
                        <div className="space-y-1">
                            <Label htmlFor="priority" className="block text-sm font-medium text-gray-500">Priority</Label>
                            <ToggleGroup
                                type="single"
                                className="w-full gap-2"
                                aria-label="Priority filter"
                                value={String(filterPriority)}
                                onValueChange={(value) => setFilterPriority(Number(value))}
                            >
                                <ToggleGroupItem
                                    key={PriorityOptions.All}
                                    className="text-sm sm:text-md rounded-md"
                                    value={String(PriorityOptions.All)}
                                    aria-label="All priorities">{nameof<typeof PriorityOptions>("All")}</ToggleGroupItem>
                                <ToggleGroupItem
                                    key={PriorityOptions.Low}
                                    className="text-sm sm:text-md rounded-md"
                                    value={String(PriorityOptions.Low)}
                                    aria-label="Low priority">{nameof<typeof PriorityOptions>("Low")}</ToggleGroupItem>
                                <ToggleGroupItem
                                    key={PriorityOptions.Medium}
                                    className="text-sm sm:text-md rounded-md"
                                    value={String(PriorityOptions.Medium)}
                                    aria-label="Medium priority">{nameof<typeof PriorityOptions>("Medium")}</ToggleGroupItem>
                                <ToggleGroupItem
                                    key={PriorityOptions.High}
                                    className="text-sm sm:text-md rounded-md"
                                    value={String(PriorityOptions.High)}
                                    aria-label="High priority">{nameof<typeof PriorityOptions>("High")}</ToggleGroupItem>
                            </ToggleGroup>
                        </div>

                        <div className="space-y-1">
                            <Label htmlFor="assignee" className="block text-sm font-medium text-gray-500">Assignee</Label>
                            <Input type="text" id="assignee" name="assignee" />
                        </div>

                        <div className="space-y-1">
                            <Label htmlFor="due-date" className="block text-sm font-medium text-gray-500">Due Date</Label>
                            <Input type="date" id="due-date" name="due-date" />
                        </div>

                        <div className="flex justify-between space-x-2 mt-8">
                            <Button type="button" size="lg" variant="outline">Clear Filters</Button>
                            <Button type="submit" size="lg" className="bg-purple-500">Apply Filters</Button>
                        </div>
                    </form>
                </DialogContent>
            </Dialog>

            {/* Board Content */}
            <main className="container mx-auto px-2 sm:px-4 py-4 sm:py-6">
                {isFetchingColumns
                    ? <p>Loading board...</p>
                    : isFetchColumnsError
                        ? <p>{fetchColumnsError?.message}</p>
                        : (
                            <Fragment>
                                <div className="flex flex-col sm:flex-row sm:items-center sm:justify-between mb-6 space-y-4 sm:space-y-0">
                                    <div className="flex flex-wrap items-center gap-4 sm:gap-6">
                                        <div className="text-sm sm:text-md text-gray-700">
                                            <span className="font-medium">Total Tasks: </span>
                                            {columnsData!.reduce((total, column) => total + column.cards.length, 0) ?? 0}
                                        </div>
                                    </div>

                                    {/* Add Task Dialog */}
                                    <AddCardDialog handleCreateTask={handleCreateTask}>
                                        <DialogTrigger
                                            className="flex font-medium text-sm sm:text-md items-center gap-2 px-4 py-2 bg-purple-500 text-white rounded-md hover:bg-purple-600">
                                            <Plus className="w-4 h-4" />Add Task
                                        </DialogTrigger>
                                    </AddCardDialog>
                                </div>

                                {/* Board Columns */}
                                {columnsData!.length === 0
                                    ? <p className="text-gray-500">No columns found. Please add a column to get started.</p>
                                    : <DndContext
                                        sensors={sensors}
                                        onDragStart={handleDragStart}
                                        onDragEnd={handleDragEnd}
                                        onDragOver={handleDragOver}
                                        collisionDetection={rectIntersection}
                                    >
                                        <div className="flex flex-col lg:flex-row lg:space-x-6 lg:pb-6 lg:px-2 lg:-mx-2 lg:[&::-webkit-scrollbar]:h-2 lg:[&::-webkit-scrollbar-thumb]:bg-gray-300 lg:[&::-webkit-scrollbar-thumb]:rounded-full lg:[&::-webkit-scrollbar-track]:bg-gray-100 space-y-4 lg:space-y-0 lg:overflow-x-scroll">
                                            {
                                                columnsData!.map((column, key) =>
                                                    <DroppableColumn
                                                        key={key}
                                                        column={column}
                                                        color={boardData!.color || "bg-gray-700"}
                                                        onCreateTask={handleCreateTask}
                                                        onEditColumn={() => { }}
                                                    >
                                                        <SortableContext
                                                            items={column.cards.map(card => card.id)}
                                                            strategy={verticalListSortingStrategy}
                                                        >
                                                            <div className="space-y-3">
                                                                {
                                                                    column.cards.map((card, cardKey) => (
                                                                        <SortableCard key={cardKey} card={card} />
                                                                    ))
                                                                }
                                                            </div>
                                                        </SortableContext>

                                                    </DroppableColumn>
                                                )
                                            }

                                            <DragOverlay>
                                                {
                                                    activeTask && <OverlayCard card={activeTask} />
                                                }
                                            </DragOverlay>
                                        </div>
                                    </DndContext>
                                }
                            </Fragment>
                        )
                }
            </main>
        </Fragment>
    );
};