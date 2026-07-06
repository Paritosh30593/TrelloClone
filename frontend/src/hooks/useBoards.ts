"use client";

import { createBoardApi, getBoardsByUserIdApi } from "@/apis/boardApi";
import { IBoardAddRequest, IBoardResponse } from "@/types/IBoard";
import { useUser } from "@clerk/nextjs";
import { useMutation, useQuery } from "@tanstack/react-query";

export function useBoards() {
    const { user } = useUser();

    const fetchUserBoards = useQuery<IBoardResponse[], Error>({
        queryKey: ['boards', user?.id],
        queryFn: () => getBoardsByUserIdApi(user!.id),
        enabled: !!user?.id,
        staleTime: 1000 * 60 * 5, // 5 minutes
    });

    const createBoard = useMutation<IBoardResponse, Error, IBoardAddRequest>({
        mutationFn: (board: IBoardAddRequest) => {
            if (board.userId === "")
                throw new Error("User not authenticated");
            return createBoardApi({ ...board });
        }
    });

    return {
        fetchUserBoards,
        createBoard
    }
}   
