import { useMutation, useQueryClient } from "@tanstack/react-query";
import { createCardApi } from "../api";
import { ICardAddRequest, ICardResponse } from "../ICard";
import { IColumnWithCardsResponse } from "@/features/column/IColumn";


export function useCreateCard(boardId: string) {
    const queryClient = useQueryClient();

    return useMutation<ICardResponse, Error, ICardAddRequest>({
        mutationFn: (card: ICardAddRequest) => {
            if (card.columnId === 0)
                throw new Error("Column ID is required");
            return createCardApi(card);
        },
        onSuccess: (newCard) => {
            queryClient.setQueryData<IColumnWithCardsResponse[]>(
                ["columns-with-cards", boardId],
                (existingColumns) => {
                    if (!existingColumns) return existingColumns;

                    const index = existingColumns.findIndex((c) => c.id === newCard.columnId);
                    if (index === -1) return existingColumns;

                    const target = existingColumns[index];
                    existingColumns[index] = { ...target, cards: [...target.cards, newCard] };
                    return existingColumns;
                }
            );
        }
    });
};