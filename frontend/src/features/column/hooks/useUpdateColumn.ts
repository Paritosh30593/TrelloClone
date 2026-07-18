import { useMutation, useQueryClient } from "@tanstack/react-query";
import { IColumnResponse, IColumnUpdateRequest, IColumnWithCardsResponse } from "../IColumn";
import { updateColumnApi } from "../api";


export const useUpdateColumn = (boardId: string) => {
    const queryClient = useQueryClient();

    return useMutation<IColumnResponse, Error, IColumnUpdateRequest>({
        mutationFn: (updateRequest: IColumnUpdateRequest) => {
            if (updateRequest.boardId === undefined || !updateRequest.title) {
                throw new Error("Board ID, Column ID, and title are required to update a column");
            }
            return updateColumnApi(updateRequest);
        },
        onSuccess: (updatedColumn) => {
            queryClient.setQueryData<IColumnWithCardsResponse[]>(['columns-with-cards', boardId], (existingColumns) => {
                if (!existingColumns) return [{ ...updatedColumn, cards: [] }];

                return existingColumns.map(column =>
                    column.id === updatedColumn.id
                        ? { ...updatedColumn, cards: column.cards }
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