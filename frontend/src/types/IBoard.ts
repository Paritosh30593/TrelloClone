export interface IBoard {
    id: number;
    userId: string;
    title: string | null;
    description: string | null;
    color: string;
    createdAt: string;
    updatedAt: string;
};