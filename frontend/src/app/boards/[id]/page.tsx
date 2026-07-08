"use client";

import { Navbar } from "@/components/common/navbar";
import { Button } from "@/components/ui/button";
import { Dialog, DialogContent, DialogHeader, DialogTitle } from "@/components/ui/dialog";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { Textarea } from "@/components/ui/textarea";
import { useBoard } from "@/hooks/board/useBoard";
import { useUpdateBoard } from "@/hooks/board/useUpdateBoard";
import { BOARD_COLORS } from "@/lib/constants";
import { IBoardResponse } from "@/types/IBoard";
import { useParams } from "next/navigation";
import { Fragment, useEffect, useState } from "react";


export default function Board() {
    const { id } = useParams<{ id: string }>();

    const { data: boardData, error: fetchBoardError, isPending: isFetchingBoard, isError: isFetchBoardError } = useBoard(id);
    //const { data: columnsData, error: fetchColumnsError, isPending: isFetchingColumns, isError: isFetchColumnsError } = useColumns(id);
    const { mutate: updateBoardMutate, isPending: isUpdatingBoard, isError: isUpdateBoardError } = useUpdateBoard();

    const [isEditingTitle, setIsEditingTitle] = useState(false);
    const [editForm, setEditForm] = useState<Pick<IBoardResponse, "title" | "description" | "color">>({
        title: "",
        description: "",
        color: "",
    });

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
            setIsEditingTitle(!isEditingTitle);
        }
    }

    return (
        <Fragment>
            <Navbar boardTitle={editForm.title} isEditingTitle={isEditingTitle} setIsEditingTitle={setIsEditingTitle} />

            <main className="container mx-auto px-4 py-6 sm:py-8">

                {/* Dialog for editing board */}
                <Dialog
                    open={isEditingTitle}
                    onOpenChange={setIsEditingTitle}
                >
                    <DialogContent className="w-[95vw] min-w-max mx-auto">
                        <DialogHeader>
                            <DialogTitle>Edit Board</DialogTitle>
                        </DialogHeader>

                        <form className="space-y-4" onSubmit={handleUpdateBoard}>
                            <div className="space-y-2">
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
                            <div className="space-y-2">
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
                            <div className="flex justify-end space-x-2">
                                <Button type="button" size="lg" variant="outline" onClick={() => setIsEditingTitle(!isEditingTitle)}>Cancel</Button>
                                <Button type="submit" size="lg" className="bg-purple-500">Save Changes</Button>
                            </div>
                        </form>
                    </DialogContent>
                </Dialog>

                {
                    isFetchingBoard
                        ? <p>Loading board...</p>
                        : isFetchBoardError
                            ? <p>Error loading board: {fetchBoardError?.message}</p>
                            : (
                                <p></p>
                            )
                }
            </main>
        </Fragment>
    );
};