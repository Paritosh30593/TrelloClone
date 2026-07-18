import { Button } from "@/components/ui/button";
import { Dialog, DialogContent, DialogHeader, DialogTitle } from "@/components/ui/dialog";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { Fragment, useState } from "react";
import { useCreateColumn } from "../hooks/useCreateColumn";
import { IColumnWithCardsResponse } from "../IColumn";


type CreateColumnDialogProps = {
    // Define any props needed for the dialog here
    boardId: string;
    columnsData: IColumnWithCardsResponse[];
    createColumnState: [boolean, (value: boolean) => void];
};

export const CreateColumnDialog = ({ boardId, columnsData, createColumnState }: CreateColumnDialogProps) => {
    const [isCreatingColumn, setIsCreatingColumn] = createColumnState;
    const [columnTitle, setColumnTitle] = useState<string>("");

    const {
        mutate: createColumnMutate,
        isPending: isCreatingColumnLoading,
        isError: isCreateColumnError
    } = useCreateColumn(boardId);

    const handleCreateColumn = async (e: React.SubmitEvent<HTMLFormElement>) => {
        e.preventDefault();

        if (!columnTitle?.trim()) return;

        createColumnMutate({
            title: columnTitle,
            boardId: Number(boardId),
            sortOrder: columnsData.length + 1
        });

        if (!isCreatingColumnLoading && !isCreateColumnError) {
            setIsCreatingColumn(false);
            setColumnTitle("");
        }
    };

    return (
        <Fragment>
            <Dialog open={isCreatingColumn} onOpenChange={setIsCreatingColumn}>
                <DialogContent className="w-[95vw] min-w-max mx-auto">
                    <DialogHeader>
                        <DialogTitle>Create Column</DialogTitle>
                        <hr className="my-1" />
                        <p className="text-sm sm:text-xs text-gray-500">Enter the title for the new column</p>
                    </DialogHeader>

                    <form className="space-y-4" onSubmit={handleCreateColumn}>
                        <div className="space-y-2">
                            <Label htmlFor="column-title">Column Title</Label>
                            <Input
                                id="column-title"
                                value={columnTitle}
                                onChange={(e) => setColumnTitle(e.target.value)}
                                placeholder="Enter column title"
                                required
                            />
                        </div>

                        <div className="flex justify-end space-x-2 mt-8">
                            <Button type="button" size="lg" variant="outline" onClick={() => setIsCreatingColumn(false)}>Cancel</Button>
                            <Button type="submit" size="lg" className="ui-btn-style">Create Column</Button>
                        </div>
                    </form>
                </DialogContent>
            </Dialog>
        </Fragment>
    );
};