export interface IColumnResponse {
    id: number;
    boardId: number;
    title: string;
    sortOrder: number;
    createdAt: string;
}

export interface IColumnAddRequest {
    boardId: number;
    title: string;
    sortOrder: number;
}

export interface IColumnUpdateRequest {
    id: number;
    boardId: number;
    title: string;
    sortOrder: number;
}