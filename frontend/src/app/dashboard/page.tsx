"use client";

import { useUser } from "@clerk/nextjs";

export default function DashboardPage() {
    const { user } = useUser();

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
                </div>
            </main>
        </div>
    );
};