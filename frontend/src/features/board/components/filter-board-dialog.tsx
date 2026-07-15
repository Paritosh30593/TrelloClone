import { Button } from "@/components/ui/button";
import { Dialog, DialogContent, DialogHeader, DialogTitle } from "@/components/ui/dialog";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { ToggleGroup, ToggleGroupItem } from "@/components/ui/toggle-group";
import { nameof } from "@/lib/utils";
import { PriorityOptions } from "@/types/PriorityOptions";
import { Fragment, useState } from "react";


type FilterBoardDialogProps = {
    filterState: [boolean, (value: boolean) => void];
};

export const FilterBoardDialog = ({ filterState }: FilterBoardDialogProps) => {
    const [isFiltering, setIsFiltering] = filterState;
    const [filterPriority, setFilterPriority] = useState<PriorityOptions>(PriorityOptions.All);

    return (
        <Fragment>
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
        </Fragment>
    );
};