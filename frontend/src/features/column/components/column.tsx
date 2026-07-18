import { Badge } from "@/components/ui/badge";
import { IColumnWithCardsResponse } from "../IColumn";
import { Button } from "@/components/ui/button";
import { MoreHorizontalIcon, Plus } from "lucide-react";
import { AddCardDialog } from "@/features/card/components/add-card-dialog";
import { DialogTrigger } from "@/components/ui/dialog";
import { useDroppable } from "@dnd-kit/core";

type ColumnProps = {
    column: IColumnWithCardsResponse;
    color: string;
    children?: React.ReactNode;
    onEditClick: () => void;
};

export const Column = ({ column, color, children, onEditClick }: ColumnProps) => {
    const { setNodeRef, isOver, } = useDroppable({ id: column.id });

    return (
        <div
            className={`w-full lg:shrink-0 lg:w-80 ${isOver ? "bg-purple-100 rounded-lg" : ""}`}
            ref={setNodeRef}
        >
            <div className="bg-white flex flex-col justify-between h-full rounded-lg shadow-sm border">
                {/* Column Header */}
                <div className="p-3 sm:p-4 border-b">
                    <div className="flex items-center justify-between">
                        <div className="flex items-center space-x-2 min-w-0">
                            <h3 className="font-semibold text-gray-900 text-sm sm:text-base truncate">{column.title}</h3>
                            <Badge className={`text-xs shrink-0 rounded-sm ${color}`}>{column.cards.length}</Badge>
                        </div>
                        <Button
                            size="sm"
                            variant="ghost"
                            className="shrink-0"
                            onClick={onEditClick}
                        >
                            <MoreHorizontalIcon />
                        </Button>
                    </div>
                </div>

                {/* Column content */}
                <div className="min-h-25 h-full p-2">
                    {children}
                </div>

                <AddCardDialog boardId={String(column.boardId)} columnsData={[column]}>
                    <DialogTrigger asChild>
                        <Button size="sm" variant="ghost" className="flex items-center gap-2 px-2 py-4 justify-center w-full text-gray-600 hover:bg-gray-100 rounded-b-lg">
                            <Plus className="w-4 h-4" />Add Task
                        </Button>
                    </DialogTrigger>
                </AddCardDialog>
            </div>
        </div >
    );
};