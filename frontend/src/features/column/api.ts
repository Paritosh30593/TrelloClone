import axiApi from "@/lib/api/httpClient";
import { IColumnAddRequest, IColumnResponse, IColumnUpdateRequest, IColumnWithCardsResponse } from "@/features/column/IColumn";
import { HttpStatusCode } from "axios";

export async function getColumnsByBoardIdApi(boardId: string): Promise<IColumnResponse[]> {
    const response = await axiApi.get(`columns/board/${boardId}`);
    return response.status === HttpStatusCode.Ok
        ? response.data as IColumnResponse[]
        : [] as IColumnResponse[];
}

export async function getColumnsByBoardIdWithCardsApi(boardId: number): Promise<IColumnWithCardsResponse[]> {
    const response = await axiApi.get(`columns/board/${boardId}/with-cards`);
    return response.status === HttpStatusCode.Ok
        ? response.data as IColumnWithCardsResponse[]
        : [] as IColumnWithCardsResponse[];
}

export async function createColumnApi(columnData: IColumnAddRequest): Promise<IColumnResponse> {
    const response = await axiApi.post(`columns`, columnData);
    return response.status === HttpStatusCode.Ok
        ? response.data as IColumnResponse
        : {} as IColumnResponse;
}

export async function updateColumnApi(columnData: IColumnUpdateRequest): Promise<IColumnResponse> {
    const response = await axiApi.put(`columns`, columnData);
    return response.status === HttpStatusCode.Ok
        ? response.data as IColumnResponse
        : {} as IColumnResponse;
}