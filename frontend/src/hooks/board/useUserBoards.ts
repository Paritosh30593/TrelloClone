import { getBoardsByUserIdApi } from "@/apis/boardApi";
import { IBoardResponse } from "@/types/IBoard";
import { useQuery } from "@tanstack/react-query";

export function useUserBoards(id: string | undefined) {
    return useQuery<IBoardResponse[], Error>({
        queryKey: ['boards', id],
        queryFn: () => getBoardsByUserIdApi(id!),
        enabled: !!id,
        //staleTime: 1000 * 60 * 5, // 5 minutes
        //gcTime: 1000 * 60 * 10, // 10 minutes
    });
}   
