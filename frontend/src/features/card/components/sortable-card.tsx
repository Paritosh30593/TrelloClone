import { Card, CardContent } from "@/components/ui/card";
import { ICardResponse } from "../ICard";
import { Calendar, User } from "lucide-react";
import { getPriorityColor } from "@/lib/utils";
import { useSortable } from "@dnd-kit/sortable";
import { CSS } from "@dnd-kit/utilities";


export const SortableCard = ({ card }: { card: ICardResponse }) => {
    const {
        attributes,
        listeners,
        setNodeRef,
        transform,
        transition,
        isDragging
    } = useSortable({ id: card.id });

    const style = {
        transform: CSS.Transform.toString(transform),
        transition,
        opacity: isDragging ? 0.5 : 1, // Optional: Change opacity when dragging
    };

    return (
        <div ref={setNodeRef} style={style} {...attributes} {...listeners}>
            <Card className="cursor-pointer hover:shadow-md transition-shadow duration-300">
                <CardContent className="p-3 sm:p-4">
                    <div className="space-y-2 sm:space-y-3">
                        {/* Task Header */}
                        <div className="flex items-start justify-between">
                            <h4 className="font-medium text-gray-900 text-sm leading-tight flex-1 min-w-0">{card.title}</h4>

                        </div>

                        {/* Task Description */}
                        <p className="text-gray-600 text-xs line-clamp-2">{card.description || "No description"}</p>

                        {/* Task Metadata */}
                        <div className="flex items-center justify-between">
                            <div className="flex items-center space-x-1 sm:space-x-2 min-w-0">
                                {card.assignee && (
                                    <div className="flex items-center space-x-1 text-xs text-gray-600">
                                        <User className="w-3 h-3" />
                                        <span className="truncate">{card.assignee}</span>
                                    </div>
                                )}
                                {card.dueDate && (
                                    <div className="flex items-center space-x-1 text-xs text-gray-600">
                                        <Calendar className="w-3 h-3" />
                                        <span className="truncate">{new Date(card.dueDate).toLocaleDateString()}</span>
                                    </div>
                                )}
                            </div>
                            <div className={`w-3 h-3 rounded-full shrink-0 ${getPriorityColor(card.priority)}`} />
                        </div>
                    </div>
                </CardContent>
            </Card >
        </div>

    );
};