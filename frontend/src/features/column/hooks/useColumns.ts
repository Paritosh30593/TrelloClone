import { getColumnsByBoardIdApi } from "@/features/column/api";
import { IColumnResponse } from "@/features/column/IColumn";
import { useQuery } from "@tanstack/react-query";

export function useColumns(boardId: string, enabled: boolean = true) {
    return useQuery<IColumnResponse[], Error>({
        queryKey: ['columns', boardId],
        queryFn: () => getColumnsByBoardIdApi(boardId),
        enabled: !!boardId && enabled
    });
}
