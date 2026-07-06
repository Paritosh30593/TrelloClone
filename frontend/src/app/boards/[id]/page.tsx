"use client";

import { Navbar } from "@/components/common/navbar";
import { Button } from "@/components/ui/button";
import { Dialog, DialogContent, DialogHeader, DialogTitle } from "@/components/ui/dialog";
import { Input } from "@/components/ui/input";
import { Textarea } from "@/components/ui/textarea";
import { useBoard } from "@/hooks/useBoard";
import { useParams } from "next/navigation";
import { Fragment, useState } from "react";


export default function Board() {
    const { id } = useParams<{ id: string }>();
    const {
        fetchBoardById: { data: boardData, error: fetchBoardError, isPending: isFetchingBoard, isError: isFetchBoardError },
        fetchColumnsByBoardId: { data: columnsData, error: fetchColumnsError, isPending: isFetchingColumns, isError: isFetchColumnsError }
    } = useBoard(id);

    const [isEditingTitle, setIsEditingTitle] = useState(false);
    const [newTitle, setNewTitle] = useState("");
    const [newColor, setNewColor] = useState("");

    const handleCancelEditTitle = (e: React.MouseEvent<HTMLButtonElement>) => {
        e.preventDefault();
        setIsEditingTitle(false);
    }

    const handleSaveEditTitle = (e: React.MouseEvent<HTMLButtonElement>) => {
        e.preventDefault();
    }

    return (
        <Fragment>
            <Navbar boardTitle={boardData?.title || ""} isEditingTitle={isEditingTitle} setIsEditingTitle={setIsEditingTitle} />

            <main className="container mx-auto px-4 py-6 sm:py-8">
                <Dialog open={isEditingTitle} onOpenChange={setIsEditingTitle}>
                    <DialogContent className="w-[95vw] max-w-106.25 mx-auto">
                        <DialogHeader>
                            <DialogTitle>Edit Board</DialogTitle>
                        </DialogHeader>

                        <form className="space-y-4">
                            <div>
                                <label htmlFor="board-title" className="block text-sm font-medium text-gray-700">Board Title</label>
                                <Input
                                    type="text"
                                    id="board-title"
                                    name="board-title"
                                    placeholder="Enter board title"
                                    defaultValue={boardData?.title}
                                    className="mt-1 block w-full rounded-sm focus:border-purple-500 focus:ring-purple-500 sm:text-sm"
                                />
                            </div>
                            <div>
                                <label htmlFor="board-description" className="block text-sm font-medium text-gray-700">Board Description</label>
                                <Textarea
                                    id="board-description"
                                    name="board-description"
                                    placeholder="Enter board description"
                                    defaultValue={boardData?.description || ""}
                                    className="mt-1 block w-full rounded-sm focus:border-purple-500 focus:ring-purple-500 sm:text-sm"
                                />
                            </div>
                            <div className="flex justify-end space-x-2">
                                <Button size="lg" variant="outline" onClick={handleCancelEditTitle}>Cancel</Button>
                                <Button size="lg" type="submit" className="bg-purple-500" onClick={handleSaveEditTitle}>Save</Button>
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