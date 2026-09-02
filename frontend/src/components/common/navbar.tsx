"use client";

import { Filter, MoreHorizontal, SquareKanban } from "lucide-react";
import Link from "next/link";
import { Button } from "../ui/button";
import { usePathname } from "next/navigation";
import { Badge } from "../ui/badge";
import { AuthenticatedTemplate, UnauthenticatedTemplate, useMsal } from "@azure/msal-react";
import { loginRequest } from "@/authConfig";

type NavbarProps = {
    boardTitle?: string;
    setIsEditingTitle?: (isEditing: boolean) => void;
    onFilterClick?: () => void;
    filterCount?: number;
};

export const Navbar = ({ boardTitle, setIsEditingTitle, onFilterClick, filterCount = 0 }: NavbarProps) => {
    const pathname = usePathname();
    const { instance, accounts } = useMsal();
    const isSignedIn = accounts.length > 0;

    const isDashboardPage = pathname === "/dashboard" && isSignedIn;
    const isBoardsPage = pathname.includes("/boards/") && isSignedIn;

    const handleSignInRedirect = () => {
        instance
            .loginRedirect(loginRequest)
            .catch((error) => console.error("Login redirect error:", error));
    }

    const handleSignOutRedirect = () => {
        document.cookie = 'msal.session=; path=/; expires=Thu, 01 Jan 1970 00:00:00 GMT; SameSite=Lax';
        instance
            .logoutRedirect()
            .catch((error) => console.error("Logout redirect error:", error));
    }

    if (isDashboardPage) {
        return (
            <header className="border-b bg-white/50 backdrop-blur-sm sticky top-0 z-40">
                <div className="container mx-auto px-4 py-3 sm:py-4 flex items-center justify-between">
                    <Link href="/dashboard" className="flex items-center gap-2">
                        <SquareKanban className="h-5 w-5 sm:h-7 sm:w-7 text-purple-600" />
                        <span className="text-lg sm:text-xl font-bold text-gray-800">Taskman</span>
                    </Link>
                    <AuthenticatedTemplate>
                        <Button variant="ghost" size="sm" className="text-xs sm:text-sm" onClick={handleSignOutRedirect}>
                            Sign Out
                        </Button>
                    </AuthenticatedTemplate>
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
                        <AuthenticatedTemplate>
                            <Button variant="ghost" size="sm" className="text-xs sm:text-sm" onClick={handleSignOutRedirect}>
                                Sign Out
                            </Button>
                        </AuthenticatedTemplate>
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
                                    <AuthenticatedTemplate>
                                        <Button variant="ghost" size="sm" className="text-xs sm:text-sm" onClick={handleSignOutRedirect}>
                                            Sign Out
                                        </Button>
                                    </AuthenticatedTemplate>
                                </>
                            )
                            : (
                                <>
                                    <UnauthenticatedTemplate>
                                        <Button variant="ghost" size="sm" className="text-xs sm:text-sm" onClick={handleSignInRedirect}>
                                            Sign In
                                        </Button>
                                    </UnauthenticatedTemplate>
                                </>
                            )
                    }
                </div>
            </div>
        </header>
    );
};