import { Button } from "@/components/ui/button";
import { Dialog, DialogContent, DialogHeader, DialogTitle } from "@/components/ui/dialog";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select";
import { Textarea } from "@/components/ui/textarea";
import { nameof } from "@/lib/utils";
import { PriorityOptions } from "@/types/PriorityOptions";
import { Fragment } from "react";
import { useCreateCard } from "../hooks/useCreateCard";
import { IColumnWithCardsResponse } from "@/features/column/IColumn";


type AddCardDialogProps = {
    boardId: string,
    columnsData: IColumnWithCardsResponse[] | undefined;
    children: React.ReactNode;
};

export const AddCardDialog = ({ boardId, columnsData, children }: AddCardDialogProps) => {
    // Mutations for updating/creating board, columns and card
    const { mutate: createCardMutate, isPending: isCreatingCard, isError: isCreateCardError } = useCreateCard(boardId);

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

    return (
        <Fragment>
            {/* Add Task Dialog */}
            <Dialog>
                {children}

                <DialogContent className="w-[95vw] min-w-max mx-auto">
                    <DialogHeader>
                        <DialogTitle>New Task</DialogTitle>
                        <hr className="my-1" />
                        <p className="text-sm sm:text-xs text-gray-500">Add a new task to your board</p>
                    </DialogHeader>

                    <form className="space-y-4" onSubmit={handleCreateTask}>
                        <div className="space-y-1">
                            <Label htmlFor="title" className="block text-sm font-medium text-gray-500">Title</Label>
                            <Input type="text" id="title" name="title" />
                        </div>

                        <div className="space-y-1">
                            <Label htmlFor="description" className="block text-sm font-medium text-gray-500">Description</Label>
                            <Textarea id="description" name="description" rows={2} />
                        </div>

                        <div className="space-y-1">
                            <Label htmlFor="assignee" className="block text-sm font-medium text-gray-500">Assignee</Label>
                            <Input type="text" id="assignee" name="assignee" />
                        </div>

                        <div className="grid grid-cols-1 sm:grid-cols-2 gap-4">
                            <div className="space-y-1">
                                <Label htmlFor="priority" className="block text-sm font-medium text-gray-500">Priority</Label>
                                <Select name="priority" defaultValue={String(PriorityOptions.Medium)}>
                                    <SelectTrigger className="w-full">
                                        <SelectValue placeholder="Select priority" />
                                    </SelectTrigger>
                                    <SelectContent className="w-full">
                                        <SelectItem
                                            key={PriorityOptions.Low}
                                            value={String(PriorityOptions.Low)}
                                        >
                                            {nameof<typeof PriorityOptions>("Low")}
                                        </SelectItem>
                                        <SelectItem
                                            key={PriorityOptions.Medium}
                                            value={String(PriorityOptions.Medium)}
                                        >
                                            {nameof<typeof PriorityOptions>("Medium")}
                                        </SelectItem>
                                        <SelectItem
                                            key={PriorityOptions.High}
                                            value={String(PriorityOptions.High)}
                                        >
                                            {nameof<typeof PriorityOptions>("High")}
                                        </SelectItem>
                                    </SelectContent>
                                </Select>
                            </div>

                            <div className="space-y-1">
                                <Label htmlFor="dueDate" className="block text-sm font-medium text-gray-500">Due Date</Label>
                                <Input type="date" id="dueDate" name="dueDate" />
                            </div>
                        </div>
                        <div className="flex justify-end space-x-2 mt-8">
                            <Button type="submit" size="lg" className="ui-btn-style">Create Task</Button>
                        </div>
                    </form>
                </DialogContent>
            </Dialog>
        </Fragment>
    );
};