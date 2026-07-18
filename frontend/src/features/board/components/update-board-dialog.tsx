import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { Textarea } from "@/components/ui/textarea";
import { BOARD_COLORS } from "@/lib/constants";
import { Dispatch, Fragment, SetStateAction } from "react";
import { useUpdateBoard } from "../hooks/useUpdateBoard";
import { IBoardResponse } from "../IBoard";
import { Dialog, DialogContent, DialogHeader, DialogTitle } from "@/components/ui/dialog";


type BoardEditForm = Pick<IBoardResponse, "title" | "description" | "color">;

type UpdateBoardDialogProps = {
    editTitleState: [boolean, (value: boolean) => void];
    editFormState: [BoardEditForm, Dispatch<SetStateAction<BoardEditForm>>];
    boardData: IBoardResponse | undefined;
};

export const UpdateBoardDialog = ({ editTitleState, editFormState, boardData }: UpdateBoardDialogProps) => {
    const {
        mutate: updateBoardMutate,
        isPending: isUpdatingBoard,
        isError: isUpdateBoardError
    } = useUpdateBoard();

    const [isEditingTitle, setIsEditingTitle] = editTitleState;
    const [editForm, setEditForm] = editFormState;

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

        if (!isUpdatingBoard && !isUpdateBoardError) setIsEditingTitle(false);
    }

    return (
        <Fragment>
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
                                className="mt-1 block w-full rounded-sm focus:border-purple-500 focus:ring-purple-500 sm:text-sm"
                                onChange={(e) => setEditForm(prev => ({
                                    ...prev,
                                    title: e.target.value
                                }))}
                            />
                        </div>
                        <div className="space-y-1">
                            <Label htmlFor="board-description" className="block text-sm font-medium text-gray-500">Board Description</Label>
                            <Textarea
                                id="board-description"
                                name="board-description"
                                placeholder="Enter board description"
                                value={editForm.description ?? ""}
                                className="mt-1 block w-full rounded-sm focus:border-purple-500 focus:ring-purple-500 sm:text-sm"
                                onChange={(e) => setEditForm(prev => ({
                                    ...prev,
                                    description: e.target.value
                                }))}
                            />
                        </div>
                        <div className="space-y-2">
                            <Label htmlFor="board-color" className="block text-sm font-medium text-gray-500">Board Color</Label>
                            <div className="grid grid-cols-6 sm:grid-cols-9 gap-2 justify-items-center">
                                {
                                    BOARD_COLORS.map((color) => (
                                        <button
                                            type="button"
                                            key={color.bgcode}
                                            onClick={() => setEditForm(prev => ({ ...prev, color: color.bgcode }))}
                                            className={`w-8 h-8 rounded-full ${color.bgcode} 
                                                ${color.bgcode === editForm.color
                                                    ? `ring-2 ring-offset-2 ${color.rgcode}`
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
                            <Button type="submit" size="lg" className="ui-btn-style">Save Changes</Button>
                        </div>
                    </form>
                </DialogContent>
            </Dialog>
        </Fragment>
    );
};