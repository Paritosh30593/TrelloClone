import { IColumnResponse } from "@/types/IColumn";
import axios, { HttpStatusCode } from "axios";

const API_BASE_URL = process.env.NEXT_PUBLIC_API_BASE_URL || "https://localhost:5002";

export async function getColumnsByBoardIdApi(boardId: string): Promise<IColumnResponse[]> {
    const response = await axios.get<IColumnResponse[]>(`${API_BASE_URL}/api/columns/board/${boardId}`);
    return response.status === HttpStatusCode.Ok
        ? response.data as IColumnResponse[]
        : [] as IColumnResponse[];
}