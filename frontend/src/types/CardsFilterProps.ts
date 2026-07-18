import { PriorityOptions } from "./PriorityOptions";

export type CardsFilterProps = {
    priority: PriorityOptions[];
    assignee: string | null;
    dueDate: string | null;
};