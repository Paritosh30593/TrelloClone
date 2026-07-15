import { getBoardByIdApi } from "@/features/board/api";
import { IBoardResponse } from "@/features/board/IBoard";
import { useQuery } from "@tanstack/react-query";

export function useBoard(boardId: string) {
    return useQuery<IBoardResponse, Error>({
        queryKey: ['board', boardId],
        queryFn: () => getBoardByIdApi(boardId),
        enabled: !!boardId
    });
}
