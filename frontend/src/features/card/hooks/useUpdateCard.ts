import { useMutation } from "@tanstack/react-query";
import { updateCardApi } from "../api";
import { ICardUpdateRequest, ICardResponse } from "../ICard";


export function useUpdateCard() {
    return useMutation<ICardResponse, Error, ICardUpdateRequest>({
        mutationFn: (card: ICardUpdateRequest) => updateCardApi(card),
    });
};
