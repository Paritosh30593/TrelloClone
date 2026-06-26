import { PriorityTypes } from "./PriorityTypes";

export interface ICard {
    id: number;
    columnId: number;
    title: string;
    description: string | null;
    assignee: string;
    dueDate: string | null;
    priority: PriorityTypes;
    sortOrder: number;
    createdAt: string;
}