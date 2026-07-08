import { Priority } from "./Priority";

export interface ICard {
    id: number;
    columnId: number;
    title: string;
    description: string | null;
    assignee: string;
    dueDate: string | null;
    priority: Priority;
    sortOrder: number;
    createdAt: string;
}