"use client";

import { SignInButton, SignUpButton, UserButton, useUser } from "@clerk/nextjs";
import { Filter, MoreHorizontal, SquareKanban } from "lucide-react";
import Link from "next/link";
import { Button } from "../ui/button";
import { usePathname } from "next/navigation";
import { Badge } from "../ui/badge";

type NavbarProps = {
    boardTitle?: string;
    setIsEditingTitle?: (isEditing: boolean) => void;
    onFilterClick?: () => void;
    filterCount?: number;
};

export const Navbar = ({ boardTitle, setIsEditingTitle, onFilterClick, filterCount = 0 }: NavbarProps) => {
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
                        <Button variant="ghost" size="sm" className="h-7 w-7 shrink-0 p-0" onClick={() => setIsEditingTitle?.(true)}>
                            <MoreHorizontal />
                        </Button>
                    </div>

                    <div className="flex items-center gap-2 sm:gap-3">
                        {
                            // onFilterClick && (
                            <Button
                                size="sm"
                                onClick={onFilterClick}
                                className={`text-xs sm:text-sm 
                                    ${filterCount > 0
                                        ? "bg-purple-200 text-purple-700 hover:bg-purple-300"
                                        : "nav-btn-style"
                                    }`
                                }
                            >
                                <Filter className="h-3 w-3 sm:h-4 sm:w-4 mr-1 sm:mr-2" />
                                <span className="hidden sm:inline">Filter</span>
                                {
                                    filterCount > 0 && (
                                        <Badge variant="secondary" className="ml-1 sm:ml-2 text-purple-700 p-1.5 text-2xs sm:text-xs">{filterCount}</Badge>
                                    )
                                }
                            </Button>
                            //)
                        }
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