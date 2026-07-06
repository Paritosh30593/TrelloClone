"use client";

import { SignInButton, SignUpButton, UserButton, useUser } from "@clerk/nextjs";
import { MoreHorizontal, SquareKanban } from "lucide-react";
import Link from "next/link";
import { Button } from "../ui/button";
import { usePathname } from "next/navigation";

export const Navbar = ({ boardTitle, isEditingTitle, setIsEditingTitle }: {
    boardTitle?: string,
    isEditingTitle?: boolean,
    setIsEditingTitle?: (isEditing: boolean) => void
}) => {
    const { isSignedIn } = useUser();
    const pathname = usePathname();

    const isDashboardPage = pathname === "/dashboard" && isSignedIn;
    const isBoardsPage = pathname.includes("/boards/") && isSignedIn;

    if (isDashboardPage) {
        return (
            <header className="border-b bg-white/50 backdrop-blur-sm sticky top-0 z-40">
                <div className="container mx-auto px-4 py-3 sm:py-4 flex items-center justify-between">
                    <Link href="/dashboard" className="flex items-center gap-2">
                        <SquareKanban className="h-5 w-5 sm:h-7 sm:w-7 text-purple-600" />
                        <span className="text-lg sm:text-xl font-bold text-gray-800">Taskman</span>
                    </Link>

                    <div className="flex items-center gap-2 sm:gap-3">
                        <UserButton userProfileMode="navigation" userProfileUrl="/user-profile" />
                    </div>
                </div>
            </header>
        );
    }

    if (isBoardsPage) {
        return (
            <header className="border-b bg-white/50 backdrop-blur-sm sticky top-0 z-40">
                <div className="container mx-auto px-4 py-3 sm:py-4 flex items-center justify-between">
                    <div className="flex items-center gap-2 sm:gap-3">
                        <Link href="/dashboard" className="flex items-center gap-2">
                            <SquareKanban className="h-5 w-5 sm:h-7 sm:w-7 text-purple-600" />
                            <span className="text-lg sm:text-xl font-bold text-gray-800">{boardTitle}</span>
                        </Link>
                        {!isEditingTitle && (
                            <Button variant="ghost" size="sm" className="h-7 w-7 shrink-0 p-0" onClick={() => setIsEditingTitle?.(true)}>
                                <MoreHorizontal />
                            </Button>
                        )}
                    </div>

                    <div className="flex items-center gap-2 sm:gap-3">
                        <UserButton userProfileMode="navigation" userProfileUrl="/user-profile" />
                    </div>
                </div>
            </header>
        );
    }

    return (
        <header className="border-b bg-white/50 backdrop-blur-sm sticky top-0 z-40">
            <div className="container mx-auto px-4 py-3 sm:py-4 flex items-center justify-between">
                <Link href={isSignedIn ? "/dashboard" : "/"} className="flex items-center gap-2">
                    <SquareKanban className="h-5 w-5 sm:h-7 sm:w-7 text-purple-600" />
                    <span className="text-lg sm:text-xl font-bold text-gray-800">Taskman</span>
                </Link>

                <div className="flex items-center gap-2 sm:gap-3">
                    {
                        isSignedIn
                            ? (
                                <>
                                    <Link href="/dashboard">
                                        <Button variant="ghost" size="sm" className="text-xs sm:text-sm">
                                            Go To Dashboard
                                        </Button>
                                    </Link>
                                    <UserButton userProfileMode="navigation" userProfileUrl="/user-profile" />
                                </>
                            )
                            : (
                                <>
                                    <SignInButton>
                                        <Button variant="ghost" size="sm" className="text-xs sm:text-sm">
                                            Sign In
                                        </Button>
                                    </SignInButton>
                                    <SignUpButton>
                                        <Button size="sm" className="nav-btn-style text-xs sm:text-sm">
                                            Sign Up
                                        </Button>
                                    </SignUpButton>
                                </>
                            )
                    }
                </div>
            </div>
        </header>
    );
};