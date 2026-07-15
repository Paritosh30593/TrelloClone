"use client";

import { useRouter } from "next/navigation";
import { startTransition } from "react";

export default function GlobalError({ error, reset }: {
    error: Error,
    reset: () => void
}) {
    const router = useRouter();

    const reloadPage = () => {
        startTransition(() => {
            router.refresh();
            reset();
        });
    }

    return (
        <html lang="en">
            <body>
                <div>{error.message}</div>
                <button type="button" className="btn" onClick={reloadPage}>Reload Page</button>
            </body>
        </html>
    );
}