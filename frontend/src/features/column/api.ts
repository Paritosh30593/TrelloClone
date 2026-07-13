import axiApi from "@/lib/api/httpClient";
import { IColumnResponse, IColumnWithCardsResponse } from "@/features/column/IColumn";
import { HttpStatusCode } from "axios";

export async function getColumnsByBoardIdApi(boardId: string): Promise<IColumnResponse[]> {
    const response = await axiApi.get(`/api/columns/board/${boardId}`);
    return response.status === HttpStatusCode.Ok
        ? response.data as IColumnResponse[]
        : [] as IColumnResponse[];
}

export async function getColumnsByBoardIdWithCardsApi(boardId: number): Promise<IColumnWithCardsResponse[]> {
    const response = await axiApi.get(`/api/columns/board/${boardId}/with-cards`);
    return response.status === HttpStatusCode.Ok
        ? response.data as IColumnWithCardsResponse[]
        : [] as IColumnWithCardsResponse[];
}