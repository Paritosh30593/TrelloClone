import { Button } from "@/components/ui/button";
import { Dialog, DialogContent, DialogHeader, DialogTitle } from "@/components/ui/dialog";
import { Fragment, useEffect, useState } from "react";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { IColumnWithCardsResponse } from "../IColumn";
import { useUpdateColumn } from "../hooks/useUpdateColumn";


type UpdateColumnDialogProps = {
    // Define any props needed for the dialog here
    updateColumnState: [boolean, (value: boolean) => void];
    column: IColumnWithCardsResponse;
};

export const UpdateColumnDialog = ({ updateColumnState, column }: UpdateColumnDialogProps) => {
    const [isUpdatingColumn, setIsUpdatingColumn] = updateColumnState;
    const [columnTitle, setColumnTitle] = useState<string>("");

    useEffect(() => {
        const handleColumnChange = () => {
            if (column) {
                setColumnTitle(column.title || "");
            }
        };
        handleColumnChange();
    }, [column]);

    const {
        mutate: updateColumnMutate,
        isPending: isUpdatingColumnLoading,
        isError: isUpdateColumnError
    } = useUpdateColumn(String(column.boardId));

    const handleUpdateColumn = async (e: React.SubmitEvent<HTMLFormElement>) => {
        e.preventDefault();

        if (!columnTitle?.trim()) return;

        updateColumnMutate({
            id: column.id,
            title: columnTitle,
            boardId: column.boardId,
            sortOrder: column.sortOrder
        });

        if (!isUpdatingColumnLoading && !isUpdateColumnError) {
            setIsUpdatingColumn(false);
            setColumnTitle("");
        }
    };

    return (
        <Fragment>
            <Dialog open={isUpdatingColumn} onOpenChange={setIsUpdatingColumn}>
                <DialogContent className="w-[95vw] min-w-max mx-auto">
                    <DialogHeader>
                        <DialogTitle>Update Column</DialogTitle>
                        <hr className="my-1" />
                        <p className="text-sm sm:text-xs text-gray-500">Enter the new title for the column</p>
                    </DialogHeader>

                    <form className="space-y-4" onSubmit={handleUpdateColumn}>
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
                            <Button type="button" size="lg" variant="outline" onClick={() => setIsUpdatingColumn(false)}>Cancel</Button>
                            <Button type="submit" size="lg" className="ui-btn-style">Update Column</Button>
                        </div>
                    </form>
                </DialogContent>
            </Dialog>
        </Fragment>
    );
};