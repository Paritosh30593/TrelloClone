import { getBoardByIdApi } from "@/apis/boardApi";
import { IBoardResponse } from "@/types/IBoard";
import { useQuery } from "@tanstack/react-query";

export function useBoard(boardId: string) {
    return useQuery<IBoardResponse, Error>({
        queryKey: ['board', boardId],
        queryFn: () => getBoardByIdApi(boardId),
        enabled: !!boardId,
        staleTime: 1000 * 60 * 5,
    });
}
