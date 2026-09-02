"use client";

import { useEffect } from "react";
import { useRouter } from "next/navigation";
import { msalConfig } from "@/authConfig";
import { InteractionStatus, PublicClientApplication } from "@azure/msal-browser";
import { MsalProvider, useMsal } from "@azure/msal-react";

export const msalInstance = new PublicClientApplication(msalConfig);

// Keeps the msal.session cookie in sync with MSAL's account state so the
// server-side middleware can determine authentication without touching sessionStorage.
function MsalCookieSync() {
    const { accounts, inProgress } = useMsal();
    const router = useRouter();
    const hasAccounts = accounts.length > 0;

    useEffect(() => {
        if (inProgress !== InteractionStatus.None) return;

        // Avoid writing the cookie when it already reflects the current state —
        // each write invalidates Next.js's RSC router cache, triggering a refetch loop.
        const cookieIsSet = document.cookie.split(';').some(c => c.trim() === 'msal.session=1');
        if (cookieIsSet === hasAccounts) return;

        document.cookie = hasAccounts
            ? 'msal.session=1; path=/; SameSite=Lax'
            : 'msal.session=; path=/; Max-Age=0; SameSite=Lax';

        // Trigger a server-side re-evaluation so the middleware can redirect.
        router.refresh();
    }, [hasAccounts, inProgress, router]);

    return null;
}

export function MsalClientProvider({ children }: { children: React.ReactNode }) {
    return (
        <MsalProvider instance={msalInstance}>
            <MsalCookieSync />
            {children}
        </MsalProvider>
    );
}