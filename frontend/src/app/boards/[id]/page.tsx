"use client";

import { Navbar } from "@/components/common/navbar";
import { DialogTrigger } from "@/components/ui/dialog";
import { useBoard } from "@/features/board/hooks/useBoard";
import { IBoardResponse } from "@/features/board/IBoard";
import { useParams } from "next/navigation";
import { Fragment, useEffect, useState } from "react";
import { useColumnsWithCards } from "@/features/column/hooks/useColumnsWithCards";
import { Plus } from "lucide-react";
import { DroppableColumn } from "@/features/column/components/droppable-column";
import { SortableCard } from "@/features/card/components/sortable-card";
import { AddCardDialog } from "@/features/card/components/add-card-dialog";
import { DndContext, DragOverlay } from "@dnd-kit/core";
import { SortableContext, verticalListSortingStrategy } from "@dnd-kit/sortable";
import { TaskCard as OverlayCard } from "@/features/card/components/task-card";
import { UpdateBoardDialog } from "@/features/board/components/update-board-dialog";
import { FilterBoardDialog } from "@/features/board/components/filter-board-dialog";
import { useDragAndDropHandler } from "@/features/board/hooks/useDragAndDropHandler";


export default function Board() {
    const { id } = useParams<{ id: string }>();

    // Fetch board and columns data
    const {
        data: boardData,
        isSuccess: isBoardSuccess,
        error: fetchBoardError,
        isFetching: isFetchingBoard,
        isError: isFetchBoardError
    } = useBoard(id);

    const {
        data: columnsData,
        error: fetchColumnsError,
        isPending: isFetchingColumns,
        isError: isFetchColumnsError
    } = useColumnsWithCards(id, isBoardSuccess);


    // Local state for editing board and filtering tasks
    const [isFiltering, setIsFiltering] = useState(false);
    const [isEditingTitle, setIsEditingTitle] = useState(false);
    const [editForm, setEditForm] = useState<Pick<IBoardResponse, "title" | "description" | "color">>({
        title: "",
        description: "",
        color: "",
    });


    // Local state for drag-and-drop functionality
    const {
        activeTask,
        sensors,
        handleDragStart,
        handleDragOver,
        handleDragEnd,
        handleDragCancel,
        rectIntersection
    } = useDragAndDropHandler({ id, columnsData });


    // Set initial form values when board data is fetched
    useEffect(() => {
        const formSetter = () => {
            if (boardData) {
                setEditForm({
                    title: boardData.title,
                    description: boardData.description,
                    color: boardData.color
                });
            }
        };
        formSetter();
    }, [boardData]);


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
                filterCount={0}
                onFilterClick={() => setIsFiltering(!isFiltering)}
            />

            {/* Dialog for editing board */}
            <UpdateBoardDialog
                editTitleState={[isEditingTitle, setIsEditingTitle]}
                editFormState={[editForm, setEditForm]}
                boardData={boardData}
            />

            {/* Dialog for filtering */}
            <FilterBoardDialog filterState={[isFiltering, setIsFiltering]} />

            {/* Board Content */}
            <main className="container mx-auto px-0 py-4 sm:py-6">
                {
                    isFetchingColumns
                        ? <p>Loading board...</p>
                        : isFetchColumnsError
                            ? <p>
                                {
                                    fetchColumnsError?.message
                                }
                            </p>
                            : (
                                <Fragment>
                                    <div className="flex flex-col sm:flex-row sm:items-center sm:justify-between mb-6 space-y-4 sm:space-y-0">
                                        <div className="flex flex-wrap items-center gap-4 sm:gap-6">
                                            <div className="text-sm sm:text-md text-gray-700">
                                                <span className="font-medium">Total Tasks: </span>
                                                {
                                                    columnsData!.reduce((total, column) => total + column.cards.length, 0) ?? 0
                                                }
                                            </div>
                                        </div>

                                        {/* Add Task Dialog */}
                                        <AddCardDialog boardId={id} columnsData={columnsData}>
                                            <DialogTrigger
                                                className="flex font-medium text-sm sm:text-md items-center gap-2 px-4 py-2 bg-purple-500 text-white rounded-md hover:bg-purple-600">
                                                <Plus className="w-4 h-4" />Add Task
                                            </DialogTrigger>
                                        </AddCardDialog>
                                    </div>

                                    {/* Board Columns */}
                                    {
                                        columnsData!.length === 0
                                            ? <p className="text-gray-500">No columns found. Please add a column to get started.</p>
                                            : <DndContext
                                                sensors={sensors}
                                                onDragStart={handleDragStart}
                                                onDragEnd={handleDragEnd}
                                                onDragCancel={handleDragCancel}
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