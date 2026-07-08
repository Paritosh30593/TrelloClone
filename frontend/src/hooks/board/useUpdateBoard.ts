import { updateBoardApi } from "@/apis/boardApi";
import { IBoardResponse, IBoardUpdateRequest } from "@/types/IBoard";
import { useMutation, useQueryClient } from "@tanstack/react-query";

export function useUpdateBoard() {
    const queryClient = useQueryClient();

    return useMutation<IBoardResponse, Error, IBoardUpdateRequest>({
        mutationFn: (board: IBoardUpdateRequest) => {
            if (board.userId === "")
                throw new Error("User not authenticated");

            return updateBoardApi(board);
        },
        onSuccess: (updatedBoard) => {
            queryClient.setQueryData<IBoardResponse>(['board', String(updatedBoard.id)], updatedBoard);

            queryClient.setQueryData<IBoardResponse[]>(['boards', updatedBoard.userId], (existingBoards) => {
                if (!existingBoards) return existingBoards;

                return existingBoards.map((board) =>
                    board.id === updatedBoard.id
                        ? updatedBoard
                        : board
                );
            });
        }
    });
}