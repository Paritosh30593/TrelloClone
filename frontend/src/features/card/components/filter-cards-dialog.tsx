import { Button } from "@/components/ui/button";
import { Dialog, DialogContent, DialogHeader, DialogTitle } from "@/components/ui/dialog";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { ToggleGroup, ToggleGroupItem } from "@/components/ui/toggle-group";
import { IColumnWithCardsResponse } from "@/features/column/IColumn";
import { nameof, toDateKey } from "@/lib/utils";
import { CardsFilterProps } from "@/types/CardsFilterProps";
import { PriorityOptions } from "@/types/PriorityOptions";
import { useQueryClient } from "@tanstack/react-query";
import { Fragment, useState } from "react";


type FilterCardsDialogProps = {
    boardId: string;
    filterState: [boolean, (value: boolean) => void];
    setFilterCount: (count: number) => void;
    setColumns: (columns: IColumnWithCardsResponse[]) => void;
};

const applyFilters = (columns: IColumnWithCardsResponse[], filters: CardsFilterProps): IColumnWithCardsResponse[] => {
    const filterDateKey = toDateKey(filters.dueDate);

    return columns.map(column => {
        const filteredCards = column.cards.filter(card => {
            const cardPriority = PriorityOptions[card.priority as keyof typeof PriorityOptions];
            const priorityMatch =
                filters.priority.length === 0 ||
                (cardPriority !== undefined && filters.priority.includes(cardPriority));

            const cardDateKey = toDateKey(card.dueDate);
            const dueDateMatch = filterDateKey === null || (cardDateKey !== null && cardDateKey === filterDateKey);
            const assigneeMatch = !filters.assignee || card.assignee === filters.assignee;

            return priorityMatch && assigneeMatch && dueDateMatch;
        });

        return { ...column, cards: filteredCards };
    });
}

export const FilterCardsDialog = ({ boardId, filterState, setFilterCount, setColumns }: FilterCardsDialogProps) => {
    const queryClient = useQueryClient();

    const [isFiltering, setIsFiltering] = filterState;
    const [filters, setFilters] = useState<CardsFilterProps>({
        priority: [] as PriorityOptions[],
        assignee: null as string | null,
        dueDate: null as string | null
    });
    const columnsData = queryClient.getQueryData<IColumnWithCardsResponse[]>(["columns-with-cards", boardId]);

    const handleFilterChange = (e: React.SubmitEvent<HTMLFormElement>) => {
        e.preventDefault();

        setFilterCount(
            (filters.priority?.length ?? 0) +
            (filters.assignee ? 1 : 0) +
            (filters.dueDate ? 1 : 0)
        );

        const result = applyFilters(columnsData!, filters);
        setColumns(result);
        setIsFiltering(false);
    };

    const handleClearFilters = () => {
        setFilters({
            priority: [] as PriorityOptions[],
            assignee: null as string | null,
            dueDate: null as string | null
        });

        setColumns(columnsData ?? []);
        setFilterCount(0);
        setIsFiltering(false);
    };

    return (
        <Fragment>
            <Dialog open={isFiltering} onOpenChange={setIsFiltering}>
                <DialogContent className="w-[95vw] min-w-max mx-auto">
                    <DialogHeader>
                        <DialogTitle>Filter Tasks</DialogTitle>
                        <hr className="my-1" />
                        <p className="text-sm sm:text-xs text-gray-500">Filter tasks by priority, due date, or assignee</p>
                    </DialogHeader>

                    <form className="space-y-4" onSubmit={handleFilterChange}>
                        <div className="space-y-1">
                            <Label htmlFor="priority" className="block text-sm font-medium text-gray-500">Priority</Label>
                            <ToggleGroup
                                type="multiple"
                                className="w-full gap-2"
                                aria-label="Priority filter"
                                value={filters.priority.map(String)}
                                onValueChange={(value) => setFilters({
                                    ...filters,
                                    priority: value.map(Number) as PriorityOptions[]
                                })}
                            >
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
                            <Input
                                type="text"
                                id="assignee"
                                name="assignee"
                                value={filters.assignee || ""}
                                onChange={(e) => setFilters({
                                    ...filters,
                                    assignee: e.target.value || null
                                })}
                            />
                        </div>

                        <div className="space-y-1">
                            <Label htmlFor="due-date" className="block text-sm font-medium text-gray-500">Due Date</Label>
                            <Input
                                type="date"
                                id="due-date"
                                name="due-date"
                                value={filters.dueDate || ""}
                                onChange={(e) => setFilters({
                                    ...filters,
                                    dueDate: e.target.value || null
                                })}
                            />
                        </div>

                        <div className="flex justify-between space-x-2 mt-8">
                            <Button type="button" size="lg" variant="outline" onClick={handleClearFilters}>Clear Filters</Button>
                            <Button type="submit" size="lg" className="ui-btn-style">Apply Filters</Button>
                        </div>
                    </form>
                </DialogContent>
            </Dialog>
        </Fragment>
    );
};