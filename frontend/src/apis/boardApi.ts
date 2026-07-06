import { IBoardAddRequest, IBoardResponse } from "@/types/IBoard";
import axios, { HttpStatusCode } from "axios";

const API_BASE_URL = process.env.NEXT_PUBLIC_API_BASE_URL || "https://localhost:5002";

export async function createBoardApi(board: IBoardAddRequest): Promise<IBoardResponse> {
    const response = await axios.post<IBoardResponse>(`${API_BASE_URL}/api/boards`, board);
    return response.status === HttpStatusCode.Ok
        ? response.data as IBoardResponse
        : {} as IBoardResponse;
}

export async function getBoardsByUserIdApi(userId: string): Promise<IBoardResponse[]> {
    const response = await axios.get<IBoardResponse[]>(`${API_BASE_URL}/api/boards/user/${userId}`);
    return response.status === HttpStatusCode.Ok
        ? response.data as IBoardResponse[]
        : [] as IBoardResponse[];
}

export async function getBoardByIdApi(boardId: string): Promise<IBoardResponse> {
    const response = await axios.get<IBoardResponse>(`${API_BASE_URL}/api/boards/${boardId}`);
    return response.status === HttpStatusCode.Ok
        ? response.data as IBoardResponse
        : {} as IBoardResponse;
}