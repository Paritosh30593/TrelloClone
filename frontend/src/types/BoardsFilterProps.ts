export type BoardsFilterProps = {
    search: string | null;
    createDateRange: {
        startDate: string | null;
        endDate: string | null;
    };
    updateDateRange: {
        startDate: string | null;
        endDate: string | null;
    };
};