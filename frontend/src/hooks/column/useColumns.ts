import { getColumnsByBoardIdApi } from "@/apis/columnApi";
import { IColumnResponse } from "@/types/IColumn";
import { useQuery } from "@tanstack/react-query";

export function useColumns(boardId: string) {
    return useQuery<IColumnResponse[], Error>({
        queryKey: ['columns', boardId],
        queryFn: () => getColumnsByBoardIdApi(boardId),
        enabled: !!boardId,
        staleTime: 1000 * 60 * 5,
    });
}
