import axiApi from "@/lib/api/httpClient";
import { IBoardAddRequest, IBoardResponse, IBoardUpdateRequest } from "@/features/board/IBoard";
import { HttpStatusCode } from "axios";


export async function createBoardApi(board: IBoardAddRequest): Promise<IBoardResponse> {
    const response = await axiApi.post(`/api/boards`, board);
    return response.status === HttpStatusCode.Ok
        ? response.data
        : {} as IBoardResponse;
}

export async function updateBoardApi(board: IBoardUpdateRequest): Promise<IBoardResponse> {
    const response = await axiApi.put(`/api/boards`, board);
    return response.status === HttpStatusCode.Ok
        ? response.data
        : {} as IBoardResponse;
}

export async function getBoardsByUserIdApi(userId: string): Promise<IBoardResponse[]> {
    const response = await axiApi.get(`/api/boards/user/${userId}`);
    return response.status === HttpStatusCode.Ok
        ? response.data
        : [] as IBoardResponse[];
}

export async function getBoardByIdApi(boardId: string): Promise<IBoardResponse> {
    // await new Promise(resolve => setTimeout(resolve, 5000)); // Simulate a delay of 5 seconds

    const response = await axiApi.get(`/api/boards/${boardId}`);
    return response.status === HttpStatusCode.Ok
        ? response.data
        : {} as IBoardResponse;
}
