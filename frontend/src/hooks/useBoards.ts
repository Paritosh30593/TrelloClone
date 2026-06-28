import { IBoardAddRequest, IBoardResponse } from "@/types/IBoard";

export function useBoards() {

    const createBoard = async (board: IBoardAddRequest): Promise<IBoardResponse> => {

    }

    return {
        createBoard
    }
}