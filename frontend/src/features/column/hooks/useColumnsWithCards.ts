import { getColumnsByBoardIdWithCardsApi } from "@/features/column/api";
import { IColumnWithCardsResponse } from "@/features/column/IColumn";
import { useQuery } from "@tanstack/react-query";

export function useColumnsWithCards(boardId: string, enabled: boolean = true) {
    return useQuery<IColumnWithCardsResponse[], Error>({
        queryKey: ['columns-with-cards', boardId],
        queryFn: () => getColumnsByBoardIdWithCardsApi(Number(boardId)),
        enabled: !!boardId && enabled
    });
}