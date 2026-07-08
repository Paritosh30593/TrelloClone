import { createBoardApi } from "@/apis/boardApi";
import { IBoardAddRequest, IBoardResponse } from "@/types/IBoard";
import { useMutation, useQueryClient } from "@tanstack/react-query";

export function useCreateBoard() {
    const queryClient = useQueryClient();

    return useMutation<IBoardResponse, Error, IBoardAddRequest>({
        mutationFn: (board: IBoardAddRequest) => {
            if (board.userId === "")
                throw new Error("User not authenticated");
            return createBoardApi(board);
        },
        onSuccess: (newBoard) => {
            queryClient.setQueryData<IBoardResponse[]>(['boards', newBoard.userId], (existingBoards) => {
                if (!existingBoards) return [newBoard];
                return [...existingBoards, newBoard];
            });
        }
    });
}