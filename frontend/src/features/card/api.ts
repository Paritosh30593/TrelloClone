import { HttpStatusCode } from "axios";
import { ICardAddRequest, ICardResponse, ICardUpdateRequest } from "./ICard";
import axiApi from "@/lib/api/httpClient";


export async function createCardApi(cardData: ICardAddRequest): Promise<ICardResponse> {
    const response = await axiApi.post(`/api/cards`, cardData);
    return response.status === HttpStatusCode.Ok
        ? response.data
        : {} as ICardResponse;
}

export async function updateCardApi(cardData: ICardUpdateRequest): Promise<ICardResponse> {
    const response = await axiApi.put(`/api/cards/${cardData.id}`, cardData);
    return response.status === HttpStatusCode.Ok
        ? response.data
        : {} as ICardResponse;
}