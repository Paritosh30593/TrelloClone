"use client";

import Link from "next/link";
import { Navbar } from "@/components/common/navbar";
import { Badge } from "@/components/ui/badge";
import { Button } from "@/components/ui/button";
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card";
import { Input } from "@/components/ui/input";
import { useCreateBoard } from "@/hooks/board/useCreateBoard";
import { useUserBoards } from "@/hooks/board/useUserBoards";
import { useUser } from "@clerk/nextjs";
import { ClockFading, Filter, Grid3X3, Kanban, List, Plus, Rocket, Search } from "lucide-react";
import { Fragment, useState } from "react";

export default function DashboardPage() {
    const { user } = useUser();
    const [viewMode, setViewMode] = useState<"grid" | "list">("grid");

    const { data: userBoards, error: fetchBoardsError, isPending: isFetchingBoards, isError: isFetchBoardsError } = useUserBoards(user?.id);
    const { mutate: createBoardMutate } = useCreateBoard();

    const handleCreateBoard = () => {
        if (!user?.id) return;

        createBoardMutate({
            userId: user.id,
            title: "New Board",
            description: "This is a new board",
            color: "bg-purple-600",
        });
    }

    return (
        <Fragment>
            <Navbar />
            <main className="container mx-auto px-4 py-6 sm:py-8">
                <div className="mb-6 sm:mb-8">
                    <h1 className="text-2xl sm:text-3xl font-bold text-gray-800 mb-2">
                        Welcome back, {user?.firstName ?? user?.lastName ?? "User"}
                    </h1>
                    <p className="text-gray-600">
                        Here&apos;s what&apos;s happening with your boards today:
                    </p>

                    <Button className="w-full sm:w-auto mt-2" onClick={handleCreateBoard}><Plus className="h-4 w-4 mr-2" />Create Board</Button>
                </div>

                {/* Stats */}
                <div className="grid grid-cols-2 lg:grid-cols-4 gap-4 sm:gap-6 mb-6 sm:mb-8">
                    <Card>
                        <CardContent className="p-4 sm:p-6">
                            <div className="flex items-center justify-between">
                                <div>
                                    <p className="text-xs sm:text-sm font-medium text-gray-600">Total Boards</p>
                                    <p className="text-xl sm:text-2xl font-bold text-gray-900">{userBoards?.length}</p>
                                </div>
                                <div className="h-10 w-10 sm:h-12 sm:w-12 bg-purple-100 p-2 rounded-lg flex items-center justify-center">
                                    <Kanban className="h-5 w-5 sm:h-6 sm:w-6 text-purple-600" />
                                </div>
                            </div>
                        </CardContent>
                    </Card>


                    <Card>
                        <CardContent className="p-4 sm:p-6">
                            <div className="flex items-center justify-between">
                                <div>
                                    <p className="text-xs sm:text-sm font-medium text-gray-600">Active Projects</p>
                                    <p className="text-xl sm:text-2xl font-bold text-gray-900">{userBoards?.length}</p>
                                </div>
                                <div className="h-10 w-10 sm:h-12 sm:w-12 bg-green-100 p-2 rounded-lg flex items-center justify-center">
                                    <Rocket className="h-5 w-5 sm:h-6 sm:w-6 text-green-600" />
                                </div>
                            </div>
                        </CardContent>
                    </Card>


                    <Card>
                        <CardContent className="p-4 sm:p-6">
                            <div className="flex items-center justify-between">
                                <div>
                                    <p className="text-xs sm:text-sm font-medium text-gray-600">Recent Activity</p>
                                    <p className="text-xl sm:text-2xl font-bold text-gray-900">{userBoards?.filter(board => {
                                        const updatedAt = new Date(board.updatedAt);
                                        const now = new Date();
                                        now.setDate(now.getDate() - 7);
                                        return updatedAt > now;
                                    }).length}</p>
                                </div>
                                <div className="h-10 w-10 sm:h-12 sm:w-12 bg-amber-100 p-2 rounded-lg flex items-center justify-center">
                                    <ClockFading className="h-5 w-5 sm:h-6 sm:w-6 text-amber-600" />
                                </div>
                            </div>
                        </CardContent>
                    </Card>


                    <Card>
                        <CardContent className="p-4 sm:p-6">
                            <div className="flex items-center justify-between">
                                <div>
                                    <p className="text-xs sm:text-sm font-medium text-gray-600">Total Boards</p>
                                    <p className="text-xl sm:text-2xl font-bold text-gray-900">{userBoards?.length ?? 0}</p>
                                </div>
                                <div className="h-10 w-10 sm:h-12 sm:w-12 bg-purple-100 p-2 rounded-lg flex items-center justify-center">
                                    <Kanban className="h-5 w-5 sm:h-6 sm:w-6 text-purple-600" />
                                </div>
                            </div>
                        </CardContent>
                    </Card>
                </div>


                {/* Boards */}
                <div className="mb-6 sm:mb-8">
                    <div className="flex flex-col sm:flex-row sm:items-center sm:justify-between mb-4 sm:mb-6 space-y-4 sm:space-y-0">
                        <div>
                            <h2 className="text-xl sm:text-2xl font-bold text-gray-800 mb-2">Your Boards</h2>
                            <p className="text-gray-600">Manage your tasks and projects.</p>
                        </div>
                        <div className="flex flex-col sm:flex-row items-stretch sm:items-center space-y-2 sm:space-y-0">
                            <div className="flex items-center bg-white space-x-2 border p-1 rounded-md">
                                <Button
                                    size="sm"
                                    variant={viewMode === "grid" ? "default" : "ghost"}
                                    onClick={() => setViewMode("grid")}>
                                    <Grid3X3 className="h-4 w-4" />
                                </Button>
                                <Button
                                    size="sm"
                                    variant={viewMode === "list" ? "default" : "ghost"}
                                    onClick={() => setViewMode("list")}>
                                    <List className="h-4 w-4" />
                                </Button>
                            </div>
                            <Button
                                variant="secondary"
                                size="sm">
                                <Filter className="h-4 w-4 mr-2" />Filter
                            </Button>

                            <Button
                                size="sm"
                                onClick={handleCreateBoard}>
                                <Plus className="h-4 w-4 mr-2" />Create Board
                            </Button>
                        </div>
                    </div>


                    {/* Search Box */}
                    <div className="mb-4 relative sm:mb-6">
                        <Search className="h-4 w-4 absolute left-3 top-1/2 transform -translate-y-1/2 text-gray-400" />
                        <Input
                            id="search"
                            type="text"
                            placeholder="Search boards..."
                            className="pl-10"
                        />
                    </div>

                    {/* Boards List/Grid */}
                    {
                        isFetchingBoards ?
                            <p>Loading boards...</p>
                            : isFetchBoardsError
                                ? <p>Error fetching boards: {fetchBoardsError?.message}</p>
                                : (userBoards?.length ?? 0) === 0
                                    ? (
                                        <div className="text-center py-10">
                                            <p className="text-gray-600 mb-4">You have no boards yet. Create your first board to get started!</p>
                                            <Button onClick={handleCreateBoard}><Plus className="h-4 w-4 mr-2" />Create Board</Button>
                                        </div>
                                    )
                                    : (
                                        <div className={`grid ${viewMode === "grid"
                                            ? "grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-4 sm:gap-6"
                                            : "grid-cols-1 gap-2"}`
                                        }>
                                            {
                                                userBoards?.map((board) => (
                                                    <Link href={`/boards/${board.id}`} key={board.id} className="no-underline">
                                                        <Card className="hover:shadow-lg transition-shadow cursor-pointer group">
                                                            <CardHeader className="pb-2">
                                                                <div className="flex items-center justify-between">
                                                                    <div className={`h-4 w-4 rounded ${board.color ?? "bg-purple-600"}`} />
                                                                    <Badge className="text-xs" variant="secondary">New</Badge>
                                                                </div>
                                                            </CardHeader>
                                                            <CardContent className="pl-4 pr-4 pt-0 pb-2 sm:pl-6 sm:pr-6 sm:pt-0 sm:pb-2">
                                                                <CardTitle className="text-base sm:text-lg mb-2 group-hover:text-purple-600 transition-colors">
                                                                    {board.title}
                                                                </CardTitle>
                                                                <CardDescription className="text-sm mb-4">
                                                                    {board.description}
                                                                </CardDescription>
                                                                <div className="flex flex-col sm:flex-row sm:items-center sm:justify-between text-xs text-gray-500 mt-4 space-y-1 sm:space-y-0">
                                                                    <span>
                                                                        Created{" "}
                                                                        {new Date(board.createdAt).toLocaleDateString()}
                                                                    </span>
                                                                    <span>
                                                                        Updated{" "}
                                                                        {new Date(board.updatedAt).toLocaleDateString()}
                                                                    </span>
                                                                </div>
                                                            </CardContent>
                                                        </Card>
                                                    </Link>
                                                ))
                                            }
                                            <Card className="hover:shadow-lg transition-shadow cursor-pointer group border-dashed border-2 bg-transparent border-gray-400 hover:border-purple-500 flex items-center justify-center">
                                                <CardContent className="p-4 sm:p-6 flex items-center justify-center cursor-pointer transition-colors text-gray-400 group-hover:text-purple-600" onClick={handleCreateBoard}>
                                                    <Plus className="h-6 w-6 sm:h-8 sm:w-8 " />
                                                    <p className="text-sm sm:text-base">Create new board</p>
                                                </CardContent>
                                            </Card>
                                        </div>
                                    )
                    }
                </div>
            </main>
        </Fragment>

    );
};
