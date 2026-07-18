import { Dialog, DialogContent, DialogHeader, DialogTitle } from "@/components/ui/dialog";
import { Label } from "@/components/ui/label";
import { Fragment, useState } from "react";
import { BoardsFilterProps } from "@/types/BoardsFilterProps";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import { useQueryClient } from "@tanstack/react-query";
import { IBoardResponse } from "../IBoard";
import { toDateKey } from "@/lib/utils";


type FilterBoardsDialogProps = {
    userId: string;
    filterState: [boolean, (value: boolean) => void];
    setBoards: (value: IBoardResponse[]) => void;
    setFilterCount: (count: number) => void;
};

const applyBoardFilters = (boards: IBoardResponse[], filters: BoardsFilterProps): IBoardResponse[] => {
    const filterCreateStartDate = filters.createDateRange.startDate ? toDateKey(filters.createDateRange.startDate) : null;
    const filterCreateEndDate = filters.createDateRange.endDate ? toDateKey(filters.createDateRange.endDate) : null;

    const filterUpdateStartDate = filters.updateDateRange.startDate ? toDateKey(filters.updateDateRange.startDate) : null;
    const filterUpdateEndDate = filters.updateDateRange.endDate ? toDateKey(filters.updateDateRange.endDate) : null;

    return boards.filter(board => {
        const searchMatch = !filters.search
            || board.title
                .toLowerCase()
                .includes(filters.search.toLowerCase())
            || (board.description != null && board.description
                .toLowerCase()
                .includes(filters.search.toLowerCase()));

        const createDateKey = toDateKey(board.createdAt);
        const startDateMatch = !filterCreateStartDate || (createDateKey != null && createDateKey >= filterCreateStartDate);
        const endDateMatch = !filterCreateEndDate || (createDateKey != null && createDateKey <= filterCreateEndDate);

        const updateDateKey = toDateKey(board.updatedAt);
        const updateStartDateMatch = !filterUpdateStartDate || (updateDateKey != null && updateDateKey >= filterUpdateStartDate);
        const updateEndDateMatch = !filterUpdateEndDate || (updateDateKey != null && updateDateKey <= filterUpdateEndDate);

        return searchMatch && startDateMatch && endDateMatch && updateStartDateMatch && updateEndDateMatch;
    });
};

export const FilterBoardsDialog = ({ userId, filterState, setBoards, setFilterCount }: FilterBoardsDialogProps) => {
    const queryClient = useQueryClient();
    const [isFilteringBoards, setIsFilteringBoards] = filterState;
    const [filters, setFilters] = useState<BoardsFilterProps>({
        search: null as string | null,
        createDateRange: {
            startDate: null as string | null,
            endDate: null as string | null
        },
        updateDateRange: {
            startDate: null as string | null,
            endDate: null as string | null
        }
    });

    const boardsData = queryClient.getQueryData<IBoardResponse[]>(["boards", userId]);

    const handleFilterChange = (e: React.SubmitEvent<HTMLFormElement>) => {
        e.preventDefault();

        setFilterCount(
            (filters.search ? 1 : 0) +
            (filters.createDateRange.startDate ? 1 : 0) +
            (filters.createDateRange.endDate ? 1 : 0)
        );

        const filteredBoards = applyBoardFilters(boardsData ?? [], filters);
        setBoards(filteredBoards);
        setIsFilteringBoards(false);
    };

    const handleClearFilters = () => {
        setFilters({
            search: null,
            createDateRange: {
                startDate: null,
                endDate: null
            },
            updateDateRange: {
                startDate: null,
                endDate: null
            }
        });
        setFilterCount(0);
        setBoards(boardsData ?? []);
        setIsFilteringBoards(false);
    };

    return (
        <Fragment>
            <Dialog open={isFilteringBoards} onOpenChange={setIsFilteringBoards}>
                <DialogContent className="w-[95vw] min-w-max mx-auto">
                    <DialogHeader>
                        <DialogTitle>Filter Boards</DialogTitle>
                        <hr className="my-1" />
                        <p className="text-sm sm:text-xs text-gray-500">Filter your boards using the options</p>
                    </DialogHeader>
                    <form className="space-y-4" onSubmit={handleFilterChange}>
                        <div className="space-y-1">
                            <Label htmlFor="search" className="block text-sm font-medium text-gray-500">Search</Label>
                            <Input
                                type="text"
                                id="search"
                                name="search"
                                placeholder="Search board titles..."
                                value={filters.search || ""}
                                onChange={(e) => setFilters({
                                    ...filters,
                                    search: e.target.value || null
                                })}
                            />
                        </div>

                        <div className="space-y-2">
                            <Label className="block text-sm font-medium text-gray-500">Created Range</Label>
                            <div className="flex gap-4">
                                <div className="space-y-1 w-[50%]">
                                    <Label htmlFor="start-date" className="block text-xs font-medium text-gray-500">Start Date</Label>
                                    <Input
                                        type="date"
                                        id="start-date"
                                        name="start-date"
                                        value={filters.createDateRange.startDate || ""}
                                        onChange={(e) => setFilters({
                                            ...filters,
                                            createDateRange: {
                                                ...filters.createDateRange,
                                                startDate: e.target.value || null
                                            }
                                        })}
                                    />
                                </div>
                                <div className="space-y-1 w-[50%]">
                                    <Label htmlFor="end-date" className="block text-xs font-medium text-gray-500">End Date</Label>
                                    <Input
                                        type="date"
                                        id="end-date"
                                        name="end-date"
                                        value={filters.createDateRange.endDate || ""}
                                        onChange={(e) => setFilters({
                                            ...filters,
                                            createDateRange: {
                                                ...filters.createDateRange,
                                                endDate: e.target.value || null
                                            }
                                        })}
                                    />
                                </div>
                            </div>
                        </div>

                        <div className="space-y-2">
                            <Label className="block text-sm font-medium text-gray-500">Updated Range</Label>
                            <div className="flex gap-4">
                                <div className="space-y-1 w-[50%]">
                                    <Label htmlFor="updated-start-date" className="block text-xs font-medium text-gray-500">Start Date</Label>
                                    <Input
                                        type="date"
                                        id="updated-start-date"
                                        name="updated-start-date"
                                        value={filters.updateDateRange.startDate || ""}
                                        onChange={(e) => setFilters({
                                            ...filters,
                                            updateDateRange: {
                                                ...filters.updateDateRange,
                                                startDate: e.target.value || null
                                            }
                                        })}
                                    />
                                </div>
                                <div className="space-y-1 w-[50%]">
                                    <Label htmlFor="updated-end-date" className="block text-xs font-medium text-gray-500">End Date</Label>
                                    <Input
                                        type="date"
                                        id="updated-end-date"
                                        name="updated-end-date"
                                        value={filters.updateDateRange.endDate || ""}
                                        onChange={(e) => setFilters({
                                            ...filters,
                                            updateDateRange: {
                                                ...filters.updateDateRange,
                                                endDate: e.target.value || null
                                            }
                                        })}
                                    />
                                </div>
                            </div>
                        </div>


                        <div className="flex justify-between space-x-2 mt-8">
                            <Button type="button" size="lg" variant="outline" onClick={handleClearFilters}>Clear Filters</Button>
                            <Button type="submit" size="lg" className="ui-btn-style">Apply Filters</Button>
                        </div>
                    </form>
                </DialogContent >
            </Dialog >
        </Fragment >
    );
};