import { useMutation, useQueryClient } from "@tanstack/react-query";
import { ICardResponse, ICardUpdateRequest } from "../ICard";
import { updateCardsApi } from "../api";
import { IColumnWithCardsResponse } from "@/features/column/IColumn";


export function useUpdateCards(boardId: string) {
    const queryClient = useQueryClient();

    return useMutation<ICardResponse[], Error, ICardUpdateRequest[]>({
        mutationFn: (cards: ICardUpdateRequest[]) => {
            if (cards.length === 0)
                throw new Error("No cards to update");

            return updateCardsApi(cards);
        },
        onSuccess: (updatedCards) => {
            queryClient.setQueryData<IColumnWithCardsResponse[]>(['columns-with-cards', boardId], (existingColumns) => {
                if (!existingColumns) return existingColumns;

                return existingColumns.map((column) =>
                    column.id === updatedCards[0].columnId
                        ? {
                            ...column,
                            cards: column.cards.map((card) => {
                                const updatedCard = updatedCards.find((c) => c.id === card.id);
                                return updatedCard
                                    ? { ...card, ...updatedCard }
                                    : card;
                            })
                        }
                        : column
                );
            });
        },
        onError: () => {
            queryClient.setQueryData<IColumnWithCardsResponse[]>(['columns-with-cards', boardId], (existingColumns) => {
                if (!existingColumns) return existingColumns;
                return existingColumns;
            });
        }
    });
};
