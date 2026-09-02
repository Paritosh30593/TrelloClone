"use client";

import { useMsal } from "@azure/msal-react";
import { InteractionStatus } from "@azure/msal-browser";
import { useRouter } from "next/navigation";
import { useEffect } from "react";

export default function SignInOidcPage() {
    const { accounts, inProgress } = useMsal();
    const router = useRouter();

    useEffect(() => {
        if (inProgress !== InteractionStatus.None) return;
        router.replace(
            accounts.length > 0
                ? '/dashboard'
                : '/'
        );
    }, [inProgress, accounts, router]);

    return (
        <main className="flex items-center justify-center min-h-screen">
            <p className="text-gray-500">Signing you in&hellip;</p>
        </main>
    );
}
