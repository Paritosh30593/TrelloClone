import { useMutation, useQueryClient } from "@tanstack/react-query";
import { IColumnAddRequest, IColumnResponse, IColumnWithCardsResponse } from "../IColumn";
import { createColumnApi } from "../api";


export const useCreateColumn = (boardId: string) => {
    const queryClient = useQueryClient();

    return useMutation<IColumnResponse, Error, IColumnAddRequest>({
        mutationFn: (column: IColumnAddRequest) => {
            if (!boardId || !column.title)
                throw new Error("Board ID and title are required to create a column");

            return createColumnApi(column);
        },
        onSuccess: (newColumn) => {
            console.log(newColumn);
            queryClient.setQueryData<IColumnWithCardsResponse[]>(['columns-with-cards', boardId], (existingColumns) => {
                if (!existingColumns) return [
                    {
                        ...newColumn,
                        cards: []
                    } as IColumnWithCardsResponse
                ];

                return [
                    ...existingColumns,
                    {
                        ...newColumn,
                        cards: []
                    } as IColumnWithCardsResponse
                ];
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