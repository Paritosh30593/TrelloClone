export interface IBoardResponse {
    id: number;
    userId: string;
    title: string;
    description: string | null;
    color: string;
    createdAt: string;
    updatedAt: string;
};

export interface IBoardAddRequest {
    userId: string;
    title: string;
    description: string | null;
    color: string;
};

export interface IBoardUpdateRequest {
    id: number;
    userId: string;
    title: string;
    description: string | null;
    color: string;
}