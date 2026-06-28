"use client";

import { Button } from "@/components/ui/button";
import { useBoards } from "@/hooks/useBoards";
import { useUser } from "@clerk/nextjs";
import { Plus } from "lucide-react";

export default function DashboardPage() {
    const { user } = useUser();
    const { createBoard } = useBoards();

    return (
        <div className="min-h-screen">

            <main className="container mx-auto px-4 py-6 sm:py-8">
                <div className="mb-6 sm:mb-8">
                    <h1 className="text-2xl sm:text-3xl font-bold text-gray-800 mb-2">
                        Welcome back, {user?.firstName ?? user?.lastName ?? "User"}
                    </h1>
                    <p className="text-gray-600">
                        Here&apos;s what&apos;s happening with your boards today:
                    </p>

                    <Button className="w-full sm:w-auto"><Plus className="h-4 w-4 mr-2" /> New Board</Button>
                </div>
            </main>
        </div>
    );
};