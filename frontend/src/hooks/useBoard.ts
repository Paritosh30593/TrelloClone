import { getBoardByIdApi } from "@/apis/boardApi";
import { getColumnsByBoardIdApi } from "@/apis/columnApi";
import { IBoardResponse } from "@/types/IBoard";
import { IColumnResponse } from "@/types/IColumn";
import { useQueries } from "@tanstack/react-query";

type UseBoardReturnType<T> = {
    queryKey: string[];
    queryFn: () => Promise<T>;
    enabled: boolean;
    staleTime: number;
};

export function useBoard(boardId: string) {
    const [fetchBoardById, fetchColumnsByBoardId] = useQueries<[UseBoardReturnType<IBoardResponse>, UseBoardReturnType<IColumnResponse[]>]>({
        queries: [
            {
                queryKey: ['board', boardId],
                queryFn: () => getBoardByIdApi(boardId),
                enabled: !!boardId,
                staleTime: 1000 * 60 * 5,
            },
            {
                queryKey: ['columns', boardId],
                queryFn: () => getColumnsByBoardIdApi(boardId),
                enabled: !!boardId,
                staleTime: 1000 * 60 * 5,
            }
        ],
    });

    return {
        fetchBoardById,
        fetchColumnsByBoardId,
    }
}