import { PriorityOptions } from "../../types/PriorityOptions";

export interface ICardResponse {
    id: number;
    columnId: number;
    title: string;
    description: string | null;
    assignee: string | null;
    dueDate: string | null;
    priority: string;
    sortOrder: number;
    createdAt: string;
    updatedAt: string;
}

export interface ICardAddRequest {
    columnId: number;
    title: string;
    description?: string | null;
    assignee?: string | null;
    dueDate?: string | null;
    priority?: PriorityOptions;
    sortOrder: number;
}

export interface ICardUpdateRequest {
    id: number;
    columnId: number;
    title: string;
    description?: string | null;
    assignee?: string | null;
    dueDate?: string | null;
    priority?: PriorityOptions;
    sortOrder: number;
}